/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.costReportByDepartment = (function() {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod"
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/CostReportByDepartment/";


    async function fillGrid() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "Grid";

        const departmentId = dom.filterFormDepartmentIdCombo.GetValue();
        const datePeriodType = dom.filterFormDateCombo.GetValue();
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();

        const apiParam = {
            departmentId: departmentId,
            startDate: persianStartDate,
            endDate: persianEndDate,
            datePeriodType: datePeriodType
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.gridParent.html(gridHtml);
        setDom();
    }

    function handleFilterFormDateComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDateComboError);

        const selectedDateId = dom.filterFormDateCombo.GetValue();

        if (selectedDateId === state.customPeriodId) {
            dom.filterFormDateComboDesign.removeClass("col-md-4").addClass("col-md-3");
            dom.filterFormDepartmentIdComboDesign.removeClass("col-md-4").addClass("col-md-3");

            tools.showItem(dom.filterFormPeriodStartDatePickerParent);
            tools.showItem(dom.filterFormPeriodEndDatePickerParent);

        } else {
            dom.filterFormDateComboDesign.removeClass("col-md-3").addClass("col-md-4");
            dom.filterFormDepartmentIdComboDesign.removeClass("col-md-3").addClass("col-md-4");

            tools.hideItem(dom.filterFormPeriodStartDatePickerParent);
            tools.hideItem(dom.filterFormPeriodEndDatePickerParent);

            dom.filterFormPeriodStartDatePicker.val("");
            dom.filterFormPeriodEndDatePicker.val("");
        }
    }

    function handleFilterFormDepartmentIdComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDepartmentIdComboError);
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
                "?departmentId=" +
                -1 +
                "&startDate=" +
                "" +
                "&endDate=" +
                "" +
                "&datePeriodType=" +
                7;
            return;
        }
            

        var departmentId = +dom.filterFormDepartmentIdCombo.GetValue() > -1
            ? dom.filterFormDepartmentIdCombo.GetValue()
            : -1;

        var dateTypeId = dom.filterFormDateCombo.GetValue();
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();

        if (dateTypeId == null ||
        (dateTypeId === state.customPeriodId &&
            (tools.isNullOrEmpty(persianStartDate) || tools.isNullOrEmpty(persianEndDate)))) {
            departmentId = -1;
            dateTypeId = -1;
        }

        command.callbackUrl =
            controllerName +
            "Grid" +
            "?departmentId=" +
            departmentId +
            "&startDate=" +
            persianStartDate +
            "&endDate=" +
            persianEndDate +
            "&datePeriodType=" +
            dateTypeId;
    }

    function init() {
        setDom();
        setEvents();
    }

    function isFilterFormValid(shouldDisplayValidationErrors = true) {
        var isValid = true;

        tools.hideItem(dom.filterFormDepartmentIdComboError);
        const departmentId = dom.filterFormDepartmentIdCombo.GetValue();
        const isDepartmentIdSelected = !tools.isNullOrEmpty(departmentId);
        if (!isDepartmentIdSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormDepartmentIdComboError);
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
        //FilterForm
        dom.filterFormDepartmentIdCombo = ASPxClientComboBox.Cast("filterFormDepartmentIdCombo");
        dom.filterFormDepartmentIdComboParent = $("#filterFormDepartmentIdComboParent");
        dom.filterFormDepartmentIdComboError = $("#filterFormDepartmentIdComboError");
        dom.filterFormDepartmentIdComboDesign = $("#filterFormDepartmentIdComboDesign");

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

        //Grid
        dom.gridParent = $("#gridParent");
        dom.grid = ASPxClientGridView.Cast("grid");
    }

    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);

        dom.filterFormDateCombo.SetEnabled(false);

        tools.hideItem(dom.filterFormPeriodStartDatePickerParent);
        tools.hideItem(dom.filterFormPeriodEndDatePickerParent);
    }

    $(document).ready(init);

    return {
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormDepartmentIdComboSelectedIndexChange: handleFilterFormDepartmentIdComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleGridBeginCallback: handleGridBeginCallback
    };
})();