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

  private readonly Dictionary<Direction, Direction> _opposite = new()
  {
    [Direction.Left] = Direction.Right,
    [Direction.Right] = Direction.Left,
    [Direction.Up] = Direction.Down,
    [Direction.Down] = Direction.Up,
  };

  private readonly Position _start;
  private readonly int _size;

  private List<Position> _segments = new();
  private Direction _direction = Direction.Right;

  private Rectangle _fieldBounds;
  private bool _forsedMove;

  public Snake(Position start, int size) 
  {
    _start = start;
    _size = size;
  }

  public void Update()
  {
    MoveBody();
    MoveHead();
  }

  public bool Update(ActionKey key)
  {
    var newDirection = GetDirection(key);
    var sameDirection = _direction == newDirection;
    var oppositeDirection = newDirection == _opposite[_direction];
    if (sameDirection || oppositeDirection)
      return false;

    _direction = newDirection;
    return true;
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

  public bool Dead()
  {
    for (int i = 0; i < _segments.Count - 2; i++)
    {
      var cannibalism = _segments[i].X == _segments[^1].X
                        && _segments[i].Y == _segments[^2].Y;
      if (cannibalism)
        return true;
    }

    return false;
  }

  public void Reset()
  {
    _direction = Direction.Right;
    _segments = Enumerable
      .Range(_start.X, _start.X + _size * 3)
      .Where(x => x % 3 != 0)
      .Select(x => new Position(x, _start.Y))
      .Take(_size * 2)
      .ToList(); 
  }

  public void SetBounds(Rectangle fieldBounds)
  {
    _fieldBounds = fieldBounds;
  }

  private void MoveBody()
  {
    for (int i = 0; i < _segments.Count - 2; i++)
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
    var next = new Position(head.X + offset.X, head.Y + offset.Y);
    _fieldBounds.Normalize(next);
    return next;
  }

  private Direction GetDirection(ActionKey key)
  {
    switch (key)
    {
      case ActionKey.Left: return Direction.Left;
      case ActionKey.Right: return Direction.Right;
      case ActionKey.Up: return Direction.Up;
      case ActionKey.Down: return Direction.Down;
      default:
        return _direction;
    }
  }
}
