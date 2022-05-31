using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Motorsazan.CMMS.Client.Report
{
    public partial class DailyAccidentalWorkOrderReport: DevExpress.XtraReports.UI.XtraReport
    {
        public DailyAccidentalWorkOrderReport()
        {
            InitializeComponent();
        }

        private void CreationDateLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("WorkOrderCreationDate") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("WorkOrderCreationDate").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void CreationTimeLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("WorkOrderCreationTime") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("WorkOrderCreationTime").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void WorkOrderSerialLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("WorkOrderSerial") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("WorkOrderSerial").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void StopTypeLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("StopTypeTitle") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("StopTypeTitle").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void OldMachineCodeLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("OldMachineCode") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("OldMachineCode").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void OperationLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("OperationCode") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("OperationCode").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void MachineNameLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("MachineName") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("MachineName").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void MaintenanceGroupLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("MaintenanceGroupName") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("MaintenanceGroupName").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void CreatorLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("WorkOrderRegistrar") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("WorkOrderRegistrar").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void DepartmentLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("DepartmentName") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("DepartmentName").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void FultDescriptionLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                var cellObj = sender as XRTableCell;
                if(GetCurrentColumnValue("Description") != null)
                {
                    cellObj.Text = GetCurrentColumnValue("Description").ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void DateLbl_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
              var cellObj = sender as XRLabel;
            try
            {
                cellObj.Text = Parameters["TodayDate"].Value.ToString();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
