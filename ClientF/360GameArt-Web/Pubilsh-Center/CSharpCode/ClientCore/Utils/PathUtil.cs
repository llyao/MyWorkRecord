namespace ClientCore.Utils
{
	public static class PathUtil
	{
		public static string GetRoot()
		{
			return System.AppDomain.CurrentDomain.BaseDirectory;
		}


		public static string GetPath(string relativeURL)
		{
			return GetRoot() + relativeURL;
		}
	}
}