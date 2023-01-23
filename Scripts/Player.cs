using Godot;
using System;
using System.Collections.Generic;

public class Player : TileActor
{
	private Vector2 Velocity = Vector2.Zero;
	private int LastX = 0;
	private int LastY = 0;

	private float Score = 0.0f;

	[Export] private int MoveScore = 10;
	[Export] private int BlockCost = 50;
	[Export] private int BridgeCost = 200;
	[Export] private float ScoreDecayRate = 5.0f;


	[Export] public PackedScene BlockScene;

	[Export] private float LightStrength = 200.0f;

	[Signal] public delegate void PlayerMoved(int x, int y);

	[Signal] public delegate void PlayerPlacedBlock(int x, int y);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Camera2D>("Camera2D").MakeCurrent();
	}


	public override void _Process(float delta)
	{
		base._Process(delta);
		// Score -= delta * ScoreDecayRate;
		if (Score < 0.0f)
		{
			Score = 0.0f;
		}

		((AnimatedSprite)FindNode("AnimatedSprite")).Playing = IsMoving();
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
		/* else if (inputEvent.IsActionPressed("interact"))
		{
			PlaceBlock(LastX, LastY);
		} */
		else if (inputEvent.IsActionPressed("interact_left"))
		{
			PlaceBlock(-1, 0);
		}
		else if (inputEvent.IsActionPressed("interact_right"))
		{
			PlaceBlock(1, 0);
		}
		else if (inputEvent.IsActionPressed("interact_up"))
		{
			PlaceBlock(0, -1);
		}
		else if (inputEvent.IsActionPressed("interact_down"))
		{
			PlaceBlock(0, 1);
		}
    }

	protected override void StopMoving()
	{
        base.StopMoving();

        List<TileActor> actors = GetParent<TileGrid>().GetActorsAtLocation(new Vector2i(GetTileX(), GetTileY()));
        foreach (TileActor actor in actors)
        {
            if (actor is Log)
            {
                Score += 100;
                actor.QueueFree();
            }
        }

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
			// Score += MoveScore;
			EmitSignal(nameof(PlayerMoved), GetTileX(), GetTileY());
		}
	}

	private void PlaceBlock(int offsetX, int offsetY)
	{
		TileGrid grid = GetParent<TileGrid>();
		if (grid != null)
		{
			int x = GetTileX() + offsetX;
			int y = GetTileY() + offsetY;

			TileActor occupant = new TileActor();
			if (!grid.IsTileOccupied(new Vector2i(x, y), out occupant))
			{
                if (Score < BlockCost)
                {
                    return;
                }
                Node block = BlockScene.Instance();
                GetParent().AddChild(block);
                TileActor tileActor = block as TileActor;
				tileActor.SetTilePosition(x, y);
				Score -= BlockCost;

				EmitSignal(nameof(PlayerPlacedBlock), x, y);
            }
			else
			{
				if (occupant is Block)
				{
					GD.Print("Block already exists at this location.");
					Score += BlockCost;
					occupant.QueueFree();
				}
				else if (occupant is Bridge)
				{
					if (Score >= BridgeCost)
					{
						Score -= BridgeCost;
						((Bridge)occupant).Build();
					}
				}
			}
        }
	}

	public int GetScore()
	{
		return (int)Math.Floor(Score);
	}

	public void SubtractScore(float score)
	{
		Score -= score;
		if (Score < 0.0f)
		{
			Score = 0.0f;
		}
	}

	public float GetLightStrength()
	{
		/* float result = Score + LightStrength;
		if (result > 500.0f)
		{
			result = 500.0f;
		}
		return result; */
		return 256.0f;
	}
}
