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

    private readonly List<Position> _segments;
    private readonly List<Position> _headMoves;
    private Direction _direction = Direction.Right;

    public Snake()
    {
        _segments =
        [
            new Position { X = 3, Y = 5 },
            new Position { X = 4, Y = 5 },
            new Position { X = 5, Y = 5 },
            new Position { X = 5, Y = 5 }
        ];

        _headMoves =
        [
            new Position { X = 0, Y = -1 },
            new Position { X = 0, Y = 1 },
            new Position { X = -1, Y = 0 },
            new Position { X = 1, Y = 0 },
        ];
    }

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
    }

    public void Reset()
    {
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
        var offset = _headMoves[(int)_direction];
        var head = _segments.Count - 1;
        _segments[head].X += offset.X;
        _segments[head].Y += offset.Y;
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