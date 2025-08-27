using System;
using System.Reflection;
using dragcrops.lib.attributes;
using Godot;

namespace dragcrops.lib.extensions;

public static class NodeExtensions
{
    public static void BindOnReadyNodes(this Node me)
    {
        var fields = me.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<OnReadyAttribute>();
            if (attribute != null)
            {
                var node = me.GetNode<Node>(attribute.Path);

                if (node == null)
                    throw new InvalidOperationException($"Nodeが見つかりませんでした: {attribute.Path}");

                field.SetValue(me, node);
            }
        }
    }
}