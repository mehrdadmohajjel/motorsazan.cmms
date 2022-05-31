using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.MachineManagement;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MachineAttachmentReportController: BaseController
    {
        public ActionResult FilterFormGetDepartmentListHasMachine()
        {
            const string partialViewUrl =
                "~/Views/MachineAttachmentReport/FilterForm/FilterFormGetDepartmentListHasMachineCombo.cshtml";

            var departmentListInSelectWithMachine = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentListInSelectWithMachine);
        }

        public ActionResult FilterFormGetMachineList(InputGetMainMachineListBySubDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/MachineAttachmentReport/FilterForm/FilterFormGetMachineListCombo.cshtml";

            var machineList = ApiList.GetMainMachineListBySubDepartmentId(input);

            return PartialView(partialViewUrl, machineList);
        }

        public ActionResult FilterFormGetSubDepartmentListHasMachineByMainDepartmentId(
            InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/MachineAttachmentReport/FilterForm/FilterFormGetSubDepartmentListHasMachineCombo.cshtml";

            var subDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(input);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public ActionResult GetMachineDocumentListByMachineId(InputGetMachineDocumentListByMachineId input)
        {
            const string partialViewUrl = "~/Views/MachineAttachmentReport/Grid/ResultGrid.cshtml";

            var machineDocumentList = ApiList.GetMachineDocumentListByMachineId(input);

            return PartialView(partialViewUrl, machineDocumentList);
        }

        [AccessToFormValidation(FormCode = "038")]
        public ActionResult Index() => View();
    }
}