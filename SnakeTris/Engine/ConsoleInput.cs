using SnakeTris.Engine.Entities;

namespace SnakeTris.Engine;

public class ConsoleInput
{
  private bool _running;

  private Action? _timerAction;
  private Action<ActionKey>? _keyPressAction;

  private Thread? _timerThread;
  private Thread? _inputThread;

  private int _speed = 1500;

  public void Begin()
  {
    _running = true;

    _timerThread = new Thread(ProcessTimer);
    _inputThread = new Thread(ProcessInput);

    _inputThread.Start();
    _timerThread.Start();
  }

  public void End()
  {
    _running = false;
  }

  public void Click(Action<ActionKey>? action)
  {
    _keyPressAction = action;
  }

  public void Tick(Action? action)
  {
    _timerAction = action;
  }

  private void SpeedUp()
  {
    _speed = Math.Max(100, _speed - 100);
  }

  public void WhenEnd()
  {
    while (_running)
    {
      Thread.Sleep(400);
    }
  }

  private void ProcessTimer()
  {
    while (_running)
    {
      _timerAction?.Invoke();
      Thread.Sleep(_speed);
    }
  }

  private void ProcessInput()
  {
    while (_running)
    {
      var keyInfo = Console.ReadKey(true);
      var key = Map(keyInfo);
      if (key == null)
        continue;

      _keyPressAction?.Invoke(key.Value);
    }
  }

  private ActionKey? Map(ConsoleKeyInfo key)
  {
    switch (key.Key)
    {
      case ConsoleKey.H: return ActionKey.Left;
      case ConsoleKey.J: return ActionKey.Down;
      case ConsoleKey.K: return ActionKey.Up;
      case ConsoleKey.L: return ActionKey.Right;

      case ConsoleKey.Escape: return ActionKey.Exit;


      default: return null;
    }
  }
}