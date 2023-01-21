using Godot;
using System;

public class Sprite_Shadowed : Sprite
{
    [Export]
    private Shader DarknessShader { get; set; }
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

    public override bool _Set(string property, object value)
    {
        if (property == "DarknessShader")
        {
            Material = new ShaderMaterial();
            return true;
        }
        return base._Set(property, value);
    }
}
