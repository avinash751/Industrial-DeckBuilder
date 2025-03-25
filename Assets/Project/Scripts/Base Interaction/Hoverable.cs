using UnityEngine;
using System;

public class Hoverable : MonoBehaviour, IHoverable
{
    [SerializeField] Vector3 hoverScale;
    public Action OnHoverStart { get; set; }
    public Action OnHoverEnd { get; set; }

    [SerializeField] string audioKey = "CardHover";
    bool isSoundPlayed;

    private void OnMouseOver()
    {
        if (DragManager.IsDragging) return;

        OnMouseHoverEnter();     
        if(!isSoundPlayed)
        {
            AudioManager.Instance?.PlayAudio(audioKey);
            isSoundPlayed = true;
        }
    }

    private void OnMouseExit()
    {
        OnMouseHoverExit();
        isSoundPlayed = false;
    }


    protected virtual void OnMouseHoverEnter()
    {
        OnHoverStart?.Invoke();
        transform.localScale = hoverScale;
    }
    protected virtual void OnMouseHoverExit()
    {
        OnHoverEnd?.Invoke();
        transform.localScale = Vector3.one;
    }

}



