using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Fireball : MonoBehaviour
{
    [Header("Настройки фаербола")]
    public float speed = 5f;
    public float damage = 10f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // чтобы не падал
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    // Выстрел в нужном направлении
    public void Shoot(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
        // Разворачиваем фаербол по направлению движения
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Попал в игрока
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerHealth player))
                player.TakeDamageFromEnemy(damage, transform.position); // <-- вот здесь моргание и отбрасывание

            Destroy(gameObject);
        }

        // Врезался в землю или что-то твёрдое
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
