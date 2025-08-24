using Godot;
using System;

public partial class GoldPanel : Panel
{
    private Label _label;

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
    }

    public override void _Process(double delta)
    {
        _label.Text = GameData.Instance.GetGoldText();
    }
}
