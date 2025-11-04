using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float attackCooldown = 1f;
    protected float lastAttackTime;

    protected bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackCooldown;
    }

    protected void DealDamage(IDamageable target)
    {
        target.TakeDamage(damage);
        lastAttackTime = Time.time;
    }
}
