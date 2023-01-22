using Godot;
using System;

[Tool]
public class Spawner : TileActor
{
    [Export] public PackedScene Actor;

    public override void _Ready()
    {
        if (!Engine.EditorHint)
        {
            ((Control)FindNode("Visual")).Visible = false;
        }
        base._Ready();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        ((Control)FindNode("Visual")).RectSize = new Vector2(SizeX * 16, SizeY * 16);
    }

    public void _on_Timer_timeout(float delta)
    {
        int l = GetTileX();
        int r = GetTileX() + SizeX;
        int u = GetTileY();
        int d = GetTileY() + SizeY;

        Random rnd = new Random();
        Vector2i spawnPosition = new Vector2i(rnd.Next(l, r), rnd.Next(u, d));
        
        while (GetParent<TileGrid>().IsTileOccupied(spawnPosition))
        {
            spawnPosition = new Vector2i(rnd.Next(l, r), rnd.Next(u, d));
        }
        Node actor = Actor.Instance();
        ((TileActor)actor).SetTilePosition(spawnPosition.X, spawnPosition.Y);
    }
}
