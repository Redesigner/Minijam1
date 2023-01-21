using Godot;
using System;

public class Player : TileActor
{
	private Vector2 Velocity = Vector2.Zero;
	private int LastX = 0;
	private int LastY = 0;

	[Export]
	public PackedScene BlockScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Camera2D>("Camera2D").MakeCurrent();
	}


	public override void _Process(float delta)
	{
		base._Process(delta);
	}

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("left"))
		{
			LastX = -1;
			LastY = 0;
			TileMove(-1, 0);
		}
		else if (inputEvent.IsActionPressed("right"))
		{
			LastX = 1;
			LastY = 0;
			TileMove(1, 0);
		}
		else if (inputEvent.IsActionPressed("up"))
		{
			LastX = 0;
			LastY = -1;
			TileMove(0, -1);
		}
		else if (inputEvent.IsActionPressed("down"))
		{
			LastX = 0;
			LastY = 1;
			TileMove(0, 1);
		}
		else if (inputEvent.IsActionPressed("interact"))
		{
			PlaceBlock(LastX, LastY);
		}
    }

	protected override void StopMoving()
	{
        base.StopMoving();
        if (Input.IsActionPressed("left"))
        {
            LastX = -1;
            LastY = 0;
            TileMove(-1, 0);
        }
        else if (Input.IsActionPressed("right"))
        {
            LastX = 1;
            LastY = 0;
            TileMove(1, 0);
        }
        else if (Input.IsActionPressed("up"))
        {
            LastX = 0;
            LastY = -1;
            TileMove(0, -1);
        }
        else if (Input.IsActionPressed("down"))
        {
            LastX = 0;
            LastY = 1;
            TileMove(0, 1);
        }
	}

	private void PlaceBlock(int offsetX, int offsetY)
	{
		TileGrid grid = GetParent<TileGrid>();
		if (grid != null)
		{
			int x = GetTileX() + offsetX;
			int y = GetTileY() + offsetY;
			if (!grid.IsTileOccupied(x, y))
			{
                Node block = BlockScene.Instance();
                GetParent().AddChild(block);
                TileActor tileActor = block as TileActor;
				tileActor.SetTilePosition(x, y);
            }
        }
	}
}
