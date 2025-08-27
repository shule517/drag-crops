using Godot;
using dragcrops.lib.attributes;
using dragcrops.lib.extensions;

public partial class UpgradeItem : HBoxContainer
{
    [OnReady("Label")] private Label _label;
    [OnReady("Button")] private Button _button;

    public override void _Ready()
    {
        this.BindOnReadyNodes();
        _button.ButtonDown += () => GameData.Instance.LevelUp();
    }

    public override void _Process(double delta)
    {
        _label.Text = GameData.Instance.Gold.ToString();
    }
}
