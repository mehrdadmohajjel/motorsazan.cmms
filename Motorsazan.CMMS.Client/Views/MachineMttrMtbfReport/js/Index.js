/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.machineMttrMtbfReport = (function() {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod",
        machineId: 0,
        workOrderId: 0,
        departmentId: 0,
        dateTypeId: 0,
        timeType: 0,
        persianStartDate: "",
        persianEndDate: "",
        datePeriodType:0


    };
    var tools = motorsazanClient.tools;
    var controllerName = "/MachineMttrMtbfReport/";


    async function fillGrid() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "Grid";

        state.departmentId = dom.filterFormSubDepartmentIdCombo.GetValue();
        state.machineId = dom.filterFormMachineIdCombo.GetValue();
        state.timeType = dom.filterFormMttrMtbfTimeTypeCombo.GetValue();
        const datePeriodType = dom.filterFormDateCombo.GetValue();
        state.datePeriodType = datePeriodType;
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        state.persianStartDate = persianStartDate;
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();
        state.persianEndDate = persianEndDate;
        const apiParam = {
            machineId: state.machineId,
            timeType: state.timeType,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.gridParent.html(gridHtml);
        setDom();
    }

    async function fillDetailGrid() {
        if (!isFilterFormValid())
            return;

        const url = controllerName + "DetailGrid";

        state.departmentId = dom.filterFormSubDepartmentIdCombo.GetValue();
        state.machineId = dom.filterFormMachineIdCombo.GetValue();
        state.timeTypeId = dom.filterFormMttrMtbfTimeTypeCombo.GetValue();
        const datePeriodType = dom.filterFormDateCombo.GetValue();
        state.datePeriodType = datePeriodType;
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        state.persianStartDate = persianStartDate;
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();
        state.persianEndDate = persianEndDate;
        const apiParam = {
            machineId: state.machineId,
            TimeType: state.timeTypeId,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate,
            datePeriodType: datePeriodType
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.detailGridParent.html(gridHtml);
        setDom();
    }

    function handleFilterFormDateComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDateComboError);

        const selectedDateId = dom.filterFormDateCombo.GetValue();

        if (selectedDateId === state.customPeriodId) {
            tools.enableItem(dom.filterFormPeriodStartDatePicker);
            tools.enableItem(dom.filterFormPeriodEndDatePicker);
        } else {
            tools.disableItem(dom.filterFormPeriodStartDatePicker);
            tools.disableItem(dom.filterFormPeriodEndDatePicker);
            dom.filterFormPeriodStartDatePicker.val("");
            dom.filterFormPeriodEndDatePicker.val("");
        }
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
        dom.filterFormMachineIdCombo.SetSelectedIndex(-1);
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
        tools.hideItem(dom.filterFormDepartmentIdComboError);
        tools.hideItem(dom.filterFormSubDepartmentIdComboError);
        tools.hideItem(dom.filterFormMachineIdComboError);

        dom.filterFormMttrMtbfTimeTypeCombo.SetEnabled(true);
    }


    function handleFilterFormMttrMtbfTimeTypeComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormMttrMtbfTimeTypeComboError);
        dom.filterFormDateCombo.SetEnabled(true);
    }

    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
    }

    function handleFilterFormSearchButtonClick() {
        if (!isFilterFormValid())
            return;

        fillGrid();
        fillDetailGrid();
    }
    function handleDetailGridBeginCallback(command) {
        if (!isFilterFormValid())
            return;

        state.machinId = dom.filterFormMachineIdCombo.GetValue() > -1
            ? dom.filterFormMachineIdCombo.GetValue()
            : -1;
        state.dateTypeId = dom.filterFormDateCombo.GetValue();
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();



        command.callbackUrl =
            controllerName +
            "DetailGrid" +
            "?machineId=" +
            state.machineId +
            "&persianStartDate=" +
            persianStartDate +
            "&persianEndDate=" +
            persianEndDate +
            "&datePeriodType=" +
            state.datePeriodType;
    }
    function handleGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            command.callbackUrl =
                controllerName +
                "Grid" +
                "?machineId=" +
                -1 +
                "&timeType=" +
                -1 +
                "&persianStartDate=" +
                "" +
                "&persianEndDate=" +
                "" +
                "&datePeriodType=" +
                -1;
            return;
        }

        state.machinId = dom.filterFormMachineIdCombo.GetValue() > -1
            ? dom.filterFormMachineIdCombo.GetValue()
            : -1;
        state.timeType = dom.filterFormMttrMtbfTimeTypeCombo.GetValue() > -1
            ? dom.filterFormMttrMtbfTimeTypeCombo.GetValue()
            : -1;
        state.dateTypeId = dom.filterFormDateCombo.GetValue();
        const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
        const persianEndDate = dom.filterFormPeriodEndDatePicker.val();



        command.callbackUrl =
            controllerName +
            "Grid" +
            "?machineId=" +
            state.machineId +
            "&timeType=" +
            state.timeType +
            "&persianStartDate=" +
            persianStartDate +
            "&persianEndDate=" +
            persianEndDate +
            "&datePeriodType=" +
            state.datePeriodType;
    }



    function init() {
        setDom();
        setEvents();
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
        const isMachineIdSelected = !tools.isNullOrEmpty(machineId);
        if (!isMachineIdSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormMachineIdComboError);
        }

        tools.hideItem(dom.filterFormMttrMtbfTimeTypeComboError);
        const timeType = dom.filterFormMttrMtbfTimeTypeCombo.GetValue();
        const isTimeTypeSelected = !tools.isNullOrEmpty(timeType);
        if (!isTimeTypeSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormMttrMtbfTimeTypeComboError);
        }

        tools.hideItem(dom.filterFormDateComboError);
        const dateId = dom.filterFormDateCombo.GetValue();
        const isDateSelected = !tools.isNullOrEmpty(dateId);
        if (!isDateSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormDateComboError);
        }


        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);

        if (dateId === state.customPeriodId) {
            const periodStart = dom.filterFormPeriodStartDatePicker.val();
            const isPeriodStartSelected = !tools.isNullOrEmpty(periodStart);
            if (!isPeriodStartSelected) {
                isValid = false;
                shouldDisplayValidationErrors && tools.showItem(dom.filterFormPeriodStartDatePickerError);
            }

            const periodEnd = dom.filterFormPeriodEndDatePicker.val();
            const isPeriodEndSelected = !tools.isNullOrEmpty(periodEnd);
            if (!isPeriodEndSelected) {
                isValid = false;
                shouldDisplayValidationErrors && tools.showItem(dom.filterFormPeriodEndDatePickerError);
            };
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

        dom.filterFormMttrMtbfTimeTypeComboParent = $("#filterFormMttrMtbfTimeTypeComboParent");
        dom.filterFormMttrMtbfTimeTypeComboError = $("#filterFormMttrMtbfTimeTypeComboError");
        dom.filterFormMttrMtbfTimeTypeCombo = ASPxClientComboBox.Cast("filterFormMttrMtbfTimeTypeCombo");


        dom.filterFormDateCombo = ASPxClientComboBox.Cast("filterFormDateCombo");
        dom.filterFormDateComboParent = $("#filterFormDateComboParent");
        dom.filterFormDateComboError = $("#filterFormDateComboError");

        dom.filterFormPeriodStartDatePicker = $("#filterFormPeriodStartDatePicker");
        dom.filterFormPeriodStartDatePickerParent = $("#filterFormPeriodStartDatePickerParent");
        dom.filterFormPeriodStartDatePickerError = $("#filterFormPeriodStartDatePickerError");

        dom.filterFormPeriodEndDatePicker = $("#filterFormPeriodEndDatePicker");
        dom.filterFormPeriodEndDatePickerParent = $("#filterFormPeriodEndDatePickerParent");
        dom.filterFormPeriodEndDatePickerError = $("#filterFormPeriodEndDatePickerError");


        //Grid
        dom.gridParent = $("#gridParent");
        dom.grid = ASPxClientGridView.Cast("grid");
        dom.detailGrid = ASPxClientGridView.Cast("detailGrid");
        dom.detailGridParent = $("#detailGridParent");

    }

    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);

        dom.filterFormDateCombo.SetEnabled(false);
        dom.filterFormMttrMtbfTimeTypeCombo.SetEnabled(false);
        tools.disableItem(dom.filterFormPeriodStartDatePicker);
        tools.disableItem(dom.filterFormPeriodEndDatePicker);

    }

    $(document).ready(init);

    return {
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormDepartmentIdComboSelectedIndexChange: handleFilterFormDepartmentIdComboSelectedIndexChange,
        handleFilterFormSubDepartmentIdComboBeginCallback: handleFilterFormSubDepartmentIdComboBeginCallback,
        handleFilterFormSubDepartmentIdComboSelectedIndexChange: handleFilterFormSubDepartmentIdComboSelectedIndexChange,
        handleFilterFormMachineIdComboBeginCallback: handleFilterFormMachineIdComboBeginCallback,
        handleFilterFormMachineIdComboSelectedIndexChange: handleFilterFormMachineIdComboSelectedIndexChange,
        handleFilterFormMttrMtbfTimeTypeComboSelectedIndexChange: handleFilterFormMttrMtbfTimeTypeComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleGridBeginCallback: handleGridBeginCallback,
        handleDetailGridBeginCallback: handleDetailGridBeginCallback
    };
})();