using UnityEngine;
using Assets.FantasyMonsters.Scripts;

[RequireComponent(typeof(Monster))]
public class EnemyController : MonoBehaviour
{
    private Monster monster;
    private Transform player;

    [Header("Параметры движения")]
    public float moveSpeed = 2f;
    public float runSpeed = 4f;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;
    public float patrolChangeInterval = 3f;

    private float patrolTimer;
    private int patrolDirection = 1;
    private bool isPlayerDetected;

    void Awake()
    {
        monster = GetComponent<Monster>();
        player = GameObject.FindWithTag("Player")?.transform;
        patrolTimer = patrolChangeInterval;
    }

    void Update()
    {
        if (player == null)
        {
            Patrol();
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            isPlayerDetected = true;

            if (distance > attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            isPlayerDetected = false;
            Patrol();
        }
    }

    void AttackPlayer()
    {
        // Запускаем триггер атаки через аниматор монстра
        if (monster.Animator && !monster.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            monster.Animator.SetTrigger("Attack");
            Debug.Log($"[ENEMY ATTACK] {gameObject.name} запускает анимацию атаки!");
        }
    }


    void Patrol()
    {
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0)
        {
            patrolDirection *= -1;
            patrolTimer = patrolChangeInterval + Random.Range(-1f, 1f);
        }

        transform.position += new Vector3(patrolDirection * moveSpeed * Time.deltaTime, 0, 0);
        transform.localScale = new Vector3(patrolDirection, 1, 1);
        monster.SetState(MonsterState.Walk);
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * runSpeed * Time.deltaTime;

        if (direction.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);

        monster.SetState(MonsterState.Run);
    }
}
