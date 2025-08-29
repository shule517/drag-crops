using Godot;

[CustomScript("ProgressBar", "res://addons/CustomScripts/icon-export.png")]
public partial class HpProgressBar : ProgressBar
{
    public override void _Ready()
    {
        Visible = false;
        ZIndex = 100;
    }

    public void UpdateProgress(int currentValue, int maxValue)
    {
        if (currentValue != maxValue)
        {
            Value = (int)(currentValue * 100 / maxValue);
            Visible = true;
        }
    }
}
