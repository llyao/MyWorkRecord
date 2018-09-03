namespace ClientCore.Logging
{
	public interface ILogAppender
	{
		void Log(LogEvent ev);
	}
}