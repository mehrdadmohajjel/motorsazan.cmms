/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.machineConsumeStockReport = (function () {

    var dom = {};
    var state = {
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/MachineConsumeStockReport/";


    async function fillGrid() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "GetMachineConsumeStockReportByCondition";

        const stockId = dom.filterFormGetStockFromHavaleWorkOrderReferralCombo.GetValue();


        const apiParam = {
            stockId: stockId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.resultGridParent.html(gridHtml);
        setDom();
    }


    function handleGetStockFromHavaleWorkOrderReferralBeginCallback(command) {
                 command.callbackUrl =
                     controllerName + "FilterFormGetStockFromHavaleWorkOrderReferral";
       
    }

    async function handleFilterFormGetStockFromHavaleWorkOrderReferralSelectedIndexChange() {
        tools.hideItem(dom.filterFormGetStockFromHavaleWorkorderReferralComboError);
        dom.filterFormSearchBtn.SetEnabled(true);
        setDom();
    }


    function handleFilterFormSearchBtnClick() {
        if (!isFilterFormValid()) return;
        fillGrid();
       
    }


    function handleResultGridBeginCallback(command) {
        if (isFilterFormValid()) {
            const stockId = dom.filterFormGetStockFromHavaleWorkOrderReferralCombo.GetValue();
                url = controllerName +
                    "GetMachineConsumeStockReportByCondition" +
                    "?stockId=" +
                    stockId;
                command.callbackUrl = url;
            }
            return;
        }
         
   

    function init() {
        setDom();
        setEvents();

    }

    function isFilterFormValid() {
        var isValid = true;
        tools.hideItem(dom.filterFormGetStockFromHavaleWorkorderReferralComboError);
        var Stock = dom.filterFormGetStockFromHavaleWorkOrderReferralCombo.GetValue();
        var stockHasValue = !tools.isNullOrEmpty(Stock);
        if (!stockHasValue) {
            isValid = false;
            tools.showItem(dom.filterFormGetStockFromHavaleWorkorderReferralComboError);
        }

        return isValid;
    }

    function setDom() {

        //Filter form With Machine

        dom.filterFormGetStockFromHavaleWorkorderReferralComboParent = $("#filterFormGetStockFromHavaleWorkorderReferralComboParent");
        dom.filterFormGetStockFromHavaleWorkorderReferralComboError = $("#filterFormGetStockFromHavaleWorkorderReferralComboError");
        dom.filterFormGetStockFromHavaleWorkOrderReferralCombo = ASPxClientComboBox.Cast("filterFormGetStockFromHavaleWorkOrderReferralCombo");


        dom.filterFormSearchBtn = ASPxClientButton.Cast("filterFormSearchBtn");

        //Grid
        dom.resultGridParent = $("#resultGridParent");
        dom.resultGrid = ASPxClientGridView.Cast("resultGrid");
    }

    function setEvents() {

        dom.filterFormGetStockFromHavaleWorkOrderReferralCombo.SetSelectedIndex(-1);
        dom.filterFormSearchBtn.SetEnabled(false);
    }

    $(document).ready(init);

    return {
        handleGetStockFromHavaleWorkOrderReferralBeginCallback: handleGetStockFromHavaleWorkOrderReferralBeginCallback,
        handleFilterFormGetStockFromHavaleWorkOrderReferralSelectedIndexChange: handleFilterFormGetStockFromHavaleWorkOrderReferralSelectedIndexChange,
        handleFilterFormSearchBtnClick: handleFilterFormSearchBtnClick,
        handleResultGridBeginCallback: handleResultGridBeginCallback
    };
})();