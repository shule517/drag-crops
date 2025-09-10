namespace dragcrops.items.item;

using Godot;
using System;
using lib;
using lib.attributes;
using lib.extensions;
using autoload;

public partial class ItemNode : CharacterBody2D
{
    [Export] public ItemType ItemType;
    [Export] public AudioStream GetItemAudio = null!;
    [Export] public AudioStream LevelUpAudio = null!;

    [Node] private Area2D _area2D = null!;
    [Node] private AnimatedSprite2D _animatedSprite2D = null!;
    [Inject] private Audio _audio = null!;

    private float _groundY;
    private float _bouncePower = -200; // 最初のバウンド力
    private bool _isDropped;
    private static readonly Scene<ItemNode> _itemNodeScene = new("res://items/item/item_node.tscn");

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
        this.BindNodes();

        GD.Print(ItemType);

        if (ItemType == ItemType.石)
        {
            _animatedSprite2D.Frame = 7;
        }

        _groundY = Position.Y;

        // 左右にバラける, 上に飛ばす
        Velocity = new Vector2(GD.RandRange(-50, 50), -200); // GD.RandRange(-200, -250)

        // マウスホバーでアイテムGET
        _area2D.MouseEntered += () =>
        {
            if (_isDropped)
            {
                GetItem();
            }
        };
    }

    void GetItem()
    {
        if (GameData.Instance.GetTreeGold())
        {
            _audio.Play(LevelUpAudio);
        }

        // _audio.PlaySound(GetItemAudio, volumeDb: -10.0f);
        _audio.Play(GetItemAudio, GD.RandRange(0.8, 1.1));
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
                _isDropped = true;

                // // アイテムが落ちた時に自動取得
                // GetItem();
            }
        }

        MoveAndSlide();
    }
}
