using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Настройки движения")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;

    [Header("Проверка земли")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Джойстик")]
    public Joystick joystick; // твой UI джойстик
    public bool useJoystick = true;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool jumpPressed = false;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public bool IsGrounded => isGrounded;
    [HideInInspector] public bool IsRunning => Input.GetKey(KeyCode.LeftShift);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround();
        GetInput();
        Move();
        Jump();
    }

    void GetInput()
    {
        if (useJoystick && joystick != null)
        {
            horizontalInput = joystick.Horizontal;
        }
        else
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
    }

    public void OnJumpButtonPressed()
    {
        // Эта функция будет вызываться кнопкой UI
        jumpPressed = true;
    }

    void Move()
    {
        float currentSpeed = IsRunning ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(horizontalInput * currentSpeed, rb.velocity.y);

        if (horizontalInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);
    }

    void Jump()
    {
        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpPressed = false; // сбрасываем флаг после прыжка
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
}
