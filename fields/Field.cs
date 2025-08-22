using Godot;
using System;
using dragcrops.extenstions;

public partial class Field : Node2D
{
    private PackedScene _treeScene = GD.Load<PackedScene>("res://objects/tree/tree.tscn");

    public override void _Ready()
    {
        100.Times((x) =>
        {
            100.Times((y) => SpawnTree(x, y));
        });
    }

    private void SpawnTree(float x, float y)
    {
        var tree = _treeScene.Instantiate<Node2D>();
        tree.GlobalPosition = new Vector2(y % 2 == 0 ? x * 30 : x * 30 + 15, y * 30);
        GetParent<Node>().CallDeferred("add_child", tree);;
    }
}
