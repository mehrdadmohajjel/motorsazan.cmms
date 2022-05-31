/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Components/_Uploader.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_LoadingModal.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.machineManagement = (function () {

    var dom = {};
    var state = {
        topMachineId: 0,
        nodeKey: null,
        documentNodeKey: null,
        sparePartNodeKey: null,
        departmentId: 0,
        uploadedFileNames: null,
        uploaderReference: null,
        GridMemberFocusedRowIndex: 0,
        machineSparePartId: null,
        uploadedFileNamesSparePart: null,
        uploaderReferenceSparePart: null,
        selectedDepartmentId: 0,
        rackCode: 73,
        ElectricalRowId: 0,
        OilRowId: 0,
        multipleFileSelectionNewNameModes: {
            disabled: 0,
            requiredForAll: 1,
            optional: 2
        }
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/MachineManagement/";

    function addDocumentToSparePartInit() {
        addMachineSparePartSetDom();
        addMachineSparePartDocumentSetDom();
        
        state.uploaderReferenceSparePart = new motorsazanClient.uploader().create({
            accept: "image/*,audio/*,video/*,compress/*,excel/*,document/*,powerpoint/*,.cdr",
            id: "machineSparePartAttachmentUploader",
            uploadCallback: uploadAttachmentsSparePart,
            deleteUploadedFilesCallback: removeUploadedAttachmentsOfMachineSparePart,
            maxSize: 2 * 1024 * 1024, // 2 GB
            isMultipleSelection: true,
            multipleSelectionConfig: {
                newNameForSelectedFilesMode: state.multipleFileSelectionNewNameModes.disabled
            },
            isLargeFileUploader: true,
            largeFileModeConfig: {
                changeFileNameToUniqueName: true
            }
        });

        state.uploaderReferenceSparePart.enable();
    }

    function addMachineSparePartDocumentSetDom() {
        // Uploader
        dom.machineSparePartAttachmentUploader = $("#machineSparePartAttachmentUploader");
        dom.machineSparePartAttachmentUploaderError = $("#machineSparePartAttachmentUploaderError");
        dom.machineSparePartAttachmentUploaderParent = $("#machineSparePartAttachmentUploaderParent");
        dom.machineSparePartAttachmentUploaderDesign = $("#machineSparePartAttachmentUploaderDesign");

        dom.addMachineSparePartDocumentElectricalSpecificationTextBox = ASPxClientTextBox.Cast("addMachineSparePartDocumentElectricalSpecificationTextBox");
        dom.addMachineSparePartDocumentElectricalSpecificationTextBoxError = $("#addMachineSparePartDocumentElectricalSpecificationTextBoxError");
        dom.addMachineSparePartDocumentElectricalSpecificationTextBoxParent = $("#addMachineSparePartDocumentElectricalSpecificationTextBoxParent");

        dom.addMachineSparePartDocumentMechanicalSpecificationTextBox = ASPxClientTextBox.Cast("addMachineSparePartDocumentMechanicalSpecificationTextBox");
        dom.addMachineSparePartDocumentMechanicalSpecificationTextBoxError = $("#addMachineSparePartDocumentMechanicalSpecificationTextBoxError");
        dom.addMachineSparePartDocumentMechanicalSpecificationTextBoxParent = $("#addMachineSparePartDocumentMechanicalSpecificationTextBoxParent");

        dom.addMachineSparePartDocumentMadeInCompanyTextBox = ASPxClientTextBox.Cast("addMachineSparePartDocumentMadeInCompanyTextBox");
        dom.addMachineSparePartDocumentMadeInCompanyTextBoxError = $("#addMachineSparePartDocumentMadeInCompanyTextBoxError");
        dom.addMachineSparePartDocumentMadeInCompanyTextBoxParent = $("#addMachineSparePartDocumentMadeInCompanyTextBoxParent");

        dom.addMachineSparePartDocumentDescription = ASPxClientMemo.Cast("addMachineSparePartDocumentDescription");
        dom.addMachineSparePartDocumentDescriptionError = $("#addMachineSparePartDocumentDescriptionError");
        dom.addMachineSparePartDocumentDescriptionParent = $("#addMachineSparePartDocumentDescriptionParent");

        dom.machineSparePartDocumentGrid = ASPxClientGridView.Cast("machineSparePartDocumentGrid");
        dom.machineSparePartDocumentGridParent = $("#machineSparePartDocumentGridParent");
    }

    function addMachineSparePartSetDom() {
        dom.addSparePartSubMachineTree = ASPxClientTreeList.Cast("addSparePartSubMachineTree");
        dom.addSparePartSubMachineTreeError = $("#addSparePartSubMachineTreeError");
        dom.addSparePartSubMachineTreeParent = $("#addSparePartSubMachineTreeParent");

        dom.addFormStoreListForSparePartCombo = ASPxClientComboBox.Cast("addFormStoreListForSparePartCombo");
        dom.addFormStoreListForSparePartComboError = $("#addFormStoreListForSparePartComboError");
        dom.addFormStoreListForSparePartComboParent = $("#addFormStoreListForSparePartComboParent");

        dom.sundryModalStockComboByRack = ASPxClientComboBox.Cast(`sundryModalStockComboByRack${state.rackCode}`);
        dom.addFormStockComboError = $("#addFormStockComboError");
        dom.addFormStockComboParent = $("#addFormStockComboParent");

        dom.addStocksImportanceCombo = ASPxClientComboBox.Cast("addStocksImportanceCombo");
        dom.addStocksImportanceComboError = $("#addStocksImportanceComboError");
        dom.addStocksImportanceComboParent = $("#addStocksImportanceComboParent");

        dom.addStockSupplyTypeCombo = ASPxClientComboBox.Cast("addStockSupplyTypeCombo");
        dom.addStockSupplyTypeComboError = $("#addStockSupplyTypeComboError");
        dom.addStockSupplyTypeComboParent = $("#addStockSupplyTypeComboParent");

        dom.addStocksDate = $("#addStocksDate");
        dom.addStocksDateError = $("#addStocksDateError");

        dom.addStockTechnicalCharacteristicsCatalog = ASPxClientTextBox.Cast("addStockTechnicalCharacteristicsCatalog");
        dom.addStockTechnicalCharacteristicsCatalogError = $("#addStockTechnicalCharacteristicsCatalogError");

        // Grid
        dom.machineSparePartDataGridParent = $("#machineSparePartDataGridParent");
        dom.machineSparePartDataGrid = ASPxClientGridView.Cast("machineSparePartDataGrid");
    }

    function addMachineSparePartTreeInit() {
        state.rackCode = 73;

        addMachineSparePartSetDom();

        tools.initDatePicker("addStocksDate");

        dom.addStocksDate.on("change", handleAddStocksDateSelectionChange);
    }

    function addMachineToLocationInit() {
        addMachineToLocationSetDom();

        dom.addFormEmployeeCombo.SetEnabled(false);
        dom.addFormSubDepartmentListCombo.SetEnabled(false);
    }

    function addMachineToLocationSetDom() {
        dom.addFormDepartmentListCombo = ASPxClientComboBox.Cast("addFormDepartmentListCombo");
        dom.addFormDepartmentListComboError = $("#addFormDepartmentListComboError");
        dom.addFormDepartmentListComboParent = $("#addFormDepartmentListComboParent");

        dom.addFormSubDepartmentListCombo = ASPxClientComboBox.Cast("addFormSubDepartmentListCombo");
        dom.addFormSubDepartmentListComboError = $("#addFormSubDepartmentListComboError");
        dom.addFormSubDepartmentListComboParent = $("#addFormSubDepartmentListComboParent");

        dom.addFormEmployeeCombo = ASPxClientComboBox.Cast("addFormEmployeeCombo");
        dom.addFormEmployeeComboError = $("#addFormEmployeeComboError");
        dom.addFormEmployeeComboParent = $("#addFormEmployeeComboParent");

        dom.machinesLocationTable = ASPxClientGridView.Cast("machinesLocationTable");
        dom.machinesLocationTableParent = $("#machinesLocationTableParent");
    }

    function addSubMachineDocumentTreeInit() {
        addSubMachineDocumentTreeSetDom();

        state.uploaderReference = null;

        state.uploadedFileNames = null;

        state.uploaderReference = new motorsazanClient.uploader().create({
            accept: "image/*,audio/*,video/*,compress/*,excel/*,document/*,powerpoint/*,.cdr",
            id: "machineAttachmentUploader",
            uploadCallback: uploadAttachments,
            deleteUploadedFilesCallback: removeUploadedAttachments,
            maxSize: 2 * 1024 * 1024, // 2 GB
            isMultipleSelection: true,
            multipleSelectionConfig: {
                newNameForSelectedFilesMode: state.multipleFileSelectionNewNameModes.disabled
            },
            isLargeFileUploader: true,
            largeFileModeConfig: {
                changeFileNameToUniqueName: true
            }
        });

        state.uploaderReference.enable();
    }

    function addSubMachineDocumentTreeSetDom() {
        dom.addMachineDocumentTree = ASPxClientTreeList.Cast("addMachineDocumentTree");
        dom.addMachineDocumentTreeError = $("#addMachineDocumentTreeError");
        dom.addMachineDocumentTreeParent = $("#addMachineDocumentTreeParent");

        dom.addDocumentTypeListCombo = ASPxClientComboBox.Cast("addDocumentTypeListCombo");
        dom.addDocumentTypeListComboError = $("#addDocumentTypeListComboError");

        dom.machineAttachmentUploader = $("#machineAttachmentUploader");
        dom.machineAttachmentUploaderError = $("#machineAttachmentUploaderError");

        // machineDocumentGrid
        dom.machineDocumentGrid = ASPxClientGridView.Cast("machineDocumentGrid");
        dom.machineDocumentGridParent = $("#machineDocumentGridParent");
    }

    function addSubMachineTreeSetDom() {
        dom.addSubMachineTree = ASPxClientTreeList.Cast("addSubMachineTree");
        dom.addSubMachineTreeError = $("#addSubMachineTreeError");
        dom.addSubMachineTreeParent = $("#addSubMachineTreeParent");

        dom.addSubMachineName = ASPxClientTextBox.Cast("addSubMachineName");
        dom.addSubMachineNameError = $("#addSubMachineNameError");
    }

    async function fillMachineManagementGrid() {
        const url = controllerName + "MachineManagementGrid";
        const apiParam = {
        };

        const machineManagementGridList = await motorsazanClient.connector.post(url, apiParam);

        dom.machineManagementGridParent.html(machineManagementGridList);
        setDom();
    }

    async function getMachineDocumentTreeSelectedNode() {
        addSubMachineDocumentTreeSetDom();

        state.documentNodeKey = dom.addMachineDocumentTree.GetFocusedNodeKey();
        tools.hideItem(dom.addMachineDocumentTreeError);

        const url = controllerName + "GetMachineDocumentListByMachineId";

        const machineId = state.documentNodeKey || 0;

        const apiParam = {
            machineId: machineId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.machineDocumentGridParent.html(gridHtml);

        addSubMachineDocumentTreeSetDom();

        state.uploaderReference.enable(false);
    }

    async function getMachineSparePartTreeSelectedNode() {
        addMachineSparePartSetDom();

        state.sparePartNodeKey = dom.addSparePartSubMachineTree.GetFocusedNodeKey();

        tools.hideItem(dom.addSparePartSubMachineTreeError);

        const url = controllerName + "GetMachineSparePartList";

        const apiParam = {
            machineId: state.sparePartNodeKey
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.machineSparePartDataGridParent.html(gridHtml);

        addMachineSparePartSetDom();
        refreshMachineSparePartDataGrid();
    }

    function getTreeSelectedNode() {
        addSubMachineTreeSetDom();

        state.nodeKey = dom.addSubMachineTree.GetFocusedNodeKey();

        tools.hideItem(dom.addSubMachineTreeError);
    }

    function handleAddFormSubDepartmentListComboBeginCallback(command) {

        const departmentId = dom.addFormDepartmentListCombo.GetValue();

        command.callbackUrl =
            controllerName + "FilterSubDepartmentCombo" + "?DepartmentId=" + departmentId;
    }

    async function handleAddFormSubDepartmentListComboSelectedIndexChanged() {
        const url = controllerName + "AddEmployeeCombo";

        tools.hideItem(dom.addFormSubDepartmentListComboError);

        dom.addFormEmployeeCombo.SetEnabled(true);
        const unitCode = dom.addFormDepartmentListCombo.GetSelectedItem().texts[1];


        const apiParam = {
            unitCode: unitCode
        };

        const employeeListCombo = await motorsazanClient.connector.post(url, apiParam);

        dom.addFormEmployeeComboParent.html(employeeListCombo);

        addMachineToLocationSetDom();
    }

    function handleAddMachineDocumentBtnClick() {
        const url = controllerName + "AddMachineDocument";

        const topMachineId = state.topMachineId;

        const apiParam = {
            topMachineId: topMachineId
        };

        const title = "ثبت ضمائم و پرونده های دستگاه";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, addSubMachineDocumentTreeInit, false, true);
    }

    function handleAddMachineSparePartBtnClick() {
        const url = controllerName + "AddFormMachineSparePart";

        const topMachineId = state.topMachineId;

        const apiParam = {
            topMachineId: topMachineId
        };

        const title = "افزودن قطعه یدکی";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, addMachineSparePartTreeInit, false, true);
    }

    function handleAddMachineBaseInfoBtnClick() {
        const url = controllerName + "ShowElectricalModal";

        const topMachineId = state.topMachineId;

        const apiParam = {
            MachineId: topMachineId
        };

        const title = "اطلاعات شناسنامه ای دستگاه";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, initMachineBaseOutputModal, false, true);

    }
    function handleAddFormPreventiveItemTextBoxValueChanged() {
        tools.hideItem(dom.addFormPreventiveItemTextBoxBoxError);
    }

    function handleAddFormOilNameTextBoxValueChanged() {
        tools.hideItem(dom.addFormOilNameTextBoxError);
    }

    function handleAddFormManufacturerTextBoxValueChanged() {
        tools.hideItem(dom.addFormManufacturerTextBoxError);

    }
    function handleAddFormTankVolumeTextBoxValueChanged() {
        tools.hideItem(dom.addFormTankVolumeBoxError);

    }
    function handleAddFormOilUnitComboChanged() {

        tools.hideItem(dom.addFormOilUnitComboError);
    }
    function handleAddFormStandardVolumeTextBoxValueChanged() {
        tools.hideItem(dom.addFormStandardVolumeTextBoxError);
    }
    function handleAddFormPeriodTextBoxValueChanged() {
        tools.hideItem(dom.addFormPeriodTextBoxError);
    }
    function handleAddFormYearlyUsageTextBoxValueChanged() {
        tools.hideItem(dom.addFormYearlyUsageTextBoxError);
    }
    function handleAddFormCharacteristicComboChanged() {
        tools.hideItem(dom.addFormCharacteristicComboError);
    }
    function handleAddFormProducerTextBoxValueChanged() {
        tools.hideItem(dom.addFormProducerTextBoxError);
    }
    function handleAddFormPowerTextBoxBoxValueChanged() {
        tools.hideItem(dom.addFormPowerTextBoxError);
    }
    function handleAddCurrentValueChanged() {
        tools.hideItem(dom.addFormcurrentTextBoxError);
    }

    function handleAddCurrentTypeValueChanged() {
        tools.hideItem(dom.addFormCurrentTypeComboError);
    }
    function handleAddFormRpmComboValueChanged() {
        tools.hideItem(dom.addFormRpmComboError);
    }
    function handleAddFormVoltageComboValueChanged() {
        tools.hideItem(dom.addFormVoltageComboError);
    }
    function handleAddFormProducerCountryNameTextBoxChanged() {
        tools.hideItem(dom.addFormProducerCountryNameTextBoxError);

    }
    function handleAddFormMachineBuiltYearTextBoxChanged() {
        tools.hideItem(dom.addFormMachineBuiltYearTextBoxError);

    }

    function initMachineBaseOutputModal() {
        setDom();
        motorsazanClient.tools.initDatePicker("addFormImportDate");
        dom.machineProducerTabBtn.off("click", showMachineProducerTab).on("click", showMachineProducerTab);
        dom.electricalTabBtn.off("click", showMachineElectricalTab).on("click", showMachineElectricalTab);
        dom.oilTabBtn.off("click", showOilAndLubricationModalTab).on("click", showOilAndLubricationModalTab);
    }
    
    function showMachineProducerTab() {
        const url = "/MachineManagement/MachineProducerTabContent";
        const apiParam = {
            MachineId: state.topMachineId
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (tabContent) {
                dom.baseinfoTabsContentWrapper.html(tabContent);
                initMachineBaseOutputModal();
                dom.machineProducerTabBtn.addClass("tab-header__btn--active");
                dom.electricalTabBtn.removeClass("tab-header__btn--active");
                dom.oilTabBtn.removeClass("tab-header__btn--active");
            });
    }

    function showMachineElectricalTab() {
        const url = "/MachineManagement/MachineElectricalTabContent";
        const apiParam = {
            MachineId: state.topMachineId
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (tabContent) {
                dom.baseinfoTabsContentWrapper.html(tabContent);
                initMachineBaseOutputModal();
                dom.electricalTabBtn.addClass("tab-header__btn--active");
                dom.oilTabBtn.removeClass("tab-header__btn--active");
                dom.machineProducerTabBtn.removeClass("tab-header__btn--active");
            });
    }

    function showOilAndLubricationModalTab() {
        const url = "/MachineManagement/ShowOilAndLubricationModal";
        const apiParam = {
            MachineId: state.topMachineId
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (tabContent) {
                dom.baseinfoTabsContentWrapper.html(tabContent);
                initMachineBaseOutputModal();
                dom.oilTabBtn.addClass("tab-header__btn--active");
                dom.electricalTabBtn.removeClass("tab-header__btn--active");
                dom.machineProducerTabBtn.removeClass("tab-header__btn--active");
            });
    }

    function handleAddMachineToLineBtnClick() {
        const url = controllerName + "AddLocation";

        const topMachineId = state.topMachineId;

        const apiParam = {
            topMachineId: topMachineId
        };

        const title = "انتساب دستگاه به فرد";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, addMachineToLocationInit, true, false);
    }

    function handleAddDocumentTypeListComboSelectedIndexChanged() {
        tools.hideItem(dom.addDocumentTypeListComboError);
    }

    function handleAddFormDepartmentListComboBeginCallback(command) {
        const url = controllerName + "FilterDepartmentCombo";

        command.callbackUrl = url;
    }

    async function handleAddFormDepartmentListComboSelectedIndexChanged() {
        const url = controllerName + "FilterSubDepartmentCombo";

        tools.hideItem(dom.addFormDepartmentListComboError);

        dom.addFormSubDepartmentListCombo.SetEnabled(true);

        const departmentId = dom.addFormDepartmentListCombo.GetValue();

        const apiParam = {
            departmentId: departmentId
        };
        const subDepartmentListCombo = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormSubDepartmentListComboParent.html(subDepartmentListCombo);

        addMachineToLocationSetDom();
    }

    function handleAddFormEmployeeComboBeginCallback(command) {

        const unitCode = dom.addFormDepartmentListCombo.GetSelectedItem().texts[1];

        command.callbackUrl =
            controllerName + "AddEmployeeCombo" + "?UnitCode=" + unitCode;
    }

    function handleAddFormEmployeeComboSelectedIndexChanged() {
        tools.hideItem(dom.addFormEmployeeComboError);
    }

    async function handleAddFormMachineToLocationSaveBtnClick() {
        if (!isMachineToLocationFormValid())
            return;

        const url = controllerName + "AssignMachineToEmployee";

        const topMachineId = state.topMachineId;
        const subDepartmentId = dom.addFormSubDepartmentListCombo.GetValue();
        const employeeId = dom.addFormEmployeeCombo.GetValue();

        const apiParam = {
            employeeId: employeeId,
            machineId: topMachineId,
            departmentId: subDepartmentId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        addMachineToLocationInit();

        motorsazanClient.messageModal.success(apiResult);

        resetAssignMachineToEmployeeForm();

        motorsazanClient.contentModal.hide();
    }

    async function handleAddMachineSparePartDocumentSaveBtnClick() {
        if (!isAddMachineSparePartDocumentFormValid())
            return;

        const url = controllerName + "AddMachineSparePartDocument";

        const machineSparePartId = state.machineSparePartId;
        const electricalSpecification = dom.addMachineSparePartDocumentElectricalSpecificationTextBox.GetText();
        const mechanicalSpecification = dom.addMachineSparePartDocumentMechanicalSpecificationTextBox.GetText();
        const madeInCompany = dom.addMachineSparePartDocumentMadeInCompanyTextBox.GetText();
        const description = dom.addMachineSparePartDocumentDescription.GetText();

        const machineFileDetailList = state.uploadedFileNamesSparePart.map(fileName => ({fileName}));

        const apiParam = {
            machineSparePartId: machineSparePartId,
            machineFileDetailList: machineFileDetailList,
            mechanicalSpecification: mechanicalSpecification,
            electricalSpecification: electricalSpecification,
            madeInCompany: madeInCompany,
            description: description
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        motorsazanClient.messageModal.success(apiResult);

        resetAddMachineSparePartDocumentForm();

        refreshMachineSparePartDocumentGrid();
    }

    function handleAddMachineSparePartDocumentElectricalSpecificationTextBoxValueChanged() {
        tools.hideItem(dom.addMachineSparePartDocumentElectricalSpecificationTextBoxError);
    }

    function handleAddMachineSparePartDocumentMechanicalSpecificationTextBoxValueChanged() {
        tools.hideItem(dom.addMachineSparePartDocumentMechanicalSpecificationTextBoxError);
    }

    function handleAddMachineSparePartDocumentMadeInCompanyTextBoxValueChanged() {
        tools.hideItem(dom.addMachineSparePartDocumentMadeInCompanyTextBoxError);
    }

    function handleAddMachineSparePartDocumentDescriptionValueChanged() {
        tools.hideItem(dom.addMachineSparePartDocumentDescriptionError);
    }

    async function handleAddFormStoreListForSparePartComboSelectedIndexChanged() {
        const url = controllerName + "SundryModalStockComboByRackCode";

        addMachineSparePartSetDom();

        const rackCode = dom.addFormStoreListForSparePartCombo.GetValue();

        const apiParam = {
            rackCode: rackCode
        };

        const comboHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.addFormStockComboParent.html(comboHtml);

        state.rackCode = rackCode;

        addMachineSparePartSetDom();
    }

    function handleAddStocksDateSelectionChange() {
        tools.hideItem(dom.addStocksDateError);
    }

    function handleAddStocksImportanceComboSelectedIndexChanged() {
        tools.hideItem(dom.addStocksImportanceComboError);
    }

    function handleAddStockSupplyTypeComboSelectedIndexChanged() {
        tools.hideItem(dom.addStockSupplyTypeComboError);
    }

    function handleAddStockTechnicalCharacteristicsCatalogValueChanged() {
        tools.hideItem(dom.addStockTechnicalCharacteristicsCatalogError);

        const stockTechNo = dom.addStockTechnicalCharacteristicsCatalog.GetValue();
        const isStockTechNoValid = !tools.isNullOrEmpty(stockTechNo);
        if (!isStockTechNoValid) {
            tools.showItem(dom.addStockTechnicalCharacteristicsCatalogError);
        }
    }

    function handleAddSubMachineBtnClick() {
        const url = controllerName + "AddSubMachine";

        const topMachineId = state.topMachineId;

        const apiParam = {
            topMachineId: topMachineId
        };

        const title = "ثبت زیر دستگاه";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, addSubMachineTreeSetDom, true, false);
    }

    async function handleAddSubMachineDocumentSaveBtnClick() {
        if (!isAddSubMachineDocumentFormValid())
            return;

        const url = controllerName + "AddNewMachineDocument";

        const machineId = dom.addMachineDocumentTree.GetFocusedNodeKey();
        const fileTypeId = dom.addDocumentTypeListCombo.GetValue();

        const fileNames = state.uploadedFileNames;

        const machineFileDetailList = fileNames.map(fileName => ({fileName}));

        const apiParam = {
            machineId: machineId,
            fileTypeId: fileTypeId,
            machineFileDetailList: machineFileDetailList
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        addSubMachineDocumentTreeSetDom();

        motorsazanClient.messageModal.success(apiResult);

        refreshMachineDocumentGrid();

        resetAddMachineDocumentForm();
    }

    function handleAddSubMachineNameTextBoxValueChanged() {
        tools.hideItem(dom.addSubMachineNameError);
    }

    async function handleAddSubMachineSaveBtnClick() {
        if (!isAddSubMachineFormValid())
            return;

        const url = controllerName + "AddNewSubMachine";


        const subMachineName = dom.addSubMachineName.GetText();
        const machineParentId = dom.addSubMachineTree.GetFocusedNodeKey();

        const apiParam = {
            machineParentId: machineParentId,
            subMachineName: subMachineName
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        addSubMachineTreeSetDom();

        resetAddSubMachineForm();

        motorsazanClient.messageModal.success(apiResult);
    }

    async function handleAddSubMachineSparePartSaveBtnClick() {

        addMachineSparePartSetDom();

        if (!isAddSparePartFormValid())
            return;

        const url = controllerName + "AddMachineSparePart";

        state.sparePartNodeKey = dom.addSparePartSubMachineTree.GetFocusedNodeKey();

        const stock = dom.sundryModalStockComboByRack.GetValue();
        const storeListForSparePart = dom.addFormStoreListForSparePartCombo.GetSelectedItem().texts[1];;
        const stockSupplyType = dom.addStockSupplyTypeCombo.GetValue();
        const stocksImportance = dom.addStocksImportanceCombo.GetValue();
        const stocksDate = dom.addStocksDate.val();
        const stockTechnicalCharacteristicsCatalog = dom.addStockTechnicalCharacteristicsCatalog.GetValue();

        const apiParam = {
            machineId: state.sparePartNodeKey,
            stockId: stock,
            wearhouseCode: storeListForSparePart,
            isInternalSupply: stockSupplyType,
            isPrivate: stocksImportance,
            persianDate: stocksDate,
            technicalCharacteristicsOfCatalog: stockTechnicalCharacteristicsCatalog
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        motorsazanClient.messageModal.success(apiResult);

        addMachineSparePartSetDom();

        refreshMachineSparePartDataGrid();

        resetMachineSparePartForm();
    }

    function handleMachineLocationRemoveButtonClick() {
        const content = "آیا از حذف این ردیف مطمئن هستید؟";
        const title = "تاییدیه حذف";
        motorsazanClient.messageModal.confirm(content, removeMachineLocationPopup, title);
    }

    function handleMachineManagementGridBeginCallback(command) {
        const url = controllerName + "MachineManagementGrid" + "?departmentId=" + state.selectedDepartmentId ;
        command.callbackUrl = url;
    }

    function handleMachineManagementTreeBeginCallback(command) {
        command.callbackUrl =
            controllerName + "SubMachineTree" + "?machineTopParentId=" + state.topMachineId;
    }

    function handleMachineSparePartDataGridBeginCallback(command) {

        const machineId = state.sparePartNodeKey ? state.sparePartNodeKey : 0;
        const url = controllerName +
            "GetMachineSparePartList" +
            "?MachineId=" +
            machineId;

        command.callbackUrl = url;
    }

    function handleMachineSparePartDocumentGridBeginCallback(command) {
        const machineSparePartId = state.machineSparePartId ? state.machineSparePartId : 0;
        const url = controllerName +
            "GetMachineSparePartDocumentList" +
            "?MachineSparePartId=" +
            machineSparePartId;

        command.callbackUrl = url;
    }

    function handleMachineDocumentGridBeginCallback(command) {
        const machineId = state.documentNodeKey ? state.documentNodeKey : 0;
        const url = controllerName +
            "GetMachineDocumentListByMachineId" +
            "?machineId=" + machineId;

        command.callbackUrl = url;
    }

    function handleMachineElectricalDataGridBeginCallback(command) {
        const url = controllerName +
            "MachineElectricalTabContentGrid" +
            "?machineId=" + state.topMachineId;

        command.callbackUrl = url;
    }

    function handleMachineOilUsageGridBeginCallback(command) {
        const url = controllerName +
            "MachineOilTabContentGrid" +
            "?machineId=" + state.topMachineId;

        command.callbackUrl = url;
    }
    
    function handleMachineDocumentGridCustomBtnClick(source, event) {
        const customBtnId = event.buttonID;
        if (customBtnId === "removeUploadedMachineDocumentMemberBtn") return showRemoveMachineDocumentConfirmation();

        if (customBtnId === "getUploadedMachineDocumentMemberBtn") {
            dom.machineDocumentGrid.GetRowValues(
                dom.machineDocumentGrid.GetFocusedRowIndex(),
                "FileName",
                (fileName) => window.open(`/UploadedFiles/${fileName}`, "_blank")
            );
        }
    }

    function handleSparePartDocumentGridCustomBtnClick(source, event) {
        const customBtnId = event.buttonID;
        if (customBtnId === "removeUploadedMemberBtn") return showRemoveMachineSparePartDocumentConfirmation();

        if (customBtnId === "getUploadedMemberBtn") {
            dom.machineSparePartDocumentGrid.GetRowValues(
                dom.machineSparePartDocumentGrid.GetFocusedRowIndex(),
                "FileName",
                (fileName) =>  window.open(`/UploadedFiles/${fileName}`, "_blank")
            );
        }
    }

    function handleShowMachineAndSubMachineBtnClick() {
        const url = controllerName + "SubMachineTreeModal";

        const machineTopParentId = state.topMachineId;

        const apiParam = {
            machineTopParentId: machineTopParentId
        };

        const title = "مشاهده دستگاه و زیر دستگاه";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, machineTreeSetDom, true, false);
    }

    function handleSparePartGridCustomBtnClick(source, event) {
        const customBtnId = event.buttonID;
        if (customBtnId === "removeMemberBtn") return showRemoveMachineSparePartConfirmation();
        if (customBtnId === "uploadMemberBtn") return showUploadFileToSpare();
        return null;
    }

    function handleSundryModalStockComboByRackSelectionChange() {
        tools.hideItem(dom.addFormStockComboError);
    }

    function handleMachineElectricalGridCustomBtnClick(source, event) {
        setDom();
        const customBtnId = event.buttonID;
        state.ElectricalRowId =
            dom.machineElectricalDataGrid.GetRowKey(event.visibleIndex);

        if (customBtnId === "removeElectricalBtn") return removeElectricalRow();
    }

    function isUpdateMachineBuiltInfoValid() {
        var isValid = true;

        tools.hideItem(dom.addFormMachineBuiltYearTextBoxError);
        const buitYear = dom.addFormMachineBuiltYearTextBox.GetValue();
        const isBuitYearValid = !tools.isNullOrEmpty(buitYear);
        if (!isBuitYearValid) {
            isValid = false;
            tools.showItem(dom.addFormMachineBuiltYearTextBoxError);
        }

        tools.hideItem(dom.addFormProducerCountryNameTextBoxError);
        const producerCountryName = dom.addFormProducerCountryNameTextBox.GetValue();
        const isProducerCountryNameValid = !tools.isNullOrEmpty(producerCountryName);
        if (!isProducerCountryNameValid) {
            isValid = false;
            tools.showItem(dom.addFormProducerCountryNameTextBoxError);
        }

        tools.hideItem(dom.addFormImportDateError);
        const power = dom.addFormImportDate.val();
        const isPowerValid = !tools.isNullOrEmpty(power);
        if (!isPowerValid) {
            tools.showItem(dom.addFormImportDateError);
            isValid = false;
        }

        return isValid;
    }

    function isRegisterOilAndLubricationValid() {
        var isValid = true;

        tools.hideItem(dom.addFormPreventiveItemTextBoxBoxError);
        const item = dom.addFormPreventiveItemTextBox.GetValue();
        const iitemValid = !tools.isNullOrEmpty(item);
        if (!iitemValid) {
            isValid = false;
            tools.showItem(dom.addFormPreventiveItemTextBoxBoxError);
        }

        tools.hideItem(dom.addFormOilNameTextBoxError);
        const oilName = dom.addFormOilNameTextBox.GetValue();
        const isOilNameValid = !tools.isNullOrEmpty(oilName);
        if (!isOilNameValid) {
            isValid = false;
            tools.showItem(dom.addFormOilNameTextBoxError);
        }


        tools.hideItem(dom.addFormManufacturerTextBoxError);
        const manufacturer = dom.addFormManufacturerTextBox.GetValue();
        const isManufacturerValid = !tools.isNullOrEmpty(manufacturer);
        if (!isManufacturerValid) {
            isValid = false;
            tools.showItem(dom.addFormManufacturerTextBoxError);
        }

        tools.hideItem(dom.addFormTankVolumeBoxError);
        const tankVolume = dom.addFormTankVolumeTextBox.GetValue();
        const isTankVolumeValid = !tools.isNullOrEmpty(tankVolume);
        if (!isTankVolumeValid) {
            isValid = false;
            tools.showItem(dom.addFormTankVolumeBoxError);
        }

        tools.hideItem(dom.addFormOilUnitComboError);
        const oilUnit = dom.addFormOilUnitCombo.GetValue();
        const isOilUnit = !tools.isNullOrEmpty(oilUnit);
        if (!isOilUnit) {
            isValid = false;
            tools.showItem(dom.addFormOilUnitComboError);
        }

        tools.hideItem(dom.addFormStandardVolumeTextBoxError);
        const standardVolume = dom.addFormStandardVolumeTextBox.GetValue();
        const isStandardVolumeValid = !tools.isNullOrEmpty(standardVolume);
        if (!isStandardVolumeValid) {
            isValid = false;
            tools.showItem(dom.addFormStandardVolumeTextBoxError);
        }

        tools.hideItem(dom.addFormPeriodTextBoxError);
        const formPeriod = dom.addFormPeriodTextBox.GetValue();
        const isFormPeriodValid = !tools.isNullOrEmpty(formPeriod);
        if (!isFormPeriodValid) {
            isValid = false;
            tools.showItem(dom.addFormPeriodTextBoxError);
        }

        tools.hideItem(dom.addFormYearlyUsageTextBoxError);
        const yearlyUsage = dom.addFormYearlyUsageTextBox.GetValue();
        const isYearlyUsageValid = !tools.isNullOrEmpty(yearlyUsage);
        if (!isYearlyUsageValid) {
            isValid = false;
            tools.showItem(dom.addFormYearlyUsageTextBoxError);
        }



        return isValid;
    }


    function isRegisterElectricalInfoValid() {
        var isValid = true;

        tools.hideItem(dom.addFormCharacteristicComboError);
        const Characteristic = dom.addFormCharacteristicCombo.GetValue();
        const isCharacteristicValid = !tools.isNullOrEmpty(Characteristic);
        if (!isCharacteristicValid) {
            isValid = false;
            tools.showItem(dom.addFormCharacteristicComboError);
        }

        tools.hideItem(dom.addFormProducerTextBoxError);
        const Producer = dom.addFormProducerTextBox.GetValue();
        const isProducerValid = !tools.isNullOrEmpty(Producer);
        if (!isProducerValid) {
            isValid = false;
            tools.showItem(dom.addFormProducerTextBoxError);
        }

        tools.hideItem(dom.addFormPowerTextBoxError);
        const power = dom.addFormPowerTextBox.GetValue();
        const isPowerValid = !tools.isNullOrEmpty(power);
        if (!isPowerValid) {
            tools.showItem(dom.addFormPowerTextBoxError);
            isValid = false;
        }

        tools.hideItem(dom.addFormRpmComboError);
        const rpm = dom.addFormRpmCombo.GetValue();
        const isRpmValid = !tools.isNullOrEmpty(rpm);
        if (!isRpmValid) {
            tools.showItem(dom.addFormRpmComboError);
            isValid = false;
        }

        tools.hideItem(dom.addFormVoltageComboError);
        const voltage = dom.addFormVoltageCombo.GetValue();
        const isVoltageValid = !tools.isNullOrEmpty(voltage);
        if (!isVoltageValid) {
            tools.showItem(dom.addFormVoltageComboError);
            isValid = false;
        }

        tools.hideItem(dom.addFormcurrentTextBoxError);
        const current = dom.addFormcurrentTextBox.GetValue();
        const isCurrentValid = !tools.isNullOrEmpty(current);
        if (!isCurrentValid) {
            tools.showItem(dom.addFormcurrentTextBoxError);
            isValid = false;
        }

        tools.hideItem(dom.addFormCurrentTypeComboError);
        const currentType = dom.addFormCurrentTypeCombo.GetValue();
        const isCurrentTypeValid = !tools.isNullOrEmpty(currentType);
        if (!isCurrentTypeValid) {
            tools.showItem(dom.addFormCurrentTypeComboError);
            isValid = false;
        }

        return isValid;
    }

    async function removeElectricalRow() {
        var title = "تاییدیه حذف";
        var content = "آیا از حذف این ردیف مطمئن هستید؟";

        motorsazanClient.messageModal.confirm(content, removeSelectedElectricalRow, title);

    }
    async function removeSelectedElectricalRow() {

        var params = {
            RecordId: state.ElectricalRowId
        };
        const url = controllerName + "RemoveMachineElectricalInfoByRecordId";
        const result = await motorsazanClient.connector.post(url, params);
        await refreshElectricalGrid();
        motorsazanClient.messageModal.success(result);
        motorsazanClient.messageModal.hide();
        resetAddMachineElectricalInfo();

    }

    async function refreshElectricalGrid() {
        const url = controllerName + "MachineElectricalTabContentGrid";
        setDom();

        const apiParam = {
            machineId: state.topMachineId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.elctircalGridParent.html(gridHtml);
        setDom();
    }



    function handleMachineOilUsageGridCustomBtnClick(source, event) {
        setDom();
        const customBtnId = event.buttonID;
        state.OilRowId =
            dom.oilGrid.GetRowKey(event.visibleIndex);

        if (customBtnId === "removeOilBtn") return removeOilUsageRow();
    }
    async function removeOilUsageRow() {
        var title = "تاییدیه حذف";
        var content = "آیا از حذف این ردیف مطمئن هستید؟";

        motorsazanClient.messageModal.confirm(content, removeSelectedUsedOilRow, title);

    }
    async function removeSelectedUsedOilRow(values) {

        var params = {
            RecordId: state.OilRowId
    };
        const url = controllerName + "RemoveMachineOilAndLubricationInfoByRecordID";
        const result = await motorsazanClient.connector.post(url, params);
        await refreshOilUsageGrid();
        motorsazanClient.messageModal.success(result);

    }
    async function refreshOilUsageGrid() {
        const url = controllerName + "MachineOilTabContentGrid";

        const apiParam = {
            machineId: state.topMachineId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.oilGridParent.html(gridHtml);
        setDom();
    }

    function init() {
        setDom();
        setEvents();
        dom.filterSubDepartmentHasMachineCombo.SetEnabled(false);

    }

    function isAddMachineSparePartDocumentFormValid() {
        addMachineSparePartDocumentSetDom();
        var isValid = true;

        tools.hideItem(dom.addMachineSparePartDocumentElectricalSpecificationTextBoxError);
        const electricalSpecification = dom.addMachineSparePartDocumentElectricalSpecificationTextBox.GetText();
        const isElectricalSpecificationValid = !tools.isNullOrEmpty(electricalSpecification);
        if (!isElectricalSpecificationValid) {
            isValid = false;
            tools.showItem(dom.addMachineSparePartDocumentElectricalSpecificationTextBoxError);
        }

        tools.hideItem(dom.addMachineSparePartDocumentMechanicalSpecificationTextBoxError);
        const mechanicalSpecification = dom.addMachineSparePartDocumentMechanicalSpecificationTextBox.GetText();
        const isMechanicalSpecificationValid = !tools.isNullOrEmpty(mechanicalSpecification);
        if (!isMechanicalSpecificationValid) {
            isValid = false;
            tools.showItem(dom.addMachineSparePartDocumentMechanicalSpecificationTextBoxError);
        }

        tools.hideItem(dom.addMachineSparePartDocumentMadeInCompanyTextBoxError);
        const madeInCompany = dom.addMachineSparePartDocumentMadeInCompanyTextBox.GetText();
        const isMadeInCompanyValid = !tools.isNullOrEmpty(madeInCompany);
        if (!isMadeInCompanyValid) {
            isValid = false;
            tools.showItem(dom.addMachineSparePartDocumentMadeInCompanyTextBoxError);
        }

        tools.hideItem(dom.addMachineSparePartDocumentDescriptionError);
        const description = dom.addMachineSparePartDocumentDescription.GetText();
        const isDescriptionValid = !tools.isNullOrEmpty(description);
        if (!isDescriptionValid) {
            isValid = false;
            tools.showItem(dom.addMachineSparePartDocumentDescriptionError);
        }

        tools.hideItem(dom.machineSparePartAttachmentUploaderError);
        const uploadedFileNamesSparePart = state.uploadedFileNamesSparePart;
        const isUploadedFileNamesSparePartValid = !tools.isNullOrEmpty(uploadedFileNamesSparePart);
        if (!isUploadedFileNamesSparePartValid) {
            isValid = false;
            tools.showItem(dom.machineSparePartAttachmentUploaderError);
        }

        return isValid;
    }

    function isAddSparePartFormValid() {
        addMachineSparePartSetDom();
        var isValid = true;

        tools.hideItem(dom.addSparePartSubMachineTreeError);
        dom.addSparePartSubMachineTree.GetFocusedNodeKey();
        state.sparePartNodeKey = dom.addSparePartSubMachineTree.GetFocusedNodeKey();
        const isSparePartSubMachineTreeValid = !tools.isNullOrEmpty(state.sparePartNodeKey);
        if (!isSparePartSubMachineTreeValid) {
            isValid = false;
            tools.showItem(dom.addSparePartSubMachineTreeError);
        }

        tools.hideItem(dom.addFormStoreListForSparePartComboError);
        const storeListForSparePart = dom.addFormStoreListForSparePartCombo.GetValue();
        const isStoreListForSparePartValue = !tools.isNullOrEmpty(storeListForSparePart);
        if (!isStoreListForSparePartValue) {
            isValid = false;
            tools.showItem(dom.addFormStoreListForSparePartComboError);
        }

        tools.hideItem(dom.addFormStockComboError);
        const stock = dom.sundryModalStockComboByRack.GetValue();
        const isStockHasValue = !tools.isNullOrEmpty(stock);
        if (!isStockHasValue) {
            isValid = false;
            tools.showItem(dom.addFormStockComboError);
        }

        tools.hideItem(dom.addStocksImportanceComboError);
        const stocksImportance = dom.addStocksImportanceCombo.GetValue();
        const isStocksImportanceValid = !tools.isNullOrEmpty(stocksImportance);
        if (!isStocksImportanceValid) {
            isValid = false;
            tools.showItem(dom.addStocksImportanceComboError);
        }

        tools.hideItem(dom.addStockSupplyTypeComboError);
        const stockSupplyType = dom.addStockSupplyTypeCombo.GetValue();
        const isStockSupplyTypeValid = !tools.isNullOrEmpty(stockSupplyType);
        if (!isStockSupplyTypeValid) {
            isValid = false;
            tools.showItem(dom.addStockSupplyTypeComboError);
        }

        tools.hideItem(dom.addStocksDateError);
        const stocksDate = dom.addStocksDate.val();
        const isStocksDateValid = tools.isValidPersianDate(stocksDate);
        if (!isStocksDateValid) {
            isValid = false;
            tools.showItem(dom.addStocksDateError);
        }

        tools.hideItem(dom.addStockTechnicalCharacteristicsCatalogError);
        const stockTechnicalCharacteristicsCatalog = dom.addStockTechnicalCharacteristicsCatalog.GetValue();
        const isStockTechnicalCharacteristicsCatalogValid = !tools.isNullOrEmpty(stockTechnicalCharacteristicsCatalog);
        if (!isStockTechnicalCharacteristicsCatalogValid) {
            isValid = false;
            tools.showItem(dom.addStockTechnicalCharacteristicsCatalogError);
        }

        return isValid;
    }

    function isAddSubMachineDocumentFormValid() {
        addSubMachineDocumentTreeSetDom();

        var isValid = true;

        tools.hideItem(dom.addMachineDocumentTreeError);
        const nodeSelect = dom.addMachineDocumentTree.GetFocusedNodeKey();
        const isNodeSelectValid = !tools.isNullOrEmpty(nodeSelect);
        if (!isNodeSelectValid) {
            tools.showItem(dom.addMachineDocumentTreeError);
            isValid = false;
        }

        tools.hideItem(dom.addDocumentTypeListComboError);
        const fileTitle = dom.addDocumentTypeListCombo.GetValue();
        const isFileTitleValid = !tools.isNullOrEmpty(fileTitle);
        if (!isFileTitleValid) {
            isValid = false;
            tools.showItem(dom.addDocumentTypeListComboError);
        }

        tools.hideItem(dom.machineAttachmentUploaderError);
        const uploadedFileNames = state.uploadedFileNames;
        const isUploadedFileNamesValid = !tools.isNullOrEmpty(uploadedFileNames);
        if (!isUploadedFileNamesValid) {
            isValid = false;
            tools.showItem(dom.machineAttachmentUploaderError);
        }

        return isValid;
    }

    function isAddSubMachineFormValid() {
        var isValid = true;

        tools.hideItem(dom.addSubMachineNameError);
        const machineName = dom.addSubMachineName.GetText();
        const isMachineNameValid = !tools.isNullOrEmpty(machineName);
        if (!isMachineNameValid) {
            isValid = false;
            tools.showItem(dom.addSubMachineNameError);
        }

        tools.hideItem(dom.addSubMachineTreeError);
        const nodeSelect = dom.addSubMachineTree.GetFocusedNodeKey();
        const isNodeSelectValid = !tools.isNullOrEmpty(nodeSelect);
        if (!isNodeSelectValid) {
            tools.showItem(dom.addSubMachineTreeError);
            isValid = false;
        }

        return isValid;
    }

    function isMachineToLocationFormValid() {
        var isValid = true;

        tools.hideItem(dom.addFormDepartmentListComboError);
        const departmentId = dom.addFormDepartmentListCombo.GetValue();
        const isDepartmentIdValid = !tools.isNullOrEmpty(departmentId);
        if (!isDepartmentIdValid) {
            isValid = false;
            tools.showItem(dom.addFormDepartmentListComboError);
        }

        tools.hideItem(dom.addFormSubDepartmentListComboError);
        const subDepartmentId = dom.addFormSubDepartmentListCombo.GetValue();
        const isSubDepartmentIdValid = !tools.isNullOrEmpty(subDepartmentId);
        if (!isSubDepartmentIdValid) {
            isValid = false;
            tools.showItem(dom.addFormSubDepartmentListComboError);
        }

        tools.hideItem(dom.addFormEmployeeComboError);
        const employeeList = dom.addFormEmployeeCombo.GetValue();
        const isEmployeeListValid = !tools.isNullOrEmpty(employeeList);
        if (!isEmployeeListValid) {
            tools.showItem(dom.addFormEmployeeComboError);
            isValid = false;
        }

        return isValid;
    }

    function machineTreeSetDom() {
        dom.machineTree = ASPxClientTreeList.Cast("machineTree");
        dom.machineTreeParent = ASPxClientTreeList.Cast("machineTreeParent");
    }

    function onMachineDocumentTreeBeginCallback(command) {
        command.callbackUrl =
            controllerName + "AddMachineDocumentTreeList" + "?MachineTopParentId=" + state.topMachineId;
    }

    function onMachineTreeBeginCallback(command) {
        command.callbackUrl =
            controllerName + "AddSubMachineTreeList" + "?MachineTopParentId=" + state.topMachineId;
    }

    function onMachineSparePartTreeBeginCallback(command) {
        command.callbackUrl =
            controllerName + "AddSparePartSubMachineTreeList" + "?MachineTopParentId=" + state.topMachineId;
    }

    async function refreshAddMachineDocumentTree() {
        const url = controllerName + "AddMachineDocumentTreeList";

        const params = {
            machineTopParentId: state.topMachineId
        };

        const treeList = await motorsazanClient.connector.post(url, params);
        dom.addMachineDocumentTreeParent.html(treeList);

        addSubMachineDocumentTreeSetDom();
    }

    async function refreshAddSubMachineTree() {
        const url = controllerName + "AddSubMachineTreeList";

        const params = {
            machineTopParentId: state.topMachineId
        };

        const treeList = await motorsazanClient.connector.post(url, params);
        dom.addSubMachineTreeParent.html(treeList);

        addSubMachineTreeSetDom();
    }

    async function refreshMachineDocumentGrid() {
        const url = controllerName + "GetMachineDocumentListByMachineId";

        const machineId = state.documentNodeKey || 0;

        const apiParam = {
            machineId: machineId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.machineDocumentGridParent.html(gridHtml);

        addSubMachineDocumentTreeSetDom();
    }

    async function refreshMachineLocationTable() {
        const url = controllerName + "MachineLocationTable";

        const params = {
            machineId: state.topMachineId
        };

        const table = await motorsazanClient.connector.post(url, params);
        dom.machinesLocationTableParent.html(table);

        addMachineToLocationSetDom();
    }

    async function refreshMachineSparePartDocumentGrid() {
        const url = controllerName + "GetMachineSparePartDocumentList";

        const machineSparePartId = state.machineSparePartId ? state.machineSparePartId : 0;

        const apiParam = {
            machineSparePartId: machineSparePartId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.machineSparePartDocumentGridParent.html(gridHtml);

        addMachineSparePartDocumentSetDom();
    }

    async function refreshMachineSparePartDataGrid() {
        const url = controllerName + "GetMachineSparePartList";

        const apiParam = {
            machineId: state.sparePartNodeKey
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.machineSparePartDataGridParent.html(gridHtml);

        addMachineSparePartSetDom();
    }

    function removeMachineLocationPopup() {
        const url = controllerName + "RemoveAssignMachineToEmployee";

        const machineLocationId = $("#MachineLocationId").val();

        const apiParam = {
            machineLocationId: machineLocationId
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (removeResult) {
                addMachineToLocationSetDom();

                refreshMachineLocationTable();

                motorsazanClient.messageModal.success(removeResult);
            });
    }

    async function removeMachineDocumentPopup(machineDocumentId, fileName) {
        const fileDeleteUrl = "/Uploader/DeleteFiles";

        let params = {
            fileNames: fileName
        };

        await motorsazanClient.connector.post(fileDeleteUrl, params);

        const url = controllerName + "RemoveMachineDocumentByMachineDocumentId";
        params = {
            machineDocumentId
        };

        const result = await motorsazanClient.connector.post(url, params);

        await refreshMachineDocumentGrid();
        motorsazanClient.messageModal.success(result);
    }

    async function removeSparePartPopup(values) {
        const url = controllerName + "RemoveMachineSparePartByMachineSparePartId";
        const params = {
            machineSparePartId: values
        };

        const result = await motorsazanClient.connector.post(url, params);

        await refreshMachineSparePartDataGrid();
        motorsazanClient.messageModal.success(result);
    }

    async function removeSparePartDocumentPopup(machineSparePartDocumentId, fileName) {
        const fileDeleteUrl = "/Uploader/DeleteFiles";

        let params = {
            fileNames: fileName
        };

        await motorsazanClient.connector.post(fileDeleteUrl, params);

        const url = controllerName + "RemoveMachineSparePartDocumentByMachineSparePartDocumentId";
        params = {
            machineSparePartDocumentId
        };

        const result = await motorsazanClient.connector.post(url, params);

        await refreshMachineSparePartDocumentGrid();
        motorsazanClient.messageModal.success(result);
    }

    function removeUploadedAttachments() {
        state.uploadedFileNames = null;
    }

    function removeUploadedAttachmentsOfMachineSparePart() {
        state.uploadedFileNamesSparePart = null;
    }

    function resetAddMachineDocumentForm() {
        state.uploadedFileNames = null;

        tools.hideItem(dom.machineAttachmentUploaderError);
        tools.hideItem(dom.addDocumentTypeListComboError);
        tools.hideItem(dom.addMachineDocumentTreeError);

        dom.addDocumentTypeListCombo.SetSelectedIndex(-1);

        refreshAddMachineDocumentTree();

        //Clear uploader without deleting uploaded files
        state.uploaderReference.clear(false);

        state.uploaderReference.enable();
    }

    function resetAddMachineSparePartDocumentForm() {
        state.uploadedFileNamesSparePart = null;

        dom.addMachineSparePartDocumentElectricalSpecificationTextBox.SetText("");
        dom.addMachineSparePartDocumentMechanicalSpecificationTextBox.SetText("");
        dom.addMachineSparePartDocumentMadeInCompanyTextBox.SetText("");
        dom.addMachineSparePartDocumentDescription.SetText("");

        tools.hideItem(dom.addMachineSparePartDocumentElectricalSpecificationTextBoxError);
        tools.hideItem(dom.addMachineSparePartDocumentMechanicalSpecificationTextBoxError);
        tools.hideItem(dom.addMachineSparePartDocumentMadeInCompanyTextBoxError);
        tools.hideItem(dom.addMachineSparePartDocumentDescriptionError);

        //Clear uploader without deleting uploaded files
        state.uploaderReferenceSparePart.clear(false);
    }

    function resetAddSubMachineForm() {
        tools.hideItem(dom.addSubMachineTreeError);
        tools.hideItem(dom.addSubMachineNameError);

        dom.addSubMachineName.SetText();

        refreshAddSubMachineTree();
    }

    function resetAssignMachineToEmployeeForm() {
        tools.hideItem(dom.addFormEmployeeComboError);
        tools.hideItem(dom.addFormSubDepartmentListComboError);
        tools.hideItem(dom.addFormDepartmentListComboError);

        dom.addFormEmployeeCombo.SetSelectedIndex(-1);
        dom.addFormSubDepartmentListCombo.SetSelectedIndex(-1);
        dom.addFormDepartmentListCombo.SetSelectedIndex(-1);

        refreshMachineLocationTable();
    }

    function resetMachineSparePartForm() {
        addMachineSparePartSetDom();

        dom.sundryModalStockComboByRack.SetSelectedIndex(-1);
        tools.hideItem(dom.addFormStockComboError);

        dom.addStocksImportanceCombo.SetSelectedIndex(-1);
        tools.hideItem(dom.addStocksImportanceComboError);


        dom.addStockSupplyTypeCombo.SetSelectedIndex(-1);
        tools.hideItem(dom.addStockSupplyTypeComboError);

        dom.addStocksDate.val("");
        tools.hideItem(dom.addStocksDateError);

        dom.addStockTechnicalCharacteristicsCatalog.SetText("");
        tools.hideItem(dom.addStockTechnicalCharacteristicsCatalogError);
    }

    async function registerElectricalInfo() {
        if (!isRegisterElectricalInfoValid())
            return;

        const url = controllerName + "AddMachineElectricalInfo";

        const topMachineId = state.topMachineId;

        const apiParam = {
            Characteristic: dom.addFormCharacteristicCombo.GetValue(),
            Producer: dom.addFormProducerTextBox.GetValue(),
            Power: dom.addFormPowerTextBox.GetValue(),
            Rpm: dom.addFormRpmCombo.GetValue(),
            Voltage: dom.addFormVoltageCombo.GetValue(),
            current: dom.addFormcurrentTextBox.GetValue(),
            CurrentType: dom.addFormCurrentTypeCombo.GetValue(),
            machineId: topMachineId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        refreshElectricalGrid();
        motorsazanClient.messageModal.success(apiResult);
        setDom();
        resetAddMachineElectricalInfo();
    }


    function resetAddMachineElectricalInfo() {
        dom.addFormCharacteristicCombo.SetSelectedIndex(-1);
        dom.addFormProducerTextBox.SetText('');
        dom.addFormPowerTextBox.SetText('');
        dom.addFormRpmCombo.SetSelectedIndex(-1);
        dom.addFormVoltageCombo.SetSelectedIndex(-1);
        dom.addFormcurrentTextBox.SetText('');
        dom.addFormCurrentTypeCombo.SetSelectedIndex(-1);
    }

    function resetOilAndLubricantInfo() {
        dom.addFormOilUnitCombo.SetSelectedIndex(-1);
        dom.addFormPreventiveItemTextBox.SetText('');
        dom.addFormOilNameTextBox.SetText('');
        dom.addFormManufacturerTextBox.SetText('');
        dom.addFormTankVolumeTextBox.SetText('');
        dom.addFormPeriodTextBox.SetText('');
        dom.addFormYearlyUsageTextBox.SetText('');
        dom.addFormStandardVolumeTextBox.SetText('');
    }


    async function registerMachineBuiltInfo() {
        if (!isUpdateMachineBuiltInfoValid())
            return;

        const url = controllerName + "EditMachineBuiltInfo";

        const topMachineId = state.topMachineId;

        const apiParam = {
            BuiltYear: dom.addFormMachineBuiltYearTextBox.GetValue(),
            CountryName: dom.addFormProducerCountryNameTextBox.GetValue(),
            ImportPersianDate: dom.addFormImportDate.val(),
            machineId: topMachineId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        setDom();
        motorsazanClient.messageModal.success(apiResult);
        showMachineProducerTab();
    }

    
    async function registerOilAndLubrication() {
        if (!isRegisterOilAndLubricationValid())
            return;

        const url = controllerName + "RegisterOilAndLubrication";

        const topMachineId = state.topMachineId;

        const apiParam = {
            predictiveItemTitle: dom.addFormPreventiveItemTextBox.GetValue(),
            MaterialName: dom.addFormOilNameTextBox.GetValue(),
            MaterialProducer: dom.addFormManufacturerTextBox.GetValue(),
            TankVolume: dom.addFormTankVolumeTextBox.GetValue(),
            Unit: dom.addFormOilUnitCombo.GetValue(),
            StandardValue: dom.addFormStandardVolumeTextBox.GetValue(),
            Period: dom.addFormPeriodTextBox.GetValue(),
            AnnualUsage: dom.addFormYearlyUsageTextBox.GetValue(),
            machineId: topMachineId
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        setDom();
        refreshOilUsageGrid();
        resetOilAndLubricantInfo();
        motorsazanClient.messageModal.success(apiResult);
    }


    function setDom() {
        //Grid
        dom.machineManagementGridParent = $("#machineManagementGridParent");
        dom.machinesGrid = ASPxClientGridView.Cast("machinesGrid");


        dom.filterDepartmentHasMachineCombo = ASPxClientComboBox.Cast("filterDepartmentHasMachineCombo");
        dom.filterDepartmentHasMachineComboError = $("#filterDepartmentHasMachineComboError");
        dom.filterDepartmentHasMachineComboParent = $("#filterDepartmentHasMachineComboParent");

        dom.filterSubDepartmentHasMachineCombo = ASPxClientComboBox.Cast("filterSubDepartmentHasMachineCombo");
        dom.filterSubDepartmentHasMachineComboError = $("#filterSubDepartmentHasMachineComboError");
        dom.filterSubDepartmentHasMachineComboParent = $("#filterSubDepartmentHasMachineComboParent");

        dom.filterFormMachineListBtn = ASPxClientButton.Cast("filterFormMachineListBtn");


        dom.oilTabBtn = $("#oilTabBtn");
        dom.electricalTabBtn = $("#electricalTabBtn");
        dom.machineProducerTabBtn = $("#machineProducerTabBtn");
        dom.baseinfoTabsContentWrapper = $("#baseinfoTabsContentWrapper");

        dom.machineElectricalDataGrid = ASPxClientGridView.Cast("machineElectricalDataGrid");
        dom.elctircalGridParent = $("#elctircalGridParent");

        dom.oilGridParent = $("#oilGridParent");
        dom.oilGrid = ASPxClientGridView.Cast("oilGrid");


        dom.addFormCharacteristicCombo = ASPxClientComboBox.Cast("addFormCharacteristicCombo");
        dom.addFormCharacteristicComboError = $("#addFormCharacteristicComboError");
        dom.addFormProducerTextBox = ASPxClientTextBox.Cast("addFormProducerTextBox");
        dom.addFormProducerTextBoxError = $("#addFormProducerTextBoxError");
        dom.addFormPowerTextBox = ASPxClientTextBox.Cast("addFormPowerTextBox");
        dom.addFormPowerTextBoxError = $("#addFormPowerTextBoxError");
        dom.addFormRpmCombo = ASPxClientComboBox.Cast("addFormRpmCombo");
        dom.addFormRpmComboError = $("#addFormRpmComboError");
        dom.addFormVoltageCombo = ASPxClientComboBox.Cast("addFormVoltageCombo");
        dom.addFormVoltageComboError = $("#addFormVoltageComboError");
        dom.addFormcurrentTextBox = ASPxClientTextBox.Cast("addFormcurrentTextBox");
        dom.addFormcurrentTextBoxError = $("#addFormcurrentTextBoxError");
        dom.addFormCurrentTypeCombo = ASPxClientComboBox.Cast("addFormCurrentTypeCombo");
        dom.addFormCurrentTypeComboError = $("#addFormCurrentTypeComboError");


        dom.addFormImportDate = $("#addFormImportDate");
        dom.addFormImportDateError = $("#addFormImportDateError");
        dom.addFormMachineBuiltYearTextBox = ASPxClientTextBox.Cast("addFormMachineBuiltYearTextBox");
        dom.addFormMachineBuiltYearTextBoxError = $("#addFormMachineBuiltYearTextBoxError");
        dom.addFormProducerCountryNameTextBox = ASPxClientTextBox.Cast("addFormProducerCountryNameTextBox");
        dom.addFormProducerCountryNameTextBoxError = $("#addFormProducerCountryNameTextBoxError");


        dom.addFormPreventiveItemTextBox = ASPxClientTextBox.Cast("addFormPreventiveItemTextBox");
        dom.addFormPreventiveItemTextBoxBoxError = $("#addFormPreventiveItemTextBoxBoxError");

        dom.addFormOilNameTextBox = ASPxClientTextBox.Cast("addFormOilNameTextBox");
        dom.addFormOilNameTextBoxError = $("#addFormOilNameTextBoxError");

        dom.addFormManufacturerTextBox = ASPxClientTextBox.Cast("addFormManufacturerTextBox");
        dom.addFormManufacturerTextBoxError = $("#addFormManufacturerTextBoxError");

        dom.addFormTankVolumeTextBox = ASPxClientTextBox.Cast("addFormTankVolumeTextBox");
        dom.addFormTankVolumeBoxError = $("#addFormTankVolumeBoxError");

        dom.addFormOilUnitCombo = ASPxClientComboBox.Cast("addFormOilUnitCombo");
        dom.addFormOilUnitComboError = $("#addFormOilUnitComboError");

        dom.addFormStandardVolumeTextBox = ASPxClientTextBox.Cast("addFormStandardVolumeTextBox");
        dom.addFormStandardVolumeTextBoxError = $("#addFormStandardVolumeTextBoxError");

        dom.addFormPeriodTextBox = ASPxClientTextBox.Cast("addFormPeriodTextBox");
        dom.addFormPeriodTextBoxError = $("#addFormPeriodTextBoxError");

        dom.addFormYearlyUsageTextBox = ASPxClientTextBox.Cast("addFormYearlyUsageTextBox");
        dom.addFormYearlyUsageTextBoxError = $("#addFormYearlyUsageTextBoxError");


    }


    function setEvents() {
        dom.addFormImportDate.change(handleFilterFormPeriodStartDatePickerSelectionChange);

    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.addFormImportDateError);
    }

    function setMachineManagementGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);

        const activeIndex = event.visibleIndex;
        state.topMachineId = dom.machinesGrid.GetRowKey(activeIndex);
    }

    function showRemoveMachineSparePartConfirmation() {
        addMachineSparePartSetDom();

        dom.machineSparePartDataGrid.GetRowValues(dom.machineSparePartDataGrid.GetFocusedRowIndex(),
            "MachineSparePartId",
            (values) => {
                motorsazanClient.messageModal.confirm("آیا از حذف این ردیف مطمئن هستید؟",
                    () => removeSparePartPopup(values),
                    "تاییدیه حذف");
            });
    }

    function showRemoveMachineDocumentConfirmation() {
        addSubMachineDocumentTreeSetDom();

        dom.machineDocumentGrid.GetRowValues(dom.machineDocumentGrid.GetFocusedRowIndex(),
            "MachineDocumentId;FileName",
            (values) => {
                const [machineDocumentId, fileName] = values;

                motorsazanClient.messageModal.confirm("آیا از حذف این فایل مطمئن هستید؟",
                    () => removeMachineDocumentPopup(machineDocumentId, fileName),
                    "تاییدیه حذف");
            });
    }

    function showRemoveMachineSparePartDocumentConfirmation() {
        addMachineSparePartDocumentSetDom();

        dom.machineSparePartDocumentGrid.GetRowValues(dom.machineSparePartDocumentGrid.GetFocusedRowIndex(),
            "MachineSparePartDocumentId;FileName",
            (values) => {
                const [machineSparePartDocumentId, fileName] = values;
                motorsazanClient.messageModal.confirm("آیا از حذف این فایل مطمئن هستید؟",
                    () => removeSparePartDocumentPopup(machineSparePartDocumentId, fileName),
                    "تاییدیه حذف");
            });
    }

    function showUploadFileToSpare() {
        const url = controllerName + "AddDocumentToSparePart";

        const activeIndex = dom.machineSparePartDataGrid.GetFocusedRowIndex();
        const machineSparePartId = dom.machineSparePartDataGrid.GetRowKey(activeIndex);
        state.machineSparePartId = machineSparePartId;

        const machineId = dom.addSparePartSubMachineTree.GetFocusedNodeKey();
        state.sparePartNodeKey = machineId;

        const topMachineId = state.topMachineId;

        const apiParam = {
            topMachineId: topMachineId,
            machineId: machineId,
            machineSparePartId: machineSparePartId
        };

        const title = "ثبت ضمائم و پرونده های قطعات یدکی";
        motorsazanClient.contentModal.ajaxShow(title, url, apiParam, addDocumentToSparePartInit, false, true);
    }

    async function uploadAttachments(fileNames) {
        if (state.uploadedFileNames && state.uploadedFileNames.length > 0) {
            state.uploadedFileNames.push(...fileNames);
        } else {
            state.uploadedFileNames = fileNames;
        }

        //For use in deleting files
        state.uploaderReference.saveNameOfUploadedFiles(state.uploadedFileNames);

        tools.hideItem(dom.machineAttachmentUploaderError);
    }

    async function uploadAttachmentsSparePart(fileNames) {
        if (state.uploadedFileNamesSparePart && state.uploadedFileNamesSparePart.length > 0) {
            state.uploadedFileNamesSparePart.push(...fileNames);
        } else {
            state.uploadedFileNamesSparePart = fileNames;
        }

        //For use in deleting files
        state.uploaderReferenceSparePart.saveNameOfUploadedFiles(state.uploadedFileNamesSparePart);

        tools.hideItem(dom.machineSparePartAttachmentUploaderError);
    }

    async function handleFilterDepartmentHasMachineComboSelectedIndexChanged() {
        const url = controllerName + "FilterSubDepartmentHasMachineCombo";

        tools.hideItem(dom.filterDepartmentHasMachineComboError);
        
        const departmentId = dom.filterDepartmentHasMachineCombo.GetValue();
        if (departmentId !== 0) {
            dom.filterSubDepartmentHasMachineCombo.SetEnabled(true);

            const apiParam = {
                departmentId: departmentId
            };
            const subDepartmentListCombo = await motorsazanClient.connector.post(url, apiParam);
            dom.filterSubDepartmentHasMachineComboParent.html(subDepartmentListCombo);
            setDom();

        }
        else
            dom.filterSubDepartmentHasMachineCombo.SetEnabled(false);

       

    }

    function handleFilterDepartmentHasMachineComboBeginCallback(command) {
        const url = controllerName + "FilterDepartmentHasMachineCombo";

        command.callbackUrl = url;
    }

    function handleFilterSubDepartmentHasMachineComboSelectedIndexChanged() {
        tools.hideItem(dom.filterSubDepartmentHasMachineComboError);
    }

    function handleFilterSubDepartmentHasMachineComboBeginCallback(command) {
        const departmentId = dom.filterDepartmentHasMachineCombo.GetValue();

        command.callbackUrl =
            controllerName + "filterSubDepartmentHasMachineCombo" + "?DepartmentId=" + departmentId;
    }

    function isFilterFormValid() {
        var isValid = true;

        tools.hideItem(dom.filterDepartmentHasMachineComboError);

        const department = dom.filterDepartmentHasMachineCombo.GetValue();
        const isDepartmentSelected = !tools.isNullOrEmpty(department);
        if (!isDepartmentSelected) {
            isValid = false;
            tools.showItem(dom.filterDepartmentHasMachineComboError);
        }
        if (department !== 0) {
            tools.hideItem(dom.filterSubDepartmentHasMachineComboError);

            const subDepartment = dom.filterSubDepartmentHasMachineCombo.GetValue();
            const isSubDepartmentSelected = !tools.isNullOrEmpty(subDepartment);
            if (!isSubDepartmentSelected) {
                isValid = false;
                tools.showItem(dom.filterSubDepartmentHasMachineComboError);
            }
        }


        return isValid;

    }

    function filterMachineList() {
        const url = "/MachineManagement/MachineManagementGrid";
        const canContinue = isFilterFormValid();
        if (!canContinue) return false;

        const topDepartment = dom.filterDepartmentHasMachineCombo.GetValue();
        state.selectedDepartmentId = topDepartment;
        if (topDepartment != 0) {
            state.selectedDepartmentId = dom.filterSubDepartmentHasMachineCombo.GetValue();
        }



        const apiParam = {
            departmentId: state.selectedDepartmentId

        };
        motorsazanClient.connector.post(url, apiParam)
            .then(function (gridSource) {
                dom.machineManagementGridParent.html(gridSource);
                setDom();
            });
    }

    $(document).ready(init);

    return {
        fillMachineManagementGrid: fillMachineManagementGrid,
        getMachineDocumentTreeSelectedNode: getMachineDocumentTreeSelectedNode,
        getMachineSparePartTreeSelectedNode: getMachineSparePartTreeSelectedNode,
        getTreeSelectedNode: getTreeSelectedNode,
        handleAddDocumentTypeListComboSelectedIndexChanged: handleAddDocumentTypeListComboSelectedIndexChanged,
        handleAddFormDepartmentListComboBeginCallback: handleAddFormDepartmentListComboBeginCallback,
        handleAddFormDepartmentListComboSelectedIndexChanged: handleAddFormDepartmentListComboSelectedIndexChanged,
        handleAddFormEmployeeComboBeginCallback: handleAddFormEmployeeComboBeginCallback,
        handleAddFormEmployeeComboSelectedIndexChanged: handleAddFormEmployeeComboSelectedIndexChanged,
        handleAddFormMachineToLocationSaveBtnClick: handleAddFormMachineToLocationSaveBtnClick,
        handleAddFormStoreListForSparePartComboSelectedIndexChanged: handleAddFormStoreListForSparePartComboSelectedIndexChanged,
        handleAddFormSubDepartmentListComboBeginCallback: handleAddFormSubDepartmentListComboBeginCallback,
        handleAddFormSubDepartmentListComboSelectedIndexChanged: handleAddFormSubDepartmentListComboSelectedIndexChanged,
        handleAddMachineDocumentBtnClick: handleAddMachineDocumentBtnClick,
        handleAddMachineSparePartBtnClick: handleAddMachineSparePartBtnClick,
        handleAddMachineBaseInfoBtnClick: handleAddMachineBaseInfoBtnClick,
        handleAddMachineSparePartDocumentElectricalSpecificationTextBoxValueChanged: handleAddMachineSparePartDocumentElectricalSpecificationTextBoxValueChanged,
        handleAddMachineSparePartDocumentDescriptionValueChanged: handleAddMachineSparePartDocumentDescriptionValueChanged,
        handleAddMachineSparePartDocumentMadeInCompanyTextBoxValueChanged: handleAddMachineSparePartDocumentMadeInCompanyTextBoxValueChanged,
        handleAddMachineSparePartDocumentMechanicalSpecificationTextBoxValueChanged: handleAddMachineSparePartDocumentMechanicalSpecificationTextBoxValueChanged,
        handleAddMachineSparePartDocumentSaveBtnClick: handleAddMachineSparePartDocumentSaveBtnClick,
        handleAddMachineToLineBtnClick: handleAddMachineToLineBtnClick,
        handleAddStocksDateSelectionChange: handleAddStocksDateSelectionChange,
        handleAddStocksImportanceComboSelectedIndexChanged: handleAddStocksImportanceComboSelectedIndexChanged,
        handleAddStockSupplyTypeComboSelectedIndexChanged: handleAddStockSupplyTypeComboSelectedIndexChanged,
        handleAddStockTechnicalCharacteristicsCatalogValueChanged: handleAddStockTechnicalCharacteristicsCatalogValueChanged,
        handleAddSubMachineBtnClick: handleAddSubMachineBtnClick,
        handleAddSubMachineDocumentSaveBtnClick: handleAddSubMachineDocumentSaveBtnClick,
        handleAddSubMachineNameTextBoxValueChanged: handleAddSubMachineNameTextBoxValueChanged,
        handleAddSubMachineSaveBtnClick: handleAddSubMachineSaveBtnClick,
        handleAddSubMachineSparePartSaveBtnClick: handleAddSubMachineSparePartSaveBtnClick,
        handleMachineDocumentGridBeginCallback: handleMachineDocumentGridBeginCallback,
        handleMachineDocumentGridCustomBtnClick: handleMachineDocumentGridCustomBtnClick,
        handleMachineLocationRemoveButtonClick: handleMachineLocationRemoveButtonClick,
        handleMachineManagementGridBeginCallback: handleMachineManagementGridBeginCallback,
        handleMachineManagementTreeBeginCallback: handleMachineManagementTreeBeginCallback,
        handleMachineSparePartDataGridBeginCallback: handleMachineSparePartDataGridBeginCallback,
        handleMachineSparePartDocumentGridBeginCallback: handleMachineSparePartDocumentGridBeginCallback,
        handleShowMachineAndSubMachineBtnClick: handleShowMachineAndSubMachineBtnClick,
        handleSparePartDocumentGridCustomBtnClick: handleSparePartDocumentGridCustomBtnClick,
        handleSparePartGridCustomBtnClick: handleSparePartGridCustomBtnClick,
        handleSundryModalStockComboByRackSelectionChange: handleSundryModalStockComboByRackSelectionChange,
        onMachineDocumentTreeBeginCallback: onMachineDocumentTreeBeginCallback,
        onMachineSparePartTreeBeginCallback: onMachineSparePartTreeBeginCallback,
        onMachineTreeBeginCallback: onMachineTreeBeginCallback,
        setMachineManagementGridFocusedRowOnExpanding: setMachineManagementGridFocusedRowOnExpanding,
        handleFilterDepartmentHasMachineComboSelectedIndexChanged: handleFilterDepartmentHasMachineComboSelectedIndexChanged,
        handleFilterDepartmentHasMachineComboBeginCallback: handleFilterDepartmentHasMachineComboBeginCallback,
        handleFilterSubDepartmentHasMachineComboSelectedIndexChanged: handleFilterSubDepartmentHasMachineComboSelectedIndexChanged,
        handleFilterSubDepartmentHasMachineComboBeginCallback: handleFilterSubDepartmentHasMachineComboBeginCallback,
        filterMachineList: filterMachineList,
        handleMachineElectricalGridCustomBtnClick: handleMachineElectricalGridCustomBtnClick,
        handleMachineOilUsageGridCustomBtnClick: handleMachineOilUsageGridCustomBtnClick,
        handleMachineOilUsageGridBeginCallback: handleMachineOilUsageGridBeginCallback,
        handleMachineElectricalDataGridBeginCallback: handleMachineElectricalDataGridBeginCallback,
        registerElectricalInfo: registerElectricalInfo,
        registerMachineBuiltInfo: registerMachineBuiltInfo,
        registerOilAndLubrication: registerOilAndLubrication,
        handleAddFormPreventiveItemTextBoxValueChanged: handleAddFormPreventiveItemTextBoxValueChanged,
        handleAddFormOilNameTextBoxValueChanged: handleAddFormOilNameTextBoxValueChanged,
        handleAddFormManufacturerTextBoxValueChanged: handleAddFormManufacturerTextBoxValueChanged,
        handleAddFormTankVolumeTextBoxValueChanged: handleAddFormTankVolumeTextBoxValueChanged,
        handleAddFormOilUnitComboChanged: handleAddFormOilUnitComboChanged,
        handleAddFormStandardVolumeTextBoxValueChanged: handleAddFormStandardVolumeTextBoxValueChanged,
        handleAddFormPeriodTextBoxValueChanged: handleAddFormPeriodTextBoxValueChanged,
        handleAddFormYearlyUsageTextBoxValueChanged: handleAddFormYearlyUsageTextBoxValueChanged,
        handleAddFormCharacteristicComboChanged: handleAddFormCharacteristicComboChanged,
        handleAddFormProducerTextBoxValueChanged: handleAddFormProducerTextBoxValueChanged,
        handleAddFormPowerTextBoxBoxValueChanged: handleAddFormPowerTextBoxBoxValueChanged,
        handleAddCurrentValueChanged: handleAddCurrentValueChanged,
        handleAddCurrentTypeValueChanged: handleAddCurrentTypeValueChanged,
        handleAddFormRpmComboValueChanged: handleAddFormRpmComboValueChanged,
        handleAddFormVoltageComboValueChanged: handleAddFormVoltageComboValueChanged,
        handleAddFormProducerCountryNameTextBoxChanged: handleAddFormProducerCountryNameTextBoxChanged,
        handleAddFormMachineBuiltYearTextBoxChanged: handleAddFormMachineBuiltYearTextBoxChanged,
        handlePrintMachineInfoBtnClick: handlePrintMachineInfoBtnClick

    };
    function handlePrintMachineInfoBtnClick() {
        return window.open("/print/PrintMachineBuiltInfoByMachineId?machineId=" + state.topMachineId);

    }

})();