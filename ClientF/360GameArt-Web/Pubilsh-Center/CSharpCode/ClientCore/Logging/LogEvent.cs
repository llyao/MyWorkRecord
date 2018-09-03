using System;

namespace ClientCore.Logging
{
	public struct LogEvent
	{
		public DateTime DateTime;
		public int Thread;
		public LogLevel Level;
		public Type Type;
		public string Message;
		public Exception Exception;

		public override string ToString()
		{
			return string.Format("{0:yyyy-MM-dd HH:mm:ss.fff} {1,-5} [{2,3:000}] {3,10} {4} {5} {6}", (object)DateTime,
				(object)Level, (object)Thread, (object)Type, (object)Message, (object)(Exception == null ? "" : Exception.Message),
				(object)(Exception == null ? "" : Exception.StackTrace));
		}
	}
}