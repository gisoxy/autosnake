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

  public void Normalize(Position position)
  {
    if (position.X >= Position.X + Size.Width)
      position.X -= Size.Width - 1;

    if (position.X <= Position.X)
      position.X += Size.Width - 1;

    if (position.Y >= Position.Y + Size.Height)
      position.Y -= Size.Height - 1;

    if (position.Y <= Position.Y)
      position.Y += Size.Height - 1;
  }
}