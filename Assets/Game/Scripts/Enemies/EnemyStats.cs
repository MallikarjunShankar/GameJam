using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 50;

    [Header("Attack")]
    [SerializeField] private int attackDamage = 10;

    private int currentHealth;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public int AttackDamage => attackDamage;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Enemy HP: " + currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack(PlayerStats player)
    {
        if (player == null)
        {
            Debug.LogWarning("Enemy has no player target.");
            return;
        }

        Debug.Log("Enemy attacks for " + attackDamage + " damage.");

        player.TakeDamage(attackDamage);
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
    }
}