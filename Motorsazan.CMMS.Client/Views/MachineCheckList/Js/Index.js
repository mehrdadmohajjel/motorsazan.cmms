/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.machineCheckList = (function() {

    var dom = {};
    var textSeparator = ";";

    var state = {
        departmentId: null,
        subDepartmentId: null,
        nodeKey: null,

        copySeveralOperationItemDepartmentId: null,
        copySeveralOperationItemSubDepartmentId: null,
        copySeveralOperationItemNodeKey: null,

        MoveOperationItemDepartmentId: null,
        MoveOperationItemSubDepartmentId: null,
        MoveOperationItemNodeKey: null,

        editOperationItemId: null,
        editMaintenanceGroupName: null,
        editMaintenanceGroupId: null,
        editOperationItemCode: null,
        editOperationItemName: null,

        operationTypeItemId: 3,
        maintenanceGroupCode: null,
        subMachineCode: null,
        serialForOperationItem: null,
        gridSelectedIds: null,
        operationItemId: null
    };

    var tools = motorsazanClient.tools;
    var controllerName = "/MachineCheckList/";


    function addSparePartToOperationItemModalSetDom() {

        dom.itemCheckComboBox = ASPxClientDropDownEdit.Cast("itemCheckComboBox");
        dom.addSparePartToOperationItemListByMachineIdCheckComboBox = ASPxClientListBox.Cast("addSparePartToOperationItemListByMachineIdCheckComboBox");
        dom.addSparePartCheckComboParent = $("#addSparePartCheckComboParent");
        dom.addSparePartCheckComboError = $("#addSparePartCheckComboError");
        dom.addSparePartCheckComboBoxDesign = $("#addSparePartCheckComboBoxDesign");

        //Grid
        dom.addSparePartToOperationItemGridParent = $("#addSparePartToOperationItemGridParent");
        dom.addSparePartToOperationItemGrid = ASPxClientGridView.Cast("addSparePartToOperationItemGrid");
    }

    function handleAddSparePartToOperationBtnModalBtnClick() {
        setDom();
        dom.machineCheckListGrid.GetRowValues(dom.machineCheckListGrid.GetFocusedRowIndex(),
            "OperationItemId",
            (values) => {
                setDom();

                const url = controllerName + "AddSparepartToOperation";

                state.operationItemId = values;
                const machineId = state.nodeKey;

                const apiParam = {
                    operationItemId: values,
                    machineId: machineId
                };

                const title = "تخصیص قطعه";
                motorsazanClient.contentModal.ajaxShow(title,
                    url,
                    apiParam,
                    initAddSparePartToOperationItemModal,
                    true,
                    false);
            });
    }

    function handleAddSparePartToOperationCheckComboBoxSelectedIndexChange() {
        addSparePartToOperationItemModalSetDom();
        var selectedItems = dom.addSparePartToOperationItemListByMachineIdCheckComboBox.GetSelectedItems();
        dom.itemCheckComboBox.SetText(getSelectedItemsText(selectedItems));

        tools.hideItem(dom.addSparePartCheckComboError);

        const sparePartItem = dom.addSparePartToOperationItemListByMachineIdCheckComboBox.GetSelectedValues();
        const isSparePartItemItemSelected = !tools.isNullOrEmpty(sparePartItem);
        if (!isSparePartItemItemSelected) {
            tools.showItem(dom.addSparePartCheckComboError);
        }

    }
    function getSelectedItemsText(items) {
        var texts = [];
        for (var i = 0; i < items.length; i++)
            texts.push(items[i].text);
        return texts.join(textSeparator);
    }

    function initAddSparePartToOperationItemModal() {
        addSparePartToOperationItemModalSetDom();

    }

    function isAddSparePartItemOperationValid() {
        var isValid = true;

        tools.hideItem(dom.addSparePartCheckComboError);

        const sparePartOperationItem = dom.addSparePartToOperationItemListByMachineIdCheckComboBox.GetSelectedValues();
        const isSparePartOperationItemItemSelected = sparePartOperationItem.length > 0;
        if (!isSparePartOperationItemItemSelected) {
            isValid = false;
            tools.showItem(dom.addSparePartCheckComboError);
        }

        return isValid;
    }

    function addSparePartToOperationItemClick() {
        const canAddSparePartToItem = isAddSparePartItemOperationValid();
        if (!canAddSparePartToItem) return false;
        const url = controllerName + "AddSparePartToItem";

        const stockItemIdList = dom.addSparePartToOperationItemListByMachineIdCheckComboBox.GetSelectedValues();
        const operationItemId = state.operationItemId;

        const apiParam = {
            operationItemId: operationItemId,
            StockList: stockItemIdList.map(stockId => { return { stockId }; })
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (apiResult) {
                motorsazanClient.messageModal.success(apiResult);

                addSparePartToOperationItemModalSetDom();

                refreshSparePartItemGrid();

                resetAddSparePartOperationItemModal();
            });
        return true;
    }

    function resetAddSparePartOperationItemModal() {
        dom.addSparePartToOperationItemListByMachineIdCheckComboBox.SetSelectedIndex(-1);
        tools.hideItem(dom.addSparePartCheckComboError);
    }

    function handleAddSparePartToOperationItemGridCallback(command) {
        const operationItemId = state.operationItemId;

        const url = controllerName +
            "GetSparePartToOperationItemGrid" +
            "?operationItemId=" +
            operationItemId;

        command.callbackUrl = url;
    }

    function handleAddSparePartToOperationItemGridCustomBtnClick(source, event) {
        const customBtnId = event.buttonID;
        if (customBtnId === "sparePartItemGridCustomBtn") return removeSparePartOperationItemRow();
        return null;
    }
    function removeSparePartOperationItemRow() {
        addSparePartToOperationItemModalSetDom();
        dom.addSparePartToOperationItemGrid.GetRowValues(dom.addSparePartToOperationItemGrid.GetFocusedRowIndex(), "OperationItemSparePartId", (values) => {
            motorsazanClient.messageModal.confirm("آیا از حذف این ردیف مطمئن هستید؟", () => confirmRemoveSparePartOperationItem(values), "تاییدیه حذف");
        });
    }

    async function confirmRemoveSparePartOperationItem(values) {
        const url = controllerName + "RemoveSparePartFromOperationItem";
        const params = {
            OperationItemSparePartId: values
        };

        const result = await motorsazanClient.connector.post(url, params);

        await refreshSparePartItemGrid();
        motorsazanClient.messageModal.success(result);
    }

    async function refreshSparePartItemGrid() {
        const url = "/MachineCheckList/GetSparePartToOperationItemGrid";

        const apiParam = {
            operationItemId: state.operationItemId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.addSparePartToOperationItemGridParent.html(gridHtml);

        addSparePartToOperationItemModalSetDom();
    }

    function addCheckList() {
        setDom();
        const canAdd = isAddFormValid();
        if (!canAdd) return false;

        const url = controllerName + "AddCheckListOperationItem";

        const nodeSelect = dom.addFormSubMachineCheckListTree.GetFocusedNodeKey();;
        const checkListTitle = dom.addCheckListTitle.GetValue();
        const finalCheckListCode = state.operationTypeItemId +
            state.maintenanceGroupCode +
            state.subMachineCode +
            state.serialForOperationItem;
        const maintenanceGroupId = dom.addFormMaintenanceGroupListCombo.GetValue();

        const apiParam = {
            operationItemName: checkListTitle,
            operationItemCode: finalCheckListCode,
            machineId: nodeSelect,
            maintenanceGroupId: maintenanceGroupId
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function(apiResult) {
                refreshGrid();
                motorsazanClient.messageModal.success(apiResult);

                setDom();
                resetAddForm();
            });
        return true;
    }

    async function confirmActiveOperationItem(values) {
        const url = controllerName + "ActiveOrDeActiveOperationItem";
        const params = {
            OperationItemId: values,
            DeActiveReason: null
        };

        const result = await motorsazanClient.connector.post(url, params);

        await refreshGrid();
        motorsazanClient.messageModal.success(result);
    }


    async function confirmDeActiveOperationItem(values) {
        const url = controllerName + "ActiveOrDeActiveOperationItem";

        const deActiveReason = dom.deActiveOperationItemReasonTextBox.GetText();

        const params = {
            OperationItemId: values,
            DeActiveReason: deActiveReason
        };

        const result = await motorsazanClient.connector.post(url, params);

        await refreshGrid();
        motorsazanClient.messageModal.success(result);

        deActiveOperationItemModalSetDom();

        initSerialForOperationLabelText();
        motorsazanClient.contentModal.hide();
    }

    function copyOneOperationItemToOtherMachineBtnClick() {
        copySeveralOperationItemToOtherMachineModalSetDom();

        const isValid = isCopySeveralOperationItemToOtherMachineValid();
        if (!isValid) return false;

        const url = controllerName + "CopyOperationItemForOtherMachine";

        const nodeSelect = dom.copySeveralOperationItemSubMachineTreeListModal.GetFocusedNodeKey();

        const apiParam = {
            OperationItemId: state.operationItemId,
            machineId: nodeSelect
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function(apiResult) {
                refreshGrid();
                motorsazanClient.messageModal.success(apiResult);

                copySeveralOperationItemToOtherMachineModalSetDom();
                resetCopySeveralOperationItemForm();

                initSerialForOperationLabelText();
                motorsazanClient.contentModal.hide();
            });
        return true;
    }

    function copySeveralOperationItemToOtherMachineBtnClick() {
        copySeveralOperationItemToOtherMachineModalSetDom();

        const isValid = isCopySeveralOperationItemToOtherMachineValid();
        if (!isValid) return false;

        const url = controllerName + "CopySeveralOperationItemToOtherMachine";

        const nodeSelect = dom.copySeveralOperationItemSubMachineTreeListModal.GetFocusedNodeKey();

        const operationItemIdList = state.gridSelectedIds;

        const apiParam = {
            machineId: nodeSelect,
            operationItemIdList: operationItemIdList.map(operationItemId => { return { operationItemId }; })

        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function(apiResult) {
                refreshGrid();
                motorsazanClient.messageModal.success(apiResult);

                copySeveralOperationItemToOtherMachineModalSetDom();
                resetCopySeveralOperationItemForm();

                initSerialForOperationLabelText();

                motorsazanClient.contentModal.hide();
            });
        return true;
    }

    function copySeveralOperationItemToOtherMachineModalSetDom() {
        dom.copySeveralOperationItemDepartmentListModal =
            ASPxClientComboBox.Cast("copySeveralOperationItemDepartmentListModal");
        dom.copySeveralOperationItemDepartmentListModalError = $("#copySeveralOperationItemDepartmentListModalError");
        dom.copySeveralOperationItemDepartmentListModalParent = $("#copySeveralOperationItemDepartmentListModalParent");

        dom.copySeveralOperationItemSubDepartmentListModal =
            ASPxClientComboBox.Cast("copySeveralOperationItemSubDepartmentListModal");
        dom.copySeveralOperationItemSubDepartmentListModalError =
            $("#copySeveralOperationItemSubDepartmentListModalError");
        dom.copySeveralOperationItemSubDepartmentListModalParent =
            $("#copySeveralOperationItemSubDepartmentListModalParent");

        dom.copySeveralOperationItemSubMachineTreeListModal =
            ASPxClientTreeList.Cast("copySeveralOperationItemSubMachineTreeListModal");
        dom.copySeveralOperationItemSubMachineTreeListModalError =
            $("#copySeveralOperationItemSubMachineTreeListModalError");
        dom.copySeveralOperationItemSubMachineTreeListModalParent =
            $("#copySeveralOperationItemSubMachineTreeListModalParent");
    }

    function deActiveOperationItemModalSetDom() {
        dom.deActiveOperationItemReasonTextBox = ASPxClientTextBox.Cast("deActiveOperationItemReasonTextBox");
        dom.deActiveOperationItemReasonTextBoxError = $("#deActiveOperationItemReasonTextBoxError");
        dom.deActiveOperationItemReasonTextBoxParent = $("#deActiveOperationItemReasonTextBoxParent");
    }

    function editOperationItemModalSetDom() {

        dom.editFormMaintenanceGroupTextBox = ASPxClientTextBox.Cast("editFormMaintenanceGroupTextBox");
        dom.editFormMaintenanceGroupTextBoxParent = $("#editFormMaintenanceGroupTextBoxParent");

        dom.editFormOperationItemCodeTextBox = ASPxClientTextBox.Cast("editFormOperationItemCodeTextBox");
        dom.editFormOperationItemCodeTextBoxParent = $("#editFormOperationItemCodeTextBoxParent");

        dom.editFormOperationItemNameTextBox = ASPxClientTextBox.Cast("editFormOperationItemNameTextBox");
        dom.editFormOperationItemNameTextBoxError = $("#editFormOperationItemNameTextBoxError");
        dom.editFormOperationItemNameTextBoxParent = $("#editFormOperationItemNameTextBoxParent");
    }

    async function getCopySeveralOperationItemSubMachineTreeListModalSelectedNode() {
        state.copySeveralOperationItemNodeKey = dom.copySeveralOperationItemSubMachineTreeListModal.GetFocusedNodeKey();
        tools.hideItem(dom.copySeveralOperationItemSubMachineTreeListModalError);
    }

    async function getMoveOperationItemSubMachineTreeListModalSelectedNode() {
        state.MoveOperationItemNodeKey = dom.moveOperationItemSubMachineTreeListModal.GetFocusedNodeKey();
        tools.hideItem(dom.moveOperationItemSubMachineTreeListModalError);
    }

    async function getSubMachineCheckListTreeSelectedNode() {
        state.nodeKey = dom.addFormSubMachineCheckListTree.GetFocusedNodeKey();
        tools.hideItem(dom.addFormSubMachineCheckListTreeError);

        const url = "/MachineCheckList/MachineCheckListGrid";

        const machineId = state.nodeKey || 0;
        const operationItemTypeId = 3;

        const apiParam = {
            operationItemTypeId: operationItemTypeId,
            machineId: machineId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.machineCheckListGridParent.html(gridHtml);

        setDom();
        dom.gridToolbarChangeCopySeveralOperationButton.SetEnabled(false);
    }

    function handleActiveOperationItemBtnClick() {
        setDom();
        dom.machineCheckListGrid.GetRowValues(dom.machineCheckListGrid.GetFocusedRowIndex(),
            "OperationItemId",
            (values) => {
                motorsazanClient.messageModal.confirm("آیا از فعال کردن این آیتم چک لیست مطمئن هستید؟",
                    () => confirmActiveOperationItem(values),
                    "تاییدیه فعال کردن");
            });
    }

    function handleAddCheckListTitleValueChanged() {
        tools.hideItem(dom.addCheckListTitleError);
        const checkListTitle = dom.addCheckListTitle.GetValue();
        const isCheckListTitleValid = !tools.isNullOrEmpty(checkListTitle);
        if (!isCheckListTitleValid) {
            tools.showItem(dom.addCheckListTitleError);
        }
    }

    async function handleAddFormDepartmentComboSelectedIndexChange() {
        tools.hideItem(dom.addFormDepartmentComboError);

        const url = controllerName + "AddMachineOperationSubDepartmentList";

        state.departmentId = dom.addFormDepartmentCombo.GetValue();

        const apiParam = {
            DepartmentId: state.departmentId
        };

        const subDepartmentList = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormSubDepartmentComboParent.html(subDepartmentList);

        initMachineTree();

        setDom();
    }

    function handleAddFormMachineTreeBeginCallback(command) {
        command.callbackUrl =
            controllerName + "AddSubMachineCheckListTreeList" + "?DepartmentId=" + state.subDepartmentId;
    }

    function handleAddFormMaintenanceGroupListCombo() {
        tools.hideItem(dom.addFormMaintenanceGroupListComboError);

        const maintenanceGroupList = dom.addFormMaintenanceGroupListCombo.GetValue();
        const isMaintenanceGroupListValid = !tools.isNullOrEmpty(maintenanceGroupList);
        if (!isMaintenanceGroupListValid) {
            tools.showItem(dom.addFormMaintenanceGroupListComboError);

        } else {
            const formattedNumber =
                maintenanceGroupList.toLocaleString("en-US", { minimumIntegerDigits: 2, useGrouping: false });
            dom.addFormMaintenanceGroupCodeLabel.SetText(formattedNumber);
            state.maintenanceGroupCode = formattedNumber;
        }
    }

    async function handleAddFormSubDepartmentComboSelectedIndexChange() {
        tools.hideItem(dom.addFormSubDepartmentComboError);

        const url = controllerName + "addSubMachineCheckListTreeList";

        state.subDepartmentId = dom.addFormSubDepartmentCombo.GetValue();
        const apiParam = {
            DepartmentId: state.subDepartmentId
        };

        const treeList = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormSubMachineCheckListTreeParent.html(treeList);

        setDom();
    }

    function handleAddFormSubMachineCodeValueChanged() {
        tools.hideItem(dom.addFormSubMachineCodeError);
        const isSubMachineCodeValid = dom.addFormSubMachineCode.GetIsValid();
        if (!isSubMachineCodeValid) {
            tools.showItem(dom.addFormSubMachineCodeError);
        } else {
            const formattedNumber = dom.addFormSubMachineCode.GetValue()
                .toLocaleString("en-US", { minimumIntegerDigits: 2, useGrouping: false });

            dom.addFormSubMachineCodeLabel.SetText(formattedNumber);
            state.subMachineCode = formattedNumber;
        }
    }

    function handleCopyOneOperationItemToOtherMachineModalBtnClick() {
        setDom();
        dom.machineCheckListGrid.GetRowValues(dom.machineCheckListGrid.GetFocusedRowIndex(),
            "OperationItemId",
            (values) => {
                setDom();
                const nodeSelect = dom.addFormSubMachineCheckListTree.GetFocusedNodeKey();
                state.operationItemId = values;

                const url = controllerName + "CopyOneOperationItemToOtherMachine";
                const params = {
                    OperationItemId: values,
                    CurrentMachineId: nodeSelect
                };
                const title = "کپی به دستگاه دیگر";
                motorsazanClient.contentModal.ajaxShow(title,
                    url,
                    params,
                    initChangeCopySeveralOperationItemForOtherMachineModal,
                    true,
                    false);
            });
    }

    async function handleCopySeveralOperationItemDepartmentListModalSelectedIndexChange() {
        tools.hideItem(dom.copySeveralOperationItemDepartmentListModalError);

        const url = controllerName + "CopySeveralOperationItemToOtherMachineModalSubDepartmentList";

        state.copySeveralOperationItemDepartmentId = dom.copySeveralOperationItemDepartmentListModal.GetValue();

        const apiParam = {
            DepartmentId: state.copySeveralOperationItemDepartmentId
        };

        const copySeveralOperationItemSubDepartmentList = await motorsazanClient.connector.post(url, apiParam);
        dom.copySeveralOperationItemSubDepartmentListModalParent.html(copySeveralOperationItemSubDepartmentList);

        initCopySeveralOperationItemToOtherMachineModalTree();

        copySeveralOperationItemToOtherMachineModalSetDom();
    }

    async function handleCopySeveralOperationItemSubDepartmentListModalSelectedIndexChange() {
        tools.hideItem(dom.copySeveralOperationItemSubDepartmentListModalError);

        const url = controllerName + "CopySeveralOperationItemToOtherMachineModalTreeList";

        state.copySeveralOperationItemSubDepartmentId = dom.copySeveralOperationItemSubDepartmentListModal.GetValue();
        const apiParam = {
            DepartmentId: state.copySeveralOperationItemSubDepartmentId
        };

        const treeList = await motorsazanClient.connector.post(url, apiParam);
        dom.copySeveralOperationItemSubMachineTreeListModalParent.html(treeList);

        copySeveralOperationItemToOtherMachineModalSetDom();
    }

    function handleCopySeveralOperationItemSubMachineTreeListModalBeginCallback(command) {
        command.callbackUrl =
            controllerName +
            "CopySeveralOperationItemToOtherMachineModalTreeList" +
            "?DepartmentId=" +
            state.copySeveralOperationItemSubDepartmentId;
    }

    function handleDeActiveOperationItemModalBtnClick() {
        setDom();
        dom.machineCheckListGrid.GetRowValues(dom.machineCheckListGrid.GetFocusedRowIndex(),
            "OperationItemId",
            (values) => {
                motorsazanClient.messageModal.confirm("آیا از غیرفعال کردن این آیتم چک لیست مطمئن هستید؟",
                    () => confirmDeActiveOperationItem(values),
                    "تاییدیه غیرفعال کردن");
            });
    }

    function handleDeActiveOperationItemBtnClick() {
        setDom();
        dom.machineCheckListGrid.GetRowValues(dom.machineCheckListGrid.GetFocusedRowIndex(),
            "OperationItemId",
            (values) => {
                setDom();

                state.operationItemId = values;

                const url = controllerName + "DeActiveOperationItem";

                const title = "غیرفعال کردن کد چک لیست";
                motorsazanClient.contentModal.ajaxShow(title, url, null, initDeActiveOperationItemModal);
            });
    }

    function handleDeActiveOperationItemReasonTextBoxValueChanged() {
        tools.hideItem(dom.deActiveOperationItemReasonTextBoxError);
        const deActiveOperationItemReason = dom.deActiveOperationItemReasonTextBox.GetText();
        const isDeActiveOperationItemReasonValid = !tools.isNullOrEmpty(deActiveOperationItemReason);
        if (!isDeActiveOperationItemReasonValid) {
            tools.showItem(dom.deActiveOperationItemReasonTextBoxError);
        }
    }

    function handleEditFormBtnClick() {
        editOperationItemModalSetDom();

        const isValid = isEditOperationItemModalValid();
        if (!isValid) return false;

        const url = controllerName + "EditOperationItemByOperationItemId";

        const operationItemName = dom.editFormOperationItemNameTextBox.GetText();

        const apiParam = {
            OperationItemId: state.operationItemId,
            MaintenanceGroupId: state.editMaintenanceGroupId,
            OperationItemCode: state.editOperationItemCode,
            OperationItemName: operationItemName
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function(apiResult) {
                refreshGrid();
                motorsazanClient.messageModal.success(apiResult);

                editOperationItemModalSetDom();

                initSerialForOperationLabelText();
                motorsazanClient.contentModal.hide();
            });
        return true;
    }

    function isCopySeveralOperationItemToOtherMachineValid() {
        var isValid = true;

        tools.hideItem(dom.copySeveralOperationItemDepartmentListModalError);
        const department = dom.copySeveralOperationItemDepartmentListModal.GetValue();
        const isDepartmentValid = !tools.isNullOrEmpty(department);
        if (!isDepartmentValid) {
            isValid = false;
            tools.showItem(dom.copySeveralOperationItemDepartmentListModalError);
        }

        tools.hideItem(dom.copySeveralOperationItemSubDepartmentListModalError);
        const subDepartment = dom.copySeveralOperationItemSubDepartmentListModal.GetValue();
        const isSubDepartmentValid = !tools.isNullOrEmpty(subDepartment);
        if (!isSubDepartmentValid) {
            isValid = false;
            tools.showItem(dom.copySeveralOperationItemSubDepartmentListModalError);
        }

        tools.hideItem(dom.copySeveralOperationItemSubMachineTreeListModalError);
        const nodeSelect = dom.copySeveralOperationItemSubMachineTreeListModal.GetFocusedNodeKey();;
        const isNodeSelectValid = !tools.isNullOrEmpty(nodeSelect);
        if (!isNodeSelectValid) {
            isValid = false;
            tools.showItem(dom.copySeveralOperationItemSubMachineTreeListModalError);
        }

        return isValid;
    }

    function handleEditFormOperationItemNameTextBoxValueChanged() {
        tools.hideItem(dom.editFormOperationItemNameTextBoxError);
        const operationItemName = dom.editFormOperationItemNameTextBox.GetText();
        const isOperationItemNameValid = !tools.isNullOrEmpty(operationItemName);
        if (!isOperationItemNameValid) {
            tools.showItem(dom.editFormOperationItemNameTextBoxError);
        }
    }

    function handleEditOperationItemBtnClick() {
        setDom();
        dom.machineCheckListGrid.GetRowValues(dom.machineCheckListGrid.GetFocusedRowIndex(),
            "OperationItemId;MaintenanceGroupName;MaintenanceGroupId;OperationItemCode;OperationItemName",
            (values) => {
                setDom();

                const url = controllerName + "EditOperationItemModal";
                const [
                    operationItemId, maintenanceGroupName, maintenanceGroupId, operationItemCode, operationItemName
                ] = values;

                state.editOperationItemId = operationItemId;
                state.editMaintenanceGroupName = maintenanceGroupName;
                state.editMaintenanceGroupId = maintenanceGroupId;
                state.editOperationItemCode = operationItemCode;
                state.editOperationItemName = operationItemName;

                const title = "ویرایش کد";
                motorsazanClient.contentModal.ajaxShow(title, url, null, initEditOperationItemModal);
            });
    }

    function handleGridSelectionChanged(source) {
        source.GetSelectedFieldValues("OperationItemId",
            (values) => {
                state.gridSelectedIds = values;

                if (values.length < 1) {
                    dom.gridToolbarChangeCopySeveralOperationButton.SetEnabled(false);
                } else {
                    dom.gridToolbarChangeCopySeveralOperationButton.SetEnabled(true);
                }
            });
    }

    function handleGridToolbarChangeCopySeveralOperationItemForOtherMachineButtonClick(s, e) {
        if (e.item.name === "gridToolbarChangeCopySeveralOperationButton") {
            const title = "کپی کدهای چک لیست انتخابی";

            const url = controllerName + "CopySeveralOperationItemModal";

            motorsazanClient.contentModal.ajaxShow(title,
                url,
                null,
                initChangeCopySeveralOperationItemForOtherMachineModal,
                true,
                false);
        }
    }

    function handleMachinesCheckListItemGridBeginCallback(command) {
        const machineId = state.nodeKey || 0;
        const operationItemTypeId = 3;

        const url = controllerName +
            "MachineCheckListGrid" +
            "?operationItemTypeId=" +
            operationItemTypeId +
            "&machineId=" +
            machineId;
        command.callbackUrl = url;
    }

    async function handleMoveOperationItemDepartmentListModalSelectedIndexChange() {
        tools.hideItem(dom.moveOperationItemDepartmentListModalError);

        const url = controllerName + "MoveOperationItemToOtherMachineModalSubDepartmentList";

        state.MoveOperationItemDepartmentId = dom.moveOperationItemDepartmentListModal.GetValue();

        const apiParam = {
            DepartmentId: state.MoveOperationItemDepartmentId
        };

        const moveOperationItemSubDepartmentList = await motorsazanClient.connector.post(url, apiParam);
        dom.moveOperationItemSubDepartmentListModalParent.html(moveOperationItemSubDepartmentList);

        initMoveOperationItemMachineTree();

        moveOperationItemToOtherMachineSetDom();
    }

    function init() {
        setDom();
        setEvent();

        initMachineTree();
    }

    function handleMoveOperationItemFromCurrentMachineToNewMachineBtnClick() {
        moveOperationItemToOtherMachineSetDom();

        const canMoveOperationItem = isMoveOperationItemFromValid();
        if (!canMoveOperationItem) return false;

        const url = controllerName + "MoveOperationItemFromCurrentMachineToNewMachine";

        const newMachineId = dom.moveOperationItemSubMachineTreeListModal.GetFocusedNodeKey();

        const apiParam = {
            operationItemId: state.operationItemId,
            newMachineId: newMachineId
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function(apiResult) {
                refreshGrid();

                motorsazanClient.messageModal.success(apiResult);

                moveOperationItemToOtherMachineSetDom();
                resetMoveOperationItemForm();

                initSerialForOperationLabelText();

                motorsazanClient.contentModal.hide();

            });
        return true;
    }

    async function handleMoveOperationItemSubDepartmentListModalSelectedIndexChange() {
        tools.hideItem(dom.moveOperationItemSubDepartmentListModalError);

        const url = controllerName + "MoveOperationItemToOtherMachineModalTreeList";

        state.MoveOperationItemSubDepartmentId = dom.moveOperationItemSubDepartmentListModal.GetValue();
        const apiParam = {
            DepartmentId: state.MoveOperationItemSubDepartmentId
        };

        const treeList = await motorsazanClient.connector.post(url, apiParam);
        dom.moveOperationItemSubMachineTreeListModalParent.html(treeList);

        moveOperationItemToOtherMachineSetDom();
    }

    function handleMoveOperationItemSubMachineTreeListModalBeginCallback(command) {
        command.callbackUrl =
            controllerName +
            "MoveOperationItemToOtherMachineModalTreeList" +
            "?DepartmentId=" +
            state.MoveOperationItemSubDepartmentId;
    }

    function handleMoveOperationItemToOtherMachineModalBtnClick() {
        setDom();
        dom.machineCheckListGrid.GetRowValues(dom.machineCheckListGrid.GetFocusedRowIndex(),
            "OperationItemId",
            (values) => {
                setDom();
                state.operationItemId = values;

                const url = controllerName + "MoveOperationItemToOtherMachine";
                const params = {
                    OperationItemId: values
                };
                const title = "انتقال به دستگاه دیگر";
                motorsazanClient.contentModal.ajaxShow(title,
                    url,
                    params,
                    moveOperationItemToOtherMachineInit,
                    true,
                    false);
            });
    }

    function initChangeCopySeveralOperationItemForOtherMachineModal() {
        copySeveralOperationItemToOtherMachineModalSetDom();

        initCopySeveralOperationItemToOtherMachineModalTree();
    }

    async function initCopySeveralOperationItemToOtherMachineModalTree() {
        const urlTree = controllerName + "CopySeveralOperationItemToOtherMachineModalTreeList";
        const departmentMachineTree = await motorsazanClient.connector.post(urlTree, {});

        dom.copySeveralOperationItemSubMachineTreeListModalParent.html(departmentMachineTree);

        copySeveralOperationItemToOtherMachineModalSetDom();
    }

    function initEditOperationItemModal() {
        editOperationItemModalSetDom();

        dom.editFormMaintenanceGroupTextBox.SetValue(state.editMaintenanceGroupName);
        dom.editFormOperationItemCodeTextBox.SetText(state.editOperationItemCode);
        dom.editFormOperationItemNameTextBox.SetText(state.editOperationItemName);
    }


    function initDeActiveOperationItemModal() {
        deActiveOperationItemModalSetDom();
    }

    async function initMachineTree() {
        const urlTree = controllerName + "AddSubMachineCheckListTreeList";
        const departmentMachineTree = await motorsazanClient.connector.post(urlTree, {});

        dom.addFormSubMachineCheckListTreeParent.html(departmentMachineTree);

        setDom();
    }

    async function initMoveOperationItemMachineTree() {
        const urlTree = controllerName + "MoveOperationItemToOtherMachineModalTreeList";

        const machineTree = await motorsazanClient.connector.post(urlTree, {});

        dom.moveOperationItemSubMachineTreeListModalParent.html(machineTree);
        moveOperationItemToOtherMachineSetDom();
    }

    async function initSerialForOperationLabelText() {
        const url = controllerName + "AddSerialForOperationItem";

        setDom();

        const serialForOperationItem = await motorsazanClient.connector.post(url, {});
        dom.addFormSerialForOperationLabelParent.html(serialForOperationItem);

        setDom();
        state.serialForOperationItem = dom.addFormSerialForOperationLabel.GetText();
    }

    function isAddFormValid() {
        var isValid = true;

        tools.hideItem(dom.addFormDepartmentComboError);
        const department = dom.addFormDepartmentCombo.GetValue();
        const isDepartmentValid = !tools.isNullOrEmpty(department);
        if (!isDepartmentValid) {
            isValid = false;
            tools.showItem(dom.addFormDepartmentComboError);
        }

        tools.hideItem(dom.addFormSubDepartmentComboError);
        const subDepartment = dom.addFormSubDepartmentCombo.GetValue();
        const isSubDepartmentValid = !tools.isNullOrEmpty(subDepartment);
        if (!isSubDepartmentValid) {
            isValid = false;
            tools.showItem(dom.addFormSubDepartmentComboError);
        }

        tools.hideItem(dom.addFormSubMachineCheckListTreeError);
        const nodeSelect = dom.addFormSubMachineCheckListTree.GetFocusedNodeKey();
        const isNodeSelectValid = !tools.isNullOrEmpty(nodeSelect);
        if (!isNodeSelectValid) {
            isValid = false;
            tools.showItem(dom.addFormSubMachineCheckListTreeError);
        }

        tools.hideItem(dom.addCheckListTitleError);
        const checkListTitle = dom.addCheckListTitle.GetValue();
        const isCheckListTitleValid = !tools.isNullOrEmpty(checkListTitle);
        if (!isCheckListTitleValid) {
            isValid = false;
            tools.showItem(dom.addCheckListTitleError);
        }

        tools.hideItem(dom.addFormMaintenanceGroupListComboError);
        const maintenanceGroupList = dom.addFormMaintenanceGroupListCombo.GetValue();
        const isMaintenanceGroupListValid = !tools.isNullOrEmpty(maintenanceGroupList);
        if (!isMaintenanceGroupListValid) {
            tools.showItem(dom.addFormMaintenanceGroupListComboError);
            isValid = false;
        }

        tools.hideItem(dom.addFormSubMachineCodeError);
        const isSubMachineCodeValid = dom.addFormSubMachineCode.GetIsValid();
        if (!isSubMachineCodeValid) {
            tools.showItem(dom.addFormSubMachineCodeError);
            isValid = false;
        }

        const isSerialValid = !tools.isNullOrEmpty(state.serialForOperationItem);
        if (!isSerialValid) {
            motorsazanClient.messageModal.error("لطفا صفحه را مجددا بارگذاری کنید ", "ایراد در ارتباط");
            isValid = false;
        }

        return isValid;
    }

    function isEditOperationItemModalValid() {
        var isValid = true;
        tools.hideItem(dom.editFormOperationItemNameTextBoxError);
        const operationItemName = dom.editFormOperationItemNameTextBox.GetText();
        const isOperationItemNameValid = !tools.isNullOrEmpty(operationItemName);
        if (!isOperationItemNameValid) {
            isValid = false;
            tools.showItem(dom.editFormOperationItemNameTextBoxError);
        }

        return isValid;
    }


    function isMoveOperationItemFromValid() {
        var isValid = true;

        tools.hideItem(dom.moveOperationItemDepartmentListModalError);
        const department = dom.moveOperationItemDepartmentListModal.GetValue();
        const isDepartmentValid = !tools.isNullOrEmpty(department);
        if (!isDepartmentValid) {
            isValid = false;
            tools.showItem(dom.moveOperationItemDepartmentListModalError);
        }

        tools.hideItem(dom.moveOperationItemSubDepartmentListModalError);
        const subDepartment = dom.moveOperationItemSubDepartmentListModal.GetValue();
        const isSubDepartmentValid = !tools.isNullOrEmpty(subDepartment);
        if (!isSubDepartmentValid) {
            isValid = false;
            tools.showItem(dom.moveOperationItemSubDepartmentListModalError);
        }

        tools.hideItem(dom.moveOperationItemSubMachineTreeListModalError);
        const nodeSelect = dom.moveOperationItemSubMachineTreeListModal.GetFocusedNodeKey();
        const isNodeSelectValid = !tools.isNullOrEmpty(nodeSelect);
        if (!isNodeSelectValid) {
            isValid = false;
            tools.showItem(dom.moveOperationItemSubMachineTreeListModalError);
        }

        return isValid;
    }

    function moveOperationItemToOtherMachineInit() {
        moveOperationItemToOtherMachineSetDom();

        initMoveOperationItemMachineTree();
    }

    function moveOperationItemToOtherMachineSetDom() {
        dom.moveOperationItemDepartmentListModal = ASPxClientComboBox.Cast("moveOperationItemDepartmentListModal");
        dom.moveOperationItemDepartmentListModalError = $("#moveOperationItemDepartmentListModalError");
        dom.moveOperationItemDepartmentListModalParent = $("#moveOperationItemDepartmentListModalParent");

        dom.moveOperationItemSubDepartmentListModal =
            ASPxClientComboBox.Cast("moveOperationItemSubDepartmentListModal");
        dom.moveOperationItemSubDepartmentListModalError = $("#moveOperationItemSubDepartmentListModalError");
        dom.moveOperationItemSubDepartmentListModalParent = $("#moveOperationItemSubDepartmentListModalParent");

        dom.moveOperationItemSubMachineTreeListModal =
            ASPxClientTreeList.Cast("moveOperationItemSubMachineTreeListModal");
        dom.moveOperationItemSubMachineTreeListModalError = $("#moveOperationItemSubMachineTreeListModalError");
        dom.moveOperationItemSubMachineTreeListModalParent = $("#moveOperationItemSubMachineTreeListModalParent");
    }

    async function refreshGrid() {
        const url = "/MachineCheckList/MachineCheckListGrid";

        const machineId = state.nodeKey || 0;
        const operationItemTypeId = 3;

        const apiParam = {
            operationItemTypeId: operationItemTypeId,
            machineId: machineId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.machineCheckListGridParent.html(gridHtml);
        setDom();

        onEndCallBack();
    }

    function resetAddForm() {
        dom.addCheckListTitle.SetValue("");
        tools.hideItem(dom.addCheckListTitleError);

        dom.addFormMaintenanceGroupListCombo.SetSelectedIndex(-1);
        tools.hideItem(dom.addFormMaintenanceGroupListComboError);

        dom.addFormSubMachineCode.SetValue("");
        tools.hideItem(dom.addFormSubMachineCodeError);

        dom.addFormOperationTypeItemIdLabel.SetText("3");
        dom.addFormMaintenanceGroupCodeLabel.SetText("00");
        dom.addFormSubMachineCodeLabel.SetText("00");

        initSerialForOperationLabelText();
        setDom();
    }

    function resetMoveOperationItemForm() {
        dom.moveOperationItemDepartmentListModal.SetSelectedIndex(-1);
        tools.hideItem(dom.moveOperationItemDepartmentListModalError);
        state.MoveOperationItemDepartmentId = null;

        dom.moveOperationItemSubDepartmentListModal.SetSelectedIndex(-1);
        tools.hideItem(dom.moveOperationItemSubDepartmentListModalError);
        state.MoveOperationItemSubDepartmentId = null;

        initMoveOperationItemMachineTree();
        tools.hideItem(dom.moveOperationItemSubMachineTreeListModalError);
        state.MoveOperationItemNodeKey = null;

        moveOperationItemToOtherMachineSetDom();
    }

    function resetCopySeveralOperationItemForm() {
        dom.copySeveralOperationItemDepartmentListModal.SetSelectedIndex(-1);
        tools.hideItem(dom.copySeveralOperationItemDepartmentListModalError);
        state.copySeveralOperationItemDepartmentId = null;

        dom.copySeveralOperationItemSubDepartmentListModal.SetSelectedIndex(-1);
        tools.hideItem(dom.copySeveralOperationItemSubDepartmentListModalError);
        state.copySeveralOperationItemSubDepartmentId = null;

        initCopySeveralOperationItemToOtherMachineModalTree();
        tools.hideItem(dom.copySeveralOperationItemSubMachineTreeListModalError);
        state.copySeveralOperationItemNodeKey = null;

        copySeveralOperationItemToOtherMachineModalSetDom();
    }

    function onEndCallBack() {
        dom.gridToolbarChangeCopySeveralOperationButton =
            machineCheckListGrid.GetToolbar(0).GetItemByName("gridToolbarChangeCopySeveralOperationButton");

        const operationItemIdList = dom.machineCheckListGrid.GetSelectedRowCount();
        if (operationItemIdList) {
            dom.gridToolbarChangeCopySeveralOperationButton.SetEnabled(true);
        } else {
            dom.gridToolbarChangeCopySeveralOperationButton.SetEnabled(false);
        }
    }

    function setDom() {
        //add form
        dom.addFormDepartmentCombo = ASPxClientComboBox.Cast("addFormDepartmentCombo");
        dom.addFormDepartmentComboError = $("#addFormDepartmentComboError");
        dom.addFormDepartmentComboParent = $("#addFormDepartmentComboParent");

        dom.addFormSubDepartmentCombo = ASPxClientComboBox.Cast("addFormSubDepartmentCombo");
        dom.addFormSubDepartmentComboError = $("#addFormSubDepartmentComboError");
        dom.addFormSubDepartmentComboParent = $("#addFormSubDepartmentComboParent");

        dom.addFormSubMachineCheckListTree = ASPxClientTreeList.Cast("addFormSubMachineCheckListTree");
        dom.addFormSubMachineCheckListTreeError = $("#addFormSubMachineCheckListTreeError");
        dom.addFormSubMachineCheckListTreeParent = $("#addFormSubMachineCheckListTreeParent");

        dom.addCheckListTitle = ASPxClientMemo.Cast("addCheckListTitle");
        dom.addCheckListTitleError = $("#addCheckListTitleError");
        dom.addCheckListTitleParent = $("#addCheckListTitleParent");

        dom.addFormMaintenanceGroupListCombo = ASPxClientComboBox.Cast("addFormMaintenanceGroupListCombo");
        dom.addFormMaintenanceGroupListComboError = $("#addFormMaintenanceGroupListComboError");
        dom.addFormMaintenanceGroupListComboParent = $("#addFormMaintenanceGroupListComboParent");

        dom.addFormSubMachineCode = ASPxClientTextBox.Cast("addFormSubMachineCode");
        dom.addFormSubMachineCodeError = $("#addFormSubMachineCodeError");
        dom.addFormSubMachineCodeParent = $("#addFormSubMachineCodeParent");


        dom.addFormOperationTypeItemIdLabel = ASPxClientLabel.Cast("addFormOperationTypeItemIdLabel");
        dom.addFormOperationTypeItemIdLabelParent = $("#addFormOperationTypeItemIdLabelParent");

        dom.addFormMaintenanceGroupCodeLabel = ASPxClientLabel.Cast("addFormMaintenanceGroupCodeLabel");
        dom.addFormMaintenanceGroupCodeLabelParent = $("#addFormMaintenanceGroupCodeLabelParent");

        dom.addFormSubMachineCodeLabel = ASPxClientLabel.Cast("addFormSubMachineCodeLabel");
        dom.addFormSubMachineCodeLabelParent = $("#addFormSubMachineCodeLabelParent");

        dom.addFormSerialForOperationLabel = ASPxClientLabel.Cast("addFormSerialForOperationLabel");
        dom.addFormSerialForOperationLabelParent = $("#addFormSerialForOperationLabelParent");

        //Grid
        dom.machineCheckListGridParent = $("#machineCheckListGridParent");
        dom.machineCheckListGrid = ASPxClientGridView.Cast("machineCheckListGrid");

        //Grid Command Button
        dom.gridToolbarChangeCopySeveralOperationButton = machineCheckListGrid.GetToolbar(0)
            .GetItemByName("gridToolbarChangeCopySeveralOperationButton");
    }

    function setEvent() {
        dom.addFormOperationTypeItemIdLabel.SetText("3");
        dom.addFormMaintenanceGroupCodeLabel.SetText("00");
        dom.addFormSubMachineCodeLabel.SetText("00");

        initSerialForOperationLabelText();

        dom.gridToolbarChangeCopySeveralOperationButton.SetEnabled(false);
    }

    function setMachineCheckListGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);

        const activeIndex = event.visibleIndex;
        state.operationItemId = dom.machineCheckListGrid.GetRowKey(activeIndex);
    }

    $(document).ready(init);

    return {
        addCheckList: addCheckList,
        copyOneOperationItemToOtherMachineBtnClick: copyOneOperationItemToOtherMachineBtnClick,
        copySeveralOperationItemToOtherMachineBtnClick: copySeveralOperationItemToOtherMachineBtnClick,
        getCopySeveralOperationItemSubMachineTreeListModalSelectedNode:
            getCopySeveralOperationItemSubMachineTreeListModalSelectedNode,
        getMoveOperationItemSubMachineTreeListModalSelectedNode:
            getMoveOperationItemSubMachineTreeListModalSelectedNode,
        getSubMachineCheckListTreeSelectedNode: getSubMachineCheckListTreeSelectedNode,
        handleActiveOperationItemBtnClick: handleActiveOperationItemBtnClick,
        handleAddCheckListTitleValueChanged: handleAddCheckListTitleValueChanged,
        handleAddFormDepartmentComboSelectedIndexChange: handleAddFormDepartmentComboSelectedIndexChange,
        handleAddFormMachineTreeBeginCallback: handleAddFormMachineTreeBeginCallback,
        handleAddFormMaintenanceGroupListCombo: handleAddFormMaintenanceGroupListCombo,
        handleAddFormSubDepartmentComboSelectedIndexChange: handleAddFormSubDepartmentComboSelectedIndexChange,
        handleAddFormSubMachineCodeValueChanged: handleAddFormSubMachineCodeValueChanged,
        handleCopyOneOperationItemToOtherMachineModalBtnClick: handleCopyOneOperationItemToOtherMachineModalBtnClick,
        handleCopySeveralOperationItemDepartmentListModalSelectedIndexChange:
            handleCopySeveralOperationItemDepartmentListModalSelectedIndexChange,
        handleCopySeveralOperationItemSubDepartmentListModalSelectedIndexChange:
            handleCopySeveralOperationItemSubDepartmentListModalSelectedIndexChange,
        handleCopySeveralOperationItemSubMachineTreeListModalBeginCallback:
            handleCopySeveralOperationItemSubMachineTreeListModalBeginCallback,
        handleDeActiveOperationItemModalBtnClick: handleDeActiveOperationItemModalBtnClick,
        handleDeActiveOperationItemBtnClick: handleDeActiveOperationItemBtnClick,
        handleDeActiveOperationItemReasonTextBoxValueChanged: handleDeActiveOperationItemReasonTextBoxValueChanged,
        handleEditFormBtnClick: handleEditFormBtnClick,
        handleEditFormOperationItemNameTextBoxValueChanged: handleEditFormOperationItemNameTextBoxValueChanged,
        handleEditOperationItemBtnClick: handleEditOperationItemBtnClick,
        handleGridSelectionChanged: handleGridSelectionChanged,
        handleGridToolbarChangeCopySeveralOperationItemForOtherMachineButtonClick:
            handleGridToolbarChangeCopySeveralOperationItemForOtherMachineButtonClick,
        handleMachinesCheckListItemGridBeginCallback: handleMachinesCheckListItemGridBeginCallback,
        handleMoveOperationItemDepartmentListModalSelectedIndexChange:
            handleMoveOperationItemDepartmentListModalSelectedIndexChange,
        handleMoveOperationItemFromCurrentMachineToNewMachineBtnClick:
            handleMoveOperationItemFromCurrentMachineToNewMachineBtnClick,
        handleMoveOperationItemSubDepartmentListModalSelectedIndexChange:
            handleMoveOperationItemSubDepartmentListModalSelectedIndexChange,
        handleMoveOperationItemSubMachineTreeListModalBeginCallback:
            handleMoveOperationItemSubMachineTreeListModalBeginCallback,
        handleMoveOperationItemToOtherMachineModalBtnClick: handleMoveOperationItemToOtherMachineModalBtnClick,
        onEndCallBack: onEndCallBack,
        setMachineCheckListGridFocusedRowOnExpanding: setMachineCheckListGridFocusedRowOnExpanding,
        handleAddSparePartToOperationBtnModalBtnClick: handleAddSparePartToOperationBtnModalBtnClick,
        handleAddSparePartToOperationCheckComboBoxSelectedIndexChange: handleAddSparePartToOperationCheckComboBoxSelectedIndexChange,
        addSparePartToOperationItemClick: addSparePartToOperationItemClick,
        handleAddSparePartToOperationItemGridCallback: handleAddSparePartToOperationItemGridCallback,
        handleAddSparePartToOperationItemGridCustomBtnClick: handleAddSparePartToOperationItemGridCustomBtnClick,
        dom
    };

})();