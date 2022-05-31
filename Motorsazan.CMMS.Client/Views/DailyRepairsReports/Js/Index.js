/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.dailyRepairsReports = (function() {

    var dom = {};
    var state = {
        specialDateTypeFilter: "CustomPeriod"
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/DailyRepairsReports/";

    async function fillGrid() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "FillResultGrid";

        const statusId = dom.filterFormStatusTypeCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const apiParam = {
            workOrderStatusTypeId: statusId,
            persianEndDate: persianEndDate,
            persianStartDate: persianStartDate,
            datePeriodType: datePeriodType
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.resultGridParent.html(gridHtml);
        setDom();
    }

    function handleFilterFormDateTypeComboSelectedIndexChanged() {
        tools.hideItem(dom.filterFormDateTypeComboError);

        const activeDateType = dom.filterFormDateTypeCombo.GetValue();
        const isSpecialTypeSelected = activeDateType === state.specialDateTypeFilter;

        if (isSpecialTypeSelected) {
            tools.showItem(dom.filterFormSpecialDateColumn);
            dom.filterFormStatusTypeComboParent.removeClass("col-md-4").addClass("col-md-3");
            dom.filterFormDateTypeComboParent.removeClass("col-md-4").addClass("col-md-3");
        } else {
            tools.hideItem(dom.filterFormSpecialDateColumn);
            dom.filterFormStartDate.val("");
            dom.filterFormEndDate.val("");
            dom.filterFormStatusTypeComboParent.removeClass("col-md-3").addClass("col-md-4");
            dom.filterFormDateTypeComboParent.removeClass("col-md-3").addClass("col-md-4");
        }
    }

    function handleFilterFormEndDateSelectionChange() {
        tools.hideItem(dom.filterFormEndDateError);
    }

    function handleFilterFormSearchBtnClick() {
        fillGrid();
    }

    function handleFilterFormStartDateSelectionChange() {
        tools.hideItem(dom.filterFormStartDateError);
    }

    function handleFilterFormStatusTypeComboSelectedIndexChanged() {
        tools.hideItem(dom.filterFormStatusTypeComboError);
        const statusType = dom.filterFormStatusTypeCombo.GetValue();
        const isStatusTypeHasValue = !tools.isNullOrEmpty(statusType);
        if (!isStatusTypeHasValue) {
            tools.showItem(dom.filterFormStatusTypeComboError);
        }

        dom.filterFormDateTypeCombo.SetEnabled(true);
    }

    function handleResultGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            const url = controllerName +
                "FillResultGrid" +
                "?workOrderStatusTypeId=" +
                -1 +
                "&datePeriodType=" +
                7 +
                "&persianStartDate=" +
                "" +
                "&persianEndDate=" +
                "";
            command.callbackUrl = url;

            return;
        }

        const workOrderStatusTypeId = dom.filterFormStatusTypeCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const url = controllerName +
            "FillResultGrid" +
            "?workOrderStatusTypeId=" +
            workOrderStatusTypeId +
            "&datePeriodType=" +
            datePeriodType +
            "&persianStartDate=" +
            persianStartDate +
            "&persianEndDate=" +
            persianEndDate;
        command.callbackUrl = url;
    }

    function init() {
        setDom();
        setEvents();

        dom.filterFormStartDate.on("change", handleFilterFormStartDateSelectionChange);
        dom.filterFormEndDate.on("change", handleFilterFormEndDateSelectionChange);
    }

    function isFilterFormValid() {
        var isValid = true;

        tools.hideItem(dom.filterFormStatusTypeComboError);
        const statusType = dom.filterFormStatusTypeCombo.GetValue();
        const statusTypeHasValue = !tools.isNullOrEmpty(statusType);
        if (!statusTypeHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormStatusTypeComboError);
        }

        tools.hideItem(dom.filterFormDateTypeComboError);
        const dateType = dom.filterFormDateTypeCombo.GetValue();
        const dateTypeHasValue = !tools.isNullOrEmpty(dateType);
        if (!dateTypeHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormDateTypeComboError);
        }

        if (dateType === state.specialDateTypeFilter) {
            tools.hideItem(dom.filterFormStartDateError);
            const startDate = dom.filterFormStartDate.val();
            const isStartDateValid = tools.isValidPersianDate(startDate);
            if (!isStartDateValid) {
                isValid = false;
                tools.showItem(dom.filterFormStartDateError);
            }
            tools.hideItem(dom.filterFormEndDateError);
            const endDate = dom.filterFormEndDate.val();
            const isEndDateValid = tools.isValidPersianDate(endDate);
            if (!isEndDateValid) {
                isValid = false;
                tools.showItem(dom.filterFormEndDateError);
            }
        }

        return isValid;
    }

    function setDom() {
        //FilterForm
        dom.filterFormStatusTypeComboParent = $("#filterFormStatusTypeComboParent");
        dom.filterFormStatusTypeComboError = $("#filterFormStatusTypeComboError");
        dom.filterFormStatusTypeCombo = ASPxClientComboBox.Cast("filterFormStatusTypeCombo");

        dom.filterFormDateTypeComboParent = $("#filterFormDateTypeComboParent");
        dom.filterFormDateTypeComboError = $("#filterFormDateTypeComboError");
        dom.filterFormDateTypeCombo = ASPxClientComboBox.Cast("filterFormDateTypeCombo");

        dom.filterFormSpecialDateColumn = $("#filterFormSpecialDateColumn");

        dom.filterFormStartDateError = $("#filterFormStartDateError");
        dom.filterFormStartDate = $("#filterFormStartDate");

        dom.filterFormEndDateError = $("#filterFormEndDateError");
        dom.filterFormEndDate = $("#filterFormEndDate");

        //Grid
        dom.resultGridParent = $("#resultGridParent");
        dom.resultGrid = ASPxClientGridView.Cast("resultGrid");
    }

    function setEvents() {
        dom.filterFormDateTypeCombo.SetEnabled(false);

        tools.hideItem(dom.filterFormSpecialDateColumn);
    }

    $(document).ready(init);

    return {
        handleFilterFormDateTypeComboSelectedIndexChanged: handleFilterFormDateTypeComboSelectedIndexChanged,
        handleFilterFormSearchBtnClick: handleFilterFormSearchBtnClick,
        handleFilterFormStatusTypeComboSelectedIndexChanged: handleFilterFormStatusTypeComboSelectedIndexChanged,
        handleResultGridBeginCallback: handleResultGridBeginCallback
    };
})();