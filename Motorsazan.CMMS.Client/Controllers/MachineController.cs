using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Machine;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MachineController: BaseController
    {
        public ActionResult AddFormMachineControlCombo()
        {
            const string partialViewUrl = "~/Views/Machine/AddMainMachineForm/AddFormMachineControlCombo.cshtml";
            var dataSource = ApiList.GetControlSystemTypeList();

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult AddFormMachineLevelCombo()
        {
            const string partialViewUrl = "~/Views/Machine/AddMainMachineForm/AddFormMachineLevelCombo.cshtml";
            var dataSource = ApiList.GetMachineLevelList();

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult AddFormMachineTypeCombo()
        {
            const string partialViewUrl = "~/Views/Machine/AddMainMachineForm/AddFormMachineTypeCombo.cshtml";
            var dataSource = ApiList.GetMachineTypeList();

            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "001")]
        public ActionResult AddNewMachine(InputAddTopMachine values)
        {
            var token = GetUserToken();
            values.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.AddTopMachine(values, token);
            return Content(apiResult);
        }

        [AccessToFormValidation(FormCode = "001")]
        public ActionResult Index() => View();
    }
}