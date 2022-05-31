using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MachineConsumeStockReport;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MachineConsumeStockReportController: Controller
    {
        public ActionResult FilterFormGetStockFromHavaleWorkorderReferral()
        {
            const string partialViewUrl =
                "~/Views/MachineConsumeStockReport/FilterForm/FilterFormGetStockFromHavaleWorkOrderReferralCombo.cshtml";

            var stockiList = ApiList.GetStockFromHavaleWorkOrderReferral();

            return PartialView(partialViewUrl, stockiList);
        }
        public ActionResult GetMachineConsumeStockReportByCondition(InputGetMachineConsumeStockReportByCondition input)
        {
            const string partialViewUrl = "~/Views/MachineConsumeStockReport/Grid/ResultGrid.cshtml";

            var machineDocumentList = ApiList.GetMachineConsumeStockReportByCondition(input);

            return PartialView(partialViewUrl, machineDocumentList);
        }
        [AccessToEventValidation(EventCode = "001", FormCode = "040")]
        public ActionResult Index()
        {
            return View();
        }

    }
}