using SnakeTris.Engine.Config;

namespace SnakeTris.Engine;

public abstract class ConsoleGame(Settings settings)
{
  protected readonly ConsoleInput ConsoleInput = new();
  protected readonly ConsoleRenderer ConsoleRenderer = new(settings);

  public void Start()
  {
    ConsoleInput.Begin();
    ConsoleRenderer.Init(BeforeRender, AfterRender);
    ConsoleRenderer.Begin();
  }

  public void Stop()
  {
    ConsoleInput.End();
    ConsoleRenderer.End();
  }

  public void Wait()
  {
    ConsoleInput.WhenEnd();
  }

  private void BeforeRender()
  {
    ConsoleInput.PauseUpdates();
  }

  private void AfterRender()
  {
    ConsoleInput.ResumeUpdates();
  }
}
