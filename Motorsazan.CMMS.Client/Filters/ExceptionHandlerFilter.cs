using Motorsazan.CMMS.Client.Utilities;
using Motorsazan.CMMS.Shared.Models.Base;
using System;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Filters
{
    public class ExceptionHandlerFilter: FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            var baseException = filterContext.Exception.GetBaseException();

            if(baseException is LoginException)
            {
                return;
            }

            string error;
            var code = (int)System.Net.HttpStatusCode.InternalServerError;

            Log.Error(baseException.ToString(), DateTime.Now.Ticks);

            if(baseException is AlertException)
            {
                error = baseException.Message;
                code = 501;
            }
            else
            {
                error = "امکان اجرای درخواست فراهم نشد لطفا با تیم توسعه تماس حاصل فرمایید";
            }

            filterContext.HttpContext.Response.StatusCode = code;
            filterContext.Result = new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Error = error
                }
            };
        }
    }
}