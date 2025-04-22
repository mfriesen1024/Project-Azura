using ProjectAzura.src.Entity;
using System;

namespace ProjectAzura.src
{
    public class GlobalEventSystem
    {
        internal static void DoNothing(Ship ship)
        {
        }

        internal static void DoNothing()
        {
        }

        internal static void DoNothing(int obj)
        {
            throw new NotImplementedException();
        }
    }
}