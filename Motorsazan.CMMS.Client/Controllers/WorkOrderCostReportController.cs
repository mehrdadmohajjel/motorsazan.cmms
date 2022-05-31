using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.WorkOrderCostReport;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motorsazan.CMMS.Client.Filters;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class WorkOrderCostReportController: BaseController
    {
        public ActionResult FilterFormDepartmentCombo()
        {
            const string partialViewUrl =
                "~/Views/WorkOrderCostReport/FilterForm/FilterFormDepartmentIdCombo.cshtml";

            var token = GetUserToken();

            var getDepartmentList = ApiList.GetAllMainDepartment(token);

            var allDepartment = new OutputGetAllMainDepartment { DepartmentId = 0, Title = "همه" };

            var dataSource = Tools.PrependGetAllItemToArray(getDepartmentList, allDepartment);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormSubDepartmentListByMainDepartmentId(
            InputGetSubDepartmentListByMainDepartmentId value)
        {
            const string partialViewUrl =
                "~/Views/WorkOrderCostReport/FilterForm/FilterFormSubDepartmentIdCombo.cshtml";
            var subDepartmentList = ApiList.GetSubDepartmentListByMainDepartmentId(value);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public static OutputGetStopTypeList[] GetAllStopTypeList() =>
            ApiList.GetStopTypeList();


        public ActionResult Grid(InputGetWorkOrderCostReportByCondition input, string startDate, string endDate,
            DatePeriodType datePeriodType = DatePeriodType.CurrentDay)
        {
            const string partialViewUrl = "~/Views/WorkOrderCostReport/Grid/Grid.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var dataSource = ApiList.GetWorkOrderCostReportByCondition(input);


            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToFormValidation(FormCode = "027")]
        public ActionResult Index() => View();
    }
}