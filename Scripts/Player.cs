using Godot;
using System;

public class Player : TileActor
{
	private Vector2 Velocity = Vector2.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Camera2D>("Camera2D").MakeCurrent();
	}


	public override void _Process(float delta)
	{
		// Vector2 input = Input.GetVector("left", "right", "up", "down");
	}

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("left"))
		{
			TileMove(-1, 0);
		}
		else if (inputEvent.IsActionPressed("right"))
		{
			TileMove(1, 0);
		}
		else if (inputEvent.IsActionPressed("up"))
		{
			TileMove(0, -1);
		}
		else if (inputEvent.IsActionPressed("down"))
		{
			TileMove(0, 1);
		}
    }
}
