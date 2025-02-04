using UnityEngine;

public class PlacePreviewConveyorSegment : ICommand
{
    private SupplyChainEditor editor;
    private Vector3 point;

    public PlacePreviewConveyorSegment(SupplyChainEditor editor, Vector3 point)
    {
        this.editor = editor;
        this.point = point;
    }

    public void Execute()
    {
        editor.AddWaypointSegment(point);
    }

    public void Undo()
    {
        editor.RemoveLastWaypointSegment();
    }
}
