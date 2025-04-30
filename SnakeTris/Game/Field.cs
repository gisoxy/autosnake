using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Field : ISegmentContainer
{
  public SegmentType Type => SegmentType.FieldSegments;

  private readonly Rectangle _bounds;
  private readonly Rectangle _area;

  private readonly List<Position> _blocks = new();

  public Field(Rectangle bounds)
  {
    _bounds = bounds;
    _area = bounds.Shrink(1);
  }

  public void Draw(Frame frame)
  {
    var batch = frame.CreateBatch();
    batch.FilledSmoothRect(_bounds, local: true);

    foreach (var item in _blocks)
      batch.Pixel(item.X, item.Y, PixelColor.Yellow, local: true);
  }

  public void AttachBlock(IEnumerable<Position> block)
  {
    _blocks.AddRange(block);
  }

  public Rectangle GetBounds()
  {
    return _bounds;
  }

  public IEnumerable<Position> GetSegments()
  {
    return _blocks;
  }

  public bool IsSegmentUsed(Position position)
  {
    return _blocks.Any(x => x == position);
  }
}
