///<reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.costReportByWorkOrder = (function() {

    var dom = {};
    var state = {
        customPeriodId: "CustomPeriod",
        idOfTicketItemTypeWithPeriodInMinute: 1
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/CostReportByWorkOrder/";

    function fillGrid() {
        const url = controllerName + "Grid";

        if (dom.filterFormSearchTypeSelectionRadioButtonList.GetSelectedIndex() === 0) {
            if (!isFilterFormByDepartmentValid(false))
                return;

            const departmentId = dom.filterFormDepartmentIdCombo.GetValue();
            const subDepartmentId = dom.filterFormSubDepartmentIdCombo.GetValue();
            const datePeriodType = dom.filterFormDateCombo.GetValue();
            const persianStartDate = dom.filterFormPeriodStartDatePicker.val();
            const persianEndDate = dom.filterFormPeriodEndDatePicker.val();
            const apiParam = {
                departmentId: departmentId,
                subDepartmentId: subDepartmentId,
                persianStartDate: persianStartDate,
                persianEndDate: persianEndDate,
                datePeriodType: datePeriodType,
                workOrderId: -1
            };

            motorsazanClient.connector.post(url, apiParam)
                .then(function(grid) {
                    dom.gridParent.html(grid);
                    setDom();
                });
        } else if (dom.filterFormSearchTypeSelectionRadioButtonList.GetSelectedIndex() === 1) {
            if (!isFilterFormByWorkOrderValid(false))
                return;

            const workOrderId = dom.filterFormWorkOrderIdTextBox.GetValue();
            const apiParam2 = {
                departmentId: -1,
                subDepartmentId: -1,
                persianStartDate: "",
                persianEndDate: "",
                datePeriodType: "CurrentDay",
                workOrderId: workOrderId
            };


            motorsazanClient.connector.post(url, apiParam2)
                .then(function(grid) {
                    dom.gridParent.html(grid);
                    setDom();
                });
        }
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

    function handleFilterFormPeriodEndDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodEndDatePickerError);
    }

    function handleFilterFormSearchTypeSelectionRadioButtonListSelectedIndexChanged() {
        if (dom.filterFormSearchTypeSelectionRadioButtonList.GetSelectedIndex() === 0) {
            tools.showItem(dom.selectByDepertment);
            tools.hideItem(dom.selectByWorkOrder);
        } else if (dom.filterFormSearchTypeSelectionRadioButtonList.GetSelectedIndex() === 1) {
            tools.showItem(dom.selectByWorkOrder);
            tools.hideItem(dom.selectByDepertment);
        }
    }

    function handleFilterFormPeriodStartDatePickerSelectionChange() {
        tools.hideItem(dom.filterFormPeriodStartDatePickerError);
    }

    function handleFilterFormTicketItemTypeComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormTicketItemTypeComboError);
        dom.filterFormDateCombo.SetEnabled(true);
    }

    function handleFilterFormDepartmentIdComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDepartmentIdComboError);
        const url = controllerName + "FilterFormSubDepartmentIdCombo";

        const departmentId = dom.filterFormDepartmentIdCombo.GetValue();


        const apiParam = {
            departmentId: departmentId
        };

        motorsazanClient.connector.post(url, apiParam)
            .then(function(apiResult) {
                dom.filterFormSubDepartmentIdComboParent.html(apiResult);
                setDom();
            });

    }

    function handleFilterFormSubDepartmentIdComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormDepartmentIdComboError);
        tools.hideItem(dom.filterFormSubDepartmentIdComboError);
        dom.filterFormDateCombo.SetEnabled(true);
    }

    function handleFilterFormSearchByDepartmentButtonClick() {
        if (!isFilterFormByDepartmentValid())
            return;

        fillGrid();
    }

    function handleFilterFormSearchByWorkOrderButtonClick() {
        if (!isFilterFormByWorkOrderValid())
            return;

        fillGrid();
    }

    function handleGridBeginCallback(command) {
        var departmentId;
        var subDepartmentId;
        var datePeriodType;
        var persianStartDate;
        var persianEndDate;
        if (dom.filterFormSearchTypeSelectionRadioButtonList.GetSelectedIndex() === 0) {
            departmentId = +dom.filterFormDepartmentIdCombo.GetValue() > -1
                ? dom.filterFormDepartmentIdCombo.GetValue()
                : -1;
            subDepartmentId = +dom.filterFormSubDepartmentIdCombo.GetValue() > -1
                ? dom.filterFormSubDepartmentIdCombo.GetValue()
                : -1;

            datePeriodType = dom.filterFormDateCombo.GetValue();
            persianStartDate = dom.filterFormPeriodStartDatePicker.val();
            persianEndDate = dom.filterFormPeriodEndDatePicker.val();

            if (datePeriodType == null ||
            (datePeriodType === state.customPeriodId &&
                (tools.isNullOrEmpty(persianStartDate) || tools.isNullOrEmpty(persianEndDate)))) {
                departmentId = -1;
                subDepartmentId = -1;
                datePeriodType = -1;
            }

            command.callbackUrl =
                controllerName +
                "Grid" +
                "?departmentId=" +
                departmentId +
                "&subDepartmentId=" +
                subDepartmentId +
                "&persianStartDate=" +
                persianStartDate +
                "&persianEndDate=" +
                persianEndDate +
                "&datePeriodType=" +
                datePeriodType +
                "&workOrderId=" +
                -1;

        }
        if (dom.filterFormSearchTypeSelectionRadioButtonList.GetSelectedIndex() === 1) {

            const workOrderId = +dom.filterFormWorkOrderIdTextBox.GetValue() > -1
                ? dom.filterFormWorkOrderIdTextBox.GetValue()
                : -1;
            command.callbackUrl =
                controllerName +
                "Grid" +
                "?departmentId=" +
                -1 +
                "&subDepartmentId=" +
                -1 +
                "&persianStartDate=" +
                "" +
                "&persianEndDate=" +
                "" +
                "&datePeriodType=" +
                "CurrentDay" +
                "&workOrderId=" +
                workOrderId;
        }
    }

    function init() {
        setDom();
        setEvents();

        tools.hideItem(dom.selectByDepertment);
        tools.hideItem(dom.selectByWorkOrder);
        dom.filterFormSubDepartmentIdCombo.SetEnabled(false);
        dom.filterFormDateCombo.SetEnabled(false);
        tools.disableItem(dom.filterFormPeriodStartDatePicker);
        tools.disableItem(dom.filterFormPeriodEndDatePicker);
    }

    function isFilterFormByDepartmentValid(shouldDisplayValidationErrors = true) {
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

    function isFilterFormByWorkOrderValid(shouldDisplayValidationErrors = true) {
        var isValid = true;

        tools.hideItem(dom.filterFormWorkOrderIdTextBoxError);
        const workOrderId = dom.filterFormWorkOrderIdTextBox.GetValue();
        const isWorkOrderIdSelected = !tools.isNullOrEmpty(workOrderId);
        if (!isWorkOrderIdSelected) {
            isValid = false;
            shouldDisplayValidationErrors && tools.showItem(dom.filterFormWorkOrderIdTextBoxError);
        }

        return isValid;
    }

    function setDom() {
        //FilterForm
        dom.selectByDepertment = $("#selectByDepartment");
        dom.selectByWorkOrder = $("#selectByWorkOrder");

        dom.filterFormDepartmentIdCombo = ASPxClientComboBox.Cast("filterFormDepartmentIdCombo");
        dom.filterFormDepartmentIdComboParent = $("#filterFormDepartmentIdComboParent");
        dom.filterFormDepartmentIdComboError = $("#filterFormDepartmentIdComboError");


        dom.filterFormSubDepartmentIdCombo = ASPxClientComboBox.Cast("filterFormSubDepartmentIdCombo");
        dom.filterFormSubDepartmentIdComboParent = $("#filterFormSubDepartmentIdComboParent");
        dom.filterFormSubDepartmentIdComboError = $("#filterFormSubDepartmentIdComboError");

        dom.filterFormSearchTypeSelectionRadioButtonList =
            ASPxClientRadioButtonList.Cast("filterFormSearchTypeSelectionRadioButtonList");

        dom.filterFormDateCombo = ASPxClientComboBox.Cast("filterFormDateCombo");
        dom.filterFormWorkOrderIdTextBox = ASPxClientTextBox.Cast("filterFormWorkOrderIdTextBox");
        dom.filterFormWorkOrderIdTextBoxError = $("#filterFormWorkOrderIdTextBoxError");
        dom.filterFormWorkOrderIdTextBoxParent = $("#filterFormWorkOrderIdTextBoxParent");


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
        handleFilterFormTicketItemTypeComboSelectedIndexChange: handleFilterFormTicketItemTypeComboSelectedIndexChange,
        handleFilterFormDepartmentIdComboSelectedIndexChange: handleFilterFormDepartmentIdComboSelectedIndexChange,
        handleFilterFormSubDepartmentIdComboSelectedIndexChange:
            handleFilterFormSubDepartmentIdComboSelectedIndexChange,
        handleFilterFormSearchByDepartmentButtonClick: handleFilterFormSearchByDepartmentButtonClick,
        handleFilterFormSearchByWorkOrderButtonClick: handleFilterFormSearchByWorkOrderButtonClick,
        handleGridBeginCallback: handleGridBeginCallback,
        handleFilterFormSearchTypeSelectionRadioButtonListSelectedIndexChanged:
            handleFilterFormSearchTypeSelectionRadioButtonListSelectedIndexChanged,
        dom
    };

})();