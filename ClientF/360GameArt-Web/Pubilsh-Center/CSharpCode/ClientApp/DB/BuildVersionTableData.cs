using System.Data;
using ClientCore.DB;
using ClientCore.Utils;

namespace ClientApp.DB
{
	public class BuildVersionTableData:BaseTableData
	{
		public int ID;
		public string Author;
		public long StartTime;
		public long EndTime;
		public string Log;
		public int ExitCode;

		public string StartTimeFormat;

		public override void ReadFrom(DataRow collection)
		{
			base.ReadFrom(collection);
			ID = (int)(collection[0]);
			Author = collection[1].ToString();
			StartTime = (long)(collection[2]);
			EndTime = (long)(collection[3]);
			Log = collection[4].ToString();
			ExitCode = (int)(collection[5]);

			StartTimeFormat = TimeUtils.GetDate(StartTime * 1000).ToString("yyyy-MM-dd HH:mm:ss");
		}
	}
}