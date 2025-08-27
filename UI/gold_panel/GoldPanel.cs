using Godot;
using dragcrops.lib.attributes;
using dragcrops.lib.extensions;

public partial class GoldPanel : Panel
{
    [OnReady("Label")] private Label _label;

    public override void _Ready()
    {
        this.BindOnReadyNodes();
    }

    public override void _Process(double delta)
    {
        _label.Text = GameData.Instance.Level + " Lv\n" + GameData.Instance.GetGoldText();
    }
}
