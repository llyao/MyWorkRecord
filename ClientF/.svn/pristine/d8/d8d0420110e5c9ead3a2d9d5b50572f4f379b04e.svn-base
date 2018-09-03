using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace ClientCore.Logging
{
	public static class Logger
	{
		public static event Action<LogEvent> LogSpi = param0 =>
		{
		};

		private static readonly Dictionary<Type, ILogAppender> appenders = new Dictionary<Type, ILogAppender>();
		private static readonly List<ILogAppender> tempAppenders = new List<ILogAppender>();

		[ThreadStatic]
		private static StringBuilder builder;

		[ThreadStatic]
		private static bool logging;

		static Logger()
		{
			Enable = true;
		}

		public static bool Enable
		{
			get;
			set;
		}

		public static void AddAppender<TA>(TA appender) where TA : class, ILogAppender
		{
			lock(appenders)
			{
				appenders[appender.GetType()] = appender;
			}
		}

		public static TA GetAppender<TA>() where TA : class, ILogAppender
		{
			var key = typeof(TA);
			lock(appenders)
			{
				if(appenders.ContainsKey(key))
				{
					return appenders[key] as TA;
				}
			}
			return default(TA);
		}

		public static void RemoveAppender<TA>() where TA : class, ILogAppender
		{
			var key = typeof(TA);
			lock(appenders)
			{
				if(!appenders.ContainsKey(key))
				{
					return;
				}
				appenders.Remove(key);
			}
		}

		public static void Fatal(string msg)
		{
			Log(LogLevel.FATAL, typeof(Logger), msg, null);
		}

		public static void Fatal(string msg, Exception e)
		{
			Log(LogLevel.FATAL, typeof(Logger), msg, e);
		}

		public static void Fatal(string msg, params object[] args)
		{
			Log(LogLevel.FATAL, typeof(Logger), string.Format(msg, args), null);
		}

		public static void Error(string msg)
		{
			Log(LogLevel.ERROR, typeof(Logger), msg, null);
		}

		public static void Error(string msg, Exception e)
		{
			Log(LogLevel.ERROR, typeof(Logger), msg, e);
		}

		public static void Error(string msg, params object[] args)
		{
			Log(LogLevel.ERROR, typeof(Logger), string.Format(msg, args), null);
		}

		public static void Warn(string msg)
		{
			Log(LogLevel.WARN, typeof(Logger), msg, null);
		}

		public static void Warn(string msg, Exception e)
		{
			Log(LogLevel.WARN, typeof(Logger), msg, e);
		}

		public static void Warn(string msg, params object[] args)
		{
			Log(LogLevel.WARN, typeof(Logger), string.Format(msg, args), null);
		}

		public static void Info(string msg)
		{
			Log(LogLevel.INFO, typeof(Logger), msg, null);
		}

		public static void Info(string msg, Exception e)
		{
			Log(LogLevel.INFO, typeof(Logger), msg, e);
		}

		public static void Info(string msg, params object[] args)
		{
			Log(LogLevel.INFO, typeof(Logger), string.Format(msg, args), null);
		}

		public static void Debug(string msg)
		{
			Log(LogLevel.DEBUG, typeof(Logger), msg, null);
		}

		public static void Debug(string msg, Exception e)
		{
			Log(LogLevel.DEBUG, typeof(Logger), msg, e);
		}

		public static void Debug(string msg, params object[] args)
		{
			Log(LogLevel.DEBUG, typeof(Logger), string.Format(msg, args), null);
		}

		public static void Trace(string msg)
		{
			Log(LogLevel.TRACE, typeof(Logger), msg, null);
		}

		public static void Trace(string msg, Exception e)
		{
			Log(LogLevel.TRACE, typeof(Logger), msg, e);
		}

		public static void Trace(string msg, params object[] args)
		{
			Log(LogLevel.TRACE, typeof(Logger), string.Format(msg, args), null);
		}

		public static void Fatal(Type type, string msg)
		{
			Log(LogLevel.FATAL, type, msg, null);
		}

		public static void Fatal(Type type, string msg, Exception e)
		{
			Log(LogLevel.FATAL, type, msg, e);
		}

		public static void Fatal(Type type, string msg, params object[] args)
		{
			Log(LogLevel.FATAL, type, string.Format(msg, args), null);
		}

		public static void Error(Type type, string msg)
		{
			Log(LogLevel.ERROR, type, msg, null);
		}

		public static void Error(Type type, string msg, Exception e)
		{
			Log(LogLevel.ERROR, type, msg, e);
		}

		public static void Error(Type type, string msg, params object[] args)
		{
			Log(LogLevel.ERROR, type, string.Format(msg, args), null);
		}

		public static void Warn(Type type, string msg)
		{
			Log(LogLevel.WARN, type, msg, null);
		}

		public static void Warn(Type type, string msg, Exception e)
		{
			Log(LogLevel.WARN, type, msg, e);
		}

		public static void Warn(Type type, string msg, params object[] args)
		{
			Log(LogLevel.WARN, type, string.Format(msg, args), null);
		}

		public static void Info(Type type, string msg)
		{
			Log(LogLevel.INFO, type, msg, null);
		}

		public static void Info(Type type, string msg, Exception e)
		{
			Log(LogLevel.INFO, type, msg, e);
		}

		public static void Info(Type type, string msg, params object[] args)
		{
			Log(LogLevel.INFO, type, string.Format(msg, args), null);
		}

		public static void Debug(Type type, string msg)
		{
			Log(LogLevel.DEBUG, type, msg, null);
		}

		public static void Debug(Type type, string msg, Exception e)
		{
			Log(LogLevel.DEBUG, type, msg, e);
		}

		public static void Debug(Type type, string msg, params object[] args)
		{
			Log(LogLevel.DEBUG, type, string.Format(msg, args), null);
		}

		public static void Trace(Type type, string msg)
		{
			Log(LogLevel.TRACE, type, msg, null);
		}

		public static void Trace(Type type, string msg, Exception e)
		{
			Log(LogLevel.TRACE, type, msg, e);
		}

		public static void Trace(Type type, string msg, params object[] args)
		{
			Log(LogLevel.TRACE, type, string.Format(msg, args), null);
		}

		public static string GetStack(int skipFrames = 1)
		{
			return GetStack(new StackTrace(skipFrames, true));
		}

		public static string GetStack(Exception e)
		{
			return GetStack(new StackTrace(e, true));
		}

		private static string GetStack(StackTrace st)
		{
			if(st == null)
			{
				return "";
			}
			try
			{
				var localStringBuilder = GetThreadLocalStringBuilder(512);
				for(var index = 0; index < st.FrameCount; ++index)
				{
					if((uint)index > 0U)
					{
						localStringBuilder.AppendFormat("\n");
					}
					var frame = st.GetFrame(index);
					if(frame != null)
					{
						var method = frame.GetMethod();
						if(method == null)
						{
							localStringBuilder.Append(" unknown method");
						}
						else
						{
							localStringBuilder.Append(" ");
							if(method.ReflectedType != null)
							{
								localStringBuilder.AppendFormat("{0}:{1}", method.ReflectedType.Name, method.Name);
							}
							else
							{
								localStringBuilder.Append(method.Name);
							}
							localStringBuilder.Append("(");
							foreach(var parameter in method.GetParameters())
							{
								if((uint)parameter.Position > 0U)
								{
									localStringBuilder.Append(", ");
								}
								if(parameter.IsOut)
								{
									localStringBuilder.Append("out ");
								}
								localStringBuilder.Append(parameter.ParameterType.Name);
							}
							localStringBuilder.Append(")");
						}
						var fileName = frame.GetFileName();
						if(fileName != null)
						{
							localStringBuilder.AppendFormat(" in {0}:{1}", fileName, frame.GetFileLineNumber());
						}
					}
				}
				return localStringBuilder.ToString();
			}
			catch(Exception ex)
			{
				return "";
			}
		}

		private static void Log(LogLevel level, Type type, string msg, Exception e)
		{
			if(!Enable)
			{
				return;
			}
			if(logging)
			{
				return;
			}
			try
			{
				logging = true;
				var ev = new LogEvent
				{
					DateTime = DateTime.Now,
					Thread = Thread.CurrentThread.ManagedThreadId,
					Level = level,
					Type = type,
					Message = msg,
					Exception = e
				};
				// ISSUE: reference to a compiler-generated field
				LogSpi(ev);
				lock(appenders)
				{
					tempAppenders.AddRange(appenders.Values);
				}
				foreach(var tempAppender in tempAppenders)
				{
					tempAppender.Log(ev);
				}
				tempAppenders.Clear();
			}
			catch(Exception ex)
			{
			}
			finally
			{
				logging = false;
			}
		}

		private static StringBuilder GetThreadLocalStringBuilder(int capacity)
		{
			if(builder == null)
			{
				builder = new StringBuilder(capacity);
			}
			else
			{
				builder.Length = 0;
			}
			return builder;
		}
	}
}