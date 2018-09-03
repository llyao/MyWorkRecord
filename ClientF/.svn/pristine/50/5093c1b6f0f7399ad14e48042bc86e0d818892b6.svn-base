using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ClientCore.Config;
using ClientCore.Platform;
using ClientCore.Utils;

namespace ClientCore.Logging
{
	//TODO 需要全部调整
	/// <summary>
	/// FileLog 完成如下功能：
	/// 
	/// 1. 将日志存入两个文件。一个是经过过滤的标准日志文件，另一个是完整的日志文件。
	/// 2. 他会将过去的日志备份为zip文件，最多备份最近的 MaxBackupGeneration 份。
	/// 3. 如果当前日志文件被占用，他会尝试换一个名字。默认是 log_0，他会尝试 log_1，log_2，最多尝试 MaxLogFailIndex 次。
	/// 
	/// 注意： 日志有长度上限，目前为50M。之前有一个死循环导致产生了5G的日志。
	/// </summary>
	public class FileLogAppender : ILogAppender
	{
		public const int MAX_LOG_LENGTH = 50 * 1024 * 1024; // 日志长度上限，设为50M。之前有一个死循环导致产生了5G的日志

		private const int MAX_LOG_FAIL_INDEX = 10;
		private const int MAX_BACKUP_GENERATION = 5;
		private const int REG_TAG = 1;
		private const int REG_H = 2;
		private const int REG_MM = 3;
		private const int REG_SS = 4;
		private const int REG_FFFF = 5;
		private const int REG_THREAD = 6;
		private const int REG_MSG = 7;

		private static readonly Regex regLog = new Regex(@"(\w+) (\d+):(\d+):(\d+).(\d+)\[(\d+)\] (.*)");

		// 标准日志，受限于LogMan.LogLevel。过往存档，以及崩溃日志均为该日志。
		private string logFile;

		private StreamWriter streamLog;

		// 完整日志。仅保留一次，控制台主动发送日志为该日志。
		private string verboseFile;

		private StreamWriter streamVerbose;

		public string LogFile
		{
			get
			{
				return logFile;
			}
		}

		public string VerboseLogFile
		{
			get
			{
				return verboseFile;
			}
		}

		public bool IsAttched()
		{
			return streamLog != null && streamVerbose != null;
		}

		public FileLogAppender()
		{
			Open();
		}

		public void Log(LogEvent ev)
		{
			if (streamLog != null && ev.Level >= LogLevel.INFO)
				LogToStream(streamLog, ev);

			if (streamVerbose != null)
				LogToStream(streamVerbose, ev);
		}

		public bool Open()
		{
			if (IsAttched())
			{
				return false;
			}

			string log = PathUtil.GetPath(ConfigManager.Instance.GetSetting("LOG")) ;
			for (int index = 0; index < MAX_LOG_FAIL_INDEX; ++index)
			{
				
				if (Open(string.Format(log, index)))
				{
					return true;
				}
			}
			return false;
		}

		public void Close()
		{
			streamLog.Close();
			streamLog = null;
			logFile = null;

			if (streamVerbose != null)
			{
				streamVerbose.Close();
				streamVerbose = null;
			}
			verboseFile = null;
		}

		private bool Open(string file)
		{
			if (file == null) return false;

			try
			{
				PlatformManager.Instance.CreateParentDirectoryIfNeed(file);
				AssetOpenable(file);
				BackupPrevLogs(file, MAX_BACKUP_GENERATION);

				PlatformManager.Instance.RemoveReadonlyAttribute(file);
				var fs = File.Open(file, FileMode.Create, FileAccess.Write, FileShare.Read);
				logFile = file;
				streamLog = new StreamWriter(fs);
				streamLog.NewLine = "\r\n";

				verboseFile = ReplaceExtension(file, "_verbose.txt");
				PlatformManager.Instance.RemoveReadonlyAttribute(verboseFile);
				if (true)
				{
					var fs2 = File.Open(verboseFile, FileMode.Create, FileAccess.Write, FileShare.Read);
					streamVerbose = new StreamWriter(fs2);
				}
				//				else
				//				{
				//					File.Delete(verboseFile);
				//					verboseFile = null;
				//				}
			}
			catch (Exception e)
			{
				if (streamLog != null)
				{
					streamLog.Dispose();
					streamLog = null;
					logFile = null;
				}
				if (streamVerbose != null)
				{
					streamVerbose.Dispose();
					streamVerbose = null;
					verboseFile = null;
				}

				Logger.Trace("Try open file {0} for log failed, {1}", file, e.Message);
				return false;
			}

			if (verboseFile != null)
				Logger.Trace("Current log is {0} and {1}", file, verboseFile);
			else
				Logger.Trace("Current log is {0}", file);

			MemoryLogAppender memoryLogAppender = Logger.GetAppender<MemoryLogAppender>();
			if (memoryLogAppender != null)
			{
				foreach (LogEvent log in memoryLogAppender.CopyCurrentMemoryLog())
				{
					if (log.Level >= LogLevel.INFO)
						LogToStream(streamLog, log, flush: false);
					LogToStream(streamVerbose, log, flush: false);
				}
			}
			if (streamLog != null) streamLog.Flush();
			if (streamVerbose != null) streamVerbose.Flush();
			return true;
		}

		public List<LogEvent> ReadMostVerboseLogFile()
		{
			var file = verboseFile ?? logFile;
			if (file == null)
				return null;

			List<LogEvent> logs = new List<LogEvent>();
			using (var fs = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (var f = new StreamReader(fs))
				{
					string readLine;
					while ((readLine = f.ReadLine()) != null)
					{
						LogEvent newlog;
						if (ParseLogEvent(readLine, out newlog) || logs.Count == 0)
						{
							logs.Add(newlog);
						}
						else
						{
							LogEvent line = logs[logs.Count - 1];
							line.Message += "\r\n" + readLine;
							logs[logs.Count - 1] = line;
						}
					}
				}
			}
			return logs;
		}

		public void Clear()
		{
			if (streamLog != null)
			{
				streamLog.BaseStream.SetLength(0);
				streamLog.Flush();
			}
		}

		private void OnLog(LogEvent log)
		{
		}

		private void LogToStream(StreamWriter stream, LogEvent log, bool flush = true)
		{
			if (stream == null || stream.BaseStream.Position >= MAX_LOG_LENGTH)
				return;

			stream.WriteLine(log.ToString());

			if (stream.BaseStream.Position >= MAX_LOG_LENGTH)
			{
				stream.Write("\r\n----------------\r\n");
				stream.Write("Log file size too long (> {0}B), ignore following logs", MAX_LOG_LENGTH);
				stream.Write("\r\n----------------\r\n");
			}
			if (flush)
				stream.Flush();
		}

		//TODO 这里重新修改
		private bool ParseLogEvent(string fileline, out LogEvent line)
		{
			var m = regLog.Match(fileline);
			try
			{
				if (m.Success)
				{
					line = new LogEvent
					{
						Message = m.Groups[REG_MSG].Value,
						Level = (LogLevel)Enum.Parse(typeof(LogLevel), m.Groups[REG_TAG].Value),
						Thread = int.Parse(m.Groups[REG_THREAD].Value),
						DateTime = new DateTime(
							int.Parse(m.Groups[REG_H].Value) * TimeSpan.TicksPerHour +
							int.Parse(m.Groups[REG_MM].Value) * TimeSpan.TicksPerMinute +
							int.Parse(m.Groups[REG_SS].Value) * TimeSpan.TicksPerSecond +
							(long)(float.Parse("0." + m.Groups[REG_FFFF].Value) * TimeSpan.TicksPerSecond)
						)
					};
					return true;
				}
			}
			catch (Exception)
			{
				// ignored
			}

			line = new LogEvent
			{
				Message = fileline,
				Level = LogLevel.WARN,
				Thread = 0,
				DateTime = new DateTime()
			};
			return false;
		}

		private void BackupPrevLogs(string file, int max)
		{
			try
			{
				string finalLog = FormatPathOfGeneration(file, max);
				if (File.Exists(finalLog))
				{
					File.Delete(finalLog);
				}

				for (int i = max - 1; i >= 1; --i)
				{
					string src = FormatPathOfGeneration(file, i);
					if (File.Exists(src))
					{
						File.Move(src, FormatPathOfGeneration(file, i + 1));
					}
				}

				if (File.Exists(file))
				{
					using (var zip = new Zipper())
					{
						zip.Append("log.txt", file);
						File.WriteAllBytes(FormatPathOfGeneration(file, 1), zip.Finish());
					}
				}
			}
			catch (Exception e)
			{
				Logger.Warn("backup prev log failed: {0}", e);
			}
		}

		private void AssetOpenable(string file)
		{
			FileStream f = null;
			try
			{
				if (File.Exists(file))
					f = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			}
			finally
			{
				if (f != null) f.Dispose();
			}
		}

		private string FormatPathOfGeneration(string file, int generation)
		{
			if (generation > 0)
				return ReplaceExtension(file, string.Format(".bak{0}.zip", generation));
			else
				return file;
		}

		private string ReplaceExtension(string file, string ext)
		{
			int index = file.LastIndexOf(".", StringComparison.Ordinal);
			if (index > 0)
			{
				return file.Substring(0, index) + ext;
			}
			return file;
		}
	}
}