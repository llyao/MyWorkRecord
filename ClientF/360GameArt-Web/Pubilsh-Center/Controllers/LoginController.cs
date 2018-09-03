using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI.WebControls;
using ClientApp.DB;
using ClientCore.DB;
using ClientCore.Logging;
using ClientCore.Response;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Index()
        {
            string ReturnUrl = Request.QueryString["url"];
            if (Session["user"] != null)
            {
                if (ReturnUrl != null && Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/") && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Url = ReturnUrl;
            return View();
		}

		// GET: Register
		public ActionResult Register()
	    {
		    return View();
	    }

		[HttpPost]
        public ActionResult Login(string EmailAddress, string Password, string ReturnUrl)
        {
	        ResponseData responseData = new ResponseData(true);

			if (!string.IsNullOrEmpty(EmailAddress) && !string.IsNullOrEmpty(Password))
            {
                UserTableData userTableData = DbManager.Instance.GetTableData<UserTableData>("select * from user where account = '" + EmailAddress + "'");
                if (userTableData == null || userTableData.Password != Password)
                {
	                responseData.Success = false;
	                responseData.Message = "用户名或者密码不正确";
                }
                else
                {
					Session["user"] = userTableData;

					Logger.Trace(EmailAddress + " 登录了");
				}

            }

	        return responseData.Json();
        }



        public ActionResult LogOff()
        {
            Session["user"] = null;
            return View("../Login/Index");
        }


	}
}