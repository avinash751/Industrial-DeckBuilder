using UnityEngine;
using System;

public class Hoverable : MonoBehaviour, IHoverable
{
    [SerializeField] Vector3 hoverScale;
    public Action OnHoverStart { get; set; }
    public Action OnHoverEnd { get; set; }


    private void OnMouseOver()
    {
        OnMouseHoverEnter();
    }

    private void OnMouseExit()
    {
        OnMouseHoverExit();
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



