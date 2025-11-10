using UnityEngine;

public class BossHealth : HealthBase
{
    private BossAnimationController anim;
    private bool isDead = false;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<BossAnimationController>();
    }

    public override void TakeDamage(float damage)
    {
        if (isDead) return;

        base.TakeDamage(damage);

        if (currentHealth > 0)
        {
            anim.PlayHit(); // босса задели
        }
    }

    protected override void Die()
    {
        if (isDead) return;

        isDead = true;
        anim.PlayDeath();
        Debug.Log("Босс побеждён 💀");

        var bossController = GetComponent<BossController>();
        if (bossController != null)
            bossController.Die(); // останавливаем движения и атаки

        // Можно добавить эффекты, дроп, катсцену и т.д.
        // Destroy(gameObject, 3f);
    }
}
