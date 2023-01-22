using Godot;
using System;

public class LightViewport : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Draw()
    {
        base._Draw();
        DrawCircle(Vector2.Zero, 128.0f, new Color(1.0f, 1.0f, 1.0f, 0.5f));
        DrawCircle(Vector2.Zero, 64.0f, new Color(1.0f, 1.0f, 1.0f, 1.0f));
    }
}
