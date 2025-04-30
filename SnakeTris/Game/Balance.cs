using SnakeTris.Engine.Entities;

namespace SnakeTris.Game;

public class Balance
{
  public int FieldX = 0;
  public int FieldY = 0;
  public int FieldWidth = 10;
  public int FieldHeight = 20;

  public int StartPositionX = 0;
  public int StartPositionY = 0;
  public int StartSize = 4;

  public Rectangle Field() => new Rectangle
  {
    Position = new(FieldX, FieldY),
    Size = new(FieldWidth, FieldHeight)
  };

  public Position Start() => new Position(StartPositionX, StartPositionY);
}
