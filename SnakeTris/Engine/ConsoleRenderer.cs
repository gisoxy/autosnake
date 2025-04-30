using SnakeTris.Engine.Config;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Engine;

public class ConsoleRenderer(Settings settings)
{
  private bool _running;
  private Action<Frame>? _draw;
  private Thread? _rendererThread;
  private Action? _beforeRender;
  private Action? _afterRender;

  private readonly Frame _mainFrame = new(settings.Screen);
  private readonly ConsoleOutput _output = new(settings.Screen);

  public void Init(Action beforeRender, Action afterRender)
  {
    _beforeRender = beforeRender;
    _afterRender = afterRender;
  }

  public void Draw(Action<Frame> draw)
  {
    _draw = draw;
  }

  public void Begin()
  {
    _running = true;

    _rendererThread = new(ProcessRendering);
    _rendererThread.Start();
  }

  public void End()
  {
    _running = false;
    _output.Clean();
  }

  private void ProcessRendering()
  {
    while (_running)
    {
      try
      {
        _beforeRender?.Invoke();

        var frame = new Frame(settings.Screen);
        _draw?.Invoke(frame);

        var updates = _mainFrame.Sync(frame);
        _output.Add(updates);

        _afterRender?.Invoke();

        Thread.Sleep(1000 / 60);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
    }
  }
}
