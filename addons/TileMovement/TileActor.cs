using Godot;
using System;

public class TileActor : Node2D
{
    private Vector2i TilePosition = new Vector2i(0, 0);
    private const int GridSize = 16;

    [Export] public bool IsSolid = true;
    [Export] public float MovementTime = 0.5f;
    [Export] public int SizeX = 1;
    [Export] public int SizeY = 1;

    private Vector2 PreviousPosition = Vector2.Zero;
    private Vector2 NewPosition = Vector2.Zero;

    private float Alpha = 0.0f;

    private bool Moving = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        TilePosition = new Vector2i( (int)(Math.Round(Position.x / GridSize)), (int)(Math.Round(Position.y / GridSize)));

        Position = new Vector2(TilePosition.X * GridSize, TilePosition.Y * GridSize);
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
            // Position = Position.Round();
        }
    }
    protected virtual void StopMoving()
    {
        Alpha = 0.0f;
        Moving = false;
        Position = NewPosition;
    }

    public bool IsMoving()
    {
        return Moving;
    }

    public void SetTilePosition(int x, int y)
    {
        TilePosition = new Vector2i(x, y);

        UpdatePosition();
    }

    public int GetTileX()
    {
        return TilePosition.X;
    }

    public int GetTileY()
    {
        return TilePosition.Y;
    }

    public bool TileMove(int x, int y)
    {
        if (Moving)
        {
            return false;
        }
        if (x == 0 && y == 0)
        {
            return true;
        }
        TileGrid grid = GetParent<TileGrid>();
        if (grid == null)
        {
            GD.Print("TileGrid parent is null...");
            return false;
        }
        if (IsSolid)
        {
            if (!grid.IsTileOccupied(new Vector2i(TilePosition.X + x, TilePosition.Y + y)))
            {
                TilePosition.X += x;
                TilePosition.Y += y;
                UpdatePosition();
                return true;
            }
            return false;
        }
        TilePosition.X += x;
        TilePosition.Y += y;
        UpdatePosition();
        return true;
    }

    private void UpdatePosition()
    {
        PreviousPosition = Position;
        NewPosition = new Vector2(TilePosition.X * GridSize, TilePosition.Y * GridSize);
        Moving = true;
    }

    public void Move(Vector2 position)
    {
    }
}
