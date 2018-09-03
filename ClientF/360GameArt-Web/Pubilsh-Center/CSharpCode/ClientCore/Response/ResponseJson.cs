using System.Web.Mvc;

namespace ClientCore.Response
{
	public class ResponseJson:IResonseJson
	{
		public JsonResult Json()
		{
			return new JsonResult()
			{
				Data = this,
				ContentType = null,
				ContentEncoding = null,
				JsonRequestBehavior = JsonRequestBehavior.AllowGet
			};
		}
	}
}