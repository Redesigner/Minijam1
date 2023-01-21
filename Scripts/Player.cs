using Godot;
using System;

public class Player : TileActor
{
	private Vector2 Velocity = Vector2.Zero;
	private int LastX = 0;
	private int LastY = 0;

	private int Score = 0;

	private const int MoveScore = 10;
	private const int BlockCost = 50;

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
			TryMove(-1, 0);
		}
		else if (inputEvent.IsActionPressed("right"))
		{
			TryMove(1, 0);
		}
		else if (inputEvent.IsActionPressed("up"))
		{
			TryMove(0, -1);
		}
		else if (inputEvent.IsActionPressed("down"))
		{
			TryMove(0, 1);
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
			TryMove(-1, 0);
        }
        if (Input.IsActionPressed("right"))
        {
			TryMove(1, 0);
        }
        if (Input.IsActionPressed("up"))
        {
			TryMove(0, -1);
        }
        if (Input.IsActionPressed("down"))
        {
			TryMove(0, 1);
        }
	}

	private void TryMove(int x, int y)
	{
		LastX = x;
		LastY = y;
		if (TileMove(x, y))
		{
			Score += MoveScore;
		}
	}

	private void PlaceBlock(int offsetX, int offsetY)
	{
		if (Score < BlockCost)
		{
			return;
		}
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
				Score -= BlockCost;
            }
        }
	}

	public int GetScore()
	{
		return Score;
	}
}
