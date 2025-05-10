using SnakeTris.Engine;
using SnakeTris.Engine.Config;
using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Game(Field field, Snake snake, Food food)
  : ConsoleGame(new Settings())
{
  private bool _skipNextMove;
  private PositionFinder _finder = new(field, snake, food);
  private readonly Rectangle _fieldBounds = field.GetBounds();

  private readonly LogPanel _logPanel = new();

  public void Play()
  {
    Reset();
    Subscribe();
    Start();
  }

  private void Subscribe()
  {
    ConsoleInput.Click(Action);
    ConsoleInput.Tick(Update);
    ConsoleRenderer.Draw(Call);
  }

  private void Call(Frame frame)
  {
    _logPanel.Draw(frame);

    field.Draw(frame);
    snake.Draw(frame);
    food.Draw(frame);
  }

  private void Update()
  {
    if (_skipNextMove)
    {
      _skipNextMove = false;
      return;
    }

    var collides = snake.Collides(food.GetSegments());
    if (!collides)
    {
      snake.Update();
      if (snake.Dead())
        Reset();

      return;
    }

    if (food.Eatable)
    {
      snake.Grow();
      var newPosition = _finder.GetRandomPosition(_fieldBounds, food.Next.Size);
      food.Relocate(newPosition);
      return;
    }

    if (!food.Eatable)
    {
      snake.Update();
      var vector = snake.MovingVector();
      var nextPositionStatus = _finder.AnalisePosition(_fieldBounds, food.GetSegments(), vector);
      if (nextPositionStatus == PositionStatus.UsedByField)
        return;

      food.Move(vector);
      var downPositionStatus = _finder.AnalisePosition(_fieldBounds, food.GetSegments(), Position.Down);
      if (downPositionStatus == PositionStatus.Free)
        return;

      ApplyChanges(downPositionStatus);
    }
  }

  private void ApplyChanges(PositionStatus status)
  {
    if (status == PositionStatus.UsedByField || status == PositionStatus.Bottom)
    {
      field.AttachBlock(food.GetSegments());
      // var position = _finder.GetRandomPosition(_fieldBounds, food.Next.Size);
      var position = new Position(_fieldBounds.Size.Width / 2 - food.Next.Size.Width / 2 - 1, 0);
      food.Relocate(position);
    }

    if (status == PositionStatus.UserBySnake)
    {
      Reset();
    }
  }

  private void ForceUpdate()
  {
    _skipNextMove = false;
    Update();
    _skipNextMove = true;
  }

  private void Action(ActionKey key)
  {
    switch (key)
    {
      case ActionKey.Up:
      case ActionKey.Down:
      case ActionKey.Left:
      case ActionKey.Right:
        var changed = snake.Update(key);
        if (changed)
          ForceUpdate();
        break;

      case ActionKey.Exit:
        Stop();
        break;

      default: return;
    }
  }

  private void Reset()
  {
    snake.Reset();

    snake.SetBounds(_fieldBounds);
    food.SetBounds(_fieldBounds);

    // var position = _finder.GetRandomPosition(_fieldBounds, food.Next.Size);
    var position = new Position(_fieldBounds.Size.Width / 2 - food.Next.Size.Width / 2 - 1, 0);
    food.Relocate(position);
  }
}