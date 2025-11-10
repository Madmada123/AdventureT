using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhotoCharacterPhysics : MonoBehaviour
{
    [Header("Настройки движений")]
    public float jumpForce = 2f;
    public float moveForce = 1f;
    public float flyForce = 0.5f;
    public float changeDirectionTime = 2f;

    private Rigidbody2D rb;
    private float timer;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // можно уменьшить, если хочешь, чтобы "летали" легко
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Начальное направление движения
        moveDirection = new Vector2(Random.Range(-1f, 1f), 0);
        timer = Random.Range(0f, changeDirectionTime);
    }

    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0f)
        {
            // Меняем направление движения случайно
            moveDirection = new Vector2(Random.Range(-1f, 1f), 0);
            timer = changeDirectionTime + Random.Range(-1f, 1f);
        }

        // Двигаемся влево-вправо
        rb.AddForce(new Vector2(moveDirection.x * moveForce, 0), ForceMode2D.Force);

        // Прыжки или полёт
        if (Random.value < 0.01f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        // Лёгкое “плавание” вверх-вниз для эффекта полёта
        rb.AddForce(new Vector2(0, Mathf.Sin(Time.time * 2f) * flyForce), ForceMode2D.Force);
    }
}
