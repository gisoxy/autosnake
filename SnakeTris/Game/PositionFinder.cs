using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Extensions;

public class PositionFinder
{
  private Random _random = new();
  private ISegmentContainer[] _containers;

  public PositionFinder(params ISegmentContainer[] containers)
  {
    _containers = containers;
  }

  public Position GetRandomPosition(Rectangle space)
  {
    var position = _random.Position(space);
    while (_containers.Any(x => x.IsSegmentUsed(position)))
      position = _random.Position(space);

    return position;
  }

  public Position GetRandomPosition(Rectangle space, Size size)
  {
    var position = _random.Position(space, size);
    while (_containers.Any(x => x.IsSegmentUsed(position)))
      position = _random.Position(space);

    return position;
  }

  public PositionStatus AnalisePosition(Rectangle space, IEnumerable<Position> positions, Position vector)
  {
    if (space.AtBottom(positions))
      return PositionStatus.Bottom;

    var nextPositions = positions.Select(x => x.Add(vector)).ToList();
    var fieldContainer = _containers.First(x => x.Type == SegmentType.FieldSegments);

    if (nextPositions.Intersect(fieldContainer.GetSegments()))
      return PositionStatus.UsedByField;

    return PositionStatus.Free;
  }
}

public enum PositionStatus
{
  Free,
  UsedByField,
  UserBySnake,
  Bottom
}

public interface ISegmentContainer
{
  bool IsSegmentUsed(Position position);
  IEnumerable<Position> GetSegments();
  SegmentType Type { get; }
}

public enum SegmentType
{
  SnakeSegments,
  FoodSegments,
  FieldSegments
}
