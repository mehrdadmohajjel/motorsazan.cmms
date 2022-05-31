/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.productiveWorkOrder = (function () {

    var dom = {};
    var state = {
        specialDateTypeFilter: "CustomPeriod",
        GridFocusedRowIndex: 0,
        GridMemberFocusedRowIndex: 0,
        GroupId: 0,
        WorkOrderId: 0,
        statusId: 0,
        dateType: 4,
        startDate: "",
        endDate: ""
    };
    var voteTitles = ["خیلی کم", "کم", "متوسط", "زیاد", "خیلی زیاد"];
    var votePrevTitle;
    var tools = motorsazanClient.tools;
    var controllerName = "/ProductiveWorkOrder/";

    async function addProductiveWorkOrder() {
        var canContinue = isAddFormValid();
        if (!canContinue) return false;

        const url = controllerName + "AddNewProductiveWorkOrder";

        const machineId = dom.addFormMachineCombo.GetValue();
        const maintenanceGroupId = dom.addFormMaintenanceGroupListCombo.GetValue();
        const stopTypeId = dom.addFormStopTypeListCombo.GetValue();
        const departmentId = dom.addFormEmployeeDepartmentCombo.GetValue();
        const description = dom.addFormFaultDescription.GetValue();
        const hasScrap = dom.addFormScrapRadio.GetValue();

        const apiParam = {
            machineId: machineId,
            maintenanceGroupId: maintenanceGroupId,
            stopTypeId: stopTypeId,
            userID: 1,
            departmentId: departmentId,
            description: description,
            hasScrap: hasScrap,
            relativeWorkOrderId: null
        };

        const gridSource = await motorsazanClient.connector.post(url, apiParam);

        dom.netExpertHub.server.newWorkOrderAdded();
        dom.stockManHub.server.newWorkOrderAdded();

        motorsazanClient.messageModal.success(gridSource);
        filterWorkOrderList();
        resetAddForm();
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

    function isFilterFormValid() {
        var isValid = true;

        tools.hideItem(dom.filterFormStatusTypeComboError);

        const status = dom.filterFormStatusTypeCombo.GetValue();
        const isStatusSelected = !tools.isNullOrEmpty(status);
        if (!isStatusSelected) {
            isValid = false;
            tools.showItem(dom.filterFormStatusTypeComboError);
        }

        tools.hideItem(dom.filterFormDateTypeComboError);
        const dateId = dom.filterFormDateTypeCombo.GetValue();
        const isDateSelected = !tools.isNullOrEmpty(dateId);
        if (!isDateSelected) {
            isValid = false;
            tools.showItem(dom.filterFormDateTypeComboError);
        }

        tools.hideItem(dom.filterFormStartDateError);
        tools.hideItem(dom.filterFormEndDateError);

        if (dateId === state.specialDateTypeFilter) {

            const periodStart = dom.filterFormStartDate.val();
            const isPeriodStartSelected = !tools.isNullOrEmpty(periodStart);
            if (!isPeriodStartSelected) {
                isValid = false;
                tools.showItem(dom.filterFormStartDateError);
            }

            const periodEnd = dom.filterFormEndDate.val();
            const isPeriodEndSelected = !tools.isNullOrEmpty(periodEnd);
            if (!isPeriodEndSelected) {
                isValid = false;
                tools.showItem(dom.filterFormEndDateError);
            };
        }

        return isValid;

    }

    function filterWorkOrderList() {
        const url = "/ProductiveWorkOrder/ProductiveWorkOrderGird";
        const canContinue = isFilterFormValid();
        if (!canContinue) return false;

        const status = dom.filterFormStatusTypeCombo.GetValue();
        state.statusId = status;

        const dateType = dom.filterFormDateTypeCombo.GetValue();
        state.dateType = dateType;

        const persianStartDate = dom.filterFormStartDate.val();
        state.startDate = persianStartDate;

        const persianEndDate = dom.filterFormEndDate.val();
        state.endDate = persianEndDate;

        const apiParam = {
            workOrderStatusTypeId: state.statusId,
            dateType: state.dateType,
            startShamsiDate: state.startDate,
            endPersianDate: state.endDate

        };
        motorsazanClient.connector.post(url, apiParam)
            .then(function (gridSource) {
                dom.ProductiveWorkOrderSourceGirdParent.html(gridSource);
                setDom();
            });
    }

    function getWorkOrderGridActiveRowKey() {
        const activeIndex = dom.ProductiveWorkOrderSourceGird.GetFocusedRowIndex();
        return dom.ProductiveWorkOrderSourceGird.GetRowKey(activeIndex);
    }

    function rateWorkOrder() {
        dom.ratingPanel = ASPxClientRoundPanel.Cast("ratingPanel");
        dom.myRating = ASPxClientRatingControl.Cast("myRating");
        dom.lbMyVote = $("#lbMyVote");
    }

    function showRateBtnPopup() {
        const url = controllerName + "RatePeople";
        state.GridMemberFocusedRowIndex = dom.ProductiveWorkOrderSourceGird.GetFocusedRowIndex();

        const apiParam = {
            WorkOrderId: state.GridMemberFocusedRowIndex
        };

        const title = "میزان رضایت از تعمیر";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, rateWorkOrder);
    }

    function handleProductiveWorkOrderGridCallbackUrl(source) {
        state.statusId = dom.filterFormStatusTypeCombo.GetValue();
        state.dateType = dom.filterFormDateTypeCombo.GetValue();
        state.startDate = dom.filterFormStartDate.val();
        state.endDate = dom.filterFormEndDate.val(); var url = controllerName + "ProductiveWorkOrderGird";
        source.callbackUrl = url + "?statusId=" + state.statusId +
            "&dateType=" + state.dateType + "&startDate=" + state.startDate +
            "&endDate=" + state.endDate;
    }

    function handleAddFormFaultDescriptionValueChanged() {
        tools.hideItem(dom.addFormFaultDescriptionError);
    }

    function machineListSelectedIndexChanged() {
        tools.hideItem(dom.addFormMachineComboError);
    }

    function maintenanceGroupListSelectedIndexChanged() {
        tools.hideItem(dom.addFormMaintenanceGroupComboError);
    }

    function stopTypeListSelectedIndexChanged() {
        tools.hideItem(dom.addFormStopTypeListComboError);
    }

    function loadMachineList() {
        tools.hideItem(dom.addFormEmployeeDepartmentComboError);
        const url = controllerName + "AddFormGetDepartmentMachineList";
        const apiParam = {
            departmentId: addFormEmployeeDepartmentCombo.GetValue()
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (groupGridSource) {
                dom.addFormMachineComboParent.html(groupGridSource);
                setDom();
            });
    }

    function handleAddFormMachinesComboBeginCallback(command) {

        const departmentId = dom.addFormEmployeeDepartmentCombo.GetValue() || 0;

        command.callbackUrl = controllerName + "/AddFormGetDepartmentMachineList?departmentId=" + departmentId;
    }

    function init() {
        setDom();

        initSignalR();
    }

    function initSignalR() {
        dom.netExpertHub = $.connection.netExpertHub;
        dom.stockManHub = $.connection.stockManHub;

        $.connection.hub.stateChanged(detectSignalRDisconnection);

        startSignalRHub();
    }

    function isAddFormValid() {
        var isValid = true;

        tools.hideItem(dom.addFormEmployeeDepartmentComboError);
        const department = dom.addFormEmployeeDepartmentCombo.GetValue();
        const isDepartmentValid = !tools.isNullOrEmpty(department);
        if (!isDepartmentValid) {
            isValid = false;
            tools.showItem(dom.addFormEmployeeDepartmentComboError);
        }

        tools.hideItem(dom.addFormMachineComboError);
        const machine = dom.addFormMachineCombo.GetValue();
        const isMachineValid = !tools.isNullOrEmpty(machine);
        if (!isMachineValid) {
            isValid = false;
            tools.showItem(dom.addFormMachineComboError);
        }

        tools.hideItem(dom.addFormScrapRadioError);
        const scrap = dom.addFormScrapRadio.GetValue();
        const isScrapValid = !tools.isNullOrEmpty(scrap);
        if (!isScrapValid) {
            isValid = false;
            tools.showItem(dom.addFormScrapRadioError);
        }


        tools.hideItem(dom.addFormMaintenanceGroupComboError);
        const maintenanceGroup = dom.addFormMaintenanceGroupListCombo.GetValue();
        const isMaintenanceGroupValid = !tools.isNullOrEmpty(maintenanceGroup);
        if (!isMaintenanceGroupValid) {
            isValid = false;
            tools.showItem(dom.addFormMaintenanceGroupComboError);
        }

        tools.hideItem(dom.addFormStopTypeListComboError);
        const stopType = dom.addFormStopTypeListCombo.GetValue();
        const isStopTypeValid = !tools.isNullOrEmpty(stopType);
        if (!isStopTypeValid) {
            isValid = false;
            tools.showItem(dom.addFormStopTypeListComboError);
        }

        tools.hideItem(dom.addFormFaultDescriptionError);
        const faultDescription = dom.addFormFaultDescription.GetValue();
        const isFaultDescription = !tools.isNullOrEmpty(faultDescription);
        if (!isFaultDescription) {
            isValid = false;
            tools.showItem(dom.addFormFaultDescriptionError);
        }

        return isValid;
    }

    function resetAddForm() {
        dom.addFormEmployeeDepartmentCombo.SetSelectedIndex(-1);
        dom.addFormMachineCombo.SetSelectedIndex(-1);
        dom.addFormMaintenanceGroupListCombo.SetSelectedIndex(-1);
        dom.addFormStopTypeListCombo.SetSelectedIndex(-1);
        dom.addFormScrapRadio.Clear();
        dom.addFormFaultDescription.SetText("");
    }

    function setDom() {
        dom.addFormEmployeeDepartmentComboParent = $("#addFormEmployeeDepartmentComboParent");
        dom.addFormEmployeeDepartmentComboError = $("#addFormEmployeeDepartmentComboError");
        dom.addFormEmployeeDepartmentCombo = ASPxClientTextBox.Cast("addFormEmployeeDepartmentCombo");

        dom.addFormMachineComboParent = $("#addFormMachineComboParent");
        dom.addFormMachineComboError = $("#addFormMachineComboError");
        dom.addFormMachineCombo = ASPxClientTextBox.Cast("addFormMachineCombo");



        dom.addFormMaintenanceGroupComboParent = $("#addFormMaintenanceGroupComboParent");
        dom.addFormMaintenanceGroupComboError = $("#addFormMaintenanceGroupComboError");
        dom.addFormMaintenanceGroupListCombo = ASPxClientTextBox.Cast("addFormMaintenanceGroupListCombo");

        dom.addFormStopTypeListComboParent = $("#addFormStopTypeListComboParent");
        dom.addFormStopTypeListComboError = $("#addFormStopTypeListComboError");
        dom.addFormStopTypeListCombo = ASPxClientTextBox.Cast("addFormStopTypeListCombo");

        dom.addFormFaultDescriptionParent = $("#addFormFaultDescriptionParent");
        dom.addFormFaultDescriptionError = $("#addFormFaultDescriptionError");
        dom.addFormFaultDescription = ASPxClientMemo.Cast("addFormFaultDescription");

        dom.addFormScrapRadioParent = $("#addFormScrapRadioParent");
        dom.addFormScrapRadioError = $("#addFormScrapRadioError");
        dom.addFormScrapRadio = ASPxClientRadioButtonList.Cast("addFormScrapRadio");



        dom.ProductiveWorkOrderSourceGirdParent = $("#ProductiveWorkOrderSourceGirdParent");
        dom.ProductiveWorkOrderSourceGird = ASPxClientGridView.Cast("ProductiveWorkOrderSourceGird");

        //---------Filter
        dom.filterFormDateTypeComboError = $("#filterFormDateTypeComboError");
        dom.filterFormStatusTypeComboError = $("#filterFormStatusTypeComboError");
        dom.filterFormStatusTypeCombo = ASPxClientTextBox.Cast("filterFormStatusTypeCombo");
        dom.filterFormDateTypeCombo = ASPxClientComboBox.Cast("filterFormDateTypeCombo");
        dom.filterFormStartDate = $("#filterFormStartDate");
        dom.filterFormStartDateError = $("#filterFormStartDateError");
        dom.filterFormEndDate = $("#filterFormEndDate");
        dom.filterFormEndDateError = $("#filterFormEndDateError");
        dom.filterFormSpecialDateColumn = $("#filterFormSpecialDateColumn");
        dom.filterFormDateTypeColumn = $("#filterFormDateTypeColumn");

        //---------Filter

    }

    function setProductiveWorkOrderGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);
        state.WorkOrderId = dom.ProductiveWorkOrderSourceGird.GetRowKey(event.visibleIndex);
    }

    function showActionHistoryModal() {
        const url = controllerName + "ShowActionHistory";
        const apiParam = {
            workOrderId: state.WorkOrderId
        };
        const title = "مشاهده عملیات انجام شده";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom);
    }

    function showRemoveConfirmation() {
        const url = controllerName + "ShowSetWorkOrderStatusToCancelByCustomer";
        state.GridMemberFocusedRowIndex = dom.ProductiveWorkOrderSourceGird.GetFocusedRowIndex();

        const apiParam = {
            WorkOrderId: state.GridMemberFocusedRowIndex
        };

        const title = "ابطال سفارشکار تعمیرات";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setWorkOrderStatusToCancel);
    }

    function showProveEndPopup() {
        const title = "تاییدیه اتمام تعمیر دستگاه";
        const content = "آیا تائید می کنید که دستگاه با موفقیت تعمیر یافته و به شماره تحویل داده شده است؟";

        motorsazanClient.messageModal.confirm(content, proveEnd, title);

    }

    function setWorkOrderStatusToNotConfirmFinishByCustomerPopUp() {
        const url = controllerName + "ShowSetWorkOrderStatusToNotConfirmFinishByCustomer";
        state.GridMemberFocusedRowIndex = dom.ProductiveWorkOrderSourceGird.GetFocusedRowIndex();

        const apiParam = {
            WorkOrderId: state.GridMemberFocusedRowIndex
        };

        var title = "عدم تائید اتمام سفارشکار";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setWorkOrderStatusToNotConfirmFinish);
    }

    function showFailEndBtnPopup() {
        const title = "عدم اتمام تعمیر دستگاه";
        const content = "دلیل شما با موفقیت ثبت شد، آیا شما نیاز به ایجاد یک سفارشکار مجدد برای رسیدگی به این مشکل هستید؟";

        motorsazanClient.messageModal.confirm(content, failEnd, title);
    }

    function setWorkOrderStatusToCancel() {
        dom.cancelDescription = ASPxClientMemo.Cast("cancelDescription");
        dom.setCancelBtn = ASPxClientButton.Cast("setCancelBtn");
    }

    function setWorkOrderStatusToNotConfirmFinish() {
        dom.notConfirmFinishDescription = ASPxClientMemo.Cast("notConfirmFinishDescription");
        dom.setNotConfirmFinishBtn = ASPxClientButton.Cast("setNotConfirmFinishBtn");

    }

    async function setWorkOrderStatusToCancelByCustomer() {
        const url = controllerName + "RemoveWorkOrder";

        const workOrderId = getWorkOrderGridActiveRowKey();
        const description = dom.cancelDescription.GetValue();

        const apiParam = {
            workOrderId,
            description
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        dom.netExpertHub.server.workOrderStatusChanged(workOrderId);
        dom.stockManHub.server.workOrderStatusChanged(workOrderId);

        filterWorkOrderList();
        motorsazanClient.messageModal.success(apiResult);
        motorsazanClient.contentModal.hide();
    }

    async function setWorkOrderStatusToNotConfirmFinishByCustomer() {
        const url = controllerName + "FailEndWorkOrder";

        const workOrderId = getWorkOrderGridActiveRowKey();
        const description = dom.notConfirmFinishDescription.GetValue();

        const apiParam = {
            workOrderId,
            description
        };

        await motorsazanClient.connector.post(url, apiParam);

        dom.netExpertHub.server.workOrderStatusChanged(workOrderId);
        dom.stockManHub.server.workOrderStatusChanged(workOrderId);

        filterWorkOrderList();
        showFailEndBtnPopup();
        motorsazanClient.contentModal.hide();
    }

    function startSignalRHub() {
        $.connection.hub.start({ pingInterval: 5000 });
    }

    async function proveEnd() {
        const url = controllerName + "ProveEndWorkOrder";

        const workOrderId = getWorkOrderGridActiveRowKey();

        const apiParam = {
            workOrderId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        dom.netExpertHub.server.workOrderStatusChanged(workOrderId);
        dom.stockManHub.server.workOrderStatusChanged(workOrderId);

        filterWorkOrderList();
        motorsazanClient.messageModal.hide();
        motorsazanClient.messageModal.success(apiResult);
    }

    function failEnd() {
        state.GridMemberFocusedRowIndex = dom.ProductiveWorkOrderSourceGird.GetFocusedRowIndex();
        const apiParam = {
            WorkOrderId: getWorkOrderGridActiveRowKey()
        };
        const url = "/ProductiveWorkOrder/DenyEndWorkOrderAndCreateNewWorkOrder";
        motorsazanClient.connector.post(url, apiParam)
            .then(function (apiResult) {
                filterWorkOrderList();
                motorsazanClient.messageModal.hide();
                motorsazanClient.messageModal.success(apiResult);
            });
    }

    function myRatingInit() {
        rateWorkOrder();
        const url = controllerName + "InitRate";
        const apiParam = {
            workOrderId: getWorkOrderGridActiveRowKey()
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (response) {
                const ratingValues = response.split(" ");
                $("#lbMyVote").html(voteTitles[ratingValues - 1]);
                dom.myRating.SetValue(ratingValues);
            });

    }

    function myRatingItemClick() {
        rateWorkOrder();
        const url = controllerName + "UpdateRating";
        const apiParam = {
            workOrderId: getWorkOrderGridActiveRowKey(),
            RepairingRate: dom.myRating.GetValue()

        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function () {
                const myRate = dom.myRating.GetValue();
                $("#lbMyVote").html(voteTitles[myRate - 1]);
                dom.myRating.SetValue(myRate);
            });

    }

    function myRatingItemMouseOver(s, e) {
        votePrevTitle = $("#lbMyVote").html();
        $("#lbMyVote").html(voteTitles[e.index]);
    }

    function myRatingItemMouseOut() {
        $("#lbMyVote").html(votePrevTitle);
    }

    function showUsedToolsModal() {
        const url = controllerName + "WorKOrderConsumableList";
        const apiParam = {
            workOrderId: state.WorkOrderId
        };
        const title = "مشاهده مواد مصرفی";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom);
    }

    function showHistoryModal() {
        const url = controllerName + "ShowReferenceHistory";
        const apiParam = {
            workOrderId: state.WorkOrderId
        };
        const title = "مشاهده تاریخچه ارجاعات";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom);
    }

    function updateFilterFormDateSectionVisibility() {
        const activeDateType = dom.filterFormDateTypeCombo.GetValue();
        const isSpecialTypeSelected = activeDateType === state.specialDateTypeFilter;


        if (isSpecialTypeSelected) {
            tools.showItem(dom.filterFormSpecialDateColumn);
            dom.filterFormDateTypeColumn.removeClass("col-md-4").addClass("col-md-2");
        }
        else {
            tools.hideItem(dom.filterFormSpecialDateColumn);
            dom.filterFormDateTypeColumn.removeClass("col-md-2").addClass("col-md-4");
        }
    }

    function handleAddFormIsHasScrapedProductiveWorkOrderRadioChanged() {
        tools.hideItem(dom.addFormScrapRadioError);
    }

    $(document).ready(init);

    return {
        addProductiveWorkOrder: addProductiveWorkOrder,
        filterWorkOrderList: filterWorkOrderList,
        updateFilterFormDateSectionVisibility: updateFilterFormDateSectionVisibility,
        loadMachineList: loadMachineList,
        handleAddFormMachinesComboBeginCallback: handleAddFormMachinesComboBeginCallback,
        handleAddFormFaultDescriptionValueChanged: handleAddFormFaultDescriptionValueChanged,
        handleProductiveWorkOrderGridCallbackUrl: handleProductiveWorkOrderGridCallbackUrl,
        setProductiveWorkOrderGridFocusedRowOnExpanding: setProductiveWorkOrderGridFocusedRowOnExpanding,
        showActionHistoryModal: showActionHistoryModal,
        showUsedToolsModal: showUsedToolsModal,
        showHistoryModal: showHistoryModal,
        myRatingInit: myRatingInit,
        myRatingItemClick: myRatingItemClick,
        myRatingItemMouseOver: myRatingItemMouseOver,
        myRatingItemMouseOut: myRatingItemMouseOut,
        showRemoveConfirmation: showRemoveConfirmation,
        setWorkOrderStatusToCancelByCustomer: setWorkOrderStatusToCancelByCustomer,
        showProveEndPopup: showProveEndPopup,
        setWorkOrderStatusToNotConfirmFinishByCustomerPopUp: setWorkOrderStatusToNotConfirmFinishByCustomerPopUp,
        setWorkOrderStatusToNotConfirmFinishByCustomer: setWorkOrderStatusToNotConfirmFinishByCustomer,
        showRateBtnPopup: showRateBtnPopup,
        handleAddFormIsHasScrapedProductiveWorkOrderRadioChanged: handleAddFormIsHasScrapedProductiveWorkOrderRadioChanged,
        machineListSelectedIndexChanged: machineListSelectedIndexChanged,
        maintenanceGroupListSelectedIndexChanged: maintenanceGroupListSelectedIndexChanged,
        stopTypeListSelectedIndexChanged: stopTypeListSelectedIndexChanged
    };

})();

