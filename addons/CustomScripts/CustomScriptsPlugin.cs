#if TOOLS
using Godot;
using System.IO;
using System.Reflection;

[Tool]
public partial class CustomScriptsPlugin : EditorPlugin
{
    public override void _EnterTree()
    {
        // アセンブリを取得
        var assembly = Assembly.GetExecutingAssembly(); 
        foreach (var type in assembly.GetTypes())
        {
            var attr = type.GetCustomAttribute<CustomScriptAttribute>();
            if (attr != null)
            {
                var path = FindScriptPath(type.FullName);
                var script = GD.Load<Script>(path); 
                Texture2D icon = null;
                if (!string.IsNullOrEmpty(attr.IconPath))
                    icon = GD.Load<Texture2D>(attr.IconPath);

                GD.Print($"AddCustomType: {type.FullName}");
                AddCustomType(type.Name, attr.BaseType, script, icon);
            }
        }
    }

    string FindScriptPath(string typeName)
    {
        foreach (var file in Directory.GetFiles(ProjectSettings.GlobalizePath("res://"), "*.cs", SearchOption.AllDirectories))
        {
            if (Path.GetFileNameWithoutExtension(file) == typeName)
            {
                // OSパス → res:// パスに変換
                return ProjectSettings.LocalizePath(file);
            }
        }
        return null;
    }

    public override void _ExitTree()
    {
        // 登録解除
        // AddCustomType で登録したクラス名を忘れずに RemoveCustomType する
    }
}
#endif