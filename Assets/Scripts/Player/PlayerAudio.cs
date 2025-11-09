using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Названия звуков в AudioManager")]
    [SerializeField] private string jumpSound = "Jump";
    [SerializeField] private string hitSound = "Hit";
    [SerializeField] private string attackSound = "Attack";
    [SerializeField] private string footstepSound = "Footstep";

    // Можно вызвать из других скриптов или анимаций
    public void PlayJump()
    {
        AudioManager.Instance.Play(jumpSound);
    }

    public void PlayHit()
    {
        AudioManager.Instance.Play(hitSound);
    }

    public void PlayAttack()
    {
        AudioManager.Instance.Play(attackSound);
    }

    public void PlayFootstep()
    {
        AudioManager.Instance.Play(footstepSound);
    }
}
