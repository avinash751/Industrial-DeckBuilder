
using System;
using UnityEngine;
using System.Collections.Generic;

public class CardFactory : MonoBehaviour
{
    public static CardFactory Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        
    }

    public Card CreateCard(CardData data)
    {
        if (data != null || data.CardPrefab != null)
        {
            GameObject cardObject = Instantiate(data.CardPrefab);
            Card card = cardObject.GetComponent<Card>();
            card.InitializeCard(data);
            card.gameObject.name = data.CardName;
            return card;
        }

        throw new ArgumentException("Invalid card data");
    }
}
