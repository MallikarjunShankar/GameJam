using UnityEngine;

public class PlayerStats : MonoBehaviour {
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    private void Awake() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Player HP: " + currentHealth + "/" + maxHealth);

        if (currentHealth <= 0) {
            Die();
        }
    }

    public void Heal(int amount) {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        Debug.Log("Player HP: " + currentHealth + "/" + maxHealth);
    }

    private void Die() {
        Debug.Log("Player died.");
    }
}