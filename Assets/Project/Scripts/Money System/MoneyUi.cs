using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] MMF_Player positiveMoneyFeedback;
    [SerializeField] MMF_Player negativeMoneyFeedback;



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

        if (change > 0)
        {
            positiveMoneyFeedback.PlayFeedbacks();
        }
        else if (change < 0)
        {
            negativeMoneyFeedback?.PlayFeedbacks();
        }
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
            return;
        }


    }
}