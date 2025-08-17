using Godot;

public partial class Tree : Area2D
{
    [Export]
    public int Hp { get; set; } = 10;

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }

    private void Damage(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            QueueFree();
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
