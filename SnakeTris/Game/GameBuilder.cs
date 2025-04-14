namespace SnakeTris.Game;

public class GameBuilder(Balance balance)
{
  private readonly Balance _balance = balance;

  public Game Build()
  {
    var field = new Field(_balance.Field());
    var snake = new Snake(_balance.Start(), _balance.StartSize);
    var food = new Food();

    var game = new Game(field, snake, food);
    return game;
  }
}
