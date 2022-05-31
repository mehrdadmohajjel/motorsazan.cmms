using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.MachineManagement;
using Motorsazan.CMMS.Shared.Models.Input.SparePartReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class SparePartReportController : Controller
    {
        public ActionResult FilterFormGetDepartmentListHasMachine()
        {
            const string partialViewUrl =
                "~/Views/SparePartReport/FilterForm/FilterFormGetDepartmentListHasMachineCombo.cshtml";

            var departmentListInSelectWithMachine = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentListInSelectWithMachine);
        }

        public ActionResult FilterFormGetMachineList(InputGetMainMachineListBySubDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/SparePartReport/FilterForm/FilterFormGetMachineListCombo.cshtml";

            var machineList = ApiList.GetMainMachineListBySubDepartmentId(input);

            return PartialView(partialViewUrl, machineList);
        }

        public ActionResult FilterFormGetSubDepartmentListHasMachineByMainDepartmentId(
            InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/SparePartReport/FilterForm/FilterFormGetSubDepartmentListHasMachineCombo.cshtml";

            var subDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(input);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public ActionResult GetMachineSparePartReportByMachineId(InputGetMachineSparePartReportByMachineId input)
        {
            const string partialViewUrl = "~/Views/SparePartReport/Grid/ResultGrid.cshtml";

            var machineDocumentList = ApiList.GetMachineSparePartReportByMachineId(input);

            return PartialView(partialViewUrl, machineDocumentList);
        }

        [AccessToFormValidation(FormCode = "039")]
        public ActionResult Index() => View();
    }
}