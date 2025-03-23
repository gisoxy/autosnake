namespace SnakeTris.Engine.Entities;

public class Size
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Size(int size)
    {
        Width = Height = size;
    }
}