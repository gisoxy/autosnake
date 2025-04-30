using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Snake : ISegmentContainer
{
  public Direction MovingDirection => _direction;

  public SegmentType Type => SegmentType.SnakeSegments;

  private readonly List<Position> _headMoves =
  [
    new(0, -1),
    new(0, 1),
    new(-1, 0),
    new(1, 0),
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
      batch.Pixel(segment.X, segment.Y, PixelColor.Green, local: true);
  }

  public bool Collides(IEnumerable<Position> positions)
  {
    var head = GetNextPosition(_segments[^1]);
    var hasCollision = positions.Any(x => x.X == head.X && x.Y == head.Y);
    return hasCollision;
  }

  public void Grow()
  {
    var newSegments = new List<Position>();
    newSegments.Add(_segments[0].Clone());
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
        && _segments[i].Y == _segments[^1].Y;
      if (cannibalism)
        return true;
    }

    return false;
  }

  public void Reset()
  {
    _direction = Direction.Right;
    _segments = Enumerable
      .Range(_start.X, _start.X + _size)
      .Select(x => new Position(x, _start.Y))
      .Take(_size)
      .ToList();
  }

  public void SetBounds(Rectangle fieldBounds)
  {
    _fieldBounds = fieldBounds;
  }

  public Position MovingVector()
  {
    var offset = _headMoves[(int)_direction];
    return offset;
  }

  public bool IsSegmentUsed(Position position)
  {
    return _segments.Any(x => position.X == x.X && position.Y == x.Y);
  }

  public IEnumerable<Position> GetSegments()
  {
    return _segments;
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
    _segments[^1] = GetNextPosition(_segments[^1]);
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
