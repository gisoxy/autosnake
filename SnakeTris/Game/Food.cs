using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Food
{
    public void Draw(Frame frame)
    {
        var batch = frame.CreateBatch();
        batch.Pixel(4, 4);
    }
}