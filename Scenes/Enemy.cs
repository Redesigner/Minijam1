using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Enemy : TileActor
{
    private List<TileGrid.TileNode> PathToPlayer;

    private TileGrid.TileNode NextNode;

    public void _on_Timer_timeout()
    {
        List<TileGrid.TileNode> tileNodes = new List<TileGrid.TileNode>();
        TileGrid grid = GetParent() as TileGrid;
        Player player = grid.FindNode("Player") as Player;

        PathToPlayer = grid.Pathfind(player.GetTileX(), player.GetTileY(), GetTileX(), GetTileY());

        String path = "Path found -- steps: ";
        foreach (TileGrid.TileNode tileNode in PathToPlayer)
        {
            path += tileNode.ToString();
        }
        GD.Print(path);

        if (PathToPlayer.Count > 0)
        {
            NextNode = PathToPlayer[1];
            MoveAlongPath();
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
    }

    protected override void StopMoving()
    {
        base.StopMoving();
        GD.Print("Enemy reached desired tile");
        if (NextNode.Parent != null)
        {
            object nextNode = NextNode.Parent.Target;
            if (nextNode == null || !(nextNode is TileGrid.TileNode))
            {
                GD.Print("Warning: nextnode in path is invalid.");
                return;
            }
            NextNode = (TileGrid.TileNode)NextNode.Parent.Target;
            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        if (NextNode != null)
        {
            GD.Print("Trying to move to next tile (" + NextNode.X + ", " + NextNode.Y + ")");
            int deltaX = GetTileX() - NextNode.X;
            int deltaY = GetTileY() - NextNode.Y;

            if (Math.Abs(deltaX) <= 1 && Math.Abs(deltaY) <= 1)
            {
                TileMove(-deltaX, -deltaY);
            }
        }
    }
}
