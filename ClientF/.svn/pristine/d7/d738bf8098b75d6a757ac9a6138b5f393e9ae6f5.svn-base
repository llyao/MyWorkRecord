using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ClientApp.DB;
using ClientApp.XmlData;
using ClientCore.Config;
using ClientCore.DB;
using ClientCore.Response;
using ClientCore.Utils;

namespace WebApp.Controllers
{
    public class ProjectCenterController : ProjectCenterBaseController
	{
        public ActionResult Index()
        {
	        SetProjectVersionData();
			return View();
        }

        public ActionResult Setting()
        {
	        string settingURL = string.Format(ConfigManager.Instance.GetSetting("VERSION_SETTING"),
		        GetCurrentSelectedVersionID());
	        XmlVersionSettingData settingData = ConfigManager.Instance.GetXML<XmlVersionSettingData>(PathUtil.GetPath(settingURL));

	        if(settingData == null)
	        {
		        return Content("项目配置文件不存在，请联系管理员！");
	        }

	        SetProjectVersionData();
			ViewData["settingData"] = settingData;
			return View();
        }

		public ActionResult Protocol()
		{
			SetProjectVersionData();
			return View();
		}

		public ActionResult SaveSetting()
		{
			string settingURL = string.Format(ConfigManager.Instance.GetSetting("VERSION_SETTING"),
				GetCurrentSelectedVersionID());
			var jsonString = new StreamReader(Request.InputStream).ReadToEnd();
			JavaScriptSerializer js = new JavaScriptSerializer();
			XmlVersionSettingData data = js.Deserialize<XmlVersionSettingData>(jsonString);
			ConfigManager.Instance.UpdateXML<XmlVersionSettingData>(PathUtil.GetPath(settingURL), data);

			ResponseData responseData = new ResponseData(true);
			try
			{
				data.Export().Save(PathUtil.GetPath(settingURL));
			}
			catch(Exception e)
			{
				responseData.Message = e.ToString();
				responseData.Success = false;
			}


			return responseData.Json();
		}

		public ActionResult GetProtoClassify()
		{
			ResponseData responseData = new ResponseData(true);
			return responseData.Json();
		}

		public ActionResult Introduction()
        {
            SetProjectVersionData();

            int versionID = GetCurrentSelectedVersionID();
            if (versionID > 0)
            {
                ProjectVersionData versionData = DbManager.Instance.GetTableData<ProjectVersionData>("select * from versions where id = '" + versionID + "'");
                ViewData["versionData"] = versionData;
            }

			return View();
        }

		public string Getstr()
		{
			return "在Controller里返回点击下载的信息";
		}

	}
}