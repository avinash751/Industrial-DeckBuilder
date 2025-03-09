using UnityEngine;
using System.Collections.Generic;
using System;
using CustomInspector;
using NUnit.Framework.Internal.Commands;

public class GameProgression : MonoBehaviour
{
    [SerializeField] List<MileStone> mileStones;
    [SerializeField] GameObject lockedElementPrefab;
    public Action OnAllCardPacksUnlocked;

    private void Start()
    {
        if(lockedElementPrefab == null)
        {
            Debug.LogError("Locked Element Prefab is not assigned in GameProgression script");
            return;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == 0) continue; // skip the first card pack to allow player to get cards when starting the game
            Vector3 spawnPosition = transform.GetChild(i).transform.position;
            GameObject newLockedElement = Instantiate(lockedElementPrefab, spawnPosition, Quaternion.identity);
            // i-1 because we are skipping the first child, so the first milestone is at index 0
            mileStones[i-1].AssociatedLockedElement = newLockedElement; 
        }

        MoneyManager.Instance.OnMoneyChanged += UnlockNextMiltsone;
    }

    private void OnValidate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // Not skipping the first child here, because the last milestone is win condition for the game
            mileStones.Add(new MileStone());
        }
        if(mileStones.Count > transform.childCount)
        {
           int difference =  mileStones.Count - transform.childCount;
            mileStones.RemoveRange(transform.childCount, difference);
        }
    }

    private void OnDisable()
    {
        MoneyManager.Instance.OnMoneyChanged -= UnlockNextMiltsone;
    }
    void UnlockNextMiltsone(float currentMoney, float unusedfloat1)
    {
        if (mileStones.Count == 1)
        {
            if (currentMoney >= mileStones[0].RequiredMoney)
            {
                OnAllCardPacksUnlocked?.Invoke();
            }
            return;
        }
        else if (mileStones.Count > 1)
        {
            if (currentMoney >= mileStones[0].RequiredMoney)
            {
                mileStones[0].AssociatedLockedElement.SetActive(false);
                mileStones.RemoveAt(0);
            }
        }
    }
}

[System.Serializable]
public class MileStone
{
    public float RequiredMoney;
    [ReadOnly] public GameObject AssociatedLockedElement;
}