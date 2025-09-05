namespace dragcrops.UI.gold_panel;

using autoload;
using Godot;
using lib.attributes;
using lib.extensions;

public partial class GoldPanel : Panel
{
    [Node] private Label _label = null!;

    public override void _Ready()
    {
        this.BindNodes();
    }

    public override void _Process(double delta)
    {
        _label.Text = GameData.Instance.Level + " Lv\n" + GameData.Instance.GetGoldText();
    }
}
