namespace SnakeTris.Engine.Entities;

public class Size(int width, int height)
{
    public int Width { get; set; } = width;
    public int Height { get; set; } = height;

    public Size(int size) : this(size, size)
    {
    }
}