using System;
using System.Collections.Generic;
using System.Web;
using ClientApp.Constants;
using ClientApp.DB;
using ClientCore.DB;

namespace WebApp.Controllers
{
	public class ProjectCenterBaseController:AuthorityController
	{
		protected void SetProjectVersionData()
		{
			List<ProjectVersionData> versionDatas = DbManager.Instance.GetTableDatas<ProjectVersionData>("select * from versions");
			ViewData["versionDatas"] = versionDatas;

			ViewData["CurrentProjectVersion"] = GetCurrentSelectedVersionID();
		}

		public int GetCurrentSelectedVersionID()
		{
		    List<ProjectVersionData> versionDatas = DbManager.Instance.GetTableDatas<ProjectVersionData>("select * from versions");
		    if (versionDatas == null || versionDatas.Count == 0)
		    {
		        return 0;
		    }
            if (Request.Cookies[CookiesConst.PROJECT_KEY] != null)
			{
			    int versionID = int.Parse(Request.Cookies[CookiesConst.PROJECT_KEY]["CurrentProjectVersion"]);
			    for (int i = 0; i < versionDatas.Count; i++)
			    {
			        if (versionDatas[i].Id == versionID)
			        {
			            return versionID;
			        }
			    }
		    }
		    AddProjectVersion(versionDatas[0].Id.ToString());
		    return versionDatas[0].Id;
        }

	    public void AddProjectVersion()
	    {
	        string versionValue = Request["versionValue"];
	        AddProjectVersion(versionValue);

	    }


	    private void AddProjectVersion(string versionValue)
	    {
	        HttpCookie cookie = Request.Cookies[CookiesConst.PROJECT_KEY];
	        if (cookie == null)
	        {
	            //创建Cookie 并命名
	            cookie = new HttpCookie(CookiesConst.PROJECT_KEY);
	        }

	        //Cookie为一年有效期
	        cookie.Expires = DateTime.Today.AddDays(360);
	        //设置Cookie对应键值 
	        cookie.Values["CurrentProjectVersion"] = versionValue;
	        Response.Cookies.Add(cookie);
	    }

    }
}