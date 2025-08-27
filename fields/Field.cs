using Godot;
using System.Collections.Generic;
using dragcrops.extenstions;

public partial class Field : Node2D
{
    private PackedScene _treeScene = GD.Load<PackedScene>("res://objects/tree/tree.tscn");
    private PackedScene _ironOreScene = GD.Load<PackedScene>("res://objects/iron_ore/iron_ore.tscn");
    private List<Vector2> _objectPositions = new List<Vector2>();
    private TileMapLayer _wallTileMapLayer;

    public override void _Ready()
    {
        _wallTileMapLayer = GetNode<TileMapLayer>("WallTileMapLayer");

        // 木の追加
        30.Times((x) =>
        {
            30.Times((y) =>
            {
                Spawn(GD.RandRange(0, 1000), GD.RandRange(5, 1000), _treeScene);
            });
        });

        // 鉱石の追加
        30.Times((x) =>
        {
            30.Times((y) =>
            {
                Spawn(GD.RandRange(0, 1000), GD.RandRange(-5, -1000), _ironOreScene);
            });
        });
    }

    private void Spawn(float x, float y, PackedScene objectScene)
    {
        foreach (var position in _objectPositions)
        {
            var distance = position.DistanceTo(new Vector2(x, y));
            if (distance < 15)
            {
                return;
            }
        }

        _objectPositions.Add(new Vector2(x, y));
        var objectNode = objectScene.Instantiate<Node2D>();
        objectNode.GlobalPosition = new Vector2(x, y);
        CallDeferred("add_child", objectNode);;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseEvent)
        {
            Vector2 localPos = GetGlobalMousePosition();

            GD.Print(localPos.Y);
            _wallTileMapLayer.Visible = 0 < localPos.Y;
        }
    }
}
