/// <reference path="../../../Client/DevExpressIntellisense/devexpress-web.d.ts" />
/// <reference path="~/Client/Src/Js/Views/Shared/Partials/_ContentModal.js" />
/// <reference path="~/Client/Src/Js/Utilites/Tools.js" />
/// <reference path="~/Client/Src/Js/Utilites/Connector.js" />

window.motorsazanClient = window.motorsazanClient || {};
window.motorsazanClient.sparePartReport = (function () {

    var dom = {};
    var state = {
    };
    var tools = motorsazanClient.tools;
    var controllerName = "/SparePartReport/";


    async function fillGridWithMachine() {
        if (!isFilterFormValid()) return;

        const url = controllerName + "GetMachineSparePartReportByMachineId";

        const machineId = dom.filterFormMachineListCombo.GetValue();


        const apiParam = {
            MachineId: machineId
        };

        const gridHtml = await motorsazanClient.connector.post(url, apiParam);

        dom.resultGridParent.html(gridHtml);
        setDom();
    }


    function handleFilterFormGetDepartmentListHasMachineComboBeginCallback(command) {

            command.callbackUrl =
                controllerName + "FilterFormGetDepartmentListHasMachine";
        
    }

    async function handleFilterFormGetDepartmentListHasMachineComboSelectedIndexChange() {
        tools.hideItem(dom.filterFormGetDepartmentListHasMachineComboError);

        dom.filterFormGetSubDepartmentListHasMachineCombo.SetSelectedIndex(-1);
        dom.filterFormMachineListCombo.SetSelectedIndex(-1);

        dom.filterFormGetSubDepartmentListHasMachineCombo.SetEnabled(true);
        dom.filterFormMachineListCombo.SetEnabled(false);


        const departmentId = dom.filterFormGetDepartmentListHasMachineCombo.GetValue();

        const url = controllerName + "FilterFormGetSubDepartmentListHasMachineByMainDepartmentId";

        const apiParam = {
            departmentId: departmentId
        };

        const subDepartmentListCombo = await motorsazanClient.connector.post(url, apiParam);

        dom.filterFormGetSubDepartmentListHasMachineComboParent.html(subDepartmentListCombo);
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

        dom.filterFormMachineListCombo.SetEnabled(true);

        const subDepartmentId = dom.filterFormGetSubDepartmentListHasMachineCombo.GetValue();

        const url = controllerName + "FilterFormGetMachineList";

        const apiParam = {
            departmentId: subDepartmentId
        };


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

    }
    

    function handleFilterFormSearchBtnClick() {
        if (!isFilterFormValid()) return;
        fillGridWithMachine();
       
    }



    function handleResultGridBeginCallback(command) {
        if (isFilterFormValid()) {
            const machineId = dom.filterFormMachineListCombo.GetValue();
                url = controllerName +
                    "GetMachineSparePartReportByMachineId" +
                    "?MachineId=" +
                    machineId;
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



        //Grid
        dom.resultGridParent = $("#resultGridParent");
        dom.grid = ASPxClientGridView.Cast("grid");
    }

    function setEvents() {


        dom.filterFormGetDepartmentListHasMachineCombo.SetSelectedIndex(-1);
        dom.filterFormGetSubDepartmentListHasMachineCombo.SetSelectedIndex(-1);
        dom.filterFormMachineListCombo.SetSelectedIndex(-1);

        dom.filterFormGetSubDepartmentListHasMachineCombo.SetEnabled(false);
        dom.filterFormMachineListCombo.SetEnabled(false);
    }


    $(document).ready(init);

    return {
        handleFilterFormGetDepartmentListHasMachineComboBeginCallback: handleFilterFormGetDepartmentListHasMachineComboBeginCallback,
        handleFilterFormGetDepartmentListHasMachineComboSelectedIndexChange: handleFilterFormGetDepartmentListHasMachineComboSelectedIndexChange,
        handleFilterFormGetSubDepartmentListHasMachineComboBeginCallback: handleFilterFormGetSubDepartmentListHasMachineComboBeginCallback,
        handleFilterFormGetSubDepartmentListHasMachineComboSelectedIndexChange: handleFilterFormGetSubDepartmentListHasMachineComboSelectedIndexChange,
        handleFilterFormMachineListComboBeginCallback: handleFilterFormMachineListComboBeginCallback,
        handleFilterFormMachineListComboSelectedIndexChange: handleFilterFormMachineListComboSelectedIndexChange,
        handleFilterFormSearchBtnClick: handleFilterFormSearchBtnClick,
        handleResultGridBeginCallback: handleResultGridBeginCallback
    };
})();