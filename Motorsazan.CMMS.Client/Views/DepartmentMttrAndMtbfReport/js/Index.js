/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.departmentMttrAndMtbfReport = (function() {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod",
        machineId: 0,
        workOrderId: 0,
        departmentId: 0,
        timeType: 0,
        persianStartDate: "",
        persianEndDate: "",
        datePeriodType:0
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/departmentMttrAndMtbfReport/";

    async function fillGrid() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "Grid";

        state.departmentId = dom.filterFormDepartmentIdCombo.GetValue();
        state.timeType = dom.filterFormMttrMtbfTimeTypeCombo.GetValue();
        const datePeriodType = dom.filterFormDateCombo.GetValue();
        state.datePeriodType = datePeriodType;
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        state.persianStartDate = persianStartDate;
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();
        state.persianEndDate = persianEndDate;
        const apiParam = {
            departmentId: state.departmentId,
            timeType: state.timeType,
            startDate: persianStartDate,
            endDate: persianEndDate,
            datePeriodType: datePeriodType
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.gridParent.html(gridHtml);
        setDom();
    }

    function handlePerformedOperationsGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "PerformedOperationsGrid" + "?WorkOrderId=" + state.workOrderId;
    }

    function handleFilterFormDateComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDateComboError);

        const selectedDateId = dom.filterFormDateCombo.GetValue();
        state.datePeriodType = selectedDateId;

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
        dom.filterFormMttrMtbfTimeTypeCombo.SetEnabled(true);
    }

    function handleFilterFormMttrMtbfTimeTypeComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormMttrMtbfTimeTypeComboError);
        dom.filterFormDateCombo.SetEnabled(true);
    }

    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        state.persianEndDate = dom.filterFormPeriodEndDatePicker.val();
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);

        state.persianStartDate = dom.filterFormPeriodStartDatePicker.val();
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
                "&timeType=" +
                -1 +
                "&startDate=" +
                "" +
                "&endDate=" +
                "" +
                "&datePeriodType=" +
                -1;
            return;
        }

        var departmentId = +dom.filterFormDepartmentIdCombo.GetValue() > -1
            ? dom.filterFormDepartmentIdCombo.GetValue()
            : -1;

        const timeType = +dom.filterFormMttrMtbfTimeTypeCombo.GetValue() > -1
            ? dom.filterFormMttrMtbfTimeTypeCombo.GetValue()
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
            "&timeType=" +
            timeType +
            "&startDate=" +
            persianStartDate +
            "&endDate=" +
            persianEndDate +
            "&datePeriodType=" +
            dateTypeId;
    }

    function handleperformanceCostGridBeginCallback(command) {
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();
        const datePeriodType = dom.filterFormDateCombo.GetValue();

        command.callbackUrl =
            controllerName +
            "ShowMTTRAndMTBFPerformanceCostReportPartial" +
            "?MachineId=" +
            state.machineId +
            "&startDate=" +
            persianStartDate  +
            "&endDate=" +
            persianEndDate  +
            "&datePeriodType=" +
            datePeriodType ;

    }

    function handleGetMttrAndMtbfFaultCodeCountGridBeginCallback(command) {
        command.callbackUrl =
            controllerName +
            "ShowGetMttrAndMtbfFaultCodeCountReportPartial" +
            "?MachineId=" +
            state.machineId +
            "&startDate=" +
            state.persianStartDate +
            "&endDate=" +
            state.persianEndDate +
            "&datePeriodType=" +
            state.datePeriodType;
    }
    
    function handleWorkOrderMainGridBeginCallback(command) {
        command.callbackUrl =
            controllerName +
            "ShowMttrAndMtbfMachineWorkOrderListPartial" +
            "?MachineId=" +
            state.machineId +
            "&startDate=" +
            state.persianStartDate +
            "&endDate=" +
            state.persianEndDate +
            "&datePeriodType=" +
            state.datePeriodType;

    }

    function handleMTTRAndMTBFWearhouseGridBeginCallback(command) {
        command.callbackUrl =
            controllerName +
            "ShowMttrAndMtbfWearhouseReport" +
            "?MachineId=" +
            state.machineId +
            "&startDate=" +
            state.persianStartDate +
            "&endDate=" +
            state.persianEndDate +
            "&datePeriodType=" +
            state.datePeriodType;

    }

    function handleMTTRAndMTBFPerformanceReportGridBeginCallback(command) {
        command.callbackUrl =
            controllerName +
            "ShowMTTRAndMTBFPerformanceReportPartial" +
            "?MachineId=" +
            state.machineId +
            "&startDate=" +
            state.persianStartDate +
            "&endDate=" +
            state.persianEndDate +
            "&datePeriodType=" +
            state.datePeriodType;

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

    function handleMainGridDetailRowPerformedOperationsBtnClick() {
        const title = "مشاهده عملیات انجام شده";
        const url = controllerName + "PerformedOperationsGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
    }

    function handleConsumingMaterialsGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "ConsumingMaterialsGrid" + "?workOrderId=" + state.workOrderId;
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

    function handleMainGridDetailRowReferralsHistoryBtnClick() {
        const title = "مشاهده تاریخچه ارجاعات";
        const url = controllerName + "ReferralsHistoryGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
    }

    function handleReferralsHistoryGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "ReferralsHistoryGrid" + "?WorkOrderId=" + state.workOrderId;
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

        tools.hideItem(dom.filterFormMttrMtbfTimeTypeComboError);
        const timeType = dom.filterFormMttrMtbfTimeTypeCombo.GetValue();
        const isTimeTypeSelected = !tools.isNullOrEmpty(timeType);
        if (!isTimeTypeSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormMttrMtbfTimeTypeComboError);
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

    function faultReportPopUp() {
        const url = controllerName + "ShowGetMttrAndMtbfFaultCodeCountReportParentPartial";
        const apiParam = {
            machineId: state.machineId,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate
        };
        const title = "گزارش تعداد رخداد کد خطا";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom);
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
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom,true,false);
    }

    function oeeChartReportPopup() {
        const url = controllerName + "ShowOeeChartReportOnMTTRAndMTBFByMachineIdPartial";
     
        const title = "مشاهده گزارش هزینه کارکرد";
        const apiParam = {
            machineId: state.machineId,
            timeType: state.timeType,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate
        };
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom, true, false);
    }

    function oeeReportPopup() {
        return window.open("/print/PrintOEEReportOnMTTRAndMTBFByMachineId?machineId=" + state.machineId + "&timeType=" + state.timeType + "&startDate=" + state.persianStartDate + "&endDate=" + state.persianEndDate + "&datePeriodType=" + state.datePeriodType);
    }

    function showCostReportPopUp() {
        const url = controllerName + "ShowMttrAndMtbfPerformanceCostReportParentPartial";
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
        const url = controllerName + "ShowMTTRAndMTBFPerformanceReportParentPartial";
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
        const url = controllerName + "ShowMttrAndMtbfWearhouseParentReport";
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
        const url = controllerName + "ShowMttrAndMtbfMachineWorkOrderListParentPartial";
        const apiParam = {
            machineId: state.machineId,
            datePeriodType: state.datePeriodType,
            startDate: state.persianStartDate,
            endDate: state.persianEndDate
        };
        const title = "مشاهده گزارش لیست سفارشکارهای دستگاه";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setWorkOrderDom, false, true);
    }

    function setGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);
        state.machineId = dom.grid.GetRowKey(event.visibleIndex);
    }

    function setWorkOrderMainGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);
        state.workOrderId = dom.workOrderGrid.GetRowKey(event.visibleIndex);

    }

    function setDom() {
        //FilterForm
        dom.filterFormDepartmentIdCombo = ASPxClientComboBox.Cast("filterFormDepartmentIdCombo");
        dom.filterFormDepartmentIdComboParent = $("#filterFormDepartmentIdComboParent");
        dom.filterFormDepartmentIdComboError = $("#filterFormDepartmentIdComboError");
        dom.filterFormDepartmentIdComboDesign = $("#filterFormDepartmentIdComboDesign");

        dom.filterFormDateCombo = ASPxClientComboBox.Cast("filterFormDateCombo");

        dom.filterFormMttrMtbfTimeTypeComboParent = $("#filterFormMttrMtbfTimeTypeComboParent");
        dom.filterFormMttrMtbfTimeTypeComboError = $("#filterFormMttrMtbfTimeTypeComboError");
        dom.filterFormMttrMtbfTimeTypeCombo = ASPxClientComboBox.Cast("filterFormMttrMtbfTimeTypeCombo");

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

        dom.mttrAndMtbferformanceCostGridModal = ASPxClientGridView.Cast("mttrAndMtbferformanceCostGridModal");


    }

    function setWorkOrderDom() {
        dom.workOrderGrid = ASPxClientGridView.Cast("workOrderGrid");

    }

    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);

        dom.filterFormDateCombo.SetEnabled(false);
        dom.filterFormMttrMtbfTimeTypeCombo.SetEnabled(false);
        tools.disableItem(dom.filterFormPeriodStartDatePicker);
        tools.disableItem(dom.filterFormPeriodEndDatePicker);

    }

    $(document).ready(init);

    return {
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormDepartmentIdComboSelectedIndexChange: handleFilterFormDepartmentIdComboSelectedIndexChange,
        handleFilterFormMttrMtbfTimeTypeComboSelectedIndexChange: handleFilterFormMttrMtbfTimeTypeComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleGridBeginCallback: handleGridBeginCallback,
        handleMainGridDetailRowRegistrarInfoBtnClick: handleMainGridDetailRowRegistrarInfoBtnClick,
        handleMainGridDetailRowPerformedOperationsBtnClick: handleMainGridDetailRowPerformedOperationsBtnClick,
        handlePerformedOperationsGridBeginCallback: handlePerformedOperationsGridBeginCallback,
        handleMainGridDetailRowConsumingMaterialsBtnClick: handleMainGridDetailRowConsumingMaterialsBtnClick,
        handleConsumingMaterialsGridBeginCallback: handleConsumingMaterialsGridBeginCallback,
        handleMainGridDetailRowReferralsHistoryBtnClick: handleMainGridDetailRowReferralsHistoryBtnClick,
        handleReferralsHistoryGridBeginCallback: handleReferralsHistoryGridBeginCallback,
        setGridFocusedRowOnExpanding: setGridFocusedRowOnExpanding,
        showCostReportPopUp: showCostReportPopUp,
        statisticReportPopup: statisticReportPopup,
        stockReportPopUp: stockReportPopUp,
        mttrChartPopup: mttrChartPopup,
        mtbfChartReportPopUp: mtbfChartReportPopUp,
        workOrderReportPopUp: workOrderReportPopUp,
        oeeChartReportPopup: oeeChartReportPopup,
        faultReportPopUp: faultReportPopUp,
        oeeReportPopup: oeeReportPopup,
        handleWorkOrderMainGridBeginCallback: handleWorkOrderMainGridBeginCallback,
        setWorkOrderMainGridFocusedRowOnExpanding: setWorkOrderMainGridFocusedRowOnExpanding,
        handleperformanceCostGridBeginCallback: handleperformanceCostGridBeginCallback,
        handleGetMttrAndMtbfFaultCodeCountGridBeginCallback: handleGetMttrAndMtbfFaultCodeCountGridBeginCallback,
        handleMTTRAndMTBFWearhouseGridBeginCallback: handleMTTRAndMTBFWearhouseGridBeginCallback,
        handleMTTRAndMTBFPerformanceReportGridBeginCallback: handleMTTRAndMTBFPerformanceReportGridBeginCallback
    };
})();