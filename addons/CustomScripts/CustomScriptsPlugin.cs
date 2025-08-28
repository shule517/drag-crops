#if TOOLS
using Godot;
using System;
using System.Linq;
using System.Reflection;

[Tool]
public partial class CustomScriptsPlugin : EditorPlugin
{
    public override void _EnterTree()
    {
        // アセンブリを取得
        var assembly = Assembly.Load("YourGameAssemblyName"); 
        foreach (var type in assembly.GetTypes())
        {
            var attr = type.GetCustomAttribute<CustomScriptAttribute>();
            if (attr != null)
            {
                var script = GD.Load<Script>(type.FullName + ".cs"); 
                Texture2D icon = null;
                if (!string.IsNullOrEmpty(attr.IconPath))
                    icon = GD.Load<Texture2D>(attr.IconPath);

                AddCustomType(type.Name, attr.BaseType, script, icon);
            }
        }
    }

    public override void _ExitTree()
    {
        // 登録解除
        // AddCustomType で登録したクラス名を忘れずに RemoveCustomType する
    }
}
#endif