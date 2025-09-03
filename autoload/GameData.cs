using System.Collections.Generic;
using dragcrops.lib.extensions;
using Godot;

// TODO: staticクラスで良いのでは？
public partial class GameData : Node
{
    public static GameData Instance => SceneTree.Root.GetNode<GameData>("/root/GameData");
    private static SceneTree SceneTree => (SceneTree)Engine.GetMainLoop();

    public long Gold { get; private set; } = 0;
    public int Level { get; private set; } = 1;
    // public long TreeGold { get { return _goldTable[Level]; }}
    public long TreeGold { get; private set; } = 5;
    public int Exp { get; private set; } = 0;

    public string GetGoldText()
    {
        if (Gold > 100000000)
            return $"{Gold/100000000.0f:F2} 億円";
        if (Gold > 10000)
            return $"{Gold/10000.0f:F2} 万円";

        return $"{Gold} 円";
    }

    public bool GetTreeGold()
    {
        Gold += TreeGold;
        Exp += 1;
        if (Exp >= 10)
        {
            LevelUp();
            Exp = 0;
            return true;
        }
        return false;
    }

    public void LevelUp()
    {
        Level++;
        TreeGold = (long)(TreeGold * 2.7);
        GD.Print("LevelUp" + Level);
    }

    private List<long> _goldTable = new List<long>()
    {
        // 序盤
        5, 10, 25, 50, 100, 250, 500, 1000, 2500, 5000,
        // 中盤
        10000, 25000, 50000, 100000, 250000, 500000, 1000000, 2500000, 5000000, 10000000,
        // 後半
        100000000, 250000000, 500000000, 1000000000, 2500000000, 5000000000, 10000000000, 25000000000, 50000000000, 100000000000
    };
}
