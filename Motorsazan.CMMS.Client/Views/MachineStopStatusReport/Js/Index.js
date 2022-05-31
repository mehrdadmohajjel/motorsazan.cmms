///<reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.machineStopStatusReport = (function() {

    var dom = {};

    var tools = motorsazanClient.tools;
    var controllerName = "/machineStopStatusReport/";
    var state = {
        machineId: 0,
        workOrderId:0
    };
    
    function handleResultGridBeginCallback(source) {
        var url = controllerName + "ShowGrid";
        source.callbackUrl = url;
    }

    function handleDetailGridCallbackUrl(source) {
        var url = controllerName + "ShowWorkOrderDetail";
        source.callbackUrl = url + "?machineId=" + state.machineId ;
    }

    function setWorkOrderGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);
        state.workOrderId = dom.workOrderDetailsourceGird.GetRowKey(event.visibleIndex);
    }

    function handleGridCustomBtnClick(source, event) {
        state.machineId =
            dom.sourceGird.GetFocusedRowIndex();
        var customBtnId = event.buttonID;

        if (customBtnId === "showStatusbtn") return showStatusbtnConfirmation();
    }

    function showStatusbtnConfirmation() {
        var url = controllerName + "ShowWorkOrderDetail";

        var activeIndex = dom.sourceGird.GetFocusedRowIndex();
        state.machineId = dom.sourceGird.GetRowKey(activeIndex);

        var apiParam = {
            MachineId: state.machineId
        };

        var title = "لیست سفارشکارها";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, setWorderDetailDom,false,true);
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

    function init() {
        setDom();
        setEvents();
    }


    function setDom() {

        //Grid
        dom.sourceGirdParent = $("#sourceGirdParent");
        dom.sourceGird = ASPxClientGridView.Cast("sourceGird");
    }
    function setWorderDetailDom() {

        //Grid
        dom.workOrderDetailsourceGird = ASPxClientGridView.Cast("workOrderDetailsourceGird");
    }

    function setEvents() {
    }

    $(document).ready(init);

    return {
        handleResultGridBeginCallback: handleResultGridBeginCallback,
        handleDetailGridCallbackUrl: handleDetailGridCallbackUrl,
        handleGridCustomBtnClick: handleGridCustomBtnClick,
        showActionHistoryModal: showActionHistoryModal,
        showUsedToolsModal: showUsedToolsModal,
        showHistoryModal: showHistoryModal,
        showRegisterInfoModal: showRegisterInfoModal,
        setWorkOrderGridFocusedRowOnExpanding: setWorkOrderGridFocusedRowOnExpanding
    };
})();