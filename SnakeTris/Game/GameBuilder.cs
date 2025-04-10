namespace SnakeTris.Game;

public class GameBuilder(Balance balance)
{
  private readonly Balance _balance = balance;

  public Game Build()
  {
    var field = new Field();
    var snake = new Snake();
    var food = new Food();

    var game = new Game(field, snake, food);
    return game;
  }
}