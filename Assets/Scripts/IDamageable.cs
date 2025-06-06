using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
    void Die();
    float GetHealth();
}
