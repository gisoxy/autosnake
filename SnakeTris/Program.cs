using SnakeTris.Game;

var game = new GameBuilder(new Balance())
  .Build();

game.Play();
game.Wait();

