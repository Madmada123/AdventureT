using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : HealthBase
{
    [Header("Настройки игрока")]
    public SpriteRenderer spriteRenderer;
    public Color hitColor = Color.red;
    public float hitFlashTime = 0.1f;
    public float knockbackForce = 5f;

    private PlayerAnimationController animController;
    private Color _defaultColor;
    private bool _isFlashing;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
            _defaultColor = spriteRenderer.color;

        animController = GetComponent<PlayerAnimationController>();
    }

    // Враг вызывает этот метод с указанием позиции атакующего
    public void TakeDamageFromEnemy(float damage, Vector2 attackerPosition)
    {
        // Моргаем и запускаем анимацию удара
        StartCoroutine(HitFlashCoroutine());

        if (animController != null)
            animController.PlayHitAnimation();

        // Отбрасывание игрока
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = ((Vector2)transform.position - attackerPosition).normalized;
            rb.velocity = direction * knockbackForce;
        }

        // Уменьшаем здоровье
        TakeDamage(damage);
    }

    private IEnumerator HitFlashCoroutine()
    {
        if (_isFlashing || spriteRenderer == null) yield break;

        _isFlashing = true;
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(hitFlashTime);
        spriteRenderer.color = _defaultColor;
        _isFlashing = false;
    }

    protected override void Die()
    {
        Debug.Log("Игрок погиб 💀");

        // Анимация смерти
        if (animController != null)
            animController.PlayDeathAnimation();

        // Отключаем управление
        var controller = GetComponent<PlayerController2D>();
        if (controller != null)
            controller.enabled = false;

        // Перезапуск сцены
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Вызов для атаки игрока (например, по кнопке UI)
    public void Attack()
    {
        if (animController != null)
            animController.PlayAttackAnimation();
        // Здесь можно добавить логику нанесения урона врагам
    }
    // PlayerHealth.cs
    public void PerformAttack()
    {
        Debug.Log("Игрок атакует!");

        // Проверяем врагов в радиусе атаки
        float attackRadius = 1.5f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                var enemyHealth = hit.GetComponent<HealthBase>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(10); // наносим урон
                }
            }
        }
    }

}
