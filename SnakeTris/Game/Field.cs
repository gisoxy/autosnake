using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Extensions;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Field
{
  private readonly Rectangle _bounds = new()
  {
    Position = new(0, 0),
    Size = new(40, 20),
  };

  private readonly Rectangle _area;

  public Field()
  {
    _area = _bounds.Shrink(1);
  }

  public void Draw(Frame frame)
  {
    var batch = frame.CreateBatch();
    batch.Rect(_bounds.Position.X, _bounds.Position.Y, _bounds.Size.Width, _bounds.Size.Height);
  }

  public Position GetPosition(List<Position> skip)
  {
    var random = new Random();
    return random.UniqPosition(_area, skip);
  }
}