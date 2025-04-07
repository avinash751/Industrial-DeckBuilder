using CustomInspector;
using UnityEngine;
using TMPro;
using System;

public abstract class Card : MonoBehaviour, ISellable
{
    [FromChildren][SerializeField] protected SpriteRenderer cardIcon;
    [FromChildren][SerializeField] protected TextMeshPro cardNameText;
    [FromChildren][SerializeField] protected ConnectorSpawner connectorSpawner;
    int sellValue;
    float monthlyUpkeepCost;
    public Action<float> OnMoneyDeducted;
    public bool canSell;
    string cantSellAudi0 = "Cancel&Deny";

    public virtual void InitializeCard(CardData data)
    {
        cardIcon.sprite = data.CardIcon;
        cardIcon.color = data.IconColor;

        cardNameText.text = data.CardName;
        sellValue = data.SellValue;
        monthlyUpkeepCost = data.MonthlyUpKeepCost;
        connectorSpawner = GetComponentInChildren<ConnectorSpawner>();
        if (connectorSpawner != null)
        {
            connectorSpawner.InitializeConnectors(data);
        }
    }

    protected abstract void HandleEndOfMonthPayment();

    protected void PayUpkeepCost()
    {
        if (MoneyManager.Instance == null)
        {
            Debug.LogError("MoneyManager.Instance is null! Cannot deduct upkeep cost.");
        }
        MoneyManager.Instance.SubtractMoney(monthlyUpkeepCost);
        OnMoneyDeducted?.Invoke(monthlyUpkeepCost);
        Debug.Log($"{cardNameText.text} paid upkeep cost of ${monthlyUpkeepCost}");
    }

    public bool SellObject()
    {
        if (!canSell)
        {
            AudioManager.Instance?.PlayAudio(cantSellAudi0);
            return false;
        }
        Destroy(gameObject);
        MoneyManager.Instance.AddMoney(sellValue);
        return true;
    }

    public virtual CardData GetCardData()
    {
        return null;
    }
}
