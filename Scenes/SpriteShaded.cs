using Godot;
using System;

public class SpriteShaded : Sprite
{
    public override void _Process(float Delta)
    {
        Material.Set("world_position", GlobalPosition);
        ShaderMaterial shaderMaterial = Material as ShaderMaterial;
        if (!(shaderMaterial is null))
        {
            Node player = GetParent().FindNode("Player");
            Node2D player2D = player as Node2D;
            shaderMaterial.SetShaderParam("world_position", GlobalPosition);
            shaderMaterial.SetShaderParam("light_position", player2D.GlobalPosition);
            shaderMaterial.SetShaderParam("light_strength", ((Player)player2D).GetLightStrength());
            // GD.Print(shaderMaterial.GetShaderParam("world_position").ToString());
        }
    }
}