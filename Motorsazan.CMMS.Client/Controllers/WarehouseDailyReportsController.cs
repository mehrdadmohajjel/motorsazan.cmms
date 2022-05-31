using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.WarehouseDailyReports;
using System.Web.Mvc;
using DevExpress.CodeParser;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class WarehouseDailyReportsController: BaseController
    {
        [AccessToFormValidation(FormCode = "037")]
        public ActionResult Index() => View();

        public static OutputGetMaintenanceGroupList[] GetWorkOrderMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult FillResultGrid(InputGetDailyWearhouseReportByCondition input, string persianStartDate,
            string persianEndDate)
        {
            const string partialViewUrl = "~/Views/WarehouseDailyReports/Grid/ResultGrid.cshtml";

            if(input.ReportType == -1)
            {
                return PartialView(partialViewUrl);
            }

            if(input.ReportType == 1)
            {
                (input.StartDate, input.EndDate) =
                    Tools.NormalizeDates(persianStartDate, persianEndDate, DatePeriodType.CustomPeriod);

            }
            else
            {
                (input.StartDate, input.EndDate) = (null, null);
            }

            var dataSource = ApiList.GetDailyWearhouseReportByCondition(input);

            return PartialView(partialViewUrl, dataSource);
        }
    }
}