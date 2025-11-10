using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossController : MonoBehaviour
{
    [Header("Параметры движения")]
    public float walkSpeed = 2f;
    public float runSpeed = 3.5f;
    public float jumpForce = 6f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    [Header("ИИ-поведение")]
    public float directionChangeInterval = 3f;
    public float jumpChance = 0.3f;

    [Header("Атака фаерболами")]
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float attackCooldown = 3f;

    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isAttacking;

    private Rigidbody2D rb;
    private float moveDirection = 1f;
    private float changeTimer;
    private float attackTimer;
    private bool isGrounded;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        changeTimer = directionChangeInterval;
        attackTimer = attackCooldown;
    }

    void Update()
    {
        if (isDead) return;

        CheckGround();
        HandleMovement();
        Flip();

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            AttackWithFireball();
            attackTimer = attackCooldown;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isJumping = !isGrounded;
    }

    void HandleMovement()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0f)
        {
            moveDirection *= -1;
            changeTimer = Random.Range(directionChangeInterval * 0.7f, directionChangeInterval * 1.3f);

            if (isGrounded && Random.value < jumpChance)
                Jump();
        }

        isRunning = Random.value > 0.5f;
        float speed = isRunning ? runSpeed : walkSpeed;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
    }

    void Flip()
    {
        if ((moveDirection > 0 && !isFacingRight) || (moveDirection < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void Attack()
    {
        if (isDead) return;
        isAttacking = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }

    public void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    public void AttackWithFireball()
    {
        if (isDead) return;

        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            GameObject fb = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
            Fireball fireball = fb.GetComponent<Fireball>();

            if (fireball != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    Vector2 dir = (player.transform.position - fireballSpawnPoint.position).normalized;
                    fireball.Shoot(dir);
                }
            }
        }

        // Анимация атаки
        GetComponent<BossAnimationController>().PlayAttack();
    }
}
