using CustomInspector;
using UnityEngine;

public class CardData : ScriptableObject
{
    [HorizontalLine("Base Card Data Settings", 3, FixedColor.DustyBlue)]
    public string CardName;
    public int sellValue;
    [Multiline] public string CardDescription;
    [Preview] public Sprite CardIcon;
    public Color IconColor;
    [Preview] public GameObject CardPrefab;
}
