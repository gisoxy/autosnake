using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Field
{
    private int _width = 40;
    private int _height = 20;
    
    public void Draw(Frame frame)
    {
        var batch = frame.CreateBatch();
        batch.Rect(0, 0, _width, _height);
    }
}