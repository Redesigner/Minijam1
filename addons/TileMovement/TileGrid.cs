using Godot;
using System;

[Tool]
public class TileGrid : Node 
{
    [Export]
    private float TileWidth = 0.0f;
    [Export]
    private float TileHeight = 0.0f; 
    public override void _Ready()
    {
        
    }

    public override Godot.Collections.Array _GetPropertyList()
    {
        Godot.Collections.Array properties = new Godot.Collections.Array();
        return properties;
    }

    public Vector2 GetTileSize()
    {
        return new Vector2(TileWidth, TileHeight);
    }

    public float GetTileWidth()
    {
        return TileWidth;
    }

    public float GetTileHeight()
    {
        return TileHeight;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
