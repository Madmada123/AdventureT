using UnityEngine;

public abstract class HealthBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} получил {damage} урона. Осталось {currentHealth} HP");

        if (currentHealth <= 0)
            Die();
    }

    protected abstract void Die();
}
