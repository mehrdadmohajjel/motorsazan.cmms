/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/DevExpressIntellisense/devexpress-aspnetcore-bootstrap.d.ts" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.oilService = (function() {

    var dom = {};
    var state = {
        specialDateTypeFilter: "CustomPeriod",
        departmentId: null,
        subDepartmentId: null,
        GridFocusedRowIndex: 0,
        GridMemberFocusedRowIndex: 0,
        GroupId: 0,
        oilServiceId: 0,
        statusId: 0,
        dateType: 0,
        startDate: "",
        endDate: "",
        workOrderType: 0,
        machineId: null
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/OilService/";

    function addOilService() {
        const canContinue = isAddFormValid();
        if (!canContinue) return false;

        const url = controllerName + "AddNewOilService";

        const persianPreferDate = dom.addFormPreferDate.val();
        const stockId = dom.addFormOilListCombo.GetValue();
        const measurementUnitId = dom.addFormMeasurementUnitListCombo.GetValue();
        const measurementValue = dom.addFormMeasurementValueTextBox.GetValue();
        const subDepartmentId = dom.addFormGetSubDepartmentListCombo.GetValue();
        const machineId = dom.addFormMachineListCombo.GetValue();
        const oilPlaces = dom.addOilPlaceListCombo.GetValue();
        const employeeIdList = dom.addOperationsModalEmployeeCheckComboBox.GetSelectedValues();
        const needTime = dom.addFormNeedTimeSpin.GetValue();
        const price = dom.addFormPriceTextBox.GetValue();
        const isOilConsumptionNormal = dom.addFormIsOilConsumptionNormal.GetValue();

        const apiParam = {
            departmentId: subDepartmentId,
            machineId: machineId,
            stockId: stockId,
            oilServicePlaceId: oilPlaces,
            measurementUnitId: measurementUnitId,
            quantity: measurementValue,
            price: price,
            isUsedOilNormal: isOilConsumptionNormal,
            employeeIdList: employeeIdList.map(employeeId => { return { employeeId }; }),
            personHour: needTime,
            persianPreferDate: persianPreferDate
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function (apiResult) {
                filterOilList();
                motorsazanClient.messageModal.success(apiResult);

                setDom();
                resetAddForm();

                tools.hideItem(dom.addOperationsModalEmployeeCheckComboBoxInfo);
            });
        return true;
    }

    async function confirmRemoveOilService(values) {
        const url = controllerName + "RemoveOilService";
        const params = {
            OilServiceId: values
        };

        const result = await motorsazanClient.connector.post(url, params);

        await refreshOilServiceGrid();
        motorsazanClient.messageModal.success(result);
    }

    function filterOilList() {
        const canContinue = isFilterFormValid();
        if (!canContinue) return false;

        refreshOilServiceGrid();

        return true;
    }

    async function handleAddFormGetDepartmentListSelectedIndexChange() {
        const url = controllerName + "AddFormGetSubDepartmentList";

        tools.hideItem(dom.addFormGetDepartmentListError);

        dom.addFormGetSubDepartmentListCombo.SetValue();
        dom.addFormMachineListCombo.SetValue();

        dom.addFormGetSubDepartmentListCombo.SetEnabled(true);
        dom.addFormMachineListCombo.SetEnabled(false);

        const departmentId = dom.addFormGetDepartmentListCombo.GetValue();
        state.departmentId = departmentId;

        const apiParam = {
            departmentId: departmentId
        };

        const subDepartmentList = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormGetSubDepartmentListParent.html(subDepartmentList);

        setDom();
    }

    async function handleAddFormGetSubDepartmentListComboSelectedIndexChange() {
        const url = controllerName + "AddFormDepartmentMachineList";

        tools.hideItem(dom.addFormGetSubDepartmentListError);

        dom.addFormMachineListCombo.SetValue();

        dom.addFormMachineListCombo.SetEnabled(true);

        const departmentId = dom.addFormGetSubDepartmentListCombo.GetValue();
        state.subDepartmentId = departmentId;

        const apiParam = {
            departmentId: departmentId
        };

        const machineList = await motorsazanClient.connector.post(url, apiParam);
        dom.addFormMachineListParent.html(machineList);

        setDom();
    }

    function handleAddFormIsOilConsumptionNormalSelectedIndexChanged() {
        tools.hideItem(dom.addFormIsOilConsumptionNormalError);
    }

    function handleAddFormMachineListComboBeginCallback(command) {
        var departmentId = dom.addFormGetSubDepartmentListCombo.GetValue();

        if (departmentId == null) departmentId = -1;

        command.callbackUrl =
            controllerName + "AddFormDepartmentMachineList?departmentId=" + departmentId;
    }

    function handleAddFormMachineListComboSelectedIndexChange() {
        tools.hideItem(dom.addFormMachineListError);
    }

    function handleAddFormMeasurementUnitListComboIndexChange() {
        tools.hideItem(dom.addFormMeasurementUnitListError);

        const measurementUnitList = dom.addFormMeasurementUnitListCombo.GetValue();
        const isMeasurementUnitListValid = !tools.isNullOrEmpty(measurementUnitList);
        if (!isMeasurementUnitListValid) {
            tools.showItem(dom.addFormMeasurementUnitListError);
        }
    }

    function handleAddFormMeasurementValueTextBoxValueChanged() {
        tools.hideItem(dom.addFormMeasurementValueError);

        const measurementValue = dom.addFormMeasurementValueTextBox.GetValue();
        const isMeasurementValueValid = !tools.isNullOrEmpty(measurementValue);
        if (!isMeasurementValueValid) {
            tools.showItem(dom.addFormMeasurementValueError);
        } else {
            const oilUnitPrice = dom.addFormOilListCombo.GetSelectedItem().texts[3];
            const isOilUnitPriceValid = !tools.isNullOrEmpty(oilUnitPrice);
            if (!isOilUnitPriceValid) {
                tools.showItem(dom.addFormOilListError);
            } else {
                dom.addFormPriceTextBox.SetValue(parseFloat(oilUnitPrice) * measurementValue);
                tools.hideItem(dom.addFormPriceError);
            }
        }
    }

    function handleAddFormNeedTimeSpinNumberChanged() {
        tools.hideItem(dom.addFormNeedTimeSpinError);

        const needTime = dom.addFormNeedTimeSpin.GetValue();
        const isNeedTimeValid = !tools.isNullOrEmpty(needTime);
        if (!isNeedTimeValid) {
            tools.showItem(dom.addFormNeedTimeSpinError);
        }
    }

    function handleAddFormOilListComboSelectedIndexChange() {
        tools.hideItem(dom.addFormOilListError);

        tools.showItem(dom.addFormOilListDetailParent);

        dom.addFormMeasurementValueTextBox.SetValue("");
        tools.hideItem(dom.addFormMeasurementValueError);

        dom.addFormMeasurementUnitListCombo.SetValue();
        tools.hideItem(dom.addFormMeasurementUnitListError);

        dom.addFormPriceTextBox.SetValue("");
        tools.hideItem(dom.addFormPriceError);
    }

    function handleAddFormOilPlaceListComboSelectedIndexChange() {
        tools.hideItem(dom.addOilPlaceListComboError);
    }

    function handleAddFormPreferDateSelectionChange() {
        tools.hideItem(dom.addFormPreferDateError);
    }

    function handleAddFormSubDepartmentComboBeginCallback(command) {

        var departmentId = dom.addFormGetSubDepartmentListCombo.GetValue();

        if (departmentId == null) departmentId = -1;

        command.callbackUrl =
            controllerName + "AddFormGetSubDepartmentList?departmentId=" + departmentId;
    }

    function handleAddOperationsModalEmployeeCheckComboBoxSelectedIndexChange() {
        tools.hideItem(dom.addOperationsModalEmployeeCheckComboBoxError);

        tools.showItem(dom.addFormEmployeeCheckDetailParent);

        const employee = dom.addOperationsModalEmployeeCheckComboBox.GetSelectedValues();
        const isEmployeeSelected = !tools.isNullOrEmpty(employee);
        if (!isEmployeeSelected) {
            tools.hideItem(dom.addOperationsModalEmployeeCheckComboBoxInfo);
            tools.showItem(dom.addOperationsModalEmployeeCheckComboBoxError);
            tools.hideItem(dom.addFormEmployeeCheckDetailParent);
            dom.addFormNeedTimeSpin.SetValue(0);
        } else {
            const len = employee.length;
            dom.addOperationsModalEmployeeCheckComboBoxInfo.html(`تعداد ${len} کارمند انتخاب شده است`);
            tools.showItem(dom.addOperationsModalEmployeeCheckComboBoxInfo);
        }
    }

    function handleFilterFormEndDateChange() {
        tools.hideItem(dom.filterFormEndDateError);
    }

    function handleFilterFormStartDateChange() {
        tools.hideItem(dom.filterFormStartDateError);
    }

    function handleOilServiceGridCallbackUrl(command) {
        if (!isFilterFormValid())
            return;

        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const url = controllerName +
            "OilServiceGird" +
            "?persianStartDate=" +
            persianStartDate +
            "&persianEndDate=" +
            persianEndDate +
            "&datePeriodType=" +
            datePeriodType;

        command.callbackUrl = url;
    }

    function handleRemoveWorkOrderBtnClick(source, event) {
        const customBtnId = event.buttonID;
        if (customBtnId === "registerOilServiceGridCustomBtn") return removeConsumablesManagementRow();
        return null;
    }

    function init() {
        setDom();
        setEvents();

        dom.addFormPreferDate.on("change", handleAddFormPreferDateSelectionChange);

        dom.filterFormStartDate.on("change", handleFilterFormStartDateChange);
        dom.filterFormEndDate.on("change", handleFilterFormEndDateChange);
    }

    function isAddFormValid() {
        var isValid = true;

        tools.hideItem(dom.addFormPreferDateError);
        const preferDate = dom.addFormPreferDate.val();
        const isValidPreferDate = tools.isValidPersianDate(preferDate);
        if (!isValidPreferDate) {
            tools.showItem(dom.addFormPreferDateError);
            isValid = false;
        }

        tools.hideItem(dom.addFormOilListError);
        const oilList = dom.addFormOilListCombo.GetValue();
        const isOilListValid = !tools.isNullOrEmpty(oilList);
        if (!isOilListValid) {
            isValid = false;
            tools.showItem(dom.addFormOilListError);
        }

        tools.hideItem(dom.addFormMeasurementUnitListError);
        const measurementUnitList = dom.addFormMeasurementUnitListCombo.GetValue();
        const isMeasurementUnitListValid = !tools.isNullOrEmpty(measurementUnitList);
        if (!isMeasurementUnitListValid) {
            isValid = false;
            tools.showItem(dom.addFormMeasurementUnitListError);
        }

        tools.hideItem(dom.addFormMeasurementValueError);
        const measurementValue = dom.addFormMeasurementValueTextBox.GetValue();
        const isMeasurementValueValid = !tools.isNullOrEmpty(measurementValue);
        if (!isMeasurementValueValid) {
            isValid = false;
            tools.showItem(dom.addFormMeasurementValueError);
        }

        tools.hideItem(dom.addFormGetDepartmentListError);
        const topDepartment = dom.addFormGetDepartmentListCombo.GetValue();

        const isTopDepartmentValid = !tools.isNullOrEmpty(topDepartment);
        if (!isTopDepartmentValid) {
            isValid = false;
            tools.showItem(dom.addFormGetDepartmentListError);
        }

        tools.hideItem(dom.addFormGetSubDepartmentListError);
        const subDepartment = dom.addFormGetSubDepartmentListCombo.GetValue();
        const isSubDepartment = !tools.isNullOrEmpty(subDepartment);
        if (!isSubDepartment) {
            isValid = false;
            tools.showItem(dom.addFormGetSubDepartmentListError);
        }

        tools.hideItem(dom.addFormMachineListError);
        const machine = dom.addFormMachineListCombo.GetValue();

        const isMachineValid = !tools.isNullOrEmpty(machine);
        if (!isMachineValid) {
            isValid = false;
            tools.showItem(dom.addFormMachineListError);
        }

        tools.hideItem(dom.addOilPlaceListComboError);
        const oilPlaces = dom.addOilPlaceListCombo.GetValue();
        const isOilPlaceValid = !tools.isNullOrEmpty(oilPlaces);
        if (!isOilPlaceValid) {
            isValid = false;
            tools.showItem(dom.addOilPlaceListComboError);
        }

        tools.hideItem(dom.addOperationsModalEmployeeCheckComboBoxError);
        const employees = dom.addOperationsModalEmployeeCheckComboBox.GetSelectedValues();
        const isEmployeeSelected = employees.length > 0;
        if (!isEmployeeSelected) {
            isValid = false;
            tools.showItem(dom.addOperationsModalEmployeeCheckComboBoxError);
        }

        tools.hideItem(dom.addFormNeedTimeSpinError);
        const needTime = dom.addFormNeedTimeSpin.GetValue();
        const isNeedTimeValid = !tools.isNullOrEmpty(needTime);
        if (!isNeedTimeValid) {
            isValid = false;
            tools.showItem(dom.addFormNeedTimeSpinError);
        }

        tools.hideItem(dom.addFormPriceError);
        const price = dom.addFormPriceTextBox.GetValue();
        const isPriceValid = !tools.isNullOrEmpty(price);
        if (!isPriceValid) {
            isValid = false;
            tools.showItem(dom.addFormPriceError);
        }

        tools.hideItem(dom.addFormIsOilConsumptionNormalError);
        var isOilConsumptionNormal = dom.addFormIsOilConsumptionNormal.GetValue();
        var isOilConsumptionNormalHasValue = !tools.isNullOrEmpty(isOilConsumptionNormal);
        if (!isOilConsumptionNormalHasValue) {
            isValid = false;
            tools.showItem(dom.addFormIsOilConsumptionNormalError);
        }

        return isValid;
    }

    function isFilterFormValid() {
        var isValid = true;

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

    async function refreshOilServiceGrid() {
        const url = controllerName + "OilServiceGird";

        state.dateType = dom.filterFormDateTypeCombo.GetValue();
        state.startDate = dom.filterFormStartDate.val();
        state.endDate = dom.filterFormEndDate.val();

        const apiParam = {
            persianStartDate: state.startDate,
            persianEndDate: state.endDate,
            datePeriodType: state.dateType
        };

        const grid = await motorsazanClient.connector.post(url, apiParam);
        dom.oilServiceGirdParent.html(grid);
        setDom();

    }

    function removeConsumablesManagementRow() {
        setDom();
        dom.oilServiceGird.GetRowValues(dom.oilServiceGird.GetFocusedRowIndex(), "OilServiceId", (values) => {
            motorsazanClient.messageModal.confirm("آیا از حذف این ردیف مطمئن هستید؟", () => confirmRemoveOilService(values), "تاییدیه حذف");
        });
    }

    function resetAddForm() {
        dom.addOperationsModalEmployeeCheckComboBox.SetValue();
        tools.hideItem(dom.addOperationsModalEmployeeCheckComboBoxError);

        dom.addFormPreferDate.val("");
        tools.hideItem(dom.addFormPreferDateError);

        dom.addFormOilListCombo.SetSelectedIndex(-1);
        tools.hideItem(dom.addFormOilListError);

        dom.addFormMeasurementUnitListCombo.SetSelectedIndex(-1);
        tools.hideItem(dom.addFormMeasurementUnitListError);

        dom.addFormMeasurementValueTextBox.SetValue("");
        tools.hideItem(dom.addFormMeasurementValueError);

        dom.addFormGetDepartmentListCombo.SetSelectedIndex(-1);
        tools.hideItem(dom.addFormGetDepartmentListError);

        dom.addFormGetSubDepartmentListCombo.SetSelectedIndex(-1);
        tools.hideItem(dom.addFormGetSubDepartmentListError);

        dom.addFormMachineListCombo.SetSelectedIndex(-1);
        tools.hideItem(dom.addFormMachineListError);

        dom.addOilPlaceListCombo.SetSelectedIndex(-1);
        tools.hideItem(dom.addOilPlaceListComboError);

        dom.addFormNeedTimeSpin.SetValue("");
        tools.hideItem(dom.addFormNeedTimeSpinError);

        dom.addFormPriceTextBox.SetValue("");
        tools.hideItem(dom.addFormPriceError);

        dom.addFormIsOilConsumptionNormal.Clear();
        tools.hideItem(dom.addFormIsOilConsumptionNormalError);

        tools.hideItem(dom.addFormOilListDetailParent);
        tools.hideItem(dom.addFormEmployeeCheckDetailParent);
    }

    function setDom() {
        //-------------- Add Form
        dom.addFormPreferDate = $("#addFormPreferDate");
        dom.addFormPreferDateError = $("#addFormPreferDateError");

        dom.addFormOilListParent = $("#addFormOilListParent");
        dom.addFormOilListError = $("#addFormOilListError");
        dom.addFormOilListCombo = ASPxClientComboBox.Cast("addFormOilListCombo");

        dom.addFormOilListDetailParent = $("#addFormOilListDetailParent");

        dom.addFormMeasurementUnitListParent = $("#addFormMeasurementUnitListParent");
        dom.addFormMeasurementUnitListError = $("#addFormMeasurementUnitListError");
        dom.addFormMeasurementUnitListCombo = ASPxClientComboBox.Cast("addFormMeasurementUnitListCombo");

        dom.addFormMeasurementValueParent = $("#addFormMeasurementValueParent");
        dom.addFormMeasurementValueError = $("#addFormMeasurementValueError");
        dom.addFormMeasurementValueTextBox = ASPxClientTextBox.Cast("addFormMeasurementValueTextBox");

        dom.addFormGetDepartmentListDesign = $("#addFormGetDepartmentListDesign");
        dom.addFormGetDepartmentListParent = $("#addFormGetDepartmentListParent");
        dom.addFormGetDepartmentListError = $("#addFormGetDepartmentListError");
        dom.addFormGetDepartmentListCombo = ASPxClientComboBox.Cast("addFormGetDepartmentListCombo");

        dom.addFormGetSubDepartmentListDesign = $("#addFormGetSubDepartmentListDesign");
        dom.addFormGetSubDepartmentListParent = $("#addFormGetSubDepartmentListParent");
        dom.addFormGetSubDepartmentListError = $("#addFormGetSubDepartmentListError");
        dom.addFormGetSubDepartmentListCombo = ASPxClientComboBox.Cast("addFormGetSubDepartmentListCombo");

        dom.addFormMachineListDesign = $("#addFormMachineListDesign");
        dom.addFormMachineListParent = $("#addFormMachineListParent");
        dom.addFormMachineListError = $("#addFormMachineListError");
        dom.addFormMachineListCombo = ASPxClientComboBox.Cast("addFormMachineListCombo");

        dom.addOilPlaceListComboDesign = $("#addOilPlaceListComboDesign");
        dom.addOilPlaceListComboParent = $("#addOilPlaceListComboParent");
        dom.addOilPlaceListComboError = $("#addOilPlaceListComboError");
        dom.addOilPlaceListCombo = ASPxClientComboBox.Cast("addOilPlaceListCombo");

        dom.addOperationsModalEmployeeCheckComboBoxDesign = $("#addOperationsModalEmployeeCheckComboBoxDesign");
        dom.addOperationsModalEmployeeCheckComboBoxParent = $("#addOperationsModalEmployeeCheckComboBoxParent");
        dom.addOperationsModalEmployeeCheckComboBoxError = $("#addOperationsModalEmployeeCheckComboBoxError");
        dom.addOperationsModalEmployeeCheckComboBoxInfo = $("#addOperationsModalEmployeeCheckComboBoxInfo");
        dom.addOperationsModalEmployeeCheckComboBox = ASPxClientListBox.Cast("addOperationsModalEmployeeCheckComboBox");

        dom.addFormEmployeeCheckDetailParent = $("#addFormEmployeeCheckDetailParent");

        dom.addFormNeedTimeSpinParent = $("#addFormNeedTimeSpinParent");
        dom.addFormNeedTimeSpinError = $("#addFormNeedTimeSpinError");
        dom.addFormNeedTimeSpin = ASPxClientTextBox.Cast("addFormNeedTimeSpin");

        dom.addFormPriceParent = $("#addFormPriceParent");
        dom.addFormPriceError = $("#addFormPriceError");
        dom.addFormPriceTextBox = ASPxClientTextBox.Cast("addFormPriceTextBox");

        dom.addFormIsOilConsumptionNormalParent = $("#addFormIsOilConsumptionNormalParent");
        dom.addFormIsOilConsumptionNormalError = $("#addFormIsOilConsumptionNormalError");
        dom.addFormIsOilConsumptionNormal = ASPxClientRadioButtonList.Cast("addFormIsOilConsumptionNormal");

        //-------------- Filter
        dom.filterFormDateTypeComboParent = $("#filterFormDateTypeComboParent");
        dom.filterFormDateTypeComboError = $("#filterFormDateTypeComboError");
        dom.filterFormDateTypeCombo = ASPxClientComboBox.Cast("filterFormDateTypeCombo");

        dom.filterFormSpecialDateParent = $("#filterFormSpecialDateParent");

        dom.filterFormStartDate = $("#filterFormStartDate");
        dom.filterFormStartDateError = $("#filterFormStartDateError");

        dom.filterFormEndDate = $("#filterFormEndDate");
        dom.filterFormEndDateError = $("#filterFormEndDateError");

        //-------------- Grid
        dom.oilServiceGirdParent = $("#oilServiceGirdParent");
        dom.oilServiceGird = ASPxClientGridView.Cast("oilServiceGird");
    }

    function setEvents() {
        dom.addFormGetSubDepartmentListCombo.SetEnabled(false);
        dom.addFormMachineListCombo.SetEnabled(false);

        dom.addFormPriceTextBox.SetEnabled(false);
    }

    function updateFilterFormDateSectionVisibility() {
        tools.hideItem(dom.filterFormDateTypeComboError);

        const activeDateType = dom.filterFormDateTypeCombo.GetValue();

        const dateTypeHasValue = !tools.isNullOrEmpty(activeDateType);
        if (!dateTypeHasValue) {
            tools.showItem(dom.filterFormDateTypeComboError);
        }

        const isSpecialTypeSelected = activeDateType === state.specialDateTypeFilter;

        if (isSpecialTypeSelected) {
            tools.showItem(dom.filterFormSpecialDateParent);
            dom.filterFormDateTypeComboParent.removeClass("col-md-4").addClass("col-md-3");
        } else {
            tools.hideItem(dom.filterFormSpecialDateParent);
            dom.filterFormDateTypeComboParent.removeClass("col-md-3").addClass("col-md-4");
        }
    }

    $(document).ready(init);

    return {
        addOilService: addOilService,
        filterOilList: filterOilList,
        handleAddFormGetDepartmentListSelectedIndexChange: handleAddFormGetDepartmentListSelectedIndexChange,
        handleAddFormGetSubDepartmentListComboSelectedIndexChange:
            handleAddFormGetSubDepartmentListComboSelectedIndexChange,
        handleAddFormIsOilConsumptionNormalSelectedIndexChanged:
            handleAddFormIsOilConsumptionNormalSelectedIndexChanged,
        handleAddFormMachineListComboBeginCallback: handleAddFormMachineListComboBeginCallback,
        handleAddFormMachineListComboSelectedIndexChange: handleAddFormMachineListComboSelectedIndexChange,
        handleAddFormMeasurementUnitListComboIndexChange: handleAddFormMeasurementUnitListComboIndexChange,
        handleAddFormMeasurementValueTextBoxValueChanged: handleAddFormMeasurementValueTextBoxValueChanged,
        handleAddFormNeedTimeSpinNumberChanged: handleAddFormNeedTimeSpinNumberChanged,
        handleAddFormOilListComboSelectedIndexChange: handleAddFormOilListComboSelectedIndexChange,
        handleAddFormOilPlaceListComboSelectedIndexChange: handleAddFormOilPlaceListComboSelectedIndexChange,
        handleAddFormPreferDateSelectionChange: handleAddFormPreferDateSelectionChange,
        handleAddFormSubDepartmentComboBeginCallback: handleAddFormSubDepartmentComboBeginCallback,
        handleAddOperationsModalEmployeeCheckComboBoxSelectedIndexChange:
            handleAddOperationsModalEmployeeCheckComboBoxSelectedIndexChange,
        handleFilterFormEndDateChange: handleFilterFormEndDateChange,
        handleFilterFormStartDateChange: handleFilterFormStartDateChange,
        handleOilServiceGridCallbackUrl: handleOilServiceGridCallbackUrl,
        handleRemoveWorkOrderBtnClick: handleRemoveWorkOrderBtnClick,
        updateFilterFormDateSectionVisibility: updateFilterFormDateSectionVisibility,
        dom
    };

})();