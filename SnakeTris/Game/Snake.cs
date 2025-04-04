using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Snake
{
  private enum Direction
  {
    Up,
    Down,
    Left,
    Right
  }

  public List<Position> Segments => _segments;

  private readonly List<Position> _headMoves =
  [
    new(0, -2),
    new(0, 2),
    new(-2, 0),
    new(2, 0),
  ];

  private List<Position> _segments = new();
  private Direction _direction = Direction.Right;

  public void Update()
  {
    MoveBody();
    MoveHead();
  }

  public void Update(ActionKey key)
  {
    switch (key)
    {
      case ActionKey.Left:
        HeadLeft();
        break;
      case ActionKey.Right:
        HeadRight();
        break;
      case ActionKey.Up:
        HeadUp();
        break;
      case ActionKey.Down:
        HeadDown();
        break;
      default:
        return;
    }
  }

  public void Draw(Frame frame)
  {
    var batch = frame.CreateBatch();
    foreach (var segment in _segments)
      batch.Pixel(segment.X, segment.Y);
    
    var headPos = _segments[^1];
    batch.Text(42, 4, $"H: {headPos.X} {headPos.Y}");
  }

  public bool TryEat(Position position)
  {
    var next = GetNextPosition();
    var canEat = next.X == position.X && next.Y == position.Y;
    if (canEat)
      _segments.Add(position);
    
    return canEat;
  }

  public void Reset()
  {
    _segments =
    [
      new Position (3, 5),
      new Position (5, 5),
      new Position (7, 5),
      new Position (9, 5)
    ];
  }

  private void MoveBody()
  {
    for (int i = 0; i < _segments.Count - 1; i++)
    {
      _segments[i].X = _segments[i + 1].X;
      _segments[i].Y = _segments[i + 1].Y;
    }
  }

  private void MoveHead()
  {
    var next = GetNextPosition();
    _segments[^1].X = next.X;
    _segments[^1].Y = next.Y;
  }

  private Position GetNextPosition()
  {
    var offset = _headMoves[(int)_direction];
    var headPos = _segments[^1];
    return new Position (headPos.X + offset.X,  headPos.Y + offset.Y);
  }
  
  private void HeadUp()
  {
    if (_direction == Direction.Down)
      return;

    _direction = Direction.Up;
  }

  private void HeadDown()
  {
    if (_direction == Direction.Up)
      return;

    _direction = Direction.Down;
  }

  private void HeadLeft()
  {
    if (_direction == Direction.Right)
      return;

    _direction = Direction.Left;
  }

  private void HeadRight()
  {
    if (_direction == Direction.Left)
      return;

    _direction = Direction.Right;
  }
}