using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Combat References")]
    [SerializeField] private PlayerStats player;
    [SerializeField] private List<EnemyStats> enemies = new();
    [SerializeField] private Hand hand;

    [Header("Energy")]
    [SerializeField] private int maxEnergy = 3;

    [Header("Cards")]
    [SerializeField] private int cardsPerTurn = 5;

    private int currentEnergy;
    private bool playerTurn;
    private bool combatOver;

    public int MaxEnergy => maxEnergy;
    public int CurrentEnergy => currentEnergy;
    public bool IsPlayerTurn => playerTurn;
    public EnemyStats CurrentEnemy { get; private set; }

    private void Start()
    {
        StartCombat();
    }

    public void StartCombat()
    {
        if (player == null || enemies.Count == 0 || hand == null)
        {
            Debug.LogError("CombatManager is missing a reference.");
            return;
        }

        combatOver = false;

        CurrentEnemy = GetFirstLivingEnemy();

        if (CurrentEnemy == null)
        {
            Debug.LogError("No living enemies found.");
            return;
        }

        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        if (combatOver)
            return;

        playerTurn = true;
        currentEnergy = maxEnergy;

        hand.DrawCards(cardsPerTurn);

        Debug.Log("Player Turn - Energy: " + currentEnergy);
    }

    public void EndPlayerTurn()
    {
        if (combatOver || !playerTurn)
            return;

        playerTurn = false;

        Debug.Log("Player Turn Ended.");

        hand.DiscardHand();

        EnemyTurn();
    }

    private void EnemyTurn()
    {
        if (combatOver)
            return;

        Debug.Log("Enemy Turn.");

        foreach (EnemyStats enemy in enemies)
        {
            if (enemy == null || enemy.CurrentHealth <= 0)
                continue;

            enemy.Attack(player);

            if (player.CurrentHealth <= 0)
            {
                combatOver = true;
                return;
            }
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

    public bool TryPlayCard(CardData card)
    {
        if (!playerTurn || combatOver)
            return false;

        if (!CanSpendEnergy(card.EnergyCost))
        {
            Debug.Log("Not enough energy to play " + card.CardName);
            return false;
        }

        SpendEnergy(card.EnergyCost);

        if (card.Type == CardType.Attack)
        {
            if (CurrentEnemy == null || CurrentEnemy.CurrentHealth <= 0)
            {
                CurrentEnemy = GetFirstLivingEnemy();
            }

            if (CurrentEnemy == null)
            {
                Debug.Log("No living enemies remain.");
                return false;
            }

            CurrentEnemy.TakeDamage(card.EffectValue);

            if (CurrentEnemy.CurrentHealth <= 0)
            {
                Debug.Log("Defeated: " + CurrentEnemy.name);

                CurrentEnemy = GetFirstLivingEnemy();

                if (CurrentEnemy != null)
                {
                    Debug.Log("New target: " + CurrentEnemy.name);
                }
                else
                {
                    combatOver = true;
                    playerTurn = false;

                    Debug.Log("Combat Won!");
                }
            }
        }
        else if (card.Type == CardType.Defense)
        {
            player.AddBlock(card.EffectValue);

            Debug.Log("Defend played. Block: " + player.CurrentBlock);
        }
        else if (card.Type == CardType.Utility)
        {
            player.Heal(card.EffectValue);

            Debug.Log("Heal played.");
        }

        return true;
    }

    private EnemyStats GetFirstLivingEnemy()
    {
        foreach (EnemyStats enemy in enemies)
        {
            if (enemy != null && enemy.CurrentHealth > 0)
                return enemy;
        }

        return null;
    }
}