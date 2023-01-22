using Godot;
using System;

[Tool]
public class Hole : TileActor
{
    public override void _Ready()
    {
        if (!Engine.EditorHint)
        {
            ((Control)FindNode("Visual")).Visible = false;
        }
        base._Ready();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        ((Control)FindNode("Visual")).RectSize= new Vector2(SizeX * 16, SizeY * 16);
    }
}
