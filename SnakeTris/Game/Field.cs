using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Extensions;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Field
{
  private readonly Rectangle _bounds = new()
  {
    Position = new(0, 0),
    Size = new(41, 21),
  };

  private readonly Rectangle _area;

  public Field()
  {
    _area = _bounds.Shrink(1);
  }

  public void Draw(Frame frame)
  {
    var batch = frame.CreateBatch();
    batch.FilledSmoothRect(_bounds);
  }

  public Position GetPosition(List<Position> skip)
  {
    var random = new Random();
    return random.UniqPosition(_area, skip);
  }
}