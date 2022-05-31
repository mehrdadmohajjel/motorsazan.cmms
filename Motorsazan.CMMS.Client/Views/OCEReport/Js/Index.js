/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.oCEReport = (function() {

    var dom = {};
    var state = {
        specialDateTypeFilter: "CustomPeriod"
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/OCEReport/";

    async function fillGrid() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "ResultGrid";

        const maintenanceGroupId = dom.filterFormGetMaintenanceGroupListCombo.GetValue();
        const maintenanceGroupMemberId = dom.filterFormGetMaintenanceGroupMemberListCombo.GetValue();

        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const apiParam = {
            maintenanceGroupId: maintenanceGroupId,
            maintenanceGroupMemberId: maintenanceGroupMemberId,
            datePeriodType: datePeriodType,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate
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

            tools.hideItem(dom.filterFormStartDateError);
            tools.hideItem(dom.filterFormEndDateError);

            dom.filterFormGetMaintenanceGroupListComboParentDesign.addClass("col-md-6");
            dom.filterFormGetMaintenanceGroupListComboParentDesign.removeClass("col-md-3");

            dom.filterFormGetMaintenanceGroupMemberListComboParentDesign.addClass("col-md-6");
            dom.filterFormGetMaintenanceGroupMemberListComboParentDesign.removeClass("col-md-3");

            dom.filterFormDateTypeComboParentDesign.addClass("col-md-4");
            dom.filterFormDateTypeComboParentDesign.removeClass("col-md-3");

        } else {
            tools.hideItem(dom.filterFormSpecialDateColumn);
            dom.filterFormStartDate.val("");
            dom.filterFormEndDate.val("");

            dom.filterFormGetMaintenanceGroupListComboParentDesign.addClass("col-md-3");
            dom.filterFormGetMaintenanceGroupListComboParentDesign.removeClass("col-md-6");

            dom.filterFormGetMaintenanceGroupMemberListComboParentDesign.addClass("col-md-3");
            dom.filterFormGetMaintenanceGroupMemberListComboParentDesign.removeClass("col-md-6");

            dom.filterFormDateTypeComboParentDesign.addClass("col-md-3");
            dom.filterFormDateTypeComboParentDesign.removeClass("col-md-4");
        }
    }

    async function handleFilterFormGetMaintenanceGroupListComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormGetMaintenanceGroupListComboError);

        const maintenanceGroupId = dom.filterFormGetMaintenanceGroupListCombo.GetValue();

        if (maintenanceGroupId === 0)
            dom.filterFormGetMaintenanceGroupListCombo.SetText("همه موارد");

        const url = controllerName + "FilterFormGetMaintenanceGroupMemberList";

        const apiParam = {
            maintenanceGroupId: maintenanceGroupId
        };

        const maintenanceGroupMemberListCombo = await motorsazanClient.connector.post(url, apiParam);

        dom.filterFormGetMaintenanceGroupMemberListComboParent.html(maintenanceGroupMemberListCombo);

        setDom();

        dom.filterFormDateTypeCombo.SetSelectedIndex(-1);

        dom.filterFormGetMaintenanceGroupMemberListCombo.SetEnabled(true);

        tools.hideItem(dom.filterFormSpecialDateColumn);

        dom.filterFormStartDate.val("");
        dom.filterFormEndDate.val("");

        dom.filterFormGetMaintenanceGroupListComboParentDesign.addClass("col-md-3");
        dom.filterFormGetMaintenanceGroupListComboParentDesign.removeClass("col-md-6");

        dom.filterFormGetMaintenanceGroupMemberListComboParentDesign.addClass("col-md-3");
        dom.filterFormGetMaintenanceGroupMemberListComboParentDesign.removeClass("col-md-6");

        dom.filterFormDateTypeComboParentDesign.addClass("col-md-3");
        dom.filterFormDateTypeComboParentDesign.removeClass("col-md-4");

        dom.filterFormDateTypeCombo.SetEnabled(false);
    }

    function handleFilterFormGetMaintenanceGroupMemberListComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormGetMaintenanceGroupMemberListComboError);

        dom.filterFormDateTypeCombo.SetSelectedIndex(-1);

        if (dom.filterFormGetMaintenanceGroupMemberListCombo.GetValue() === 0)
            dom.filterFormGetMaintenanceGroupMemberListCombo.SetText("همه موارد");

        tools.hideItem(dom.filterFormSpecialDateColumn);

        dom.filterFormStartDate.val("");
        dom.filterFormEndDate.val("");

        dom.filterFormGetMaintenanceGroupListComboParentDesign.addClass("col-md-3");
        dom.filterFormGetMaintenanceGroupListComboParentDesign.removeClass("col-md-6");

        dom.filterFormGetMaintenanceGroupMemberListComboParentDesign.addClass("col-md-3");
        dom.filterFormGetMaintenanceGroupMemberListComboParentDesign.removeClass("col-md-6");

        dom.filterFormDateTypeComboParentDesign.addClass("col-md-3");
        dom.filterFormDateTypeComboParentDesign.removeClass("col-md-4");

        dom.filterFormDateTypeCombo.SetEnabled(true);
    }

    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormEndDateError);
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormStartDateError);
    }

    function handleFilterFormSearchBtnClick() {
        if (!isFilterFormValid()) return;

        fillGrid();
    }

    function handleResultGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            const url = controllerName +
                "ResultGrid" +
                "?maintenanceGroupId=" +
                -1 +
                "&maintenanceGroupMemberId=" +
                -1 +
                "&datePeriodType=" +
                -1 +
                "&persianStartDate=" +
                "" +
                "&persianEndDate=" +
                "";
            command.callbackUrl = url;
            return;
        }

        const maintenanceGroupId = dom.filterFormGetMaintenanceGroupListCombo.GetValue();
        const maintenanceGroupMemberId = dom.filterFormGetMaintenanceGroupMemberListCombo.GetValue();

        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const url = controllerName +
            "ResultGrid" +
            "?maintenanceGroupId=" +
            maintenanceGroupId +
            "&maintenanceGroupMemberId=" +
            maintenanceGroupMemberId +
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

        dom.filterFormStartDate.on("change", handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormEndDate.on("change", handleFilterFormPeriodEndDatePickerSelectionChange);
    }

    function isFilterFormValid() {
        var isValid = true;

        tools.hideItem(dom.filterFormGetMaintenanceGroupListComboError);
        const maintenanceGroupListSelect = dom.filterFormGetMaintenanceGroupListCombo.GetValue();
        const maintenanceGroupListSelectHasValue = !tools.isNullOrEmpty(maintenanceGroupListSelect);
        if (!maintenanceGroupListSelectHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormGetMaintenanceGroupListComboError);
        }

        tools.hideItem(dom.filterFormGetMaintenanceGroupMemberListComboError);
        const maintenanceGroupMemberListSelect = dom.filterFormGetMaintenanceGroupMemberListCombo.GetValue();
        const maintenanceGroupMemberListSelectHasValue = !tools.isNullOrEmpty(maintenanceGroupMemberListSelect);
        if (!maintenanceGroupMemberListSelectHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormGetMaintenanceGroupMemberListComboError);
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
        //Filter form
        dom.filterFormGetMaintenanceGroupListComboParentDesign =
            $("#filterFormGetMaintenanceGroupListComboParentDesign");
        dom.filterFormGetMaintenanceGroupListComboParent = $("#filterFormGetMaintenanceGroupListComboParent");
        dom.filterFormGetMaintenanceGroupListComboError = $("#filterFormGetMaintenanceGroupListComboError");
        dom.filterFormGetMaintenanceGroupListCombo = ASPxClientComboBox.Cast("filterFormGetMaintenanceGroupListCombo");

        dom.filterFormGetMaintenanceGroupMemberListComboParentDesign =
            $("#filterFormGetMaintenanceGroupMemberListComboParentDesign");
        dom.filterFormGetMaintenanceGroupMemberListComboParent =
            $("#filterFormGetMaintenanceGroupMemberListComboParent");
        dom.filterFormGetMaintenanceGroupMemberListComboError = $("#filterFormGetMaintenanceGroupMemberListComboError");
        dom.filterFormGetMaintenanceGroupMemberListCombo =
            ASPxClientComboBox.Cast("filterFormGetMaintenanceGroupMemberListCombo");

        dom.filterFormDateTypeComboParentDesign = $("#filterFormDateTypeComboParentDesign");
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
        dom.filterFormGetMaintenanceGroupMemberListCombo.SetEnabled(false);
        dom.filterFormDateTypeCombo.SetEnabled(false);
    }

    $(document).ready(init);

    return {
        handleFilterFormDateTypeComboSelectedIndexChanged: handleFilterFormDateTypeComboSelectedIndexChanged,
        handleFilterFormGetMaintenanceGroupListComboSelectedIndexChange:
            handleFilterFormGetMaintenanceGroupListComboSelectedIndexChange,
        handleFilterFormGetMaintenanceGroupMemberListComboSelectedIndexChange:
            handleFilterFormGetMaintenanceGroupMemberListComboSelectedIndexChange,
        handleFilterFormPeriodEndDatePickerSelectionChange: handleFilterFormPeriodEndDatePickerSelectionChange,
        handleFilterFormPeriodStartDatePickerSelectionChange: handleFilterFormPeriodStartDatePickerSelectionChange,
        handleFilterFormSearchBtnClick: handleFilterFormSearchBtnClick,
        handleResultGridBeginCallback: handleResultGridBeginCallback
    };
})();