/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.mainMachineOilServiceReport = (function () {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod"
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/MainMachineOilServiceReport/";

    async function fillGrid() {
        if (!isFilterFormValid(false))
            return;

        const url = controllerName + "Grid";

        const subDepartmentId = dom.filterFormSubDepartmentIdCombo.GetValue();
        const machineId = dom.filterFormMachineIdCombo.GetValue();
        const datePeriodType = dom.filterFormDateCombo.GetValue();
        const periodStartDate = dom.filterFormPeriodStartDatePicker.val();
        const periodEndDate = dom.filterFormPeriodEndDatePicker.val();


        const apiParam = {
            departmentId: subDepartmentId,
            machineId: machineId,
            startDate: periodStartDate,
            endDate: periodEndDate,
            datePeriodType: datePeriodType
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.gridParent.html(gridHtml);
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
        const subDepartmentListHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.filterFormSubDepartmentIdComboParent.html(subDepartmentListHtml);
        setDom();

        dom.filterFormSubDepartmentIdCombo.SetEnabled(true);
        dom.filterFormMachineIdCombo.SetEnabled(false);
        dom.filterFormDateCombo.SetEnabled(false);

        dom.filterFormMachineIdCombo.SetSelectedIndex(-1);
        dom.filterFormDateCombo.SetSelectedIndex(-1);
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

        const subDepartmentId = dom.filterFormSubDepartmentIdCombo.GetValue();

        const apiParam = {
            departmentId: subDepartmentId
        };

        const machineListHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.filterFormMachineIdComboParent.html(machineListHtml);
        setDom();

        dom.filterFormMachineIdCombo.SetEnabled(true);
    }

    function handleGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            command.callbackUrl =
                controllerName +
                "Grid" +
                "?machineId=" +
                -1 +
                "&departmentId=" +
                -1 +
                "&startDate=" +
                "" +
                "&endDate=" +
                "" +
                "&datePeriodType=" +
                7;
            return;
        }

        var subDepartmentId =
            +dom.filterFormSubDepartmentIdCombo.GetValue() > -1
                ? dom.filterFormSubDepartmentIdCombo.GetValue()
                : -1;
        var machineId =
            +dom.filterFormMachineIdCombo.GetValue() > -1
                ? dom.filterFormMachineIdCombo.GetValue()
                : -1;

        var dateTypeId = dom.filterFormDateCombo.GetValue();
        const periodStartDate = dom.filterFormPeriodStartDatePicker.val();
        const periodEndDate = dom.filterFormPeriodEndDatePicker.val();

        if (dateTypeId == null ||
            (dateTypeId === state.customPeriodId &&
                (tools.isNullOrEmpty(periodStartDate) || tools.isNullOrEmpty(periodEndDate)))) {
            subDepartmentId = -1;
            machineId = -1;
            dateTypeId = -1;
        }

        command.callbackUrl =
            controllerName +
            "Grid" +
            "?machineId=" +
            machineId +
            "&departmentId=" +
            subDepartmentId +
            "&startDate=" +
            periodStartDate +
            "&endDate=" +
            periodEndDate +
            "&datePeriodType=" +
            dateTypeId;
    }

    function init() {
        setDom();
        setEvents();

        dom.filterFormSubDepartmentIdCombo.SetEnabled(false);
        dom.filterFormMachineIdCombo.SetEnabled(false);
        dom.filterFormDateCombo.SetEnabled(false);
        tools.disableItem(dom.filterFormPeriodEndDatePicker);
        tools.disableItem(dom.filterFormPeriodStartDatePicker);
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

        dom.filterFormDateCombo = ASPxClientComboBox.Cast("filterFormDateCombo");
        dom.filterFormDateComboParent = $("#filterFormDateComboParent");
        dom.filterFormDateComboError = $("#filterFormDateComboError");

        dom.filterFormPeriodStartDatePicker = $("#filterFormPeriodStartDatePicker");
        dom.filterFormPeriodStartDatePickerParent = $("#filterFormPeriodStartDatePickerParent");
        dom.filterFormPeriodStartDatePickerError = $("#filterFormPeriodStartDatePickerError");

        dom.filterFormPeriodEndDatePicker = $("#filterFormPeriodEndDatePicker");
        dom.filterFormPeriodEndDatePickerParent = $("#filterFormPeriodEndDatePickerParent");
        dom.filterFormPeriodEndDatePickerError = $("#filterFormPeriodEndDatePickerError");
        dom.filterFormBeingItemsDivCenter = $("#filterFormBeingItemsDivCenter");

        //Grid
        dom.gridParent = $("#gridParent");
        dom.grid = ASPxClientGridView.Cast("grid");
    }

    function setEvents() {
        dom.filterFormPeriodStartDatePicker.change(handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormPeriodEndDatePicker.change(handleFilterFormPeriodEndDatePickerSelectionChange);
    }

    $(document).ready(init);

    return {
        handleFilterFormDateComboSelectedIndexChange: handleFilterFormDateComboSelectedIndexChange,
        handleFilterFormDepartmentIdComboSelectedIndexChange: handleFilterFormDepartmentIdComboSelectedIndexChange,
        handleFilterFormMachineIdComboBeginCallback: handleFilterFormMachineIdComboBeginCallback,
        handleFilterFormMachineIdComboSelectedIndexChange: handleFilterFormMachineIdComboSelectedIndexChange,
        handleFilterFormSearchButtonClick: handleFilterFormSearchButtonClick,
        handleFilterFormSubDepartmentIdComboBeginCallback: handleFilterFormSubDepartmentIdComboBeginCallback,
        handleFilterFormSubDepartmentIdComboSelectedIndexChange:
            handleFilterFormSubDepartmentIdComboSelectedIndexChange,
        handleGridBeginCallback: handleGridBeginCallback
    };
})();