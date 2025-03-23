using SnakeTris.Engine.Entities;

namespace SnakeTris.Engine.Rendering;

public class BackedFrame(Frame frame)
{
    public FrameBuffer Flush()
    {
        var size = frame.Size;
        var buffer = new Content[size.Height, size.Width];
        for (int i = 0; i < size.Height; i++)
        {
            for (int j = 0; j < size.Width; j++)
                buffer[i, j] = Content.Transparent;
        }

        foreach (var batch in frame.Batches)
        {
            foreach (var item in batch)
                buffer[item.Position.Y, item.Position.X] = item.Content;
        }

        return new FrameBuffer(size, buffer);
    }
}