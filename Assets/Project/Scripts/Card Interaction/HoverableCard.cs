using UnityEngine;

public class HoverableCard : Hoverable
{
    protected override void OnMouseHoverEnter()
    { 
        if(DragManager.IsDragging) return;
        base.OnMouseHoverEnter();

    }
    protected override void OnMouseHoverExit()
    {
        if (DragManager.IsDragging) return;
        base.OnMouseHoverExit();
    }
}
