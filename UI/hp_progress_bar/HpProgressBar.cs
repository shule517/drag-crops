using Godot;

[CustomScript("ProgressBar", "res://addons/CustomScripts/icon-export.png")]
public partial class HpProgressBar : ProgressBar
{
    public override void _Ready()
    {
        Visible = false;
        ZIndex = 100;
    }
}
