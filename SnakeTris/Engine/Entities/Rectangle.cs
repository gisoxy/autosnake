namespace SnakeTris.Engine.Entities;

public class Rectangle
{
  public Position Position { get; set; }
  public Size Size { get; set; }

  public Rectangle Shrink(int size)
  {
    return new Rectangle
    {
      Position = new Position(Position.X + size, Position.Y + size),
      Size = new Size(Size.Width - size, Size.Height - size)
    };
  }
}