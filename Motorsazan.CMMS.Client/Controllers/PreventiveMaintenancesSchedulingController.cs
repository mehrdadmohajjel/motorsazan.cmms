using DevExpress.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.OperationItem;
using Motorsazan.CMMS.Shared.Models.Input.PLCReport;
using Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesScheduling;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.PreventiveMaintenancesScheduling;
using Motorsazan.CMMS.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OutputGetMaintenanceGroupList =
    Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder.OutputGetMaintenanceGroupList;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class PreventiveMaintenancesSchedulingController: BaseController
    {
        private DateTime enEndDate;
        private DateTime enStartDate;

        public InputGetPreventiveMaintenanceSchedulingListByCondition GridInput =
            new InputGetPreventiveMaintenanceSchedulingListByCondition();

        public ActionResult ChangeToWorkOrderModal(long[] pmSchedulingInfoIdList)
        {
            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesScheduling/Modals/ChangeToWorkOrderModal.cshtml";
            ViewData["PMSchedulingInfoIDList"] = pmSchedulingInfoIdList;

            return PartialView(partialViewUrl);
        }

        public ActionResult ChangeToWorkOrderModalGrid(long[] model)
        {
            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesScheduling/Modals/ChangeToWorkOrderModalGrid.cshtml";

            var item = new InputGetOperationItemListByPMSchedulingInfoId();
            var rowsCount = model.Length;
            var finalResult = new List<PMSchedulingInfoIDList>();
            for(var i = 0; i < rowsCount; i++)
            {
                var tempDataRow = model[i];
                var idItem = new PMSchedulingInfoIDList {PMSchedulingInfoId = tempDataRow};
                finalResult.Add(idItem);
            }

            item.PMSchedulingInfoIdList = finalResult.ToDataTable<PMSchedulingInfoIDList>();
            var apiResult = ApiList.GetOperationItemListByPMSchedulingInfoId(item);


            return PartialView(partialViewUrl, apiResult);
        }


        [HttpPost]
        [AccessToEventValidation(EventCode = "002", FormCode = "007")]
        public ActionResult ChangeToWorkOrderUpdateModel(
            MVCxGridViewBatchUpdateValues<OutputGetOperationItemListByPMSchedulingInfoId, int> batchValues,
            string ChangeToWorkOrderModalSuggestedDateDatePicker)
        {
            var token = GetUserToken();

            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesScheduling/Modals/ChangeToWorkOrderModalGrid.cshtml";
            if(ModelState.IsValid)
            {
                try
                {
                    var input = new InputSetWorkOrderToSeveralPereventiveMaintenanceScheduling();
                    var preferDate = Tools.ConvertToLatinDate(ChangeToWorkOrderModalSuggestedDateDatePicker);
                    foreach(var item in batchValues.Update)
                    {
                        var rowsCount = batchValues.Update.Count;
                        if(batchValues.IsValid(item))
                        {
                            var detail = new PMSchedulingInfoDetail();
                            var finalResult = new List<PMSchedulingInfoDetail>();
                            for(var i = 0; i < rowsCount; i++)
                            {
                                var tempDataRow = batchValues.Update[i];
                                var idItem = new PMSchedulingInfoDetail
                                {
                                    MachineId = tempDataRow.MachineId,
                                    PersonHour = tempDataRow.PersonHour,
                                    PMSchedulingInfoId = tempDataRow.PMSchedulingInfoId,
                                    PreferDate = preferDate
                                };
                                finalResult.Add(idItem);
                            }

                            input.PMSchedulingInfoDetailList = finalResult.ToDataTable<PMSchedulingInfoDetail>();
                            input.UserId = GetCurrentUser().UserID;
                        }
                    }

                    var apiResult = ApiList.SetWorkOrderToSeveralPereventiveMaintenanceScheduling(input, token);
                    ViewData["apiResult"] = apiResult;
                    ViewData["IsEnedeSuccesfully"] = true;

                    return PartialView(partialViewUrl);
                }
                catch(Exception e)
                {
                    ViewData["apiResult"] = e.Message;
                    ViewData["IsEnedeSuccesfully"] = false;
                    //throw new Exception(e.Message.ToString());
                }
            }

            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "003", FormCode = "007")]
        public ActionResult DeleteUntilYearEnd(long[] pmSchedulingInfoIdList, string deactiveReason)
        {
            var token = GetUserToken();

            var item = new InputRemoveSeveralOperationItemByPMSchedulingInfoId();
            var rowsCount = pmSchedulingInfoIdList.Length;
            var finalResult = new List<PMSchedulingInfoIDList>();
            for(var i = 0; i < rowsCount; i++)
            {
                var tempDataRow = pmSchedulingInfoIdList[i];
                var idItem = new PMSchedulingInfoIDList {PMSchedulingInfoId = tempDataRow};
                finalResult.Add(idItem);
            }

            item.DeactiveReason = deactiveReason;
            item.PMSchedulingInfoIDList = finalResult.ToDataTable<PMSchedulingInfoIDList>();
            var apiResult = ApiList.RemoveSeveralOperationItemByPMSchedulingInfoId(item, token);
            return Content(apiResult);
        }

        public ActionResult DeleteUntilYearEndPartial()
        {
            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesScheduling/DeactivateItemOperationUntileEndOfYear/DeleteUntilYearEndPartial.cshtml";
            return PartialView(partialViewUrl);
        }

        public ActionResult Grid(InputGetPreventiveMaintenanceSchedulingListByCondition input, string startShamsiDate,
            string endShamsiDate, int CustomWeekNumber = 0, DatePeriodType dateType = DatePeriodType.RecentThreeMonths)
        {
            const string partialViewUrl = "~/Views/PreventiveMaintenancesScheduling/Grid/Grid.cshtml";


            if(CustomWeekNumber == 0)
            {
                (enStartDate, enEndDate) = Tools.NormalizeDates(startShamsiDate, endShamsiDate, dateType);
            }
            else
            {
                var item = new InputGetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear
                {
                    NumberOfWeeksOfYear = CustomWeekNumber
                };
                var customDateByWeekNumber =
                    ApiList.GetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear(item);

                enStartDate = customDateByWeekNumber.StartDate;
                enEndDate = customDateByWeekNumber.EndDate;
            }

            input.StartDate = enStartDate;
            input.EndDate = enEndDate;
            GridInput.DepartmentId = input.DepartmentId;
            GridInput.EndDate = input.EndDate;
            GridInput.MachineId = input.MachineId;
            GridInput.MaintenanceGroupId = input.MaintenanceGroupId;
            GridInput.StartDate = input.StartDate;

            var dataSource = GetPreventiveMaintenanceSchedulingListByCondition(GridInput);
            return PartialView(partialViewUrl, dataSource);
        }

        public OutputGetPreventiveMaintenanceSchedulingListByCondition[]
            GetPreventiveMaintenanceSchedulingListByCondition(
                InputGetPreventiveMaintenanceSchedulingListByCondition values)
        {
            var token = GetUserToken();

            return ApiList.GetPreventiveMaintenanceSchedulingListByCondition(values, token);
        }

        public ActionResult FilterFormMachineCombo(InputGetMainMachineListByMainDepartmentIdAndSubDepartmentId value)
        {
            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesScheduling/FilterForm/FilterFormMachineCombo.cshtml";

            var MachineList = ApiList.GetMainMachineListByMainDepartmentIdAndSubDepartmentId(value);
            var allMachineList =
                new OutputGetMainMachineListByMainDepartmentIdAndSubDepartmentId
                {
                    MachineId = 0, MachineName = " همه", OldMachineCode = "دستگاه ها"
                };

            var dataSource = Tools.PrependGetAllItemToArray(MachineList, allMachineList);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormMainDepartmentCombo()
        {
            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesScheduling/FilterForm/FilterFormMainDepartmentCombo.cshtml";
            var token = GetUserToken();
            var departmentList = ApiList.GetAllMainDepartment(token);


            return PartialView(partialViewUrl, departmentList);
        }

        public ActionResult FilterFormSubDepartmentCombo(InputGetSubDepartmentListHasMachineByMainDepartmentId value)
        {
            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesScheduling/FilterForm/FilterFormSubDepartmentCombo.cshtml";
            var subDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(value);
            var allsubDepartmentList =
                new OutputGetSubDepartmentListHasMachineByMainDepartmentId {DepartmentId = 0, Title = " همه خطوط"};

            var dataSource = Tools.PrependGetAllItemToArray(subDepartmentList, allsubDepartmentList);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormMaintenanceGroupCombo()
        {
            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesScheduling/FilterForm/FilterFormMaintenanceGroupCombo.cshtml";
            var getMaintenanceGroupList = ApiList.GetMaintenanceGroupList();

            var allMaintenanceGroup =
                new OutputGetMaintenanceGroupList {MaintenanceGroupId = 0, MaintenanceGroupName = "همه"};

            var dataSource = Tools.PrependGetAllItemToArray(getMaintenanceGroupList, allMaintenanceGroup);

            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToFormValidation(FormCode = "007")]
        public ActionResult Index() => View();


        public ActionResult ShowDetail(InputGetOperationItemSparePartListByOperationItemId input)
        {
            var source = ApiList.GetOperationItemSparePartListByOperationItemId(input);


            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesScheduling/Grid/DetailGrid.cshtml";
            return PartialView(partialViewUrl, source);
        }

        public ActionResult ResetPredectiveMaintenanceJob()

        {
            var apiResult = ApiList.ResetPredectiveMaintenanceJob();
            return Content(apiResult);
        }


        public ActionResult ChangeJobStatus(InputChangeJobStatus input)

        {
            var apiResult = ApiList.ChangeJobStatusAsync(input);

            if(apiResult.JobStatus.ToLower() == "started" || apiResult.JobStatus.ToLower() == "stopped")
            {
                apiResult.IsSuccess = true;
                apiResult.JobStatus = "واکشی اطلاعات با موفقیت انجام شد";
            }

            return Json(apiResult);
        }
    }
}