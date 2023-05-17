using Stride.Engine;
using Stride.Games;

namespace NexCoreExtension;

public static class DeltaExtension
{
    public static double Delta(this IGame gameTime)
    {
        return gameTime.UpdateTime.Elapsed.TotalSeconds;
    }
    public static float Delta(this SyncScript script)
    {
        return (float)script.Game.Delta();
    }
}
