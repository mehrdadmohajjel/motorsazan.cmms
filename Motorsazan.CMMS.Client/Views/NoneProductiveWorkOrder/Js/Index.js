/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Dependencies/SignalR/jquery.signalR-2.4.2.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.noneProductiveWorkOrder = (function () {

    var dom = {};
    var state = {
        specialDateTypeFilter: "CustomPeriod",
        GridFocusedRowIndex: 0,
        GridMemberFocusedRowIndex: 0,
        GroupId: 0,
        WorkOrderId: 0,
        statusId: 0,
        dateType: 0,
        startDate: "",
        endDate: ""

    };
    var Votetitles = ["خیلی کم", "کم", "متوسط", "زیاد", "خیلی زیاد"];
    var VoteprevTitle;

    var tools = motorsazanClient.tools;
    var controllerName = "/NoneProductiveWorkOrder/";



    async function addNoneProductiveWorkOrder() {
        const canContinue = isAddFormValid();
        if (!canContinue) return false;

        const url = controllerName + "AddNewNoneProductiveWorkOrder";

        const apiParam = {
            DepartmentId: dom.addFormEmployeeDepartmentCombo.GetValue(),
            MaintenanceGroupId: dom.addFormMaintenanceGroupListCombo.GetValue(),
            StopTypeId: dom.addFormStopTypeListCombo.GetValue(),
            Description: dom.addFormFaultDescription.GetValue()
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        dom.netExpertHub.server.newWorkOrderAdded();
        dom.stockManHub.server.newWorkOrderAdded();

        filterWorkOrderList();
        motorsazanClient.messageModal.success(apiResult);
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

    function resetAddForm() {
        dom.addFormEmployeeDepartmentCombo.SetSelectedIndex(-1);
        dom.addFormMaintenanceGroupListCombo.SetSelectedIndex(-1);
        dom.addFormStopTypeListCombo.SetSelectedIndex(-1);
        dom.addFormFaultDescription.SetText('');
        tools.hideItem(dom.addFormMaintenanceGroupComboError);
        tools.hideItem(dom.addFormEmployeeDepartmentComboError);
        tools.hideItem(dom.addFormStopTypeListComboError);
        tools.hideItem(dom.addFormFaultDescriptionError);

    }

    async function filterWorkOrderList() {
        const canContinue = isFilterFormValid();
        if (!canContinue) return false;

        state.statusId = dom.filterFormStatusTypeCombo.GetValue();

        state.dateType = dom.filterFormDateTypeCombo.GetValue();
        state.startDate = dom.filterFormStartDate.val();
        state.endDate = dom.filterFormEndDate.val();

        const url = controllerName + "NoneProductiveWorkOrderGird";

        const apiParam = {
            workOrderStatusTypeId: state.statusId,
            dateType: state.dateType,
            startShamsiDate: state.startDate,
            endPersianDate: state.endDate
        };

        const groupGridSource = await motorsazanClient.connector.post(url, apiParam);
        dom.NoneProductiveWorkOrderSourceGirdParent.html(groupGridSource);
        setDom();
    }

    function handleNoneProductiveWorkOrderGridCallbackUrl(source) {
        state.statusId = dom.filterFormStatusTypeCombo.GetValue();
        state.dateType = dom.filterFormDateTypeCombo.GetValue();
        state.startDate = dom.filterFormStartDate.val();
        state.endDate = dom.filterFormEndDate.val();

        const url = controllerName + "NoneProductiveWorkOrderGird";

        source.callbackUrl = url + "?workOrderStatusTypeId=" + state.statusId +
            "&dateType=" + state.dateType + "&startShamsiDate=" + state.startDate +
            "&endPersianDate=" + state.endDate;
    }

    function handleAddFormFaultDescriptionValueChanged() {
        tools.hideItem(dom.addFormFaultDescriptionError);
    }

    function handleFilterFormEndDateChange() {
        tools.hideItem(dom.filterFormEndDateError);
    }

    function handleFilterFormStartDateChange() {
        tools.hideItem(dom.filterFormStartDateError);
    }

    function isAddFormValid() {
        var isValid = true;

        tools.hideItem(dom.addFormEmployeeDepartmentComboError);
        var department = dom.addFormEmployeeDepartmentCombo.GetValue();
        var isDepartmentValid = !tools.isNullOrEmpty(department);
        if (!isDepartmentValid) {
            isValid = false;
            tools.showItem(dom.addFormEmployeeDepartmentComboError);
        }

        tools.hideItem(dom.addFormMaintenanceGroupComboError);
        var maintenanceGroup = dom.addFormMaintenanceGroupListCombo.GetValue();
        var isMaintenanceGroupValid = !tools.isNullOrEmpty(maintenanceGroup);
        if (!isMaintenanceGroupValid) {
            isValid = false;
            tools.showItem(dom.addFormMaintenanceGroupComboError);
        }

        tools.hideItem(dom.addFormStopTypeListComboError);
        var stopType = dom.addFormStopTypeListCombo.GetValue();
        var isStopTypeValid = !tools.isNullOrEmpty(stopType);
        if (!isStopTypeValid) {
            isValid = false;
            tools.showItem(dom.addFormStopTypeListComboError);
        }

        tools.hideItem(dom.addFormFaultDescriptionError);
        var faultDescription = dom.addFormFaultDescription.GetValue();
        var isFaultDescription = !tools.isNullOrEmpty(faultDescription);
        if (!isFaultDescription) {
            isValid = false;
            tools.showItem(dom.addFormFaultDescriptionError);
        }

        return isValid;
    }

    function isFilterFormValid() {

        var isValid = true;

        tools.hideItem(dom.filterFormStatusTypeComboError);
        const formStatusType = dom.filterFormStatusTypeCombo.GetValue();
        const isFormStatusTypeValid = !tools.isNullOrEmpty(formStatusType);
        if (!isFormStatusTypeValid) {
            tools.showItem(dom.filterFormStatusTypeComboError);
            isValid = false;
        }


        tools.hideItem(dom.filterFormDateTypeComboError);
        const kanbanType = dom.filterFormDateTypeCombo.GetValue();
        const isKanbanSelected = !tools.isNullOrEmpty(kanbanType);

        if (!isKanbanSelected) {
            tools.showItem(dom.filterFormDateTypeComboError);
            isValid = false;
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
        dom.addFormEmployeeDepartmentComboParent = $("#addFormEmployeeDepartmentComboParent");
        dom.addFormEmployeeDepartmentComboError = $("#addFormEmployeeDepartmentComboError");
        dom.addFormEmployeeDepartmentCombo = ASPxClientComboBox.Cast("addFormEmployeeDepartmentCombo");

        dom.addFormMaintenanceGroupComboParent = $("#addFormMaintenanceGroupComboParent");
        dom.addFormMaintenanceGroupComboError = $("#addFormMaintenanceGroupComboError");
        dom.addFormMaintenanceGroupListCombo = ASPxClientComboBox.Cast("addFormMaintenanceGroupListCombo");

        dom.addFormStopTypeListComboParent = $("#addFormStopTypeListComboParent");
        dom.addFormStopTypeListComboError = $("#addFormStopTypeListComboError");
        dom.addFormStopTypeListCombo = ASPxClientComboBox.Cast("addFormStopTypeListCombo");

        dom.addFormFaultDescriptionParent = $("#addFormFaultDescriptionParent");
        dom.addFormFaultDescriptionError = $("#addFormFaultDescriptionError");
        dom.addFormFaultDescription = ASPxClientMemo.Cast("addFormFaultDescription");

        dom.NoneProductiveWorkOrderSourceGirdParent = $("#NonProductiveWorkOrderSourceGirdParent");
        dom.NoneProductiveWorkOrderSourceGird = ASPxClientGridView.Cast("NoneProductiveWorkOrderSourceGird");

        //---------Filter
        dom.filterFormDateTypeComboError = $("#filterFormDateTypeComboError");
        dom.filterFormStatusTypeComboError = $("#filterFormStatusTypeComboError");
        dom.filterFormStatusTypeCombo = ASPxClientComboBox.Cast("filterFormStatusTypeCombo");
        dom.filterFormDateTypeCombo = ASPxClientComboBox.Cast("filterFormDateTypeCombo");
        dom.filterFormStartDate = $("#filterFormStartDate");
        dom.filterFormStartDateError = $("#filterFormStartDateError");
        dom.filterFormEndDate = $("#filterFormEndDate");
        dom.filterFormEndDateError = $("#filterFormEndDateError");
        dom.filterFormSpecialDateColumn = $("#filterFormSpecialDateColumn");
        dom.filterFormDateTypeColumn = $("#filterFormDateTypeColumn");

    }
    function setNoneProductiveWorkOrderGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);
        state.WorkOrderId = dom.NoneProductiveWorkOrderSourceGird.GetRowKey(event.visibleIndex)

    }

    function showActionHistoryModal() {
        var url = controllerName + "ShowActionHistory";
        var apiParam = {
            workOrderId: state.WorkOrderId
        };
        var title = "مشاهده عملیات انجام شده";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom);
    }

    function showRemoveConfirmation() {
        var url = controllerName + "ShowSetWorkOrderStatusToCancelByCustomer";
        state.GridMemberFocusedRowIndex = dom.NoneProductiveWorkOrderSourceGird.GetFocusedRowIndex();

        var apiParam = {
            WorkOrderId: state.GridMemberFocusedRowIndex
        };

        var title = "ابطال سفارشکار تعمیرات";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, SetWorkOrderStatusToCancel);
    }

    function showProveEndPopup() {
        var title = "تاییدیه اتمام تعمیر دستگاه";
        var content = "آیا تائید می کنید که دستگاه با موفقیت تعمیر یافته و به شماه تحویل داده شده است؟";

        motorsazanClient.messageModal.confirm(content, proveEnd, title);

    }

    function setWorkOrderStatusToNotConfirmFinishByCustomerPopUp() {
        var url = controllerName + "ShowSetWorkOrderStatusToNotConfirmFinishByCustomer";
        state.GridMemberFocusedRowIndex = dom.NoneProductiveWorkOrderSourceGird.GetFocusedRowIndex();

        var apiParam = {
            WorkOrderId: state.GridMemberFocusedRowIndex
        };

        var title = "عدم تائید اتمام سفارشکار";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setWorkOrderStatusToNotConfirmFinish);
    }

    function showFailEndBtnPopup() {
        var title = "عدم اتمام تعمیر دستگاه";
        var content = "دلیل شما با موفقیت ثبت شد، آیا شما نیاز به ایجاد یک سفارشکار مجدد برای رسیدگی به این مشکل هستید؟";

        motorsazanClient.messageModal.confirm(content, failEnd, title);
    }

    function SetWorkOrderStatusToCancel() {
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


    function rateWorkOrder() {
        dom.ratingPanel = ASPxClientRoundPanel.Cast("ratingPanel");
        dom.myRating = ASPxClientRatingControl.Cast("myRating");
        dom.lbMyVote = $("#lbMyVote");
    }

    function showRateBtnPopup() {
        var url = controllerName + "RatePeople";
        state.GridMemberFocusedRowIndex = dom.NoneProductiveWorkOrderSourceGird.GetFocusedRowIndex();

        var apiParam = {
            WorkOrderId: state.GridMemberFocusedRowIndex
        };

        var title = "میزان رضایت از تعمیر";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, rateWorkOrder);
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
        state.GridMemberFocusedRowIndex = dom.NoneProductiveWorkOrderSourceGird.GetFocusedRowIndex();
        var apiParam = {
            WorkOrderId: getWorkOrderGridActiveRowKey()
        };
        var url = "/NoneProductiveWorkOrder/DenyEndWorkOrderAndCreateNewWorkOrder";
        motorsazanClient.connector.post(url, apiParam)
            .then(function (apiResult) {
                filterWorkOrderList();
                motorsazanClient.messageModal.hide();
                motorsazanClient.messageModal.success(apiResult);
            });
    }

    function getWorkOrderGridActiveRowKey() {
        var activeIndex = dom.NoneProductiveWorkOrderSourceGird.GetFocusedRowIndex();
        return dom.NoneProductiveWorkOrderSourceGird.GetRowKey(activeIndex);
    }

    function myRating_Init(s, e) {
        rateWorkOrder();
        var url = controllerName + "InitRate";
        var apiParam = {
            workorderId: getWorkOrderGridActiveRowKey()
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (response) {
                var ratingValues = response.split(' ');
                $("#lbMyVote").html(Votetitles[ratingValues - 1]);
                dom.myRating.SetValue(ratingValues);
            });

    }

    function myRating_ItemClick(s, e) {
        rateWorkOrder();
        const url = controllerName + "UpdateRating";
        const apiParam = {
            workorderId: getWorkOrderGridActiveRowKey(),
            RepairingRate: dom.myRating.GetValue()

        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (response) {
                var myRate = dom.myRating.GetValue();
                $("#lbMyVote").html(Votetitles[myRate - 1]);
                dom.myRating.SetValue(myRate);
            });

    }

    function myRating_ItemMouseOver(s, e) {
        VoteprevTitle = $("#lbMyVote").html();
        $("#lbMyVote").html(Votetitles[e.index]);
    }

    function myRating_ItemMouseOut(s, e) {
        $("#lbMyVote").html(VoteprevTitle);
    }

    function showUsedToolsModal() {
        var url = controllerName + "GetWorKOrderConsumableListByWorkOrderId";
        var apiParam = {
            workOrderId: state.WorkOrderId
        };
        var title = "مشاهده مواد مصرفی";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setDom);
    }

    function showHistoryModal() {
        var url = controllerName + "ShowReferenceHistory";
        var apiParam = {
            workOrderId: state.WorkOrderId
        };
        var title = "مشاهده تاریخچه ارجاعات";
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

    function stopTypeListComboChanged() {
        tools.hideItem(dom.addFormStopTypeListComboError);
    }
    function employeeDepartmentComboChanged() {
        tools.hideItem(dom.addFormEmployeeDepartmentComboError);
    }
    function maintenanceGroupComboChanged() {
        tools.hideItem(dom.addFormMaintenanceGroupComboError);
    }

    function init() {
        setDom();

        initSignalR();

        dom.filterFormStartDate.on("change", handleFilterFormStartDateChange);
        dom.filterFormEndDate.on("change", handleFilterFormEndDateChange);
    }

    function initSignalR() {
        dom.netExpertHub = $.connection.netExpertHub;
        dom.stockManHub = $.connection.stockManHub;

        $.connection.hub.stateChanged(detectSignalRDisconnection);

        startSignalRHub();
    }

    function startSignalRHub() {
        $.connection.hub.start({ pingInterval: 5000 });
    }

    $(document).ready(init);

    return {
        addNoneProductiveWorkOrder: addNoneProductiveWorkOrder,
        filterWorkOrderList: filterWorkOrderList,
        handleNoneProductiveWorkOrderGridCallbackUrl: handleNoneProductiveWorkOrderGridCallbackUrl,
        handleAddFormFaultDescriptionValueChanged: handleAddFormFaultDescriptionValueChanged,
        handleFilterFormStartDateChange: handleFilterFormStartDateChange,
        handleFilterFormEndDateChange: handleFilterFormEndDateChange,
        setNoneProductiveWorkOrderGridFocusedRowOnExpanding: setNoneProductiveWorkOrderGridFocusedRowOnExpanding,
        showActionHistoryModal: showActionHistoryModal,
        showUsedToolsModal: showUsedToolsModal,
        showHistoryModal: showHistoryModal,
        myRating_Init: myRating_Init,
        myRating_ItemClick: myRating_ItemClick,
        myRating_ItemMouseOver: myRating_ItemMouseOver,
        myRating_ItemMouseOut: myRating_ItemMouseOut,
        showRemoveConfirmation: showRemoveConfirmation,
        setWorkOrderStatusToCancelByCustomer: setWorkOrderStatusToCancelByCustomer,
        showProveEndPopup: showProveEndPopup,
        setWorkOrderStatusToNotConfirmFinishByCustomerPopUp: setWorkOrderStatusToNotConfirmFinishByCustomerPopUp,
        setWorkOrderStatusToNotConfirmFinishByCustomer: setWorkOrderStatusToNotConfirmFinishByCustomer,
        showRateBtnPopup: showRateBtnPopup,
        updateFilterFormDateSectionVisibility: updateFilterFormDateSectionVisibility,
        stopTypeListComboChanged: stopTypeListComboChanged,
        maintenanceGroupComboChanged: maintenanceGroupComboChanged,
        employeeDepartmentComboChanged: employeeDepartmentComboChanged
    };

})();