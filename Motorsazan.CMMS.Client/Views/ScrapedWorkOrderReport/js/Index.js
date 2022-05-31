/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.scrapedWorkOrderReport = (function () {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod",
        workOrderId: 0,
        workOrderTypeId: 0,
        WorkOrderStatusTypeId: 0,
        persianStartDate: null,
        persianEndDate: null,
        datePeriodType: null

    };
    var tools = motorsazanClient.tools;
    var controllerName = "/ScrapedWorkOrderReport/";

    function fillGrid() {
        if (!isFilterFormValid(false))
            return;

        const url = controllerName + "Grid";

        state.datePeriodType = dom.filterFormDateCombo.GetValue();
        state.persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        state.persianEndDate = dom.filterFormPeriodEndDatePicker.val();


        const apiParam = {
            dateType: state.datePeriodType,
            startShamsiDate: state.persianStartDate,
            endPersianDate: state.persianEndDate
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (grid) {
                dom.gridParent.html(grid);
                setDom();
            });
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
        if (!isFilterFormValid()) {
            command.callbackUrl =
                controllerName +
                "Grid" +
                "?dateType=" +
                -1 +
                "&startShamsiDate=" +
                "" +
                "&endPersianDate=" +
                "";
            return;
        }

        command.callbackUrl =
            controllerName +
            "Grid" +
            "?dateType=" +
            state.datePeriodType +
            "&startShamsiDate=" +
            state.persianStartDate +
            "&endPersianDate=" +
            state.persianEndDate;
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
        //FilterForm
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

    function showActionHistoryModal() {
        var url = controllerName + "ShowActionHistory";
        var apiParam = {
            workOrderId: state.workOrderId
        };
        var title = "مشاهده عملیات انجام شده";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom);
    }

    function showUsedToolsModal() {
        var url = controllerName + "WorKOrderConsumableList";
        var apiParam = {
            workOrderId: state.workOrderId
        };
        var title = "مشاهده مواد مصرفی";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom);
    }

    function showHistoryModal() {
        var url = controllerName + "ShowReferenceHistory";
        var apiParam = {
            workOrderId: state.workOrderId
        };
        var title = "مشاهده تاریخچه ارجاعات";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom);
    }

    function showRegisterInfoModal() {
        const title = "مشخصات ثبت کننده";
        const url = controllerName + "RegistrarInfoModal";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
    }
    function setWorkOrderGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);
        state.workOrderId = dom.grid.GetRowKey(event.visibleIndex);
    }
    $(document).ready(init);

    return {
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleGridBeginCallback: handleGridBeginCallback,
        setWorkOrderGridFocusedRowOnExpanding: setWorkOrderGridFocusedRowOnExpanding,
        showRegisterInfoModal: showRegisterInfoModal,
        showHistoryModal: showHistoryModal,
        showUsedToolsModal: showUsedToolsModal,
        showActionHistoryModal: showActionHistoryModal
    };

})();