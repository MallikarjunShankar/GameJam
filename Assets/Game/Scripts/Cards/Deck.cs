using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<CardData> startingDeck = new();

    private List<CardData> drawPile = new();
    private List<CardData> discardPile = new();

    private void Awake()
    {
        drawPile.AddRange(startingDeck);
        Shuffle(drawPile);
    }

    private void Shuffle(List<CardData> cards)
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            CardData temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public CardData DrawCard()
    {
        if (drawPile.Count == 0)
        {
            ReshuffleDiscardPile();
        }

        if (drawPile.Count == 0)
        {
            return null;
        }

        CardData card = drawPile[0];
        drawPile.RemoveAt(0);

        return card;
    }

    public void DiscardCard(CardData card)
    {
        if (card != null)
        {
            discardPile.Add(card);
        }
    }

    private void ReshuffleDiscardPile()
    {
        if (discardPile.Count == 0)
        {
            return;
        }

        drawPile.AddRange(discardPile);
        discardPile.Clear();

        Shuffle(drawPile);
    }
}