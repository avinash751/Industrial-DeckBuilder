using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    public CardSupplyChain SupplyChain; // Acts as a pool of cards
    public int packSize = 5; // Number of cards in the final pack
    public Vector3 cardSpawnOffset = new Vector3(1, 0, 0); // Offset for spawning car

    private List<CardData> finalPack; // The final pack of cards to spawn
    private int currentCardIndex = 0; // Tracks which card to spawn next

    private void Start()
    {
        GenerateFinalPack();
    }

    private void GenerateFinalPack()
    {
        if (SupplyChain == null || SupplyChain.Cards.Count == 0)
        {
            Debug.LogError("SupplyChain is missing or empty!");
            return;
        }

        finalPack = new List<CardData>();

        // Randomly select cards from the SupplyChain to fill the final pack
        for (int i = 0; i < packSize; i++)
        {
            int randomIndex = Random.Range(0, SupplyChain.Cards.Count);
            finalPack.Add(SupplyChain.Cards[randomIndex]);
        }
    }

    private void OnMouseUp()
    {
        if (DragManager.IsDragging) return;
        SpawnNextCard();
    }

    private void SpawnNextCard()
    {
        if (finalPack != null && currentCardIndex < finalPack.Count)
        {
            CardData cardData = finalPack[currentCardIndex];
            Card card = CardFactory.Instance.CreateCard(cardData);

            card.transform.position = transform.position + (cardSpawnOffset);
            currentCardIndex++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}