using SnakeTris.Engine.Entities;
using SnakeTris.Game;

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

  public static Position UniqPosition(this Random self, Rectangle bounds, List<Position> ignore)
  {
      var pos = self.Position(bounds);
      while (ignore.Any(x => x.X == pos.X && x.Y == pos.Y))
        pos = self.Position(bounds);
      
      return pos;
  }
}