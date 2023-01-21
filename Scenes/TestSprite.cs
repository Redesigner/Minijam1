using Godot;
using System;

public class TestSprite : Sprite
{
    public override void _Process(float Delta)
    {
        Material.Set("world_position", GlobalPosition);
        ShaderMaterial shaderMaterial = Material as ShaderMaterial;
        if (!(shaderMaterial is null))
        {
            shaderMaterial.SetShaderParam("world_position", GlobalPosition);
            GD.Print(shaderMaterial.GetShaderParam("world_position").ToString());
        }
    }
}
