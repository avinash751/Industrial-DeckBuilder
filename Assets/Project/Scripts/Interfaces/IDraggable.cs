using UnityEngine;
using System;

public interface IDraggable
{
    public Action OnDragStarted { get; set; }
    public Action OnDragUpdate { get; set; }
    public Action OnDragEnd { get; set; }
}
