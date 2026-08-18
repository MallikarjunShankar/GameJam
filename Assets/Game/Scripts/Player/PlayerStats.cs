using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private int currentBlock;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public int CurrentBlock => currentBlock;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        int remainingDamage = damage;

        if (currentBlock > 0)
        {
            int blocked = Mathf.Min(currentBlock, damage);

            currentBlock -= blocked;
            remainingDamage -= blocked;

            Debug.Log("Blocked: " + blocked);
        }

        if (remainingDamage > 0)
        {
            currentHealth -= remainingDamage;
            currentHealth = Mathf.Max(currentHealth, 0);
        }

        Debug.Log(
            "Player HP: " + currentHealth + "/" + maxHealth +
            " | Block: " + currentBlock
        );

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AddBlock(int amount)
    {
        currentBlock += amount;

        Debug.Log("Block: " + currentBlock);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        Debug.Log("Player HP: " + currentHealth + "/" + maxHealth);
    }

    public void ResetBlock()
    {
        currentBlock = 0;
    }

    private void Die()
    {
        Debug.Log("Player died.");
    }
}