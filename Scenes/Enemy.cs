using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Enemy : TileActor
{
    private List<TileGrid.TileNode> PathToPlayer = new List<TileGrid.TileNode>();

    private int CurrentPathIndex;

    public void _on_Timer_timeout()
    {
        // UpdatePath();
    }

    public void _on_Player_PlayerMoved(int x, int y)
    {
        UpdatePath();
    }

    public void _on_Player_PlayerPlacedBlock(int x, int y)
    {
        if (PathToPlayer.Contains(new TileGrid.TileNode(x, y)))
        {
            UpdatePath();
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    protected override void StopMoving()
    {
        base.StopMoving();
        // GD.Print("Enemy reached desired tile");
        if (CurrentPathIndex < PathToPlayer.Count - 1)
        {
            GD.Print ("Step #" + CurrentPathIndex + " in path completed. Arrived at (" + GetTileX() + ", " + GetTileY() + ")");
            object nextNode = PathToPlayer[CurrentPathIndex + 1];
            CurrentPathIndex++;
            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        TileGrid.TileNode nextNode = PathToPlayer[CurrentPathIndex];
        int deltaX = GetTileX() - nextNode.X;
        int deltaY = GetTileY() - nextNode.Y;

        if (Math.Abs(deltaX) + Math.Abs(deltaY) > 1)
        {
            GD.Print("Enemy move too large! X: " + deltaX + " Y: " + deltaY +
                "\n (" + GetTileX() + ", " + GetTileY() + ") => (" + nextNode.X + ", " + nextNode.Y + ")");
        }
        TileMove(-deltaX, -deltaY);
    }

    private void UpdatePath()
    {
        TileGrid grid = GetParent() as TileGrid;
        Player player = grid.FindNode("Player") as Player;
        PathToPlayer.Clear();

        PathToPlayer = grid.Pathfind(player.GetTileX(), player.GetTileY(), GetTileX(), GetTileY());

        String path = "Path found to player at "
            + "(" + player.GetTileX() + ", " + player.GetTileY() + ")"
            + " from enemy at (" + GetTileX() + ", " + GetTileY() + ")"
            + "\nsteps: ";
        foreach (TileGrid.TileNode tileNode in PathToPlayer)
        {
            path += tileNode.ToString() + " => ";
        }
        GD.Print(path);

        if (PathToPlayer.Count > 0)
        {
            CurrentPathIndex = 0;
            if (!IsMoving())
            {
                CurrentPathIndex++;
                MoveAlongPath();
            }
        }
    }
}
