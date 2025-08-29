using Godot;
using dragcrops.extenstions;
using dragcrops.items.item;
using dragcrops.lib.attributes;
using dragcrops.lib.extensions;

public partial class IronOre : Area2D
{
    [Export] public int MaxHp { get; set; } = 10;
    [Export] public int Hp { get; set; } = 10;
    [Export] public AudioStream ChopAudio;
    [Export] public AudioStream BreakAudio;

    [OnReady("AnimatedSprite2D")] private AnimatedSprite2D _animatedSprite2D;
    [OnReady("HpProgressBar")] private HpProgressBar _hpProgressBar;

    public override void _Ready()
    {
        this.BindOnReadyNodes();

        _animatedSprite2D.Frame = GD.RandRange(0, 4);
        if (_animatedSprite2D.Frame == 2) _animatedSprite2D.Frame = 0;

        MouseEntered += () => SetSharderParamIsSeleted(true);
        MouseExited += () => SetSharderParamIsSeleted(false);
    }

    private void SetSharderParamIsSeleted(bool isSelected)
    {
        _animatedSprite2D.SetShaderParameter("is_selected", isSelected);
    }

    private void Damage(int damage)
    {
        Audio.PlaySound(ChopAudio, GD.RandRange(0.8, 1.1));
        Hp -= damage;
        _hpProgressBar.UpdateProgress(Hp, MaxHp);

        if (Hp <= 0)
        {
            // 壊れた
            GD.RandRange(3, 10).Times((i) => DropItem());
            QueueFree();
        }
    }

    // TODO: 別クラスに移動させる
    private void DropItem()
    {
        var globalPosition = GlobalPosition + new Vector2(GD.RandRange(-10, 10), -7);
        var item = ItemNode.Instantiate(globalPosition, ItemType.石);
        Field.Instance.AddChild(item);
    }

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            Damage(1);
        }
    }
}
