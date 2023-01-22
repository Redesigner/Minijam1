using Godot;
using System;

public class Light : Light2D
{
    public override void _Process(float delta)
    {
        base._Process(delta);
        Player player = GetParent<Player>();
        float scale = player.GetLightStrength() / 256.0f;
        Scale = new Vector2(scale, scale);
    }

    public override void _Ready()
    {
        base._Ready();
        // Visible = true;
    }
}
