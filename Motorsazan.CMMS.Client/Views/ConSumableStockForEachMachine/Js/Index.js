/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.consumableStockForEachMachine = (function () {

    var dom = {};
    var state = {
        specialDateTypeFilter: "CustomPeriod",
        selectByMachine: "SelectWithMachine",
        selectByDepartment: "SelectWithDepartment",
        selectByAll: "SpecialPeriodDate"
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/consumableStockForEachMachine/";

    async function fillGridWithMachine() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "FillResultGrid";

        const machineId = dom.filterFormMachineListCombo.GetValue();
        const departmentId = dom.filterFormGetSubDepartmentListHasMachineCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const apiParam = {
            machineId: machineId,
            departmentId: departmentId,
            datePeriodType: datePeriodType,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.resultGridParent.html(gridHtml);
        setDom();
    }

    function handleFilterFormDateTypeComboSelectedIndexChanged() {
        tools.hideItem(dom.filterFormDateTypeComboError);

        const activeDateType = dom.filterFormDateTypeCombo.GetValue();
        const isSpecialTypeSelected = activeDateType === state.specialDateTypeFilter;

        const filterTypeSelect = dom.filterFormDateTypeCombo.GetValue();

        if (isSpecialTypeSelected) {
            tools.showItem(dom.filterFormSpecialDateColumn);

            tools.hideItem(dom.filterFormStartDateError);
            tools.hideItem(dom.filterFormEndDateError);

            if (filterTypeSelect === state.selectByMachine) {
                dom.filterFormDateTypeComboParentDesign.addClass("col-md-12");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-12");

                dom.filterFormSpecialDateColumn.addClass("col-md-12");
                dom.filterFormSpecialDateColumn.removeClass("col-md-12");

            } else {
                dom.filterFormDateTypeComboParentDesign.addClass("col-md-12");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-12");

                dom.filterFormSpecialDateColumn.addClass("col-md-12");
                dom.filterFormSpecialDateColumn.removeClass("col-md-12");
            }

        } else {
            tools.hideItem(dom.filterFormSpecialDateColumn);
            dom.filterFormStartDate.val("");
            dom.filterFormEndDate.val("");
            if (filterTypeSelect === state.selectByMachine) {
                dom.filterFormDateTypeComboParentDesign.addClass("col-md-12");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-12");

                dom.filterFormSpecialDateColumn.addClass("col-md-12");
                dom.filterFormSpecialDateColumn.removeClass("col-md-12");

            }  else {
                dom.filterFormDateTypeComboParentDesign.addClass("col-md-12");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-12");

                dom.filterFormSpecialDateColumn.addClass("col-md-12");
                dom.filterFormSpecialDateColumn.removeClass("col-md-12");

            }
        }
    }

    function handleFilterFormGetDepartmentListHasMachineComboBeginCallback(command) {

        const subDepartmentId = dom.filterFormSubDepartmentIdCombo.GetValue();
        const isSubDepartmentIdSelected = !tools.isNullOrEmpty(subDepartmentId);
        if (isSubDepartmentIdSelected) {
            command.callbackUrl =
                controllerName + "FilterFormGetDepartmentListHasMachine";
        }
    }

    async function handleFilterFormGetDepartmentListHasMachineComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormGetDepartmentListHasMachineComboError);

        dom.filterFormGetSubDepartmentListHasMachineCombo.SetSelectedIndex(-1);
        dom.filterFormMachineListCombo.SetSelectedIndex(-1);
        dom.filterFormDateTypeCombo.SetSelectedIndex(-1);

        dom.filterFormGetSubDepartmentListHasMachineCombo.SetEnabled(true);
        dom.filterFormMachineListCombo.SetEnabled(false);
        dom.filterFormDateTypeCombo.SetEnabled(false);

        handleFilterFormDateTypeComboSelectedIndexChanged();

        const departmentId = dom.filterFormGetDepartmentListHasMachineCombo.GetValue();

        const url = controllerName + "FilterFormGetSubDepartmentListHasMachineByMainDepartmentId";

        const apiParam = {
            departmentId: departmentId
        };

        const subDepartmentListCombo = await motorsazanClient.connector.post(url, apiParam);

        dom.filterFormGetSubDepartmentListHasMachineComboParent.html(subDepartmentListCombo);
        setDom();
    }


    async function handleFilterFormGetSubDepartmentListByMainDepartmentComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormGetSubDepartmentListByMainDepartmentComboError);

        dom.filterFormDateTypeCombo.SetSelectedIndex(-1);
        dom.filterFormDateTypeCombo.SetEnabled(true);
        handleFilterFormDateTypeComboSelectedIndexChanged();
        setDom();
    }

    function handleFilterFormGetSubDepartmentListHasMachineComboBeginCallback(command) {

        const departmentId = dom.filterFormGetDepartmentListHasMachineCombo.GetValue();
        const isDepartmentIdSelected = !tools.isNullOrEmpty(departmentId);
        if (isDepartmentIdSelected) {
            command.callbackUrl =
                controllerName +
                "FilterFormGetSubDepartmentListHasMachineByMainDepartmentId" +
                "?DepartmentId=" +
                departmentId;
        }
    }

    async function handleFilterFormGetSubDepartmentListHasMachineComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormGetSubDepartmentListHasMachineComboError);

        dom.filterFormMachineListCombo.SetSelectedIndex(-1);
        dom.filterFormDateTypeCombo.SetSelectedIndex(-1);

        dom.filterFormMachineListCombo.SetEnabled(true);
        dom.filterFormDateTypeCombo.SetEnabled(false);

        const subDepartmentId = dom.filterFormGetSubDepartmentListHasMachineCombo.GetValue();

        const url = controllerName + "FilterFormGetMachineList";

        const apiParam = {
            departmentId: subDepartmentId
        };

        handleFilterFormDateTypeComboSelectedIndexChanged();

        const machineListCombo = await motorsazanClient.connector.post(url, apiParam);

        dom.filterFormMachineListComboParent.html(machineListCombo);
        setDom();
    }

    function handleFilterFormMachineListComboBeginCallback(command) {

        const subDepartmentId = dom.filterFormGetSubDepartmentListHasMachineCombo.GetValue();
        const isSubDepartmentIdSelected = !tools.isNullOrEmpty(subDepartmentId);
        if (isSubDepartmentIdSelected) {
            command.callbackUrl =
                controllerName +
                "FilterFormGetMachineList" +
                "?DepartmentId=" + subDepartmentId;
        }
    }
    function handleFilterFormMachineListComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormMachineListComboError);

        dom.filterFormDateTypeCombo.SetSelectedIndex(-1);
        handleFilterFormDateTypeComboSelectedIndexChanged();
        dom.filterFormDateTypeCombo.SetEnabled(true);
    }
    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormEndDateError);
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormStartDateError);
    }

    function handleFilterFormSearchBtnClick() {
        if (!isFilterFormValid()) return;


        fillGridWithMachine();

    }

    function handleResultGridBeginCallback(command) {
        if (!isFilterFormValid()) {
                url = controllerName +
                    "FillResultGrid" +
                    "?machineId=" +
                    -1 +
                    "&departmentId=" +
                    -1 +
                    "&datePeriodType=" +
                    -1 +
                    "&persianStartDate=" +
                    "" +
                    "&persianEndDate="
                    + "";
                command.callbackUrl = url;
            return;
         }

        var url;
        const machineId = dom.filterFormMachineListCombo.GetValue();
        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();
        const departmentId = dom.filterFormGetSubDepartmentListHasMachineCombo.GetValue();
                url = controllerName +
                    "FillResultGrid" +
                    "?machineId=" +
                    machineId +
                    "&departmentId=" +
                    departmentId +
                    "&datePeriodType=" +
                    datePeriodType +
                    "&persianStartDate=" +
                    persianStartDate +
                    "&persianEndDate="
                    + persianEndDate;
                command.callbackUrl = url;
    }

    function init() {
        setDom();
        setEvents();

        dom.filterFormStartDate.on("change", handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormEndDate.on("change", handleFilterFormPeriodEndDatePickerSelectionChange);
    }

    function isFilterFormValid() {
        var isValid = true;

        tools.hideItem(dom.filterFormDateTypeComboError);
        const filterTypeSelect = dom.filterFormDateTypeCombo.GetValue();
        const filterTypeSelectTypeHasValue = !tools.isNullOrEmpty(filterTypeSelect);
        if (!filterTypeSelectTypeHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormDateTypeComboError);
        }

        tools.hideItem(dom.filterFormGetDepartmentListHasMachineComboError);
        var departmentIdWithMachine = dom.filterFormGetDepartmentListHasMachineCombo.GetValue();
        var departmentIdWithMachineHasValue = !tools.isNullOrEmpty(departmentIdWithMachine);
        if (!departmentIdWithMachineHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormGetDepartmentListHasMachineComboError);
        }

        tools.hideItem(dom.filterFormGetSubDepartmentListHasMachineComboError);
        var subDepartmentIdWithMachine = dom.filterFormGetSubDepartmentListHasMachineCombo.GetValue();
        var subDepartmentIdWithMachineHasValue = !tools.isNullOrEmpty(subDepartmentIdWithMachine);
        if (!subDepartmentIdWithMachineHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormGetSubDepartmentListHasMachineComboError);
        }

        tools.hideItem(dom.filterFormMachineListComboError);
        var machineId = dom.filterFormMachineListCombo.GetValue();
        var machineIdHasValue = !tools.isNullOrEmpty(machineId);
        if (!machineIdHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormMachineListComboError);
        }

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

    function setDom() {

        //Filter form With Machine
        dom.filterFormWithMachine = $("#filterFormWithMachine");

        dom.filterFormGetDepartmentListHasMachineComboParentDesign = $("#filterFormGetDepartmentListHasMachineComboParentDesign");
        dom.filterFormGetDepartmentListHasMachineComboParent = $("#filterFormGetDepartmentListHasMachineComboParent");
        dom.filterFormGetDepartmentListHasMachineComboError = $("#filterFormGetDepartmentListHasMachineComboError");
        dom.filterFormGetDepartmentListHasMachineCombo = ASPxClientComboBox.Cast("filterFormGetDepartmentListHasMachineCombo");

        dom.filterFormGetSubDepartmentListHasMachineComboParentDesign = $("#filterFormGetSubDepartmentListHasMachineComboParentDesign");
        dom.filterFormGetSubDepartmentListHasMachineComboParent = $("#filterFormGetSubDepartmentListHasMachineComboParent");
        dom.filterFormGetSubDepartmentListHasMachineComboError = $("#filterFormGetSubDepartmentListHasMachineComboError");
        dom.filterFormGetSubDepartmentListHasMachineCombo = ASPxClientComboBox.Cast("filterFormGetSubDepartmentListHasMachineCombo");

        dom.filterFormMachineListComboParentDesign = $("#filterFormMachineListComboParentDesign");
        dom.filterFormMachineListComboParent = $("#filterFormMachineListComboParent");
        dom.filterFormMachineListComboError = $("#filterFormMachineListComboError");
        dom.filterFormMachineListCombo = ASPxClientComboBox.Cast("filterFormMachineListCombo");


        //General item
        dom.filterFormDateTypeComboParentDesign = $("#filterFormDateTypeComboParentDesign");
        dom.filterFormDateTypeComboParent = $("#filterFormDateTypeComboParent");
        dom.filterFormDateTypeComboError = $("#filterFormDateTypeComboError");
        dom.filterFormDateTypeCombo = ASPxClientComboBox.Cast("filterFormDateTypeCombo");

        dom.filterFormSpecialDateColumn = $("#filterFormSpecialDateColumn");

        dom.filterFormStartDateError = $("#filterFormStartDateError");
        dom.filterFormStartDate = $("#filterFormStartDate");

        dom.filterFormEndDateError = $("#filterFormEndDateError");
        dom.filterFormEndDate = $("#filterFormEndDate");

        //Grid
        dom.resultGridParent = $("#resultGridParent");
        dom.resultGrid = ASPxClientGridView.Cast("resultGrid");
    }

    function setEvents() {

        dom.filterFormGetDepartmentListHasMachineCombo.SetSelectedIndex(-1);
        dom.filterFormGetSubDepartmentListHasMachineCombo.SetSelectedIndex(-1);
        dom.filterFormMachineListCombo.SetSelectedIndex(-1);
        dom.filterFormDateTypeCombo.SetSelectedIndex(-1);

        dom.filterFormGetSubDepartmentListHasMachineCombo.SetEnabled(false);
        dom.filterFormMachineListCombo.SetEnabled(false);
        dom.filterFormDateTypeCombo.SetEnabled(false);
    }



    $(document).ready(init);

    return {
        handleFilterFormDateTypeComboSelectedIndexChanged: handleFilterFormDateTypeComboSelectedIndexChanged,
        handleFilterFormGetDepartmentListHasMachineComboBeginCallback: handleFilterFormGetDepartmentListHasMachineComboBeginCallback,
        handleFilterFormGetDepartmentListHasMachineComboSelectedIndexChange: handleFilterFormGetDepartmentListHasMachineComboSelectedIndexChange,
        handleFilterFormGetSubDepartmentListByMainDepartmentComboSelectedIndexChange: handleFilterFormGetSubDepartmentListByMainDepartmentComboSelectedIndexChange,
        handleFilterFormGetSubDepartmentListHasMachineComboBeginCallback: handleFilterFormGetSubDepartmentListHasMachineComboBeginCallback,
        handleFilterFormGetSubDepartmentListHasMachineComboSelectedIndexChange: handleFilterFormGetSubDepartmentListHasMachineComboSelectedIndexChange,
        handleFilterFormMachineListComboBeginCallback: handleFilterFormMachineListComboBeginCallback,
        handleFilterFormPeriodEndDatePickerSelectionChange: handleFilterFormPeriodEndDatePickerSelectionChange,
        handleFilterFormPeriodStartDatePickerSelectionChange: handleFilterFormPeriodStartDatePickerSelectionChange,
        handleFilterFormSearchBtnClick: handleFilterFormSearchBtnClick,
        handleResultGridBeginCallback: handleResultGridBeginCallback,
        handleFilterFormMachineListComboSelectedIndexChange: handleFilterFormMachineListComboSelectedIndexChange
    };
})();