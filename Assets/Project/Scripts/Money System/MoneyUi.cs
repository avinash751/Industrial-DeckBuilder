using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText; // Assign your TextMeshProUGUI in the Inspector

    void Update()
    {
        if (MoneyManager.Instance != null && moneyText != null)
        {
            moneyText.text = "$" + MoneyManager.Instance.CurrentMoney.ToString("F2"); // Format to 2 decimal places
        }
        else if (moneyText == null)
        {
            Debug.LogError("MoneyText TextMeshProUGUI not assigned in MoneyUI script!");
        }
        else if (MoneyManager.Instance == null)
        {
            Debug.LogError("MoneyManager Instance is null! Make sure MoneyManager is in the scene.");
        }
    }
}