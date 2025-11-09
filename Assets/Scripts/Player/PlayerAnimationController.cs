using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    private PlayerController2D player;

    private bool isDead = false; // блокировка анимаций после смерти

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController2D>();
    }

    void Update()
    {
        if (isDead) return;
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        float speed = Mathf.Abs(player.horizontalInput);
        anim.SetFloat("Speed", speed);                // ходьба/бег
        anim.SetBool("IsJumping", !player.IsGrounded); // прыжок
        anim.SetBool("IsRunning", player.IsRunning);   // бег
    }

    // Анимация смерти
    public void PlayDeathAnimation()
    {
        if (isDead) return;
        isDead = true;
        anim.SetTrigger("Death");
    }

    // Анимация получения урона
    public void PlayHitAnimation()
    {
        if (!isDead)
            anim.SetTrigger("Hit"); // триггер "Hit" в Animator
    }

    // Анимация атаки игрока
    public void PlayAttackAnimation()
    {
        if (!isDead)
            anim.SetTrigger("Attack"); // триггер "Attack" в Animator
    }
}
