/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/DevExpressIntellisense/devexpress-aspnetcore-bootstrap.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.inspection = (function () {

    var dom = {};
    var state = {
        specialDateTypeFilter: 'CustomPeriod',
        GridFocusedRowIndex: 0,
        GridMemberFocusedRowIndex: 0,
        GroupId: 0,
        InspectionId: 0,
        statusId: 0,
        dateType: 4,
        startDate: '',
        endDate: '',
        workorderType: 0,
        machineId: null,
        workorderId: 0,
        inspectionDetailId: 0,
        actionName: '',
        nodeSelected: null,
        editDepartment: null,
        editSubDepartment: null,
        editMachine: null,
        personHou: 0

    };
    var tools = motorsazanClient.tools;
    var controllerName = "/Inspection/";

    function addInspectionDetail(s, e) {
        dom.inspectionSourceGird.GetRowValues(dom.inspectionSourceGird.GetFocusedRowIndex(), "InspectionId", registerInspectionDetail);
    }

    function registerInspectionDetail(values) {
        const url = controllerName + "AddInspectionDetailPartial";
        const params = {
            InspectionId: values
        };
        state.gridMemberFocusedRowIndex = values;
        const title = "ثبت جزئیات بازرسی";
        motorsazanClient.contentModal.ajaxShow(title, url, params, setDom);


    }

    async function addInspection() {
        var canContinue = isAddFormValid();
        const url = "/Inspection/AddInspection";
        if (!canContinue) return false;
        if (!state.workorderType) {

            const apiParam = {
                InspectionTypeId: dom.addFormWorkOrderList.GetValue(),
                IsProductive: state.workorderType,
                DepartmentId: dom.addFormSubDepartmentCombo.GetValue(),
                MaintenanceGroupId: dom.addFormMaintenanceGroupListCombo.GetValue(),
                PreferDate: dom.addFormPreferDate.val(),
                PersonHour: dom.addFormNeedTime.GetValue(),
                Description: dom.addFormFaultDescription.GetValue(),
            };

            const apiResult = await motorsazanClient.connector.post(url, apiParam);
            filterInspectionList();
            motorsazanClient.messageModal.success(apiResult);
            resetAddForm();

        } else if (state.workorderType) {

            const apiParam = {
                InspectionTypeId: dom.addFormWorkOrderList.GetValue(),
                IsProductive: state.workorderType,
                DepartmentId: dom.addFormGetProdcutviceSubDepartmentListCombo.GetValue(),
                MachineId: dom.addFormMachineCombo.GetValue(),
                MaintenanceGroupId: dom.addFormMaintenanceGroupListCombo.GetValue(),
                PreferDate: dom.addFormPreferDate.val(),
                PersonHour: dom.addFormNeedTime.GetValue(),
                Description: dom.addFormFaultDescription.GetValue()
            };

            const apiResult = await motorsazanClient.connector.post(url, apiParam);
            setEditDom();
            filterInspectionList();
            motorsazanClient.messageModal.success(apiResult);
            resetAddForm();
        }
    }

    async function addInceptionDetail() {

        const canContinue = isAddDetailFormValid();
        const url = "/Inspection/AddInspectionDetailToInspection";
        if (!canContinue) return false;

        const apiParam = {
            InspectionId: state.gridMemberFocusedRowIndex,
            ActionName: dom.addInceptionDetailDescription.GetValue(),
            PersonHour: dom.addInceptionDetailNeedTime.GetValue()
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        filterInspectionDetailList();
        motorsazanClient.messageModal.success(apiResult);
        setDom();
        resetAddDeatilForm();
        state.nodeSelected = dom.inspectionSourceGird.GetFocusedRowIndex();

        refreshGrid();

        dom.inspectionSourceGird.ExpandRow(state.nodeSelected);

    }

    async function changeToWorkOrder() {

        const apiParam = {
            InspectionId: state.InspectionId
        };
        const url = "/Inspection/changeToWorkOrder";
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        refreshGrid();
        motorsazanClient.messageModal.hide();
        motorsazanClient.messageModal.success(apiResult);

    }

    function changeInspectionToWorkOrder() {
        const title = "تبدیل به سفارشکار";
        const content = "آیا از تبدیل این برگ به سفارشکار مطمئن هستید؟";

        motorsazanClient.messageModal.confirm(content, changeToWorkOrder, title);

    }

    async function editTheInspection() {
        const canContinue = isEditFormValid();
        if (!canContinue) return false;
        const url = "/Inspection/EditInspection";
        if (!state.workorderType) {

            const apiParam = {
                InspectionId: state.InspectionId,
                InspectionTypeId: dom.editFormWorkOrderList.GetValue(),
                MaintenanceGroupId: dom.editFormMaintenanceGroupListCombo.GetValue(),
                DepartmentId: state.editSubDepartment,
                Description: dom.editFormFaultDescription.GetValue(),
                PersonHour: dom.editFormNeedTime.GetValue(),
                PreferDate: dom.editFormPreferDate.val(),
                IsProductive: state.workorderType
            };

            const apiResult = await motorsazanClient.connector.post(url, apiParam);
            setEditDom();
            filterInspectionList();
            motorsazanClient.messageModal.success(apiResult);
            motorsazanClient.messageModal.hide();
            resetEditForm();
            motorsazanClient.contentModal.hide();

        } else if (state.workorderType) {

            const apiParam = {
                InspectionId: state.InspectionId,
                InspectionTypeId: dom.editFormWorkOrderList.GetValue(),
                MaintenanceGroupId: dom.editFormMaintenanceGroupListCombo.GetValue(),
                DepartmentId: state.editSubDepartment,
                MachineId: state.editMachine,
                Description: dom.editFormFaultDescription.GetValue(),
                PersonHour: dom.editFormNeedTime.GetValue(),
                PreferDate: dom.editFormPreferDate.val(),
                IsProductive: state.workorderType
            };

            const apiResult = await motorsazanClient.connector.post(url, apiParam);
            filterInspectionList();
            setEditDom();
            motorsazanClient.messageModal.success(apiResult);
            motorsazanClient.messageModal.hide();
            resetEditForm();
            motorsazanClient.contentModal.hide();
        }
    }

    async function editInceptionDetail() {
        const canContinue = isEditDetailFormValid();
        if (!canContinue) return false;
        const url = "/Inspection/EditInspectionDetails";
        const apiParam = {
            InspectionDetailId: getInspectionDetailActiveRowKey(),
            ActionName: dom.editInceptionDetailDescription.GetValue(),
            PersonHour: dom.editInceptionDetailNeedTime.GetValue(),
        };

        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        motorsazanClient.messageModal.success(apiResult);
        motorsazanClient.messageModal.hide();
        motorsazanClient.contentModal.hide();
        refreshInspectionManagementGrid();
        resetEditDeatilForm();
    }

    function ebtalInspection() {
        var title = "درخواست ابطال";
        var content = "آیا از ابطال این سفارشکار مطمئن هستید؟";

        motorsazanClient.messageModal.confirm(content, RemoveWorkOrder, title);

    }

    async function editInspection(s, e) {
        const url = "/Inspection/EditInspectionModalPartial";
        motorsazanClient.contentModal.ajaxShow(" ویرایش", url, {}, initAddOrEditPlanningCommentModal);

    }

    function initAddOrEditPlanningCommentModal() {
        setEditDom();

        setDefaultValuesOfEditInspectiontModal();
    }

    async function setDefaultValuesOfEditInspectiontModal() {
        const url = "/Inspection/GetInspectionByInspectionId";
        const apiParam = {
            InspectionId: state.InspectionId
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);

        dom.editFormWorkOrderList.SetValue(apiResult.InspectionTypeId);
        if (apiResult.IsProductive) {
            dom.editWorkOrderTypeCombo.SetValue(1);
            handleEditFormWorkOrderTypeComboSelectedIndexChange();
            await fillProdcutviceDepartmentListCombo();
            dom.editFormGetProdcutviceDepartmentListCombo.SetValue(apiResult.ParentDepartmentId);

            await handleFilterFormProdcutviceTopDepartmentComboSelectedIndexChange();
            await fillProdcutviceSubDepartmentListCombo(apiResult.ParentDepartmentId);
            dom.editFormGetProdcutviceSubDepartmentListCombo.SetValue(apiResult.ChildDepartmentId);

            await handleEditFormProdcutviceSubDepartmentComboSelectedIndexChange();
            await fillMachineListCombo(apiResult.ChildDepartmentId);


            dom.editFormMachineCombo.SetText(apiResult.MachineName);

            tools.showItem(dom.editProductiveTopDepartmentsDiv);
            tools.showItem(dom.editproductiveSubDepartmentDiv);
            tools.showItem(dom.editproductiveMachiveDiv);
            tools.hideItem(dom.editnonProductiveSubDepartmentDiv);
            tools.hideItem(dom.editnonProductiveTopDepartmentDiv);


            state.editDepartment = apiResult.ParentDepartmentId;
            state.editSubDepartment = apiResult.ChildDepartmentId;
            state.editMachine = apiResult.MachineId;
        }
        else {
            tools.hideItem(dom.editProductiveTopDepartmentsDiv);
            tools.hideItem(dom.editproductiveSubDepartmentDiv);
            tools.hideItem(dom.editproductiveMachiveDiv);
            tools.showItem(dom.editnonProductiveSubDepartmentDiv);
            tools.showItem(dom.editnonProductiveTopDepartmentDiv);
            dom.editWorkOrderTypeCombo.SetValue(0);

            await fillNoneProdcutviceDepartmentListCombo();
            dom.editFormGetDepartmentListCombo.SetValue(apiResult.ParentDepartmentId);
            handleEditFormTopDepartmentComboSelectedIndexChange();

            await fillNoneProdcutviceSubDepartmentListCombo(apiResult.ParentDepartmentId);
            dom.editFormGetSubDepartmentListCombo.SetValue(apiResult.ChildDepartmentId);

            state.editDepartment = apiResult.ParentDepartmentId;
            state.editSubDepartment = apiResult.ChildDepartmentId;

        }

        dom.editFormMaintenanceGroupListCombo.SetValue(apiResult.MaintenanceGroupId);
        dom.editFormFaultDescription.SetText(apiResult.Description);
        
        dom.editFormNeedTime.SetText(apiResult.PersonHour);
        dom.editFormPreferDate.val(apiResult.PreferDate);
    }

    async function handleFilterFormProdcutviceTopDepartmentComboSelectedIndexChange() {
        tools.hideItem(dom.AddFormGetProdcutviceDepartmentListError);

        const url = "/Inspection/AddFormGetProdcutviceSubDepartmentListByMainDepartmentIdPartial";
        const apiParam = {
            departmentId: dom.addFormGetProdcutviceDepartmentListCombo.GetValue()
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.AddFormGetProdcutviceSubDepartmentListParent.html(apiResult);
        setDom();

        state.editDepartment = dom.addFormGetProdcutviceDepartmentListCombo.GetValue();
    }

    async function handleEditFormProdcutviceTopDepartmentComboSelectedIndexChange() {
        const url = "/Inspection/EditFormGetProdcutviceSubDepartmentListByMainDepartmentIdPartial";
        const apiParam = {
            departmentId: dom.editFormGetProdcutviceDepartmentListCombo.GetValue()
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.editFormGetProdcutviceSubDepartmentListParent.html(apiResult);
        setDom();

        state.editDepartment = dom.editFormGetProdcutviceDepartmentListCombo.GetValue();
    }

    async function fillProdcutviceDepartmentListCombo() {
        const url = controllerName + "EditFormGetProdcutviceAllMainDepartmentPartial";

        const comboHtml = await motorsazanClient.connector.post(url);

        dom.editFormGetProdcutviceDepartmentListParent.html(comboHtml);
        setEditDom();

    }

    async function fillProdcutviceSubDepartmentListCombo(subdepartmentId) {
        const url = controllerName + "EditFormGetProdcutviceSubDepartmentListByMainDepartmentIdPartial";
        const apiParam = {
            departmentId: subdepartmentId
        };

        const comboHtml = await motorsazanClient.connector.post(url, apiParam);
        dom.editFormGetProdcutviceSubDepartmentListParent.html(comboHtml);
        setEditDom();

    }

    async function fillNoneProdcutviceSubDepartmentListCombo(subdepartmentId) {
        const url = controllerName + "EditFormGetSubDepartmentListByMainDepartmentIdPartial";
        const apiParam = {
            departmentId: subdepartmentId
        };

        const comboHtml = await motorsazanClient.connector.post(url, apiParam);
        dom.editFormGetSubDepartmentListParent.html(comboHtml);
        setEditDom();

    }

    async function fillMachineListCombo(subdepartmentId) {
        const url = controllerName + "EditFormGetDepartmentMachineListPartial";
        const apiParam = {
            departmentId: subdepartmentId
        };

        const comboHtml = await motorsazanClient.connector.post(url, apiParam);
        dom.editFormMachineComboParent.html(comboHtml);
        setEditDom();

        state.editMachine = dom.editFormMachineCombo.GetValue();
    }

    async function fillNoneProdcutviceDepartmentListCombo() {
        const url = controllerName + "EditFormGetAllMainDepartmentPartial";

        const comboHtml = await motorsazanClient.connector.post(url);

        dom.editFormGetDepartmentListParent.html(comboHtml);
        setEditDom();

    }

    function isAddDetailFormValid() {
        var isAddValid = true;

        tools.hideItem(dom.addInceptionDetailDescriptionError);
        var detail = dom.addInceptionDetailDescription.GetValue();
        var isdetailValid = !tools.isNullOrEmpty(detail);
        if (!isdetailValid) {
            isAddValid = false;
            tools.showItem(dom.addInceptionDetailDescriptionError);
        }

        tools.hideItem(dom.addInceptionDetailNeedTimeError);
        var time = dom.addInceptionDetailNeedTime.GetValue();
        var istimeValid = !tools.isNullOrEmpty(time);
        if (!istimeValid) {
            isAddValid = false;
            tools.showItem(dom.addInceptionDetailNeedTimeError);
        }
        return isAddValid;

    }



    function isAddFormValid() {
        var isValid = true;

        tools.hideItem(dom.addFormWorKOrderListError);
        var workOrderList = dom.addFormWorkOrderList.GetValue();
        var isworkOrderListValid = !tools.isNullOrEmpty(workOrderList);
        if (!isworkOrderListValid) {
            isValid = false;
            tools.showItem(dom.addFormWorKOrderListError);
        }

        tools.hideItem(dom.addWorkOrderTypeError);
        var workOrderType = dom.addWorkOrderType.GetValue();
        var isworkOrderTypeValid = !tools.isNullOrEmpty(workOrderType);
        if (!isworkOrderTypeValid) {
            isValid = false;
            tools.showItem(dom.addWorkOrderTypeError);
        }

        tools.hideItem(dom.addFormMaintenanceGroupComboError);
        var maintenanceGroup = dom.addFormMaintenanceGroupListCombo.GetValue();
        var ismaintenanceGroupValid = !tools.isNullOrEmpty(maintenanceGroup);
        if (!ismaintenanceGroupValid) {
            isValid = false;
            tools.showItem(dom.addFormMaintenanceGroupComboError);
        }

        tools.hideItem(dom.addFormPreferDateError);
        var preferDate = dom.addFormPreferDate.val();
        var isValidpreferDate = tools.isValidPersianDate(preferDate);
        if (!isValidpreferDate) {
            tools.showItem(dom.addFormPreferDateError);
        }


        tools.hideItem(dom.addFormNeedTimeError);
        var needTime = dom.addFormNeedTime.GetValue();
        var isneedTimeValid = !tools.isNullOrEmpty(needTime);
        if (!isneedTimeValid) {
            isValid = false;
            tools.showItem(dom.addFormNeedTimeError);
        }

        tools.hideItem(dom.addFormFaultDescriptionError);
        var FaultDescription = dom.addFormFaultDescription.GetValue();
        var isFaultDescription = !tools.isNullOrEmpty(FaultDescription);
        if (!isFaultDescription) {
            isValid = false;
            tools.showItem(dom.addFormFaultDescriptionError);
        }

        if (state.workorderType) {

            tools.hideItem(dom.AddFormGetProdcutviceDepartmentListError);
            var productiveDepartment = dom.addFormGetProdcutviceDepartmentListCombo.GetValue();
            var isproductiveDepartmentValid = !tools.isNullOrEmpty(productiveDepartment);
            if (!isproductiveDepartmentValid) {
                isValid = false;
                tools.showItem(dom.AddFormGetProdcutviceDepartmentListError);
            }

            tools.hideItem(dom.AddFormGetProdcutviceSubDepartmentListoError);
            var productiveSubDepartment = dom.addFormGetProdcutviceSubDepartmentListCombo.GetValue();
            var isproductiveSubDepartmentValid = !tools.isNullOrEmpty(productiveSubDepartment);
            if (!isproductiveSubDepartmentValid) {
                isValid = false;
                tools.showItem(dom.AddFormGetProdcutviceSubDepartmentListoError);
            }

            tools.hideItem(dom.addFormMachineComboError);
            var MachineCombo = dom.addFormMachineCombo.GetValue();
            var isMachineComboValid = !tools.isNullOrEmpty(MachineCombo);
            if (!isMachineComboValid) {
                isValid = false;
                tools.showItem(dom.addFormMachineComboError);
            }
        }
        else {
            tools.hideItem(dom.AddFormGetDepartmentListError);
            var Department = dom.addFormGetDepartmentListCombo.GetValue();
            var isDepartmentValid = !tools.isNullOrEmpty(Department);
            if (!isDepartmentValid) {
                isValid = false;
                tools.showItem(dom.AddFormGetDepartmentListError);
            }

            tools.hideItem(dom.AddFormGetSubDepartmentListoError);
            var SubDepartment = dom.addFormSubDepartmentCombo.GetValue();
            var isSubDepartmentValid = !tools.isNullOrEmpty(SubDepartment);
            if (!isSubDepartmentValid) {
                isValid = false;
                tools.showItem(dom.AddFormGetSubDepartmentListoError);
            }

        }


        return isValid;
    }

    function isEditFormValid() {
        var isValid = true;

        tools.hideItem(dom.editFormWorKOrderListError);
        var workOrderList = dom.editFormWorkOrderList.GetValue();
        var isWorkOrderListValid = !tools.isNullOrEmpty(workOrderList);
        if (!isWorkOrderListValid) {
            isValid = false;
            tools.showItem(dom.editFormWorKOrderListError);
        }

        tools.hideItem(dom.editWorkOrderTypeError);
        var workOrderType = dom.editWorkOrderTypeCombo.GetValue();
        var isWorkOrderTypeValid = !tools.isNullOrEmpty(workOrderType);
        if (!isWorkOrderTypeValid) {
            isValid = false;
            tools.showItem(dom.editWorkOrderTypeError);
        }

        tools.hideItem(dom.editFormGetDepartmentListError);
        var department = state.editDepartment;
        var isDepartmentValid = !tools.isNullOrEmpty(department);
        if (!isDepartmentValid) {
            isValid = false;
            tools.showItem(dom.editFormGetDepartmentListError);
        }

        tools.hideItem(dom.editFormGetSubDepartmentListError);
        var subDepartment = state.editSubDepartment;
        var isSubDepartmentValid = !tools.isNullOrEmpty(subDepartment);
        if (!isSubDepartmentValid) {
            isValid = false;
            tools.showItem(dom.editFormGetSubDepartmentListError);
        }


        tools.hideItem(dom.editFormMaintenanceGroupComboError);
        var maintenanceGroup = dom.editFormMaintenanceGroupListCombo.GetValue();
        var isMaintenanceGroupValid = !tools.isNullOrEmpty(maintenanceGroup);
        if (!isMaintenanceGroupValid) {
            isValid = false;
            tools.showItem(dom.editFormMaintenanceGroupComboError);
        }

        tools.hideItem(dom.editFormPreferDateError);
        var preferredDate = dom.editFormPreferDate.val();
        var isPreferentialDate = tools.isValidPersianDate(preferredDate);
        if (!isPreferentialDate) {
            isValid = false;
            tools.showItem(dom.editFormPreferDateError);
        }


        tools.hideItem(dom.editFormNeedTimeError);
        var needTime = dom.editFormNeedTime.GetValue();
        var isNeedTimeValid = !tools.isNullOrEmpty(needTime);
        if (!isNeedTimeValid) {
            isValid = false;
            tools.showItem(dom.editFormNeedTimeError);
        }

        tools.hideItem(dom.editFormFaultDescriptionError);
        var faultDescription = dom.editFormFaultDescription.GetValue();
        var isFaultDescription = !tools.isNullOrEmpty(faultDescription);
        if (!isFaultDescription) {
            isValid = false;
            tools.showItem(dom.editFormFaultDescriptionError);
        }

        return isValid;
    }

    function isEditDetailFormValid() {
        var isValid = true;

        tools.hideItem(dom.editInceptionDetailDescriptionError);
        const description = dom.editInceptionDetailDescription.GetValue();
        const isDescriptionValid = !tools.isNullOrEmpty(description);
        if (!isDescriptionValid) {
            isValid = false;
            tools.showItem(dom.editInceptionDetailDescriptionError);
        }

        tools.hideItem(dom.editInceptionDetailNeedTimeError);
        const needTime = dom.editInceptionDetailNeedTime.GetValue();
        const isNeedTimeValid = !tools.isNullOrEmpty(needTime);
        if (!isNeedTimeValid) {
            isValid = false;
            tools.showItem(dom.editInceptionDetailNeedTimeError);
        }

        return isValid;
    }

    function isFilterFormValid() {


        tools.hideItem(dom.filterFormDateTypeComboError);
        var kanbanType = dom.filterFormDateTypeCombo.GetValue();
        var isKanbanSelected = !tools.isNullOrEmpty(kanbanType);

        if (!isKanbanSelected) {
            tools.showItem(dom.filterFormDateTypeComboError);
        }

        var dateType = dom.filterFormDateTypeCombo.GetValue();
        var isSpecialDateSelected = dateType == state.specialDateTypeFilter;
        if (!isSpecialDateSelected) return isKanbanSelected;

        tools.hideItem(dom.filterFormStartDateError);
        var startDate = dom.filterFormStartDate.val();
        var isValidStartDate = tools.isValidPersianDate(startDate);
        if (!isValidStartDate) {
            tools.showItem(dom.filterFormStartDateError);
        }


        tools.hideItem(dom.filterFormEndDateError);
        var endDate = dom.filterFormEndDate.val();
        var isValidEndDate = tools.isValidPersianDate(endDate);
        if (!isValidStartDate) {
            tools.showItem(dom.filterFormEndDateError);
        }
        return isKanbanSelected && isValidStartDate && isValidEndDate ;
    }

    async function filterInspectionDetailList() {
        const url = "/Inspection/InspectionDetailGrid";

        const apiParam = {
            InspectionId: state.gridMemberFocusedRowIndex,
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.inspectionDetailSourceGirdParent.html(apiResult);
        setDom();

    }

    async function filterInspectionList() {
        const url = "/Inspection/InspectionGird";

        var canContinue = isFilterFormValid();
        if (!canContinue) return false;

        state.dateType = dom.filterFormDateTypeCombo.GetValue();
        state.startDate = dom.filterFormStartDate.val();
        state.endDate = dom.filterFormEndDate.val();
        const apiParam = {
            dateType: state.dateType,
            startDate: state.startDate,
            endDate: state.endDate
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.inspectionSourceGirdParent.html(apiResult);

        setDom();

    }

    function handleProductiveWorkOrderGridCallbackUrl(source) {
        var url = controllerName + "InspectionGird";
        source.callbackUrl = url + "?dateType=" + state.dateType + "&startDate=" + state.startDate +
            "&endDate=" + state.endDate;
    }

    async function loadMachineList(machineId = null) {
        const url = "/Inspection/EditFormGetDepartmentMachineList";
        const apiParam = {
            departmentId: dom.editFormGetSubDepartmentListCombo.GetValue()
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.editFormMachineComboParent.html(apiResult);
        setDom();
        if (machineId != null) {
            dom.editFormMachineCombo.SetValue(machineId);
        }
    }


    function handleFilterFormWorkOrderTypeComboSelectedIndexChange() {
        tools.hideItem(dom.addWorkOrderTypeError);
        var type = dom.addWorkOrderType.GetValue();
        if (type == 1) {
            state.workorderType = Boolean(type);
        }
        else {
            state.workorderType = Boolean(!type);
        }

        if (!state.workorderType) {
            tools.hideItem(dom.productiveTopDepartmentDiv);
            tools.hideItem(dom.productiveSubDepartmentDiv);
            tools.hideItem(dom.productiveMachiveDiv);
            tools.showItem(dom.nonProductiveSubDepartmentDiv);
            tools.showItem(dom.nonProductiveTopDepartmentDiv);
        } else {
            tools.showItem(dom.productiveTopDepartmentDiv);
            tools.showItem(dom.productiveSubDepartmentDiv);
            tools.showItem(dom.productiveMachiveDiv);
            tools.hideItem(dom.nonProductiveSubDepartmentDiv);
            tools.hideItem(dom.nonProductiveTopDepartmentDiv);

        }
    }

    function handleEditFormWorkOrderTypeComboSelectedIndexChange() {
        var type = dom.editWorkOrderTypeCombo.GetValue();
        if (type == 1) {
            state.workorderType = Boolean(type);
        }
        else {
            state.workorderType = Boolean(!type);
        }
        if (!state.workorderType) {
            tools.hideItem(dom.editProductiveTopDepartmentsDiv);
            tools.hideItem(dom.editproductiveSubDepartmentDiv);
            tools.hideItem(dom.editproductiveMachiveDiv);
            tools.showItem(dom.editnonProductiveSubDepartmentDiv);
            tools.showItem(dom.editnonProductiveTopDepartmentDiv);
        } else {
            tools.showItem(dom.editProductiveTopDepartmentsDiv);
            tools.showItem(dom.editproductiveSubDepartmentDiv);
            tools.showItem(dom.editproductiveMachiveDiv);
            tools.hideItem(dom.editnonProductiveSubDepartmentDiv);
            tools.hideItem(dom.editnonProductiveTopDepartmentDiv);

        }
    }
    function handleInspectionTypeSelectedIndexChange() {
        tools.hideItem(dom.addFormWorKOrderListError);
    }

    function handleFilterFormMachineComboSelectedIndexChange() {
        tools.hideItem(dom.addFormMachineComboError);
    }

    function handleaddFormFaultDescriptionChange() {
        tools.hideItem(dom.addFormFaultDescriptionError);
    }

    function handleAddInceptionDetailNeedTimeNumberChanged() {
        tools.hideItem(dom.addInceptionDetailNeedTimeError);

    }

    function handleAddInceptionDetailDescriptionTextChanged() {
        tools.hideItem(dom.addInceptionDetailDescriptionError);
    }

    function handleMaintenanceGroupNameComboSelectedIndexChange() {
        tools.hideItem(dom.addFormMaintenanceGroupComboError);
    }

    function handleFilterFormNeedTimeSelectedIndexChange() {
        tools.hideItem(dom.addFormNeedTimeError);
    }
    function handleFilterFormGetSubDepartmentListComboSelectedIndexChange() {
        tools.hideItem(dom.AddFormGetSubDepartmentListoError);
    }
    async function handleFilterFormTopDepartmentComboSelectedIndexChange() {
        tools.hideItem(dom.AddFormGetDepartmentListError);

        const url = "/Inspection/AddFormGetSubDepartmentListByMainDepartmentIdPartial";
        const apiParam = {
            departmentId: dom.addFormGetDepartmentListCombo.GetValue()
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormGetSubDepartmentListParent.html(apiResult);
        setDom();
    }

    async function handleEditFormTopDepartmentComboSelectedIndexChange() {
        const url = "/Inspection/EditFormGetSubDepartmentListByMainDepartmentIdPartial";
        const apiParam = {
            departmentId: dom.editFormGetDepartmentListCombo.GetValue()
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.editFormGetSubDepartmentListParent.html(apiResult);
        setEditDom();

        state.editDepartment = dom.editFormGetDepartmentListCombo.GetValue();
    }

    async function handleFilterFormSubDepartmentComboSelectedIndexChanged() {
        tools.hideItem(dom.editFormGetSubDepartmentListError);

        state.editSubDepartment = dom.editFormGetSubDepartmentListCombo.GetValue();
    }

    function handleFilterFormSubDepartmentComboBeginCallback(command) {
        var departmentId = dom.addFormGetDepartmentListCombo.GetValue();

        if (departmentId == null) departmentId = -1;

        command.callbackUrl =
            controllerName + "AddFormGetSubDepartmentList?departmentId=" + departmentId;
    }

    async function handleFilterFormProdcutviceSubDepartmentComboSelectedIndexChange() {

        tools.hideItem(dom.AddFormGetProdcutviceSubDepartmentListoError);

        var departmentId = dom.addFormGetProdcutviceSubDepartmentListCombo.GetValue();
        const url = "/Inspection/AddFormGetDepartmentMachineList";
        const apiParam = {
            departmentId: departmentId
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormMachineComboParent.html(apiResult);
        setDom();

    }

    async function handleEditFormProdcutviceSubDepartmentComboSelectedIndexChange() {
        const departmentId = dom.editFormGetProdcutviceSubDepartmentListCombo.GetValue();
        const url = "/Inspection/EditFormGetDepartmentMachineListPartial";
        const apiParam = {
            departmentId: departmentId
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.editFormMachineComboParent.html(apiResult);
        setEditDom();

        state.editSubDepartment = dom.editFormGetProdcutviceSubDepartmentListCombo.GetValue();
    }

    function handleEditFormMachineComboSelectedIndexChanged() {
        state.editMachine = dom.editFormMachineCombo.GetValue();
    }

    function handleFilterFormProdcutviceSubDepartmentComboBeginCallback(command) {
        var departmentId = dom.addFormGetProdcutviceDepartmentListCombo.GetValue();

        if (departmentId == null) departmentId = -1;

        command.callbackUrl =
            controllerName + "AddFormGetProdcutviceSubDepartmentListByMainDepartmentIdPartial?departmentId=" + departmentId;
    }

    function handleEditFormProdcutviceSubDepartmentComboBeginCallback(command) {
        var departmentId = dom.editFormGetProdcutviceDepartmentListCombo.GetValue();
        if (departmentId == null) departmentId = -1;

        command.callbackUrl =
            controllerName + "EditFormGetProdcutviceSubDepartmentListByMainDepartmentIdPartial?departmentId=" + departmentId;
    }

    function handleInspectionDetailGridCustomBtnClick(source, event) {
        const customBtnId = event.buttonID;

        if (customBtnId === "inspectionDetailGridEditCustomBtn") return editInspectionDetailRow();
        if (customBtnId === "inspectionDetailGridDeleteCustomBtn") return removeInspectionDetailRow(source, event);
        return null;
    }

    function editInspectionDetailRow() {
        setDom();
        const url = "/Inspection/EditInspectionDetailGridModal";
        motorsazanClient.contentModal.ajaxShow(" ویرایش", url, {}, initAddOrEditDetailCommentModal);

    }
    function initEditInspectionDetailItemModal() {
        editInceptionDetailItemModalSetDom();

        dom.editFormMaintenanceGroupListCombo.SetValue(state.editMaintenanceGroupId);
        dom.editFormOperationItemCodeTextBox.SetText(state.editOperationItemCode);
    }

    function editInceptionDetailItemModalSetDom() {

    }

    function initAddOrEditDetailCommentModal() {
        setEditDom();
        setDom();
        setDefaultValuesOfEditDetailModal();
    }

    async function setDefaultValuesOfEditDetailModal() {
        const url = "/Inspection/GetInspectionDetailByInspectionDetailId";
        var apiParam = {
            InspectionDetailId: getInspectionDetailActiveRowKey()
        }
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.editInceptionDetailDescription.SetText(apiResult.ActionName);
        dom.editInceptionDetailNeedTime.SetText(apiResult.PersonHour);
}

    async function removeInspectionDetailRow() {
        var title = "تاییدیه حذف";
        var content = "آیا از حذف این ردیف مطمئن هستید؟";

        motorsazanClient.messageModal.confirm(content, removeInspectionDetail, title);

    }

    async function removeInspectionDetail(values) {

        var params = {
            InspectionDetailId: getInspectionDetailActiveRowKey()
        };
        const url = controllerName + "RemoveInspectionDeatilsByInspectionDetailID";
        const result = await motorsazanClient.connector.post(url, params);
        await refreshInspectionManagementGrid();
        motorsazanClient.messageModal.success(result);
        motorsazanClient.messageModal.hide();

    }

    function getInspectionDetailActiveRowKey() {
        var activeIndex = dom.inspectionDetailSourceGird.GetFocusedRowIndex();
        return dom.inspectionDetailSourceGird.GetRowKey(activeIndex);
    }

    async function refreshInspectionManagementGrid() {
        const url = controllerName + "InspectionDetailGrid";

        dom.inspectionSourceGird.GetRowValues(dom.inspectionSourceGird.GetFocusedRowIndex(), "InspectionId", async InspectionId => {
            const params = {
                InspectionId
            };
            state.InspectionId = InspectionId;
            const grid = await motorsazanClient.connector.post(url, params);
            dom.inspectionDetailSourceGirdParent.html(grid);
        });
    }

    function handleEditFormSubDepartmentComboBeginCallback(command) {
        var departmentId = dom.editFormGetDepartmentListCombo.GetValue();

        if (departmentId == null) departmentId = -1;

        command.callbackUrl =
            controllerName + "EditFormGetSubDepartmentList?departmentId=" + departmentId;
    }

    function handleAddFormMachineComboBeginCallback(command) {
        var departmentId = dom.addFormGetProdcutviceSubDepartmentListCombo.GetValue();

        if (departmentId == null) departmentId = -1;

        command.callbackUrl =
            controllerName + "AddFormGetDepartmentMachineList?departmentId=" + departmentId;
    }

    function handleEditFormMachineComboBeginCallback(command) {
        var departmentId = dom.editFormGetProdcutviceSubDepartmentListCombo.GetValue();

        if (departmentId == null) departmentId = -1;

        command.callbackUrl =
            controllerName + "EditFormGetDepartmentMachineListPartial?departmentId=" + departmentId;
    }

    function handleEditFormSubDepartmentComboSelectedIndexChange() {
        loadMachineList();
    }

    function handleAddFormMachinesComboBeginCallback(command) {

        var departmentId = dom.addFormSubDepartmentCombo.GetValue() || 0;

        command.callbackUrl = "/Inspection/AddFormGetDepartmentMachineList?departmentId=" + departmentId;
    }

    async function refreshGrid() {
        const url = "/Inspection/InspectionGird";
        const apiParam = {
            dateType: state.dateType,
            startDate: state.startDate,
            endDate: state.endDate
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        dom.inspectionSourceGirdParent.html(apiResult);
        setDom();


    }

    async function RemoveWorkOrder() {
        const url = "/Inspection/removeInspection";
        const apiParam = {
            InspectionId: state.InspectionId
        };
        const apiResult = await motorsazanClient.connector.post(url, apiParam);
        refreshGrid();
        motorsazanClient.messageModal.success(apiResult);

    }

    function resetAddForm() {
        dom.addFormPreferDate.val("");
        dom.addFormWorkOrderList.SetSelectedIndex(-1);
        dom.addWorkOrderType.SetSelectedIndex(-1);
        dom.addFormMaintenanceGroupListCombo.SetSelectedIndex(-1);
        dom.addFormMachineCombo.SetSelectedIndex(-1);
        dom.addFormGetDepartmentListCombo.SetSelectedIndex(-1);
        dom.addFormSubDepartmentCombo.SetSelectedIndex(-1);
        dom.addFormGetProdcutviceDepartmentListCombo.SetSelectedIndex(-1);
        dom.addFormGetProdcutviceSubDepartmentListCombo.SetSelectedIndex(-1);

        dom.addFormFaultDescription.SetText("");
        dom.addFormNeedTime.SetText("");
    }

    function resetAddDeatilForm() {
        dom.addInceptionDetailDescription.SetText("");
        dom.addInceptionDetailNeedTime.SetText("");

    }
    function resetEditDeatilForm() {
        dom.editInceptionDetailDescription.SetText("");
        dom.editFormNeedTime.SetText("");
    }

    function resetEditForm() {
        dom.editFormWorkOrderList.SetSelectedIndex(-1);
        dom.editWorkOrderTypeCombo.SetSelectedIndex(-1);
        dom.editFormGetProdcutviceSubDepartmentListCombo.SetSelectedIndex(-1);
        dom.editFormMachineCombo.SetSelectedIndex(-1);
        dom.editFormGetProdcutviceSubDepartmentListCombo.SetSelectedIndex(-1);
        dom.editFormMaintenanceGroupListCombo.SetSelectedIndex(-1);
        dom.editFormGetDepartmentListCombo.SetSelectedIndex(-1);
        dom.editFormGetSubDepartmentListCombo.SetSelectedIndex(-1);
        dom.editFormFaultDescription.SetText("");
        dom.editFormNeedTime.SetText("");
    }

    function setDom() {
        dom.addFormEWorOrderListParent = $("#addFormWorkOrderListParent");
        dom.addFormWorKOrderListError = $("#addFormWorKOrderListError");
        dom.addFormWorkOrderList = ASPxClientComboBox.Cast("addFormWorkOrderList");

        dom.addWorkOrderTypeParent = $("#addWorkOrderTypeParent");
        dom.addWorkOrderTypeError = $("#addWorkOrderTypeError");
        dom.addWorkOrderType = ASPxClientComboBox.Cast("addWorkOrderType");

        dom.addFormPreferDate = $("#addFormPreferDate");
        dom.addFormPreferDateError = $("#addFormPreferDateError");

        dom.addFormNeedTimeParent = $("#addFormNeedTimeParent");
        dom.addFormNeedTimeError = $("#addFormNeedTimeError");
        dom.addFormNeedTime = ASPxClientSpinEdit.Cast("addFormNeedTime");

        dom.addFormMaintenanceGroupComboParent = $("#addFormMaintenanceGroupComboParent");
        dom.addFormMaintenanceGroupComboError = $("#addFormMaintenanceGroupComboError");
        dom.addFormMaintenanceGroupListCombo = ASPxClientComboBox.Cast("addFormMaintenanceGroupListCombo");

        dom.addFormFaultDescriptionParent = $("#addFormFaultDescriptionParent");
        dom.addFormFaultDescriptionError = $("#addFormFaultDescriptionError");
        dom.addFormFaultDescription = ASPxClientMemo.Cast("addFormFaultDescription");


        dom.AddFormGetDepartmentListParent = $("#AddFormGetDepartmentListParent");
        dom.AddFormGetDepartmentListError = $("#AddFormGetDepartmentListError");
        dom.addFormGetDepartmentListCombo = ASPxClientComboBox.Cast("addFormGetDepartmentListCombo");

        dom.addFormGetSubDepartmentListParent = $("#addFormGetSubDepartmentListParent");
        dom.AddFormGetSubDepartmentListoError = $("#AddFormGetSubDepartmentListoError");
        dom.addFormSubDepartmentCombo = ASPxClientComboBox.Cast("addFormGetSubDepartmentListCombo");

        dom.addFormMachineComboParent = $("#addFormMachineComboParent");
        dom.addFormMachineComboError = $("#addFormMachineComboError");
        dom.addFormMachineCombo = ASPxClientComboBox.Cast("addFormMachineCombo");

        dom.nonProductiveTopDepartmentDiv = $("#NonProductiveTopDepartmentDiv");
        dom.nonProductiveSubDepartmentDiv = $("#NonProductiveSubDepartmentDiv");

        dom.productiveTopDepartmentDiv = $("#ProductiveTopDepartmentDiv");
        dom.productiveSubDepartmentDiv = $("#ProductiveSubDepartmentDiv");
        dom.productiveMachiveDiv = $("#ProductiveMachiveDiv");

        dom.AddFormGetProdcutviceDepartmentListParent = $("#AddFormGetProdcutviceDepartmentListParent");
        dom.AddFormGetProdcutviceDepartmentListError = $("#AddFormGetProdcutviceDepartmentListError");
        dom.addFormGetProdcutviceDepartmentListCombo = ASPxClientComboBox.Cast("addFormGetProdcutviceDepartmentListCombo");

        dom.AddFormGetProdcutviceSubDepartmentListParent = $("#AddFormGetProdcutviceSubDepartmentListParent");
        dom.AddFormGetProdcutviceSubDepartmentListoError = $("#AddFormGetProdcutviceSubDepartmentListoError");
        dom.addFormGetProdcutviceSubDepartmentListCombo = ASPxClientComboBox.Cast("addFormGetProdcutviceSubDepartmentListCombo");

        dom.inspectionSourceGirdParent = $("#inspectionSourceGirdParent");
        dom.inspectionSourceGird = ASPxClientGridView.Cast("inspectionSourceGird");

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

        dom.addInceptionDetailDescriptionError = $("#addInceptionDetailDescriptionError");
        dom.addInceptionDetailDescription = ASPxClientMemo.Cast("addInceptionDetailDescription");

        dom.addInceptionDetailNeedTimeError = $("#addInceptionDetailNeedTimeError");
        dom.addInceptionDetailNeedTime = ASPxClientSpinEdit.Cast("addInceptionDetailNeedTime");

        dom.inspectionDetailSourceGirdParent = $("#inspectionDetailSourceGirdParent");
        dom.inspectionDetailSourceGird = ASPxClientGridView.Cast("inspectionDetailSourceGird");


        dom.editInceptionDetailDescriptionError = $("#editInceptionDetailDescriptionError");
        dom.editInceptionDetailDescription = ASPxClientMemo.Cast("editInceptionDetailDescription");


    }

    function setEditDom() {
        dom.editmachineDiv = $("#editmachineDiv");

        dom.editFormWorkOrderList = ASPxClientComboBox.Cast("editFormWorkOrderList");
        dom.editFormWorKOrderListError = $("#editFormWorKOrderListError");

        dom.editWorkOrderTypeCombo = ASPxClientComboBox.Cast("editWorkOrderTypeCombo");
        dom.editWorkOrderTypeError = $("#editWorkOrderTypeError");

        dom.editFormGetDepartmentListParent = $("#editFormGetDepartmentListParent");
        dom.editFormGetDepartmentListError = $("#editFormGetDepartmentListError");
        dom.editFormGetDepartmentListCombo = ASPxClientComboBox.Cast("editFormGetDepartmentListCombo");

        dom.editFormGetSubDepartmentListParent = $("#editFormGetSubDepartmentListParent");
        dom.editFormGetSubDepartmentListError = $("#editFormGetSubDepartmentListError");
        dom.editFormGetSubDepartmentListCombo = ASPxClientComboBox.Cast("editFormGetSubDepartmentListCombo");

        dom.editFormMachineComboParent = $("#editFormMachineComboParent");
        dom.editFormMachineComboError = $("#editFormMachineComboError");
        dom.editFormMachineCombo = ASPxClientComboBox.Cast("editFormMachineCombo");

        dom.editFormPreferDate = $("#editFormPreferDate");
        dom.editFormPreferDateError = $("#editFormPreferDateError");

        dom.editFormNeedTimeParent = $("#editFormNeedTimeParent");
        dom.editFormNeedTimeError = $("#editFormNeedTimeError");
        dom.editFormNeedTime = ASPxClientSpinEdit.Cast("editFormNeedTime");

        dom.editFormFaultDescriptionParent = $("#editFormFaultDescriptionParent");
        dom.editFormFaultDescriptionError = $("#editFormFaultDescriptionError");
        dom.editFormFaultDescription = ASPxClientMemo.Cast("editFormFaultDescription");

        dom.editFormMaintenanceGroupComboParent = $("#editFormMaintenanceGroupComboParent");
        dom.editFormMaintenanceGroupComboError = $("#editFormMaintenanceGroupComboError");
        dom.editFormMaintenanceGroupListCombo = ASPxClientComboBox.Cast("editFormMaintenanceGroupListCombo");

        dom.editFormGetDepartmentListParent = $("#editFormGetDepartmentListParent");
        dom.editFormGetDepartmentListError = $("#editFormGetDepartmentListError");
        dom.editFormGetDepartmentListCombo = ASPxClientComboBox.Cast("editFormGetDepartmentListCombo");

        dom.editFormGetSubDepartmentListParent = $("#editFormGetSubDepartmentListParent");
        dom.editFormGetSubDepartmentListoError = $("#editFormGetSubDepartmentListoError");
        dom.editFormSubDepartmentCombo = ASPxClientComboBox.Cast("editFormGetSubDepartmentListCombo");

        dom.editFormMachineComboParent = $("#editFormMachineComboParent");
        dom.editFormMachineComboError = $("#editFormMachineComboError");
        dom.editFormMachineCombo = ASPxClientComboBox.Cast("editFormMachineCombo");

        dom.editnonProductiveTopDepartmentDiv = $("#editNonProductiveTopDepartmentDiv");
        dom.editnonProductiveSubDepartmentDiv = $("#editNonProductiveSubDepartmentDiv");

        dom.editProductiveTopDepartmentsDiv = $("#editProductiveTopDepartmentsDiv");
        dom.editproductiveSubDepartmentDiv = $("#editProductiveSubDepartmentDiv");
        dom.editproductiveMachiveDiv = $("#editProductiveMachiveDiv");

        dom.editFormGetProdcutviceDepartmentListParent = $("#editFormGetProdcutviceDepartmentListParent");
        dom.editFormGetProdcutviceDepartmentListError = $("#editFormGetProdcutviceDepartmentListError");
        dom.editFormGetProdcutviceDepartmentListCombo = ASPxClientComboBox.Cast("editFormGetProdcutviceDepartmentListCombo");

        dom.editFormGetProdcutviceSubDepartmentListParent = $("#editFormGetProdcutviceSubDepartmentListParent");
        dom.editFormGetProdcutviceSubDepartmentListoError = $("#editFormGetProdcutviceSubDepartmentListoError");
        dom.editFormGetProdcutviceSubDepartmentListCombo = ASPxClientComboBox.Cast("editFormGetProdcutviceSubDepartmentListCombo");



        dom.editInceptionDetailDescriptionError = $("#editInceptionDetailDescriptionError");
        dom.editInceptionDetailDescription = ASPxClientMemo.Cast("editInceptionDetailDescription");

        dom.editInceptionDetailNeedTimeError = $("#editInceptionDetailNeedTimeError");
        dom.editInceptionDetailNeedTime = ASPxClientSpinEdit.Cast("editInceptionDetailNeedTime");


    }



    function setProductiveWorkOrderGridFocusedRowOnExpanding(source, event) {
        source.SetFocusedRowIndex(event.visibleIndex);
        state.InspectionId = dom.inspectionSourceGird.GetRowKey(event.visibleIndex);
    }



    function showActionHistoryModal() {
        setDom();
        dom.inspectionSourceGird.GetRowValues(dom.inspectionSourceGird.GetFocusedRowIndex(), "WorkOrderId", (values) => {
            setDom();
            const url = controllerName + "ShowActionHistory";
            const params = {
                workOrderId: values
            };
            const title = "مشاهده عملیات انجام شده ";
            motorsazanClient.contentModal.ajaxShow(title, url, params, setDom);

        });


    }

    function showHistoryModal() {

        setDom();
        dom.inspectionSourceGird.GetRowValues(dom.inspectionSourceGird.GetFocusedRowIndex(), "WorkOrderId", (values) => {
            setDom();
            const url = controllerName + "ShowReferenceHistory";
            const params = {
                workOrderId: values
            };
            var title = "مشاهده تاریخچه ارجاعات";
            motorsazanClient.contentModal.ajaxShow(title, url, params, setDom);

        });

    }



    function showUsedToolsModal() {
        setDom();
        dom.inspectionSourceGird.GetRowValues(dom.inspectionSourceGird.GetFocusedRowIndex(), "WorkOrderId", (values) => {
            setDom();
            const url = controllerName + "ShowUsedTools";
            const params = {
                workOrderId: values
            };
            var title = "مشاهده مواد مصرفی";
            motorsazanClient.contentModal.ajaxShow(title, url, params, setDom);
        });
    }


    function updateFilterFormDateSectionVisibility() {
        var activeDateType = dom.filterFormDateTypeCombo.GetValue();
        var isSpecialTypeSelected = activeDateType == state.specialDateTypeFilter;


        if (isSpecialTypeSelected) {
            tools.showItem(dom.filterFormSpecialDateColumn);
            dom.filterFormDateTypeColumn.removeClass("col-md-4").addClass("col-md-2");
        }
        else {
            tools.hideItem(dom.filterFormSpecialDateColumn);
            dom.filterFormDateTypeColumn.removeClass("col-md-2").addClass("col-md-4");
        }
    }

    function init() {
        setDom();
        setEvents();
    }
    function setEvents() {
        dom.addFormPreferDate.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormStartDate.change(handleFilterFormStartDatePickerSelectionChange);
        dom.filterFormEndDate.change(handlefilterFormEndDatePickerSelectionChange);
    }
    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.addFormPreferDateError);
    }

    function handleFilterFormStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormStartDateError);
    }
    function handlefilterFormEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormEndDateError);
    }
    
   
    $(document).ready(init);
    return {
        addInspection: addInspection,
        editTheInspection: editTheInspection,
        handleFilterFormWorkOrderTypeComboSelectedIndexChange: handleFilterFormWorkOrderTypeComboSelectedIndexChange,
        filterInspectionList: filterInspectionList,
        updateFilterFormDateSectionVisibility: updateFilterFormDateSectionVisibility,
        handleAddFormMachinesComboBeginCallback: handleAddFormMachinesComboBeginCallback,
        handleProductiveWorkOrderGridCallbackUrl: handleProductiveWorkOrderGridCallbackUrl,
        setProductiveWorkOrderGridFocusedRowOnExpanding: setProductiveWorkOrderGridFocusedRowOnExpanding,
        handleFilterFormTopDepartmentComboSelectedIndexChange: handleFilterFormTopDepartmentComboSelectedIndexChange,
        handleFilterFormGetSubDepartmentListComboSelectedIndexChange: handleFilterFormGetSubDepartmentListComboSelectedIndexChange,
        handleInspectionTypeSelectedIndexChange: handleInspectionTypeSelectedIndexChange,
        handleFilterFormSubDepartmentComboSelectedIndexChanged: handleFilterFormSubDepartmentComboSelectedIndexChanged,
        handleFilterFormSubDepartmentComboBeginCallback: handleFilterFormSubDepartmentComboBeginCallback,
        handleaddFormFaultDescriptionChange: handleaddFormFaultDescriptionChange,
        handleAddInceptionDetailNeedTimeNumberChanged: handleAddInceptionDetailNeedTimeNumberChanged,
        handleAddInceptionDetailDescriptionTextChanged: handleAddInceptionDetailDescriptionTextChanged,
        handleAddFormMachineComboBeginCallback: handleAddFormMachineComboBeginCallback,
        handleFilterFormMachineComboSelectedIndexChange: handleFilterFormMachineComboSelectedIndexChange,
        handleFilterFormNeedTimeSelectedIndexChange: handleFilterFormNeedTimeSelectedIndexChange,
        handleMaintenanceGroupNameComboSelectedIndexChange: handleMaintenanceGroupNameComboSelectedIndexChange,
        ebtalInspection: ebtalInspection,
        showActionHistoryModal: showActionHistoryModal,
        showUsedToolsModal: showUsedToolsModal,
        showHistoryModal: showHistoryModal,
        changeInspectionToWorkOrder: changeInspectionToWorkOrder,
        editInspection: editInspection,
        handleEditFormTopDepartmentComboSelectedIndexChange: handleEditFormTopDepartmentComboSelectedIndexChange,
        handleEditFormWorkOrderTypeComboSelectedIndexChange: handleEditFormWorkOrderTypeComboSelectedIndexChange,
        handleEditFormSubDepartmentComboBeginCallback: handleEditFormSubDepartmentComboBeginCallback,
        handleEditFormSubDepartmentComboSelectedIndexChange: handleEditFormSubDepartmentComboSelectedIndexChange,
        handleEditFormMachineComboBeginCallback: handleEditFormMachineComboBeginCallback,
        handleFilterFormProdcutviceTopDepartmentComboSelectedIndexChange: handleFilterFormProdcutviceTopDepartmentComboSelectedIndexChange,
        handleEditFormProdcutviceTopDepartmentComboSelectedIndexChange: handleEditFormProdcutviceTopDepartmentComboSelectedIndexChange,
        addInspectionDetail: addInspectionDetail,
        handleFilterFormProdcutviceSubDepartmentComboSelectedIndexChange: handleFilterFormProdcutviceSubDepartmentComboSelectedIndexChange,
        handleEditFormMachineComboSelectedIndexChanged: handleEditFormMachineComboSelectedIndexChanged,
        handleFilterFormProdcutviceSubDepartmentComboBeginCallback: handleFilterFormProdcutviceSubDepartmentComboBeginCallback,
        handleEditFormProdcutviceSubDepartmentComboBeginCallback: handleEditFormProdcutviceSubDepartmentComboBeginCallback,
        handleEditFormProdcutviceSubDepartmentComboSelectedIndexChange: handleEditFormProdcutviceSubDepartmentComboSelectedIndexChange,
        handleEditFormSubDepartmentComboBeginCallback: handleEditFormSubDepartmentComboBeginCallback,
        addInceptionDetail: addInceptionDetail,
        handleInspectionDetailGridCustomBtnClick: handleInspectionDetailGridCustomBtnClick,
        editInceptionDetail: editInceptionDetail

    };

})();

