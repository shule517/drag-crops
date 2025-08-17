namespace dragcrops.extenstions;
using System;

public static class IntExtension
{
    public static void Times(this int me, Action action)
    {
        for (var i = 0; i < me; i++)
        {
            action();
        }
    }
}
