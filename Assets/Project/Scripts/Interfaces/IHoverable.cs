using System;

public interface IHoverable
{
    public Action OnHoverStart { get; set; }
    public Action OnHoverEnd { get; set; }
}
