using System.Collections.Concurrent;

public class Logger
{
  private static readonly ConcurrentBag<Log> _logs = new();

  public static void Error(string message)
  {
    _logs.Add(new(Level: LogLevel.Error, Message: message, Time: DateTime.Now));
  }

  public static void Warn(string message)
  {
    _logs.Add(new(Level: LogLevel.Warn, Message: message, Time: DateTime.Now));
  }

  public static void Info(string message)
  {
    _logs.Add(new(Level: LogLevel.Info, Message: message, Time: DateTime.Now));
  }

  public static IEnumerable<Log> TakeLogs(int count)
  {
    return _logs.Take(count);
  }
}

public enum LogLevel { Info, Warn, Error }

public record Log(LogLevel Level, string Message, DateTime Time);
