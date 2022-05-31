/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.warehousedailyreports = (function () {

    var dom = {};
    var state = {
        datePeriodComboIndex: 1,
        workOrderWithoutNewReceiptComboIndex: 2
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/WarehouseDailyReports/";


    async function fillGrid() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "FillResultGrid";
        const selectedComboId = dom.filterFormReportsTypeCombo.GetValue();

        var reportFilterType;
        if (selectedComboId === state.datePeriodComboIndex) {
            reportFilterType = 1;
        }
        else {
            reportFilterType = 2;
        }

        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();

        const apiParam = {
            ReportType: reportFilterType,
            persianStartDate,
            persianEndDate
        };
        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.ResultGridParent.html(gridHtml);

        setDom();
    }

    function handleFilterFormReportsTypeComboChange() {
        tools.hideItem(dom.filterFormReportsTypeComboError);
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        const selectedComboId = dom.filterFormReportsTypeCombo.GetValue();

        if (selectedComboId === state.datePeriodComboIndex) {
            tools.showItem(dom.filterFormPeriodStartDatePickerParent);
            tools.showItem(dom.filterFormPeriodEndDatePickerParent);
        } else {
            tools.hideItem(dom.filterFormPeriodStartDatePickerParent);
            tools.hideItem(dom.filterFormPeriodEndDatePickerParent);
            dom.filterFormPeriodStartDatePicker.val("");
            dom.filterFormPeriodEndDatePicker.val("");
        }
    }


    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
    }

    function handleFilterFormSearchBtnClick() {
        fillGrid();
    }

    function handleResultGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            const url = controllerName +
                "FillResultGrid" +
                "?ReportType=" +
                -1 +
                "&persianStartDate=" +
                "" +
                "&persianEndDate=" +
                "";

            command.callbackUrl = url;
            return;
        }

        const selectedComboId = dom.filterFormReportsTypeCombo.GetValue();

        var reportFilterType;
        if (selectedComboId === state.datePeriodComboIndex) {
            reportFilterType = 1;
        }
        else {
            reportFilterType = 2;
        }

        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();

        const url = controllerName +
            "FillResultGrid" +
            "?ReportType=" +
            reportFilterType +
            "&persianStartDate=" +
            persianStartDate +
            "&persianEndDate=" +
            persianEndDate;

        command.callbackUrl = url;
    }

    function init() {
        setDom();
        setEvents();
    }

    function isFilterFormValid() {
        var isValid = true;

        tools.hideItem(dom.filterFormReportsTypeComboError);
        const reportsType = dom.filterFormReportsTypeCombo.GetValue();
        const reportsTypeHasValue = !tools.isNullOrEmpty(reportsType);
        if (!reportsTypeHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormReportsTypeComboError);
        }

        if (reportsType === state.datePeriodComboIndex) {
            tools.hideItem(dom.filterFormPeriodStartDatePickerError);
            const startDate = dom.filterFormPeriodStartDatePicker.val();
            const isStartDateValid = tools.isValidPersianDate(startDate);
            if (!isStartDateValid) {
                isValid = false;
                tools.showItem(dom.filterFormPeriodStartDatePickerError);
            }
            tools.hideItem(dom.filterFormPeriodEndDatePickerError);
            const endDate = dom.filterFormPeriodEndDatePicker.val();
            const isEndDateValid = tools.isValidPersianDate(endDate);
            if (!isEndDateValid) {
                isValid = false;
                tools.showItem(dom.filterFormPeriodEndDatePickerError);
            }
        }

        return isValid;
    }

    function setDom() {
        //FilterForm
        dom.filterFormReportsTypeComboParent = $("#filterFormReportsTypeComboParent");
        dom.filterFormReportsTypeComboError = $("#filterFormReportsTypeComboError");
        dom.filterFormReportsTypeCombo = ASPxClientComboBox.Cast("filterFormReportsTypeCombo");

        dom.filterFormPeriodStartDatePickerParent = $("#filterFormPeriodStartDatePickerParent");
        dom.filterFormPeriodStartDatePickerError = $("#filterFormPeriodStartDatePickerError");
        dom.filterFormPeriodStartDatePicker = $("#filterFormPeriodStartDatePicker");

        dom.filterFormPeriodEndDatePickerParent = $("#filterFormPeriodEndDatePickerParent");
        dom.filterFormPeriodEndDatePickerError = $("#filterFormPeriodEndDatePickerError");
        dom.filterFormPeriodEndDatePicker = $("#filterFormPeriodEndDatePicker");

        //Grid
        dom.ResultGridParent = $("#ResultGridParent");
        dom.ResultGrid = ASPxClientGridView.Cast("ResultGrid");
    }

    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);
    }

    $(document).ready(init);

    return {
        handleFilterFormReportsTypeComboChange: handleFilterFormReportsTypeComboChange,
        handleFilterFormPeriodEndDatePickerSelectionChange: handleFilterFormPeriodEndDatePickerSelectionChange,
        handleFilterFormPeriodStartDatePickerSelectionChange: handleFilterFormPeriodStartDatePickerSelectionChange,
        handleFilterFormSearchBtnClick: handleFilterFormSearchBtnClick,
        handleResultGridBeginCallback: handleResultGridBeginCallback,
        dom
    };
})();