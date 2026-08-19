using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hand : MonoBehaviour
{
    [SerializeField] private Deck deck;
    [SerializeField] private int startingHandSize = 5;
    [SerializeField] private CombatManager combatManager;
    private List<CardData> cardsInHand = new();

    public IReadOnlyList<CardData> CardsInHand => cardsInHand;

    public UnityEvent OnHandChanged = new UnityEvent();

    private void Start()
    {
    }

    private void DrawStartingHand()
    {
        DrawCards(startingHandSize);

        PrintHand();
        OnHandChanged.Invoke();
    }

    public void DrawCard()
    {
        CardData card = deck.DrawCard();

        if (card == null)
        {
            Debug.Log("No cards available to draw.");
            return;
        }

        cardsInHand.Add(card);
    }

    public void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            DrawCard();
        }

        PrintHand();
        OnHandChanged.Invoke();
    }

    public void PlayCard(int index)
    {
        if (index < 0 || index >= cardsInHand.Count)
        {
            Debug.LogWarning("Invalid card index.");
            return;
        }

        CardData card = cardsInHand[index];

        if (!combatManager.TryPlayCard(card))
        {
            return;
        }

        cardsInHand.RemoveAt(index);

        deck.DiscardCard(card);

        Debug.Log("Played: " + card.CardName);

        OnHandChanged.Invoke();

        PrintHand();
    }

    public void DiscardHand()
    {
        foreach (CardData card in cardsInHand)
        {
            deck.DiscardCard(card);
        }

        cardsInHand.Clear();

        Debug.Log("Hand discarded.");

        OnHandChanged.Invoke();
    }

    private void PrintHand()
    {
        Debug.Log("Current Hand:");

        foreach (CardData card in cardsInHand)
        {
            Debug.Log("- " + card.CardName);
        }
    }
}