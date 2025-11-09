using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttack : AttackBase
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Вызывается кнопкой или вводом игрока
    public void Attack()
    {
        if (!CanAttack()) return;

        // Запускаем анимацию атаки
        if (anim != null)
            anim.SetTrigger("Attack"); // имя триггера должно совпадать с Animator
    }

    // Этот метод вызывается **через Animation Event** в анимации атаки
    public void SpawnFireball()
    {
        if (fireballPrefab != null && firePoint != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
            fireball.GetComponent<FireballProjectile>().Init(damage);
        }
    }
}
