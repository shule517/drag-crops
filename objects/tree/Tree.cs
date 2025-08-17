using Godot;

public partial class Tree : Area2D
{
    [Export] public int Hp { get; set; } = 10;
    [Export] public AudioStream ChopTreeAudio;
    [Export] public AudioStream FallTreeAudio;

    private Audio _audio;
    private AnimatedSprite2D _animatedSprite2D;

    public override void _Ready()
    {
        MouseEntered += () => SetIsSeleted(true);
        MouseExited += () => SetIsSeleted(false);

        _audio = GetNode<Audio>("/root/Audio");
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _Process(double delta)
    {
    }

    private void SetIsSeleted(bool isSelected)
    {
        (_animatedSprite2D.Material as ShaderMaterial).SetShaderParameter("is_selected", isSelected);
    }

    private void Damage(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            _audio.PlaySound(ChopTreeAudio, GD.RandRange(0.8, 1.1));
            GD.Print("Tree hit!");
            QueueFree();
        }
        else
        {
            _audio.PlaySound(ChopTreeAudio, GD.RandRange(0.8, 1.1));
        }
    }

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            Damage(1);
        }
    }
}
