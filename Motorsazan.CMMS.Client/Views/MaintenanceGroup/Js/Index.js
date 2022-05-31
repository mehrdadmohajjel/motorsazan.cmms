/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.maintenanceGroup = (function () {

    var dom = {};
    var state = {
        maintenanceGroupRowIndex: 0,
        maintenanceGroupMemberRowIndex:0
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/MaintenanceGroup/";

    async function refreshMaintenanceGroupMemberGrid() {

        const url = controllerName + "MaintenanceGroupMemberGird";
        const apiParam = {
            maintenanceGroupId: state.maintenanceGroupRowIndex
        };
        
        const grid = await motorsazanClient.connector.post(url, apiParam);
        dom.maintenanceGroupMemberGirdParent.html(grid);
        setDom();
    }

    function handleAddFormEmployeeComboSelectedIndexChanged() {
        tools.hideItem(dom.addFormEmployeeComboError);
    }

    async function handleAssignEmployeeToMaintenanceGroupBtn() {

        if (!isAssignEmployeeToMaintenanceGroupFormValid()) return false;
        const url = controllerName + "AssignEmployeeToMaintenanceGroup";
        const apiParam = {
            EmployeeId: dom.addFormEmployeeCombo.GetValue(),
            MaintenanceGroupId: state.maintenanceGroupRowIndex
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        await refreshMaintenanceGroupMemberGrid();
        motorsazanClient.messageModal.success(apiResult);
        dom.addFormEmployeeCombo.SetSelectedIndex(-1);
        return false;
    }

    function handleGridCustomBtnClick(source, event) {
        setDom();
        assignEmployeeToMaintenanceGroupDom();
        state.maintenanceGroupRowIndex =
            dom.maintenanceGroupGird.GetRowKey(event.visibleIndex);
        const customBtnId = event.buttonID;

        if (customBtnId === "assignEmployeeToMaintenanceGroupBtn") return showAssignEmployeeToMaintenanceGroupForm();
        return showMaintenanceGroupMember();
    }

    function isAssignEmployeeToMaintenanceGroupFormValid() {
        var isValid = true;

        tools.hideItem(dom.addFormEmployeeComboError);
        const employeeId = dom.addFormEmployeeCombo.GetValue();
        const isEmployeeValid = !tools.isNullOrEmpty(employeeId);
        if (!isEmployeeValid) {
            isValid = false;
            tools.showItem(dom.addFormEmployeeComboError);
        }

        return isValid;
    }

    function setDom() {
        dom.maintenanceGroupGirdParent = $("#maintenanceGroupGirdParent");
        dom.maintenanceGroupGird = ASPxClientGridView.Cast("maintenanceGroupGird");

    }

    function maintenanceGroupGirdBeginCallback(source) {
        const url = controllerName + "MaintenanceGroupGird";
        source.callbackUrl = url ;
    }

    function handleMaintenanceGroupMemberGridCallback(source) {
        const url = controllerName + "MaintenanceGroupMemberGird";
        source.callbackUrl = url + "?MaintenanceGroupId=" + state.maintenanceGroupRowIndex;

    }

    function showMaintenanceGroupMemberGidBeginCallback(source) {
        const url = controllerName + "ShowMaintenanceGroupMemberGird";
        source.callbackUrl = url + "?MaintenanceGroupId=" + state.maintenanceGroupRowIndex;

    }

    function showMaintenanceGroupMember() {
        const url = controllerName + "ShowMaintenanceGroupMemberGird";

        const apiParam = {
            MaintenanceGroupId: state.maintenanceGroupRowIndex
        };

        const title = "مشاهده نفرات گروه تعمیراتی";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, showMaintenanceGroupMemberDom);
    }

    function showMaintenanceGroupMemberDom() {
        dom.showMaintenanceGroupMemberGid = ASPxClientGridView.Cast("showMaintenanceGroupMemberGid");
    }

    $(document).ready(setDom);

    function showAssignEmployeeToMaintenanceGroupForm() {
        const url = controllerName + "AssignEmployeeToMaintenanceGroupForm";
        const apiParam = {
            maintenanceGroupId: state.maintenanceGroupRowIndex
        };

        const title = "تخصیص نفر به گروه تعمیراتی";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, assignEmployeeToMaintenanceGroupDom);

    }

    function assignEmployeeToMaintenanceGroupDom() {
        dom.addFormEmployeeCombo = ASPxClientComboBox.Cast("addFormEmployeeCombo");
        dom.addFormEmployeeComboError = $("#addFormEmployeeComboError");

        dom.maintenanceGroupMemberGirdParent = $("#maintenanceGroupMemberGirdParent");
        dom.maintenanceGroupMemberGird = ASPxClientGridView.Cast("maintenanceGroupMemberGird");

    }

    function maintenanceGroupMemberGirdCustomButton(source, event) {
        setDom();
        assignEmployeeToMaintenanceGroupDom();
        state.maintenanceGroupMemberRowIndex =
            dom.maintenanceGroupMemberGird .GetRowKey(event.visibleIndex);
        const customBtnId = event.buttonID;
        if (customBtnId === "removeMaintenanceGroupMemberFromMaintenanceGroupBtn") return removeMaintenanceGroupMemberFromMaintenanceGroup();
        if (customBtnId === "setSuperviserToMaintenanceGroupMemberBtn") return setSuperviserToMaintenanceGroupMember();

    }

    function removeMaintenanceGroupMemberFromMaintenanceGroup() {
        const content = "آیا از حذف این ردیف مطمئن هستید؟";
        const title = "تاییدیه حذف";
        motorsazanClient.messageModal.confirm(content, removeMaintenanceGroupMemberFromMaintenanceGroupPopup, title);
    }

    async function removeMaintenanceGroupMemberFromMaintenanceGroupPopup() {
        const url = controllerName + "RemoveMaintenanceGroupMemberFromMaintenanceGroup";

        const apiParam = {
            maintenanceGroupMemberId: state.maintenanceGroupMemberRowIndex
        };

        const result = await motorsazanClient.connector.post(url, apiParam);
        await refreshMaintenanceGroupMemberGrid();
        motorsazanClient.messageModal.success(result);
    }


    function setSuperviserToMaintenanceGroupMember() {
        const content = "آیا از تغییر وضعیت مطمئن  هستید؟";
        const title = "تاییدیه تغییر وضعیت";
        motorsazanClient.messageModal.confirm(content, setSuperviserToMaintenanceGroupMemberPopup, title);
    }

    async function setSuperviserToMaintenanceGroupMemberPopup() {
        const url = controllerName + "SetSuperviserToMaintenanceGroupMember";

        const apiParam = {
            MaintenanceGroupMemberId: state.maintenanceGroupMemberRowIndex,
            MaintenanceGroupId: state.maintenanceGroupRowIndex
        };

        const result = await motorsazanClient.connector.post(url, apiParam);
        await refreshMaintenanceGroupMemberGrid();
        motorsazanClient.messageModal.success(result);
    }


    return {
        maintenanceGroupGirdBeginCallback: maintenanceGroupGirdBeginCallback,
        handleGridCustomBtnClick: handleGridCustomBtnClick,
        maintenanceGroupMemberGirdCustomButton: maintenanceGroupMemberGirdCustomButton,
        handleMaintenanceGroupMemberGridCallback: handleMaintenanceGroupMemberGridCallback,
        handleAddFormEmployeeComboSelectedIndexChanged: handleAddFormEmployeeComboSelectedIndexChanged,
        handleAssignEmployeeToMaintenanceGroupBtn: handleAssignEmployeeToMaintenanceGroupBtn,
        showMaintenanceGroupMemberGidBeginCallback: showMaintenanceGroupMemberGidBeginCallback

    };

})();

