using UnityEngine;

public enum CardType
{
    Attack,
    Defense,
    Utility
}

[CreateAssetMenu(fileName = "NewCard", menuName = "Game/Cards/Card")]
public class CardData : ScriptableObject
{
    [Header("Basic Information")]
    [SerializeField] private string cardName;
    [TextArea]
    [SerializeField] private string description;

    [Header("Card Properties")]
    [SerializeField] private CardType cardType;
    [SerializeField] private int energyCost = 1;

    [Header("Effect")]
    [SerializeField] private int effectValue = 10;

    public string CardName => cardName;
    public string Description => description;
    public CardType Type => cardType;
    public int EnergyCost => energyCost;
    public int EffectValue => effectValue;
}