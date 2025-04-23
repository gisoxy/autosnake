using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Food
{
  public Position Position => _current!.Position;

  private readonly FoodBuilder _builder = new();
  private FoodKind? _current;
  
  public void Draw(Frame frame)
  {
    if (_current == null)
      return;

    var batch = frame.CreateBatch();
    var position = _current.Position;
    batch.Pixel(position.X, position.Y);
    batch.Pixel(position.X + 1, position.Y);
    batch.Text(42, 3, $"F: {position.X} {position.Y}");
  }

  public void Move(Position position)
  {
    _current = _builder.RandomFood();
    _current.Position = position;
  }
}
