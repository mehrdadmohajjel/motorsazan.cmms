using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.ItemOperationReport;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.ItemOperationReport;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class ItemOperationReportController: Controller
    {
        public ActionResult FilterFormDepartmentIdCombo()
        {
            const string partialViewUrl =
                "~/Views/ItemOperationReport/FilterForm/FilterFormDepartmentIdCombo.cshtml";

            var getDepartmentList = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, getDepartmentList);
        }

        public ActionResult FilterFormMachineIdCombo(InputGetMainMachineListBySubDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/ItemOperationReport/FilterForm/FilterFormMachineIdCombo.cshtml";

            var getMachineList = ApiList.GetMainMachineListBySubDepartmentId(input);

            var allMachine = new OutputGetMainMachineListBySubDepartmentId {MachineId = 0, MachineName = "همه"};

            var dataSource = Tools.PrependGetAllItemToArray(getMachineList, allMachine);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormItemTypeCombo()
        {
            const string partialViewUrl =
                "~/Views/ItemOperationReport/FilterForm/FilterFormItemTypeCombo.cshtml";

            var getMachineList = ApiList.GetOperationItemTypeList();

            var allMachine =
                new OutputGetOperationItemTypeList {OperationItemTypeId = 0, OperationItemTypeShowName = "همه"};

            var dataSource = Tools.PrependGetAllItemToArray(getMachineList, allMachine);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormSubDepartmentIdCombo(InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/ItemOperationReport/FilterForm/FilterFormSubDepartmentIdCombo.cshtml";

            var getSubDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(input);

            return PartialView(partialViewUrl, getSubDepartmentList);
        }

        public static OutputGetMaintenanceGroupList[] GetAllMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetItemOperationReportByCondtion input)
        {
            const string partialViewUrl = "~/Views/ItemOperationReport/Grid/Grid.cshtml";

            if(input.OperationTypeId == -1)
            {
                return PartialView(partialViewUrl);
            }


            var result = ApiList.GetItemOperationReportByCondtion(input);

            return PartialView(partialViewUrl, result);
        }

        [AccessToFormValidation(FormCode = "032")]
        public ActionResult Index() => View();
    }
}