/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.preventiveMaintenancesScheduling = (function () {

    const dom = {};
    const state = {
        customPeriodId: "CustomPeriod",
        customWeekId: "CustomWeek",
        customWeekId: "CustomWeek",
        dateType: "RecentThreeMonths",
        gridSelectedIds: null,
        machineIds: null,
        customWeekNumber: 0,
        maintenanceGroup: 0,
        departmentId: 0,
        machineId: 0,
        operationItemId:0
    };
    const tools = motorsazanClient.tools;
    const controllerName = "/PreventiveMaintenancesScheduling/";

    function batchEditStart(s, e) {
        if (e.focusedColumn.fieldName === "PersonHour")
            dom.changeToWorkOrderModalSaveBtn.SetEnabled(true);
    }



    function closePopUp() {
        motorsazanClient.contentModal.hideModal();

    }

    async function deleteUntilYearEnd() {
        const url = controllerName + "DeleteUntilYearEndPartial";
        const title = "حذف تولید برنامه پیشگیرانه برای آیتم های انتخابی";

        await motorsazanClient.contentModal.ajaxShow(title, url, null, setDom);

    }

    function handleGridBeginCallback(source) {
        state.dateType = dom.filterFormDateCombo.GetValue();
        state.startDate = dom.filterFormPeriodStartDatePicker.val();
        state.endDate = dom.filterFormPeriodEndDatePicker.val();
        state.customWeekNumber = dom.filterFormWeekNumberSpinEdit.GetValue();
        var url = controllerName + "Grid";

        source.callbackUrl = url + "?departmentId=" + dom.filterFormSubDepartmentCombo.GetValue() +
            "&machineId=" + dom.filterFormMachineCombo.GetValue() + "&maintenanceGroupId=" + dom.filterFormMaintenanceGroupCombo.GetValue() +
            "&dateType=" + state.dateType + "&startShamsiDate=" + state.startDate +
            "&endShamsiDate=" + state.endDate + "&CustomWeekNumber= " + state.customWeekNumber;
    }

    function handleToolbarButtonClick(s, e) {

        switch (e.item.name) {
            case "gridToolbarChangeToWorkOrderButton":
                handleGridToolbarChangeToWorkOrderButtonClick();
                break;
            case "gridToolbarDeleteUntilYearEndButton":
                handleGridToolbarDeleteUntilYearEndButtonClick();
                break;
            case "gridToolbarFetchPreventiveItemButton":
                handleGridToolbarFetchPreventiveIteButtonClick();
                break;
        }
    }

    function handleGridCustomBtnClick(source, event) {
        setDom();
        detailDom();
        state.pmschedulingId =
            dom.grid.GetRowKey(event.visibleIndex);
        const customBtnId = event.buttonID;

        if (customBtnId === "shoedetailBTN") return shoeDetailGrid();
        return showMaintenanceGroupMember();
    }

    function shoeDetailGrid() {

        dom.grid.GetRowValues(dom.grid.GetFocusedRowIndex(),
            "PMSchedulingInfoId;OperationItemId",
            (values) => {
                detailDom();

                const url = controllerName + "showDetail";

                state.operationItemId = values[1];
                
                const apiParam = {
                    OperationItemId: state.operationItemId
                };

                const title = "مشاهده جزئیات";
                motorsazanClient.contentModal.ajaxShow(title,
                    url,
                    apiParam,
                    detailDom,
                    false,
                    false);
            });






    }

    function handleDetailGridCallback(command) {
        const operationItemId = state.operationItemId;
        const url = controllerName +
            "ShowDetail" +
            "?OperationItemId=" +
            operationItemId;

        command.callbackUrl = url;
    }

    function detailDom() {
        dom.detailGrid = ASPxClientGridView.Cast("detailGrid");

    }
    function handleFilterFormDateComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDateComboError);
        tools.hideItem(dom.filterFormPeriodStartDatePickerParent);
        tools.hideItem(dom.filterFormPeriodEndDatePickerParent);
        tools.hideItem(dom.filterFormWeekNumberSpinEditParent);
        dom.filterFormWeekNumberSpinEdit.SetText(0);
        dom.filterFormMainDepartmentComboGrandParent.removeClass("col-md-4");
        dom.filterFormMainDepartmentComboGrandParent.addClass("col-md-4");

        dom.filterFormSubDepartmentComboGrandParent.removeClass("col-md-4");
        dom.filterFormSubDepartmentComboGrandParent.addClass("col-md-4");

        const dateType = dom.filterFormDateCombo.GetValue();
        const isDateTypeSelected = !tools.isNullOrEmpty(dateType);
        if (!isDateTypeSelected) {
            tools.showItem(dom.filterFormDateComboError);

            return;
        }

        if (dateType === state.customPeriodId) {
            dom.filterFormDateComboParent.removeClass("col-md-3");
            dom.filterFormDateComboParent.addClass("col-md-2");

            dom.filterFormMainDepartmentComboGrandParent.removeClass("col-md-4");
            dom.filterFormMainDepartmentComboGrandParent.addClass("col-md-4");

            dom.filterFormSubDepartmentComboGrandParent.removeClass("col-md-4");
            dom.filterFormSubDepartmentComboGrandParent.addClass("col-md-4");

            tools.showItem(dom.filterFormPeriodStartDatePickerParent);
            tools.showItem(dom.filterFormPeriodEndDatePickerParent);
            return;
        }

        if (dateType === state.customWeekId) {
            dom.filterFormDateComboParent.removeClass("col-md-3");
            dom.filterFormDateComboParent.addClass("col-md-2");

            tools.showItem(dom.filterFormWeekNumberSpinEditParent);
            return;
        }
    }

    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);
    }
    function handlechangeToWorkOrderModalSuggestedDateDatePickerChange() {
        tools.hideItem(dom.changeToWorkOrderModalSuggestedDateDatePickerError);
    }
    

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
    }

    function handlefilterFormMaintenanceGroupComboSelectedIndexChange()
    {
        tools.hideItem(dom.filterFormMaintenanceComboError);

    }

    function handleFilterFormMaintenanceGroupNameComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormMaintenanceComboError);
    }

    async function handleFilterFormMainDepartmentComboSelectedIndexChange() {

        tools.hideItem(dom.filterFormMainDepartmentComboError);

        const url = "/PreventiveMaintenancesScheduling/FilterFormSubDepartmentCombo";

        const apiParam = {
            departmentId: dom.filterFormMainDepartmentCombo.GetValue()
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.filterFormSubDepartmentComboParent.html(apiResult);
        setDom();

        dom.filterFormSubDepartmentCombo.SetValue();
        dom.filterFormMachineCombo.SetValue();

        dom.filterFormSubDepartmentCombo.SetEnabled(true);
        dom.filterFormMachineCombo.SetEnabled(false);
    }

    function handlePreventiveBeginCallback(s, e) {
        e.customArgs["ChangeToWorkOrderModalSuggestedDateDatePicker"] = dom.changeToWorkOrderModalSuggestedDateDatePicker.val();
        var pmSchedulingInfoIDList = state.gridSelectedIds;
        s.callbackUrl =
            controllerName + "ChangeToWorkOrderModalGrid?model=" + pmSchedulingInfoIDList;

    }
    function handlePreventiveEndCallback(s, e) {
        if (s.cpIsValid) {
            handleFilterFormGetPreventiveMaintenanceSchedulingListByConditionClick();
            motorsazanClient.messageModal.success(s.cpMessage);
            motorsazanClient.contentModal.hideModal();
            dom.grid.UnselectRows();
            delete s.cpIsValid;
            delete s.cpMessage;
        } else {
            handleFilterFormGetPreventiveMaintenanceSchedulingListByConditionClick();
            motorsazanClient.messageModal.error(s.cpMessage);
            delete s.cpIsValid;
            delete s.cpMessage;
        }

    }
    function handleFilterFormMachineComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormMachineComboError);
    }
    function handleFilterFormMachineComboBeginCallback(command) {
        var subDepartmentId = dom.filterFormSubDepartmentCombo.GetValue();
        var mainDepartmentId = dom.filterFormMainDepartmentCombo.GetValue();
        if (departmentID == null) departmentID = -1;

        command.callbackUrl =
            controllerName + "FilterFormMachineCombo?MainDepartmentId=" + mainDepartmentId + "&SubDepartmentId=" + subDepartmentId;
    }

    function handleFilterFormSubDepartmentComboBeginCallback(command) {
        var departmentID = dom.filterFormMainDepartmentCombo.GetValue();

        if (departmentID == null) departmentID = -1;

        command.callbackUrl =
            controllerName + "FilterFormSubDepartmentCombo?departmentID=" + departmentID;
    }


    async function handleFilterFormSubDepartmentComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormSubDepartmentComboError);

        const url = "/PreventiveMaintenancesScheduling/FilterFormMachineCombo";
        const apiParam = {
            mainDepartmentId: dom.filterFormMainDepartmentCombo.GetValue(),
            subDepartmentId : dom.filterFormSubDepartmentCombo.GetValue()
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.filterFormMachineComboParent.html(apiResult);
        setDom();

        dom.filterFormMachineCombo.SetValue();

        dom.filterFormMachineCombo.SetEnabled(true);
    }


    function handleGridSelectionChanged(source) {
        source.GetSelectedFieldValues("PMSchedulingInfoId",
            (values) => {
                state.gridSelectedIds = values;
                console.log(values);
                if (values.length < 1) {
                    dom.gridToolbarDeleteUntilYearEndButton.SetEnabled(false);
                    dom.gridToolbarChangeToWorkOrderButton.SetEnabled(false);

                }
                else {
                    dom.gridToolbarChangeToWorkOrderButton.SetEnabled(true);
                    dom.dom.gridToolbarDeleteUntilYearEndButton.SetEnabled(true);
                }
            });
    } 

    function handleGridToolbarChangeToWorkOrderButtonClick() {
        const title = "تبدیل به سفارشکار";
        const url = controllerName + "ChangeToWorkOrderModal";
        tools.initDatePicker("changeToWorkOrderModalSuggestedDateDatePicker");

        const pmSchedulingInfoIdList = state.gridSelectedIds;
        const apiParam = {
            pmSchedulingInfoIdList
        };
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, initChangeToWorkOrderModal);
    }


    function handleGridToolbarDeleteUntilYearEndButtonClick() {
        const message = "آیا از حذف برنامه ریزی آیتم های انتخاب شده اطمینان دارید؟";
        motorsazanClient.messageModal.confirm(message, deleteUntilYearEnd);
    }

     function handleGridToolbarFetchPreventiveIteButtonClick() {

        var url = "/PreventiveMaintenancesScheduling/ResetPredectiveMaintenanceJob";

        motorsazanClient.connector.post(url)
            .then(function (resultMessage) {
                motorsazanClient.contentModal.hide();
                motorsazanClient.messageModal.success(resultMessage);
                 handleResetJob();
            });
    }

    async function handleResetJob() {

        const url = controllerName + "ChangeJobStatus";

        const apiParam = {
            jobName: "PredictiveMaintenance-old",
            jobNewStatus: 1,
            projectName: "CMMS"
        }
        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        if (apiResult.IsSuccess) {
            motorsazanClient.messageModal.success(apiResult.JobStatus);
        } else {
            motorsazanClient.messageModal.error(apiResult.JobStatus);
        }
    }

    
     async function handleChangeToWorkOrderModalSaveBtnClick() {
        const canContinue = isBatchEditUpdateFormValid();
        if (!canContinue) return false;
        await dom.changeToWorkOrderModalGrid.UpdateEdit();
    }

    function isFilterFormValid() {

        var isValid = true;

        tools.hideItem(dom.filterFormMainDepartmentComboError);
        var MainDepartment = dom.filterFormMainDepartmentCombo.GetValue();
        var isMainDepartment = !tools.isNullOrEmpty(MainDepartment);
        if (!isMainDepartment) {
            isValid = false;
            tools.showItem(dom.filterFormMainDepartmentComboError);
        }

        tools.hideItem(dom.filterFormSubDepartmentComboError);
        var SubDepartment = dom.filterFormSubDepartmentCombo.GetValue();
        var isSubDepartmentSelected = !tools.isNullOrEmpty(SubDepartment);
        if (!isSubDepartmentSelected) {
            isValid = false;
            tools.showItem(dom.filterFormSubDepartmentComboError);
        }

        tools.hideItem(dom.filterFormMachineComboError);
        var Machine = dom.filterFormMachineCombo.GetValue();
        var isMachineSelected = !tools.isNullOrEmpty(Machine);
        if (!isMachineSelected) {
            isValid = false;
            tools.showItem(dom.filterFormMachineComboError);
        }

        tools.hideItem(dom.filterFormMaintenanceComboError);
        var Machine = dom.filterFormMaintenanceGroupCombo.GetValue();
        var isMachineSelected = !tools.isNullOrEmpty(Machine);
        if (!isMachineSelected) {
            isValid = false;
            tools.showItem(dom.filterFormMaintenanceComboError);
        }


        tools.hideItem(dom.filterFormDateComboError);
        var dateId = dom.filterFormDateCombo.GetValue();
        var isDateSelected = !tools.isNullOrEmpty(dateId);
        if (!isDateSelected) {
            isValid = false;
            tools.showItem(dom.filterFormDateComboError);
        }

        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        if (dateId == state.customPeriodId) {

            var periodStart = dom.filterFormPeriodStartDatePicker.val();
            var isPeriodStartSelected = !tools.isNullOrEmpty(periodStart);
            if (!isPeriodStartSelected) {
                isValid = false;
                tools.showItem(dom.filterFormPeriodStartDatePickerError);
            }

            var periodEnd = dom.filterFormPeriodEndDatePicker.val();
            var isPeriodEndSelected = !tools.isNullOrEmpty(periodEnd);
            if (!isPeriodEndSelected) {
                isValid = false;
                tools.showItem(dom.filterFormPeriodEndDatePickerError);
            };
        }
        else if (dateId == state.customWeekId) {
            tools.hideItem(dom.filterFormWeekNumberSpinEditError);
            var week = dom.filterFormWeekNumberSpinEdit.GetValue();
            var isWeekSelected = !tools.isNullOrEmpty(week);
            if (!isWeekSelected) {
                isValid = false;
                tools.showItem(dom.filterFormWeekNumberSpinEditError);
            }
        }
        return isValid;
    }

    function isBatchEditUpdateFormValid() {

        var isValid = true;

        tools.hideItem(dom.changeToWorkOrderModalSuggestedDateDatePickerError);
        var periodEnd = dom.changeToWorkOrderModalSuggestedDateDatePicker.val();
        var isPeriodEndSelected = !tools.isNullOrEmpty(periodEnd);
        if (!isPeriodEndSelected) {
            isValid = false;
            tools.showItem(dom.changeToWorkOrderModalSuggestedDateDatePickerError);
        };
        return isValid;
    }


    function init() {
        setDom();
        setEvents();

    }

    function initChangeToWorkOrderModal() {
        setChangeToWorkOrderModalDom();
        tools.initDatePicker("changeToWorkOrderModalSuggestedDateDatePicker");
        dom.changeToWorkOrderModalSaveBtn = ASPxClientButton.Cast("changeToWorkOrderModalSaveBtn");
    }

    async function loadPreventiveGrid() {
        var url = controllerName + "Grid";
        const apiParam = {
            departmentId: state.departmentId,
            machineId: state.machineId,
            maintenanceGroupId: state.maintenanceGroup,
            dateType: state.dateType,
            startShamsiDate: state.startDate,
            endShamsiDate: state.endDate,
            CustomWeekNumber: state.customWeekNumber
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.gridParent.html(apiResult);
        setDom();
    }

    function setChangeToWorkOrderModalDom() {
        dom.changeToWorkOrderModalSuggestedDateDatePicker = $("#changeToWorkOrderModalSuggestedDateDatePicker");
        dom.changeToWorkOrderModalSuggestedDateDatePickerError = $("#changeToWorkOrderModalSuggestedDateDatePickerError");
        dom.changeToWorkOrderModalGrid = ASPxClientGridView.Cast("changeToWorkOrderModalGrid");
    }
    async function setDeleteUntilYearEnd() {
        var pmSchedulingInfoIDList = state.gridSelectedIds;
        const apiParam = {
            deactiveReason: dom.deactiveReason.GetValue(),
            PMSchedulingInfoIDList: pmSchedulingInfoIDList
        };
        var url = "/preventiveMaintenancesScheduling/DeleteUntilYearEnd";
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        handleFilterFormGetPreventiveMaintenanceSchedulingListByConditionClick();
        motorsazanClient.contentModal.hide();
        motorsazanClient.messageModal.success(apiResult);
    }
    function setDom() {
        //FilterForm

        dom.filterFormMainDepartmentComboGrandParent = $("#filterFormMainDepartmentComboGrandParent");
        dom.filterFormMainDepartmentComboParent = $("#filterFormMainDepartmentComboParent");
        dom.filterFormMainDepartmentCombo = ASPxClientComboBox.Cast("filterFormMainDepartmentCombo");
        dom.filterFormMainDepartmentComboError = $("#filterFormMainDepartmentComboError");

        dom.filterFormSubDepartmentComboGrandParent = $("#filterFormSubDepartmentComboGrandParent");
        dom.filterFormSubDepartmentComboParent = $("#filterFormSubDepartmentComboParent");
        dom.filterFormSubDepartmentCombo = ASPxClientComboBox.Cast("filterFormSubDepartmentCombo");
        dom.filterFormSubDepartmentComboError = $("#filterFormSubDepartmentComboError");

        dom.filterFormMachineComboGrandParent = $("#filterFormMachineComboGrandParent");
        dom.filterFormMachineComboParent = $("#filterFormMachineComboParent");
        dom.filterFormMachineCombo = ASPxClientComboBox.Cast("filterFormMachineCombo");
        dom.filterFormMachineComboError = $("#filterFormMachineComboError");


        dom.filterFormMaintenanceGroupComboParent = $("#filterFormMaintenanceGroupComboParent");
        dom.filterFormMaintenanceGroupCombo = ASPxClientComboBox.Cast("filterFormMaintenanceGroupCombo");
        dom.filterFormMaintenanceComboError = $("#filterFormMaintenanceComboError");

        dom.filterFormDateComboParent = $("#filterFormDateComboParent");
        dom.filterFormDateCombo = ASPxClientComboBox.Cast("filterFormDateCombo");
        dom.filterFormDateComboError = $("#filterFormDateComboError");

        dom.filterFormPeriodStartDatePickerParent = $("#filterFormPeriodStartDatePickerParent");
        dom.filterFormPeriodStartDatePicker = $("#filterFormPeriodStartDatePicker");
        dom.filterFormPeriodStartDatePickerError = $("#filterFormPeriodStartDatePickerError");

        dom.filterFormPeriodEndDatePickerParent = $("#filterFormPeriodEndDatePickerParent");
        dom.filterFormPeriodEndDatePicker = $("#filterFormPeriodEndDatePicker");
        dom.filterFormPeriodEndDatePickerError = $("#filterFormPeriodEndDatePickerError");

        dom.filterFormWeekNumberSpinEditParent = $("#filterFormWeekNumberSpinEditParent");
        dom.filterFormWeekNumberSpinEdit = ASPxClientSpinEdit.Cast("filterFormWeekNumberSpinEdit");
        dom.filterFormWeekNumberSpinEditError = $("#filterFormWeekNumberSpinEditError");

        dom.deactiveReasonParent = $("#deactiveReasonParent");
        dom.deactiveReason = ASPxClientMemo.Cast("deactiveReason");
        dom.deactiveReasonError = $("#deactiveReasonError");


        
        dom.changeToWorkOrderModalSuggestedDateDatePicker = $("#changeToWorkOrderModalSuggestedDateDatePicker");
        dom.changeToWorkOrderModalSuggestedDateDatePickerError = $("#changeToWorkOrderModalSuggestedDateDatePickerError");
        dom.changeToWorkOrderModalGrid = ASPxClientGridView.Cast("changeToWorkOrderModalGrid");
        dom.changeToWorkOrderModalSaveBtn = ASPxClientButton.Cast("changeToWorkOrderModalSaveBtn");

        //grid
        dom.gridParent = $("#gridParent");
        dom.grid = ASPxClientGridView.Cast("grid");
        dom.gridToolbarDeleteUntilYearEndButton = grid.GetToolbar(0)
            .GetItemByName("gridToolbarDeleteUntilYearEndButton");

        dom.gridToolbarChangeToWorkOrderButton = grid.GetToolbar(0)
            .GetItemByName("gridToolbarChangeToWorkOrderButton");
    }

    async function handleFilterFormGetPreventiveMaintenanceSchedulingListByConditionClick() {
        var canContinue = isFilterFormValid();
        if (!canContinue) return false;

        state.dateType = dom.filterFormDateCombo.GetValue();
        state.startDate = dom.filterFormPeriodStartDatePicker.val();
        state.endDate = dom.filterFormPeriodEndDatePicker.val();
        state.customWeekNumber = dom.filterFormWeekNumberSpinEdit.GetValue();
        state.maintenanceGroup = dom.filterFormMaintenanceGroupCombo.GetValue();
        state.departmentId = dom.filterFormSubDepartmentCombo.GetValue();
        state.machineId = dom.filterFormMachineCombo.GetValue();
        await loadPreventiveGrid();
    }

    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);
        dom.changeToWorkOrderModalSuggestedDateDatePicker.change(handlechangeToWorkOrderModalSuggestedDateDatePickerChange);
        dom.gridToolbarDeleteUntilYearEndButton.SetEnabled(false);
        dom.gridToolbarChangeToWorkOrderButton.SetEnabled(false);

        dom.filterFormSubDepartmentCombo.SetValue();
        dom.filterFormMachineCombo.SetValue();

        dom.filterFormSubDepartmentCombo.SetEnabled(false);
        dom.filterFormMachineCombo.SetEnabled(false);
    }


    $(document).ready(init);

    return {
        handleFilterFormGetPreventiveMaintenanceSchedulingListByConditionClick:
            handleFilterFormGetPreventiveMaintenanceSchedulingListByConditionClick,
        handleGridBeginCallback: handleGridBeginCallback,
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormMainDepartmentComboSelectedIndexChange: handleFilterFormMainDepartmentComboSelectedIndexChange,
        handlefilterFormMaintenanceGroupComboSelectedIndexChange:
            handlefilterFormMaintenanceGroupComboSelectedIndexChange,
        handleFilterFormSubDepartmentComboBeginCallback: handleFilterFormSubDepartmentComboBeginCallback,
        handleFilterFormMaintenanceGroupNameComboSelectedIndexChange:
            handleFilterFormMaintenanceGroupNameComboSelectedIndexChange,
        handleFilterFormMachineComboSelectedIndexChange: handleFilterFormMachineComboSelectedIndexChange,
        handleFilterFormMachineComboBeginCallback: handleFilterFormMachineComboBeginCallback,
        handleFilterFormSubDepartmentComboSelectedIndexChange: handleFilterFormSubDepartmentComboSelectedIndexChange,
        handleGridSelectionChanged: handleGridSelectionChanged,
        handleChangeToWorkOrderModalSaveBtnClick: handleChangeToWorkOrderModalSaveBtnClick,
        setDeleteUntilYearEnd: setDeleteUntilYearEnd,
        handlePreventiveBeginCallback: handlePreventiveBeginCallback,
        handlePreventiveEndCallback: handlePreventiveEndCallback,
        batchEditStart: batchEditStart,
        closePopUp: closePopUp,
        handleToolbarButtonClick: handleToolbarButtonClick,
        handleGridCustomBtnClick: handleGridCustomBtnClick,
        handleDetailGridCallback:handleDetailGridCallback
};
})();