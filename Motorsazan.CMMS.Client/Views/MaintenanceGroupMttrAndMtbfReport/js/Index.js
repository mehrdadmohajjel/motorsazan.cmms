/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.maintenanceGroupMttrAndMtbfReport = (function() {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod",
        machineId: null,
        workOrderId: null,
        departmentId: null,
        maintenanceGroupId: null,
        timeType: null,
        persianStartDate: "",
        persianEndDate: "",
        datePeriodType: null


    };
    var tools = motorsazanClient.tools;
    var controllerName = "/MaintenanceGroupMttrAndMtbfReport/";


    function faultReportPopUp() {
        const url = controllerName + "ShowGetMttrAndMtbfFaultCodeCountReportPartial";
        const apiParam = {
            machineId: state.machineId,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate
        };
        const title = "گزارش تعداد رخداد کد خطا";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom);
    }

    async function fillGrid() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "Grid";

        const departmentId = dom.filterFormDepartmentIdCombo.GetValue();
        state.departmentId = departmentId;

        const maintenanceGroupId = dom.filterFormMaintenanceGroupCombo.GetValue();
        state.maintenanceGroupId = maintenanceGroupId;

        const timeType = dom.filterFormMttrMtbfTimeTypeCombo.GetValue();
        state.timeType = timeType;

        const datePeriodType = dom.filterFormDateCombo.GetValue();
        state.datePeriodType = datePeriodType;

        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        state.persianStartDate = persianStartDate;

        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();
        state.persianEndDate = persianEndDate;

        const apiParam = {
            departmentId: departmentId,
            maintenanceGroupId: maintenanceGroupId,
            timeType: timeType,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.gridParent.html(gridHtml);
        setDom();
    }

    function handleConsumingMaterialsGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "ConsumingMaterialsGrid" + "?workOrderId=" + state.workOrderId;
    }

    function handleFilterFormDateComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDateComboError);

        const selectedDateId = dom.filterFormDateCombo.GetValue();

        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

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
        dom.filterFormMaintenanceGroupCombo.SetEnabled(true);
    }

    function handleFilterFormMaintenanceGroupComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormMaintenanceGroupComboError);
        dom.filterFormMttrMtbfTimeTypeCombo.SetEnabled(true);
    }

    function handleFilterFormMttrMtbfTimeTypeComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormMttrMtbfTimeTypeComboError);
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

    function handleGetMttrAndMtbfFaultCodeCountGridBeginCallback(command) {
        command.callbackUrl =
            controllerName +
            "ShowGetMttrAndMtbfFaultCodeCountReportPartial" +
            "?machineId=" + state.machineId +
            "&startDate=" + state.persianStartDate +
            "&endDate=" + state.persianEndDate +
            "&datePeriodType=" + state.datePeriodType;
    }

    function handleGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            command.callbackUrl =
                controllerName +
                "Grid" +
                "?departmentId=" + -1 +
                "&maintenanceGroupId=" + -1 +
                "&timeType=" + -1 +
                "&persianStartDate=" + "" +
                "&persianEndDate=" + "" +
                "&datePeriodType=" + -1;
            return;
        }

        const departmentId = dom.filterFormDepartmentIdCombo.GetValue();
        state.departmentId = departmentId;

        const maintenanceGroupId = dom.filterFormMaintenanceGroupCombo.GetValue();
        state.maintenanceGroupId = maintenanceGroupId;

        const timeType = dom.filterFormMttrMtbfTimeTypeCombo.GetValue();
        state.timeType = timeType;

        const datePeriodType = dom.filterFormDateCombo.GetValue();
        state.datePeriodType = datePeriodType;

        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        state.persianStartDate = persianStartDate;

        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();
        state.persianEndDate = persianEndDate;

        command.callbackUrl =
            controllerName +
            "Grid" +
            "?departmentId=" + departmentId +
            "&maintenanceGroupId=" + maintenanceGroupId +
            "&timeType=" + timeType +
            "&persianStartDate=" + persianStartDate +
            "&persianEndDate=" + persianEndDate +
            "&datePeriodType=" + datePeriodType;
    }

    function handleMainGridDetailRowConsumingMaterialsBtnClick() {
        const title = "مشاهده مواد مصرفی";
        const url = controllerName + "ConsumingMaterialsGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
    }

    function handleMainGridDetailRowPerformedOperationsBtnClick() {
        const title = "مشاهده عملیات انجام شده";
        const url = controllerName + "PerformedOperationsGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
    }

    function handleMainGridDetailRowReferralsHistoryBtnClick() {
        const title = "مشاهده تاریخچه ارجاعات";
        const url = controllerName + "ReferralsHistoryGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
    }

    function handleMainGridDetailRowRegistrarInfoBtnClick() {
        const title = "مشخصات ثبت کننده";
        const url = controllerName + "RegistrarInfoModal";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
    }

    function handleMTTRAndMTBFPerformanceReportGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "ShowMTTRAndMTBFPerformanceReportPartial" +
            "?machineId=" + state.machineId +
            "&startDate=" + state.persianStartDate +
            "&endDate=" + state.persianEndDate +
            "&datePeriodType=" + state.datePeriodType;
    }

    function handleMTTRAndMTBFWearhouseGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "ShowMttrAndMtbfWearhouseReport" +
            "?machineId=" + state.machineId +
            "&startDate=" + state.persianStartDate +
            "&endDate=" + state.persianEndDate +
            "&datePeriodType=" + state.datePeriodType;
    }

    function handlePerformanceCostGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "ShowMTTRAndMTBFPerformanceCostReportPartial" +
            "?machineId=" + state.machineId +
            "&startDate=" + state.persianStartDate +
            "&endDate=" + state.persianEndDate +
            "&datePeriodType=" + state.datePeriodType;
    }

    function handlePerformedOperationsGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "PerformedOperationsGrid" + "?workOrderId=" + state.workOrderId;
    }

    function handleReferralsHistoryGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "ReferralsHistoryGrid" + "?workOrderId=" + state.workOrderId;
    }

    function handleWorkOrderMainGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "ShowMttrAndMtbfMachineWorkOrderListPartial" +
            "?machineId=" + state.machineId +
            "&startDate=" + state.persianStartDate +
            "&endDate=" + state.persianEndDate +
            "&datePeriodType=" + state.datePeriodType;
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

        tools.hideItem(dom.filterFormMaintenanceGroupComboError);
        const maintenanceGroupId = dom.filterFormMaintenanceGroupCombo.GetValue();
        const isMaintenanceGroupIdSelected = !tools.isNullOrEmpty(maintenanceGroupId);
        if (!isMaintenanceGroupIdSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormMaintenanceGroupComboError);
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

    function mtbfChartReportPopUp() {
        const url = controllerName + "ShowMtbfChartReportByMachineIdPartial";
        const apiParam = {
            machineId: state.machineId,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate
        };
        const title = "مشاهده نمودار مقایسه ای MTBF";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom, true, false);
    }

    function mttrChartPopup() {
        const url = controllerName + "ShowMttrChartReportByMachineIdPartial";
        const apiParam = {
            machineId: state.machineId,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate
        };
        const title = "مشاهده نمودار مقایسه ای MTTR";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom, true, false);
    }

    function oeeChartReportPopup() {
        const url = controllerName + "ShowOeeChartReportOnMTTRAndMTBFByMachineIdPartial";

        const title = "مشاهده گزارش هزینه کارکرد";
        const apiParam = {
            machineId: state.machineId,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate
        };
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom, true, false);
    }

    function oeeReportPopup() {
        return window.open(
            `/print/PrintOEEReportOnMTTRAndMTBFByMachineId?machineId=${state.machineId}&timeType=${state.timeType}&startDate=${state.persianStartDate}&endDate=${state.persianEndDate}&datePeriodType=${state.datePeriodType}`);
    }

    function setDom() {
        //FilterForm
        dom.filterFormDepartmentIdCombo = ASPxClientComboBox.Cast("filterFormDepartmentIdCombo");
        dom.filterFormDepartmentIdComboParent = $("#filterFormDepartmentIdComboParent");
        dom.filterFormDepartmentIdComboError = $("#filterFormDepartmentIdComboError");
        dom.filterFormDepartmentIdComboDesign = $("#filterFormDepartmentIdComboDesign");

        dom.filterFormMaintenanceGroupCombo = ASPxClientComboBox.Cast("filterFormMaintenanceGroupCombo");
        dom.filterFormMaintenanceGroupComboParent = $("#filterFormMaintenanceGroupComboParent");
        dom.filterFormMaintenanceGroupComboError = $("#filterFormMaintenanceGroupComboError");
        dom.filterFormMaintenanceGroupComboDesign = $("#filterFormMaintenanceGroupComboDesign");

        dom.filterFormMttrMtbfTimeTypeComboParent = $("#filterFormMttrMtbfTimeTypeComboParent");
        dom.filterFormMttrMtbfTimeTypeComboError = $("#filterFormMttrMtbfTimeTypeComboError");
        dom.filterFormMttrMtbfTimeTypeCombo = ASPxClientComboBox.Cast("filterFormMttrMtbfTimeTypeCombo");


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

        dom.filterFormMttrTypeCombo = ASPxClientComboBox.Cast("filterFormMttrTypeCombo");
        dom.filterFormMttrTypeComboParent = $("#filterFormMttrTypeComboParent");
        dom.filterFormMttrTypeComboComboError = $("#filterFormMttrTypeComboComboError");

        //Grid
        dom.gridParent = $("#gridParent");
        dom.grid = ASPxClientGridView.Cast("grid");

        dom.mttrAndMtbfPerformanceCostGridModal = ASPxClientGridView.Cast("mttrAndMtbfPerformanceCostGridModal");
    }

    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);

        dom.filterFormMaintenanceGroupCombo.SetEnabled(false);
        dom.filterFormMttrMtbfTimeTypeCombo.SetEnabled(false);
        dom.filterFormDateCombo.SetEnabled(false);

        tools.disableItem(dom.filterFormPeriodStartDatePicker);
        tools.disableItem(dom.filterFormPeriodEndDatePicker);
    }

    function setGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);

        const activeIndex = event.visibleIndex;
        state.machineId = dom.grid.GetRowKey(activeIndex);
    }

    function setWorkOrderDom() {
        dom.workOrderGrid = ASPxClientGridView.Cast("workOrderGrid");

    }

    function setWorkOrderMainGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);
        state.workOrderId = dom.workOrderGrid.GetRowKey(event.visibleIndex);

    }

    function showCostReportPopUp() {
        const url = controllerName + "ShowMttrAndMtbfPerformanceCostReportPartial";

        const apiParam = {
            machineId: state.machineId,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate
        };

        const title = "مشاهده گزارش هزینه کارکرد";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom, false, true);
    }

    function statisticReportPopup() {
        const url = controllerName + "ShowMTTRAndMTBFPerformanceReportPartial";
        const apiParam = {
            machineId: state.machineId,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate

        };
        const title = "مشاهده گزارش عملکرد";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom, false, true);
    }

    function stockReportPopUp() {
        const url = controllerName + "ShowMttrAndMtbfWearhouseReport";
        const apiParam = {
            machineId: state.machineId,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate
        };
        const title = "مشاهده گزارش انبار";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom, false, true);
    }

    function workOrderReportPopUp() {
        const url = controllerName + "ShowMttrAndMtbfMachineWorkOrderListPartial";
        const apiParam = {
            machineId: state.machineId,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate
        };
        const title = "مشاهده گزارش لیست سفارشکارهای دستگاه";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setWorkOrderDom, false, true);
    }


    $(document).ready(init);

    return {
        faultReportPopUp: faultReportPopUp,
        handleConsumingMaterialsGridBeginCallback: handleConsumingMaterialsGridBeginCallback,
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormDepartmentIdComboSelectedIndexChange: handleFilterFormDepartmentIdComboSelectedIndexChange,
        handleFilterFormMaintenanceGroupComboSelectedIndexChange: handleFilterFormMaintenanceGroupComboSelectedIndexChange,
        handleFilterFormMttrMtbfTimeTypeComboSelectedIndexChange: handleFilterFormMttrMtbfTimeTypeComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleGetMttrAndMtbfFaultCodeCountGridBeginCallback: handleGetMttrAndMtbfFaultCodeCountGridBeginCallback,
        handleGridBeginCallback: handleGridBeginCallback,
        handleMainGridDetailRowConsumingMaterialsBtnClick: handleMainGridDetailRowConsumingMaterialsBtnClick,
        handleMainGridDetailRowPerformedOperationsBtnClick: handleMainGridDetailRowPerformedOperationsBtnClick,
        handleMainGridDetailRowReferralsHistoryBtnClick: handleMainGridDetailRowReferralsHistoryBtnClick,
        handleMainGridDetailRowRegistrarInfoBtnClick: handleMainGridDetailRowRegistrarInfoBtnClick,
        handleMTTRAndMTBFPerformanceReportGridBeginCallback: handleMTTRAndMTBFPerformanceReportGridBeginCallback,
        handleMTTRAndMTBFWearhouseGridBeginCallback: handleMTTRAndMTBFWearhouseGridBeginCallback,
        handlePerformanceCostGridBeginCallback: handlePerformanceCostGridBeginCallback,
        handlePerformedOperationsGridBeginCallback: handlePerformedOperationsGridBeginCallback,
        handleReferralsHistoryGridBeginCallback: handleReferralsHistoryGridBeginCallback,
        handleWorkOrderMainGridBeginCallback: handleWorkOrderMainGridBeginCallback,
        mtbfChartReportPopUp: mtbfChartReportPopUp,
        mttrChartPopup: mttrChartPopup,
        oeeChartReportPopup: oeeChartReportPopup,
        oeeReportPopup: oeeReportPopup,
        setGridFocusedRowOnExpanding: setGridFocusedRowOnExpanding,
        setWorkOrderMainGridFocusedRowOnExpanding: setWorkOrderMainGridFocusedRowOnExpanding,
        showCostReportPopUp: showCostReportPopUp,
        statisticReportPopup: statisticReportPopup,
        stockReportPopUp: stockReportPopUp,
        workOrderReportPopUp: workOrderReportPopUp
    };
})();