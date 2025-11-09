using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [Header("Названия звуков в AudioManager")]
    [SerializeField] private string moveSound = "EnemyMove";
    [SerializeField] private string attackSound = "EnemyAttack";
    [SerializeField] private string deathSound = "EnemyDeath";

    // Можно вызывать из скрипта AI или анимации

    public void PlayMove()
    {
        AudioManager.Instance.Play(moveSound);
    }

    public void PlayAttack()
    {
        AudioManager.Instance.Play(attackSound);
    }

    public void PlayDeath()
    {
        AudioManager.Instance.Play(deathSound);
    }
}
