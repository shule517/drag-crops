using Godot;
using dragcrops.lib.attributes;
using dragcrops.lib.extensions;

public partial class UpgradeItem : HBoxContainer
{
    [Node] private Label _label = null!;
    [Node] private Button _button = null!;

    public override void _Ready()
    {
        this.BindNodes();
        _button.ButtonDown += () => GameData.Instance.LevelUp();
    }

    public override void _Process(double delta)
    {
        _label.Text = GameData.Instance.Gold.ToString();
    }
}
