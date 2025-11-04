using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    private EnemyController enemyController;

    void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что столкновение с игроком
        if (other.CompareTag("Player"))
        {
            // Проверяем, что не сам себя бьёт
            if (enemyController != null && other.gameObject != enemyController.gameObject)
            {
                var health = other.GetComponent<HealthBase>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    Debug.Log($"⚔️ Враг {enemyController.name} нанёс {damage} урона игроку!");
                }
            }
        }
    }
}
