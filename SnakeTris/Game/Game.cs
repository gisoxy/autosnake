using SnakeTris.Engine;
using SnakeTris.Engine.Config;
using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

namespace SnakeTris.Game;

public class Game(Field field, Snake snake, Food food) 
    : ConsoleGame(new Settings())
{
    public void Play()
    {
        Reset();
        Subscribe();
        Start();
    }

    private void Call(Frame frame)
    {
        field.Draw(frame);
        snake.Draw(frame);
        // food.Draw(frame);
    }

    private void Subscribe()
    {
        ConsoleInput.Click(Action);
        ConsoleInput.Tick(Update);
        ConsoleRenderer.Draw(Call);
    }

    private void Update()
    {
        snake.Update();
    }

    private void Action(ActionKey key)
    {
        switch (key)
        {
            case ActionKey.Up: 
            case ActionKey.Down:
            case ActionKey.Left: 
            case ActionKey.Right: 
                snake.Update(key); 
                break;

            case ActionKey.Exit: 
                Stop(); 
                break;

            default: return;
        }
    }

    private void Reset()
    {
        snake.Reset();
    }
}