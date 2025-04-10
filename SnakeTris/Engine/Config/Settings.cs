using SnakeTris.Engine.Entities;

namespace SnakeTris.Engine.Config;

public class Settings
{
  public ScreenSettings Screen { get; set; } = new()
  {
    Size = new Size(50)
  };

  public class ScreenSettings
  {
    public Size Size { get; set; }
  }
}