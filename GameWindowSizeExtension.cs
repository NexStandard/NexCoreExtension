using Stride.Core.Mathematics;
using Stride.Graphics;

namespace NexCoreExtension;

public static class GameWindowSizeExtension
{
    public static Int2 GetWindowSize(this GraphicsDevice graphics)
    {
        int width = graphics.Presenter.BackBuffer.Width;
        int height = graphics.Presenter.BackBuffer.Height;
        return new Int2(width, height);
    }
}
