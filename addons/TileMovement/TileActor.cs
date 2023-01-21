using Godot;
using System;

[Tool]
public class TileActor : Node2D
{
	[Export]
	private int TileX = 0;
	[Export]
	private int TileY = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	public override Godot.Collections.Array _GetPropertyList()
	{
		Godot.Collections.Array properties = new Godot.Collections.Array();
		return properties;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
