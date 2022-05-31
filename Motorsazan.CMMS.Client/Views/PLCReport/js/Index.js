/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Views/PerformanceReportByMachine/js/Index.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_LoadingModal.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.pLCReport = (function() {

    const dom = {};
    const state = {
        specialDateTypeFilter: "CustomPeriod",
        detailMachineId: null,
        detailPalletChanger: null

    };
    const tools = motorsazanClient.tools;
    const controllerName = "/PLCReport/";

    function accessHourToMachineBtnInit() {
        accessHourToMachineBtnSetDom();
    }

    function accessHourToMachineBtnSetDom() {
        dom.accessHourToMachineGrid = $("#accessHourToMachineGrid");
    }

    function comparisonOfToolsChangesAndPalletBtnInit() {
        comparisonOfToolsChangesAndPalletBtnSetDom();
    }

    function comparisonOfToolsChangesAndPalletBtnSetDom() {
        dom.comparisonBetweenToolsAndPalletChangerChartReport =
            ASPxClientGridView.Cast("comparisonBetweenToolsAndPalletChangerChartReport");
    }

    function consumerFlowChangesBtnInit() {
        consumerFlowChangesBtnSetDom();
    }

    function consumerFlowChangesBtnSetDom() {
        dom.machineOilPressureChartReport = ASPxClientGridView.Cast("machinePowerChangerChartReport");
    }

    function detailBtnInit() {
        detailBtnSetDom();

        motorsazanClient.loadingModal.hide();
    }

    function detailBtnSetDom() {
        dom.pLCMachineSpindlePerformanceReportDetailGrid =
            ASPxClientGridView.Cast("pLCMachineSpindlePerformanceReportDetailGrid");
    }

    async function handleAccessHourToMachineBtn() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "AccessHourToMachineForm";

        const machineId = dom.filterFormMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();
        const machineName = dom.filterFormMachineCombo.GetSelectedItem().texts[0];

        const apiParam = {
            machineId: machineId,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType,
            machineName: machineName
        };

        const title = "گزارش اطلاعات دسترسی دستگاه";
        await motorsazanClient.contentModal.ajaxShow(title, url, apiParam, accessHourToMachineBtnInit, true, false);

    }

    function handleAccessHourToMachineGridBeginCallback(command) {
        if (!isFilterFormValid())
            return;

        const machineId = dom.filterFormMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        command.callbackUrl =
            controllerName +
            "AccessHourToMachineGrid" +
            "?machineId=" +
            machineId +
            "&persianStartDate=" +
            persianStartDate +
            "&persianEndDate=" +
            persianEndDate +
            "&datePeriodType=" +
            datePeriodType;
    }

    async function handleComparisonOfToolsChangesAndPalletBtn() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "ComparisonBetweenToolsAndPalletChangerChartReport";

        const machineId = dom.filterFormMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();
        const machineName = dom.filterFormMachineCombo.GetSelectedItem().texts[0];
        const oldMachineCode = dom.filterFormMachineCombo.GetSelectedItem().texts[1];

        const apiParam = {
            machineId: machineId,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType,
            machineName: machineName,
            oldMachineCode: oldMachineCode
        };

        const title = "گزارش مقایسه تغییرات ابزار و پالت";
        await motorsazanClient.contentModal.ajaxShow(title,
            url,
            apiParam,
            comparisonOfToolsChangesAndPalletBtnInit,
            true,
            false);
    }

    async function handleConsumerFlowChangesBtn() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "MachinePowerChangerChartReport";

        const machineId = dom.filterFormMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();
        const machineName = dom.filterFormMachineCombo.GetSelectedItem().texts[0];
        const oldMachineCode = dom.filterFormMachineCombo.GetSelectedItem().texts[1];

        const apiParam = {
            machineId: machineId,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType,
            machineName: machineName,
            oldMachineCode: oldMachineCode
        };

        const title = "گزارش تغییرات جریان مصرفی";
        await motorsazanClient.contentModal.ajaxShow(title, url, apiParam, consumerFlowChangesBtnInit, true, false);
    }

    function handleFilterFormDateTypeComboSelectedIndexChanged() {
        tools.hideItem(dom.filterFormDateTypeComboError);

        const activeDateType = dom.filterFormDateTypeCombo.GetValue();
        const isSpecialTypeSelected = activeDateType === state.specialDateTypeFilter;


        if (isSpecialTypeSelected) {
            tools.showItem(dom.filterFormSpecialDateColumn);

            tools.hideItem(dom.filterFormStartDateError);
            tools.hideItem(dom.filterFormEndDateError);


            dom.FilterFormMachineComboDesign.addClass("col-md-4");
            dom.FilterFormMachineComboDesign.removeClass("col-md-6");

            dom.filterFormDateTypeComboParentDesign.addClass("col-md-4");
            dom.filterFormDateTypeComboParentDesign.removeClass("col-md-6");

        } else {
            tools.hideItem(dom.filterFormSpecialDateColumn);
            dom.filterFormStartDate.val("");
            dom.filterFormEndDate.val("");

            dom.FilterFormMachineComboDesign.addClass("col-md-6");
            dom.FilterFormMachineComboDesign.removeClass("col-md-4");

            dom.filterFormDateTypeComboParentDesign.addClass("col-md-6");
            dom.filterFormDateTypeComboParentDesign.removeClass("col-md-4");
        }
    }

    function handleFilterFormMachineComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormMachineComboError);
        dom.filterFormDateTypeCombo.SetEnabled(true);
    }

    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormEndDateError);
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormStartDateError);
    }

    async function handleOilHeatChangesBtn() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "MachineOilTemperatureChartReport";

        const machineId = dom.filterFormMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();
        const machineName = dom.filterFormMachineCombo.GetSelectedItem().texts[0];
        const oldMachineCode = dom.filterFormMachineCombo.GetSelectedItem().texts[1];

        const apiParam = {
            machineId: machineId,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType,
            machineName: machineName,
            oldMachineCode: oldMachineCode
        };

        const title = "گزارش درجه حرارت روغن";
        await motorsazanClient.contentModal.ajaxShow(title, url, apiParam, oilHeatChangesBtnInit, true, false);
    }

    async function handleOilPressureChangesBtn() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "MachineOilPressureChartReport";

        const machineId = dom.filterFormMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();
        const machineName = dom.filterFormMachineCombo.GetSelectedItem().texts[0];
        const oldMachineCode = dom.filterFormMachineCombo.GetSelectedItem().texts[1];

        const apiParam = {
            machineId: machineId,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType,
            machineName: machineName,
            oldMachineCode: oldMachineCode
        };

        const title = "گزارش تغییرات فشار روغن";
        await motorsazanClient.contentModal.ajaxShow(title, url, apiParam, oilPressureChangesBtnInit, true, false);
    }

    async function handlePalletChangeCountBtn() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "PalletChangeCountPLCReportChart";

        const machineId = dom.filterFormMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();
        const machineName = dom.filterFormMachineCombo.GetSelectedItem().texts[0];
        const oldMachineCode = dom.filterFormMachineCombo.GetSelectedItem().texts[1];

        const apiParam = {
            machineId: machineId,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType,
            machineName: machineName,
            oldMachineCode: oldMachineCode
        };

        const title = "گزارش تعویض پالت";
        await motorsazanClient.contentModal.ajaxShow(title, url, apiParam, palletChangeCountBtnInit, true, false);
    }

    function handlePLCMachineSpindlePerformanceReportDetailGridBeginCallback(command) {
        command.callbackUrl =
            controllerName +
            "PLCMachineSpindlePerformanceReportDetailGrid" +
            "?machineId=" +
            state.detailMachineId +
            "&palletChanger=" +
            state.detailPalletChanger;
    }

    function handlePLCMachineSpindlePerformanceReportGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            command.callbackUrl =
                controllerName +
                "PLCMachineSpindlePerformanceReportGrid" +
                "?machineId=" +
                -1 +
                "&persianStartDate=" +
                "" +
                "&persianEndDate=" +
                "" +
                "&datePeriodType=" +
                -1;
            return;
        }

        const machineId = dom.filterFormMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();
        const machineName = dom.filterFormMachineCombo.GetSelectedItem().texts[0];
        const oldMachineCode = dom.filterFormMachineCombo.GetSelectedItem().texts[1];

        command.callbackUrl =
            controllerName +
            "PLCMachineSpindlePerformanceReportGrid" +
            "?machineId=" + machineId +
            "&persianStartDate=" + persianStartDate +
            "&persianEndDate=" + persianEndDate +
            "&datePeriodType=" + datePeriodType + 
            "&machineName=" + machineName +
            "&oldMachineCode=" + oldMachineCode;
    }

    function handlePLCMachineSpindlePerformanceReportGridCustomBtnClick(source, event) {
        spindlePerformanceBtnSetDom();

        const customBtnId = event.buttonID;

        if (customBtnId === "showDetailBtn") {
            motorsazanClient.loadingModal.show();

            dom.pLCMachineSpindlePerformanceReportGrid.GetRowValues(
                dom.pLCMachineSpindlePerformanceReportGrid.GetFocusedRowIndex(),
                "MachineId;PalletChanger;CreateDate;MachinePLCFileDataId",
                (values) => {
                    showDetailBtnConfirmation(values);
                }
            );
        }
    }

    async function handleSpindlePerformanceBtn() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "PLCMachineSpindlePerformanceReportForm";

        const machineId = dom.filterFormMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();
        const machineName = dom.filterFormMachineCombo.GetSelectedItem().texts[0];
        const oldMachineCode = dom.filterFormMachineCombo.GetSelectedItem().texts[1];

        const apiParam = {
            machineId: machineId,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType,
            machineName: machineName,
            oldMachineCode: oldMachineCode
        };

        const title = "گزارش عملکرد اسپیندل";
        await motorsazanClient.contentModal.ajaxShow(title, url, apiParam, spindlePerformanceBtnInit, false, true);
    }

    async function handleToolsChangeRateBtn() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "ToolsChangeRatePLCReportChart";

        const machineId = dom.filterFormMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();
        const machineName = dom.filterFormMachineCombo.GetSelectedItem().texts[0];
        const oldMachineCode = dom.filterFormMachineCombo.GetSelectedItem().texts[1];

        const apiParam = {
            machineId: machineId,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType,
            machineName: machineName,
            oldMachineCode: oldMachineCode
        };

        const title = "گزارش تعویض ابزار";
        await motorsazanClient.contentModal.ajaxShow(title, url, apiParam, toolsChangeRateBtnInit, true, false);
    }

    function init() {
        setDom();
        setEvents();
    }

    function isFilterFormValid(shouldDisplayValidationErrors = true) {
        let isValid = true;

        setDom();

        tools.hideItem(dom.filterFormMachineComboError);
        const machineId = dom.filterFormMachineCombo.GetValue();
        const isMachineIdSelected = !tools.isNullOrEmpty(machineId);
        if (!isMachineIdSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormMachineComboError);
        }

        tools.hideItem(dom.filterFormDateTypeComboError);
        const dateId = dom.filterFormDateTypeCombo.GetValue();
        const isDateSelected = !tools.isNullOrEmpty(dateId);
        if (!isDateSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormDateTypeComboError);
        }

        tools.hideItem(dom.filterFormStartDateError);
        tools.hideItem(dom.filterFormEndDateError);

        if (dateId === state.specialDateTypeFilter) {

            const periodStart = dom.filterFormStartDate.val();
            const isPeriodStartSelected = !tools.isNullOrEmpty(periodStart);
            if (!isPeriodStartSelected) {
                isValid = false;
                shouldDisplayValidationErrors && tools.showItem(dom.filterFormStartDateError);
            }

            const periodEnd = dom.filterFormEndDate.val();
            const isPeriodEndSelected = !tools.isNullOrEmpty(periodEnd);
            if (!isPeriodEndSelected) {
                isValid = false;
                shouldDisplayValidationErrors && tools.showItem(dom.filterFormEndDateError);
            };
        }

        return isValid;
    }

    function oilHeatChangesBtnInit() {
        oilHeatChangesBtnSetDom();
    }

    function oilHeatChangesBtnSetDom() {
        dom.machineOilTemperatureChartReport = ASPxClientGridView.Cast("machineOilTemperatureChartReport");
    }

    function oilPressureChangesBtnInit() {
        oilPressureChangesBtnSetDom();
    }

    function oilPressureChangesBtnSetDom() {
        dom.machineOilPressureChartReport = ASPxClientGridView.Cast("machineOilPressureChartReport");
    }

    function palletChangeCountBtnInit() {
        palletChangeCountBtnSetDom();
    }

    function palletChangeCountBtnSetDom() {
        dom.palletChangeCountPLCReportChart = ASPxClientGridView.Cast("palletChangeCountPLCReportChart");
    }

    function setDom() {
        //FilterForm
        dom.filterFormMachineCombo = ASPxClientComboBox.Cast("filterFormMachineCombo");
        dom.filterFormMachineComboParent = $("#filterFormMachineComboParent");
        dom.filterFormMachineComboError = $("#filterFormMachineComboError");
        dom.FilterFormMachineComboDesign = $("#FilterFormMachineComboDesign");

        dom.filterFormDateTypeComboParentDesign = $("#filterFormDateTypeComboParentDesign");
        dom.filterFormDateTypeComboParent = $("#filterFormDateTypeComboParent");
        dom.filterFormDateTypeComboError = $("#filterFormDateTypeComboError");
        dom.filterFormDateTypeCombo = ASPxClientComboBox.Cast("filterFormDateTypeCombo");

        dom.filterFormSpecialDateColumn = $("#filterFormSpecialDateColumn");

        dom.filterFormStartDateError = $("#filterFormStartDateError");
        dom.filterFormStartDate = $("#filterFormStartDate");

        dom.filterFormEndDateError = $("#filterFormEndDateError");
        dom.filterFormEndDate = $("#filterFormEndDate");
    }

    function setEvents() {
        dom.filterFormStartDate.on("change", handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormEndDate.on("change", handleFilterFormPeriodEndDatePickerSelectionChange);

        dom.filterFormDateTypeCombo.SetEnabled(false);

    }

    async function showDetailBtnConfirmation(values) {
        const url = controllerName + "PLCMachineSpindlePerformanceReportDetailForm";

        const machineId = values[0];
        state.detailMachineId = machineId;
        const palletChanger = values[1];
        state.detailPalletChanger = palletChanger;

        const offset = values[2].getTimezoneOffset()
        values[2] = new Date(values[2].getTime() - (offset * 60 * 1000))
        const createDate = values[2].toISOString().split('T')[0]

        const machinePLCFileDataId = values[3];
        const machineName = dom.filterFormMachineCombo.GetSelectedItem().texts[0];
        const oldMachineCode = dom.filterFormMachineCombo.GetSelectedItem().texts[1];
         
        const apiParam = {
            machineId: machineId,
            palletChanger: palletChanger,
            machineName: machineName,
            oldMachineCode: oldMachineCode,
            createDate: createDate,
            machinePLCFileDataId: machinePLCFileDataId
        };

        const title = "جزئیات تعویض ابزار";
        await motorsazanClient.contentModal.ajaxShow(title, url, apiParam, detailBtnInit, true, false);
    }

    function spindlePerformanceBtnInit() {
        spindlePerformanceBtnSetDom();
    }

    function spindlePerformanceBtnSetDom() {
        dom.pLCMachineSpindlePerformanceReportGrid = ASPxClientGridView.Cast("pLCMachineSpindlePerformanceReportGrid");
    }

    function toolsChangeRateBtnInit() {
        toolsChangeRateBtnSetDom();
    }

    function toolsChangeRateBtnSetDom() {
        dom.toolsChangeRatePLCReportChart = ASPxClientGridView.Cast("toolsChangeRatePLCReportChart");
    }



    async function handleRunPlcJobButtonClick() {

        var url = "/PLCReport/ResetMachinePlcFileDataTransferJob";

        motorsazanClient.connector.post(url)
            .then(function (resultMessage) {
                motorsazanClient.contentModal.hide();
                motorsazanClient.messageModal.success(resultMessage);
                handleResetJob();
            });

    }
    
    async function handleResetJob() {
        // const url = "http://erp-server:2019/WebUI/default.aspx/WebUI/default.aspx/ChangeJobStatus";

        const url = controllerName + "ChangeJobStatus";

        const apiParam = {
            jobName: "TransferPLCData",
            jobNewStatus: 1,
            projectName: "CMMS"
        }
        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        if (apiResult.IsSuccess) {
            motorsazanClient.messageModal.success(apiResult.JobStatus);
        } else {
            motorsazanClient.messageModal.error(apiResult.JobStatus);
        }
    }




    $(document).ready(init);

    return {
        handleAccessHourToMachineBtn: handleAccessHourToMachineBtn,
        handleAccessHourToMachineGridBeginCallback: handleAccessHourToMachineGridBeginCallback,
        handleComparisonOfToolsChangesAndPalletBtn: handleComparisonOfToolsChangesAndPalletBtn,
        handleConsumerFlowChangesBtn: handleConsumerFlowChangesBtn,
        handleFilterFormDateTypeComboSelectedIndexChanged: handleFilterFormDateTypeComboSelectedIndexChanged,
        handleFilterFormMachineComboSelectedIndexChange: handleFilterFormMachineComboSelectedIndexChange,
        handleFilterFormPeriodEndDatePickerSelectionChange: handleFilterFormPeriodEndDatePickerSelectionChange,
        handleFilterFormPeriodStartDatePickerSelectionChange: handleFilterFormPeriodStartDatePickerSelectionChange,
        handleOilHeatChangesBtn: handleOilHeatChangesBtn,
        handleOilPressureChangesBtn: handleOilPressureChangesBtn,
        handlePalletChangeCountBtn: handlePalletChangeCountBtn,
        handlePLCMachineSpindlePerformanceReportDetailGridBeginCallback:
            handlePLCMachineSpindlePerformanceReportDetailGridBeginCallback,
        handlePLCMachineSpindlePerformanceReportGridBeginCallback:
            handlePLCMachineSpindlePerformanceReportGridBeginCallback,
        handlePLCMachineSpindlePerformanceReportGridCustomBtnClick:
            handlePLCMachineSpindlePerformanceReportGridCustomBtnClick,
        handleSpindlePerformanceBtn: handleSpindlePerformanceBtn,
        handleToolsChangeRateBtn: handleToolsChangeRateBtn,
        handleRunPlcJobButtonClick: handleRunPlcJobButtonClick,
        dom
    };
})();