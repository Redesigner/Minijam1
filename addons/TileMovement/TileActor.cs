using Godot;
using System;

[Tool]
public class TileActor : Node2D
{
    [Export]
    private int TileX = 0;
    [Export]
    private int TileY = 0;
    [Export]
    public bool IsSolid = true;

    [Export]
    public float MovementTime = 0.5f;

    private Vector2 PreviousPosition = Vector2.Zero;
    private Vector2 NewPosition = Vector2.Zero;

    private float Alpha = 0.0f;

    private bool Moving = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("TileActor Spawned");
        UpdatePosition();
    }

    public override Godot.Collections.Array _GetPropertyList()
    {
        Godot.Collections.Array properties = new Godot.Collections.Array();

        return properties;
    }

    public override void _Process(float delta)
    {
        if (Moving)
        {
            Alpha += delta;
            if (Alpha >= MovementTime)
            {
                StopMoving();
                return;
            }
            Position = PreviousPosition + (NewPosition - PreviousPosition) * (Alpha / MovementTime);
            Position = Position.Round();
        }
    }
    protected virtual void StopMoving()
    {
        Alpha = 0.0f;
        Moving = false;
        Position = NewPosition;
    }

    public void SetTilePosition(int x, int y)
    {
        TileX = x;
        TileY = y;

        UpdatePosition();
    }

    public int GetTileX()
    {
        return TileX;
    }

    public int GetTileY()
    {
        return TileY;
    }

    public bool TileMove(int x, int y)
    {
        if (Moving)
        {
            return false;
        }
        TileGrid grid = GetParent<TileGrid>();
        if (grid == null)
        {
            GD.Print("TileGrid parent is null...");
            return false;
        }
        GD.Print("Tile move: (" + x + ", " + y + ") from (" + TileX + ", " + TileY + ")");
        if (IsSolid)
        {
            if (!grid.IsTileOccupied(TileX + x, TileY + y))
            {
                TileX += x;
                TileY += y;
                UpdatePosition();
                return true;
            }
            return false;
        }
        return true;
    }

    private void UpdatePosition()
    {
        TileGrid grid = GetParent<TileGrid>();
        PreviousPosition = Position;
        NewPosition = new Vector2(TileX * grid.GetTileWidth(), TileY * grid.GetTileHeight());
        Moving = true;
    }
}
