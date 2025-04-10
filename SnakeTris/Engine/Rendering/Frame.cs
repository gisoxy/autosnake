using SnakeTris.Engine.Config;
using SnakeTris.Engine.Entities;

namespace SnakeTris.Engine.Rendering;

public class Frame(Settings.ScreenSettings screen)
{
  public Size Size => screen.Size;
  public List<Batch> Batches => _batches;

  private readonly List<Batch> _batches = new();
  private FrameBuffer? _currentBuffer;

  public Batch CreateBatch()
  {
    var batch = new Batch();
    _batches.Add(batch);
    return batch;
  }

  public Content[,] Sync(Frame frame)
  {
    _currentBuffer ??= new BackedFrame(this).Flush();
    var newBuffer = new BackedFrame(frame).Flush();
    var changes = _currentBuffer.GetChanges(newBuffer);

    _currentBuffer = newBuffer;

    return changes;
  }
}