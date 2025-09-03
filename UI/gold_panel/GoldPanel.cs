using Godot;
using dragcrops.lib.attributes;
using dragcrops.lib.extensions;

public partial class GoldPanel : Panel
{
    [Node("Label")] private Label _label = null!;

    public override void _Ready()
    {
        this.BindNodes();
    }

    public override void _Process(double delta)
    {
        _label.Text = GameData.Instance.Level + " Lv\n" + GameData.Instance.GetGoldText();
    }
}
