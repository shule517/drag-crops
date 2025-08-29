using dragcrops.extenstions;
using dragcrops.items.item;
using dragcrops.lib.attributes;
using dragcrops.lib.extensions;
using Godot;

public partial class Tree : Area2D
{
    [Export] public int MaxHp { get; set; } = 10;
    [Export] public int Hp { get; set; } = 10;
    [Export] public AudioStream ChopTreeAudio;
    [Export] public AudioStream FallTreeAudio;

    [OnReady("AnimatedSprite2D")] private AnimatedSprite2D _animatedSprite2D;
    [OnReady("HpProgressBar")] private HpProgressBar _hpProgressBar;

    public override void _Ready()
    {
        this.BindOnReadyNodes();

        MouseEntered += () => SetSharderParamIsSeleted(true);
        MouseExited += () => SetSharderParamIsSeleted(false);
    }

    private void SetSharderParamIsSeleted(bool isSelected)
    {
        _animatedSprite2D.SetShaderParameter("is_selected", isSelected);
    }

    private void Damage(int damage)
    {
        Audio.PlaySound(ChopTreeAudio, GD.RandRange(0.8, 1.1));
        Hp -= damage;
        _hpProgressBar.UpdateProgress(Hp, MaxHp);

        // 木が倒れた
        if (Hp <= 0)
        {
            GD.RandRange(3, 10).Times((i) => DropItem());
            QueueFree();
        }
    }

    // TODO: 別クラスに移動させる
    private void DropItem()
    {
        var globalPosition = GlobalPosition + new Vector2(GD.RandRange(-10, 10), -7);
        var item = ItemNode.Instantiate(globalPosition, ItemType.木材);
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
