using System;
using System.Linq;
using System.Reflection;
using dragcrops.lib.attributes;
using Godot;

namespace dragcrops.lib.extensions;

public static class NodeExtensions
{
    public static void AutoLoad(this Node me)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var autoLoadClasses = assembly.GetTypes().Where(t => t.GetCustomAttribute<AutoLoadAttribute>() != null);
        foreach (var autoLoadClass in autoLoadClasses)
        {
            var instance = Activator.CreateInstance(autoLoadClass) as Node;
            if (instance != null)
            {
                instance.Name = autoLoadClass.Name;
                me.CallDeferred("add_child", instance);
            }

            // TODO: AutoLoadしたインスタンスを取得できるようにする
            GD.Print(autoLoadClass);
        }
    }

    public static void BindOnReadyNodes(this Node me)
    {
        var fields = me.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<NodeAttribute>();
            if (attribute == null)
            {
                continue;
            }

            if (attribute.Path == null)
            {
                // TODO: プロパティ名と一致するものを取得
            }
            else
            {
                // 指定されたパスのNodeを取得する
                var node = me.GetNode<Node>(attribute.Path);
                if (node == null)
                    throw new InvalidOperationException($"Nodeが見つかりませんでした: {attribute.Path}");

                field.SetValue(me, node);
            }
        }
    }
}