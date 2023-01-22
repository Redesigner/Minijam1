using Godot;
using System;

public class Bridge : TileActor
{
    public void Build()
    {
        IsSolid = false;
        ((Sprite)FindNode("Empty")).Visible = false;
        ((Sprite)FindNode("Sprite")).Visible = true;
    }
}
