using SnakeTris.Engine.Config;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Engine;

public class ConsoleRenderer(Settings settings)
{
    private bool _running;
    private Action<Frame>? _draw;
    private Thread? _rendererThread;

    private readonly Frame _mainFrame = new(settings.Screen);
    private readonly ConsoleOutput _output = new(settings.Screen);
    
    public void Draw(Action<Frame> draw)
    {
        _draw = draw;
    }

    public void Begin()
    {
        _running = true;
        
        _rendererThread = new (ProcessRendering);
        _rendererThread.Start();
    }

    public void End()
    {
        _running = false;
    }
    
    private void ProcessRendering()
    {
        while (_running)
        {
            var frame = new Frame(settings.Screen);
            _draw?.Invoke(frame);

            var updates = _mainFrame.Sync(frame);
            _output.Add(updates);

            Thread.Sleep(1000/60);
        }
    }
}