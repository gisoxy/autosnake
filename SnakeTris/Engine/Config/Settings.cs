using SnakeTris.Engine.Entities;

namespace SnakeTris.Engine.Config;

public class Settings
{
  public ScreenSettings Screen { get; set; } = new()
  {
    Size = new Size(100, 41),
    LocalCoordsTranslationMap = new Position(3, 2),
    LocalWidthMultiply = 2
  };

  public class ScreenSettings
  {
    public int LocalWidthMultiply { get; set; }
    public Position LocalCoordsTranslationMap { get; set; }
    public Size Size { get; set; }
  }
}
