using UnityEngine;

public class CardSortingManager : MonoBehaviour
{
    public static CardSortingManager Instance;

    private const int BASE_ORDER = 0;
    private int currentMaxOrder = BASE_ORDER;

    private void Awake() => Instance = this;

    public int GetNewTopOrder()
    {
        currentMaxOrder++;
        return currentMaxOrder;
    }
}
