using SnakeTris.Engine.Entities;

namespace SnakeTris.Engine.Extensions;

public static class RandomExtensions
{
  public static Position Position(this Random self, Rectangle bounds)
  {
    var pos = new Position(
      x: self.Next(bounds.Position.X, bounds.Size.Width),
      y: self.Next(bounds.Position.Y, bounds.Size.Height)
    );
    return pos;
  }

  public static Position Position(this Random self, Rectangle bounds, Size size)
  {
    var pos = new Position(
      x: self.Next(bounds.Position.X, bounds.Size.Width - size.Width),
      y: self.Next(bounds.Position.Y, bounds.Size.Height - size.Height)
    );
    return pos;
  }

  public static Position UniqPosition(this Random self, Rectangle bounds, List<Position> ignore)
  {
    var pos = self.Position(bounds);
    while (ignore.Any(x => x == pos))
      pos = self.Position(bounds);

    return pos;
  }
}

public static class PositionExtensions
{
  public static bool Intersect(this IEnumerable<Position> self, IEnumerable<Position> other)
  {
    foreach (var item in self)
      if (other.Any(x => x == item))
        return true;

    return false;
  }
}
