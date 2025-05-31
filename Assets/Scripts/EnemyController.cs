using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{

    public Animator animator;
    public float maxHealth;
    public float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die ();
        }
    }

    public void Die(){
        animator.SetTrigger("isDead");
    }

    public float GetHealth(){
        return currentHealth;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
