using Godot;
using System;

public partial class Item : CharacterBody2D
{
    private float _groundY;
    private float _bouncePower = -200; // 最初のバウンド力

    public override void _Ready()
    {
        _groundY = Position.Y;

        // 左右にバラける, 上に飛ばす
        Velocity = new Vector2(GD.RandRange(-50, 50), -200);
    }

    public override void _PhysicsProcess(double delta)
    {
        // だんだん左右の動きはゆっくりに, 重力をかける
        Velocity = new Vector2(Velocity.X * 0.95f, Velocity.Y + (float)(800 * delta));

        if (Position.Y > _groundY)
        {
            Position = Position with { Y = _groundY };
            Velocity = Velocity with { Y = Velocity.Y * -0.6f };

            if (Math.Abs(Velocity.Y) < 100) Velocity = Velocity with { Y = 0 };
        }
        MoveAndSlide();
    }
}
