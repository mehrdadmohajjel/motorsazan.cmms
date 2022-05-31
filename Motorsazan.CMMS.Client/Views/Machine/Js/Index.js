/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.machine = (function () {

    var dom = {};
    var tools = motorsazanClient.tools;
    var controllerName = "/Machine/";

    async function addNewMainMachine() {
        setDom();
        const canAdd = isAddFormValid();
        if (!canAdd) return false;

        const url = controllerName + "AddNewMachine";

        const isTools = dom.addFormIsTools.GetValue();

        const isToolsType = dom.addFormIsTools.GetSelectedIndex();

        var oldMachineCode;
        if (!isToolsType) {
            oldMachineCode = dom.addFormMachineOldMachineCodeIsTools.GetText();
        } else {
            oldMachineCode = dom.addFormMachineOldMachineCode.GetText();
        }

        const apiParam = {
            builderMachineCode: dom.addFormMainMachineSerial.GetText(),
            machineName: dom.addFormMainMachineName.GetText(),
            machineLevelId: dom.addFormMachineLevelCombo.GetValue(),
            machineTypeId: dom.addFormMachineTypeCombo.GetValue(),
            controlSystemTypeId: dom.addFormMachineControlCombo.GetValue(),
            model: dom.addFormMainMachineModel.GetText(),
            controlSystemName: dom.addFormMachineControlSystemName.GetText(),
            operationCode: dom.addFormMachineOperation.GetText(),
            systemModel: dom.addFormMachineControlSystemModelName.GetText(),
            isTools: isTools,
            oldMachineCode: oldMachineCode
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        motorsazanClient.messageModal.success(apiResult);
        setDom();
        resetForm();

        return true;
    }

    function handleAddFormIsToolsSelectedIndexChanged() {
        tools.hideItem(dom.addFormIsToolsError);

        const isTools = dom.addFormIsTools.GetSelectedIndex();
        if (!isTools) {
            tools.showItem(dom.addFormMachineOldMachineCodeIsToolsDesign);
            tools.hideItem(dom.addFormMachineOldMachineCodeDesign);

            dom.addFormMachineOldMachineCode.SetValue("");
            dom.addFormMachineOldMachineCodeIsTools.SetValue("");
        } else {
            tools.showItem(dom.addFormMachineOldMachineCodeDesign);
            tools.hideItem(dom.addFormMachineOldMachineCodeIsToolsDesign);

            dom.addFormMachineOldMachineCode.SetValue("");
            dom.addFormMachineOldMachineCodeIsTools.SetValue("");
        }
    }

    function handleAddFormMachineControlComboSelectedIndexChanged() {
        tools.hideItem(dom.addFormMachineControlComboError);
        const machineControl = dom.addFormMachineControlCombo.GetValue();
        const isMachineControlValid = !tools.isNullOrEmpty(machineControl);
        if (!isMachineControlValid) {
            tools.showItem(dom.addFormMachineControlComboError);
        }
    }

    function handleAddFormMachineLevelComboSelectedIndexChanged() {

        tools.hideItem(dom.addFormMachineLevelComboError);
        const machineLevel = dom.addFormMachineLevelCombo.GetValue();
        const isMachineLevelValid = !tools.isNullOrEmpty(machineLevel);
        if (!isMachineLevelValid) {
            tools.showItem(dom.addFormMachineLevelComboError);
        }
    }
    
    function handleAddFormMachineOperationValueChanged() {

        tools.hideItem(dom.addFormMachineOperationError);
        const machineOperation = dom.addFormMachineOperation.GetValue();
        const isMachineOperationValid = !tools.isNullOrEmpty(machineOperation);
        if (!isMachineOperationValid) {
            tools.showItem(dom.addFormMachineOperationError);
        }
    }
    
    function handleAddFormMachineOldMachineCodeValueChanged() {
        tools.hideItem(dom.addFormMachineOldMachineCodeError);
        const isOldMachineCodeValid = dom.addFormMachineOldMachineCode.GetIsValid();
        if (!isOldMachineCodeValid) {
            tools.showItem(dom.addFormMachineOldMachineCodeError);
        }
    }

    function handleAddFormMachineOldMachineCodeIsToolsValueChanged() {
        tools.hideItem(dom.addFormMachineOldMachineCodeIsToolsError);
        const isOldMachineCodeValid = dom.addFormMachineOldMachineCodeIsTools.GetIsValid();
        if (!isOldMachineCodeValid) {
            tools.showItem(dom.addFormMachineOldMachineCodeIsToolsError);
        }
    }

    function handleAddFormMachineTypeComboSelectedIndexChanged() {

        tools.hideItem(dom.addFormMachineTypeComboError);
        const machineType = dom.addFormMachineTypeCombo.GetValue();
        const isMachineTypeValid = !tools.isNullOrEmpty(machineType);
        if (!isMachineTypeValid) {
            tools.showItem(dom.addFormMachineTypeComboError);
        }
    }

    function handleAddFormMainMachineNameValueChanged() {

        tools.hideItem(dom.addFormMainMachineNameError);
        const machineName = dom.addFormMainMachineName.GetValue();
        const isMachineNameValid = !tools.isNullOrEmpty(machineName);
        if (!isMachineNameValid) {
            tools.showItem(dom.addFormMainMachineNameError);
        }
    }

    function isAddFormValid() {
        var isValid = true;
        tools.hideItem(dom.addFormMainMachineNameError);
        var machineName = dom.addFormMainMachineName.GetValue();
        var isMachineNameValid = !tools.isNullOrEmpty(machineName);
        if (!isMachineNameValid) {
            isValid = false;
            tools.showItem(dom.addFormMainMachineNameError);
        }

        tools.hideItem(dom.addFormMachineOperationError);
        var machineOperation = dom.addFormMachineOperation.GetText();
        var isMachineOperationValid = !tools.isNullOrEmpty(machineOperation);
        if (!isMachineOperationValid) {
            isValid = false;
            tools.showItem(dom.addFormMachineOperationError);
        }

        tools.hideItem(dom.addFormIsToolsError);
        var isTools = dom.addFormIsTools.GetValue();
        var isToolsHasValue = !tools.isNullOrEmpty(isTools);
        if (!isToolsHasValue) {
            tools.showItem(dom.addFormIsToolsError);
            isValid = false;
        }
        var isToolsType = dom.addFormIsTools.GetSelectedIndex();
        if (!isToolsType) {
            tools.hideItem(dom.addFormMachineOldMachineCodeIsToolsError);
            var isOldMachineCodeIsToolsValid = dom.addFormMachineOldMachineCodeIsTools.isValid;
            if (!isOldMachineCodeIsToolsValid) {
                isValid = false;
                tools.showItem(dom.addFormMachineOldMachineCodeIsToolsError);
            }
        } else {
            tools.hideItem(dom.addFormMachineOldMachineCodeError);
            var isOldMachineCodeValid = dom.addFormMachineOldMachineCode.isValid;
            if (!isOldMachineCodeValid) {
                isValid = false;
                tools.showItem(dom.addFormMachineOldMachineCodeError);
            }
        }

        tools.hideItem(dom.addFormMachineLevelComboError);
        var machineLevelList = dom.addFormMachineLevelCombo.GetValue();
        var isMachineLevelListHasValue = !tools.isNullOrEmpty(machineLevelList);
        if (!isMachineLevelListHasValue) {
            tools.showItem(dom.addFormMachineLevelComboError);
            isValid = false;
        }

        tools.hideItem(dom.addFormMachineControlComboError);
        var machineControlCombo = dom.addFormMachineControlCombo.GetValue();
        var isMachineControlComboHasValue = !tools.isNullOrEmpty(machineControlCombo);
        if (!isMachineControlComboHasValue) {
            tools.showItem(dom.addFormMachineControlComboError);
            isValid = false;
        }

        tools.hideItem(dom.addFormMachineTypeComboError);
        var machineTypeCombo = dom.addFormMachineTypeCombo.GetValue();
        var isMachineTypeComboHasValue = !tools.isNullOrEmpty(machineTypeCombo);
        if (!isMachineTypeComboHasValue) {
            tools.showItem(dom.addFormMachineTypeComboError);
            isValid = false;
        }

        return isValid;
    }

    function resetForm() {
        dom.addFormMainMachineName.SetValue('');
        dom.addFormMainMachineSerial.SetValue('');
        dom.addFormMainMachineModel.SetValue('');
        dom.addFormMachineControlSystemName.SetValue('');
        dom.addFormMachineControlSystemModelName.SetValue('');
        dom.addFormMachineOperation.SetValue('');
        dom.addFormMachineOldMachineCode.SetValue('');
        dom.addFormMachineOldMachineCodeIsTools.SetValue('');
        dom.addFormMachineLevelCombo.SetSelectedIndex(-1);
        dom.addFormMachineControlCombo.SetSelectedIndex(-1);
        dom.addFormMachineTypeCombo.SetSelectedIndex(-1);

    }

    function setDom() {
        dom.addFormMainMachineNameParent = $("#addFormMainMachineNameParent");
        dom.addFormMainMachineName = ASPxClientTextBox.Cast("addFormMainMachineName");
        dom.addFormMainMachineNameError = $("#addFormMainMachineNameError");

        dom.addFormMainMachineSerialParent = $("#addFormMainMachineSerialParent");
        dom.addFormMainMachineSerial = ASPxClientTextBox.Cast("addFormMainMachineSerial");

        dom.addFormMachineLevelComboParent = $("#addFormMachineLevelComboParent");
        dom.addFormMachineLevelCombo =
            ASPxClientComboBox.Cast("addFormMachineLevelCombo");
        dom.addFormMachineLevelComboError = $("#addFormMachineLevelComboError");

        dom.addFormMachineTypeComboParent = $("#addFormMachineTypeComboParent");
        dom.addFormMachineTypeCombo =
            ASPxClientComboBox.Cast("addFormMachineTypeCombo");
        dom.addFormMachineTypeComboError = $("#addFormMachineTypeComboError");

        dom.addFormMainMachineModelParent = $("#addFormMainMachineModelParent");
        dom.addFormMainMachineModel = ASPxClientTextBox.Cast("addFormMainMachineModel");

        dom.addFormMachineControlComboParent = $("#addFormMachineControlComboParent");
        dom.addFormMachineControlCombo =
            ASPxClientComboBox.Cast("addFormMachineControlCombo");
        dom.addFormMachineControlComboError = $("#addFormMachineControlComboError");

        dom.addFormMachineControlSystemName = ASPxClientTextBox.Cast("addFormMachineControlSystemName");

        dom.addFormMachineControlSystemModelName = ASPxClientTextBox.Cast("addFormMachineControlSystemModelName");

        dom.addFormMachineOperationParent = $("#addFormMachineOperationParent");
        dom.addFormMachineOperation = ASPxClientTextBox.Cast("addFormMachineOperation");
        dom.addFormMachineOperationError = $("#addFormMachineOperationError");

        dom.addFormMachineOldMachineCodeIsTools = ASPxClientTextBox.Cast("addFormMachineOldMachineCodeIsTools");
        dom.addFormMachineOldMachineCodeIsToolsError = $("#addFormMachineOldMachineCodeIsToolsError");
        dom.addFormMachineOldMachineCodeIsToolsDesign = $("#addFormMachineOldMachineCodeIsToolsDesign");

        dom.addFormMachineOldMachineCode = ASPxClientTextBox.Cast("addFormMachineOldMachineCode");
        dom.addFormMachineOldMachineCodeError = $("#addFormMachineOldMachineCodeError");
        dom.addFormMachineOldMachineCodeDesign = $("#addFormMachineOldMachineCodeDesign");

        dom.addFormIsTools = ASPxClientRadioButtonList.Cast("addFormIsTools");
        dom.addFormIsToolsError = $("#addFormIsToolsError");
    }

    $(document).ready(setDom);

    return {
        addNewMainMachine: addNewMainMachine,
        handleAddFormIsToolsSelectedIndexChanged: handleAddFormIsToolsSelectedIndexChanged,
        handleAddFormMachineControlComboSelectedIndexChanged: handleAddFormMachineControlComboSelectedIndexChanged,
        handleAddFormMachineLevelComboSelectedIndexChanged: handleAddFormMachineLevelComboSelectedIndexChanged,
        handleAddFormMachineOldMachineCodeValueChanged: handleAddFormMachineOldMachineCodeValueChanged,
        handleAddFormMachineOldMachineCodeIsToolsValueChanged: handleAddFormMachineOldMachineCodeIsToolsValueChanged,
        handleAddFormMachineOperationValueChanged: handleAddFormMachineOperationValueChanged,
        handleAddFormMachineTypeComboSelectedIndexChanged: handleAddFormMachineTypeComboSelectedIndexChanged,
        handleAddFormMainMachineNameValueChanged: handleAddFormMainMachineNameValueChanged,
        dom
    };

})();
