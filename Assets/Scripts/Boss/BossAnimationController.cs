using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossAnimationController : MonoBehaviour
{
    private Animator anim;
    private BossController boss;

    void Start()
    {
        anim = GetComponent<Animator>();
        boss = GetComponent<BossController>();
    }

    void Update()
    {
        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        anim.SetBool("IsRunning", boss.isRunning);
        anim.SetBool("IsJumping", boss.isJumping);
        anim.SetBool("IsDead", boss.isDead);

        if (boss.isAttacking)
            anim.SetTrigger("Attack");
    }

    // Методы для внешнего вызова
    public void PlayHit()
    {
        if (!boss.isDead)
            anim.SetTrigger("Hit");
    }

    public void PlayDeath()
    {
        anim.SetBool("IsDead", true);
    }

    public void PlayAttack()
    {
        if (!boss.isDead)
            anim.SetTrigger("Attack");
    }
}
