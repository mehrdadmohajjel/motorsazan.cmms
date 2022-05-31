/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_LoadingModal.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.stockMan = (function () {

    var dom = {};

    var state = {
        workOrderId: null,
        workOrderSerial: null,
        gridMemberFocusedRowIndex: null,
        customPeriodId: "CustomPeriod",
        havaleWorkOderReferral: null,
        gridSelectedIds: null,
        rackCode: 61,
        isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange: false
    };

    var tools = motorsazanClient.tools;

    var controllerName = "/StockMan/";

    function addingRegisterConsumables(values) {
        setDom();
        const url = controllerName + "AddingRegisterConsumableForm";
        const params = {
            workOrderSerial: values[0],
            referralNumber: values[1],
            employeeId: values[2]
        };
        const title = "افزودن مواد مصرفی سفارشکار";

        motorsazanClient.contentModal.ajaxShow(title, url, params, setDom);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    async function confirmedConsumablesManagement(values) {
        var url = controllerName + "ConfirmConsumablesManagement";
        const params = {
            HavaleWorkOrderReferralId: values
        };

        const result = await motorsazanClient.connector.post(url, params);
        motorsazanClient.loadingModal.show();
        await refreshConsumablesManagementGrid();
        motorsazanClient.loadingModal.hide();
        motorsazanClient.messageModal.success(result);
    }

    function confirmedConsumablesManagementRow() {
        setDom();
        dom.consumablesManagementGrid.GetRowValues(dom.consumablesManagementGrid.GetFocusedRowIndex(), "HavaleWorkOrderReferralId", (
            values) => {
            motorsazanClient.messageModal.confirm("آیا با تایید حواله موافق هستید؟", () => confirmedConsumablesManagement(values), "تاییدیه حواله");
        });


    }

    function editConsumablesManagementRow() {
        setDom();
        dom.consumablesManagementGrid.GetRowValues(dom.consumablesManagementGrid.GetFocusedRowIndex(), "HavaleWorkOrderReferralId", (values) => {
            var url = controllerName + "EditConsumablesManagement";
            state.havaleWorkOderReferral = values;
            const title = "ویرایش شماره حواله";
            motorsazanClient.contentModal.ajaxShow(title, url, null, () => dom.recipeNumberTextBox = ASPxClientTextBox.Cast("recipeNumberTextBox"));
            state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
        });
    }

    async function addedRegisterConsumable() {
        if (!isAddingFormRegisterConsumableValid()) return;

        const url = controllerName + "AddingRegisterConsumable";

        const workOrderReferralId = dom.referralNumberLabel.text();
        const stockId = dom.sundryModalStockComboByRack.GetValue();
        const quantity = dom.registerConsumablesStockNumberSpinEdit.GetValue();


        const apiParam = {
            workOrderReferralId,
            stockId,
            quantity
        };


        motorsazanClient.connector.post(url, apiParam)
            .then(function (apiResult) {
                motorsazanClient.messageModal.success(apiResult);
                setDom();
                resetForm();
                fillAddedRegisterConsumableGrid(workOrderReferralId);

            });

    }

    function resetForm() {
        dom.sundryModalStockComboByRack.SetSelectedIndex(-1);
        dom.registerConsumablesStockNumberSpinEdit.SetText("");
        handleRegisterConsumablesStoreComboSelectedIndexChanged();
    }

    async function fillGrid() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "StockManGrid";

        const workOrderStatus = dom.filterFormWorkOrderStatusCombo.GetValue();
        const dateTypeId = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();

        const apiParam = {
            workOrderStatus,
            dateTypeId,
            persianStartDate,
            persianEndDate
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.stockManGridParent.html(gridHtml);
        setDom();
    }

    async function handleAddedRegisterConsumableBtnClick() {
        addedRegisterConsumable();
    }

    function handleConsumablesManagementBtnClick() {
        setDom();
        const url = controllerName + "ConsumablesManagementModal";
        const params = {
            workOrderId: state.workOrderId
        };
        const title = "مشاهده مواد ثبت شده";
        motorsazanClient.contentModal.ajaxShow(title, url, params,
            () => {
                dom.consumablesManagementGridParent = $("#consumablesManagementGridParent");
            }, false, true);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleConsumablesManagementGridCustomBtnClick(source, event) {
        const customBtnId = event.buttonID;
        if (customBtnId === "consumablesManagementGridEditCustomBtn") return editConsumablesManagementRow();
        if (customBtnId === "registerConsumablesGridDeleteCustomBtn") return removeConsumablesManagementRow(source, event);
        if (customBtnId === "registerConsumablesGridConfirmCustomBtn") return confirmedConsumablesManagementRow();
        return null;
    }

    async function handleEditRecipeNumberButtonClick() {
        setDom();
        if (!isRecipeNumberValid()) return;
        var url = controllerName + "EditReferralNumber";
        const recipeNumber = dom.recipeNumberTextBox.GetValue();

        const apiParam = {
            havaleWorkOrderReferralId: state.havaleWorkOderReferral,
            havaleNo: recipeNumber
        };

        const result = await motorsazanClient.connector.post(url, apiParam);

        motorsazanClient.contentModal.hideModal();
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = false;
        motorsazanClient.loadingModal.show();
        await refreshConsumablesManagementGrid();
        motorsazanClient.loadingModal.hide();
        motorsazanClient.messageModal.success(result);

    }

    function handleFilterFormDateTypeComboChange() {

        tools.hideItem(dom.filterFormDateTypeComboError);
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        const selectedDateId = dom.filterFormDateTypeCombo.GetValue();

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

    function handleFilterFormSearchBtnClick() {
        if (!isFilterFormValid())
            return;

        fillGrid();
    }

    function handleFilterFormWorkOrderStatusComboChange() {
        tools.hideItem(dom.filterFormWorkOrderStatusComboError);
        dom.filterFormDateTypeCombo.SetEnabled(true);
    }

    function handleRegisterConsumablesBtnClick() {
        const url = controllerName + "RegisterConsumables";
        const params = {
            workOrderId: state.workOrderId
        };

        state.gridMemberFocusedRowIndex = state.workOrderId;
        const title = "ثبت مواد مصرفی سفارشکار";
        motorsazanClient.contentModal.ajaxShow(title, url, params);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleRegisterConsumablesGridBeginCallback(command) {
        const url = controllerName + "RegisterConsumables" + "?WorkOrderId=" + state.workOrderId;
        command.callbackUrl = url;
    }

    function handleRegisterConsumablesGridCustomBtnClick() {
        setDom();
        dom.registerConsumablesGrid.GetRowValues(dom.registerConsumablesGrid.GetFocusedRowIndex(), "WorkOrderSerial;WorkOrderReferralId;EmployeeId", addingRegisterConsumables);
        /*dom.registerConsumablesGrid.GetRowValues(dom.registerConsumablesGrid.GetFocusedRowIndex(), "WorkOrderReferralId", fillAddedRegisterConsumableGrid);*/
    }

    function handleWorkOrderPerformedOperationGridBeginCallback(command) {
        const url = controllerName + "WorkOrderPerformedOperationGrid" + "?WorkOrderId=" + state.workOrderId;
        command.callbackUrl = url;
    }

    async function fillAddedRegisterConsumableGrid(values) {
        setDom();
        const url = controllerName + "HavaleWorkOrderReferralGrid";
        const params = {
            WorkOrderReferralId: values
        };
        const gridHtml = await motorsazanClient.connector.post(url, params);

        dom.addedRegisterConsumableGridParent.html(gridHtml);
        setDom();
    }

    function handleRegistrarProfileObservationBtnClick() {
        setDom();
        const url = controllerName + "RegistrarProfileObservation";
        const params = {
            WorkOrderId: state.workOrderId
        };
        const title = "مشاهده مشخصات ثبت کننده درخواست";
        motorsazanClient.contentModal.ajaxShow(title, url, params, setDom);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    function handleStockManGridEndCallback() {
        motorsazanClient.loadingModal.hide();
    }


    function handleStockManGridBeginCallback(command) {
        motorsazanClient.loadingModal.show();

        if (!isFilterFormValid()) {
            const url = controllerName + "StockManGrid"
                + "?WorkOrderStatus=" + -1
                + "&dateTypeId=" + -1
                + "&persianStartDate=" + ""
                + "&persianEndDate=" + "";
            command.callbackUrl = url;
            return;
        }

        const workOrderStatus = dom.filterFormWorkOrderStatusCombo.GetValue();
        const dateTypeId = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();
        const url = controllerName + "StockManGrid"
            + "?WorkOrderStatus=" + workOrderStatus
            + "&dateTypeId=" + dateTypeId
            + "&persianStartDate=" + persianStartDate
            + "&persianEndDate=" + persianEndDate;
        command.callbackUrl = url;
    }

    async function handleWorkOrderPerformedOperationBtnClick() {

        const url = controllerName + "WorkOrderPerformedOperationGrid";
        const params = {
            workOrderId: state.workOrderId
        };
        const title = "مشاهده عملیات انجام شده سفارشکار";
        await motorsazanClient.contentModal.ajaxShow(title, url, params, setDom);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;

    }

    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
    }


    function handleSundryModalStockComboByRackSelectionChange() {
        tools.hideItem(dom.registerConsumablesStockComboError);
    }

    function handleRegisterConsumablesStockNumberSpinEditValueChanged() {
        tools.hideItem(dom.registerConsumablesStockNumberSpinEditError);
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

    function init() {
        setDom();

        initSignalR();

        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);
    }

    function initSignalR() {
        dom.stockManHub = $.connection.stockManHub;
        dom.stockManHub.client.newWorkOrderAdded = newWorkOrderAdded;
        dom.stockManHub.client.workOrderStatusChanged = workOrderStatusChanged;


        $.connection.hub.stateChanged(detectSignalRDisconnection);

        startSignalRHub();
    }

    function startSignalRHub() {
        $.connection.hub.start({ pingInterval: 5000 });
    }

    function newWorkOrderAdded() {
        if (!isFilterFormValid(false)) return;

        fillGrid();
    }

    function workOrderStatusChanged(workOrderId) {
        if (!isFilterFormValid(false)) return;

        fillGrid();

        const isAnyModalOpen = state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange;

        if (isAnyModalOpen && Number(state.workOrderId) === workOrderId ) {
            motorsazanClient.contentModal.hideAllModal();
            motorsazanClient.messageModal.error(`با توجه به تغییر وضعیت سفارشکار شماره ${state.workOrderSerial} توسط سایر همکاران ادامه عملیات امکان پذیر نمی باشد، جهت ادامه مجددا اقدام نمایید`);
        }
    }

    function isAddingFormRegisterConsumableValid() {
        setDom();
        var isValid = true;

        tools.hideItem(dom.registerConsumablesStoreComboError);
        const storeId = dom.registerConsumablesStoreCombo.GetValue();
        const storeIdHasValue = !tools.isNullOrEmpty(storeId);
        if (!storeIdHasValue) {
            isValid = false;
            tools.showItem(dom.registerConsumablesStoreComboError);
        }

        dom.sundryModalStockComboByRack = ASPxClientComboBox.Cast(`sundryModalStockComboByRack${state.rackCode}`);

        tools.hideItem(dom.registerConsumablesStockComboError);
        const rackCode = dom.sundryModalStockComboByRack.GetValue();
        const rackCodeHasValue = !tools.isNullOrEmpty(rackCode);
        if (!rackCodeHasValue) {
            isValid = false;
            tools.showItem(dom.registerConsumablesStockComboError);
        }


        tools.hideItem(dom.registerConsumablesStockNumberSpinEditError);
        const stockNumber = dom.registerConsumablesStockNumberSpinEdit.GetValue();
        const stockNumberHasValue = !tools.isNullOrEmpty(stockNumber);
        if (!stockNumberHasValue) {
            isValid = false;
            tools.showItem(dom.registerConsumablesStockNumberSpinEditError);
        }

        return isValid;
    }

    function isFilterFormValid(shouldShowErrors = true) {
        var isValid = true;

        tools.hideItem(dom.filterFormWorkOrderStatusComboError);
        const workOrderStatus = dom.filterFormWorkOrderStatusCombo.GetValue();
        const workOrderStatusHasValue = !tools.isNullOrEmpty(workOrderStatus);
        if (!workOrderStatusHasValue) {
            isValid = false;
            shouldShowErrors && tools.showItem(dom.filterFormWorkOrderStatusComboError);
        }

        tools.hideItem(dom.filterFormDateTypeComboError);
        const dateType = dom.filterFormDateTypeCombo.GetValue();
        const isDateTypeSelected = !tools.isNullOrEmpty(dateType);

        if (!isDateTypeSelected) {
            isValid = false;
            shouldShowErrors && tools.showItem(dom.filterFormDateTypeComboError);
        }
        else if (dateType === state.customPeriodId) {
            tools.hideItem(dom.filterFormPeriodStartDatePickerError);
            const startDate = dom.filterFormPeriodStartDatePicker.val();
            const isStartDateValid = tools.isValidPersianDate(startDate);
            if (!isStartDateValid) {
                isValid = false;
                shouldShowErrors && tools.showItem(dom.filterFormPeriodStartDatePickerError);
            }

            tools.hideItem(dom.filterFormPeriodEndDatePickerError);
            const endDate = dom.filterFormPeriodEndDatePicker.val();
            const isEndDateValid = tools.isValidPersianDate(endDate);
            if (!isEndDateValid) {
                isValid = false;
                shouldShowErrors && tools.showItem(dom.filterFormPeriodEndDatePickerError);
            }
        }

        return isValid;
    }

    function isRecipeNumberValid() {
        var isValid = true;
        tools.hideItem(dom.recipeNumberTextBoxError);
        const recipeNumber = dom.recipeNumberTextBox.GetValue();
        const recipeNumberHasValue = !tools.isNullOrEmpty(recipeNumber);
        if (!recipeNumberHasValue) {
            isValid = false;
            tools.showItem(dom.recipeNumberTextBoxError);
        }

        return isValid;
    }

    function referralHistoryObservationBtnClick() {
        setDom();
        const url = controllerName + "ReferralHistoryObservationGrid";
        const params = {
            workOrderId: state.workOrderId
        };
        motorsazanClient.loadingModal.hide();
        const title = "مشاهده تاریخچه ارجاعات سفارشکار";
        motorsazanClient.contentModal.ajaxShow(title, url, params, setDom);
        state.isThereAnyOpenModalThatShouldCloseModalsOnWorkOrderStatusChange = true;
    }

    async function refreshConsumablesManagementGrid() {
        const url = controllerName + "ConsumablesManagementGrid";
        const params = {
            workOrderId: state.workOrderId
        };
        state.gridMemberFocusedRowIndex = state.workOrderId;

        const grid = await motorsazanClient.connector.post(url, params);
        dom.consumablesManagementGridParent.html(grid);
    }

    async function removeConsumablesManagement(values) {
        var url = controllerName + "DeleteConsumablesManagement";
        const params = {
            havaleWorkOrderReferralId: values
        };
        const result = await motorsazanClient.connector.post(url, params);
        motorsazanClient.loadingModal.show();
        await refreshConsumablesManagementGrid();
        motorsazanClient.loadingModal.hide();
        motorsazanClient.messageModal.success(result);
    }

    async function removeConsumablesManagementRow() {
        setDom();
        motorsazanClient.loadingModal.show();
        dom.consumablesManagementGrid.GetRowValues(dom.consumablesManagementGrid.GetFocusedRowIndex(), "HavaleWorkOrderReferralId", (values) => {
            motorsazanClient.loadingModal.hide();
            motorsazanClient.messageModal.confirm("آیا از حذف این ردیف مطمئن هستید؟", () => removeConsumablesManagement(values), "تاییدیه حذف");
        });
    }

    async function handleRegisterConsumablesStoreComboSelectedIndexChanged() {
        const url = controllerName + "SundryModalStockComboByRackCode";

        setDom();

        const rackCode = dom.registerConsumablesStoreCombo.GetValue();

        const apiParam = {
            rackCode: rackCode
        };

        const comboHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.registerConsumablesStockComboParent.html(comboHtml);

        state.rackCode = rackCode;

        setDom();
    }

    function setDom() {
        //FilterForm
        dom.filterFormWorkOrderStatusComboParent = $("#filterFormWorkOrderStatusComboParent");
        dom.filterFormWorkOrderStatusComboError = $("#filterFormWorkOrderStatusComboError");
        dom.filterFormWorkOrderStatusCombo = ASPxClientComboBox.Cast("filterFormWorkOrderStatusCombo");

        dom.filterFormDateTypeComboParent = $("#filterFormDateTypeComboParent");
        dom.filterFormDateTypeComboError = $("#filterFormDateTypeComboError");
        dom.filterFormDateTypeCombo = ASPxClientComboBox.Cast("filterFormDateTypeCombo");

        dom.filterFormPeriodStartDatePickerParent = $("#filterFormPeriodStartDatePickerParent");
        dom.filterFormPeriodStartDatePicker = $("#filterFormPeriodStartDatePicker");
        dom.filterFormPeriodStartDatePickerError = $("#filterFormPeriodStartDatePickerError");

        dom.filterFormPeriodEndDatePickerParent = $("#filterFormPeriodEndDatePickerParent");
        dom.filterFormPeriodEndDatePicker = $("#filterFormPeriodEndDatePicker");
        dom.filterFormPeriodEndDatePickerError = $("#filterFormPeriodEndDatePickerError");

        //RegisterConsumable
        dom.workOrderLabel = $("#workOrderLabel");
        dom.referralNumberLabel = $("#referralNumberLabel");
        dom.officerLabel = $("#officerLabel");

        dom.registerConsumablesStockComboParent = $("#registerConsumablesStockComboParent");
        dom.registerConsumablesStockComboError = $("#registerConsumablesStockComboError");
        dom.registerConsumablesStockCombo = ASPxClientComboBox.Cast("registerConsumablesStockCombo");

        dom.registerConsumablesStoreCombo = ASPxClientComboBox.Cast("registerConsumablesStoreCombo");
        dom.registerConsumablesStoreComboParent = $("#registerConsumablesStoreComboParent");
        dom.registerConsumablesStoreComboError = $("#registerConsumablesStoreComboError");

        dom.sundryModalStockComboByRack = ASPxClientComboBox.Cast(`sundryModalStockComboByRack${state.rackCode}`);
        dom.registerConsumablesStockComboError = $("#registerConsumablesStockComboError");
        dom.registerConsumablesStockComboParent = $("#registerConsumablesStockComboParent");

        dom.registerConsumablesStockNumberSpinEditParent = $("#registerConsumablesStockNumberSpinEditParent");
        dom.registerConsumablesStockNumberSpinEditError = $("#registerConsumablesStockNumberSpinEditError");
        dom.registerConsumablesStockNumberSpinEdit = ASPxClientSpinEdit.Cast("registerConsumablesStockNumberSpinEdit");
        dom.registerConsumablesStockNumberSpinEditRangeError = $("#registerConsumablesStockNumberSpinEditRangeError");

        //ConsumablesManagement
        dom.consumablesManagementGrid = ASPxClientGridView.Cast("consumablesManagementGrid");

        dom.recipeNumberTextBoxParent = $("#recipeNumberTextBoxParent");
        dom.recipeNumberTextBoxError = $("#recipeNumberTextBoxError");

        //StockManGrid
        dom.stockManGridParent = $("#stockManGridParent");
        dom.stockManGrid = ASPxClientGridView.Cast("stockManGrid");

        dom.registerConsumablesGrid = ASPxClientGridView.Cast("registerConsumablesGrid");
        dom.addedRegisterConsumableGridParent = $("#addedRegisterConsumableGridParent");
        dom.addedRegisterConsumableGrid = ASPxClientGridView.Cast("addedRegisterConsumableGrid");
    }


    function setStockManGridFocusedRowOnExpanding(source, event) {
        setDom();

        source.SetFocusedRowIndex(event.visibleIndex);

        const activeIndex = event.visibleIndex;
        state.workOrderId = dom.stockManGrid.GetRowKey(activeIndex);

        dom.stockManGrid.GetRowValues(activeIndex,
            [
                "WorkOrderSerial"
            ],
            async (workOrderSerial) => {
                state.workOrderSerial = workOrderSerial;
            });
    }

    $(document).ready(init);

    async function handleRegisterConsumablesStoreComboChange() {
        const url = controllerName + "SundryModalStockComboByRackCode";


        const rackCode = dom.registerConsumablesStoreCombo.GetValue();

        const apiParam = {
            rackCode: rackCode
        };

        const comboHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.registerConsumablesStockComboParent.html(comboHtml);
        state.rackCode = rackCode;
        tools.hideItem(dom.registerConsumablesStoreComboError);
        setDom();

    }

    function handleGridSelectionChanged(source) {
        source.GetSelectedFieldValues("WorkOrderId",
            (values) => {
                state.workOrderId = values;
            });
    }

    return {
        handleFilterFormWorkOrderStatusComboChange: handleFilterFormWorkOrderStatusComboChange,
        handleRegisterConsumablesStoreComboSelectedIndexChanged: handleRegisterConsumablesStoreComboSelectedIndexChanged,
        handleFilterFormDateTypeComboChange: handleFilterFormDateTypeComboChange,
        handleFilterFormSearchBtnClick: handleFilterFormSearchBtnClick,
        handleStockManGridEndCallback: handleStockManGridEndCallback,
        handleStockManGridBeginCallback: handleStockManGridBeginCallback,
        setStockManGridFocusedRowOnExpanding: setStockManGridFocusedRowOnExpanding,
        handleRegisterConsumablesBtnClick: handleRegisterConsumablesBtnClick,
        handleRegisterConsumablesGridBeginCallback: handleRegisterConsumablesGridBeginCallback,
        handleRegisterConsumablesGridCustomBtnClick: handleRegisterConsumablesGridCustomBtnClick,
        handleWorkOrderPerformedOperationGridBeginCallback: handleWorkOrderPerformedOperationGridBeginCallback,
        handleAddedRegisterConsumableBtnClick: handleAddedRegisterConsumableBtnClick,
        handleRegistrarProfileObservationBtnClick: handleRegistrarProfileObservationBtnClick,
        handleWorkOrderPerformedOperationBtnClick: handleWorkOrderPerformedOperationBtnClick,
        handleRegisterConsumablesStockNumberSpinEditValueChanged: handleRegisterConsumablesStockNumberSpinEditValueChanged,
        handleSundryModalStockComboByRackSelectionChange: handleSundryModalStockComboByRackSelectionChange,
        handleConsumablesManagementBtnClick: handleConsumablesManagementBtnClick,
        referralHistoryObservationBtnClick: referralHistoryObservationBtnClick,
        handleConsumablesManagementGridCustomBtnClick: handleConsumablesManagementGridCustomBtnClick,
        handleEditRecipeNumberButtonClick: handleEditRecipeNumberButtonClick,
        handleGridSelectionChanged: handleGridSelectionChanged,
        handleRegisterConsumablesStoreComboChange: handleRegisterConsumablesStoreComboChange
    }
})();