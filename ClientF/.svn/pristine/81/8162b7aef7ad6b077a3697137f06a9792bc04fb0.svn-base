using System;

namespace ClientCore.Utils
{
	public static class TimeUtils
	{
		/// <summary>
		/// 获取1970-01-01至dateTime的毫秒数
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static long GetTimeMillisecond(DateTime dateTime)
		{
			DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return (dateTime.Ticks - dt.Ticks) / 10000;
		}

		/// <summary>
		/// 获取1970-01-01至dateTime的秒数
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static long GetTimeSecond(DateTime dateTime)
		{
			return (long)(GetTimeMillisecond(dateTime) * 0.001f);
		}

		/// <summary>
		/// 根据时间戳millisecond（单位毫秒）计算日期
		/// </summary>
		/// <param name="millisecond">毫秒数</param>
		/// <returns></returns>
		public static DateTime GetDate(long millisecond)
		{
			DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			long tt = dt.Ticks + millisecond * 10000;
			return new DateTime(tt);
		}
	}
}