using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private float damage;
    private float speed = 10f; // можно вынести в SerializeField
    private Vector2 direction;

    public void Init(float damage)
    {
        this.damage = damage;
        direction = transform.right; // если у тебя спрайт направлен вправо
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
