using UnityEngine;
using Assets.FantasyMonsters.Scripts;

[RequireComponent(typeof(Monster))]
public class EnemyAnimatorDiagnostic : MonoBehaviour
{
    private Monster monster;
    private Animator anim;

    void Start()
    {
        monster = GetComponent<Monster>();
        anim = monster.Animator;

        Debug.Log($"[Diag] Monster assigned: {monster != null}");
        Debug.Log($"[Diag] Animator assigned: {anim != null}");
        if (anim == null) return;

        Debug.Log($"[Diag] Controller: {anim.runtimeAnimatorController?.name ?? "NULL"}");
        Debug.Log($"[Diag] animator.enabled = {anim.enabled}, speed = {anim.speed}, updateMode = {anim.updateMode}, cullingMode = {anim.cullingMode}");

        for (int i = 0; i < anim.layerCount; i++)
            Debug.Log($"[Diag] Layer {i} name={anim.GetLayerName(i)} weight={anim.GetLayerWeight(i)}");

        Debug.Log("[Diag] Params:");
        foreach (var p in anim.parameters)
            Debug.Log($"[Diag] param: name={p.name}, type={p.type}");

        var info = anim.GetCurrentAnimatorStateInfo(0);
        string tag = "Idle";
        Debug.Log($"[Diag] CurrentState: fullPathHash={info.fullPathHash}, normalizedTime={info.normalizedTime}, isTag('{tag}')={info.IsTag(tag)}");

        // Проверим установку параметра
        anim.SetInteger("State", 2);
        Debug.Log($"[Diag] After SetInteger State=2, GetInteger returns {anim.GetInteger("State")}");

        // Принудительно проигрываем стейт
        try
        {
            anim.Play("Walk");
            Debug.Log("[Diag] anim.Play(\"Walk\") executed");
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("[Diag] anim.Play(\"Walk\") threw: " + e);
        }

        // Проверка клипов
        var clips = anim.runtimeAnimatorController.animationClips;
        foreach (var c in clips)
            Debug.Log($"[Diag] clip: {c.name}, length={c.length}");
    }

    void Update()
    {
        if (anim == null) return;
        float speed = anim.HasParameter("Speed") ? anim.GetFloat("Speed") : -1f;
        Debug.Log($"[Diag UPDATE] StateInt={anim.GetInteger("State")}, SpeedFloat={speed}, Animator.enabled={anim.enabled}");
    }
}

public static class AnimatorExtensions
{
    public static bool HasParameter(this Animator animator, string paramName)
    {
        foreach (var p in animator.parameters)
            if (p.name == paramName)
                return true;
        return false;
    }
}
