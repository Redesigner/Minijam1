using Godot;
using System;

public class Player : Node2D
{
	private Vector2 Velocity = Vector2.Zero;
	private const float MovementSpeed = 100.0f;
	private Timer MoveDebounceTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Camera2D>("Camera2D").MakeCurrent();
	}


	public override void _Process(float delta)
	{
		Velocity = Input.GetVector("left", "right", "up", "down") * MovementSpeed;
		Position += Velocity * delta;
		Position.Round();
	}
}
