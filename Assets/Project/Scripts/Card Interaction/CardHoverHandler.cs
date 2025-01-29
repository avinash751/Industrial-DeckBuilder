using UnityEngine;

public class CardHoverHandler : MonoBehaviour
{
    [SerializeField] private float hoverScale = 1.1f;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
        HoverManager.OnHoverStarted += HandleHoverStart;
        HoverManager.OnHoverEnded += HandleHoverEnd;
    }

    private void OnDestroy()
    {
        HoverManager.OnHoverStarted -= HandleHoverStart;
        HoverManager.OnHoverEnded -= HandleHoverEnd;
    }

    private void HandleHoverStart(CardHoverHandler hoveredCard)
    {
        if (hoveredCard == this)
            transform.localScale = originalScale * hoverScale;
    }

    private void HandleHoverEnd(CardHoverHandler unhoveredCard)
    {
        if (unhoveredCard == this)
            transform.localScale = originalScale;
    }
}