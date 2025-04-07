using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Food
{
    public Position Position { get; set; } = new Position(0, 0);
    
    public void Draw(Frame frame)
    {
        var batch = frame.CreateBatch();
        batch.Pixel(Position.X, Position.Y);
        batch.Pixel(Position.X + 1, Position.Y);
        
        batch.Text(42, 3, $"F: {Position.X} {Position.Y}");
    }

    public void Move(Position position)
    {
        Position = position;
    }
}
