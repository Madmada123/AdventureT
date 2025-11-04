using UnityEngine;

public class EnemyHealth : HealthBase
{
    public SpriteRenderer spriteRenderer;
    public Color hitColor = Color.red;
    public float hitFlashTime = 0.1f;
    private Color defaultColor;
    private bool isFlashing;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
            defaultColor = spriteRenderer.color;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(HitFlash());
    }

    protected override void Die()
    {
        Debug.Log($"{gameObject.name} погибает");
        var monster = GetComponent<Assets.FantasyMonsters.Scripts.Monster>();
        if (monster != null)
            monster.SetState(Assets.FantasyMonsters.Scripts.MonsterState.Death);

        Destroy(gameObject, 1.5f);
    }

    private System.Collections.IEnumerator HitFlash()
    {
        if (isFlashing || spriteRenderer == null) yield break;
        isFlashing = true;
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(hitFlashTime);
        spriteRenderer.color = defaultColor;
        isFlashing = false;
    }
}
