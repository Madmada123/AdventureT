using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthBase
{
    [Header("Настройки игрока")]
    public SpriteRenderer spriteRenderer;
    public Color hitColor = Color.red;
    public float hitFlashTime = 0.1f;

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

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(HitFlash());
    }

    protected override void Die()
    {
        Debug.Log("Игрок погиб 💀");

        // Воспроизводим анимацию смерти
        if (animController != null)
            animController.PlayDeathAnimation();

        // Выключаем управление
        var controller = GetComponent<PlayerController2D>();
        if (controller != null)
            controller.enabled = false;

        // Перезапускаем сцену
        StartCoroutine(RestartLevel());
    }

    private System.Collections.IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private System.Collections.IEnumerator HitFlash()
    {
        if (_isFlashing || spriteRenderer == null) yield break;

        _isFlashing = true;
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(hitFlashTime);
        spriteRenderer.color = _defaultColor;
        _isFlashing = false;
    }
}
