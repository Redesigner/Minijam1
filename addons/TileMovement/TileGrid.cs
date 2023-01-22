using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

[Tool]
public class TileGrid : Node 
{
    public struct TileNode
    {
        public int X;
        public int Y;
        public int Distance;
        public WeakReference Parent;
        public TileNode(int x, int y)
        {
            X = x;
            Y = y;
            Distance = 0;
            Parent = null;
        }

        public TileNode(TileNode tileNode)
        {
            X = tileNode.X;
            Y = tileNode.Y;
            Distance = tileNode.Distance;
            Parent = new WeakReference(tileNode);
        }

        public TileNode(int x, int y, int distance, TileNode parent)
        {
            X = x;
            Y = y;
            Distance = distance;
            Parent = new WeakReference(parent);
        }


        public static bool operator ==(TileNode left, TileNode right)
        {
            return (left.X == right.X && left.Y == right.Y);
        }

        public static bool operator !=(TileNode left, TileNode right)
        {
            return (left.X != right.X || left.Y != right.Y);
        }

        public override bool Equals(object o)
        {
            if (!(o is TileNode))
            {
                return false;
            }
            TileNode tile = (TileNode) o;
            return tile == this;
        }
        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public override string ToString()
        {
            String result = "(" + X + ", " + Y + ")";
            /* if (Parent != null)
            {
                TileNode parentNode = (TileNode) Parent.Target;
                result += " parent at : ( " + parentNode.X + ", " + parentNode.Y + ")";
            } */
            return result;
        }
    }

    [Export]
    private float TileWidth = 0.0f;
    [Export]
    private float TileHeight = 0.0f;


    public override void _Ready()
    {
    }

    public override Godot.Collections.Array _GetPropertyList()
    {
        Godot.Collections.Array properties = new Godot.Collections.Array();
        return properties;
    }

    public Vector2 GetTileSize()
    {
        return new Vector2(TileWidth, TileHeight);
    }

    public float GetTileWidth()
    {
        return TileWidth;
    }

    public float GetTileHeight()
    {
        return TileHeight;
    }

    public bool IsTileOccupied(int x, int y)
    {
        Godot.Collections.Array children = GetChildren();
        for (int i = 0; i < GetChildCount(); i++)
        {
            if (children[i] is TileActor)
            {
                TileActor actor = children[i] as TileActor;
                if (actor.IsSolid)
                {
                    if (actor.GetTileX() == x && actor.GetTileY() == y)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public List<TileNode> Pathfind(int x0, int y0, int x1, int y1)
    {
        const int maxSearchDistance = 15;
        List<TileNode> open = new List<TileNode>();
        List<TileNode> closed = new List<TileNode>();

        TileNode start = new TileNode(x0, y0);
        open.Add(start);

        int iter = 0;
        
        while (open.Count > 0 && iter < maxSearchDistance)
        {
            iter++;
            // Make a copy of open so we don't cause problems when we modify it
            List<TileNode> openCopy = new List<TileNode>(open);

            foreach (TileNode node in openCopy)
            {
                if (!IsTileOccupied(node.X, node.Y) || (node.X == x0 && node.Y == y0))
                {
                    // Make an entry for each of the cardinal directions
                    List<TileNode> newNodes = new List<TileNode>()
                    {
                        new TileNode(node.X - 1, node.Y, node.Distance + 1, node), // left
                        new TileNode(node.X + 1, node.Y, node.Distance + 1, node), // right
                        new TileNode(node.X, node.Y - 1, node.Distance + 1, node), // up
                        new TileNode(node.X, node.Y + 1, node.Distance + 1, node)  // down
                    };
                    foreach (TileNode newNode in newNodes)
                    {
                        if (open.Contains(newNode) || closed.Contains(newNode))
                        {
                            // Don't test existing nodes, whether pending or not
                            continue;
                        }
                        if (newNode.X == x1 && newNode.Y == y1)
                        {
                            return Traverse(newNode);
                        }
                        open.Add(newNode);
                    }
                }
                closed.Add(new TileNode(node));
                open.Remove(node);
            }
        }
        GD.Print("Could not find path after " + iter + " iterations.");
        return new List<TileNode>();
    }

    private List<TileNode> Traverse(TileNode node)
    {
        const int overflow = 15;

        int iter = 0;
        List<TileNode> result = new List<TileNode>();

        TileNode current = node;
        while (iter < overflow)
        {
            result.Add(current);
            if (current.Parent == null || current.Parent.Target == null)
            {
                GD.Print("Traversal of path completed at node " + iter);
                if (iter != node.Distance)
                {
                    GD.Print("!!Warning!! Traversal list size (" + iter + ") and distance mismatch (" + node.Distance + ") Did something happen to a node in the chain?");
                }
                return result;
            }
            else
            {
                current = (TileNode) current.Parent.Target;
            }
            iter++;
        }
        if (iter >= overflow)
        {
            GD.Print("Attempting to traverse list of tilenodes caused an overflow...");
        }
        return result;
    }
}
