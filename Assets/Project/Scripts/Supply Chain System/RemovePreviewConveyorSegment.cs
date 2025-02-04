using UnityEngine;

public class RemovePreviewConveyorSegment : ICommand
{
    private SupplyChainEditor editor;
    private Vector3 removedPoint;

    public RemovePreviewConveyorSegment(SupplyChainEditor editor)
    {
        this.editor = editor;
    }

    public void Execute()
    {
        if (editor.WaypointCount > 2)
        {
            removedPoint = editor.GetSecondToLastWaypoint();
            editor.RemoveLastWaypointSegment();
        }
    }

    public void Undo()
    {
        editor.AddWaypointSegment(removedPoint);
    }
}