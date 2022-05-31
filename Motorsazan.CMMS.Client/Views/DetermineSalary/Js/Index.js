/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_MessageModal.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.determineSalary = (function() {

    var dom = {};
    var state = {
        salaryId: null
    };

    var tools = motorsazanClient.tools;
    var controllerName = "/DetermineSalary/";

    function detailSalary(salaryId) {
        const url = controllerName + "DetailDetermineSalaryModal";
        state.salaryId = salaryId;
        const params = {
            salaryId: salaryId
        };

        const title = "جزئیات";
        motorsazanClient.contentModal.ajaxShow(title, url, params, true, false);

        setDom();
    }

    async function fillGrid() {
        const url = controllerName + "FillResultGrid";

        const gridHtml = await motorsazanClient.connector.post(url, null);

        dom.resultGridParent.html(gridHtml);

        setDom();
    }

    function handelSuggestedDateChange() {
        tools.hideItem(dom.changeSuggestedDatePickerError);
        const suggestedDate = dom.changeSuggestedDatePicker.val();
        const isSuggestedDateSelected = !tools.isNullOrEmpty(suggestedDate);
        if (!isSuggestedDateSelected) {
            tools.showItem(dom.changeSuggestedDatePickerError);
        };
    }

    async function handleBatchEditChangesSaving(s, e) {
        e.cancel = true;

        const canRegister = isBatchEditValid();
        if (!canRegister) return;

        const suggestedDate = dom.changeSuggestedDatePicker.val();

        const inputData = Object.entries(e.updatedValues);
        const count = inputData.length;
        const salaryDetailList = new Array(count);

        for (let i in inputData) {
            if (Object.prototype.hasOwnProperty.call(inputData, i)) {

                const salaryId = dom.resultGrid.GetRowKey(inputData[i][0]);
                const salary = Object.values(inputData[i][1])[0];
                salaryDetailList[i] = {
                    salaryId: salaryId,
                    salary: salary
                };

            }
        }

        const url = controllerName + "BatchEditingUpdateSalaryModel";

        const params = {
            SalaryDetailList: salaryDetailList,
            startDate: null,
            RegisterUserId: null,
            persianStartDate: suggestedDate
        };

        const apiResult = await motorsazanClient.connector.post(url, params);
        setDom();
        motorsazanClient.messageModal.success(apiResult);

        
        await fillGrid();
        setDom();

        dom.resultGrid.CancelEdit();

        dom.gridToolbarChangeSaveButton.SetEnabled(false);
        dom.gridToolbarChangeCancelButton.SetEnabled(false);
    }

    function handleBatchEditStartEditing() {
        dom.gridToolbarChangeSaveButton.SetEnabled(true);
        dom.gridToolbarChangeCancelButton.SetEnabled(true);
    }

    function handleDetailGridBeginCallback(command) {
        const salaryId = state.salaryId;

        command.callbackUrl =
            controllerName + "DetailDetermineSalaryModal" + "?SalaryId=" + salaryId;
    }

    function handleGridCustomBtnClick(source, event) {
        const customBtnId = event.buttonID;
        if (customBtnId === "GridDetailCustomBtn")
            source.GetRowValues(source.GetFocusedRowIndex(), "SalaryId", detailSalary);
    }

    function handleResultGridBeginCallback(command) {

        const url = controllerName +
            "FillResultGrid";

        command.callbackUrl = url;
    }

    function handleToolbarButtonClick(s, e) {
        if (e.item.name === "gridToolbarChangeSaveButton") {
            const canContinue = isBatchEditValid();
            if (!canContinue) return false;

            dom.resultGrid.UpdateEdit();
        }

        if (e.item.name === "gridToolbarChangeCancelButton") {
            dom.resultGrid.CancelEdit();

            dom.gridToolbarChangeSaveButton.SetEnabled(false);
            dom.gridToolbarChangeCancelButton.SetEnabled(false);
        }
        return true;
    }

    function init() {
        setDom();
        setEvents();
    }

    function isBatchEditValid() {
        var isValid = true;

        tools.hideItem(dom.changeSuggestedDatePickerError);
        const suggestedDate = dom.changeSuggestedDatePicker.val();
        const isSuggestedDateSelected = !tools.isNullOrEmpty(suggestedDate);
        if (!isSuggestedDateSelected) {
            isValid = false;
            tools.showItem(dom.changeSuggestedDatePickerError);
        };
        return isValid;
    }

    function onEndCallBack() {
        const suggestedDate = dom.changeSuggestedDatePicker.val();
        const isSuggestedDateSelected = !tools.isNullOrEmpty(suggestedDate);
        if (!isSuggestedDateSelected) {
            dom.gridToolbarChangeSaveButton.SetEnabled(false);
            dom.gridToolbarChangeCancelButton.SetEnabled(false);
        };

    }

    function setDom() {
        dom.changeSuggestedDatePicker = $("#changeSuggestedDatePicker");
        dom.changeSuggestedDatePickerError = $("#changeSuggestedDatePickerError");
        dom.changeSuggestedDatePickerParent = $("#changeSuggestedDatePickerParent");

        //Grid
        dom.resultGridParent = $("#resultGridParent");
        dom.resultGrid = ASPxClientGridView.Cast("resultGrid");

        //Grid Command Button
        dom.gridToolbarChangeSaveButton = resultGrid.GetToolbar(0)
            .GetItemByName("gridToolbarChangeSaveButton");

        dom.gridToolbarChangeCancelButton = resultGrid.GetToolbar(0)
            .GetItemByName("gridToolbarChangeCancelButton");

        dom.detailGridParent = $("#detailGridParent");
        dom.detailGrid = ASPxClientGridView.Cast("detailGrid");
    }

    function setEvents() {
        dom.changeSuggestedDatePicker.on("change", handelSuggestedDateChange);

        dom.gridToolbarChangeSaveButton.SetEnabled(false);
        dom.gridToolbarChangeCancelButton.SetEnabled(false);
    }

    $(document).ready(init);

    return {
        handleBatchEditChangesSaving: handleBatchEditChangesSaving,
        handleBatchEditStartEditing: handleBatchEditStartEditing,
        handleDetailGridBeginCallback: handleDetailGridBeginCallback,
        handleGridCustomBtnClick: handleGridCustomBtnClick,
        handleResultGridBeginCallback: handleResultGridBeginCallback,
        handleToolbarButtonClick: handleToolbarButtonClick,
        onEndCallBack: onEndCallBack
    };
})();