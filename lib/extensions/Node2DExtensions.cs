using System.Reflection;
using dragcrops.lib.attributes;
using Godot;

namespace dragcrops.lib.extensions;

public static class Node2DExtensions
{
    public static void BindOnReadyNodes(this Node2D me)
    {
        var fields = me.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<OnReadyAttribute>();
            if (attribute != null)
            {
                var node = me.GetNode<Node>(attribute.Path);
                field.SetValue(me, node);
            }
        }
    }
}