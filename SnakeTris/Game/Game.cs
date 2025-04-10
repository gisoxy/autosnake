using SnakeTris.Engine;
using SnakeTris.Engine.Config;
using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Game(Field field, Snake snake, Food food)
  : ConsoleGame(new Settings())
{
  private bool _skipNextMove;

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

    var beEaten = snake.Collides(food.Position);
    if (beEaten)
    {
      snake.Grow();
      var newPosition = field.GetPosition(snake.Segments);
      food.Move(newPosition);
      return;
    }

    snake.Update();
    if (snake.Dead()) 
      Reset();
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
    snake.SetBounds(field.GetBounds());
    var position = field.GetPosition(snake.Segments);
    food.Move(position);
  }
}
