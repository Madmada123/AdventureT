using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackRadius = 1.5f;

    private EnemyController enemyController;

    void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    // Вызываем через Animation Event
    public void PerformAttack()
    {
        Debug.Log($"⚔️ {enemyController.name} пытается атаковать!");

        Collider2D[] hits = Physics2D.OverlapCircleAll(enemyController.transform.position, attackRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    // Передаем позицию врага для отбрасывания
                    playerHealth.TakeDamageFromEnemy(enemyController.GetComponent<EnemyAttack>().damage,
                                                    enemyController.transform.position);

                    Debug.Log($"⚔️ {enemyController.name} нанес {damage} урона игроку!");
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (enemyController != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(enemyController.transform.position, attackRadius);
        }
    }
}
