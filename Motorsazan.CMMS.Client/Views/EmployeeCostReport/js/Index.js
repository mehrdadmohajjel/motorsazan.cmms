/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.employeeCostReport = (function () {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod"
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/EmployeeCostReport/";

    async function fillGrid() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "Grid";

        const employeeId = dom.filterFormEmployeeCombo.GetValue();
        const datePeriodType = dom.filterFormDateCombo.GetValue();
        const periodStartDate = dom.filterFormPeriodStartDatePicker.val();
        const periodEndDate = dom.filterFormPeriodEndDatePicker.val();


        const apiParam = {
            employeeId: employeeId,
            startDate: periodStartDate,
            endDate: periodEndDate,
            datePeriodType: datePeriodType
        };
        const grid = await motorsazanClient.connector.post(url, apiParam);
        dom.gridParent.html(grid);
        setDom();
    }

    function handleFilterFormDateComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDateComboError);
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        const selectedDateId = dom.filterFormDateCombo.GetValue();

        if (selectedDateId === state.customPeriodId) {
            dom.filterFormDateComboDesign.removeClass("col-md-4").addClass("col-md-2");
            tools.showItem(dom.filterFormPeriodStartDatePickerParent);
            tools.showItem(dom.filterFormPeriodEndDatePickerParent);
            tools.hideItem(dom.filterFormBeingItemsDivCenter);
        } else {
            dom.filterFormDateComboDesign.removeClass("col-md-2").addClass("col-md-4");
            tools.hideItem(dom.filterFormPeriodStartDatePickerParent);
            tools.hideItem(dom.filterFormPeriodEndDatePickerParent);
            tools.showItem(dom.filterFormBeingItemsDivCenter);
            dom.filterFormPeriodStartDatePicker.val("");
            dom.filterFormPeriodEndDatePicker.val("");
        }
    }

    function handleFilterFormEmployeeComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormEmployeeComboError);
        dom.filterFormDateCombo.SetEnabled(true);
    }

    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
    }

    function handleFilterFormSearchButtonClick() {
        if (!isFilterFormValid())
            return;

        fillGrid();
    }

    function handleGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            command.callbackUrl =
                controllerName +
                "Grid" +
                "?employeeId=" +
                -1 +
                "&startDate=" +
                "" +
                "&endDate=" +
                "" +
                "&datePeriodType=" +
                7;
            return;
        }

        var employeeId =
            +dom.filterFormEmployeeCombo.GetValue() > -1
                ? dom.filterFormEmployeeCombo.GetValue()
                : -1;

        var dateTypeId = dom.filterFormDateCombo.GetValue();
        const periodStartDate = dom.filterFormPeriodStartDatePicker.val();
        const periodEndDate = dom.filterFormPeriodEndDatePicker.val();

        if (dateTypeId == null ||
            (dateTypeId === state.customPeriodId &&
                (tools.isNullOrEmpty(periodStartDate) || tools.isNullOrEmpty(periodEndDate)))) {
            employeeId = -1;
            dateTypeId = -1;
        }

        command.callbackUrl =
            controllerName +
            "Grid" +
            "?employeeId=" +
            employeeId +
            "&startDate=" +
            periodStartDate +
            "&endDate=" +
            periodEndDate +
            "&datePeriodType=" +
            dateTypeId;
    }

    function init() {
        setDom();
        setEvents();

        dom.filterFormDateCombo.SetEnabled(false);
    }

    function isFilterFormValid(shouldDisplayValidationErrors = true) {
        var isValid = true;

        tools.hideItem(dom.filterFormEmployeeComboError);

        const employeeId = dom.filterFormEmployeeCombo.GetValue();
        const isEmployeeSelected = !tools.isNullOrEmpty(employeeId);
        if (!isEmployeeSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormEmployeeComboError);
        }

        tools.hideItem(dom.filterFormDateComboError);
        const dateId = dom.filterFormDateCombo.GetValue();
        const isDateSelected = !tools.isNullOrEmpty(dateId);
        if (!isDateSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormDateComboError);
        }

        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        if (dateId === state.customPeriodId) {

            const periodStart = dom.filterFormPeriodStartDatePicker.val();
            const isPeriodStartSelected = !tools.isNullOrEmpty(periodStart);
            if (!isPeriodStartSelected) {
                isValid = false;
                shouldDisplayValidationErrors && tools.showItem(dom.filterFormPeriodStartDatePickerError);
            }

            const periodEnd = dom.filterFormPeriodEndDatePicker.val();
            const isPeriodEndSelected = !tools.isNullOrEmpty(periodEnd);
            if (!isPeriodEndSelected) {
                isValid = false;
                shouldDisplayValidationErrors && tools.showItem(dom.filterFormPeriodEndDatePickerError);
            };
        }

        return isValid;
    }

    function setDom() {

        //Filter Form
        dom.filterFormEmployeeCombo = ASPxClientComboBox.Cast("filterFormEmployeeCombo");
        dom.filterFormEmployeeComboError = $("#filterFormEmployeeComboError");

        dom.filterFormDateCombo = ASPxClientComboBox.Cast("filterFormDateCombo");
        dom.filterFormDateComboParent = $("#filterFormDateComboParent");
        dom.filterFormDateComboError = $("#filterFormDateComboError");
        dom.filterFormDateComboDesign = $("#filterFormDateComboDesign");

        dom.filterFormPeriodStartDatePicker = $("#filterFormPeriodStartDatePicker");
        dom.filterFormPeriodStartDatePickerParent = $("#filterFormPeriodStartDatePickerParent");
        dom.filterFormPeriodStartDatePickerError = $("#filterFormPeriodStartDatePickerError");

        dom.filterFormPeriodEndDatePicker = $("#filterFormPeriodEndDatePicker");
        dom.filterFormPeriodEndDatePickerParent = $("#filterFormPeriodEndDatePickerParent");
        dom.filterFormPeriodEndDatePickerError = $("#filterFormPeriodEndDatePickerError");
        dom.filterFormBeingItemsDivCenter = $("#filterFormBeingItemsDivCenter");

        //Grid
        dom.gridParent = $("#gridParent");
        dom.grid = ASPxClientGridView.Cast("grid");
    }

    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);
    }


    $(document).ready(init);

    return {
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormEmployeeComboSelectedIndexChange: handleFilterFormEmployeeComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleGridBeginCallback: handleGridBeginCallback
    };
})();
