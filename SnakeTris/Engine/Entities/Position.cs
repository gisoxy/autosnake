namespace SnakeTris.Engine.Entities;

public class Position(int x, int y)
{
  public int X { get; set; } = x;
  public int Y { get; set; } = y;

  public Position Clone()
  {
    return new Position(X, Y);
  }

  public Position Add(Position position)
  {
    return new(X + position.X, Y + position.Y);
  }

  public override bool Equals(object? obj)
  {
    if (obj is Position position)
      return position.X == X && position.Y == position.Y;

    return false;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(X, Y);
  }
}
