using CustomInspector;
using UnityEngine;

public class CardData : ScriptableObject
{
    [HorizontalLine("Base Card Data Settings", 3, FixedColor.DustyBlue)]
    public string CardName;
    [Multiline] public string CardDescription;
    [Preview] public Sprite CardIcon;
    public Color IconColor;
    [Preview] public GameObject CardPrefab;

    [HorizontalLine("Base Card Stats", 3, FixedColor.Green)]
    public float MonthlyUpKeepCost = 0f;
    public int SellValue;
}
