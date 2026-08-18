using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text cardText;
    [SerializeField] private Button button;

    private int cardIndex;
    private Hand hand;

    public void Setup(CardData card, int index, Hand handReference)
    {
        cardIndex = index;
        hand = handReference;

        cardText.text =
            card.CardName + "\n" +
            "Cost: " + card.EnergyCost + "\n" +
            card.Description;

        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(PlayCard);
    }

    private void PlayCard()
    {
        hand.PlayCard(cardIndex);
    }
}