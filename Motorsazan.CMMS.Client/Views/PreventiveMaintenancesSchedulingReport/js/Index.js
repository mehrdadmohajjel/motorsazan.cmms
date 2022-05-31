/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.preventiveMaintenancesSchedulingReport = (function () {

    const dom = {};
    const state = {
        customPeriodId: "CustomPeriod",
        customWeekId: "CustomWeek",
        gridSelectedIds: null,
        machineIds: null,
        customWeekNumber: 0,
        maintenanceGroup: 0,
        departmentId: 0,
        childDepartmentId:0
    };
    const tools = motorsazanClient.tools;
    const controllerName = "/PreventiveMaintenancesSchedulingReport/";


    function handleGridBeginCallback(source) {
        state.dateType = dom.filterFormDateCombo.GetValue();
        state.startDate = dom.filterFormPeriodStartDatePicker.val();
        state.endDate = dom.filterFormPeriodEndDatePicker.val();
        state.customWeekNumber = dom.filterFormWeekNumberSpinEdit.GetValue();

        var url = controllerName + "Grid";

        source.callbackUrl = url + "?ParentDepartmentId=" + dom.filterFormMainDepartmentCombo.GetValue() +
            "&childDepartmentId=" + dom.filterFormSubDepartmentCombo.GetValue() +
            "&maintenanceGroupId=" + dom.filterFormMaintenanceGroupCombo.GetValue() +
            "&dateType=" + state.dateType + "&startShamsiDate=" + state.startDate +
            "&endShamsiDate=" + state.endDate + "&CustomWeekNumber= " + state.customWeekNumber;
    }

    function handleFilterFormDateComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDateComboError);
        tools.hideItem(dom.filterFormPeriodStartDatePickerParent);
        tools.hideItem(dom.filterFormPeriodEndDatePickerParent);
        tools.hideItem(dom.filterFormWeekNumberSpinEditParent);
        dom.filterFormWeekNumberSpinEdit.SetText(0);

        const dateType = dom.filterFormDateCombo.GetValue();
        const isDateTypeSelected = !tools.isNullOrEmpty(dateType);
        if (!isDateTypeSelected) {
            tools.showItem(dom.filterFormDateComboError);

            return;
        }

        if (dateType === state.customPeriodId) {
            dom.filterFormDateComboParent.removeClass("col-md-4");
            dom.filterFormDateComboParent.addClass("col-md-4");

            tools.showItem(dom.filterFormPeriodStartDatePickerParent);
            tools.showItem(dom.filterFormPeriodEndDatePickerParent);
            return;
        }

        if (dateType === state.customWeekId) {
            dom.filterFormDateComboParent.removeClass("col-md-4");
            dom.filterFormDateComboParent.addClass("col-md-4");

            tools.showItem(dom.filterFormWeekNumberSpinEditParent);
            return;
        }
    }


    async function handleFilterFormMainDepartmentComboSelectedIndexChange() {

        tools.hideItem(dom.filterFormMainDepartmentComboError);

        const url = "/PreventiveMaintenancesSchedulingReport/FilterFormSubDepartmentCombo";

        const apiParam = {
            departmentId: dom.filterFormMainDepartmentCombo.GetValue()
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.filterFormSubDepartmentComboParent.html(apiResult);
        setDom();


    }

    function handlePreventiveBeginCallback(s, e) {
        e.customArgs["ChangeToWorkOrderModalSuggestedDateDatePicker"] = dom.changeToWorkOrderModalSuggestedDateDatePicker.val();
    }


    function handleFilterFormSubDepartmentComboBeginCallback(command) {
        var departmentID = dom.filterFormMainDepartmentCombo.GetValue();

        if (departmentID == null) departmentID = -1;

        command.callbackUrl =
            controllerName + "FilterFormSubDepartmentCombo?departmentID=" + departmentID;
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



    function init() {
        setDom();
        setEvents();

    }


    async function loadPreventiveGrid() {
        var url = controllerName + "Grid";
        const apiParam = {
            parentDepartmentId: state.departmentId,
            childDepartmentId: state.childDepartmentId,
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

        

        //grid
        dom.gridParent = $("#gridParent");
        dom.grid = ASPxClientGridView.Cast("grid");
        dom.gridToolbarDeleteUntilYearEndButton = ASPxClientButton.Cast("gridToolbarDeleteUntilYearEndButton");
        dom.gridToolbarChangeToWorkOrderButton = ASPxClientButton.Cast("gridToolbarChangeToWorkOrderButton");
    }



    async function handleFilterFormGetPreventiveMaintenanceSchedulingListByConditionClick() {
        var canContinue = isFilterFormValid();
        if (!canContinue) return false;

        state.dateType = dom.filterFormDateCombo.GetValue();
        state.startDate = dom.filterFormPeriodStartDatePicker.val();
        state.endDate = dom.filterFormPeriodEndDatePicker.val();
        state.customWeekNumber = dom.filterFormWeekNumberSpinEdit.GetValue();
        state.maintenanceGroup = dom.filterFormMaintenanceGroupCombo.GetValue();
        state.childDepartmentId = dom.filterFormSubDepartmentCombo.GetValue();
        state.departmentId = dom.filterFormMainDepartmentCombo.GetValue();
        loadPreventiveGrid();
    }

    function setEvents() {
    }

    $(document).ready(init);

    return {
        handleFilterFormGetPreventiveMaintenanceSchedulingListByConditionClick: handleFilterFormGetPreventiveMaintenanceSchedulingListByConditionClick,
        handleGridBeginCallback: handleGridBeginCallback,
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormMainDepartmentComboSelectedIndexChange: handleFilterFormMainDepartmentComboSelectedIndexChange,
        handleFilterFormSubDepartmentComboBeginCallback: handleFilterFormSubDepartmentComboBeginCallback,
        handlePreventiveBeginCallback: handlePreventiveBeginCallback
    };
})();