using CustomInspector;
using UnityEngine;
using System; 

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; } 

    [SerializeField] float startingMoney = 100f;
    [SerializeField][ReadOnly] private float currentMoney;

    public float CurrentMoney => currentMoney; 

    public event Action<float, float> OnMoneyChanged; 

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("More than one MoneyManager in the scene! Destroying the new one.");
            Destroy(gameObject);
            return;
        }

        currentMoney = startingMoney;
        OnMoneyChanged?.Invoke(currentMoney, 0f); 
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        Debug.Log($"Money added: {amount}. Current money: {currentMoney}");
        OnMoneyChanged?.Invoke(currentMoney, amount);
    }

    public void SubtractMoney(float amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            Debug.Log($"Money subtracted: {amount}. Current money: {currentMoney}");
            OnMoneyChanged?.Invoke(currentMoney, -amount); 
        }
        else
        {
            Debug.Log("Insufficient funds!");
            // You could also invoke the event here with 0 change or a specific value if you want to signal insufficient funds in the UI
            OnMoneyChanged?.Invoke(currentMoney, 0f); 
        }
    }

    public bool HasEnoughMoney(float amount)
    {
        return currentMoney >= amount;
    }
}