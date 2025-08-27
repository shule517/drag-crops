using System;

namespace dragcrops.lib.attributes;

[AttributeUsage(AttributeTargets.Field)]
public class OnReadyAttribute(string path) : Attribute
{
    public string Path { get; } = path;
}
