using SnakeTris.Engine.Entities;

namespace SnakeTris.Game;

public enum FoodType
{
  Classic, BlockO, BlockI, BlockL, BlockT, BlockN, BlockS
}

public class FoodBuilder
{
  public Func<FoodKind> ClassicFood = CreateClassic;

  public Func<FoodKind> BlockO = CreateBlockO;
  public Func<FoodKind> BlockI = CreateBlockI;
  public Func<FoodKind> BlockL = CreateBlockL;
  public Func<FoodKind> BlockT = CreateBlockT;
  public Func<FoodKind> BlockN = CreateBlockN;
  public Func<FoodKind> BlockS = CreateBlockS;

  public List<Func<FoodKind>> Blocks;
  public List<Func<FoodKind>> BlocksAndFood;
  public List<Func<FoodKind>> Food;

  public FoodBuilder()
  {
    Blocks = new() {
      // BlockO,
      // BlockI,
      BlockL,
      // BlockT,
      // BlockN,
      // BlockS,
    };

    BlocksAndFood = new() {
      ClassicFood,
      BlockO,
      BlockI,
      BlockL,
      BlockT,
      BlockN,
      BlockS,
    };

    Food = new() { ClassicFood };
  }

  public FoodKind RandomAny()
  {
    var index = Random.Shared.Next(BlocksAndFood.Count);
    return BlocksAndFood[index]().Init();
  }

  public FoodKind RandomBlock()
  {
    var index = Random.Shared.Next(Blocks.Count);
    return Blocks[index]().Init();
  }

  public FoodKind RandomFood()
  {
    var index = Random.Shared.Next(Food.Count);
    return Food[index]().Init();
  }

  private static FoodKind CreateClassic()
  {
    return new FoodKind
    {
      TypeId = FoodType.Classic,
      Shape = new int[,] {
        {1}
      }
    };
  }

  private static FoodKind CreateBlockO()
  {
    return new FoodKind
    {
      TypeId = FoodType.BlockO,
      Shape = new int[,] {
        {1, 1},
        {1, 1}
      }
    };
  }

  private static FoodKind CreateBlockI()
  {
    return new FoodKind
    {
      TypeId = FoodType.BlockI,
      Shape = new int[,] {
        {0, 1, 0, 0},
        {0, 1, 0, 0},
        {0, 1, 0, 0},
        {0, 1, 0, 0}
      }
    };
  }

  private static FoodKind CreateBlockL()
  {
    return new FoodKind
    {
      TypeId = FoodType.BlockL,
      Size = new(3, 3),
      Shape = new int[,] {
        {0, 1, 0},
        {0, 1, 0},
        {0, 1, 1}
      }
    };
  }

  private static FoodKind CreateBlockT()
  {
    return new FoodKind
    {
      TypeId = FoodType.BlockT,
      Shape = new int[,] {
        {0, 1, 0},
        {0, 1, 1},
        {0, 1, 0}
      }
    };
  }

  private static FoodKind CreateBlockN()
  {
    return new FoodKind
    {
      TypeId = FoodType.BlockN,
      Shape = new int[,] {
        {0, 0, 0},
        {1, 1, 0},
        {0, 1, 1}
      }
    };
  }

  private static FoodKind CreateBlockS()
  {
    return new FoodKind
    {
      TypeId = FoodType.BlockS,
      Shape = new int[,] {
        {0, 0, 0},
        {0, 1, 1},
        {1, 1, 0}
      }
    };
  }
}

public class FoodKind
{
  public required FoodType TypeId { get; init; }
  public required int[,] Shape { get; set; }

  public Size Size { get; set; } = new(0, 0);
  public List<Position> Segments => _shapePositions;

  private List<Position> _shapePositions = new();

  public FoodKind Init()
  {
    _shapePositions = Sync();
    CalcSize();
    return this;
  }

  public void Rotate()
  {
    var size = Shape.Length;
    for (int layer = 0; layer < size / 2; layer++)
    {
      int first = layer;
      int last = size - 1 - layer;
      for (int i = first; i < last; i++)
      {
        int offset = i - first;
        int top = Shape[first, i];
        Shape[first, i] = Shape[last - offset, first];
        Shape[last - offset, first] = Shape[last, last - offset];
        Shape[last, last - offset] = Shape[i, last];
        Shape[i, last] = top;
      }
    }
  }

  public void Relocate(Position position)
  {
    _shapePositions = Sync(position);
  }

  public void Move(Position offset)
  {
    var segments = new List<Position>();
    foreach (var item in _shapePositions)
      segments.Add(item.Add(offset));

    _shapePositions = segments;
  }

  private void CalcSize()
  {
    var startX = _shapePositions.MinBy(x => x.X)?.X ?? 0;
    var endX = _shapePositions.MaxBy(x => x.X)?.X ?? 0;

    var startY = _shapePositions.MinBy(x => x.Y)?.Y ?? 0;
    var endY = _shapePositions.MaxBy(x => x.Y)?.Y ?? 0;

    var width = endX - startX + 1;
    var height = endY - startY + 1;

    Size = new(width, height);
  }

  private List<Position> Sync()
  {
    return Sync(new Position(0, 0));
  }

  private List<Position> Sync(Position position)
  {
    var segments = new List<Position>();
    for (var y = 0; y < Shape.GetLength(0); y++)
    {
      for (var x = 0; x < Shape.GetLength(1); x++)
      {
        var shapeValue = Shape[y, x];
        if (shapeValue == 0)
          continue;

        var newPosition = new Position(x + position.X, y + position.Y);
        segments.Add(newPosition);
      }
    }
    return segments;
  }
}
