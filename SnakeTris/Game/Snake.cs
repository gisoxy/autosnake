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
    new(-3, 0),
    new(3, 0),
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

  public bool Collides(Position position)
  {
    var head = GetNextPosition(_segments[^2]);
    var hasCollision = head.X == position.X && head.Y == position.Y;
    return hasCollision;
  }

  public void Grow() 
  {
    var newSegments = new List<Position>();
    newSegments.Add(_segments[0].Clone());
    newSegments.Add(_segments[1].Clone());
    newSegments.AddRange(_segments);
    _segments = newSegments;
    MoveBody();
    MoveHead();
  }

  public void Reset()
  {
    _segments =
    [
      new Position (3, 5),
      new Position (4, 5),
      new Position (7, 5),
      new Position (8, 5),
      new Position (10, 5),
      new Position (11, 5),
      new Position (13, 5),
      new Position (14, 5)
    ];
    
  }

  private void MoveBody()
  {
    for (int i = 0; i < _segments.Count - 2; i ++)
    {
      _segments[i].X = _segments[i + 2].X;
      _segments[i].Y = _segments[i + 2].Y;
    }
  }

  private void MoveHead()
  {
    _segments[^1] = GetNextPosition(_segments[^1]);
    _segments[^2] = GetNextPosition(_segments[^2]);
  }

  private Position GetNextPosition(Position head)
  {
    var offset = _headMoves[(int)_direction];
    return new Position (head.X + offset.X, head.Y + offset.Y);
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
