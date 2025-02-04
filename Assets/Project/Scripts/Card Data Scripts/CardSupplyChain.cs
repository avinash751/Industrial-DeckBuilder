using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSupplyChain", menuName = "Cards/CardSupplyChain")]
public class CardSupplyChain : ScriptableObject
{
    public string ChainName; 
    public List<CardData> Cards;
}