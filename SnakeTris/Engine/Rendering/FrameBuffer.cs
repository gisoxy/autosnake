using SnakeTris.Engine.Entities;

namespace SnakeTris.Engine.Rendering;

public class FrameBuffer(Size size, Content[,] value)
{
    private Content[,] Value { get; } = value;

    public Content[,] GetChanges(FrameBuffer newBuffer)
    {
        for (int i = 0; i < size.Height; i++)
        {
            for (int j = 0; j < size.Width; j++)
            {
                var left = Value[i, j];
                var right = newBuffer.Value[i, j];
                
                if (left.Character == right.Character && left.Foreground == right.Foreground && left.Background == right.Background)
                    Value[i, j] = Content.Empty;
                else
                    Value[i, j] = newBuffer.Value[i, j];
            }
        }

        return Value;
    }
}