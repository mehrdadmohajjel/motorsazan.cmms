/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.delayInWorkOrderReport = (function () {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod",
        GridFocusedRowIndex: 0,
        delayTypeId: 0,
        workOrderId: 0,
        startDate: '',
        endDate: '',
        dateType: ''
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/DelayInWorkOrderReport/";

    async function fillGrid() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "Grid";
        const datePeriodType = dom.filterFormDateCombo.GetValue();
        const periodStartDate = dom.filterFormPeriodStartDatePicker.val();
        const periodEndDate = dom.filterFormPeriodEndDatePicker.val();
        state.dateType = datePeriodType;
        state.startDate = periodStartDate;
        state.endDate = periodEndDate;
        const apiParam = {
            startShamsiDate: periodStartDate,
            endPersianDate: periodEndDate,
            dateType: datePeriodType
        };
        const grid = await motorsazanClient.connector.post(url, apiParam);
        dom.gridParent.html(grid);
        setDom();
    }

    function handleDetailGridCallbackUrl(source) {
        const url = controllerName + "ShowWorkOrderDetail";
        if (!isFilterFormValid()) {
            source.callbackUrl = url + "?delayTypeId=" + -1 +
                "&startShamsiDate=" + "" +
                "&endPersianDate=" + ""+
                "&dateType=" + -1;
            return;
        }

        source.callbackUrl = url + "?delayTypeId=" + state.delayTypeId +
            "&startShamsiDate=" + state.startDate +
            "&endPersianDate=" + state.endDate +
            "&dateType=" + state.dateType;
    }

    function handleGridCustomBtnClick(source, event) {

        state.GridFocusedRowIndex =
            dom.grid.GetFocusedRowIndex();

        const selectedWokOrderRowIndex = event.visibleIndex;
        const selectedId = dom.grid.GetRowKey(selectedWokOrderRowIndex);
        state.delayTypeId = selectedId;


        const customBtnId = event.buttonID;

        if (customBtnId === "showDetailBtn") return showDetailbtnConfirmation();
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
        if (!isFilterFormValid())
            return;

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
            "?startShamsiDate=" +
            periodStartDate +
            "&endPersianDate=" +
            periodEndDate +
            "&dateType=" +
            dateTypeId;
    }

    function init() {
        setDom();
        setEvents();
        tools.disableItem(dom.filterFormPeriodStartDatePicker);
        tools.disableItem(dom.filterFormPeriodEndDatePicker);

    }

    function isFilterFormValid(shouldDisplayValidationErrors = true) {
        var isValid = true;

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
    function setDetailGridDom() {
        dom.detailGridParent = ASPxClientGridView.Cast("detailGridParent");

    }


    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);
    }


    function showDetailbtnConfirmation() {
        const url = controllerName + "ShowWorkOrderDetail";
        const apiParam = {
            DelayTypeId: state.delayTypeId,
            startShamsiDate: state.startDate,
            endPersianDate: state.endDate,
            dateType: state.dateType
        };

        const title = "لیست سفارشکارها";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDetailGridDom, false, true);
    }


    $(document).ready(init);

    return {
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleGridBeginCallback: handleGridBeginCallback,
        handleGridCustomBtnClick: handleGridCustomBtnClick,
        handleDetailGridCallbackUrl: handleDetailGridCallbackUrl

    };
})();
