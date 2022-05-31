/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.allworkorderstatusreport = (function() {

    var dom = {};
    var state = {
        specialDateTypeFilter: "CustomPeriod"
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/AllWorkOrderStatusReport/";

    async function fillGrid() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "FillResultGrid";

        const maintenanceGroupType = dom.filterFormMaintenanceGroupTypeCombo.GetValue();
        const workOrderType = dom.filterFormWorkOrderTypeCombo.GetValue();
        const stopType = dom.filterFormStopTypeCombo.GetValue();
        const maintenanceGroup = dom.filterFormMaintenanceGroupCombo.GetValue();
        const workOrderStatusType = dom.filterFormWorkOrderStatusTypeCombo.GetValue();
        const departmentList = dom.filterFormDepartmentListCombo.GetValue();

        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const apiParam = {
            maintenanceGroupType: maintenanceGroupType,
            workOrderType: workOrderType,
            stopType: stopType,
            maintenanceGroup: maintenanceGroup,
            workOrderStatusType: workOrderStatusType,
            departmentList: departmentList,
            persianEndDate: persianEndDate,
            persianStartDate: persianStartDate,
            datePeriodType: datePeriodType
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.ResultGridParent.html(gridHtml);
        setDom();
    }

    function handleFilterFormDateTypeComboSelectedIndexChanged() {
        tools.hideItem(dom.filterFormDateTypeComboError);

        const activeDateType = dom.filterFormDateTypeCombo.GetValue();
        const isSpecialTypeSelected = activeDateType === state.specialDateTypeFilter;

        if (isSpecialTypeSelected) {
            tools.showItem(dom.filterFormSpecialDateColumn);
        } else {
            tools.hideItem(dom.filterFormSpecialDateColumn);
            dom.filterFormStartDate.val("");
            dom.filterFormEndDate.val("");
        }
    }

    function handleFilterFormDepartmentListComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDepartmentListComboError);
        const departmentList = dom.filterFormDepartmentListCombo.GetValue();
        const departmentListHasValue = !tools.isNullOrEmpty(departmentList);
        if (!departmentListHasValue) {
            tools.showItem(dom.filterFormDepartmentListComboError);
        }
    }

    function handleFilterFormMaintenanceGroupComboIndexChange() {
        tools.hideItem(dom.filterFormMaintenanceGroupComboError);
        const maintenanceGroup = dom.filterFormMaintenanceGroupCombo.GetValue();
        const maintenanceGroupHasValue = !tools.isNullOrEmpty(maintenanceGroup);
        if (!maintenanceGroupHasValue) {
            tools.showItem(dom.filterFormMaintenanceGroupComboError);
        }
    }

    function handleFilterFormMaintenanceGroupTypeComboSelectedIndexChanged() {
        tools.hideItem(dom.filterFormMaintenanceGroupTypeComboError);
        const maintenanceGroupType = dom.filterFormMaintenanceGroupTypeCombo.GetValue();
        const maintenanceGroupTypeHasValue = !tools.isNullOrEmpty(maintenanceGroupType);
        if (!maintenanceGroupTypeHasValue) {
            tools.showItem(dom.filterFormMaintenanceGroupTypeComboError);
        }
    }

    function handleFilterFormSearchBtnClick() {
        fillGrid();
    }

    function handleFilterFormStopTypeComboIndexChange() {
        tools.hideItem(dom.filterFormStopTypeComboError);
        const stopType = dom.filterFormStopTypeCombo.GetValue();
        const stopTypeHasValue = !tools.isNullOrEmpty(stopType);
        if (!stopTypeHasValue) {
            tools.showItem(dom.filterFormStopTypeComboError);
        }
    }

    function handleFilterFormWorkOrderStatusComboIndexChange() {
        tools.hideItem(dom.filterFormWorkOrderStatusTypeComboError);
        const workOrderStatusType = dom.filterFormWorkOrderStatusTypeCombo.GetValue();
        const workOrderStatusTypeHasValue = !tools.isNullOrEmpty(workOrderStatusType);
        if (!workOrderStatusTypeHasValue) {
            tools.showItem(dom.filterFormWorkOrderStatusTypeComboError);
        }
    }

    function handleFilterFormWorkOrderTypeComboSelectedIndexChanged() {
        tools.hideItem(dom.filterFormWorkOrderTypeComboError);
        const workOrderType = dom.filterFormWorkOrderTypeCombo.GetValue();
        const workOrderTypeHasValue = !tools.isNullOrEmpty(workOrderType);
        if (!workOrderTypeHasValue) {
            tools.showItem(dom.filterFormWorkOrderTypeComboError);
        }
    }

    function handleResultGridBeginCallback(command) {
        if (!isFilterFormValid())
            return;

        const maintenanceGroupType = dom.filterFormMaintenanceGroupTypeCombo.GetValue();
        const workOrderType = dom.filterFormWorkOrderTypeCombo.GetValue();
        const stopType = dom.filterFormStopTypeCombo.GetValue();
        const maintenanceGroup = dom.filterFormMaintenanceGroupCombo.GetValue();
        const workOrderStatusType = dom.filterFormWorkOrderStatusTypeCombo.GetValue();
        const departmentList = dom.filterFormDepartmentListCombo.GetValue();

        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const url = controllerName +
            "FillResultGrid" +
            "?maintenanceGroupType=" + maintenanceGroupType +
            "&workOrderType=" + workOrderType +
            "&stopType=" + stopType +
            "&maintenanceGroup=" + maintenanceGroup +
            "&workOrderStatusType=" + workOrderStatusType +
            "&departmentList=" + departmentList +
            "&datePeriodType=" + datePeriodType +
            "&persianStartDate=" + persianStartDate +
            "&persianEndDate=" + persianEndDate;
        command.callbackUrl = url;
    }

    function init() {
        setDom();
        setEvents();
    }

    function isFilterFormValid() {
        var isValid = true;

        tools.hideItem(dom.filterFormMaintenanceGroupTypeComboError);
        const maintenanceGroupType = dom.filterFormMaintenanceGroupTypeCombo.GetValue();
        const maintenanceGroupTypeHasValue = !tools.isNullOrEmpty(maintenanceGroupType);
        if (!maintenanceGroupTypeHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormMaintenanceGroupTypeComboError);
        }

        tools.hideItem(dom.filterFormWorkOrderTypeComboError);
        const workOrderType = dom.filterFormWorkOrderTypeCombo.GetValue();
        const workOrderTypeHasValue = !tools.isNullOrEmpty(workOrderType);
        if (!workOrderTypeHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormWorkOrderTypeComboError);
        }

        tools.hideItem(dom.filterFormStopTypeComboError);
        const stopType = dom.filterFormStopTypeCombo.GetValue();
        const stopTypeHasValue = !tools.isNullOrEmpty(stopType);
        if (!stopTypeHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormStopTypeComboError);
        }

        tools.hideItem(dom.filterFormMaintenanceGroupComboError);
        const maintenanceGroup = dom.filterFormMaintenanceGroupCombo.GetValue();
        const maintenanceGroupHasValue = !tools.isNullOrEmpty(maintenanceGroup);
        if (!maintenanceGroupHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormMaintenanceGroupComboError);
        }

        tools.hideItem(dom.filterFormWorkOrderStatusTypeComboError);
        const workOrderStatusType = dom.filterFormWorkOrderStatusTypeCombo.GetValue();
        const workOrderStatusTypeHasValue = !tools.isNullOrEmpty(workOrderStatusType);
        if (!workOrderStatusTypeHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormWorkOrderStatusTypeComboError);
        }

        tools.hideItem(dom.filterFormDepartmentListComboError);
        const departmentList = dom.filterFormDepartmentListCombo.GetValue();
        const departmentListHasValue = !tools.isNullOrEmpty(departmentList);
        if (!departmentListHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormDepartmentListComboError);
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
        dom.filterFormMaintenanceGroupTypeComboParent = $("#filterFormMaintenanceGroupTypeComboParent");
        dom.filterFormMaintenanceGroupTypeComboError = $("#filterFormMaintenanceGroupTypeComboError");
        dom.filterFormMaintenanceGroupTypeCombo = ASPxClientComboBox.Cast("filterFormMaintenanceGroupTypeCombo");

        dom.filterFormWorkOrderTypeComboParent = $("#filterFormWorkOrderTypeComboParent");
        dom.filterFormWorkOrderTypeComboError = $("#filterFormWorkOrderTypeComboError");
        dom.filterFormWorkOrderTypeCombo = ASPxClientComboBox.Cast("filterFormWorkOrderTypeCombo");

        dom.filterFormStopTypeComboParent = $("#filterFormStopTypeComboParent");
        dom.filterFormStopTypeComboError = $("#filterFormStopTypeComboError");
        dom.filterFormStopTypeCombo = ASPxClientComboBox.Cast("filterFormStopTypeCombo");

        dom.filterFormMaintenanceGroupComboParent = $("#filterFormMaintenanceGroupComboParent");
        dom.filterFormMaintenanceGroupComboError = $("#filterFormMaintenanceGroupComboError");
        dom.filterFormMaintenanceGroupCombo = ASPxClientComboBox.Cast("filterFormMaintenanceGroupCombo");

        dom.filterFormWorkOrderStatusTypeComboParent = $("#filterFormWorkOrderStatusTypeComboParent");
        dom.filterFormWorkOrderStatusTypeComboError = $("#filterFormWorkOrderStatusTypeComboError");
        dom.filterFormWorkOrderStatusTypeCombo = ASPxClientComboBox.Cast("filterFormWorkOrderStatusTypeCombo");

        dom.filterFormDepartmentListComboParent = $("#filterFormDepartmentListComboParent");
        dom.filterFormDepartmentListComboError = $("#filterFormDepartmentListComboError");
        dom.filterFormDepartmentListCombo = ASPxClientComboBox.Cast("filterFormDepartmentListCombo");

        dom.filterFormDateTypeComboParent = $("#filterFormDateTypeComboParent");
        dom.filterFormDateTypeComboError = $("#filterFormDateTypeComboError");
        dom.filterFormDateTypeCombo = ASPxClientComboBox.Cast("filterFormDateTypeCombo");

        dom.filterFormSpecialDateColumn = $("#filterFormSpecialDateColumn");

        dom.filterFormStartDateError = $("#filterFormStartDateError");
        dom.filterFormStartDate = $("#filterFormStartDate");

        dom.filterFormEndDateError = $("#filterFormEndDateError");
        dom.filterFormEndDate = $("#filterFormEndDate");

        //Grid
        dom.ResultGridParent = $("#ResultGridParent");
        dom.ResultGrid = ASPxClientGridView.Cast("ResultGrid");
    }

    function setEvents() {
    }

    $(document).ready(init);

    return {
        handleFilterFormDateTypeComboSelectedIndexChanged: handleFilterFormDateTypeComboSelectedIndexChanged,
        handleFilterFormDepartmentListComboSelectedIndexChange: handleFilterFormDepartmentListComboSelectedIndexChange,
        handleFilterFormMaintenanceGroupComboIndexChange: handleFilterFormMaintenanceGroupComboIndexChange,
        handleFilterFormMaintenanceGroupTypeComboSelectedIndexChanged: handleFilterFormMaintenanceGroupTypeComboSelectedIndexChanged,
        handleFilterFormSearchBtnClick: handleFilterFormSearchBtnClick,
        handleFilterFormStopTypeComboIndexChange: handleFilterFormStopTypeComboIndexChange,
        handleFilterFormWorkOrderStatusComboIndexChange: handleFilterFormWorkOrderStatusComboIndexChange,
        handleFilterFormWorkOrderTypeComboSelectedIndexChanged: handleFilterFormWorkOrderTypeComboSelectedIndexChanged,
        handleResultGridBeginCallback: handleResultGridBeginCallback,
        dom
    };
})();