///<reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.dailyworkorderprint = (function () {

    var dom = {};
    var state = {
        customPeriodId: 'CustomPeriod',
        idOfTicketItemTypeWithPeriodInMinute: 1
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/DailyWorkOrderPrint/";

    function fillGrid() {
        if (!isFilterFormValid(false))
            return;

        var url = controllerName + "Grid";

        var statusId = dom.filterFormStatusTypeCombo.GetValue();
        var datePeriodType = dom.filterFormDateCombo.GetValue();
        var periodStartDate = dom.filterFormPeriodStartDatePicker.val();
        var periodEndDate = dom.filterFormPeriodEndDatePicker.val();


        var apiParam = {
            statusId: statusId,
            startDate: periodStartDate,
            endDate: periodEndDate,
            datePeriodType: datePeriodType
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (grid) {
                dom.gridParent.html(grid);
                setDom();
            });
    }

    function handleFilterFormDateComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDateComboError);
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        var selectedDateId = dom.filterFormDateCombo.GetValue();

        if (selectedDateId === state.customPeriodId) {
            tools.showItem(dom.filterFormPeriodStartDatePickerParent);
            tools.showItem(dom.filterFormPeriodEndDatePickerParent);
            tools.hideItem(dom.filterFormBeingItemsDivCenter);
        } else {
            tools.hideItem(dom.filterFormPeriodStartDatePickerParent);
            tools.hideItem(dom.filterFormPeriodEndDatePickerParent);
            tools.showItem(dom.filterFormBeingItemsDivCenter);
            dom.filterFormPeriodStartDatePicker.val("");
            dom.filterFormPeriodEndDatePicker.val("");
        }
    }

    function handleFilterFormStatusComboIndexChange() {
        dom.filterFormDateCombo.SetEnabled(true);

    }
    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
    }

    function handleFilterFormTicketItemTypeComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormTicketItemTypeComboError);
        dom.filterFormDateCombo.SetEnabled(true);
    }

    function handleFilterFormEmployeeComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormEmployeeComboError);
        dom.filterFormDateCombo.SetEnabled(true);
    }

    function handleFilterFormSearchButtonClick() {
        if (!isFilterFormValid())
            return;

        fillGrid();
    }

    function handleGridBeginCallback(command) {
        var statusId =
            +dom.filterFormStatusTypeCombo.GetValue() > -1
                ? dom.filterFormStatusTypeCombo.GetValue()
                : -1;

        var dateTypeId = dom.filterFormDateCombo.GetValue();
        var periodStartDate = dom.filterFormPeriodStartDatePicker.val();
        var periodEndDate = dom.filterFormPeriodEndDatePicker.val();

        if (dateTypeId == null ||
            (dateTypeId === state.customPeriodId &&
                (tools.isNullOrEmpty(periodStartDate) || tools.isNullOrEmpty(periodEndDate)))) {
            statusId = -1;
            dateTypeId = -1;
        }

        command.callbackUrl =
            controllerName + "Grid"
        + "?statusId=" + statusId
            + "&startDate=" + periodStartDate
            + "&endDate=" + periodEndDate
            + "&datePeriodType=" + dateTypeId;
    }
    function handleGridCustomBtnClick(source, event) {
        state.GridFocusedRowIndex =
            dom.grid.GetFocusedRowIndex();

        var selectedWokOrderRowIndex = event.visibleIndex;
        var selectedWorkOrderId = dom.grid.GetRowKey(selectedWokOrderRowIndex);
        var customBtnId = event.buttonID;

        if (customBtnId === "printButton") return window.open("/print/Print?WorkorderID=" + selectedWorkOrderId);
    }



    function init() {
        setDom();
        setEvents();

        dom.filterFormDateCombo.SetEnabled(false);
    }

    function isFilterFormValid(shouldDisplayValidationErrors = true) {
        var isValid = true;

        tools.hideItem(dom.filterFormStatusComboError);

        var statusId = dom.filterFormStatusTypeCombo.GetValue();
        var isStatusSelected = !tools.isNullOrEmpty(statusId);
        if (!isStatusSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormStatusComboError);
        }

        tools.hideItem(dom.filterFormDateComboError);
        var dateId = dom.filterFormDateCombo.GetValue();
        var isDateSelected = !tools.isNullOrEmpty(dateId);
        if (!isDateSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormDateComboError);
        }

        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        if (dateId === state.customPeriodId) {

            var periodStart = dom.filterFormPeriodStartDatePicker.val();
            var isPeriodStartSelected = !tools.isNullOrEmpty(periodStart);
            if (!isPeriodStartSelected) {
                isValid = false;
                shouldDisplayValidationErrors && tools.showItem(dom.filterFormPeriodStartDatePickerError);
            }

            var periodEnd = dom.filterFormPeriodEndDatePicker.val();
            var isPeriodEndSelected = !tools.isNullOrEmpty(periodEnd);
            if (!isPeriodEndSelected) {
                isValid = false;
                shouldDisplayValidationErrors && tools.showItem(dom.filterFormPeriodEndDatePickerError);
            };
        }

        return isValid;
    }

    function setDom() {

        //Filter Form
        dom.filterFormStatusTypeCombo = ASPxClientComboBox.Cast("filterFormStatusTypeCombo");
        dom.filterFormStatusComboError = $("#filterFormStatusComboError");

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

    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);
    }


    $(document).ready(init);

    return {
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormStatusComboIndexChange: handleFilterFormStatusComboIndexChange,
        handleFilterFormTicketItemTypeComboSelectedIndexChange: handleFilterFormTicketItemTypeComboSelectedIndexChange,
        handleFilterFormEmployeeComboSelectedIndexChange: handleFilterFormEmployeeComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleGridBeginCallback: handleGridBeginCallback,
        handleGridCustomBtnClick: handleGridCustomBtnClick
        
    };

})();
