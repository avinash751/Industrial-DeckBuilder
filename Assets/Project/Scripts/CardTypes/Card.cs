using CustomInspector;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [FromChildren][SerializeField] protected SpriteRenderer cardIcon;
    [FromChildren][SerializeField] protected TextMeshPro cardNameText;
    [FromChildren][SerializeField] protected TextMeshPro cardTypeText;
    public virtual void InitializeCard(CardData data)
    {
        cardIcon.sprite = data.CardIcon;
        cardIcon.color = data.IconColor;
        cardNameText.text = data.CardName;
    }
}
