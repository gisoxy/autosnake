using SnakeTris.Engine.Entities;

namespace SnakeTris.Game;

public enum FoodType 
{
  Classic, BlockO, BlockI, BlockL, BlockT, BlockN, BlockS
}

public class FoodBuilder
{
  public Func<FoodKind> ClassicFood = () => new FoodKind { TypeId = FoodType.Classic, Shape = new int [,] {{0}} };

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
      BlockO,
      BlockI,
      BlockL,
      BlockT,
      BlockN,
      BlockS,
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
    return BlocksAndFood[index]();
  }

  public FoodKind RandomBlock() 
  {
    var index = Random.Shared.Next(Blocks.Count);
    return Blocks[index]();
  }

  public FoodKind RandomFood() 
  {
    var index = Random.Shared.Next(Food.Count);
    return Food[index]();
  }

  private static FoodKind CreateBlockO() 
  {
    return new FoodKind 
    {
      TypeId = FoodType.BlockO, 
      Shape = new int [,] { 
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
      Shape = new int [,] { 
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
      Shape = new int [,] { 
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
      Shape = new int [,] { 
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
      Shape = new int [,] { 
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
      Shape = new int [,] { 
        {0, 0, 0},
        {0, 1, 1},
        {1, 1, 0}
      }
    };
  }
}

public class FoodKind
{
  public Position Position { get; set; } = new(0, 0);
  public required FoodType TypeId { get; init; }
  public required int[,] Shape { get; set; }

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
}





