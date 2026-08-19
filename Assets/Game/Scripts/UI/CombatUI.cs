using TMPro;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStats player;
    [SerializeField] private EnemyStats enemy;
    [SerializeField] private CombatManager combatManager;

    [Header("UI")]
    [SerializeField] private TMP_Text playerHPText;
    [SerializeField] private TMP_Text playerBlockText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text enemyHPText;

    private void Update()
    {
        if (player == null || enemy == null || combatManager == null)
            return;

        playerHPText.text =
            "Player HP: " + player.CurrentHealth + "/" + player.MaxHealth;

        playerBlockText.text =
            "Block: " + player.CurrentBlock;

        energyText.text =
            "Energy: " + combatManager.CurrentEnergy + "/" + combatManager.MaxEnergy;

        enemyHPText.text =
            "Enemy HP: " + enemy.CurrentHealth + "/" + enemy.MaxHealth;
    }
}