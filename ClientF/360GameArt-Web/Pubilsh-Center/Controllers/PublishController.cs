using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientApp.DB;
using ClientApp.Manager;
using ClientCore.DB;
using ClientCore.Utils;

namespace WebApp.Controllers
{
    public class PublishController : ProjectCenterBaseController
    {
	    private static Process RunningProcess;
		
		public ActionResult Index()
        {
			SetProjectVersionData();
	        string cmd = Request["cmd"];
	        List<BuildVersionTableData> buildVersionTableDatas = DbManager.Instance.GetTableDatas<BuildVersionTableData>("select * from allpackhistory");
	        buildVersionTableDatas.Reverse();
			ViewData["buildVersionTableDatas"] = buildVersionTableDatas;
			ViewData["cmd"] = cmd;

			return View();
        }

		public ActionResult History()
        {
			SetProjectVersionData();
            return View();
        }

	    public void BuildAll()
	    {
		    var user = (Session["user"] as UserTableData);
		    string nick = user != null ? user.Nick : "";
			PublishManager.Instance.RunBat("E:/ClientFramework/PlatformBuilder/BuildAll.bat", GetCurrentUser());
	    }

	    public ActionResult IsBuilding()
	    {
		    return Content(PublishManager.Instance.IsRunning().ToString());
	    }

	    public ActionResult GetBuildingLog()
	    {
			return Content(PublishManager.Instance.GetLog());
		}

	    public ActionResult GetBuildedLog(string cmd, int versionID)
	    {
		    if(cmd == "allpack")
		    {
			    BuildVersionTableData versionTableData = DbManager.Instance.GetTableData<BuildVersionTableData>("select * from allpackhistory where id = " + versionID);
			    if(versionTableData != null)
			    {
				    string result = "启动者：" + versionTableData.Author + "\n";
				    result += "启动时间：" + versionTableData.StartTimeFormat + "\n";
					return Content(result + ZipHelper.DecompressString(versionTableData.Log));
				}
		    }
		    return Content("");
	    }
	}
}