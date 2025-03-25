using UnityEngine;
using System;
using CustomInspector;
using Unity.VisualScripting;

public enum Month { January, February, March, April, May, June, July, August, September, October, November, December }

public class MonthTimer : MonoBehaviour
{
    public static MonthTimer Instance { get; private set; }
    public event Action OnMonthEnd;

    [SerializeField] private float monthDurationSeconds = 60f;
    [SerializeField][ReadOnly] private float timer;
    [SerializeField][ReadOnly] private int currentMonthIndex = 0;

    public Month CurrentMonthEnum => (Month)currentMonthIndex;
    public string CurrentMonthName => CurrentMonthEnum.ToString();
    public float MonthProgress => timer / monthDurationSeconds;

    bool isSoundPlayed;


    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one MonthTimer in the scene! Destroying the new one.");
            Destroy(gameObject);
            return;
        }
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void Start()
    {
        timer = 0f;
        currentMonthIndex = 0;
    }


    private void Update()
    {
        timer += Time.deltaTime;

        UpdateCurrentMonth();
        PlaySound();
    }

    private void UpdateCurrentMonth()
    {
        if (timer >= monthDurationSeconds)
        {
            timer -= monthDurationSeconds;
            currentMonthIndex++;
            if (currentMonthIndex > 11)
            {
                currentMonthIndex = 0; // Wrap back to January
            }
            Debug.Log($" Month {((Month)currentMonthIndex).ToString()} starting.");
            OnMonthEnd?.Invoke();
            isSoundPlayed = false;
        }
    }

    void PlaySound()
    {
        float t = Mathf.InverseLerp(0, monthDurationSeconds, timer);

        if(t > 0.98f && !isSoundPlayed)
        {
            AudioManager.Instance?.PlayAudio("MonthlyPayment");
            isSoundPlayed = true;
        }
    }
}