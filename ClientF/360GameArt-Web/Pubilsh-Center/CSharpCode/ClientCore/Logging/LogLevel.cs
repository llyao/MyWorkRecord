using System;

namespace ClientCore.Logging
{
	[Flags]
	public enum LogLevel
	{
		FATAL = 1,
		ERROR = 2,
		WARN = 4,
		INFO = 8,
		DEBUG = 16,
		TRACE = 32,
	}
}