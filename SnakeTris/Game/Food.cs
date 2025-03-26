using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Food
{
    public Position Position { get; set; } = new Position();
    
    public void Draw(Frame frame)
    {
        var batch = frame.CreateBatch();
        batch.Pixel(Position.X, Position.Y);
    }

    public void Move(Position position)
    {
        Position = position;
    }
}