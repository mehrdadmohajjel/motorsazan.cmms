using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Models.Input.DailyWorkOrderPrint;
using Motorsazan.CMMS.Shared.Models.Output.DailyWorkOrderPrint;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class DailyWorkOrderPrintController: Controller
    {
        // GET: DailyWorkOrderPrint
        public ActionResult Index() => View();

        public ActionResult FilterFormGetWorkOrderAllStatusList()
        {
            const string partailViewUrl = "~/Views/DailyWorkOrderPrint/FilterForm/FilterFormGetWorkOrderAllStatusList.cshtml";

            var dataSource = GetWorkOrderStatusList();
            return PartialView(partailViewUrl, dataSource);

        }
        public OutputGetWorkOrderStatusTypeList[] GetWorkOrderStatusList() => ApiList.GetWorkOrderStatusTypeList();
        public static OutputGetMaintenanceGroupList[] GetMaintenanceGroupList()
        {
            var maintenanceGroupList = ApiList.GetMaintenanceGroupList();
            return maintenanceGroupList;
        }

        public static OutputGetStopTypeList[] GetWorkOrderStopTypeList()
        {
            var stopTypeList = ApiList.GetStopTypeList();
            return stopTypeList;

        }

        public OutputGetDailyWorkOrderPrintReport[] GetDailyWorkOrderPrintReport(int dateType = 0, string startShamsiDate = "", string endPersianDate = "", long statusId = 0)
        {
            var now = DateTime.Now;
            var endDate = now;
            var startDate = now.AddDays(-7);


            var mouth = 1;
            var year = 2;
            var specialDate = 3;

            if(dateType == mouth)
            {
                startDate = now.AddMonths(-1);
            }
            else if(dateType == year)
            {
                var persianCalander = new PersianCalendar();
                var persianYear = persianCalander.GetYear(now);
                var persianThisYearFirstDate = persianYear + "/01/01";
                startDate = Tools.ConvertToLatinDate(persianThisYearFirstDate);
            }
            else if(dateType == specialDate)
            {
                startDate = Tools.ConvertToLatinDate(startShamsiDate);
                endDate = Tools.ConvertToLatinDate(endPersianDate);
            }

            var apiParams = new InputGetDailyWorkOrderPrintReport
            {
                StartDate = startDate,
                EndDate = endDate,
                StatusID = statusId
            };
            var token = "a";

            return ApiList.GetDailyWorkOrderPrintReport(apiParams, token);
        }

        public ActionResult Grid(int dateType = 0, string startDate = "", string endDate = "", long statusId = 0)
        {
            const string partailViewUrl = "~/Views/DailyWorkOrderPrint/Grid/Grid.cshtml";
            var dataSource = GetDailyWorkOrderPrintReport(dateType, startDate, endDate, statusId);
            return PartialView(partailViewUrl, dataSource);
        }
    }
}