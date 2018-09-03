using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientApp.DB;
using ClientCore.DB;
using ClientCore.Response;

namespace WebApp.Controllers
{
    public class AdminController : AuthorityController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: AddUser
        public ActionResult AddUser()
        {
            return View();
        }

		// GET: AddVersion
		public ActionResult AddVersion()
        {
            return View();
		}

		// GET: UserList
		public ActionResult UserList()
		{
			List<UserTableData> users = DbManager.Instance.GetTableDatas<UserTableData>("select * from user");
			ViewData["userList"] = users;
			return View();
		}

	    // GET: UserList
	    public ActionResult ModifyUser()
	    {
		    UserTableData user = DbManager.Instance.GetTableData<UserTableData>("select * from user where id = " + Request["userID"]);
		    if(user == null)
		    {
				//AlertManager.Alert("用户不存在！");
				return Content("用户不存在！");
			}
		    ViewData["user"] = user;
		    return View();
	    }

		public void AddNewUser()
        {
            string accountText = Request["accountText"];
            string passworldText = Request["passworldText"];
            string authority = (Request["authority"]);
            DbManager.Instance.ExecuteNonQuery("insert into user(account,password,authority) values('" + accountText +
                                      "','" + passworldText + "','" + authority + "')", GetCurrentUser());
		}



	    [HttpPost]
		public ActionResult EditUser(int userID, int authority)
	    {
			UserTableData user = DbManager.Instance.GetTableData<UserTableData>("select * from user where id = " + userID);
		    ResponseData responseData = new ResponseData(true);
			if (user == null)
		    {
				responseData.Success = false;
			    responseData.Message = "用户不存在";
			}
			else
			{
				DbManager.Instance.ExecuteNonQuery("UPDATE user SET authority = '" + authority + "' WHERE id = '" + userID + "'", GetCurrentUser());
			}
		    return responseData.Json();
	    }

		public void AddNewVersion()
	    {
			string NameText = Request["NameText"];
		    string designerSvn = Request["designerSvn"];
		    string clientSvn = (Request["clientSvn"]);
		    string artSvn = (Request["artSvn"]);
		    string contactInfo = (Request["contactInfo"]);
		    DbManager.Instance.ExecuteNonQuery("insert into versions(NAME,designerSvn,clientSvn,artSvn,contactInfo) values('" + NameText +
		                              "','" + designerSvn + "','" + clientSvn + "','" + artSvn + "','" + contactInfo + "')", GetCurrentUser());
	    }


		override protected  int GetNeedAuthority()
	    {
		    return 100;
	    }
	}
}