using Godot;
using System;
using dragcrops.items.item;
using dragcrops.lib;
using dragcrops.lib.attributes;
using dragcrops.lib.extensions;

public partial class ItemNode : CharacterBody2D
{
    [Export] public ItemType ItemType;
    [Export] public AudioStream GetItemAudio;
    [Export] public AudioStream LevelUpAudio;

    [OnReady("Area2D")] private Area2D _area2D;

    private float _groundY;
    private float _bouncePower = -200; // 最初のバウンド力
    private bool _isDroped = false;
    private static Scene<ItemNode> _itemNodeScene = Scene<ItemNode>.Load("res://items/item/item_node.tscn");

    // アイテムの生成
    public static ItemNode Instantiate(Vector2 globalPosition, ItemType itemType)
    {
        var itemNode = _itemNodeScene.Instantiate();
        itemNode.GlobalPosition = globalPosition;
        itemNode.ItemType = itemType;
        return itemNode;
    }

    public override void _Ready()
    {
        this.BindOnReadyNodes();

        GD.Print(ItemType);

        _groundY = Position.Y;

        // 左右にバラける, 上に飛ばす
        Velocity = new Vector2(GD.RandRange(-50, 50), -200); // GD.RandRange(-200, -250)

        // マウスホバーでアイテムGET
        _area2D.MouseEntered += () =>
        {
            if (_isDroped)
            {
                GetItem();
            }
        };
    }

    void GetItem()
    {
        if (GameData.Instance.GetTreeGold())
        {
            Audio.PlaySound(LevelUpAudio);
        }
        // Audio.PlaySound(GetItemAudio, volumeDb: -10.0f);
        Audio.PlaySound(GetItemAudio, GD.RandRange(0.8, 1.1));
        QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {
        // だんだん左右の動きはゆっくりに, 重力をかける
        Velocity = new Vector2(Velocity.X * 0.95f, Velocity.Y + (float)(800 * delta));

        if (_groundY < Position.Y)
        {
            Position = Position with { Y = _groundY };
            Velocity = Velocity with { Y = Velocity.Y * -0.6f };

            if (Math.Abs(Velocity.Y) < 100)
            {
                Velocity = Velocity with { X = 0, Y = 0 };
                _isDroped = true;

                // // アイテムが落ちた時に自動取得
                // GetItem();
            }
        }
        MoveAndSlide();
    }
}
