namespace SnakeTris.Engine.Entities;

public class Position(int x, int y)
{
  public static readonly Position Up = new(0, -1);
  public static readonly Position Down = new(0, 1);
  public static readonly Position Left = new(-1, 0);
  public static readonly Position Right = new(1, 0);

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
    {
      return position.X == X && position.Y == Y;
    }

    return false;
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(X, Y);
  }

  public override string ToString()
  {
    return $"{X},{Y}";
  }

  public static bool operator ==(Position a, Position b)
  {
    if (ReferenceEquals(a, b)) return true;
    if (a is null || b is null) return false;
    return a.Equals(b);
  }

  public static bool operator !=(Position a, Position b)
  {
    return !(a == b);
  }
}
