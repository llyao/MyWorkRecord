using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientApp.DB;
using ClientCore.DB;

namespace WebApp.Controllers
{
    public class AuthorityController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var user = GetCurrentUser();
            if (user == null || user.Authority < GetNeedAuthority())
            {
                //重定向至登录页面
                filterContext.Result = RedirectToAction("LogOff", "Login", new { url = Request.RawUrl });
            }
        }

	    protected UserTableData GetCurrentUser()
	    {
		    return Session["user"] as UserTableData;
	    }


	    protected virtual int GetNeedAuthority()
	    {
		    return 1;
	    }

	}
}