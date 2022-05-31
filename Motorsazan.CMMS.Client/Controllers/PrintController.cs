using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Report;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.DepartmentMttrAndMtbfReport;
using Motorsazan.CMMS.Shared.Models.Input.MachineManagement;
using Motorsazan.CMMS.Shared.Models.Input.SchedulerWorkOrderPrintReport;
using Motorsazan.CMMS.Shared.Models.Output.MachineManagement;
using Motorsazan.CMMS.Shared.Models.Output.SchedulerWorkOrderPrintReport;
using Motorsazan.CMMS.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class PrintController: Controller
    {
        const string AvatarBaseUrl = "http://erp-server/Images/PersonalPic/";
        [HttpGet]
        public ActionResult Print(long workOrderId)
        {
            _ = Stopwatch.StartNew();

            XtraReport report = new DailyAccidentalWorkOrderReport();
            report.Parameters["WorkOrderId"].Value = workOrderId;


            using(var ms = new MemoryStream())
            {
                report.ExportToPdf(ms, new PdfExportOptions() { ShowPrintDialogOnOpen = false });
                return ExportDocument(ms.ToArray(), "pdf", "DailyWorkOrderReport.pdf", true);
            }
        }


        [HttpGet]
        public ActionResult PrintOEEReportOnMTTRAndMTBFByMachineId(long machineId, int timeType, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            _ = Stopwatch.StartNew();
            XtraReport report = new OeeChartReport();
            var input = new InputGetOEEPrintReportOnMTTRAndMTBFByMachineId();
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);
            input.MachineId = machineId;
            input.TimeType = timeType;
            var fromDate = Tools.ToShamsiDateString(input.StartDate);
            var toDate = Tools.ToShamsiDateString(input.EndDate);

            var printItem = ApiList.GetOEEPrintReportOnMTTRAndMTBFByMachineId(input);
            report.FindControl("OldMachineCodeLBL", true).Text = printItem.OldMachineCode;
            report.FindControl("FromDateLBL", true).Text = fromDate;
            report.FindControl("ToDateLBL", true).Text = toDate;
            report.FindControl("MachineNameLBL", true).Text = printItem.MachineName;
            report.FindControl("OEELBL", true).Text = string.Format("{0:00.0}", printItem.OEE);
            report.FindControl("EALBL", true).Text = string.Format("{0:00.0}", printItem.PureEA);
            report.FindControl("PELBL", true).Text = string.Format("{0:00.0}", printItem.Performance);
            report.FindControl("QALBL", true).Text = string.Format("{0:00.0}", printItem.Quality);
            report.FindControl("OEEResultLBL", true).Text = printItem.OEE;
            report.FindControl("LineNameLBL", true).Text = printItem.DepartmentName;
            report.FindControl("OPLBL", true).Text = printItem.OperationCode;
            report.FindControl("EmployeeNameLBL", true).Text = printItem.EmployeeName;
            report.FindControl("EIDLBL", true).Text = printItem.EId;
            report.FindControl("EmployeePicture", true).Text = string.Format("{0:00.0}", printItem.OEE);
            report.FindControl("AvailablityHourLBL", true).Text = string.Format("{0:00.0}", printItem.AccessTimeInHour);
            report.FindControl("WorkOrderCountLBL", true).Text = printItem.AllWorkOrderCount;
            report.FindControl("MeanTimeToRepairLBL", true).Text = string.Format("{0:00.0}", printItem.PureMTTR);
            report.FindControl("MeanTimeBetweenFailureLBL", true).Text = string.Format("{0:00.0}", printItem.PureMTBF);
            report.FindControl("EquipmentAvailablityLBL", true).Text = string.Format("{0:00.0}", printItem.PureEA);
            var avatarUrl = AvatarBaseUrl + printItem.EId + ".jpg";
            report.Parameters["ReportImageURL"].Value = avatarUrl;


            var fileName = Guid.NewGuid().ToString().Substring(0, 3) + "_" + printItem.EId + ".pdf";
            fileName = fileName.Replace('/', '_');
            var filepath = Server.MapPath("~/FileBank/Report/");

            var exists = Directory.Exists(filepath);

            if(!exists)
            {
                Directory.CreateDirectory(Server.MapPath("~/FileBank"));
                Directory.CreateDirectory(Server.MapPath("~/FileBank/Report"));
            }
            var path = Server.MapPath("~/FileBank/Report/") + fileName;

            report.ExportToPdf(path);
            Tools.RemoveFileWithDelay(fileName, path, 15);

            return File(path, "application/pdf", fileName);

        }


        [HttpGet]
        public ActionResult PrintDailyAccidentalWorkOrder(long workOrderId)
        {
            _ = Stopwatch.StartNew();
            XtraReport report = new DailyAccidentalWorkOrderReport();
            var input = new InputGetVisitFormPrintByWorkOrderId
            {
                WorkOrderId = workOrderId
            };
            var todayDate = Tools.ToShamsiDateString(DateTime.Now);

            var printItem = ApiList.GetVisitFormPrintByWorkOrderId(input);
            report.FindControl("CreationDateLBL", true).Text = printItem.WorkOrderCreationDate;
            report.FindControl("CreationTimeLBL", true).Text = printItem.WorkOrderCreationTime;
            report.FindControl("WorkOrderSerialLBL", true).Text = printItem.WorkOrderSerial;
            report.FindControl("StopTypeLBL", true).Text = printItem.StopTypeTitle;
            report.FindControl("OldMachineCodeLBL", true).Text = printItem.OldMachineCode;
            report.FindControl("OperationLBL", true).Text = printItem.OperationCode;
            report.FindControl("MachineNameLBL", true).Text = printItem.MachineName;
            report.FindControl("MaintenanceGroupLBL", true).Text = printItem.MaintenanceGroupName;
            report.FindControl("CreatorLBL", true).Text = printItem.WorkOrderRegistrar;
            report.FindControl("DepartmentLBL", true).Text = printItem.DepartmentName;
            report.FindControl("FultDescriptionLBL", true).Text = printItem.Description;
            report.Parameters["TodayDate"].Value = todayDate;


            var filepath = Server.MapPath("~/FileBank/Report/");

            var exists = Directory.Exists(filepath);

            if(!exists)
            {
                Directory.CreateDirectory(Server.MapPath("~/FileBank"));
                Directory.CreateDirectory(Server.MapPath("~/FileBank/Report"));
            }
            var fileName = printItem.WorkOrderSerial + "_" + Guid.NewGuid().ToString().Substring(0, 3) + "_" + printItem.WorkOrderId + ".pdf";
            fileName = fileName.Replace('/', '_');
            var path = Server.MapPath("~/FileBank/Report/") + fileName;

            report.ExportToPdf(path);
            Tools.RemoveFileWithDelay(fileName, path, 15);

            return File(path, "application/pdf", fileName);

        }

        [HttpGet]
        public ActionResult PrintDailyPreventiveWorkOrder(long workOrderId)
        {
            _ = Stopwatch.StartNew();
            var report = new DailyPreventiveWorkOrderReport();
            var input = new InputGetPreventiveVisitFormPrintByWorkOrderId
            {
                WorkOrderId = workOrderId
            };
            var todayDate = Tools.ToShamsiDateString(DateTime.Now);

            var printItem = ApiList.GetPreventiveVisitFormPrintByWorkOrderId(input);
            report.FindControl("CreationDateLBL", true).Text = printItem.WorkOrderCreationDate;
            report.FindControl("CreationTimeLBL", true).Text = printItem.WorkOrderCreationTime;
            report.FindControl("WorkOrderSerialLBL", true).Text = printItem.WorkOrderSerial;
            report.FindControl("StopTypeLBL", true).Text = printItem.StopTypeTitle;
            report.FindControl("OldMachineCodeLBL", true).Text = printItem.OldMachineCode;
            report.FindControl("OperationLBL", true).Text = printItem.OperationCode;
            report.FindControl("MachineNameLBL", true).Text = printItem.MachineName;
            report.FindControl("MaintenanceGroupLBL", true).Text = printItem.MaintenanceGroupName;
            report.FindControl("CreatorLBL", true).Text = printItem.WorkOrderRegistrar;
            report.FindControl("DepartmentLBL", true).Text = printItem.DepartmentName;
            report.FindControl("FultDescriptionLBL", true).Text = printItem.Description;
            report.Parameters["TodayDate"].Value = todayDate;

            #region ItemDetail

            var detailList = new List<PreventiveItemList>();
            for(var i = 0; i < printItem.PreventiveItemList.Length; i++)
            {
                var item = new PreventiveItemList
                {
                    OperationItemCode = printItem.PreventiveItemList[i].OperationItemCode,
                    OperationItemName = printItem.PreventiveItemList[i].OperationItemName,
                    PreferTime = printItem.PreventiveItemList[i].PreferTime,
                    WorkOrderPreferDate = printItem.PreventiveItemList[i].WorkOrderPreferDate,
                    EmployeeName = printItem.PreventiveItemList[i].EmployeeName
                };
                detailList.Add(item);
            }
            var sourceDt = Tools.ToDataTable<PreventiveItemList>(detailList);
            report.DataSource = sourceDt;


            #endregion

            var fileName = printItem.WorkOrderSerial + "_" + Guid.NewGuid().ToString().Substring(0, 3) + "_" + printItem.WorkOrderId + ".pdf";

            var filepath = Server.MapPath("~/FileBank/Report/");

            var exists = Directory.Exists(filepath);

            if(!exists)
            {
                Directory.CreateDirectory(Server.MapPath("~/FileBank"));
                Directory.CreateDirectory(Server.MapPath("~/FileBank/Report"));
            }
            var path = Server.MapPath("~/FileBank/Report/") + fileName;

            report.ExportToPdf(path);
            Tools.RemoveFileWithDelay(fileName, path, 15);

            return File(path, "application/pdf", fileName);

        }
        FileResult ExportDocument(byte[] document, string format, string fileName, bool isInline)
        {
            string contentType;
            var disposition = isInline ? "inline" : "attachment";

            switch(format.ToLower())
            {
                case "docx":
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "xls":
                    contentType = "application/vnd.ms-excel";
                    break;
                case "xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case "mht":
                    contentType = "message/rfc822";
                    break;
                case "html":
                    contentType = "text/html";
                    break;
                case "txt":
                case "csv":
                    contentType = "text/plain";
                    break;
                case "png":
                    contentType = "image/png";
                    break;
                default:
                    contentType = string.Format("application/{0}", format);
                    break;
            }

            Response.AddHeader("Content-Disposition", string.Format("{0}; filename={1}", disposition, fileName));
            return File(document, contentType);
        }



        [HttpGet]
        public ActionResult PrintMachineBuiltInfoByMachineId(long machineId)
        {
            _ = Stopwatch.StartNew();
            var report = new MachineBaseInfoReport();
            var input = new InputGetMachineBuiltInfo
            {
                MachineId = machineId
            };
            var todayDate = Tools.ToShamsiDateString(DateTime.Now);

            var printItem = ApiList.GetMachineBuiltInfo(input);





            report.Parameters["todayDate"].Value = todayDate;
            report.Parameters["machineName"].Value = printItem.MachineName;
            report.Parameters["oldMachineCode"].Value = printItem.OldMachineCode;
            report.Parameters["machineModel"].Value = printItem.Model;
            report.Parameters["ImportDate"].Value = printItem.ImportDate;
            report.Parameters["builtCountry"].Value = printItem.CountryName;
            report.Parameters["builtYear"].Value = printItem.BuiltYear;
            report.Parameters["machineLevel"].Value = printItem.MachineLevelName;
            report.Parameters["machineId"].Value = machineId;


            var fileName = printItem.OldMachineCode + "_" + Guid.NewGuid().ToString().Substring(0, 3) + ".pdf";

            var filepath = Server.MapPath("~/FileBank/Report/");

            var exists = Directory.Exists(filepath);

            if(!exists)
            {
                Directory.CreateDirectory(Server.MapPath("~/FileBank"));
                Directory.CreateDirectory(Server.MapPath("~/FileBank/Report"));
            }
            var path = Server.MapPath("~/FileBank/Report/") + fileName;

            report.ExportToPdf(path);
            Tools.RemoveFileWithDelay(fileName, path, 15);

            return File(path, "application/pdf", fileName);

        }

    }
}
