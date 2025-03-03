using UnityEngine;

public class CardPackPurchaser : MonoBehaviour
{
    public GameObject cardPackPrefab; // Prefab of the CardPack
    public Transform spawnTransform; // Where to spawn the CardPack
    [field:SerializeField]public bool IsPurchasable { get; private set; } = true; // For future progression

    private void OnMouseDown()
    {
        if (IsPurchasable)
        {
            SpawnCardPack();
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