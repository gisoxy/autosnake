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

    public void Up()
    {
        _height += 1;
    }

    public void Down()
    {
        _height -= 1;
    }

    public void Left()
    {
        _width -= 1;
    }

    public void Right()
    {
        _width += 1;
    }
}