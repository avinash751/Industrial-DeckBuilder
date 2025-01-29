using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

[RequireComponent(typeof(BoxCollider2D))]
public class CardDraggable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SortingGroup sortingGroup;
    [SerializeField] private SpriteRenderer backgroundSprite;

    [Header("Settings")]
    [SerializeField] private Color invalidColor = Color.red;
    [SerializeField] private LayerMask cardLayer;
    [SerializeField] private float moveSmoothness = 10f;

    private Color originalColor;
    private Vector3 originalPosition;
    private int originalSortOrder;
    private bool isValidPlacement;
    private List<Collider2D> overlappingCards = new List<Collider2D>();

    private void Awake()
    {
        backgroundSprite = GetComponentInChildren<SpriteRenderer>();
        sortingGroup = GetComponentInChildren<SortingGroup>();
        originalColor = backgroundSprite.color;
        originalSortOrder = sortingGroup.sortingOrder;
    }

    public void StartDrag()
    {
        originalPosition = transform.position;
        sortingGroup.sortingOrder = 1;
        backgroundSprite.color = originalColor;
    }

    public void UpdateDrag(Vector2 targetPosition)
    {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * moveSmoothness
        );

        CheckOverlaps();
    }

    private void CheckOverlaps()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position,
            GetComponent<BoxCollider2D>().size,
            0,
            cardLayer
        );

        overlappingCards.Clear();
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject != gameObject) overlappingCards.Add(hit);
        }

        isValidPlacement = overlappingCards.Count == 0;
        backgroundSprite.color = isValidPlacement ? originalColor : invalidColor;
    }

    public void EndDrag()
    {
        sortingGroup.sortingOrder = originalSortOrder;
        if (!isValidPlacement) transform.position = originalPosition;
        backgroundSprite.color = originalColor;
    }
}