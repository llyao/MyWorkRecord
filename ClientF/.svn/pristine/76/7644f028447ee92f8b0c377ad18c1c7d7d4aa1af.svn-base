using System.Collections.Generic;

namespace ClientCore.Logging
{
	public class MemoryLogAppender : ILogAppender
	{
		private readonly List<LogEvent> logs = new List<LogEvent>();

		public void Log(LogEvent line)
		{
			logs.Add(line);
		}

		public LogEvent[] CopyCurrentMemoryLog()
		{
			return logs.ToArray();
		}

		public void Clear()
		{
			logs.Clear();
		}

		public MemoryLogAppender Clone()
		{
			var clone = new MemoryLogAppender();
			clone.logs.AddRange(logs);
			return clone;
		}

		public IEnumerable<LogEvent> ReverseFilterLogs(LogLevel filter, int maxLine)
		{
			var logLines = 0;
			for (var index = logs.Count - 1; index >= 0 && logLines < maxLine; --index)
			{
				var log = logs[index];
				if ((int)(log.Level & filter) != 0)
				{
					yield return log;
					++logLines;
				}
			}
		}

		// O(n)
		public int Count(LogLevel filter)
		{
			var total = 0;
			for (var index = logs.Count - 1; index >= 0; --index)
			{
				var log = logs[index];
				if ((int)(log.Level & filter) != 0)
				{
					++total;
				}
			}
			return total;
		}
	}
}