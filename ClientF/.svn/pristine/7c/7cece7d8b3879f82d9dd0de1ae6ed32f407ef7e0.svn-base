using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ClientCore.Response
{
	public class ResponseData:ResponseJson
	{
		protected bool success;
		protected string message;

		public ResponseData():this(false, String.Empty)
		{
		}

		public ResponseData(bool success) :this(success, String.Empty)
		{
		}

		public ResponseData(bool success, string message)
		{
			this.success = success;
			this.message = message;
		}

		public string Message
		{
			get => message;
			set => message = value;
		}

		public bool Success
		{
			get => success;
			set => success = value;
		}
	}
}