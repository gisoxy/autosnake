using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Food : ISegmentContainer
{
  public bool Eatable => Current!.TypeId == FoodType.Classic;
  public SegmentType Type => SegmentType.FoodSegments;

  public FoodKind Current { get; private set; }
  public FoodKind Next { get; private set; }

  private readonly FoodBuilder _builder = new();
  private Rectangle _fieldBounds;

  public Food()
  {
    Current = _builder.RandomBlock();
    Next = _builder.RandomBlock();
  }

  public void Draw(Frame frame)
  {
    if (Current == null)
      return;

    var batch = frame.CreateBatch();
    foreach (var item in Current.Segments)
      batch.Pixel(item.X, item.Y, PixelColor.Orange, local: true);
  }

  public void Move(Position offsets)
  {
    Current!.Move(offsets);
    Current!.Segments.ForEach(_fieldBounds.Normalize);
  }

  public void Relocate(Position newPosition)
  {
    Logger.Warn("Relocate");
    Current = Next;
    Current.Relocate(newPosition);
    Next = _builder.RandomBlock();
  }

  public bool IsSegmentUsed(Position position)
  {
    if (Current == null)
      return false;

    return Current.Segments.Any(x => position.X == x.X && position.Y == x.Y);
  }

  public IEnumerable<Position> GetSegments()
  {
    if (Current == null)
      return Array.Empty<Position>();

    return Current.Segments;
  }

  public void SetBounds(Rectangle fieldBounds)
  {
    _fieldBounds = fieldBounds;
  }
}
