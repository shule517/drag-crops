using dragcrops.extenstions;
using Godot;

public partial class Tree : Area2D
{
    [Export] public int Hp { get; set; } = 10;
    [Export] public AudioStream ChopTreeAudio;
    [Export] public AudioStream FallTreeAudio;

    private Audio _audio;
    private AnimatedSprite2D _animatedSprite2D;
    private static readonly PackedScene ItemScene = GD.Load<PackedScene>("res://items/item/item.tscn");

    public override void _Ready()
    {
        _audio = Audio.Instance;
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        MouseEntered += () => SetSharderParamIsSeleted(true);
        MouseExited += () => SetSharderParamIsSeleted(false);
    }

    public override void _Process(double delta)
    {
    }

    private void SetSharderParamIsSeleted(bool isSelected)
    {
        _animatedSprite2D.SetShaderParameter("is_selected", isSelected);
    }

    private void Damage(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            // 木が倒れた
            _audio.PlaySound(ChopTreeAudio, GD.RandRange(0.8, 1.1));
            GD.RandRange(3, 10).Times((i) => DropItem());
            QueueFree();
        }
        else
        {
            _audio.PlaySound(ChopTreeAudio, GD.RandRange(0.8, 1.1));
        }
    }

    // TODO: 別クラスに移動させる
    private void DropItem()
    {
        var item = ItemScene.Instantiate<Node2D>();
        item.GlobalPosition = new Vector2(GlobalPosition.X + GD.RandRange(-10, 10), GlobalPosition.Y - 7);
        GetParent<Node>().AddChild(item);
    }

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            Damage(1);
        }
    }
}
