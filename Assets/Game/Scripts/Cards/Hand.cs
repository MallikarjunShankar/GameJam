using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Deck deck;
    [SerializeField] private int startingHandSize = 5;

    private List<CardData> cardsInHand = new();

    public IReadOnlyList<CardData> CardsInHand => cardsInHand;

    private void Start()
    {
        DrawStartingHand();
    }

    private void DrawStartingHand()
    {
        for (int i = 0; i < startingHandSize; i++)
        {
            DrawCard();
        }

        PrintHand();
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

    public void PlayCard(int index)
    {
        if (index < 0 || index >= cardsInHand.Count)
        {
            Debug.LogWarning("Invalid card index.");
            return;
        }

        CardData card = cardsInHand[index];

        cardsInHand.RemoveAt(index);
        deck.DiscardCard(card);

        Debug.Log("Played: " + card.CardName);

        PrintHand();
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