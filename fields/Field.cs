using Godot;
using System.Collections.Generic;
using dragcrops.extenstions;

public partial class Field : Node2D
{
    private PackedScene _treeScene = GD.Load<PackedScene>("res://objects/tree/tree.tscn");
    private List<Vector2> _treePositions = new List<Vector2>();

    public override void _Ready()
    {
        30.Times((x) =>
        {
            30.Times((y) =>
            {
                SpawnTree(GD.RandRange(0, 1000), GD.RandRange(0, 1000));
            });
        });
    }

    private void SpawnTree(float x, float y)
    {
        foreach (var position in _treePositions)
        {
            var distance = position.DistanceTo(new Vector2(x, y));
            if (distance < 15)
            {
                GD.Print(distance);
                return;
            }
        }

        _treePositions.Add(new Vector2(x, y));
        var tree = _treeScene.Instantiate<Node2D>();
        tree.GlobalPosition = new Vector2(x, y);
        CallDeferred("add_child", tree);;
    }
}
