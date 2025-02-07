using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;



    private void Start()
    {
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.OnMoneyChanged += HandleMoneyChanged;
            UpdateMoneyDisplay(MoneyManager.Instance.CurrentMoney);
        }
        else
        {
            Debug.LogError("MoneyManager Instance is null! Make sure MoneyManager is in the scene.");
        }
    }

    private void OnDisable()
    {
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.OnMoneyChanged -= HandleMoneyChanged;
        }
    }


    private void HandleMoneyChanged(float currentMoney, float change)
    {
        UpdateMoneyDisplay(currentMoney);

    }

    private void UpdateMoneyDisplay(float money)
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + money.ToString("F2");
        }
        else
        {
            Debug.LogError("MoneyText TextMeshProUGUI not assigned in MoneyUI script!");
        }
    }
}