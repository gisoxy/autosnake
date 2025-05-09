using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Field : ISegmentContainer
{
  public SegmentType Type => SegmentType.FieldSegments;

  private readonly Rectangle _bounds;
  private readonly Rectangle _area;

  private List<Position> _blocks = new();

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
    RemoveLines();
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

  private void RemoveLines(int blocksPerLine = 10)
  {
    var countByLine = _blocks
      .GroupBy(x => x.Y)
      .ToDictionary(x => x.Key, x => x.Count());

    var linesToRemove = countByLine
      .Where(x => x.Value == blocksPerLine)
      .Select(x => x.Key)
      .OrderByDescending(x => x)
      .ToList();

    foreach (var line in linesToRemove)
    {
      _blocks = _blocks.Where(x => x.Y != line).ToList();
      _blocks.ForEach(x =>
      {
        if (x.Y < line)
          x.Y++;
      });
    }
  }
}
