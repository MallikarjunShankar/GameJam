using UnityEngine;

public class HandUI : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform handContainer;
    [SerializeField] private Hand hand;

    private void Start()
    {
        hand.OnHandChanged.AddListener(RefreshHand);

        RefreshHand();
    }

    public void RefreshHand()
    {
        foreach (Transform child in handContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < hand.CardsInHand.Count; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, handContainer);

            CardUI cardUI = cardObject.GetComponent<CardUI>();

            if (cardUI != null)
            {
                cardUI.Setup(
                    hand.CardsInHand[i],
                    i,
                    hand
                );
            }
        }
    }
}