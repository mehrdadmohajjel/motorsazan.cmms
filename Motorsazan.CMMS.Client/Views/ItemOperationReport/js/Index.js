/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.itemOperationReport = (function () {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod"
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/ItemOperationReport/";

    async function fillGrid() {
        if (!isFilterFormValid(false))
            return;

        const url = controllerName + "Grid";

        const machineId = dom.filterFormMachineIdCombo.GetValue();
        const itemTypeId = dom.filterFormItemTypeCombo.GetValue();
        const departmentId = dom.filterFormSubDepartmentIdCombo.GetValue();
        const apiParam = {
            MachineId: machineId,
            operationTypeId: itemTypeId,
            departmentId: departmentId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.gridParent.html(gridHtml);
        setDom();
    }



    async function handleFilterFormDepartmentIdComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDepartmentIdComboError);
        const url = controllerName + "FilterFormSubDepartmentIdCombo";

        const departmentId = dom.filterFormDepartmentIdCombo.GetValue();


        const apiParam = {
            departmentId: departmentId
        };

        const subDepartmentHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.filterFormSubDepartmentIdComboParent.html(subDepartmentHtml);
        setDom();
        dom.filterFormSubDepartmentIdCombo.SetEnabled(true);
    }

    function handleFilterFormMachineIdComboBeginCallback(command) {

        const subDepartmentId = dom.filterFormSubDepartmentIdCombo.GetValue();
        const isSubDepartmentIdSelected = !tools.isNullOrEmpty(subDepartmentId);
        if (isSubDepartmentIdSelected) {
            command.callbackUrl =
                controllerName +
                "FilterFormMachineIdCombo" +
                "?DepartmentId=" +
                subDepartmentId;
        }
    }

    function handleFilterFormMachineIdComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormMachineIdComboError);

        dom.filterFormItemTypeCombo.SetEnabled(true);
    }

    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
    }

    function handleFilterFormSubDepartmentIdComboBeginCallback(command) {

        const departmentId = dom.filterFormDepartmentIdCombo.GetValue();
        const isDepartmentIdSelected = !tools.isNullOrEmpty(departmentId);
        if (isDepartmentIdSelected) {
            command.callbackUrl =
                controllerName +
                "FilterFormSubDepartmentIdCombo" +
                "?DepartmentId=" +
                departmentId;
        }
    }

    async function handleFilterFormSubDepartmentIdComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormSubDepartmentIdComboError);
        const url = controllerName + "FilterFormMachineIdCombo";

        const departmentId = dom.filterFormSubDepartmentIdCombo.GetValue();

        const apiParam = {
            departmentId: departmentId
        };

        const machineHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.filterFormMachineIdComboParent.html(machineHtml);
        setDom();
        dom.filterFormMachineIdCombo.SetEnabled(true);
    }

    function handleFilterFormItemTypeComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormItemTypeComboError);

    }

    function handleFilterFormSearchButtonClick() {
        if (!isFilterFormValid())
            return;

        fillGrid();
    }

    function handleGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            command.callbackUrl =
                controllerName + "Grid" + "?machineId=" + -1+"&ItemTypeId="+ -1+"&departmentId="+ -1;
            return;
        }
        var machineId =
            +dom.filterFormMachineIdCombo.GetValue() > -1
                ? dom.filterFormMachineIdCombo.GetValue()
                : -1;
        var itemTypeId = + dom.filterFormItemTypeCombo.GetValue() > -1 ? dom.filterFormItemTypeCombo.GetValue() : -1;
        var departmentId = + dom.filterFormSubDepartmentIdCombo.GetValue() > -1 ? dom.filterFormSubDepartmentIdCombo.GetValue() : -1;
        command.callbackUrl =
            controllerName + "Grid" + "?machineId=" + machineId + "&operationTypeId=" + itemTypeId + "&departmentId=" + departmentId;
    }

    function init() {
        setDom();
        setEvents();

        dom.filterFormSubDepartmentIdCombo.SetEnabled(false);
        dom.filterFormMachineIdCombo.SetEnabled(false);
        dom.filterFormItemTypeCombo.SetEnabled(false);
    }

    function isFilterFormValid(shouldDisplayValidationErrors = true) {
        var isValid = true;

        tools.hideItem(dom.filterFormDepartmentIdComboError);
        const departmentId = dom.filterFormDepartmentIdCombo.GetValue();
        const isDepartmentIdSelected = !tools.isNullOrEmpty(departmentId);
        if (!isDepartmentIdSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormDepartmentIdComboError);
        }

        tools.hideItem(dom.filterFormSubDepartmentIdComboError);
        const subDepartmentId = dom.filterFormSubDepartmentIdCombo.GetValue();
        const isSubDepartmentIdSelected = !tools.isNullOrEmpty(subDepartmentId);
        if (!isSubDepartmentIdSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormSubDepartmentIdComboError);
        }

        tools.hideItem(dom.filterFormMachineIdComboError);
        const machineId = dom.filterFormMachineIdCombo.GetValue();
        const isSubMachineIdSelected = !tools.isNullOrEmpty(machineId);
        if (!isSubMachineIdSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormMachineIdComboError);
        }

        tools.hideItem(dom.filterFormItemTypeComboError);
        const itemType = dom.filterFormItemTypeCombo.GetValue();
        const isitemTypeSelected = !tools.isNullOrEmpty(itemType);
        if (!isitemTypeSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormItemTypeComboError);
        }


        return isValid;
    }

    function setDom() {
        //FilterForm
        dom.filterFormDepartmentIdCombo = ASPxClientComboBox.Cast("filterFormDepartmentIdCombo");
        dom.filterFormDepartmentIdComboParent = $("#filterFormDepartmentIdComboParent");
        dom.filterFormDepartmentIdComboError = $("#filterFormDepartmentIdComboError");

        dom.filterFormSubDepartmentIdCombo = ASPxClientComboBox.Cast("filterFormSubDepartmentIdCombo");
        dom.filterFormSubDepartmentIdComboParent = $("#filterFormSubDepartmentIdComboParent");
        dom.filterFormSubDepartmentIdComboError = $("#filterFormSubDepartmentIdComboError");

        dom.filterFormMachineIdCombo = ASPxClientComboBox.Cast("filterFormMachineIdCombo");
        dom.filterFormMachineIdComboParent = $("#filterFormMachineIdComboParent");
        dom.filterFormMachineIdComboError = $("#filterFormMachineIdComboError");

        dom.filterFormItemTypeCombo = ASPxClientComboBox.Cast("filterFormItemTypeCombo");
        dom.filterFormItemTypeComboParent = $("#filterFormItemTypeComboParent");
        dom.filterFormItemTypeComboError = $("#filterFormItemTypeComboError");

        

        //Grid
        dom.gridParent = $("#gridParent");
        dom.grid = ASPxClientGridView.Cast("grid");
    }

    function setEvents() {
    }

    $(document).ready(init);

    return {
        handleFilterFormDepartmentIdComboSelectedIndexChange: handleFilterFormDepartmentIdComboSelectedIndexChange,
        handleFilterFormSubDepartmentIdComboBeginCallback: handleFilterFormSubDepartmentIdComboBeginCallback,
        handleFilterFormSubDepartmentIdComboSelectedIndexChange: handleFilterFormSubDepartmentIdComboSelectedIndexChange,
        handleFilterFormMachineIdComboBeginCallback: handleFilterFormMachineIdComboBeginCallback,
        handleFilterFormMachineIdComboSelectedIndexChange: handleFilterFormMachineIdComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleFilterFormItemTypeComboSelectedIndexChange: handleFilterFormItemTypeComboSelectedIndexChange,
        handleGridBeginCallback: handleGridBeginCallback
        
    };

})();
