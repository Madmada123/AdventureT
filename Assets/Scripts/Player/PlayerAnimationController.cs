using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    private PlayerController2D player;

    private bool isDead = false; // чтобы анимации не срабатывали после смерти

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController2D>();
    }

    void Update()
    {
        if (isDead) return; // блокируем обновление после смерти
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        float speed = Mathf.Abs(player.horizontalInput);
        anim.SetFloat("Speed", speed);          // ходьба
        anim.SetBool("IsJumping", !player.IsGrounded); // прыжок
        anim.SetBool("IsRunning", player.IsRunning);   // бег
    }

    public void PlayDeathAnimation()
    {
        isDead = true;
        anim.SetTrigger("Death"); // в Animator добавь триггер "Death"
    }
}
