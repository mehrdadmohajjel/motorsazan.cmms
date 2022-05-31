using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroup;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MaintenanceGroupController: BaseController
    {
        private const string PartialViewAddress = "~/Views/MaintenanceGroup/";

        public ActionResult AddFormEmployeeCombo()
        {
            const string partialUrl = PartialViewAddress + "AssignEmployeeToMaintenance/AddFormEmployeeCombo.cshtml";
            var token = GetUserToken();
            var dataSource = ApiList.GetAllEmployeeList(token);
            return PartialView(partialUrl, dataSource);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "003")]
        public ActionResult AssignEmployeeToMaintenanceGroup(InputAddEmployeeToMaintenanceGroup input)
        {
            var token = GetUserToken();
            var apiResult = ApiList.AssignEmployeeToMaintenanceGroup(input, token);
            return Content(apiResult);
        }

        public ActionResult AssignEmployeeToMaintenanceGroupForm(long maintenanceGroupId)
        {
            const string partialViewUrl = PartialViewAddress + "AssignEmployeeToMaintenance/AddForm.cshtml";
            ViewData["MaintenanceGroupId"] = maintenanceGroupId;
            return PartialView(partialViewUrl);
        }

        [AccessToFormValidation(FormCode = "003")]
        public ActionResult Index() => View();

        public ActionResult MaintenanceGroupGird()
        {
            const string partialUrl = PartialViewAddress + "Grid/MaintenanceGroupGird.cshtml";
            var dataSource = ApiList.GetMaintenanceGroupList();
            return PartialView(partialUrl, dataSource);
        }

        public ActionResult MaintenanceGroupMemberGird(InputGetMaintenanceGroupMemberListByMaintenanceGroupId values)
        {
            const string partialUrl =
                PartialViewAddress + "AssignEmployeeToMaintenance/MaintenanceGroupMemberGird.cshtml";
            var dataSource = ApiList.GetMaintenanceGroupMemberListByMaintenanceGroupId(values);
            return PartialView(partialUrl, dataSource);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "003")]
        public ActionResult RemoveMaintenanceGroupMemberFromMaintenanceGroup(
            InputRemoveMaintenanceGroupMemberFromMaintenanceGroup values)
        {
            var token = GetUserToken();

            var apiResult = ApiList.RemoveMaintenanceGroupMemberFromMaintenanceGroup(values, token);
            return Content(apiResult);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "003")]
        public ActionResult SetSuperviserToMaintenanceGroupMember(InputGetSuperviserToMaintenanceGroupMember values)
        {
            var token = GetUserToken();
            var apiResult = ApiList.SetSuperviserToMaintenanceGroupMember(values, token);
            return Content(apiResult);
        }

        public ActionResult ShowMaintenanceGroupMemberGird(InputGetMaintenanceGroupMemberListByMaintenanceGroupId values)
        {
            const string partialUrl = PartialViewAddress + "ShowMember/ShowMaintenanceGroupPeopleGird.cshtml";
            var dataSource = ApiList.GetMaintenanceGroupMemberListByMaintenanceGroupId(values);
            return PartialView(partialUrl, dataSource);
        }
    }
}