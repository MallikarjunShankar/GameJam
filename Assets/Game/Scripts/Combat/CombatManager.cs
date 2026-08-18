using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Combat References")]
    [SerializeField] private PlayerStats player;
    [SerializeField] private EnemyStats enemy;
    [SerializeField] private Hand hand;

    [Header("Energy")]
    [SerializeField] private int maxEnergy = 3;

    private int currentEnergy;
    private bool playerTurn;
    private bool combatOver;

    public int MaxEnergy => maxEnergy;
    public int CurrentEnergy => currentEnergy;
    public bool IsPlayerTurn => playerTurn;

    private void Start()
    {
        StartCombat();
    }

    public void StartCombat()
    {
        if (player == null || enemy == null || hand == null)
        {
            Debug.LogError("CombatManager is missing a reference.");
            return;
        }

        combatOver = false;
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        if (combatOver)
            return;

        playerTurn = true;
        currentEnergy = maxEnergy;

        Debug.Log("Player Turn - Energy: " + currentEnergy);
    }

    public void EndPlayerTurn()
    {
        if (combatOver || !playerTurn)
            return;

        playerTurn = false;

        Debug.Log("Player Turn Ended.");

        EnemyTurn();
    }

    private void EnemyTurn()
    {
        if (combatOver)
            return;

        Debug.Log("Enemy Turn.");

        enemy.Attack(player);

        if (player.CurrentHealth <= 0)
        {
            combatOver = true;
            return;
        }

        player.ResetBlock();

        StartPlayerTurn();
    }

    public bool CanSpendEnergy(int amount)
    {
        return playerTurn && amount <= currentEnergy;
    }

    public void SpendEnergy(int amount)
    {
        if (!CanSpendEnergy(amount))
        {
            Debug.LogWarning("Not enough energy.");
            return;
        }

        currentEnergy -= amount;

        Debug.Log("Energy: " + currentEnergy + "/" + maxEnergy);
    }
}