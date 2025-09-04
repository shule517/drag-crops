namespace dragcrops.objects.tree;
using autoload;
using extenstions;
using items.item;
using lib.attributes;
using lib.extensions;
using Godot;
using fields;
using UI.hp_progress_bar;

public partial class Tree : Area2D
{
    [Export] public int MaxHp { get; set; } = 10;
    [Export] public int Hp { get; set; } = 10;
    [Export] public AudioStream ChopTreeAudio = null!;
    [Export] public AudioStream FallTreeAudio = null!;

    [Node] private AnimatedSprite2D _animatedSprite2D = null!;
    [Node] private HpProgressBar _hpProgressBar = null!;

    [Inject] private Audio _audio = null!;

    public override void _Ready()
    {
        this.BindNodes();

        MouseEntered += () => SetSharderParamIsSeleted(true);
        MouseExited += () => SetSharderParamIsSeleted(false);
    }

    private void SetSharderParamIsSeleted(bool isSelected)
    {
        _animatedSprite2D.SetShaderParameter("is_selected", isSelected);
    }

    private void Damage(int damage)
    {
        _audio.Play(ChopTreeAudio, GD.RandRange(0.8, 1.1));
        // _audio.PlaySound(ChopTreeAudio, GD.RandRange(0.8, 1.1));
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
