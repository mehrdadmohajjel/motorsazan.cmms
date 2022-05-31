/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.oEEReport = (function () {

    var dom = {};
    var state = {
        specialDateTypeFilter: "CustomPeriod",
        selectByMachine: "SelectWithMachine",
        selectByDepartment: "SelectWithDepartment",
        selectByAll: "SpecialPeriodDate"
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/OEEReport/";

    async function fillGridWithDepartment() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "FillResultGridWithDepartment";

        const departmentId = dom.filterFormGetSubDepartmentListByMainDepartmentCombo.GetValue();

        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const apiParam = {
            departmentId: departmentId,
            datePeriodType: datePeriodType,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.resultGridParent.html(gridHtml);
        setDom();
    }

    async function fillGridWithGeneralSelection() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "FillResultGrid";

        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const apiParam = {
            datePeriodType: datePeriodType,
            persianStartDate: persianStartDate,
            persianEndDate: persianEndDate
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.resultGridParent.html(gridHtml);
        setDom();
    }

    async function fillGridWithMachine() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "FillResultGridWithMachine";

        const machineId = dom.filterFormMachineListCombo.GetValue();

        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        const apiParam = {
            machineId: machineId,
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

        const filterTypeSelect = dom.filterFormFilterTypeSelectCombo.GetValue();

        if (isSpecialTypeSelected) {
            tools.showItem(dom.filterFormSpecialDateColumn);

            tools.hideItem(dom.filterFormStartDateError);
            tools.hideItem(dom.filterFormEndDateError);

            if (filterTypeSelect === state.selectByMachine) {
                dom.filterFormDateTypeComboParentDesign.addClass("col-md-3");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-4");

                dom.filterFormSpecialDateColumn.addClass("col-md-6");
                dom.filterFormSpecialDateColumn.removeClass("col-md-4");

            } else if (filterTypeSelect === state.selectByDepartment) {
                dom.filterFormDateTypeComboParentDesign.addClass("col-md-4");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-3");

                dom.filterFormSpecialDateColumn.addClass("col-md-4");
                dom.filterFormSpecialDateColumn.removeClass("col-md-6");

            } else {
                dom.filterFormDateTypeComboParentDesign.addClass("col-md-3");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-4");

                dom.filterFormSpecialDateColumn.addClass("col-md-4");
                dom.filterFormSpecialDateColumn.removeClass("col-md-6");
            }

        } else {
            tools.hideItem(dom.filterFormSpecialDateColumn);
            dom.filterFormStartDate.val("");
            dom.filterFormEndDate.val("");
            if (filterTypeSelect === state.selectByMachine) {
                dom.filterFormDateTypeComboParentDesign.addClass("col-md-3");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-4");

                dom.filterFormSpecialDateColumn.addClass("col-md-6");
                dom.filterFormSpecialDateColumn.removeClass("col-md-4");

            } else if (filterTypeSelect === state.selectByDepartment) {
                dom.filterFormDateTypeComboParentDesign.addClass("col-md-4");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-3");

                dom.filterFormSpecialDateColumn.addClass("col-md-6");
                dom.filterFormSpecialDateColumn.removeClass("col-md-4");

            } else {
                dom.filterFormDateTypeComboParentDesign.addClass("col-md-3");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-4");

                dom.filterFormSpecialDateColumn.addClass("col-md-6");
                dom.filterFormSpecialDateColumn.removeClass("col-md-4");

            }
        }
    }

    function handleFilterFormFilterTypeSelectComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormFilterTypeSelectComboError);
        const filterTypeSelect = dom.filterFormFilterTypeSelectCombo.GetValue();
        const filterTypeSelectHasValue = !tools.isNullOrEmpty(filterTypeSelect);
        if (!filterTypeSelectHasValue) {
            tools.showItem(dom.filterFormFilterTypeSelectComboError);
        }

        switch (filterTypeSelect) {
            case state.selectByMachine:
                tools.hideItem(dom.filterFormWithDepartment);
                tools.showItem(dom.filterFormWithMachine);

                tools.hideItem(dom.filterFormGetDepartmentListHasMachineComboError);
                tools.hideItem(dom.filterFormGetSubDepartmentListHasMachineComboError);
                tools.hideItem(dom.filterFormMachineListComboError);
                tools.hideItem(dom.filterFormDateTypeComboError);

                dom.filterFormGetAllMainDepartmentListCombo.SetSelectedIndex(-1);
                dom.filterFormGetSubDepartmentListByMainDepartmentCombo.SetSelectedIndex(-1);

                dom.filterFormGetDepartmentListHasMachineCombo.SetSelectedIndex(-1);
                dom.filterFormGetSubDepartmentListHasMachineCombo.SetSelectedIndex(-1);
                dom.filterFormMachineListCombo.SetSelectedIndex(-1);
                dom.filterFormDateTypeCombo.SetSelectedIndex(-1);

                handleFilterFormDateTypeComboSelectedIndexChanged();

                dom.filterFormGetSubDepartmentListHasMachineCombo.SetEnabled(false);
                dom.filterFormMachineListCombo.SetEnabled(false);
                dom.filterFormDateTypeCombo.SetEnabled(false);

                dom.filterFormFilterTypeSelectComboParentDesign.addClass("col-md-3");
                dom.filterFormFilterTypeSelectComboParentDesign.removeClass("col-md-4");

                dom.filterFormDateTypeComboParentDesign.addClass("col-md-3");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-4");
                break;

            case state.selectByDepartment:
                tools.hideItem(dom.filterFormWithMachine);
                tools.showItem(dom.filterFormWithDepartment);

                tools.hideItem(dom.filterFormGetAllMainDepartmentListComboError);
                tools.hideItem(dom.filterFormGetSubDepartmentListByMainDepartmentComboError);
                tools.hideItem(dom.filterFormDateTypeComboError);

                dom.filterFormGetAllMainDepartmentListCombo.SetSelectedIndex(-1);
                dom.filterFormGetSubDepartmentListByMainDepartmentCombo.SetSelectedIndex(-1);

                dom.filterFormGetDepartmentListHasMachineCombo.SetSelectedIndex(-1);
                dom.filterFormGetSubDepartmentListHasMachineCombo.SetSelectedIndex(-1);
                dom.filterFormMachineListCombo.SetSelectedIndex(-1);
                dom.filterFormDateTypeCombo.SetSelectedIndex(-1);

                handleFilterFormDateTypeComboSelectedIndexChanged();

                dom.filterFormGetSubDepartmentListByMainDepartmentCombo.SetEnabled(false);
                dom.filterFormDateTypeCombo.SetEnabled(false);

                dom.filterFormFilterTypeSelectComboParentDesign.addClass("col-md-4");
                dom.filterFormFilterTypeSelectComboParentDesign.removeClass("col-md-3");

                dom.filterFormDateTypeComboParentDesign.addClass("col-md-4");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-3");
                break;

            default:
                tools.hideItem(dom.filterFormWithMachine);
                tools.hideItem(dom.filterFormWithDepartment);

                tools.hideItem(dom.filterFormDateTypeComboError);

                dom.filterFormGetAllMainDepartmentListCombo.SetSelectedIndex(-1);
                dom.filterFormGetSubDepartmentListByMainDepartmentCombo.SetSelectedIndex(-1);

                dom.filterFormGetDepartmentListHasMachineCombo.SetSelectedIndex(-1);
                dom.filterFormGetSubDepartmentListHasMachineCombo.SetSelectedIndex(-1);
                dom.filterFormMachineListCombo.SetSelectedIndex(-1);
                dom.filterFormDateTypeCombo.SetSelectedIndex(-1);

                handleFilterFormDateTypeComboSelectedIndexChanged();

                dom.filterFormDateTypeCombo.SetEnabled(true);

                dom.filterFormFilterTypeSelectComboParentDesign.addClass("col-md-3");
                dom.filterFormFilterTypeSelectComboParentDesign.removeClass("col-md-4");

                dom.filterFormDateTypeComboParentDesign.addClass("col-md-3");
                dom.filterFormDateTypeComboParentDesign.removeClass("col-md-4");
                break;
        }
    }

    function handleFilterFormGetAllMainDepartmentListComboBeginCallback(command) {

        const url = controllerName + "FilterFormGetAllMainDepartmentList";

        command.callbackUrl = url;
    }

    async function handleFilterFormGetAllMainDepartmentListComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormGetAllMainDepartmentListComboError);

        dom.filterFormGetSubDepartmentListByMainDepartmentCombo.SetSelectedIndex(-1);
        dom.filterFormDateTypeCombo.SetSelectedIndex(-1);

        dom.filterFormGetSubDepartmentListByMainDepartmentCombo.SetEnabled(true);
        dom.filterFormDateTypeCombo.SetEnabled(false);

        const departmentId = dom.filterFormGetAllMainDepartmentListCombo.GetValue();

        const url = controllerName + "FilterFormGetSubDepartmentListByMainDepartment";

        const apiParam = {
            departmentId: departmentId
        };

        handleFilterFormDateTypeComboSelectedIndexChanged();

        const subDepartmentListCombo = await motorsazanClient.connector.post(url, apiParam);

        dom.filterFormGetSubDepartmentListByMainDepartmentComboParent.html(subDepartmentListCombo);
        setDom();
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

    function handleFilterFormGetSubDepartmentListByMainDepartmentComboBeginCallback(command) {

        const departmentId = dom.filterFormGetAllMainDepartmentListCombo.GetValue();
        const isDepartmentIdSelected = !tools.isNullOrEmpty(departmentId);
        if (isDepartmentIdSelected) {
            command.callbackUrl =
                controllerName +
                "FilterFormGetSubDepartmentListByMainDepartment" +
                "?DepartmentId=" +
                departmentId;
        }
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

        const filterTypeSelect = dom.filterFormFilterTypeSelectCombo.GetValue();

        switch (filterTypeSelect) {
        case state.selectByMachine:
            fillGridWithMachine();
            break;
        case state.selectByDepartment:
            fillGridWithDepartment();
            break;
        default:
            fillGridWithGeneralSelection();
            break;
        }
    }

    function handleResultGridBeginCallback(command) {
        if (!isFilterFormValid()) {
            const filterTypeSelect = dom.filterFormFilterTypeSelectCombo.GetValue();
            switch (filterTypeSelect) {
            case state.selectByMachine:
                const machineId = dom.filterFormMachineListCombo.GetValue();
                url = controllerName +
                    "FillResultGridWithMachine" +
                    "?machineId=" +
                    -1 +
                    "&datePeriodType=" +
                    -1 +
                    "&persianStartDate=" +
                    "" +
                    "&persianEndDate="
                    + "";
                command.callbackUrl = url;
                break;

            case state.selectByDepartment:
                const departmentId = dom.filterFormGetSubDepartmentListByMainDepartmentCombo.GetValue();
                url = controllerName +
                    "FillResultGridWithDepartment" +
                    "?departmentId=" + -1 +
                    "&datePeriodType=" + -1 +
                    "&persianStartDate=" + "" +
                    "&persianEndDate=" + "";
                command.callbackUrl = url;
                break;

            default:
                url = controllerName +
                    "FillResultGrid" +
                    "?datePeriodType=" + -1 +
                    "&persianStartDate=" + "" +
                    "&persianEndDate=" + "";
                command.callbackUrl = url;
                break;
            }
            return;
        }
            

        const filterTypeSelect = dom.filterFormFilterTypeSelectCombo.GetValue();

        var url;

        const datePeriodType = dom.filterFormDateTypeCombo.GetValue();
        const persianStartDate = dom.filterFormStartDate.val();
        const persianEndDate = dom.filterFormEndDate.val();

        switch (filterTypeSelect) {
            case state.selectByMachine:
                const machineId = dom.filterFormMachineListCombo.GetValue();
                url = controllerName +
                    "FillResultGridWithMachine" +
                    "?machineId=" +
                    machineId +
                    "&datePeriodType=" +
                    datePeriodType +
                    "&persianStartDate=" +
                    persianStartDate +
                    "&persianEndDate="
                    + persianEndDate;
                command.callbackUrl = url;
                break;

            case state.selectByDepartment:
                const departmentId = dom.filterFormGetSubDepartmentListByMainDepartmentCombo.GetValue();
                url = controllerName +
                    "FillResultGridWithDepartment" +
                    "?departmentId=" +
                    departmentId +
                    "&datePeriodType=" +
                    datePeriodType +
                    "&persianStartDate=" +
                    persianStartDate +
                    "&persianEndDate="
                    + persianEndDate;
                command.callbackUrl = url;
                break;

            default:
                url = controllerName +
                    "FillResultGrid" +
                    "?datePeriodType=" +
                    datePeriodType +
                    "&persianStartDate=" +
                    persianStartDate +
                    "&persianEndDate="
                    + persianEndDate;
                command.callbackUrl = url;
                break;
        }
    }

    function init() {
        setDom();
        setEvents();

        dom.filterFormStartDate.on("change", handleFilterFormPeriodStartDatePickerSelectionChange);
        dom.filterFormEndDate.on("change", handleFilterFormPeriodEndDatePickerSelectionChange);
    }

    function isFilterFormValid() {
        var isValid = true;

        tools.hideItem(dom.filterFormFilterTypeSelectComboError);
        const filterTypeSelect = dom.filterFormFilterTypeSelectCombo.GetValue();
        const filterTypeSelectTypeHasValue = !tools.isNullOrEmpty(filterTypeSelect);
        if (!filterTypeSelectTypeHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormFilterTypeSelectComboError);
        }

        switch (filterTypeSelect) {
            case state.selectByMachine:
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

                break;
            case state.selectByDepartment:
                tools.hideItem(dom.filterFormGetAllMainDepartmentListComboError);
                var departmentIdWithDepartment = dom.filterFormGetAllMainDepartmentListCombo.GetValue();
                var departmentIdWithDepartmentHasValue = !tools.isNullOrEmpty(departmentIdWithDepartment);
                if (!departmentIdWithDepartmentHasValue) {
                    isValid = false;
                    tools.showItem(dom.filterFormGetAllMainDepartmentListComboError);
                }

                tools.hideItem(dom.filterFormGetSubDepartmentListByMainDepartmentComboError);
                var subDepartmentIdWithDepartment = dom.filterFormGetSubDepartmentListByMainDepartmentCombo.GetValue();
                var subDepartmentIdWithDepartmentHasValue = !tools.isNullOrEmpty(subDepartmentIdWithDepartment);
                if (!subDepartmentIdWithDepartmentHasValue) {
                    isValid = false;
                    tools.showItem(dom.filterFormGetSubDepartmentListByMainDepartmentComboError);
                }
                break;
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
        //Filter form
        dom.filterFormFilterTypeSelectComboParentDesign = $("#filterFormFilterTypeSelectComboParentDesign");
        dom.filterFormFilterTypeSelectComboParent = $("#filterFormFilterTypeSelectComboParent");
        dom.filterFormFilterTypeSelectComboError = $("#filterFormFilterTypeSelectComboError");
        dom.filterFormFilterTypeSelectCombo = ASPxClientComboBox.Cast("filterFormFilterTypeSelectCombo");

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

        //Filter form With Department
        dom.filterFormWithDepartment = $("#filterFormWithDepartment");

        dom.filterFormGetAllMainDepartmentListComboParentDesign = $("#filterFormGetAllMainDepartmentListComboParentDesign");
        dom.filterFormGetAllMainDepartmentListComboParent = $("#filterFormGetAllMainDepartmentListComboParent");
        dom.filterFormGetAllMainDepartmentListComboError = $("#filterFormGetAllMainDepartmentListComboError");
        dom.filterFormGetAllMainDepartmentListCombo = ASPxClientComboBox.Cast("filterFormGetAllMainDepartmentListCombo");

        dom.filterFormGetSubDepartmentListByMainDepartmentComboParentDesign = $("#filterFormGetSubDepartmentListByMainDepartmentComboParentDesign");
        dom.filterFormGetSubDepartmentListByMainDepartmentComboParent = $("#filterFormGetSubDepartmentListByMainDepartmentComboParent");
        dom.filterFormGetSubDepartmentListByMainDepartmentComboError = $("#filterFormGetSubDepartmentListByMainDepartmentComboError");
        dom.filterFormGetSubDepartmentListByMainDepartmentCombo = ASPxClientComboBox.Cast("filterFormGetSubDepartmentListByMainDepartmentCombo");

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
        handleFilterFormFilterTypeSelectComboSelectedIndexChange();

        dom.filterFormGetAllMainDepartmentListCombo.SetSelectedIndex(-1);
        dom.filterFormGetSubDepartmentListByMainDepartmentCombo.SetSelectedIndex(-1);

        dom.filterFormGetDepartmentListHasMachineCombo.SetSelectedIndex(-1);
        dom.filterFormGetSubDepartmentListHasMachineCombo.SetSelectedIndex(-1);
        dom.filterFormMachineListCombo.SetSelectedIndex(-1);
        dom.filterFormDateTypeCombo.SetSelectedIndex(-1);

        dom.filterFormGetSubDepartmentListHasMachineCombo.SetEnabled(false);
        dom.filterFormGetSubDepartmentListByMainDepartmentCombo.SetEnabled(false);
        dom.filterFormMachineListCombo.SetEnabled(false);
        dom.filterFormDateTypeCombo.SetEnabled(false);
    }

    function showHelpModal() {
        const url = controllerName + "ShowHelpModal";

        const title = "راهنما";
        motorsazanClient.contentModal.ajaxShow(title, url);
        setDom();
    }

    $(document).ready(init);

    return {
        handleFilterFormDateTypeComboSelectedIndexChanged: handleFilterFormDateTypeComboSelectedIndexChanged,
        handleFilterFormFilterTypeSelectComboSelectedIndexChange: handleFilterFormFilterTypeSelectComboSelectedIndexChange,
        handleFilterFormGetAllMainDepartmentListComboBeginCallback: handleFilterFormGetAllMainDepartmentListComboBeginCallback,
        handleFilterFormGetAllMainDepartmentListComboSelectedIndexChange: handleFilterFormGetAllMainDepartmentListComboSelectedIndexChange,
        handleFilterFormGetDepartmentListHasMachineComboBeginCallback: handleFilterFormGetDepartmentListHasMachineComboBeginCallback,
        handleFilterFormGetDepartmentListHasMachineComboSelectedIndexChange: handleFilterFormGetDepartmentListHasMachineComboSelectedIndexChange,
        handleFilterFormGetSubDepartmentListByMainDepartmentComboBeginCallback: handleFilterFormGetSubDepartmentListByMainDepartmentComboBeginCallback,
        handleFilterFormGetSubDepartmentListByMainDepartmentComboSelectedIndexChange: handleFilterFormGetSubDepartmentListByMainDepartmentComboSelectedIndexChange,
        handleFilterFormGetSubDepartmentListHasMachineComboBeginCallback: handleFilterFormGetSubDepartmentListHasMachineComboBeginCallback,
        handleFilterFormGetSubDepartmentListHasMachineComboSelectedIndexChange: handleFilterFormGetSubDepartmentListHasMachineComboSelectedIndexChange,
        handleFilterFormMachineListComboBeginCallback: handleFilterFormMachineListComboBeginCallback,
        handleFilterFormMachineListComboSelectedIndexChange: handleFilterFormMachineListComboSelectedIndexChange,
        handleFilterFormPeriodEndDatePickerSelectionChange: handleFilterFormPeriodEndDatePickerSelectionChange,
        handleFilterFormPeriodStartDatePickerSelectionChange: handleFilterFormPeriodStartDatePickerSelectionChange,
        handleFilterFormSearchBtnClick: handleFilterFormSearchBtnClick,
        handleResultGridBeginCallback: handleResultGridBeginCallback,
        showHelpModal: showHelpModal
    };
})();