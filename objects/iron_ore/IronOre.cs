namespace dragcrops.objects.iron_ore;
using autoload;
using Godot;
using extenstions;
using fields;
using items.item;
using lib.attributes;
using lib.extensions;
using UI.hp_progress_bar;

public partial class IronOre : Area2D
{
    [Export] public int MaxHp { get; set; } = 10;
    [Export] public int Hp { get; set; } = 10;
    [Export] public AudioStream ChopAudio = null!;
    [Export] public AudioStream BreakAudio = null!;

    [Node] private AnimatedSprite2D _animatedSprite2D = null!;
    [Node] private HpProgressBar _hpProgressBar = null!;
    [Inject] private Audio _audio = null!;

    public override void _Ready()
    {
        this.BindNodes();

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
        _audio.Play(ChopAudio, GD.RandRange(0.8, 1.1));
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
