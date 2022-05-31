/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/DevExpressIntellisense/jquery.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_LoadingModal.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.netExpert = (function () {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod",
        workOrderId: null,
        workOrderSerial: null,
        mainGridRowIndex: null,
        operationTypeComboDefaultValue: null,
        requestNumber: 0,
        requestYear: 0,
        gridSelectedValues: null,
        employeeSelectedValues: null,
        pmSchedulingInfoIDList: null,
        inspectionEmployeeSelectedValues: null,
        addPreventiveModalGridFocusedRowIndex: 0,
        addInspectionModalGridFocusedRowIndex: 0,
        actionOrDelayId: 0,
        eId: null,
        maintenanceGroupsOfUser: null,
        isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange: false,
        machineId: null
    };
    var textSeparator = ";";

    var tools = motorsazanClient.tools;
    var controllerName = "/NetExpert/";

    async function addNetFinishedStatusToWorkOrder(workOrderId, operationItemId, description) {
        const url = controllerName + "AddNetFinishedStatusToWorkOrder";

        const apiParam = {
            workOrderId,
            operationItemId,
            description
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        motorsazanClient.contentModal.hideModal();
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = false;

        await fillMainGrid();

        motorsazanClient.messageModal.success(apiResult);
    }

    function convertSelectedListToArray() {
        var count = state.gridSelectedValues.length;
        var result = new Array(count);

        for (var i = 0; i < count; i++) {
            result[i] = {
                StockCode: state.gridSelectedValues[i][3],
                RequestCount: state.gridSelectedValues[i][4],
                RemainingCount: state.gridSelectedValues[i][5],
                DeliverLocation: state.gridSelectedValues[i][6],
                RequestCode: state.gridSelectedValues[i][7],
                RequestYear: state.gridSelectedValues[i][8],
                StockClass: state.gridSelectedValues[i][9],
                StockFinalSerialNO: state.gridSelectedValues[i][10]
            };
        }

        return result;
    }

    function convertSelectedPmSchedulingIdToArray() {
        var count = state.pmSchedulingInfoIDList.length;
        var result = new Array(count);

        for (var i = 0; i < count; i++) {
            result[i] = {
                PMSchedulingInfoId: state.pmSchedulingInfoIDList[i]
            };
        }

        return result;
    }

    function convertSelectedEmployeeListToArray() {
        var count = state.employeeSelectedValues.length;
        var result = new Array(count);

        for (var i = 0; i < count; i++) {
            result[i] = {
                EmployeeId: state.employeeSelectedValues[i]
            };
        }

        return result;
    }
    function convertInspectionSelectedEmployeeListToArray() {
        var count = state.inspectionEmployeeSelectedValues.length;
        var result = new Array(count);

        for (var i = 0; i < count; i++) {
            result[i] = {
                EmployeeId: state.inspectionEmployeeSelectedValues[i]
            };
        }

        return result;
    }

    function detectSignalRDisconnection(connection) {
        if (connection.newState === $.signalR.connectionState.reconnecting) {
            motorsazanClient.loadingModal.hide();
            motorsazanClient.loadingModal.show("درحال ایجاد ارتباط با سرور...");
        }
        else if (connection.newState === $.signalR.connectionState.connected) {
            motorsazanClient.loadingModal.hide();
            state.signalRReconnectAttemptCount = 0;
        }
        else if (connection.newState === $.signalR.connectionState.disconnected) {

            if (state.signalRReconnectAttemptCount === 2) {
                motorsazanClient.loadingModal.hide();
                motorsazanClient.messageModal
                    .error("مشکل در ایجاد ارتباط با سرور، لطفا دوباره صفحه را بارگذاری کنید. <br> در صورت مشاهده دوباره این مشکل با تیم توسعه تماس حاصل فرمایید");
            }
            else {
                setTimeout(startSignalRHub, 5000);
                state.signalRReconnectAttemptCount++;
            }
        }
    }

    function fillMainGrid() {
        const url = controllerName + "MainGrid";

        const status = dom.filterFormStatusCombo.GetValue();
        const datePeriodType = dom.filterFormDateCombo.GetValue();
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();

        const apiParam = {
            WorkOrderStatusTypeId: status,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (mainGrid) {
                dom.mainGridParent.html(mainGrid);
                setDom();
            });
    }

    async function fillStockWaitingSGrid() {
        var url = controllerName + "StockWaitingModalGrid";
        state.requestNumber = dom.requestNumber.GetValue();
        state.requestYear = dom.requestYear.GetValue();
        const apiParam = {
            requestNumber: state.requestNumber,
            requestYear: state.requestYear
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.stockWaitingModalGridParent.html(apiResult);
        setStockWaitingModal();
    }


    function handleFilterFormDateComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDateComboError);
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        const selectedDateId = dom.filterFormDateCombo.GetValue();

        if (selectedDateId === state.customPeriodId) {
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

    function handleFilterFormStatusComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormStatusComboError);

        const status = dom.filterFormStatusCombo.GetValue();
        const isStatusSelected = !tools.isNullOrEmpty(status);
        if (!isStatusSelected) {
            tools.showItem(dom.filterFormStatusComboError);
        }
    }

    function handleFilterFormSearchButtonClick() {
        if (!isFilterFormValid()) {
            return;
        }

        fillMainGrid();

    }

    function handleStockWaitingSearchButtonClick() {
        if (!isStockWaitingFormValid()) {
            return;
        }

        fillStockWaitingSGrid();

    }

    function handleMainGridDetailRowPerformedOperationsBtnClick() {
        const title = "مشاهده عملیات انجام شده";
        const url = controllerName + "PerformedOperationsGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        }

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleMainGridDetailRowRegistrarInfoBtnClick() {
        const title = "مشخصات ثبت کننده";
        const url = controllerName + "RegistrarInfoModal";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleUpdateConsumableList() {
        dom.consumingMaterialsGrid.UpdateEdit();


    }

    function handleToolbarButtonClick(s, e) {
        if (e.item.name === "gridToolbarChangeSaveButton") {
            dom.consumingMaterialsGrid.UpdateEdit();
        }

        if (e.item.name === "gridToolbarChangeCancelButton") {
            dom.consumingMaterialsGrid.CancelEdit();
        }
        return true;
    }

    function handleMainGridDetailRowConsumingMaterialsBtnClick() {
        const title = "مشاهده مواد مصرفی";
        const url = controllerName + "ConsumingMaterialsGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setConsumeDom, false, false);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleConsumingMaterialsGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "ConsumingMaterialsGrid"
            + "?workOrderId=" + state.workOrderId;
    }

    function handleMainGridDetailRowReferralsHistoryBtnClick() {
        const title = "مشاهده تاریخچه ارجاعات";
        const url = controllerName + "ReferralsHistoryGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleMainGridDetailRowReferToAnotherGroupBtnClick() {
        const title = "ارجاع به گروه دیگر";
        const url = controllerName + "ReferToAnotherGroupModal";

        motorsazanClient.contentModal.ajaxShow(title, url, null, initReferToAnotherGroupModalDom);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleMainGridDetailRowReferToAnotherPersonBtnClick() {
        const title = "ارجاع به فرد دیگر";
        const url = controllerName + "ReferToAnotherPersonModal";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setReferToAnotherPersonModalDom);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleMainGridDetailRowAddOperationsBtnClick() {
        const title = "ثبت عملیات انجام شده";
        const url = controllerName + "AddOperationsModal";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, initAddOperationsModal, false, true);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleMainGridDetailRowAddPreventiveDetailsBtnClick() {
        const workOrderId = state.workOrderId;
        const title = "ثبت جزئیات پیشگیرانه";
        const url = controllerName + "AddPreventiveDetailsModal";
        const apiParam = {
            workOrderId
        };
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, initPreventiveDetails, false, true);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function initPreventiveDetails() {
        dom.addPreventiveItemsComboBoxError = $("#addPreventiveItemsComboBoxError");
        dom.addPreventiveItemsComboBox = ASPxClientComboBox.Cast("addPreventiveItemsComboBox");

        dom.itemCheckComboBox = ASPxClientListBox.Cast("itemCheckComboBox");
        dom.checkComboBox = ASPxClientListBox.Cast("checkComboBox");
        dom.addPreventiveItemsComboBoxError = $("#addPreventiveItemsComboBoxError");


        dom.addOperationsModalEmployeeCheckComboBox = ASPxClientListBox.Cast("addOperationsModalEmployeeCheckComboBox");
        dom.addOperationsModalEmployeeCheckComboBoxError = $("#addOperationsModalEmployeeCheckComboBoxError");


        dom.addPreventiveEmployeeComboBoxError = $("#addPreventiveEmployeeComboBoxError");
        dom.addPreventiveEmployeeComboBox = ASPxClientComboBox.Cast("addPreventiveEmployeeComboBox");

        dom.addPreventiveModalStartDatePicker = $("#addPreventiveModalStartDatePicker");
        dom.addPreventiveModalStartDatePickerError = $("#addPreventiveModalStartDatePickerError");


        dom.addFormStartPeriodInMinuteSpinEditError = $("#addFormStartPeriodInMinuteSpinEditError");
        dom.addFormStartPeriodInMinuteSpinEditRangeError = $("#addFormStartPeriodInMinuteSpinEditRangeError");
        dom.addFormStartPeriodInMinuteSpinEdit = ASPxClientTextBox.Cast("addFormStartPeriodInMinuteSpinEdit");

        dom.addPreventiveModalEndDatePicker = $("#addPreventiveModalEndDatePicker");
        dom.addPreventiveModalEndDatePickerError = $("#addPreventiveModalEndDatePickerError");


        dom.addPreventiveDetailsModalRunEndTimeSpinEditError = $("#addPreventiveDetailsModalRunEndTimeSpinEditError");
        dom.addFormEndPeriodInMinuteSpinEditRangeError = $("#addFormEndPeriodInMinuteSpinEditRangeError");
        dom.addPreventiveDetailsModalRunEndTimeSpinEdit = ASPxClientTextBox.Cast("addPreventiveDetailsModalRunEndTimeSpinEdit");


        dom.durationInMinuteError = $("#durationInMinuteError");
        dom.durationInMinuteRangeError = $("#durationInMinuteRangeError");
        dom.durationInMinute = ASPxClientSpinEdit.Cast("durationInMinute");


        dom.referToAnotherGroupPreventiveModalDescriptionMemoError = $("#referToAnotherGroupPreventiveModalDescriptionMemoError");
        dom.referToAnotherGroupPreventiveModalDescriptionMemo = ASPxClientMemo.Cast("referToAnotherGroupPreventiveModalDescriptionMemo");


        dom.addPreventiveModalGridParent = $("#addPreventiveModalGridParent");
        dom.addPreventiveModalGrid = ASPxClientGridView.Cast("addPreventiveModalGrid");

        tools.initDatePicker("addPreventiveModalStartDatePicker");
        tools.initDatePicker("addPreventiveModalEndDatePicker");


        dom.gridPreventiveToolbarChangeSaveButton = addPreventiveModalGrid.GetToolbar(0)
            .GetItemByName("gridPreventiveToolbarChangeSaveButton");

        dom.gridPreventiveToolbarChangeCancelButton = addPreventiveModalGrid.GetToolbar(0)
            .GetItemByName("gridPreventiveToolbarChangeCancelButton");

    }

    function handleMainGridDetailRowAddInspectionDetailsBtnClick() {
        const workOrderId = state.workOrderId;
        const title = "ثبت جزئیات بازرسی";
        const url = controllerName + "AddInspectionDetailsModal";
        const apiParam = {
            workOrderId
        };
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, initInpectionDetails, false, true);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function initInpectionDetails() {
        dom.addInspectionItemsComboBoxError = $("#addInspectionItemsComboBoxError");
        dom.addInspectionItemsComboBox = ASPxClientComboBox.Cast("addInspectionItemsComboBox");

        dom.addInspectionModalEmployeeCheckComboBoxError = $("#addInspectionModalEmployeeCheckComboBoxError");
        dom.addInspectionModalEmployeeCheckComboBox = ASPxClientComboBox.Cast("addInspectionModalEmployeeCheckComboBox");

        dom.addFormInspectionPeriodStartDatePicker = $("#addFormInspectionPeriodStartDatePicker");
        dom.addFormInspectionPeriodStartDatePickerError = $("#addFormInspectionPeriodStartDatePickerError");


        dom.addFormInspectionStartPeriodInMinuteSpinEditError = $("#addFormInspectionStartPeriodInMinuteSpinEditError");
        dom.addFormInspectionStartPeriodInMinuteSpinEdit = ASPxClientTextBox.Cast("addFormInspectionStartPeriodInMinuteSpinEdit");

        dom.addFormInspectionPeriodEndDatePicker = $("#addFormInspectionPeriodEndDatePicker");
        dom.addFormInspectionPeriodEndDatePickerError = $("#addFormInspectionPeriodEndDatePickerError");


        dom.addInspectionDetailsModalRunEndTimeSpinEditError = $("#addInspectionDetailsModalRunEndTimeSpinEditError");
        dom.addInspectionDetailsModalRunEndTimeSpinEdit = ASPxClientTextBox.Cast("addInspectionDetailsModalRunEndTimeSpinEdit");


        dom.inspectionDurationInMinuteError = $("#inspectionDurationInMinuteError");
        dom.inspectionDurationInMinuteRangeError = $("#inspectionDurationInMinuteRangeError");
        dom.inspectionDurationInMinute = ASPxClientSpinEdit.Cast("inspectionDurationInMinute");


        dom.inspectionModalDescriptionMemoError = $("#inspectionModalDescriptionMemoError");
        dom.inspectionModalDescriptionMemo = ASPxClientMemo.Cast("inspectionModalDescriptionMemo");


        dom.addInspectionModalGridParent = $("#addInspectionModalGridParent");
        dom.addInspectionModalGrid = ASPxClientGridView.Cast("addInspectionModalGrid");

        tools.initDatePicker("addFormInspectionPeriodStartDatePicker");
        tools.initDatePicker("addFormInspectionPeriodEndDatePicker");
    }

    function handleMainGridDetailRowStockWaitingBtnClick() {
        const title = "منتظر قطعه";
        const url = controllerName + "StockWaitingModal";

        motorsazanClient.contentModal.ajaxShow(title, url, {}, setStockWaitingModal, false, true);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleMainGridDetailRowEndStockWaitingBtnClick() {
        const content = "آیا از تغییر وضعیت این قطعه مطمئن هستید؟";
        const title = "تاییدیه خروج از حالت منتظر قطعه";
        motorsazanClient.messageModal.confirm(content, FinishingStockWaitingForWorkOrderModal, title);

    }

    async function FinishingStockWaitingForWorkOrderModal() {
        const url = controllerName + "FinishingStockWaitingForWorkOrder";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        await fillMainGrid();
        tools.showItem(dom.mainGridDetailRowStockWaiting);
        tools.hideItem(dom.mainGridDetailRowEndStockWaiting);

        motorsazanClient.messageModal.success(apiResult);
    }

    function setMainGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);

        const activeIndex = event.visibleIndex;
        state.workOrderId = Number(dom.mainGrid.GetRowKey(activeIndex));
        state.mainGridRowIndex = activeIndex;

        dom.mainGrid.GetRowValues(activeIndex,
            [
                "WorkOrderSerial;" +
                "MachineId"
            ],
            async (values) => {
                const [workOrderSerial, machineId, departmentId] = values;
                state.workOrderSerial = workOrderSerial;
                state.machineId = machineId;
            });
    }

    function handleMainGridBeginCallback(command) {
        if (!isFilterFormValid())
            return;

        motorsazanClient.loadingModal.show();
        const status =
            +dom.filterFormStatusCombo.GetValue() > -1
                ? dom.filterFormStatusCombo.GetValue()
                : -1;

        const datePeriodType = dom.filterFormDateCombo.GetValue();
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();

        command.callbackUrl =
            controllerName + "MainGrid"
            + "?workOrderStatusTypeId=" + status
            + "&persianStartDate=" + persianStartDate
            + "&persianEndDate=" + persianEndDate
            + "&datePeriodType=" + datePeriodType;
    }

    function handleMainGridEndCallback() {
        motorsazanClient.loadingModal.hide();
    }

    function handlePerformedOperationsGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "PerformedOperationsGrid" + "?WorkOrderId=" + state.workOrderId;
    }

    function handleGridSelectionChanged(source, e) {
        source.GetSelectedFieldValues("StockName;RackCode;TechNO;StockCode;RequestCount;RemainingCount;DeliverLocation;RequestCode;RequestYear;StockClass;FinalSerialNO",
            (values) => {
                state.gridSelectedValues = values;
                if (values.length < 1) {
                    dom.stockWaitingModalFilterFormSaveBtn.SetEnabled(false);
                } else {
                    dom.stockWaitingModalFilterFormSaveBtn.SetEnabled(true);
                }
            });
    }

    function handleWaitingStockGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "StockWaitingModalGrid" + "?requestNumber=" + state.requestNumber +
            "&requestYear=" + state.requestYear;
    }

    function handlePreventiveGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "AddPreventiveDetailsModalGrid" + "?workOrderId=" + state.workOrderId

    }

    function handleInspectionGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "AddInspectionDetailsModalGrid" + "?workOrderId=" + state.workOrderId

    }

    function handleReferralsHistoryGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "ReferralsHistoryGrid" + "?WorkOrderId=" + state.workOrderId;
    }

    function initReferToAnotherGroupModalDom() {
        setReferToAnotherGroupModalDom();

        const rowIndex = state.mainGridRowIndex;

        dom.mainGrid.GetRowValues(rowIndex,
            [
                "WorkOrderSerial;" +
                "WorkOrderTypeTitle"
            ],
            async (values) => {
                const [workOrderSerial, workOrderTypeTitle] = values;

                dom.referToAnotherGroupModalWorkOrderSerialLabel.text(workOrderSerial);
                dom.referToAnotherGroupModalWorkOrderTypeTitleLabel.text(workOrderTypeTitle);
            });
    }

    function initAddOperationsModal() {
        setAddOperationsModalDom();

        state.operationTypeComboDefaultValue = dom.addOperationsModalOperationTypeComboDefaultValueHidden.val();

        tools.initDatePicker("addOperationsModalStartDatePicker");
        tools.initDatePicker("addOperationsModalEndDatePicker");

        dom.addOperationsModalStartDatePicker.on("change", handleAddOperationsModalStartDatePickerChange);
        dom.addOperationsModalEndDatePicker.on("change", handleAddOperationsModalEndDatePickerChange);
    }

    function handleAddOperationsModalStartDatePickerChange() {
        tools.hideItem(dom.addOperationsModalStartDatePickerError);

        const date = dom.addOperationsModalStartDatePicker.val();
        const dateHasValue = !tools.isNullOrEmpty(date);
        if (!dateHasValue) {
            tools.showItem(dom.addOperationsModalStartDatePickerError);
        }
    }

    function handleAddOperationsModalEndDatePickerChange() {
        tools.hideItem(dom.addOperationsModalEndDatePickerError);

        const date = dom.addOperationsModalEndDatePicker.val();
        const dateHasValue = !tools.isNullOrEmpty(date);
        if (!dateHasValue) {
            tools.showItem(dom.addOperationsModalEndDatePickerError);
        }
    }

    function validateAddOperationsModalOperationsDoneTimeSpinEdit() {
        let isValid = true;

        const periodInMinute = dom.addOperationsModalOperationsDoneTimeSpinEdit.GetValue();

        if (periodInMinute < 1) {
            isValid = false;
            tools.showItem(dom.addOperationsModalOperationsDoneTimeSpinEditRangeError);
        }
        else {
            tools.hideItem(dom.addOperationsModalOperationsDoneTimeSpinEditRangeError);
        }

        if (tools.isNullOrEmpty(periodInMinute)) {
            tools.showItem(dom.addOperationsModalOperationsDoneTimeSpinEditError);
            tools.hideItem(dom.addOperationsModalOperationsDoneTimeSpinEditRangeError);
        }
        else {
            const durationInMinute = +dom.addOperationsModalDurationInMinuteLabel.text();
            if (periodInMinute > durationInMinute) {
                isValid = false;
                tools.showItem(dom.addOperationsModalOperationsDoneTimeSpinEditMoreThanDurationInMinuteError);
            }
            else {
                tools.hideItem(dom.addOperationsModalOperationsDoneTimeSpinEditMoreThanDurationInMinuteError);
            }
        }

        if (periodInMinute > 0) {
            tools.hideItem(dom.addOperationsModalOperationsDoneTimeSpinEditError);
        }

        return isValid;
    }

    function handleAddOperationsModalOperationsDoneTimeSpinEditKeyUp() {
        validateAddOperationsModalOperationsDoneTimeSpinEdit();
    }

    function handleAddOperationsModalOperationsDoneTimeSpinEditLostFocus() {
        validateAddOperationsModalOperationsDoneTimeSpinEdit();
    }

    function handleAddOperationsModalOperationsDoneTimeSpinEditNumberChanged() {
        validateAddOperationsModalOperationsDoneTimeSpinEdit();
    }

    function handleAddOperationsModalGridRemoveCustomBtnClick(_, event) {
        var selectedWokOrderRowIndex = event.visibleIndex;
        state.actionOrDelayId = dom.addOperationsModalGrid.GetRowKey(selectedWokOrderRowIndex);
        const customBtnId = event.buttonID;
        if (customBtnId === "addOperationsModalGridRemoveCustomBtn")
            return showOperationsRemoveConfirm();
    }

    function handlePreventiveGridCustomBtnClick(_, event) {

        var selectedWokOrderRowIndex = event.visibleIndex;
        state.actionOrDelayId = dom.addPreventiveModalGrid.GetRowKey(selectedWokOrderRowIndex);

        const customBtnId = event.buttonID;
        if (customBtnId === "preventiveItemGridRemoveCustomBtn")
            return showPreventiveItemRemoveConfirm();

    }

    function handleInspectionGridCustomBtnClick(_, event) {
        state.GridFocusedRowIndex =
            dom.addInspectionModalGrid.GetFocusedRowIndex();

        var selectedWokOrderRowIndex = event.visibleIndex;
        state.actionOrDelayId = dom.addInspectionModalGrid.GetRowKey(selectedWokOrderRowIndex);
        const customBtnId = event.buttonID;
        if (customBtnId === "inspectionItemGridRemoveCustomBtn")
            return showInspectionItemRemoveConfirm();

    }

    function handleMainGridDetailRowCancelBtnClick() {
        const title = "ابطال";

        const url = controllerName + "WorkOrderCancelModal";

        const apiParam = {};

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setWorkOrderCancelModal);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleWorkOrderTempFinishModalTempFinishBtnClick() {
        if (!isTempFinishModalValid())
            return;

        const workOrderId = state.workOrderId;
        const operationItemId = dom.workOrderTempFinishModalOperationItemCombo.GetValue();
        const description = dom.workOrderTempFinishModalDescriptionMemo.GetValue();

        addNetFinishedStatusToWorkOrder(workOrderId, operationItemId, description);
    }

    function handleWorkOrderTempFinishModalWithDescriptionBtnClick() {
        if (!isTempFinishWithDescriptionModalValid())
            return;

        const workOrderId = state.workOrderId;
        const description = dom.workOrderTempFinishWithDescriptionModalDescriptionMemo.GetValue();
        const operationItemId = null;
        addNetFinishedStatusToWorkOrder(workOrderId, operationItemId, description);
    }

    function isTempFinishModalValid() {
        let isValid = true;

        tools.hideItem(dom.workOrderTempFinishModalMaintenanceGroupComboError);

        const maintenanceGroup = dom.workOrderTempFinishModalMaintenanceGroupCombo.GetValue();
        const isMaintenanceGroupSelected = !tools.isNullOrEmpty(maintenanceGroup);
        if (!isMaintenanceGroupSelected) {
            tools.showItem(dom.workOrderTempFinishModalMaintenanceGroupComboError);
            isValid = false;
        }

        tools.hideItem(dom.workOrderTempFinishModalOperationItemComboError);

        const operationItem = dom.workOrderTempFinishModalOperationItemCombo.GetValue();
        const isOperationItemSelected = !tools.isNullOrEmpty(operationItem);
        if (!isOperationItemSelected) {
            tools.showItem(dom.workOrderTempFinishModalOperationItemComboError);
            isValid = false;
        }

        tools.hideItem(dom.workOrderTempFinishModalDescriptionMemoError);

        const description = dom.workOrderTempFinishModalDescriptionMemo.GetValue();
        const isDescriptionEntered = !tools.isNullOrEmpty(description);
        if (!isDescriptionEntered) {
            tools.showItem(dom.workOrderTempFinishModalDescriptionMemoError);
            isValid = false;
        }

        return isValid;
    }

    function isTempFinishWithDescriptionModalValid() {
        let isValid = true;
        tools.hideItem(dom.workOrderTempFinishWithDescriptionModalDescriptionMemoError);

        const description = dom.workOrderTempFinishWithDescriptionModalDescriptionMemo.GetValue();
        const isDescriptionEntered = !tools.isNullOrEmpty(description);
        if (!isDescriptionEntered) {
            tools.showItem(dom.workOrderTempFinishWithDescriptionModalDescriptionMemoError);
            isValid = false;
        }

        return isValid;
    }

    function handleWorkOrderCancelModalCancelBtnClick() {
        if (!isWorkOrderCancelModalValid())
            return;

        addCancelStatusToWorkOrder();
    }

    function handleWorkOrderCancelModalDescriptionMemoKeyUp() {
        tools.hideItem(dom.workOrderCancelModalDescriptionMemoError);

        const description = dom.workOrderCancelModalDescriptionMemo.GetValue();
        const descriptionHasValue = !tools.isNullOrEmpty(description);
        if (!descriptionHasValue) {
            tools.showItem(dom.workOrderCancelModalDescriptionMemoError);
        }
    }

    function handleWorkOrderTempFinishModalDescriptionMemoKeyUp() {
        tools.hideItem(dom.workOrderTempFinishModalDescriptionMemoError);

        const description = dom.workOrderTempFinishModalDescriptionMemo.GetValue();
        const descriptionHasValue = !tools.isNullOrEmpty(description);
        if (!descriptionHasValue) {
            tools.showItem(dom.workOrderTempFinishModalDescriptionMemoError);
        }
    }

    function handleWorkOrderTempFinishWithDescriptionModalDescriptionMemoKeyUp() {
        tools.hideItem(dom.workOrderTempFinishWithDescriptionModalDescriptionMemoError);

        const description = dom.workOrderTempFinishWithDescriptionModalDescriptionMemo.GetValue();
        const descriptionHasValue = !tools.isNullOrEmpty(description);
        if (!descriptionHasValue) {
            tools.showItem(dom.workOrderTempFinishWithDescriptionModalDescriptionMemoError);
        }
    }

    function handleAddPreventiveDetailsModalSaveBtnClick() {
        if (!isPreventiveDetailModalValid())
            return;
        addPreventiveMaintenanceActionToWorkOrder();

    }

    function handleAddInspectionDetailsModalSaveBtnClick() {
        if (!isInspectionDetailModalValid())
            return;
        addInspectionMaintenanceActionToWorkOrder();

    }

    function handleMainGridDetailRowImmediateTempFinishBtnClick() {
        const title = "اتمام کارنت";
        const url = controllerName + "WorkOrderTempFinishWithDescriptionModal";

        const apiParam = {}

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, initWorkOrderTempFinisWithDescriptionhModal);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    async function handleMainGridDetailRowTempFinishBtnClick() {
        const isImmediateTempFinish = !(await CheckMachineHasOperationItem(state.machineId));
        if (isImmediateTempFinish) {
            await handleMainGridDetailRowImmediateTempFinishBtnClick();
            return;
        }

        const title = "اتمام کارنت";
        const url = controllerName + "WorkOrderTempFinishModal";

        const apiParam = {}

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, initWorkOrderTempFinishModal);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    async function CheckMachineHasOperationItem(machineId) {
        const url = controllerName + "CheckMachineHasOperationItem";

        const apiParam = {
            machineId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        return apiResult.HasOperationItem;
    }

    function initWorkOrderTempFinisWithDescriptionhModal() {
        dom.workOrderTempFinishWithDescriptionModalDescriptionMemo = ASPxClientMemo.Cast("workOrderTempFinishWithDescriptionModalDescriptionMemo");
        dom.workOrderTempFinishWithDescriptionModalDescriptionMemoError = $("#workOrderTempFinishWithDescriptionModalDescriptionMemoError");
    }

    function initWorkOrderTempFinishModal() {
        setWorkOrderTempFinishModalDom();

        dom.workOrderTempFinishModalOperationItemCombo.SetEnabled(false);
        dom.workOrderTempFinishModalDescriptionMemo.SetEnabled(false);
    }

    function setWorkOrderTempFinishModalDom() {
        dom.workOrderTempFinishModalMaintenanceGroupCombo = ASPxClientComboBox.Cast("workOrderTempFinishModalMaintenanceGroupCombo");
        dom.workOrderTempFinishModalMaintenanceGroupComboError = $("#workOrderTempFinishModalMaintenanceGroupComboError");

        dom.workOrderTempFinishModalOperationItemComboParent = $("#workOrderTempFinishModalOperationItemComboParent");
        dom.workOrderTempFinishModalOperationItemCombo = ASPxClientComboBox.Cast("workOrderTempFinishModalOperationItemCombo");
        dom.workOrderTempFinishModalOperationItemComboError = $("#workOrderTempFinishModalOperationItemComboError");


        dom.workOrderTempFinishModalDescriptionMemo = ASPxClientMemo.Cast("workOrderTempFinishModalDescriptionMemo");
        dom.workOrderTempFinishModalDescriptionMemoError = $("#workOrderTempFinishModalDescriptionMemoError");
    }

    async function handleWorkOrderTempFinishModalMaintenanceGroupComboSelectedIndexChange() {
        tools.hideItem(dom.workOrderTempFinishModalMaintenanceGroupComboError);

        const maintenanceGroupId = dom.workOrderTempFinishModalMaintenanceGroupCombo.GetValue();
        const isMaintenanceGroupSelected = !tools.isNullOrEmpty(maintenanceGroupId);
        if (!isMaintenanceGroupSelected) {
            tools.showItem(dom.workOrderTempFinishModalMaintenanceGroupComboError);
            return;
        }

        const url = controllerName + "WorkOrderTempFinishModalOperationItemCombo";

        const apiParam = {
            maintenanceGroupId,
            machineId: state.machineId
        };

        const operationItemCombo = await motorsazanClient.connector.post(url, apiParam);

        dom.workOrderTempFinishModalOperationItemComboParent.html(operationItemCombo);

        setWorkOrderTempFinishModalDom();
    }

    function handleWorkOrderTempFinishModalOperationItemComboSelectedIndexChange() {
        tools.hideItem(dom.workOrderTempFinishModalOperationItemComboError);

        dom.workOrderTempFinishModalDescriptionMemo.SetEnabled(true);
    }

    function isPreventiveDetailModalValid() {
        var isValid = true;

        tools.hideItem(dom.addPreventiveItemsComboBoxError);
        const preventiveItem = dom.addPreventiveItemsComboBox.GetValue();
        const isPreventiveItemSelected = !tools.isNullOrEmpty(preventiveItem);
        if (!isPreventiveItemSelected) {
            isValid = false;
            tools.showItem(dom.addPreventiveItemsComboBoxError);
        }


        tools.hideItem(dom.addPreventiveEmployeeComboBoxError);
        const employee = dom.addPreventiveEmployeeComboBox.GetValue();
        const isEmployeeSelected = !tools.isNullOrEmpty(employee);
        if (!isEmployeeSelected) {
            isValid = false;
            tools.showItem(dom.addPreventiveEmployeeComboBoxError);
        }

        tools.hideItem(dom.referToAnotherGroupPreventiveModalDescriptionMemoError);
        const description = dom.referToAnotherGroupPreventiveModalDescriptionMemo.GetValue();
        const isDescriptionSelected = !tools.isNullOrEmpty(description);
        if (!isDescriptionSelected) {
            isValid = false;
            tools.showItem(dom.referToAnotherGroupPreventiveModalDescriptionMemoError);
        }

        tools.hideItem(dom.addPreventiveModalStartDatePickerError);
        const periodStart = dom.addPreventiveModalStartDatePicker.val();
        const isPeriodStartSelected = !tools.isNullOrEmpty(periodStart);
        if (!isPeriodStartSelected) {
            isValid = false;
            tools.showItem(dom.addPreventiveModalStartDatePickerError);
        }

        tools.hideItem(dom.addPreventiveModalEndDatePickerError);
        const periodEnd = dom.addPreventiveModalEndDatePicker.val();
        const isPeriodEndSelected = !tools.isNullOrEmpty(periodEnd);
        if (!isPeriodEndSelected) {
            isValid = false;
            tools.showItem(dom.addPreventiveModalEndDatePickerError);
        }

        tools.hideItem(dom.durationInMinuteError);
        const durationInMinute = dom.durationInMinute.GetValue();
        const isDurationInMinuteSelected = !tools.isNullOrEmpty(durationInMinute);
        if (!isDurationInMinuteSelected) {
            isValid = false;
            tools.showItem(dom.durationInMinuteError);
        }

        tools.hideItem(dom.addFormStartPeriodInMinuteSpinEditError);
        const startPeriodInMinute = dom.addFormStartPeriodInMinuteSpinEdit.GetValue();
        const isStartPeriodInMinute = !tools.isNullOrEmpty(startPeriodInMinute);
        if (!isStartPeriodInMinute) {
            isValid = false;
            tools.showItem(dom.addFormStartPeriodInMinuteSpinEditError);
        }


        tools.hideItem(dom.addPreventiveDetailsModalRunEndTimeSpinEditError);
        const endPeriodInMinute = dom.addPreventiveDetailsModalRunEndTimeSpinEdit.GetValue();
        const isEndPeriodInMinute = !tools.isNullOrEmpty(endPeriodInMinute);
        if (!isEndPeriodInMinute) {
            isValid = false;
            tools.showItem(dom.addPreventiveDetailsModalRunEndTimeSpinEditError);
        }

        return isValid;

    }

    function isInspectionDetailModalValid() {
        var isValid = true;

        tools.hideItem(dom.addInspectionItemsComboBoxError);
        const inspectionItem = dom.addInspectionItemsComboBox.GetValue();
        const isinspectionItemSelected = !tools.isNullOrEmpty(inspectionItem);
        if (!isinspectionItemSelected) {
            isValid = false;
            tools.showItem(dom.addInspectionItemsComboBoxError);
        }


        tools.hideItem(dom.addInspectionModalEmployeeCheckComboBoxError);
        const employee = dom.addInspectionModalEmployeeCheckComboBox.GetValue();
        const isEmployeeSelected = !tools.isNullOrEmpty(employee);
        if (!isEmployeeSelected) {
            isValid = false;
            tools.showItem(dom.addInspectionModalEmployeeCheckComboBoxError);
        }

        tools.hideItem(dom.inspectionModalDescriptionMemoError);
        const description = dom.inspectionModalDescriptionMemo.GetValue();
        const isDescriptionSelected = !tools.isNullOrEmpty(description);
        if (!isDescriptionSelected) {
            isValid = false;
            tools.showItem(dom.inspectionModalDescriptionMemoError);
        }

        tools.hideItem(dom.addFormInspectionPeriodStartDatePickerError);
        const periodStart = dom.addFormInspectionPeriodStartDatePicker.val();
        const isPeriodStartSelected = !tools.isNullOrEmpty(periodStart);
        if (!isPeriodStartSelected) {
            isValid = false;
            tools.showItem(dom.addFormInspectionPeriodStartDatePickerError);
        }

        tools.hideItem(dom.addFormInspectionPeriodEndDatePickerError);
        const periodEnd = dom.addFormInspectionPeriodEndDatePicker.val();
        const isPeriodEndSelected = !tools.isNullOrEmpty(periodEnd);
        if (!isPeriodEndSelected) {
            isValid = false;
            tools.showItem(dom.addFormInspectionPeriodEndDatePickerError);
        }

        tools.hideItem(dom.inspectionDurationInMinuteError);
        const durationInMinute = dom.inspectionDurationInMinute.GetValue();
        const isDurationInMinuteSelected = !tools.isNullOrEmpty(durationInMinute);
        if (!isDurationInMinuteSelected) {
            isValid = false;
            tools.showItem(dom.inspectionDurationInMinuteError);
        }

        tools.hideItem(dom.addFormInspectionStartPeriodInMinuteSpinEditError);
        const startPeriodInMinute = dom.addFormInspectionStartPeriodInMinuteSpinEdit.GetValue();
        const isStartPeriodInMinute = !tools.isNullOrEmpty(startPeriodInMinute);
        if (!isStartPeriodInMinute) {
            isValid = false;
            tools.showItem(dom.addFormInspectionStartPeriodInMinuteSpinEditError);
        }


        tools.hideItem(dom.addInspectionDetailsModalRunEndTimeSpinEditError);
        const endPeriodInMinute = dom.addInspectionDetailsModalRunEndTimeSpinEdit.GetValue();
        const isEndPeriodInMinute = !tools.isNullOrEmpty(endPeriodInMinute);
        if (!isEndPeriodInMinute) {
            isValid = false;
            tools.showItem(dom.addInspectionDetailsModalRunEndTimeSpinEditError);
        }

        return isValid;

    }

    function isWorkOrderCancelModalValid() {
        tools.hideItem(dom.workOrderCancelModalDescriptionMemoError);

        const description = dom.workOrderCancelModalDescriptionMemo.GetValue();
        const isDescriptionEntered = !tools.isNullOrEmpty(description);
        if (!isDescriptionEntered) {
            tools.showItem(dom.workOrderCancelModalDescriptionMemoError);

            return false;
        }

        return true;
    }

    function setWorkOrderCancelModal() {
        dom.workOrderCancelModalDescriptionMemo = ASPxClientMemo.Cast("workOrderCancelModalDescriptionMemo");
        dom.workOrderCancelModalDescriptionMemoError = $("#workOrderCancelModalDescriptionMemoError");

        dom.workOrderCancelModalCancelBtn = ASPxClientButton.Cast("workOrderCancelModalCancelBtn");
    }

    function showOperationsRemoveConfirm() {
        setAddOperationsModalDom();

        dom.addOperationsModalGrid.GetRowValues(dom.addOperationsModalGrid.GetFocusedRowIndex(),
            "ActionOrDelayListId",
            (values) => {
                motorsazanClient.messageModal.confirm("آیا از حذف این ردیف مطمئن هستید؟",
                    () => removeSparePartPopup(values),
                    "تاییدیه حذف");
            });
    }

    function showPreventiveItemRemoveConfirm() {
        const content = "آیا از حذف این ردیف مطمئن هستید؟";
        const title = "تاییدیه حذف";
        motorsazanClient.messageModal.confirm(content, removePreventiveActionOrDelayByActionOrDelayListId, title);
    }

    function showInspectionItemRemoveConfirm() {
        const content = "آیا از حذف این ردیف مطمئن هستید؟";
        const title = "تاییدیه حذف";
        motorsazanClient.messageModal.confirm(content, removeInspectionActionOrDelayByActionOrDelayListId, title);
    }

    function handleMainGridDetailRowFinalFinishBtnClick() {
        const content = "آیا از اتمام نهایی این سفارشکار مطمئن هستید؟";
        const title = "تاییدیه اتمام نهایی";
        motorsazanClient.messageModal.confirm(content, addRepairFinishedStatusToWorkOrder, title);
    }
    async function removePreventiveActionOrDelayByActionOrDelayListId() {
        const url = controllerName + "RemoveActionOrDelayByActionOrDelayListId";

        const actionOrDelayListId = state.actionOrDelayId;

        const apiParam = {
            actionOrDelayListId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        await fillAddPreventiveModalGrid();

        motorsazanClient.messageModal.success(apiResult);
    }

    function handleMainGridDetailRowShowStockBtnClick() {
        const title = "مشاهده قطعات";
        const url = controllerName + "StocksGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        }

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleStocksGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "StocksGrid" + "?WorkOrderId=" + state.workOrderId;
    }

    async function addRepairFinishedStatusToWorkOrder() {
        const url = controllerName + "AddRepairFinishedStatusToWorkOrder";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        await fillMainGrid();

        motorsazanClient.messageModal.success(apiResult);
    }

    async function removeSparePartPopup(values) {
        const url = controllerName + "RemoveActionOrDelayByActionOrDelayListId";
        const params = {
            actionOrDelayListId: values
        };

        const result = await motorsazanClient.connector.post(url, params);

        await fillAddOperationsModalGrid();
        motorsazanClient.messageModal.success(result);
    }



    async function removeInspectionActionOrDelayByActionOrDelayListId() {
        const url = controllerName + "RemoveActionOrDelayByActionOrDelayListId";

        const actionOrDelayListId = state.actionOrDelayId;

        const apiParam = {
            actionOrDelayListId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        await fillInspectionModalGrid();

        motorsazanClient.messageModal.success(apiResult);
    }

    async function addCancelStatusToWorkOrder() {
        const url = controllerName + "AddCancelStatusToWorkOrder";

        const workOrderId = state.workOrderId;
        const description = dom.workOrderCancelModalDescriptionMemo.GetValue();

        const apiParam = {
            workOrderId,
            description
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        motorsazanClient.contentModal.hideModal();
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = false;

        await fillMainGrid();

        motorsazanClient.messageModal.success(apiResult);
    }

    async function addInspectionMaintenanceActionToWorkOrder() {
        const url = controllerName + "addInspectionMaintenanceActionToWorkOrder";

        const workOrderId = state.workOrderId;
        const OperationItemId = dom.addInspectionItemsComboBox.GetValue();
        const description = dom.inspectionModalDescriptionMemo.GetValue();
        const StartTime = dom.addFormInspectionStartPeriodInMinuteSpinEdit.GetValue();
        const EndTime = dom.addInspectionDetailsModalRunEndTimeSpinEdit.GetValue();
        const DurationInMinute = dom.inspectionDurationInMinute.GetValue();
        const employeeIdList = dom.addInspectionModalEmployeeCheckComboBox.GetSelectedValues();
        state.inspectionEmployeeSelectedValues = employeeIdList;
        const ResposibleEmployeeIDList = convertInspectionSelectedEmployeeListToArray();
        const StarFatDate = dom.addFormInspectionPeriodStartDatePicker.val();
        const EndFaDate = dom.addFormInspectionPeriodEndDatePicker.val();
        const InspectionDetailId = dom.addInspectionItemsComboBox.GetValue();

        const apiParam = {
            WorkOrderId: workOrderId,
            InspectionDetailId: InspectionDetailId,
            OperationItemId: OperationItemId,
            ResposibleEmployeeIDList: ResposibleEmployeeIDList,
            Description: description,
            StarFatDate: StarFatDate,
            EndFaDate: EndFaDate,
            StartTime: StartTime,
            EndTime: EndTime,
            DurationInMinute: DurationInMinute
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        motorsazanClient.contentModal.hideModal();
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = false;

        await fillInspectionModalGrid();

        motorsazanClient.messageModal.success(apiResult);
    }

    async function addPreventiveMaintenanceActionToWorkOrder() {
        const url = controllerName + "AddPreventiveMaintenanceActionToWorkOrder";

        const workOrderId = state.workOrderId;
        const OperationItemIdList = dom.addPreventiveItemsComboBox.GetSelectedValues();
        state.pmSchedulingInfoIDList = OperationItemIdList;
        const SelectedPmSchedulingIdToArray = convertSelectedPmSchedulingIdToArray();
        const description = dom.referToAnotherGroupPreventiveModalDescriptionMemo.GetValue();
        const StartTime = dom.addFormStartPeriodInMinuteSpinEdit.GetValue();
        const EndTime = dom.addPreventiveDetailsModalRunEndTimeSpinEdit.GetValue();
        const DurationInMinute = dom.durationInMinute.GetValue();
        const employeeIdList = dom.addPreventiveEmployeeComboBox.GetSelectedValues();
        state.employeeSelectedValues = employeeIdList;
        const ResposibleEmployeeIDList = convertSelectedEmployeeListToArray();
        const StarFatDate = dom.addPreventiveModalStartDatePicker.val();
        const EndFaDate = dom.addPreventiveModalEndDatePicker.val();


        const apiParam = {
            WorkOrderId: workOrderId,
            PMSchedulingInfoIDList: SelectedPmSchedulingIdToArray,
            ResposibleEmployeeIDList: ResposibleEmployeeIDList,
            Description: description,
            StarFatDate: StarFatDate,
            EndFaDate: EndFaDate,
            StartTime: StartTime,
            EndTime: EndTime,
            DurationInMinute: DurationInMinute
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        //motorsazanClient.contentModal.hideModal();
        //state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = false;

        await fillAddPreventiveModalGrid();

        motorsazanClient.messageModal.success(apiResult);
    }

    async function fillAddOperationsModalGrid() {
        const url = controllerName + "AddOperationsModalGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        const grid = await motorsazanClient.connector.post(url, apiParam);

        dom.addOperationsModalGridParent.html(grid);
    }

    async function fillAddPreventiveModalGrid() {
        const url = controllerName + "AddPreventiveDetailsModalGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        const grid = await motorsazanClient.connector.post(url, apiParam);

        dom.addPreventiveModalGridParent.html(grid);
    }

    async function fillInspectionModalGrid() {
        const url = controllerName + "AddInspectionDetailsModalGrid";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        const grid = await motorsazanClient.connector.post(url, apiParam);

        dom.addInspectionModalGridParent.html(grid);
    }

    async function fillConsumingMaterialsGrid() {
        const url = controllerName + "ConsumingMaterialsGrid";

        const params = {
            workOrderId: state.workOrderId
        }; const gridHtml = await motorsazanClient.connector.post(url, params);

        dom.consumingMaterialsGrid.html(gridHtml);

        setDom();
    }

    function handleAddOperationsModalModalDescriptionMemoKeyUp() {
        tools.hideItem(dom.addOperationsModalModalDescriptionMemoError);

        const description = dom.addOperationsModalModalDescriptionMemo.GetValue();
        const descriptionHasValue = !tools.isNullOrEmpty(description);
        if (!descriptionHasValue) {
            tools.showItem(dom.addOperationsModalModalDescriptionMemoError);
        }
    }

    function handleAddOperationsModalDelayReasonComboSelectedIndexChange() {
        tools.hideItem(dom.addOperationsModalDelayReasonComboError);

        const delayReason = dom.addOperationsModalDelayReasonCombo.GetValue();
        const isDelayReasonSelected = !tools.isNullOrEmpty(delayReason);
        if (!isDelayReasonSelected) {
            tools.showItem(dom.addOperationsModalDelayReasonComboError);
        }
    }

    function handleAddOperationsModalGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "AddOperationsModalGrid"
            + "?WorkOrderId=" + state.workOrderId;
    }

    async function handleReferToAnotherGroupModalSaveBtnClick() {
        if (!isReferToAnotherGroupModalValid())
            return;

        const url = controllerName + "SendWorkOrderToMaintenanceGroup";

        const workOrderId = state.workOrderId;
        const maintenanceGroupId = dom.referToAnotherGroupModalGroupCombo.GetValue();
        const referralDescription = dom.referToAnotherGroupModalDescriptionMemo.GetValue();

        const apiParam = {
            workOrderId,
            maintenanceGroupId,
            referralDescription
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        dom.netExpertHub.server.workOrderReferredToAnotherMaintenanceGroup(maintenanceGroupId);

        motorsazanClient.contentModal.hide();
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = false;
        fillMainGrid();
        motorsazanClient.messageModal.success(apiResult);
    }

    async function handleStockWaitingModalFilterFormSaveBtnClick() {
        var canRegister = isStockWaitingFormValid();
        if (!canRegister) return;
        var itemlist = convertSelectedListToArray();
        var apiParam = {
            workOrderId: state.workOrderId,
            requestNumber: state.requestNumber,
            requestYear: state.requestYear,
            PurchaseDetails: itemlist
        };
        var url = "/NetExpert/AddStockWaitingStatusToWorkOrder";
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        motorsazanClient.messageModal.success(apiResult);
        motorsazanClient.contentModal.hideModal();
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = false;
        tools.hideItem(dom.mainGridDetailRowStockWaiting);
        tools.showItem(dom.mainGridDetailRowEndStockWaiting);
        fillMainGrid();
    }

    async function handleReferToAnotherPersonModalSaveBtnClick() {
        if (!isReferToAnotherPersonModalValid())
            return;

        const url = controllerName + "SendWorkOrderToMaintenanceGroupMember";

        const workOrderId = state.workOrderId;
        const maintenanceGroupId = dom.referToAnotherPersonModalReceiverMaintenanceGroupIdHidden.val();
        const description = dom.referToAnotherPersonModalDescriptionMemo.GetValue();
        const receiverEmployeeId = dom.referToAnotherPersonModalPersonCombo.GetValue();
        const receiverEId = dom.referToAnotherPersonModalPersonCombo.GetSelectedItem().texts[0];

        const apiParam = {
            workOrderId,
            maintenanceGroupId,
            description,
            receiverEmployeeId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        dom.netExpertHub.server.workOrderReferredToAnotherPerson(receiverEId);

        motorsazanClient.contentModal.hide();
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = false;
        fillMainGrid();
        motorsazanClient.messageModal.success(apiResult);
    }

    function handleAddOperationsModalEmployeeCheckComboBoxSelectedIndexChange() {
        tools.hideItem(dom.addPreventiveEmployeeComboBoxError);

        const employee = dom.addPreventiveEmployeeComboBox.GetSelectedValues();
        const isEmployeeSelected = !tools.isNullOrEmpty(employee);
        if (!isEmployeeSelected) {
            tools.showItem(dom.addPreventiveEmployeeComboBoxError);
        }
    }


    function handleAddOperationsModalOperationItemCheckComboBoxSelectedIndexChange() {
        tools.hideItem(dom.addPreventiveItemsComboBoxError);

        const preventiveItems = dom.addPreventiveItemsComboBox.GetSelectedValues();
        const isPreventiveItemsSelected = !tools.isNullOrEmpty(preventiveItems);

        if (!isPreventiveItemsSelected) {
            tools.showItem(dom.addPreventiveItemsComboBoxError);
        }
    }

    function handleAddInspectionModalEmployeeCheckComboBoxSelectedIndexChange() {
        tools.hideItem(dom.addInspectionModalEmployeeCheckComboBoxError);

        const employee = dom.addInspectionModalEmployeeCheckComboBox.GetSelectedValues();
        const isEmployeeSelected = !tools.isNullOrEmpty(employee);
        if (!isEmployeeSelected) {
            tools.showItem(dom.addInspectionModalEmployeeCheckComboBoxError);
        }
    }

    function handleAddOperationsModalOperationTypeComboSelectedIndexChange() {
        tools.hideItem(dom.addOperationsModalOperationTypeComboError);

        const operationType = dom.addOperationsModalOperationTypeCombo.GetValue();
        const isOperationTypeSelected = !tools.isNullOrEmpty(operationType);
        if (!isOperationTypeSelected) {
            tools.showItem(dom.addOperationsModalOperationTypeComboError);
            return;
        }

        if (operationType === state.operationTypeComboDefaultValue) {
            tools.hideItem(dom.addOperationsModalDelayReasonComboParent);
            dom.addOperationsModalDelayReasonCombo.SetValue();
        }
        else {
            tools.showItem(dom.addOperationsModalDelayReasonComboParent);
        }
    }

    async function handleAddOperationsModalSaveBtnClick() {
        if (!isAddOperationsModalAddFormValid())
            return;

        const url = controllerName + "AddActionToWorkOrder";

        const workOrderId = state.workOrderId;
        const isActivity = dom.addOperationsModalOperationTypeCombo.GetValue() === state.operationTypeComboDefaultValue;
        const delayTypeId = !isActivity ? dom.addOperationsModalDelayReasonCombo.GetValue() : null;
        const employeeIdList = dom.addOperationsModalEmployeeCheckComboBox.GetSelectedValues();
        const persianStartDate = dom.addOperationsModalStartDatePicker.val();
        const persianEndDate = dom.addOperationsModalEndDatePicker.val();
        const startTime = dom.addOperationsModalStartTimeTextBox.GetText();
        const endTime = dom.addOperationsModalEndTimeTextBox.GetText();
        const durationInMinute = dom.addOperationsModalOperationsDoneTimeSpinEdit.GetValue();
        const description = dom.addOperationsModalModalDescriptionMemo.GetValue();
        const employeeNamesList = dom.addOperationsModalEmployeeCheckComboBox.GetSelectedItems().map(item => item.GetColumnTextByIndex(1));
        const stopDurationInMinute = dom.addOperationsModalDurationInMinuteLabel.text();

        const apiParam = {
            workOrderId,
            isActivity,
            delayTypeId,
            employeeIdList: employeeIdList.map(employeeId => { return { employeeId }; }),
            persianStartDate,
            persianEndDate,
            startTime,
            endTime,
            durationInMinute,
            description,
            employeeNamesList,
            stopDurationInMinute
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        if (apiResult.IsSuccess) {

            resetAddOperationModalResetAddForm();
            setAddOperationsModalDom();

            await fillAddOperationsModalGrid();

            motorsazanClient.messageModal.success(apiResult.Message);
        }
        else {
            motorsazanClient.messageModal.error(apiResult.Message);
        }

    }

    function handleReferToAnotherGroupModalGroupComboSelectedIndexChange() {
        tools.hideItem(dom.referToAnotherGroupModalGroupComboError);

        const group = dom.referToAnotherGroupModalGroupCombo.GetValue();
        const isGroupSelected = !tools.isNullOrEmpty(group);
        if (!isGroupSelected) {
            tools.showItem(dom.referToAnotherGroupModalGroupComboError);
        }
    }

    function handleReferToAnotherGroupModalDescriptionMemoKeyUp() {
        tools.hideItem(dom.referToAnotherGroupModalDescriptionMemoError);

        const description = dom.referToAnotherGroupModalDescriptionMemo.GetValue();
        const descriptionHasValue = !tools.isNullOrEmpty(description);
        if (!descriptionHasValue) {
            tools.showItem(dom.referToAnotherGroupModalDescriptionMemoError);
        }
    }

    function handleReferToAnotherPersonModalPersonComboSelectedIndexChange() {
        tools.hideItem(dom.referToAnotherPersonModalPersonComboError);

        const person = dom.referToAnotherPersonModalPersonCombo.GetValue();
        const isPersonSelected = !tools.isNullOrEmpty(person);
        if (!isPersonSelected) {
            tools.showItem(dom.referToAnotherPersonModalPersonComboError);
        }
    }

    function handleReferToAnotherPersonModalDescriptionMemoKeyUp() {
        tools.hideItem(dom.referToAnotherPersonModalDescriptionMemoError);

        const description = dom.referToAnotherPersonModalDescriptionMemo.GetValue();
        const descriptionHasValue = !tools.isNullOrEmpty(description);
        if (!descriptionHasValue) {
            tools.showItem(dom.referToAnotherPersonModalDescriptionMemoError);
        }
    }

    function handleMainGridDetailRowPrintPreventiveDetailsBtnClick() {
        return window.open("/print/PrintDailyPreventiveWorkOrder?workOrderId=" + state.workOrderId);
    }

    function handleMainGridDetailRowAccidentalPreventiveDetailsBtnClick() {
        return window.open("/print/PrintDailyAccidentalWorkOrder?workOrderId=" + state.workOrderId);
    }

    function init() {
        setDom();
        setEvents();

        initSignalR();

        state.eId = Number(dom.eIdHidden.val());

        getMaintenanceGroupListByEId();
    }

    async function getMaintenanceGroupListByEId() {
        const url = controllerName + "GetMaintenanceGroupListByEId";

        const eId = Number(state.eId);

        const apiParam = {
            eId
        }

        let maintenanceGroupsOfUser = await motorsazanClient.connector.post(url, apiParam);

        maintenanceGroupsOfUser = maintenanceGroupsOfUser.map(maintenanceGroup => ({
            maintenanceGroupId: maintenanceGroup.MaintenanceGroupId,
            maintenanceGroupName: maintenanceGroup.MaintenanceGroupName
        }));

        state.maintenanceGroupsOfUser = maintenanceGroupsOfUser;
    }

    function initSignalR() {
        dom.netExpertHub = $.connection.netExpertHub;
        dom.netExpertHub.client.workOrderReferredToAnotherPerson = workOrderReferredToAnotherPerson;
        dom.netExpertHub.client.workOrderReferredToAnotherMaintenanceGroup = workOrderReferredToAnotherMaintenanceGroup;
        dom.netExpertHub.client.newWorkOrderAdded = newWorkOrderAdded;
        dom.netExpertHub.client.workOrderStatusChanged = workOrderStatusChanged;

        $.connection.hub.stateChanged(detectSignalRDisconnection);

        startSignalRHub();
    }

    function workOrderReferredToAnotherPerson(receiverEId) {
        if (state.eId !== receiverEId || !isFilterFormValid(false)) return;

        fillMainGrid();
    }

    function newWorkOrderAdded() {
        if (!isFilterFormValid(false)) return;

        fillMainGrid();
    }

    function workOrderStatusChanged(workOrderId) {
        if (!isFilterFormValid(false)) return;

        fillMainGrid();

        const isAnyModalOpen = state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange;

        if (isAnyModalOpen && Number(state.workOrderId) === workOrderId) {
            motorsazanClient.contentModal.hideAllModal();
            motorsazanClient.messageModal.error(`با توجه به تغییر وضعیت سفارشکار شماره ${state.workOrderSerial} توسط سایر همکاران ادامه عملیات امکان پذیر نمی باشد، جهت ادامه مجددا اقدام نمایید`);
        }
    }

    function workOrderReferredToAnotherMaintenanceGroup(receiverMaintenanceGroupId) {
        const isReceiverMaintenanceGroupIdInMaintenanceGroupsOfUser =
            state.maintenanceGroupsOfUser
                .filter(maintenanceGroup => maintenanceGroup.maintenanceGroupId === receiverMaintenanceGroupId).length > 0;

        if (!isReceiverMaintenanceGroupIdInMaintenanceGroupsOfUser || !isFilterFormValid(false)) return;

        fillMainGrid();
    }

    function isFilterFormValid(shouldShowError = true) {
        var isValid = true;

        tools.hideItem(dom.filterFormStatusComboError);

        const status = dom.filterFormStatusCombo.GetValue();
        const isStatusSelected = !tools.isNullOrEmpty(status);
        if (!isStatusSelected) {
            isValid = false;
            shouldShowError && tools.showItem(dom.filterFormStatusComboError);
        }

        tools.hideItem(dom.filterFormDateComboError);
        const dateId = dom.filterFormDateCombo.GetValue();
        const isDateSelected = !tools.isNullOrEmpty(dateId);
        if (!isDateSelected) {
            isValid = false;
            shouldShowError && tools.showItem(dom.filterFormDateComboError);
        }

        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        if (dateId === state.customPeriodId) {

            const periodStart = dom.filterFormPeriodStartDatePicker.val();
            const isPeriodStartSelected = !tools.isNullOrEmpty(periodStart);
            if (!isPeriodStartSelected) {
                isValid = false;
                shouldShowError && tools.showItem(dom.filterFormPeriodStartDatePickerError);
            }

            const periodEnd = dom.filterFormPeriodEndDatePicker.val();
            const isPeriodEndSelected = !tools.isNullOrEmpty(periodEnd);
            if (!isPeriodEndSelected) {
                isValid = false;
                shouldShowError && tools.showItem(dom.filterFormPeriodEndDatePickerError);
            };
        }

        return isValid;
    }

    function isAddOperationsModalAddFormValid() {
        let isValid = true;

        tools.hideItem(dom.addOperationsModalEmployeeCheckComboBoxError);

        const employees = dom.addOperationsModalEmployeeCheckComboBox.GetSelectedValues();
        const isEmployeeSelected = employees.length > 0;
        if (!isEmployeeSelected) {
            isValid = false;
            tools.showItem(dom.addOperationsModalEmployeeCheckComboBoxError);
        }

        tools.hideItem(dom.addOperationsModalOperationTypeComboError);
        tools.hideItem(dom.addOperationsModalDelayReasonComboError);

        const operationType = dom.addOperationsModalOperationTypeCombo.GetValue();
        const isOperationTypeSelected = !tools.isNullOrEmpty(operationType);
        if (!isOperationTypeSelected) {
            isValid = false;
            tools.showItem(dom.addOperationsModalOperationTypeComboError);
        }
        else if (operationType !== state.operationTypeComboDefaultValue) {

            const delayReason = dom.addOperationsModalDelayReasonCombo.GetValue();
            const isDelayReasonSelected = !tools.isNullOrEmpty(delayReason);
            if (!isDelayReasonSelected) {
                isValid = false;
                tools.showItem(dom.addOperationsModalDelayReasonComboError);
            }
        }

        tools.hideItem(dom.addOperationsModalStartDatePickerError);

        const startDate = dom.addOperationsModalStartDatePicker.val();
        const isStartDateEntered = !tools.isNullOrEmpty(startDate);
        if (!isStartDateEntered) {
            isValid = false;
            tools.showItem(dom.addOperationsModalStartDatePickerError);
        }

        tools.hideItem(dom.addOperationsModalStartTimeTextBoxError);

        const startTime = dom.addOperationsModalStartTimeTextBox.GetText();
        const isStartTimeEntered = tools.isValidTime(startTime);
        if (!isStartTimeEntered) {
            isValid = false;
            tools.showItem(dom.addOperationsModalStartTimeTextBoxError);
        }

        tools.hideItem(dom.addOperationsModalEndDatePickerError);

        const endDate = dom.addOperationsModalEndDatePicker.val();
        const isEndDateSelected = !tools.isNullOrEmpty(endDate);
        if (!isEndDateSelected) {
            isValid = false;
            tools.showItem(dom.addOperationsModalEndDatePickerError);
        }

        tools.hideItem(dom.addOperationsModalEndTimeTextBoxError);

        const endTime = dom.addOperationsModalEndTimeTextBox.GetText();
        const isEndTimeEntered = tools.isValidTime(endTime);
        if (!isEndTimeEntered) {
            isValid = false;
            tools.showItem(dom.addOperationsModalEndTimeTextBoxError);
        }

        const isAddOperationsModalOperationsDoneTimeSpinEditValid = validateAddOperationsModalOperationsDoneTimeSpinEdit();
        if (!isAddOperationsModalOperationsDoneTimeSpinEditValid) {
            isValid = false;
        }

        tools.hideItem(dom.addOperationsModalModalDescriptionMemoError);

        const description = dom.addOperationsModalModalDescriptionMemo.GetValue();
        const isDescriptionEntered = !tools.isNullOrEmpty(description);
        if (!isDescriptionEntered) {
            isValid = false;
            tools.showItem(dom.addOperationsModalModalDescriptionMemoError);
        }

        return isValid;
    }

    function isReferToAnotherGroupModalValid() {
        var isValid = true;

        tools.hideItem(dom.referToAnotherGroupModalGroupComboError);

        const group = dom.referToAnotherGroupModalGroupCombo.GetValue();
        const isGroupSelected = !tools.isNullOrEmpty(group);
        if (!isGroupSelected) {
            isValid = false;
            tools.showItem(dom.referToAnotherGroupModalGroupComboError);
        }


        tools.hideItem(dom.referToAnotherGroupModalDescriptionMemoError);

        const description = dom.referToAnotherGroupModalDescriptionMemo.GetValue();
        const isDescriptionEntered = !tools.isNullOrEmpty(description);
        if (!isDescriptionEntered) {
            isValid = false;
            tools.showItem(dom.referToAnotherGroupModalDescriptionMemoError);
        }

        return isValid;
    }

    function isReferToAnotherPersonModalValid() {
        var isValid = true;

        tools.hideItem(dom.referToAnotherPersonModalPersonComboError);

        const person = dom.referToAnotherPersonModalPersonCombo.GetValue();
        const isPersonSelected = !tools.isNullOrEmpty(person);
        if (!isPersonSelected) {
            isValid = false;
            tools.showItem(dom.referToAnotherPersonModalPersonComboError);
        }


        tools.hideItem(dom.referToAnotherPersonModalDescriptionMemoError);

        const description = dom.referToAnotherPersonModalDescriptionMemo.GetValue();
        const isDescriptionEntered = !tools.isNullOrEmpty(description);
        if (!isDescriptionEntered) {
            isValid = false;
            tools.showItem(dom.referToAnotherPersonModalDescriptionMemoError);
        }

        return isValid;
    }

    function isStockWaitingFormValid() {
        var isValid = true;

        tools.hideItem(dom.requestNumberError);

        const requestNumber = dom.requestNumber.GetValue();
        const isRequestNumberValid = !tools.isNullOrEmpty(requestNumber);
        if (!isRequestNumberValid) {
            isValid = false;
            tools.showItem(dom.requestNumberError);
        }


        tools.hideItem(dom.requestYearError);

        const requestYear = dom.requestYear.GetValue();
        const isRequestYearValid = !tools.isNullOrEmpty(requestYear);
        if (!isRequestYearValid) {
            isValid = false;
            tools.showItem(dom.requestYearError);
        }

        return isValid;
    }

    function setDom() {
        dom.eIdHidden = $("#eIdHidden");

        //Filter Form
        dom.filterFormStatusCombo = ASPxClientComboBox.Cast("filterFormStatusCombo");
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

        //Grid
        dom.mainGridParent = $("#mainGridParent");
        dom.mainGrid = ASPxClientGridView.Cast("mainGrid");

        dom.mainGridDetailRowStockWaitingBtn = ASPxClientButton.Cast("mainGridDetailRowStockWaitingBtn");
        dom.mainGridDetailRowEndStockWaitingBtn = ASPxClientButton.Cast("mainGridDetailRowEndStockWaitingBtn");

        dom.mainGridDetailRowEndStockWaiting = $("#mainGridDetailRowEndStockWaiting");
        dom.mainGridDetailRowStockWaiting = $("#mainGridDetailRowStockWaiting");

        dom.consumingMaterialsGrid = ASPxClientGridView.Cast("consumingMaterialsGrid");

    }

    function setAddOperationsModalDom() {
        dom.addOperationsModalOperationTypeComboDefaultValueHidden =
            $("#addOperationsModalOperationTypeComboDefaultValueHidden");

        //Add Form
        dom.addOperationsModalDurationInMinuteLabel = $("#addOperationsModalDurationInMinuteLabel");

        dom.addOperationsModalEmployeeCheckComboBox = ASPxClientListBox.Cast("addOperationsModalEmployeeCheckComboBox");
        dom.addOperationsModalEmployeeCheckComboBoxError = $("#addOperationsModalEmployeeCheckComboBoxError");

        dom.addOperationsModalOperationTypeCombo = ASPxClientComboBox.Cast("addOperationsModalOperationTypeCombo");
        dom.addOperationsModalOperationTypeComboError = $("#addOperationsModalOperationTypeComboError");

        dom.addOperationsModalDelayReasonComboParent = $("#addOperationsModalDelayReasonComboParent");
        dom.addOperationsModalDelayReasonCombo = ASPxClientComboBox.Cast("addOperationsModalDelayReasonCombo");
        dom.addOperationsModalDelayReasonComboError = $("#addOperationsModalDelayReasonComboError");

        dom.addOperationsModalStartDatePicker = $("#addOperationsModalStartDatePicker");
        dom.addOperationsModalStartDatePickerError = $("#addOperationsModalStartDatePickerError");

        dom.addOperationsModalStartTimeTextBox = ASPxClientTextBox.Cast("addOperationsModalStartTimeTextBox");
        dom.addOperationsModalStartTimeTextBoxError = $("#addOperationsModalStartTimeTextBoxError");

        dom.addOperationsModalEndDatePicker = $("#addOperationsModalEndDatePicker");
        dom.addOperationsModalEndDatePickerError = $("#addOperationsModalEndDatePickerError");

        dom.addOperationsModalEndTimeTextBox = ASPxClientTextBox.Cast("addOperationsModalEndTimeTextBox");
        dom.addOperationsModalEndTimeTextBoxError = $("#addOperationsModalEndTimeTextBoxError");

        dom.addOperationsModalOperationsDoneTimeSpinEdit = ASPxClientSpinEdit.Cast("addOperationsModalOperationsDoneTimeSpinEdit");
        dom.addOperationsModalOperationsDoneTimeSpinEditError = $("#addOperationsModalOperationsDoneTimeSpinEditError");
        dom.addOperationsModalOperationsDoneTimeSpinEditRangeError = $("#addOperationsModalOperationsDoneTimeSpinEditRangeError");
        dom.addOperationsModalOperationsDoneTimeSpinEditMoreThanDurationInMinuteError = $("#addOperationsModalOperationsDoneTimeSpinEditMoreThanDurationInMinuteError");

        dom.addOperationsModalModalDescriptionMemo = ASPxClientMemo.Cast("addOperationsModalModalDescriptionMemo");
        dom.addOperationsModalModalDescriptionMemoError = $("#addOperationsModalModalDescriptionMemoError");

        //Grid 
        dom.addOperationsModalGridParent = $("#addOperationsModalGridParent");
        dom.addOperationsModalGrid = ASPxClientGridView.Cast("addOperationsModalGrid");


    }

    function startSignalRHub() {
        $.connection.hub.start({ pingInterval: 5000 });
    }

    function resetAddOperationModalResetAddForm() {
        dom.addOperationsModalEmployeeCheckComboBox.SetValue();
        tools.hideItem(dom.addOperationsModalEmployeeCheckComboBoxError);

        dom.addOperationsModalOperationTypeCombo.SetValue();
        tools.hideItem(dom.addOperationsModalOperationTypeComboError);

        dom.addOperationsModalDelayReasonCombo.SetValue();
        tools.hideItem(dom.addOperationsModalDelayReasonComboError);

        dom.addOperationsModalStartDatePicker.val("");
        tools.hideItem(dom.addOperationsModalStartDatePickerError);

        dom.addOperationsModalStartTimeTextBox.SetValue();
        tools.hideItem(dom.addOperationsModalStartTimeTextBoxError);

        dom.addOperationsModalEndDatePicker.val("");
        tools.hideItem(dom.addOperationsModalEndDatePickerError);

        dom.addOperationsModalEndTimeTextBox.SetValue();
        tools.hideItem(dom.addOperationsModalEndDatePickerError);

        dom.addOperationsModalOperationsDoneTimeSpinEdit.SetValue();
        tools.hideItem(dom.addOperationsModalOperationsDoneTimeSpinEditError);
        tools.hideItem(dom.addOperationsModalOperationsDoneTimeSpinEditRangeError);
        tools.hideItem(dom.addOperationsModalOperationsDoneTimeSpinEditMoreThanDurationInMinuteError);

        dom.addOperationsModalModalDescriptionMemo.SetValue();
        tools.hideItem(dom.addOperationsModalModalDescriptionMemoError);
    }

    function setStockWaitingModal() {
        dom.requestNumber = ASPxClientTextBox.Cast("requestNumber");
        dom.requestNumberError = $("#requestNumberError");

        dom.requestYear = ASPxClientTextBox.Cast("requestYear");
        dom.requestYearError = $("#requestYearError");

        dom.stockWaitingModalGridParent = $("#stockWaitingModalGridParent");
        dom.stockWaitingModalSourceGrid = ASPxClientGridView.Cast("stockWaitingModalSourceGrid");

        dom.stockWaitingModalFilterFormSaveBtn = ASPxClientButton.Cast("stockWaitingModalFilterFormSaveBtn");

        dom.mainGridDetailRowStockWaitingBtn = ASPxClientButton.Cast("mainGridDetailRowStockWaitingBtn");
        dom.mainGridDetailRowEndStockWaitingBtn = ASPxClientButton.Cast("mainGridDetailRowEndStockWaitingBtn");
        dom.mainGridDetailRowEndStockWaiting = $("#mainGridDetailRowEndStockWaiting");
        dom.mainGridDetailRowStockWaiting = $("#mainGridDetailRowStockWaiting");

    }

    function setReferToAnotherGroupModalDom() {
        dom.referToAnotherGroupModalWorkOrderTypeTitleLabel = $("#referToAnotherGroupModalWorkOrderTypeTitleLabel");
        dom.referToAnotherGroupModalWorkOrderSerialLabel = $("#referToAnotherGroupModalWorkOrderSerialLabel");

        dom.referToAnotherGroupModalGroupCombo = ASPxClientComboBox.Cast("referToAnotherGroupModalGroupCombo");
        dom.referToAnotherGroupModalGroupComboError = $("#referToAnotherGroupModalGroupComboError");

        dom.referToAnotherGroupModalDescriptionMemo = ASPxClientComboBox.Cast("referToAnotherGroupModalDescriptionMemo");
        dom.referToAnotherGroupModalDescriptionMemoError = $("#referToAnotherGroupModalDescriptionMemoError");
    }

    function setReferToAnotherPersonModalDom() {
        dom.referToAnotherPersonModalReceiverMaintenanceGroupIdHidden = $("#referToAnotherPersonModalReceiverMaintenanceGroupIdHidden");

        dom.referToAnotherPersonModalPersonCombo = ASPxClientComboBox.Cast("referToAnotherPersonModalPersonCombo");
        dom.referToAnotherPersonModalPersonComboError = $("#referToAnotherPersonModalPersonComboError");

        dom.referToAnotherPersonModalDescriptionMemo = ASPxClientComboBox.Cast("referToAnotherPersonModalDescriptionMemo");
        dom.referToAnotherPersonModalDescriptionMemoError = $("#referToAnotherPersonModalDescriptionMemoError");
    }

    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);

    }
    function setConsumeDom() {
        dom.consumingMaterialsGrid = ASPxClientGridView.Cast("consumingMaterialsGrid");
        dom.gridToolbarChangeSaveButton = consumingMaterialsGrid.GetToolbar(0)
            .GetItemByName("gridToolbarChangeSaveButton");

        dom.gridToolbarChangeCancelButton = consumingMaterialsGrid.GetToolbar(0)
            .GetItemByName("gridToolbarChangeCancelButton");

    }

    function handlePreventiveToolbarButtonClick(s, e) {
        if (e.item.name === "gridPreventiveToolbarChangeSaveButton") {
            dom.addPreventiveModalGrid.UpdateEdit();
        }

        if (e.item.name === "gridPreventiveToolbarChangeCancelButton") {
            dom.addPreventiveModalGrid.CancelEdit();
        }
        return true;
    }
    $(document).ready(init);

    function handleBatchEditChangesSaving() {
        console.log();


    }

    async function handlePreventiveBatchEditChangesSaving(s, e) {
       
            e.cancel = true;


            const inputData = Object.entries(e.updatedValues);
            const count = inputData.length;
            const detailList = new Array(count);

            for (let i in inputData) {
                if (Object.prototype.hasOwnProperty.call(inputData, i)) {

                    const actionOrDelayListId = dom.addPreventiveModalGrid.GetRowKey(inputData[i][0]);
                    const durationInMinute = Object.values(inputData[i][1])[0];
                    detailList[i] = {
                        ActionOrDelayListId: actionOrDelayListId,
                        DurationInMinute: durationInMinute
                    };

                }
            }
            initPreventiveDetails();
            const url = controllerName + "BatchEditingUpdatePreventiveModel";
            const params = {
                ActionOrDelayListDetailList: detailList
        };

            const  apiResult = await motorsazanClient.connector.post(url, params);
        initPreventiveDetails();
        await fillAddPreventiveModalGrid();
        initPreventiveDetails();

            dom.addPreventiveModalGrid.CancelEdit();
            dom.gridPreventiveToolbarChangeSaveButton.SetEnabled(false);
            dom.gridPreventiveToolbarChangeCancelButton.SetEnabled(false);
            motorsazanClient.messageModal.success(apiResult);
        }

    function handlePreventiveBatchEditStartEditing() {
        dom.gridPreventiveToolbarChangeSaveButton.SetEnabled(true);
        dom.gridPreventiveToolbarChangeCancelButton.SetEnabled(true);
    }

    function handleWorkOrderTempFinishModalOperationItemComboBeginCallback(command) {

        const maintenanceGroupId = dom.workOrderTempFinishModalMaintenanceGroupCombo.GetValue();
        command.callbackUrl =
            controllerName +
            "WorkOrderTempFinishModalOperationItemCombo" +
            "?maintenanceGroupId=" +
            maintenanceGroupId +
            "&machineId=" +
            state.machineId;
    }

    function handleMainGridWorkOrderStatusHistoryBtnClick() {
        const title = "مشاهده تاریخچه وضعیت های سفارشکار";
        const url = controllerName + "GetWorkOrderStatusHistoryByWorkOrderId";

        const workOrderId = state.workOrderId;

        const apiParam = {
            workOrderId
        };

        motorsazanClient.contentModal.ajaxShow(title, url, apiParam);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleWorkOrderStatusHistoryGridBeginCallback(command) {
        command.callbackUrl =
            controllerName + "GetWorkOrderStatusHistoryByWorkOrderId" + "?WorkOrderId=" + state.workOrderId;
    }
    function onPreventiveEndCallBack() {
        dom.gridPreventiveToolbarChangeSaveButton.SetEnabled(false);
        dom.gridPreventiveToolbarChangeCancelButton.SetEnabled(false);


    }
    return {
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormStatusComboSelectedIndexChange: handleFilterFormStatusComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleMainGridDetailRowPerformedOperationsBtnClick: handleMainGridDetailRowPerformedOperationsBtnClick,
        handleMainGridDetailRowRegistrarInfoBtnClick: handleMainGridDetailRowRegistrarInfoBtnClick,
        handleMainGridDetailRowConsumingMaterialsBtnClick: handleMainGridDetailRowConsumingMaterialsBtnClick,
        handleConsumingMaterialsGridBeginCallback: handleConsumingMaterialsGridBeginCallback,
        handleMainGridDetailRowReferralsHistoryBtnClick: handleMainGridDetailRowReferralsHistoryBtnClick,
        handleMainGridDetailRowReferToAnotherGroupBtnClick: handleMainGridDetailRowReferToAnotherGroupBtnClick,
        handleMainGridDetailRowReferToAnotherPersonBtnClick: handleMainGridDetailRowReferToAnotherPersonBtnClick,
        handleMainGridDetailRowAddOperationsBtnClick: handleMainGridDetailRowAddOperationsBtnClick,
        handleMainGridDetailRowAddPreventiveDetailsBtnClick: handleMainGridDetailRowAddPreventiveDetailsBtnClick,
        handleMainGridDetailRowStockWaitingBtnClick: handleMainGridDetailRowStockWaitingBtnClick,
        handleMainGridDetailRowEndStockWaitingBtnClick: handleMainGridDetailRowEndStockWaitingBtnClick,
        setMainGridFocusedRowOnExpanding: setMainGridFocusedRowOnExpanding,
        handleMainGridBeginCallback: handleMainGridBeginCallback,
        handleMainGridEndCallback: handleMainGridEndCallback,
        handlePerformedOperationsGridBeginCallback: handlePerformedOperationsGridBeginCallback,
        handleReferralsHistoryGridBeginCallback: handleReferralsHistoryGridBeginCallback,
        handleReferToAnotherGroupModalSaveBtnClick: handleReferToAnotherGroupModalSaveBtnClick,
        handleReferToAnotherGroupModalGroupComboSelectedIndexChange: handleReferToAnotherGroupModalGroupComboSelectedIndexChange,
        handleReferToAnotherGroupModalDescriptionMemoKeyUp: handleReferToAnotherGroupModalDescriptionMemoKeyUp,
        handleReferToAnotherPersonModalPersonComboSelectedIndexChange: handleReferToAnotherPersonModalPersonComboSelectedIndexChange,
        handleReferToAnotherPersonModalDescriptionMemoKeyUp: handleReferToAnotherPersonModalDescriptionMemoKeyUp,
        handleReferToAnotherPersonModalSaveBtnClick: handleReferToAnotherPersonModalSaveBtnClick,
        handleAddOperationsModalEmployeeCheckComboBoxSelectedIndexChange: handleAddOperationsModalEmployeeCheckComboBoxSelectedIndexChange,
        handleAddInspectionModalEmployeeCheckComboBoxSelectedIndexChange: handleAddInspectionModalEmployeeCheckComboBoxSelectedIndexChange,
        handleAddOperationsModalOperationTypeComboSelectedIndexChange: handleAddOperationsModalOperationTypeComboSelectedIndexChange,
        handleAddOperationsModalSaveBtnClick: handleAddOperationsModalSaveBtnClick,
        handleAddOperationsModalOperationsDoneTimeSpinEditKeyUp: handleAddOperationsModalOperationsDoneTimeSpinEditKeyUp,
        handleAddOperationsModalOperationsDoneTimeSpinEditLostFocus: handleAddOperationsModalOperationsDoneTimeSpinEditLostFocus,
        handleAddOperationsModalOperationsDoneTimeSpinEditNumberChanged: handleAddOperationsModalOperationsDoneTimeSpinEditNumberChanged,
        handleAddOperationsModalModalDescriptionMemoKeyUp: handleAddOperationsModalModalDescriptionMemoKeyUp,
        handleAddOperationsModalDelayReasonComboSelectedIndexChange: handleAddOperationsModalDelayReasonComboSelectedIndexChange,
        handleAddOperationsModalGridBeginCallback: handleAddOperationsModalGridBeginCallback,
        handleAddOperationsModalGridRemoveCustomBtnClick: handleAddOperationsModalGridRemoveCustomBtnClick,
        handleMainGridDetailRowCancelBtnClick: handleMainGridDetailRowCancelBtnClick,
        handleWorkOrderCancelModalCancelBtnClick: handleWorkOrderCancelModalCancelBtnClick,
        handleWorkOrderCancelModalDescriptionMemoKeyUp: handleWorkOrderCancelModalDescriptionMemoKeyUp,
        handleMainGridDetailRowTempFinishBtnClick: handleMainGridDetailRowTempFinishBtnClick,
        handleWorkOrderTempFinishModalMaintenanceGroupComboSelectedIndexChange: handleWorkOrderTempFinishModalMaintenanceGroupComboSelectedIndexChange,
        handleWorkOrderTempFinishModalOperationItemComboSelectedIndexChange: handleWorkOrderTempFinishModalOperationItemComboSelectedIndexChange,
        handleWorkOrderTempFinishModalDescriptionMemoKeyUp: handleWorkOrderTempFinishModalDescriptionMemoKeyUp,
        handleWorkOrderTempFinishWithDescriptionModalDescriptionMemoKeyUp: handleWorkOrderTempFinishWithDescriptionModalDescriptionMemoKeyUp,
        handleWorkOrderTempFinishModalTempFinishBtnClick: handleWorkOrderTempFinishModalTempFinishBtnClick,
        handleWorkOrderTempFinishModalWithDescriptionBtnClick: handleWorkOrderTempFinishModalWithDescriptionBtnClick,
        handleMainGridDetailRowFinalFinishBtnClick: handleMainGridDetailRowFinalFinishBtnClick,
        handleMainGridDetailRowShowStockBtnClick: handleMainGridDetailRowShowStockBtnClick,
        handleStocksGridBeginCallback: handleStocksGridBeginCallback,
        handleStockWaitingSearchButtonClick: handleStockWaitingSearchButtonClick,
        handleWaitingStockGridBeginCallback: handleWaitingStockGridBeginCallback,
        handleGridSelectionChanged: handleGridSelectionChanged,
        handleStockWaitingModalFilterFormSaveBtnClick: handleStockWaitingModalFilterFormSaveBtnClick,
        handlePreventiveGridBeginCallback: handlePreventiveGridBeginCallback,
        handleInspectionGridBeginCallback: handleInspectionGridBeginCallback,
        handlePreventiveGridCustomBtnClick: handlePreventiveGridCustomBtnClick,
        handleInspectionGridCustomBtnClick: handleInspectionGridCustomBtnClick,
        handleAddPreventiveDetailsModalSaveBtnClick: handleAddPreventiveDetailsModalSaveBtnClick,
        handleMainGridDetailRowAddInspectionDetailsBtnClick: handleMainGridDetailRowAddInspectionDetailsBtnClick,
        handleAddInspectionDetailsModalSaveBtnClick: handleAddInspectionDetailsModalSaveBtnClick,
        handleMainGridDetailRowImmediateTempFinishBtnClick: handleMainGridDetailRowImmediateTempFinishBtnClick,
        handleMainGridDetailRowPrintPreventiveDetailsBtnClick: handleMainGridDetailRowPrintPreventiveDetailsBtnClick,
        handleMainGridDetailRowAccidentalPreventiveDetailsBtnClick: handleMainGridDetailRowAccidentalPreventiveDetailsBtnClick,
        handleUpdateConsumableList: handleUpdateConsumableList,
        handleBatchEditChangesSaving: handleBatchEditChangesSaving,
        handleToolbarButtonClick: handleToolbarButtonClick,
        handleWorkOrderTempFinishModalWithDescriptionBtnClick: handleWorkOrderTempFinishModalWithDescriptionBtnClick,
        handleWorkOrderTempFinishModalOperationItemComboBeginCallback: handleWorkOrderTempFinishModalOperationItemComboBeginCallback,
        handleMainGridWorkOrderStatusHistoryBtnClick: handleMainGridWorkOrderStatusHistoryBtnClick,
        handleWorkOrderStatusHistoryGridBeginCallback: handleWorkOrderStatusHistoryGridBeginCallback,
        handleAddOperationsModalOperationItemCheckComboBoxSelectedIndexChange: handleAddOperationsModalOperationItemCheckComboBoxSelectedIndexChange,
        handlePreventiveBatchEditChangesSaving: handlePreventiveBatchEditChangesSaving,
        handlePreventiveBatchEditStartEditing: handlePreventiveBatchEditStartEditing,
        handlePreventiveToolbarButtonClick: handlePreventiveToolbarButtonClick,
        onPreventiveEndCallBack: onPreventiveEndCallBack

    };




})();