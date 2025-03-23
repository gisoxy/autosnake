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
        food.Draw(frame);
    }

    private void Subscribe()
    {
        ConsoleInput.Click(Action);
        ConsoleInput.Tick(Update);
        ConsoleRenderer.Draw(Call);
    }

    private void Update()
    {
    }

    private void Action(ActionKey key)
    {
        switch (key)
        {
            case ActionKey.Up: 
                field.Up(); 
                break;
            case ActionKey.Down:
                field.Down(); 
                break;
            case ActionKey.Left: 
                field.Left(); 
                break;
            case ActionKey.Right: 
                field.Right(); 
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