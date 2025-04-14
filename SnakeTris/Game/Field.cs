using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Extensions;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Field
{
  private readonly Rectangle _bounds = new()
  {
    Position = new(0, 0),
    Size = new(31, 41),
  };

  private readonly Rectangle _area;

  public Field(Rectangle bounds)
  {
    _bounds = bounds;
    _area = bounds.Shrink(1);
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

  public Rectangle GetBounds()
  {
    return _bounds;
  }
}
