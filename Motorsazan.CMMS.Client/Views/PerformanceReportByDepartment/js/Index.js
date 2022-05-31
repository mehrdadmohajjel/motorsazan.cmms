/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.performanceReportByDepartment = (function () {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod",
        departmentId: null,
        subDepartmentId: null
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/PerformanceReportByDepartment/";

    async function fillGrid() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "Grid";

        var departmentId = dom.filterFormDepartmentIdCombo.GetValue();
        const datePeriodType = dom.filterFormDateCombo.GetValue();
        const periodStartDate = dom.filterFormPeriodStartDatePicker.val();
        const periodEndDate = dom.filterFormPeriodEndDatePicker.val();

        if (departmentId != 0) {
            departmentId = dom.filterFormSubDepartmentIdCombo.GetValue();
        }

        const apiParam = {
            departmentId: departmentId,
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

        const selectedDateId = dom.filterFormDateCombo.GetValue();

        if (selectedDateId === state.customPeriodId) {
            tools.enableItem(dom.filterFormPeriodStartDatePicker);
            tools.enableItem(dom.filterFormPeriodEndDatePicker);
        } else {
            tools.disableItem(dom.filterFormPeriodStartDatePicker);
            tools.disableItem(dom.filterFormPeriodEndDatePicker);
            dom.filterFormPeriodStartDatePicker.val("");
            dom.filterFormPeriodEndDatePicker.val("");
        }
    }

    function handleFilterFormDepartmentIdComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDepartmentIdComboError);
        const url = controllerName + "FilterFormSubDepartmentListByMainDepartmentId";

        const departmentId = dom.filterFormDepartmentIdCombo.GetValue();

        state.departmentId = departmentId;

        tools.disableItem(dom.filterFormPeriodStartDatePicker);
        tools.disableItem(dom.filterFormPeriodEndDatePicker);
        dom.filterFormDateCombo.SetSelectedIndex(-1);
        dom.filterFormPeriodStartDatePicker.val("");
        dom.filterFormPeriodEndDatePicker.val("");

        if (departmentId == 0) {
            setDom();

            tools.hideItem(dom.filterFormSubDepartmentIdComboDesign);
            dom.filterFormDateCombo.SetEnabled(true);

            dom.filterFormDepartmentIdComboDesign.removeClass("col-md-6").addClass("col-md-3");
            dom.filterFormSubDepartmentIdComboDesign.removeClass("col-md-6").addClass("col-md-3");
            dom.filterFormDateComboDesign.removeClass("col-md-4").addClass("col-md-3");

            dom.filterFormPeriodStartDatePickerParent.removeClass("col-md-3").addClass("col-md-2");
            dom.filterFormPeriodEndDatePickerParent.removeClass("col-md-3").addClass("col-md-2");
        }
        else {
            tools.showItem(dom.filterFormSubDepartmentIdComboDesign);

            const apiParam = {
                departmentId: departmentId
            };

            motorsazanClient.connector.post(url, apiParam)
                .then(function (apiResult) {
                    dom.filterFormSubDepartmentIdComboParent.html(apiResult);
                    setDom();

                    dom.filterFormDateCombo.SetEnabled(false);
                    dom.filterFormSubDepartmentIdCombo.SetEnabled(true);

                    dom.filterFormDepartmentIdComboDesign.removeClass("col-md-3").addClass("col-md-6");
                    dom.filterFormSubDepartmentIdComboDesign.removeClass("col-md-3").addClass("col-md-6");
                    dom.filterFormDateComboDesign.removeClass("col-md-3").addClass("col-md-4");

                    dom.filterFormPeriodStartDatePickerParent.removeClass("col-md-2").addClass("col-md-3");
                    dom.filterFormPeriodEndDatePickerParent.removeClass("col-md-2").addClass("col-md-3");
                });
        }
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

    function handleFilterFormSubDepartmentIdComboBeginCallback(command) {

        const departmentId = dom.filterFormDepartmentIdCombo.GetValue();
        const isDepartmentIdSelected = !tools.isNullOrEmpty(departmentId);
        if (isDepartmentIdSelected) {
            command.callbackUrl =
                controllerName +
                "FilterFormSubDepartmentListByMainDepartmentId" +
                "?DepartmentId=" +
                departmentId;
        }
    }

    function handleFilterFormSubDepartmentIdComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormSubDepartmentIdComboError);
        state.subDepartmentId = dom.filterFormSubDepartmentIdCombo.GetValue();

        dom.filterFormDateCombo.SetEnabled(true);
    }

    function handleGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            command.callbackUrl =
                controllerName + "Grid"
                + "?departmentId=" + -1
                + "&startDate=" + ""
                + "&endDate=" + ""
                + "&datePeriodType=" + -1;
            return;
        }

        var departmentId =
            +dom.filterFormDepartmentIdCombo.GetValue() > -1
                ? dom.filterFormDepartmentIdCombo.GetValue()
                : -1;

        if (departmentId != 0) {
            departmentId =
                +dom.filterFormSubDepartmentIdCombo.GetValue() > -1
                    ? dom.filterFormSubDepartmentIdCombo.GetValue()
                    : -1;
        }


        var dateTypeId = dom.filterFormDateCombo.GetValue();
        const periodStartDate = dom.filterFormPeriodStartDatePicker.val();
        const periodEndDate = dom.filterFormPeriodEndDatePicker.val();

        if (dateTypeId == null ||
            (dateTypeId === state.customPeriodId &&
                (tools.isNullOrEmpty(periodStartDate) || tools.isNullOrEmpty(periodEndDate)))) {
            departmentId = -1;
            subDepartmentId = -1;
            dateTypeId = -1;
        }

        command.callbackUrl =
            controllerName + "Grid"
            + "?departmentId=" + departmentId
            + "&startDate=" + periodStartDate
            + "&endDate=" + periodEndDate
            + "&datePeriodType=" + dateTypeId;
    }

    function init() {
        setDom();
        setEvents();

        dom.filterFormSubDepartmentIdCombo.SetEnabled(false);
        dom.filterFormDateCombo.SetEnabled(false);
        tools.disableItem(dom.filterFormPeriodStartDatePicker);
        tools.disableItem(dom.filterFormPeriodEndDatePicker);
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

        if (state.departmentId != 0) {
            tools.hideItem(dom.filterFormSubDepartmentIdComboError);
            const subDepartmentId = dom.filterFormSubDepartmentIdCombo.GetValue();
            const isSubDepartmentIdSelected = !tools.isNullOrEmpty(subDepartmentId);
            if (!isSubDepartmentIdSelected) {
                isValid = false;
                shouldDisplayValidationErrors && tools.showItem(dom.filterFormSubDepartmentIdComboError);
            }
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


        dom.filterFormSubDepartmentIdCombo = ASPxClientComboBox.Cast("filterFormSubDepartmentIdCombo");
        dom.filterFormSubDepartmentIdComboParent = $("#filterFormSubDepartmentIdComboParent");
        dom.filterFormSubDepartmentIdComboError = $("#filterFormSubDepartmentIdComboError");
        dom.filterFormSubDepartmentIdComboDesign = $("#filterFormSubDepartmentIdComboDesign");

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
    }

    $(document).ready(init);

    return {
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormDepartmentIdComboSelectedIndexChange: handleFilterFormDepartmentIdComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleFilterFormSubDepartmentIdComboBeginCallback: handleFilterFormSubDepartmentIdComboBeginCallback,
        handleFilterFormSubDepartmentIdComboSelectedIndexChange: handleFilterFormSubDepartmentIdComboSelectedIndexChange,
        handleGridBeginCallback: handleGridBeginCallback,
        dom
    };

})();
