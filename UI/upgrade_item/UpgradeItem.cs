using Godot;
using System;

public partial class UpgradeItem : HBoxContainer
{
    private Label _label;
    private Button _button;

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        _button = GetNode<Button>("Button");
        _button.ButtonDown += () => GameData.Instance.LevelUp();
    }

    public override void _Process(double delta)
    {
        _label.Text = GameData.Instance.Gold.ToString();
    }
}
