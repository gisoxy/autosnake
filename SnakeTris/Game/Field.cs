using System.Linq.Expressions;
using SnakeTris.Engine.Entities;
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

  public Position GetPosition(List<Position> skip)
  {
    var random = new Random();
    var position = new Position();
    position.X = random.Next(_width);
    position.Y = random.Next(_height);

    while (skip.Any(x => x.X == position.X && x.Y == position.Y))
    {
      position.X = random.Next(_width);
      position.Y = random.Next(_height);
    }
    
    return position;
  }
}