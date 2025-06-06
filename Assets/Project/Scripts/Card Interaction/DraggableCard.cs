using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using System;

public class DraggableCard : Draggable
{
    [Header("References")]
    [SerializeField] private SortingGroup sortingGroup;
    [SerializeField] private SpriteRenderer backgroundSprite;

    [Header("Settings")]
    [SerializeField] private Color invalidColor = Color.red;
    [SerializeField] private LayerMask cardLayer;
    [SerializeField] Vector2 collsionDetectionScale;


    private Color originalColor;
    private Vector3 originalPosition;
    private int originalSortOrder;
    private bool isValidPlacement;
    private List<Collider2D> overlappingCards = new List<Collider2D>();

    private void Awake()
    {
        backgroundSprite = GetComponentInChildren<SpriteRenderer>() ?? backgroundSprite;
        sortingGroup = GetComponentInChildren<SortingGroup>() ?? sortingGroup;
        originalColor = backgroundSprite.color;
        originalSortOrder = sortingGroup.sortingOrder;
    }
    protected override void OnMouseStartDrag()
    {
        base.OnMouseStartDrag();
        sortingGroup.sortingOrder = 100;
        backgroundSprite.color = originalColor;
        originalPosition = transform.position;
    }


    protected override void UpdateDrag()
    {
        base.UpdateDrag();
        CheckForOverlappingCards();
    }

    protected override void OnMouseEndDrag()
    {
        base.OnMouseEndDrag();
        sortingGroup.sortingOrder = originalSortOrder;
        if (!isValidPlacement) transform.position = originalPosition;
        backgroundSprite.color = originalColor;

    }
    private void CheckForOverlappingCards()
    {


        Collider2D[] hits = Physics2D.OverlapBoxAll(
       transform.position,
      collsionDetectionScale, 0, cardLayer);




        overlappingCards.Clear();
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject != gameObject) overlappingCards.Add(hit);
        }

        isValidPlacement = overlappingCards.Count == 0;
        backgroundSprite.color = isValidPlacement ? originalColor : invalidColor;
    }
}
