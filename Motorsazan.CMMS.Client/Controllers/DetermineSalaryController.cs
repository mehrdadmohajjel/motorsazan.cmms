using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Models.Input.DetermineSalary;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class DetermineSalaryController: BaseController
    {
        [AccessToEventValidation(EventCode = "002", FormCode = "008")]
        public ActionResult BatchEditingUpdateSalaryModel(InputEditMaintenanceGroupMemberSalaryBySalaryId input,
            string persianStartDate)
        {
            input.StartDate = Tools.ConvertToLatinDate(persianStartDate);

            var token = GetUserToken();

            input.RegisterUserId = GetCurrentUser().UserID;

            var apiResult = ApiList.EditMaintenanceGroupMemberSalaryBySalaryId(input, token);
            return Content(apiResult);
        }

        public ActionResult DetailDetermineSalaryModal(InputGetMaintenanceGroupMemberSalaryDetailListBySalaryId input)
        {
            const string partialUrl = "~/Views/DetermineSalary/DetailModal/EmployeeSalaryDetailGridModal.cshtml";

            var apiResult = ApiList.GetMaintenanceGroupMemberSalaryDetailListBySalaryId(input);

            return PartialView(partialUrl, apiResult);
        }

        public ActionResult FillResultGrid()
        {
            const string partialViewUrl = "~/Views/DetermineSalary/Grid/ResultGrid.cshtml";

            var apiResult = ApiList.GetMaintenanceGroupMemberSalaryList();

            return PartialView(partialViewUrl, apiResult);
        }

        [AccessToFormValidation(FormCode = "008")]
        public ActionResult Index() => View();
    }
}