using System.Collections;
using UnityEngine;

public class ToolTipTrigger : MonoBehaviour
{
    [SerializeField] private Card card;
    [SerializeField] private CardData cardData;

    private void Start()
    {
        card = GetComponent<Card>();
        cardData = card.GetCardData();
    }

    private void OnMouseOver()
    {
        if (DragManager.IsDragging)
        {
            ToolTipSystem.Instance.Hide();
        }
    }
    private void OnMouseDown()
    {
        StartCoroutine(ShowToolTip());
        ToolTipSystem.Instance.ishiding = false;

    }

    private void OnMouseExit()
    {
        ToolTipSystem.Instance.Hide();
    }


    IEnumerator ShowToolTip()
    {

        yield return new WaitForSeconds(0.2f);
        if (DragManager.IsDragging)
        {
            ToolTipSystem.Instance.Hide();
        }
        else if (!ToolTipSystem.Instance.ishiding)
        {
            ToolTipSystem.Instance.Show(card, cardData);
        }

    }
}