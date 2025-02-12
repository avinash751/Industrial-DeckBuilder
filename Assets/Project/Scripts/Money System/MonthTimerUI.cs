using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonthTimerUI : MonoBehaviour
{
    [SerializeField] private Image monthProgressBarImage;
    [SerializeField] private TextMeshProUGUI monthNameText;

    private void Update()
    {
        if (MonthTimer.Instance == null)
        {
            Debug.LogWarning("MonthTimer.Instance is null! Make sure MonthTimer is in the scene and active.");
            return;
        }

        if (monthProgressBarImage == null || monthNameText == null) return;
        monthProgressBarImage.fillAmount = MonthTimer.Instance.MonthProgress;
        monthNameText.text = MonthTimer.Instance.CurrentMonthName;

    }
}