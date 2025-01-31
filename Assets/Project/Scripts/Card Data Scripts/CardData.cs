using CustomInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    [HorizontalLine("Base Card Data Settings",3,FixedColor.DustyBlue)]
    public string CardName;
    public string CardDescription;
    public Sprite CardIcon;
    public Color CardBackgroundColor;
    public GameObject CardPrefab;
}
