using SnakeTris.Engine.Entities;

namespace SnakeTris.Game;

public class Balance
{
  public int FieldX = 0;
  public int FieldY = 0;
  public int FieldWidth = 10;
  public int FieldHeight = 20;
  public int RectSizeX = 3;
  public int RectSizeY = 2;

  public int StartPositionX = 4;
  public int StartPositionY = 5;
  public int StartSize = 4;

  public Rectangle Field() => new Rectangle
  {
    Position = new(FieldX, FieldY),
    Size = new(FieldWidth * RectSizeX + 1, FieldHeight * RectSizeY + 1)
  };

  public Position Start() => new Position(StartPositionX, StartPositionY);
}
