using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ClientCore.Core;
using ClientCore.DB;
using ClientCore.Utils;

namespace ClientApp.Manager
{
	public class PublishManager:Singleton<PublishManager>
	{
		public bool IsRunning()
		{
			return isRunning;
		}

		public string GetLog()
		{
			return Log;
		}

		public Process RunningProcess;
		public DateTime StarTime;
		public string Author;
		public int ExitCode;
		private string Log;
		private bool isRunning;
		private string batPath;
		private UserTableData user;

		public void RunBat(string batPath, UserTableData user)
		{
			this.batPath = batPath;
			this.user = user;
			ThreadStart threadStart = new ThreadStart(Start);
			Thread thread = new Thread(threadStart);
			thread.Start();
		}

		public void Start()
		{
			StarTime = System.DateTime.Now;
			Log = "";
			isRunning = true;
			RunningProcess = new Process();
			FileInfo file = new FileInfo(batPath);
			RunningProcess.StartInfo.WorkingDirectory = file.Directory.FullName;
			//RunningProcess.StartInfo.Arguments = "Hello World";
			RunningProcess.StartInfo.FileName = batPath;
			RunningProcess.StartInfo.CreateNoWindow = true;
			RunningProcess.StartInfo.UseShellExecute = false;
			RunningProcess.StartInfo.RedirectStandardInput = true;
			RunningProcess.StartInfo.RedirectStandardOutput = true;
			RunningProcess.StartInfo.RedirectStandardError = true;
			RunningProcess.EnableRaisingEvents = true;
			RunningProcess.OutputDataReceived += RecieveOutput;
			RunningProcess.ErrorDataReceived += RecieveError;
			RunningProcess.Start();
			RunningProcess.BeginOutputReadLine();
			RunningProcess.BeginErrorReadLine();
			RunningProcess.WaitForExit();
			isRunning = false;
			ExitCode = RunningProcess.ExitCode;

			string compressStr = ZipHelper.CompressString(Log);

			DbManager.Instance.ExecuteNonQuery("insert into allpackhistory(author,startTime,endTime,log,ExitCode) values('" + user.Nick +
			                          "','" + TimeUtils.GetTimeSecond(StarTime)  + "','" + TimeUtils.GetTimeSecond(System.DateTime.Now) + "','" + compressStr + "','" + ExitCode + "')", user);
		}

		private void RecieveOutput(object sender, DataReceivedEventArgs e)
		{
			Log += (e.Data) + "\n";
		}

		private void RecieveError(object sender, DataReceivedEventArgs e)
		{
			Log += "<p style='color:#ff0000'>" + (e.Data) + "</p>\n";
		}
	}
}