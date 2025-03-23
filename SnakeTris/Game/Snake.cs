using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Snake
{
    private Position? _initialPosition;
    private Position? _position;

    public void Go(Position position)
    {
        _initialPosition = _position = position;
    }

    public void Up()
    {
        _position!.Y--;
    }

    public void Down()
    {
        _position!.Y++;
    }

    public void Left()
    {
        _position!.X--;
    }

    public void Right()
    {
        _position!.X++;
    }

    public void Draw(Frame frame)
    {
        var batch = frame.CreateBatch();
        batch.Pixel(0, 0);
    }

    public void Reset()
    {
        _position = _initialPosition;
    }
}