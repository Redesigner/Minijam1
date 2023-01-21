using Godot;
using System;

[Tool]
public class TileActor : Node2D
{
	[Export]
	private int TileX = 0;
	[Export]
	private int TileY = 0;

	private TileGrid TileGrid;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TileGrid = GetParent<TileGrid>();
	}
	
	public override Godot.Collections.Array _GetPropertyList()
	{
		Godot.Collections.Array properties = new Godot.Collections.Array();
		return properties;
	}

	public override void _Process(float delta)
	{
	}

	public void SetTilePosition(int x, int y)
	{
		TileX = x;
		TileY = y;

		UpdatePosition();
	}

	public void TileMove(int x, int y)
	{
		GD.Print("Tile move: (" + x + ", " + y + ")");
		TileX += x;
		TileY += y;
		UpdatePosition();
	}

	private void UpdatePosition()
	{
		if (TileGrid == null)
		{
			GD.Print("TileGrid parent is null...");
			TileGrid = GetParent<TileGrid>();
		}
        Position = new Vector2(TileX * TileGrid.GetTileWidth(), TileY * TileGrid.GetTileHeight());
    }
}
