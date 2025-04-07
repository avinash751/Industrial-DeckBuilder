using UnityEngine;
using TMPro;
using System;

public class MoneyFeedbackDisplay : MonoBehaviour
{
    [SerializeField] Card card;
    [SerializeField] private TextMeshPro deductionText;
    [SerializeField] private float displayDuration = 1.5f;
    private float timer = 0f;
    [SerializeField]private Color activeColor;
    [SerializeField]private Color transparentColor;

    protected void Awake()
    {

        if (deductionText == null)
        {
            Debug.LogError($"MoneyFeedbackDisplay on {gameObject.name} requires a TextMeshPro component!");
            enabled = false;
            return;
        }
        ResetFeedback();
    }

    private void OnEnable()
    {
        if (card != null) 
        {
            card.OnMoneyDeducted += ShowMoneyFeedback; 
        }
        else
        {
            Debug.LogError("Card reference is null in MoneyFeedbackDisplay. OnEnable will not subscribe to events.");
        }
    }

    private void OnDisable()
    {
        if (card != null) 
        {
            card.OnMoneyDeducted -= ShowMoneyFeedback;
        }
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                ResetFeedback();
            }
        }
    }

    private void ShowMoneyFeedback(float amount)
    {
        if (deductionText == null) return;
        deductionText.text = $"-{amount}$";
        deductionText.color =activeColor;
        timer = displayDuration;
    }

    public  void ResetFeedback()
    {
        if (deductionText == null) return;
        deductionText.text = "";
        deductionText.color = transparentColor;
        timer = 0f;
    }
}