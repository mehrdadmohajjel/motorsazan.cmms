using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Models.Input.MachineStopStatusReport;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using System.Web.Mvc;
using Motorsazan.CMMS.Client.Filters;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MachineStopStatusReportController: BaseController
    {

        [AccessToFormValidation(FormCode = "015")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowGrid()
        {
            const string partialViewUrl = "~/Views/MachineStopStatusReport/Grid/ResultGrid.cshtml";
            var dataSource = ApiList.GetMachineStopStatusReportList();
            return PartialView(partialViewUrl, dataSource);

        }
        public ActionResult ShowWorkOrderDetail(long machineId)
        {
            const string partialViewUrl = "~/Views/MachineStopStatusReport/Grid/WorkOrderDetailGrid.cshtml";
            var item = new InputGetWorkOrderListByMachineId
            {
                MachineId = machineId
            };
            var dataSource = ApiList.GetWorkOrderListByMachineId(item);
            return PartialView(partialViewUrl, dataSource);

        }
        public ActionResult DetailRow(long workOrderId = 0)
        {
            const string partialViewUrl = "~/Views/MachineStopStatusReport/Grid/DetailRow.cshtml";
            var apiParam = new InputGetWorkOrderByWorkOrderId
            {
                WorkOrderId = workOrderId
            };
            var token = GetUserToken();
            var workOrderItem = ApiList.GetWorkOrderByWorkOrderId(apiParam, token);

            return PartialView(partialViewUrl, workOrderItem);
        }

        public ActionResult ShowActionHistory(long workOrderId)
        {
            var apiParam = new InputGetActionOrDelayListByWorkOrderId
            {
                WorkOrderId = workOrderId
            };
            var token = GetUserToken();
            var partialViewUrl = "~/Views/MachineStopStatusReport/Grid/ShowActionHistoryPartial.cshtml";

            var dataSource = ApiList.GetActionOrDelayListByWorkOrderId(apiParam, token);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult ShowReferenceHistory(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderReferralListByWorkOrderId
            {
                WorkOrderId = workOrderId
            };
            var token = GetUserToken();
            const string partialViewUrl = "~/Views/MachineStopStatusReport/Grid/ShowReferenceHistoryPartial.cshtml";

            var dataSource = ApiList.GetWorKOrderReferralListByWorkOrderId(apiParam, token);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult WorKOrderConsumableList(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderConsumableListByWorkOrderId
            {
                WorkOrderId = workOrderId
            };
            var token = GetUserToken();
            var partialViewUrl = "~/Views/MachineStopStatusReport/Grid/ShowWorKOrderConsumableList.cshtml";

            var dataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(apiParam, token);
            return PartialView(partialViewUrl, dataSource);
        }
        public ActionResult RegistrarInfoModal(InputGetWorkOrderRegistrantInfoByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/MachineStopStatusReport/Grid/RegistrarInfoModal.cshtml";

            var workOrderRegistrantInfoDataSource = ApiList.GetWorkOrderRegistrantInfoByWorkOrderId(input);

            return PartialView(partialViewUrl, workOrderRegistrantInfoDataSource);
        }

    }
}