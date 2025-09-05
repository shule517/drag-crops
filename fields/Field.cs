namespace dragcrops.fields;

using Godot;
using System.Collections.Generic;
using extenstions;
using lib;
using lib.attributes;
using lib.extensions;
using objects.iron_ore;

public partial class Field : Node2D
{
    public static Field Instance { get; } = SceneTree.Root.GetNode<Field>("/root/Game/Field");
    private static SceneTree SceneTree => (SceneTree)Engine.GetMainLoop();

    [Node] private TileMapLayer _wallTileMapLayer = null!;

    private static readonly Scene<dragcrops.objects.tree.Tree> TreeScene = new("res://objects/tree/tree.tscn");
    private static readonly Scene<IronOre> IronOreScene = new("res://objects/iron_ore/iron_ore.tscn");
    private readonly List<Vector2> _objectPositions = [];

    public override void _Ready()
    {
        this.BindNodes();

        // 木の追加
        30.Times((x) =>
        {
            30.Times((y) =>
            {
                Spawn(GD.RandRange(0, 1000), GD.RandRange(5, 1000), TreeScene);
            });
        });

        // 鉱石の追加
        30.Times((x) =>
        {
            30.Times((y) =>
            {
                Spawn(GD.RandRange(0, 1000), GD.RandRange(-5, -1000), IronOreScene);
            });
        });
    }

    private void Spawn<T>(float x, float y, Scene<T> scene) where T : Node2D
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
        var node = scene.Instantiate();
        node.GlobalPosition = new Vector2(x, y);
        CallDeferred("add_child", node);
        ;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseEvent)
        {
            Vector2 localPos = GetGlobalMousePosition();
            _wallTileMapLayer.Visible = 0 < localPos.Y;
        }
    }
}
