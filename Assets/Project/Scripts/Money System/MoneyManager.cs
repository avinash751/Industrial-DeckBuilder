using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; } // Singleton instance

    private float currentMoney;

    public float CurrentMoney => currentMoney; // Public getter for the current money

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            // Don't destroy on scene load so money persists (if needed across scenes)
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("More than one MoneyManager in the scene! Destroying the new one.");
            Destroy(gameObject);
            return;
        }

        currentMoney = 100f; // Starting money as you specified
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        Debug.Log($"Money added: {amount}. Current money: {currentMoney}");
    }

    public void SubtractMoney(float amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            Debug.Log($"Money subtracted: {amount}. Current money: {currentMoney}");
        }
        else
        {
            Debug.Log("Insufficient funds!");
            // Optionally handle insufficient funds (e.g., return false, trigger an event)
        }
    }

    public bool HasEnoughMoney(float amount)
    {
        return currentMoney >= amount;
    }
}