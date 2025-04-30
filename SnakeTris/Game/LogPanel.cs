using System.Text.RegularExpressions;
using SnakeTris.Engine.Entities;
using SnakeTris.Engine.Rendering;

public class LogPanel
{
  private readonly Dictionary<LogLevel, PixelColor> _colorByLevel = new()
  {
    [LogLevel.Error] = PixelColor.Red,
    [LogLevel.Warn] = PixelColor.Yellow,
    [LogLevel.Info] = PixelColor.Green,
  };

  private readonly Dictionary<LogLevel, string> _prefByLevel = new()
  {
    [LogLevel.Error] = "[ERR]",
    [LogLevel.Warn] = "[WRN]",
    [LogLevel.Info] = "[INF]"
  };

  public void Draw(Frame frame)
  {
    var batch = frame.CreateBatch();
    batch.SmoothRect(new Rectangle(33, 0, 67, 41));
    batch.Text(65, 0, "Logs");

    var logs = Logger.TakeLogs(39).ToList();

    for (int i = 0; i < logs.Count; i++)
    {
      var log = logs[i];
      var message = $"{log.Time.ToString("HH.mm.ss")} {_prefByLevel[log.Level]} {log.Message}";
      if (log.Level == LogLevel.Error)
      {
        var allText = SmartSplitStackTrace(message);
        foreach (var logPart in allText)
        {
          batch.Text(34, i + 1, logPart, _colorByLevel[log.Level]);
          i++;
        }

        continue;
      }

      batch.Text(34, i + 1, Truncate(message), _colorByLevel[log.Level]);
    }
  }

  public static string Truncate(string input, int maxLength = 65)
  {
    const string ellipsis = "...";
    if (input.Length <= maxLength)
      return input;
    return input.Substring(0, maxLength - ellipsis.Length) + ellipsis;
  }

  public static List<string> Fit(string input, int maxLength = 65)
  {
    var result = new List<string>();

    for (int i = 0; i < input.Length; i += maxLength)
    {
      int length = Math.Min(maxLength, input.Length - i);
      result.Add(input.Substring(i, length));
    }

    return result;
  }

  public static List<string> SmartSplitStackTrace(string input, int maxLineLength = 65)
  {
    var result = new List<string>();
    if (string.IsNullOrEmpty(input) || maxLineLength <= 0)
      return result;

    var parts = Regex.Split(input, @"(?=\bat\b|\bin\b)");

    foreach (var part in parts)
    {
      string trimmed = part.TrimStart();

      if (trimmed.Length <= maxLineLength)
      {
        result.Add(trimmed);
      }
      else
      {
        for (int i = 0; i < trimmed.Length; i += maxLineLength)
        {
          int length = Math.Min(maxLineLength, trimmed.Length - i);
          result.Add(trimmed.Substring(i, length));
        }
      }
    }

    return result;
  }
}
