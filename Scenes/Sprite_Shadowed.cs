using Godot;
using System;

public class Sprite_Shadowed : Sprite
{
    public override void _Process(float Delta)
    {
        Material.Set("world_position", GlobalPosition);
        ShaderMaterial shaderMaterial = Material as ShaderMaterial;
        if (!(shaderMaterial is null))
        {
            Node player = GetParent().GetParent().FindNode("Player");
            Node2D player2D = player as Node2D;
            shaderMaterial.SetShaderParam("world_position", GlobalPosition);
            shaderMaterial.SetShaderParam("light_position", player2D.GlobalPosition);
            // GD.Print(shaderMaterial.GetShaderParam("world_position").ToString());
        }
    }
}
