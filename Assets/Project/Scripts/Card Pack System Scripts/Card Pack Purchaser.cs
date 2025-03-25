using TMPro;
using UnityEngine;

public class CardPackPurchaser : MonoBehaviour
{
    public GameObject cardPackPrefab; // Prefab of the CardPack
    public Transform spawnTransform; // Where to spawn the CardPack
    [SerializeField]TextMeshPro costText;
    [SerializeField] int cardPackCost;
    [field:SerializeField]public bool IsPurchasable { get; private set; } = true; // For future progression
    string audioKey = "BuyingCard";
    private void Start()
    {
        if (costText == null) return;
        costText.text = "Cost: $" + cardPackCost.ToString();
    }

    private void OnMouseDown()
    {
        if (IsPurchasable)
        {
            if (!MoneyManager.Instance.SubtractMoney(cardPackCost)) return;
            SpawnCardPack();
            AudioManager.Instance?.PlayAudio(audioKey);
        }
    }

    private void SpawnCardPack()
    {
        if (cardPackPrefab != null && spawnTransform != null)
        {
            GameObject cardPackObject = Instantiate(cardPackPrefab, spawnTransform.position, spawnTransform.rotation);
            CardPack cardPack = cardPackObject.GetComponent<CardPack>();
            if (cardPack == null)
            {
                Debug.LogError("CardPack prefab is missing the CardPack component!");
            }
        }
    }
}