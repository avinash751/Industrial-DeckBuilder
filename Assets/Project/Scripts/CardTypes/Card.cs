using CustomInspector;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour, ISellable
{
    [FromChildren][SerializeField] protected SpriteRenderer cardIcon;
    [FromChildren][SerializeField] protected TextMeshPro cardNameText;
    [FromChildren][SerializeField] protected TextMeshPro cardTypeText;
    [FromChildren][SerializeField] protected ConnectorSpawner connectorSpawner;
    int sellValue;

    public virtual void InitializeCard(CardData data)
    {
        cardIcon.sprite = data.CardIcon;
        cardIcon.color = data.IconColor;                                 
        cardNameText.text = data.CardName;     
        sellValue = data.sellValue;
        connectorSpawner = GetComponentInChildren<ConnectorSpawner>();         
        if (connectorSpawner != null)
        {
            connectorSpawner.InitializeConnectors(data);
        }
    }

    public void SellObject()
    {
        Destroy(gameObject);
        MoneyManager.Instance.AddMoney(sellValue);
    }
}
