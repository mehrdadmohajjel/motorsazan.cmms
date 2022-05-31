using DevExpress.XtraReports.UI;
using Motorsazan.CMMS.Shared.Utilities;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

/// <summary>
/// Summary description for OeeChartReport
/// </summary>
public class OeeChartReport: DevExpress.XtraReports.UI.XtraReport
{
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private DetailBand Detail;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private XRTable xrTable6;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell37;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel3;
    private XRLabel xrLabel7;
    private XRLabel MachineNameLBL;
    private XRLabel xrLabel8;
    private XRLabel ToDateLBL;
    private XRLabel FromDateLBL;
    private XRLabel xrLabel4;
    private XRLabel OldMachineCodeLBL;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell2;
    private XRLabel QALBL;
    private XRLabel xrLabel16;
    private XRLabel PELBL;
    private XRLabel xrLabel14;
    private XRLabel EALBL;
    private XRLabel xrLabel12;
    private XRLabel OEELBL;
    private XRLabel xrLabel11;
    private XRLabel xrLabel10;
    private XRLabel xrLabel13;
    private XRLabel xrLabel25;
    private XRLabel xrLabel22;
    private XRLabel OEEResultLBL;
    private XRLabel xrLabel15;
    private XRLabel LineNameLBL;
    private XRLabel xrLabel17;
    private XRLabel OPLBL;
    private XRLabel xrLabel18;
    private XRLabel EmployeeNameLBL;
    private XRLabel xrLabel21;
    private XRLabel EIDLBL;
    private XRLabel xrLabel27;
    private XRPictureBox EmployeePicture;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableRow xrTableRow4;
    private XRTableCell AvailablityHourLBL;
    private XRTableCell WorkOrderCountLBL;
    private XRTableCell MeanTimeToRepairLBL;
    private XRTableCell MeanTimeBetweenFailureLBL;
    private XRTableCell xrTableCell12;
    private XRTableCell EquipmentAvailablityLBL;
    public DevExpress.XtraReports.Parameters.Parameter ReportImageURL;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public OeeChartReport()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if(disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OeeChartReport));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.EmployeePicture = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.AvailablityHourLBL = new DevExpress.XtraReports.UI.XRTableCell();
            this.WorkOrderCountLBL = new DevExpress.XtraReports.UI.XRTableCell();
            this.MeanTimeToRepairLBL = new DevExpress.XtraReports.UI.XRTableCell();
            this.MeanTimeBetweenFailureLBL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.EquipmentAvailablityLBL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.EIDLBL = new DevExpress.XtraReports.UI.XRLabel();
            this.EmployeeNameLBL = new DevExpress.XtraReports.UI.XRLabel();
            this.OPLBL = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.LineNameLBL = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.OEEResultLBL = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.QALBL = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.PELBL = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.EALBL = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.OEELBL = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.MachineNameLBL = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.ToDateLBL = new DevExpress.XtraReports.UI.XRLabel();
            this.FromDateLBL = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.OldMachineCodeLBL = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportImageURL = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable6});
            this.TopMargin.Name = "TopMargin";
            // 
            // xrTable6
            // 
            this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable6.Font = new System.Drawing.Font("B Nazanin", 9.75F);
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(1.087785E-05F, 0F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
            this.xrTable6.SizeF = new System.Drawing.SizeF(703.2457F, 100F);
            this.xrTable6.StylePriority.UseBorders = false;
            this.xrTable6.StylePriority.UseFont = false;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell37});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel6,
            this.xrLabel5,
            this.xrPictureBox1});
            this.xrTableCell37.Multiline = true;
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.Weight = 1D;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel6.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(447.6492F, 33.30799F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(245.5966F, 23F);
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "« نرم افزار نگهداری و تعمیرات »";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel6.TextTrimming = System.Drawing.StringTrimming.Word;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel5.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(9.710744F, 33.30799F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(245.5966F, 23F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "سیستم های جامع یکپارچه نرم افزاری موتور سازان";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel5.TextTrimming = System.Drawing.StringTrimming.Word;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPictureBox1.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("xrPictureBox1.ImageSource"));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(284.7084F, 10.00001F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(105.2083F, 79.99998F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 45.2191F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel27,
            this.EmployeePicture,
            this.xrTable3,
            this.xrLabel21,
            this.EIDLBL,
            this.EmployeeNameLBL,
            this.OPLBL,
            this.xrLabel18,
            this.LineNameLBL,
            this.xrLabel17,
            this.xrTable2,
            this.xrLabel7,
            this.MachineNameLBL,
            this.xrLabel8,
            this.ToDateLBL,
            this.FromDateLBL,
            this.xrLabel4,
            this.OldMachineCodeLBL,
            this.xrLabel3});
            this.Detail.HeightF = 925.1819F;
            this.Detail.Name = "Detail";
            // 
            // xrLabel27
            // 
            this.xrLabel27.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel27.Font = new System.Drawing.Font("B Nazanin", 15.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(18.36224F, 632.6966F);
            this.xrLabel27.Multiline = true;
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel27.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.xrLabel27.SizeF = new System.Drawing.SizeF(170.6727F, 32.21054F);
            this.xrLabel27.StylePriority.UseBorders = false;
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.StylePriority.UseTextAlignment = false;
            this.xrLabel27.Text = "نام و نام خانوادگی :";
            this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // EmployeePicture
            // 
            this.EmployeePicture.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.EmployeePicture.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("EmployeePicture.ImageSource"));
            this.EmployeePicture.LocationFloat = new DevExpress.Utils.PointFloat(528.23F, 636.7F);
            this.EmployeePicture.Name = "EmployeePicture";
            this.EmployeePicture.SizeF = new System.Drawing.SizeF(125F, 125F);
            this.EmployeePicture.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.EmployeePicture.StylePriority.UseBorders = false;
            this.EmployeePicture.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.EmployeePicture_BeforePrint);
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Font = new System.Drawing.Font("B Nazanin", 14.25F, System.Drawing.FontStyle.Bold);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(76.63318F, 791.3377F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow4});
            this.xrTable3.SizeF = new System.Drawing.SizeF(527.559F, 63.1579F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTable3.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrTable3_BeforePrint);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell6,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Font = new System.Drawing.Font("B Nazanin", 14.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Text = "ساعات دسترس";
            this.xrTableCell7.Weight = 1.3855268502207458D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Font = new System.Drawing.Font("B Nazanin", 14.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell6.Multiline = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.Text = "تعداد سفارشکار";
            this.xrTableCell6.Weight = 1.2405873784229415D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "MTTR";
            this.xrTableCell3.Weight = 1.0792527663706895D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.Text = "MTBF";
            this.xrTableCell4.Weight = 0.833795689558957D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.Text = "EA";
            this.xrTableCell5.Weight = 1.1069340114899409D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.AvailablityHourLBL,
            this.WorkOrderCountLBL,
            this.MeanTimeToRepairLBL,
            this.MeanTimeBetweenFailureLBL,
            this.xrTableCell12,
            this.EquipmentAvailablityLBL});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // AvailablityHourLBL
            // 
            this.AvailablityHourLBL.Font = new System.Drawing.Font("B Nazanin", 14.25F, System.Drawing.FontStyle.Bold);
            this.AvailablityHourLBL.Multiline = true;
            this.AvailablityHourLBL.Name = "AvailablityHourLBL";
            this.AvailablityHourLBL.StylePriority.UseFont = false;
            this.AvailablityHourLBL.Text = "455";
            this.AvailablityHourLBL.Weight = 1.3855268502207458D;
            this.AvailablityHourLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.AvailablityHourLBL_BeforePrint);
            // 
            // WorkOrderCountLBL
            // 
            this.WorkOrderCountLBL.Font = new System.Drawing.Font("B Nazanin", 14.25F, System.Drawing.FontStyle.Bold);
            this.WorkOrderCountLBL.Multiline = true;
            this.WorkOrderCountLBL.Name = "WorkOrderCountLBL";
            this.WorkOrderCountLBL.StylePriority.UseFont = false;
            this.WorkOrderCountLBL.Text = "50";
            this.WorkOrderCountLBL.Weight = 1.2405873784229415D;
            this.WorkOrderCountLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.WorkOrderCountLBL_BeforePrint);
            // 
            // MeanTimeToRepairLBL
            // 
            this.MeanTimeToRepairLBL.Font = new System.Drawing.Font("B Nazanin", 14.25F, System.Drawing.FontStyle.Bold);
            this.MeanTimeToRepairLBL.Multiline = true;
            this.MeanTimeToRepairLBL.Name = "MeanTimeToRepairLBL";
            this.MeanTimeToRepairLBL.StylePriority.UseFont = false;
            this.MeanTimeToRepairLBL.Text = "45.5";
            this.MeanTimeToRepairLBL.Weight = 1.0792527663706895D;
            this.MeanTimeToRepairLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.MeanTimeToRepairLBL_BeforePrint);
            // 
            // MeanTimeBetweenFailureLBL
            // 
            this.MeanTimeBetweenFailureLBL.Font = new System.Drawing.Font("B Nazanin", 14.25F, System.Drawing.FontStyle.Bold);
            this.MeanTimeBetweenFailureLBL.Multiline = true;
            this.MeanTimeBetweenFailureLBL.Name = "MeanTimeBetweenFailureLBL";
            this.MeanTimeBetweenFailureLBL.StylePriority.UseFont = false;
            this.MeanTimeBetweenFailureLBL.Text = "33";
            this.MeanTimeBetweenFailureLBL.Weight = 0.833795689558957D;
            this.MeanTimeBetweenFailureLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.MeanTimeBetweenFailureLBL_BeforePrint);
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Font = new System.Drawing.Font("B Nazanin", 14.25F, System.Drawing.FontStyle.Bold);
            this.xrTableCell12.Multiline = true;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.Text = "%";
            this.xrTableCell12.Weight = 0.29483168820122219D;
            // 
            // EquipmentAvailablityLBL
            // 
            this.EquipmentAvailablityLBL.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.EquipmentAvailablityLBL.Font = new System.Drawing.Font("B Nazanin", 14.25F, System.Drawing.FontStyle.Bold);
            this.EquipmentAvailablityLBL.Multiline = true;
            this.EquipmentAvailablityLBL.Name = "EquipmentAvailablityLBL";
            this.EquipmentAvailablityLBL.StylePriority.UseBorders = false;
            this.EquipmentAvailablityLBL.StylePriority.UseFont = false;
            this.EquipmentAvailablityLBL.StylePriority.UseTextAlignment = false;
            this.EquipmentAvailablityLBL.Text = "43.99";
            this.EquipmentAvailablityLBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.EquipmentAvailablityLBL.Weight = 0.81210232328871856D;
            this.EquipmentAvailablityLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.EquipmentAvailablityLBL_BeforePrint);
            // 
            // xrLabel21
            // 
            this.xrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel21.Font = new System.Drawing.Font("B Nazanin", 15.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(18.11652F, 684.4925F);
            this.xrLabel21.Multiline = true;
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.xrLabel21.SizeF = new System.Drawing.SizeF(105.684F, 32.21051F);
            this.xrLabel21.StylePriority.UseBorders = false;
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "کد پرسنلی :";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // EIDLBL
            // 
            this.EIDLBL.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.EIDLBL.Font = new System.Drawing.Font("B Nazanin", 15.75F, System.Drawing.FontStyle.Bold);
            this.EIDLBL.LocationFloat = new DevExpress.Utils.PointFloat(188.9517F, 684.4925F);
            this.EIDLBL.Multiline = true;
            this.EIDLBL.Name = "EIDLBL";
            this.EIDLBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.EIDLBL.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.EIDLBL.SizeF = new System.Drawing.SizeF(96.77533F, 32.21051F);
            this.EIDLBL.StylePriority.UseBorders = false;
            this.EIDLBL.StylePriority.UseFont = false;
            this.EIDLBL.StylePriority.UseTextAlignment = false;
            this.EIDLBL.Text = "8401";
            this.EIDLBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.EIDLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.EIDLBL_BeforePrint);
            // 
            // EmployeeNameLBL
            // 
            this.EmployeeNameLBL.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.EmployeeNameLBL.Font = new System.Drawing.Font("B Nazanin", 15.75F, System.Drawing.FontStyle.Bold);
            this.EmployeeNameLBL.LocationFloat = new DevExpress.Utils.PointFloat(189.035F, 632.6966F);
            this.EmployeeNameLBL.Multiline = true;
            this.EmployeeNameLBL.Name = "EmployeeNameLBL";
            this.EmployeeNameLBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.EmployeeNameLBL.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.EmployeeNameLBL.SizeF = new System.Drawing.SizeF(307.5461F, 32.21051F);
            this.EmployeeNameLBL.StylePriority.UseBorders = false;
            this.EmployeeNameLBL.StylePriority.UseFont = false;
            this.EmployeeNameLBL.StylePriority.UseTextAlignment = false;
            this.EmployeeNameLBL.Text = "وحید";
            this.EmployeeNameLBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.EmployeeNameLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.EmployeeNameLBL_BeforePrint);
            // 
            // OPLBL
            // 
            this.OPLBL.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.OPLBL.Font = new System.Drawing.Font("B Nazanin", 15.75F, System.Drawing.FontStyle.Bold);
            this.OPLBL.LocationFloat = new DevExpress.Utils.PointFloat(579.5945F, 581.03F);
            this.OPLBL.Multiline = true;
            this.OPLBL.Name = "OPLBL";
            this.OPLBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.OPLBL.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.OPLBL.SizeF = new System.Drawing.SizeF(80.26592F, 33.30701F);
            this.OPLBL.StylePriority.UseBorders = false;
            this.OPLBL.StylePriority.UseFont = false;
            this.OPLBL.StylePriority.UseTextAlignment = false;
            this.OPLBL.Text = "80";
            this.OPLBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.OPLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.OPLBL_BeforePrint);
            // 
            // xrLabel18
            // 
            this.xrLabel18.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel18.Font = new System.Drawing.Font("B Nazanin", 15.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(499.5743F, 581.03F);
            this.xrLabel18.Multiline = true;
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.xrLabel18.SizeF = new System.Drawing.SizeF(80.26593F, 33.30701F);
            this.xrLabel18.StylePriority.UseBorders = false;
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "اپریشن :";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LineNameLBL
            // 
            this.LineNameLBL.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.LineNameLBL.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.LineNameLBL.LocationFloat = new DevExpress.Utils.PointFloat(80.32642F, 581.03F);
            this.LineNameLBL.Multiline = true;
            this.LineNameLBL.Name = "LineNameLBL";
            this.LineNameLBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LineNameLBL.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.LineNameLBL.SizeF = new System.Drawing.SizeF(405.7855F, 33.30701F);
            this.LineNameLBL.StylePriority.UseBorders = false;
            this.LineNameLBL.StylePriority.UseFont = false;
            this.LineNameLBL.StylePriority.UseTextAlignment = false;
            this.LineNameLBL.Text = "بلوک بدنه و سر سیلندر 2- GP1";
            this.LineNameLBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.LineNameLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.LineNameLBL_BeforePrint);
            // 
            // xrLabel17
            // 
            this.xrLabel17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel17.Font = new System.Drawing.Font("B Nazanin", 15.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(17.87067F, 581.03F);
            this.xrLabel17.Multiline = true;
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.xrLabel17.SizeF = new System.Drawing.SizeF(62.20996F, 33.30701F);
            this.xrLabel17.StylePriority.UseBorders = false;
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "خط :";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.BorderWidth = 2F;
            this.xrTable2.Font = new System.Drawing.Font("B Nazanin", 9.75F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(52.527F, 278.8369F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(607.3334F, 241.7763F);
            this.xrTable2.StylePriority.UseBorderDashStyle = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseBorderWidth = false;
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel25,
            this.xrLabel22,
            this.OEEResultLBL,
            this.xrLabel15,
            this.xrLabel13,
            this.QALBL,
            this.xrLabel16,
            this.PELBL,
            this.xrLabel14,
            this.EALBL,
            this.xrLabel12,
            this.OEELBL,
            this.xrLabel11,
            this.xrLabel10});
            this.xrTableCell2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Weight = 1D;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel25.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
            this.xrLabel25.ForeColor = System.Drawing.Color.Red;
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(360.683F, 197.7018F);
            this.xrLabel25.Multiline = true;
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(94.40773F, 34.07458F);
            this.xrLabel25.StylePriority.UseBorders = false;
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseForeColor = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "OEE";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel22.Font = new System.Drawing.Font("B Nazanin", 26F, System.Drawing.FontStyle.Bold);
            this.xrLabel22.ForeColor = System.Drawing.Color.Red;
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(329.9451F, 197.7017F);
            this.xrLabel22.Multiline = true;
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(39.06827F, 34.07452F);
            this.xrLabel22.StylePriority.UseBorders = false;
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseForeColor = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "=";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // OEEResultLBL
            // 
            this.OEEResultLBL.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.OEEResultLBL.Font = new System.Drawing.Font("B Nazanin", 26F, System.Drawing.FontStyle.Bold);
            this.OEEResultLBL.ForeColor = System.Drawing.Color.Red;
            this.OEEResultLBL.LocationFloat = new DevExpress.Utils.PointFloat(233.0377F, 197.7017F);
            this.OEEResultLBL.Multiline = true;
            this.OEEResultLBL.Name = "OEEResultLBL";
            this.OEEResultLBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.OEEResultLBL.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.No;
            this.OEEResultLBL.SizeF = new System.Drawing.SizeF(96.82404F, 34.07458F);
            this.OEEResultLBL.StylePriority.UseBorders = false;
            this.OEEResultLBL.StylePriority.UseFont = false;
            this.OEEResultLBL.StylePriority.UseForeColor = false;
            this.OEEResultLBL.StylePriority.UseTextAlignment = false;
            this.OEEResultLBL.Text = ".97";
            this.OEEResultLBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.OEEResultLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.OEEResultLBL_BeforePrint);
            // 
            // xrLabel15
            // 
            this.xrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel15.Font = new System.Drawing.Font("B Nazanin", 26F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.ForeColor = System.Drawing.Color.Red;
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(176.5811F, 197.7018F);
            this.xrLabel15.Multiline = true;
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(56.45654F, 34.07452F);
            this.xrLabel15.StylePriority.UseBorders = false;
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseForeColor = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "%";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel13.Font = new System.Drawing.Font("B Nazanin", 22F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.ForeColor = System.Drawing.Color.Red;
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(460.0096F, 137.1769F);
            this.xrLabel13.Multiline = true;
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.No;
            this.xrLabel13.SizeF = new System.Drawing.SizeF(27.37483F, 27.82455F);
            this.xrLabel13.StylePriority.UseBorders = false;
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseForeColor = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "%";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // QALBL
            // 
            this.QALBL.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.QALBL.Font = new System.Drawing.Font("B Nazanin", 22F, System.Drawing.FontStyle.Bold);
            this.QALBL.ForeColor = System.Drawing.Color.Red;
            this.QALBL.LocationFloat = new DevExpress.Utils.PointFloat(50.76554F, 137.1768F);
            this.QALBL.Multiline = true;
            this.QALBL.Name = "QALBL";
            this.QALBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.QALBL.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.No;
            this.QALBL.SizeF = new System.Drawing.SizeF(105.8898F, 27.82455F);
            this.QALBL.StylePriority.UseBorders = false;
            this.QALBL.StylePriority.UseFont = false;
            this.QALBL.StylePriority.UseForeColor = false;
            this.QALBL.StylePriority.UseTextAlignment = false;
            this.QALBL.Text = ".97";
            this.QALBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.QALBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.QALBL_BeforePrint);
            // 
            // xrLabel16
            // 
            this.xrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel16.Font = new System.Drawing.Font("B Nazanin", 22F, System.Drawing.FontStyle.Bold);
            this.xrLabel16.ForeColor = System.Drawing.Color.Red;
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(156.6554F, 137.1768F);
            this.xrLabel16.Multiline = true;
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(51.01038F, 27.82455F);
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseForeColor = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "×";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PELBL
            // 
            this.PELBL.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.PELBL.Font = new System.Drawing.Font("B Nazanin", 22F, System.Drawing.FontStyle.Bold);
            this.PELBL.ForeColor = System.Drawing.Color.Red;
            this.PELBL.LocationFloat = new DevExpress.Utils.PointFloat(207.4201F, 137.1768F);
            this.PELBL.Multiline = true;
            this.PELBL.Name = "PELBL";
            this.PELBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.PELBL.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.No;
            this.PELBL.SizeF = new System.Drawing.SizeF(92.09814F, 27.82455F);
            this.PELBL.StylePriority.UseBorders = false;
            this.PELBL.StylePriority.UseFont = false;
            this.PELBL.StylePriority.UseForeColor = false;
            this.PELBL.StylePriority.UseTextAlignment = false;
            this.PELBL.Text = ".83";
            this.PELBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.PELBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PELBL_BeforePrint);
            // 
            // xrLabel14
            // 
            this.xrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel14.Font = new System.Drawing.Font("B Nazanin", 22F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.ForeColor = System.Drawing.Color.Red;
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(299.6016F, 137.1768F);
            this.xrLabel14.Multiline = true;
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(30.67258F, 27.82455F);
            this.xrLabel14.StylePriority.UseBorders = false;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseForeColor = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "×";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // EALBL
            // 
            this.EALBL.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.EALBL.Font = new System.Drawing.Font("B Nazanin", 22F, System.Drawing.FontStyle.Bold);
            this.EALBL.ForeColor = System.Drawing.Color.Red;
            this.EALBL.LocationFloat = new DevExpress.Utils.PointFloat(330.0284F, 137.1768F);
            this.EALBL.Multiline = true;
            this.EALBL.Name = "EALBL";
            this.EALBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.EALBL.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.No;
            this.EALBL.SizeF = new System.Drawing.SizeF(93.8524F, 27.82455F);
            this.EALBL.StylePriority.UseBorders = false;
            this.EALBL.StylePriority.UseFont = false;
            this.EALBL.StylePriority.UseForeColor = false;
            this.EALBL.StylePriority.UseTextAlignment = false;
            this.EALBL.Text = ".99";
            this.EALBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.EALBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.EALBL_BeforePrint);
            // 
            // xrLabel12
            // 
            this.xrLabel12.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel12.Font = new System.Drawing.Font("B Nazanin", 22F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.ForeColor = System.Drawing.Color.Red;
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(423.9642F, 137.1769F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(35.9621F, 27.82452F);
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseForeColor = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "=";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // OEELBL
            // 
            this.OEELBL.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.OEELBL.Font = new System.Drawing.Font("B Nazanin", 22F, System.Drawing.FontStyle.Bold);
            this.OEELBL.ForeColor = System.Drawing.Color.Red;
            this.OEELBL.LocationFloat = new DevExpress.Utils.PointFloat(485.4176F, 137.1768F);
            this.OEELBL.Multiline = true;
            this.OEELBL.Name = "OEELBL";
            this.OEELBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.OEELBL.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.No;
            this.OEELBL.SizeF = new System.Drawing.SizeF(85.12014F, 27.82455F);
            this.OEELBL.StylePriority.UseBorders = false;
            this.OEELBL.StylePriority.UseFont = false;
            this.OEELBL.StylePriority.UseForeColor = false;
            this.OEELBL.StylePriority.UseTextAlignment = false;
            this.OEELBL.Text = "5.29";
            this.OEELBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.OEELBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.OEELBL_BeforePrint);
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel11.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(9.916733F, 82.26892F);
            this.xrLabel11.Multiline = true;
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.No;
            this.xrLabel11.SizeF = new System.Drawing.SizeF(587.4167F, 54.90787F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.Text = "درصد اثر بخشی جامع ماشین  = درصد دسترسی ماشین × درصد تولید اسمی × درصد تولید سالم" +
    "";
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel10.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(136.508F, 10F);
            this.xrLabel10.Multiline = true;
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(353.3668F, 72.26892F);
            this.xrLabel10.StylePriority.UseBorders = false;
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "OEE = EA × PE × QA";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("B Nazanin", 23F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(9.916626F, 186.3334F);
            this.xrLabel7.Multiline = true;
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(139.892F, 56.33327F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "نام دستگاه :";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // MachineNameLBL
            // 
            this.MachineNameLBL.Font = new System.Drawing.Font("B Nazanin", 20F, System.Drawing.FontStyle.Bold);
            this.MachineNameLBL.LocationFloat = new DevExpress.Utils.PointFloat(149.7253F, 186.3334F);
            this.MachineNameLBL.Multiline = true;
            this.MachineNameLBL.Name = "MachineNameLBL";
            this.MachineNameLBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.MachineNameLBL.SizeF = new System.Drawing.SizeF(545.4371F, 56.33327F);
            this.MachineNameLBL.StylePriority.UseFont = false;
            this.MachineNameLBL.StylePriority.UseTextAlignment = false;
            this.MachineNameLBL.Text = "دستگاه مته مخصوص polladr3964";
            this.MachineNameLBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.MachineNameLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.MachineNameLBL_BeforePrint);
            // 
            // xrLabel8
            // 
            this.xrLabel8.Font = new System.Drawing.Font("B Nazanin", 18F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(437.7631F, 94.91671F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(88.375F, 43.83332F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.Text = "تا تاریخ: ";
            // 
            // ToDateLBL
            // 
            this.ToDateLBL.Font = new System.Drawing.Font("B Nazanin", 18F, System.Drawing.FontStyle.Bold);
            this.ToDateLBL.LocationFloat = new DevExpress.Utils.PointFloat(526.2214F, 94.91671F);
            this.ToDateLBL.Multiline = true;
            this.ToDateLBL.Name = "ToDateLBL";
            this.ToDateLBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ToDateLBL.SizeF = new System.Drawing.SizeF(139.5F, 43.83332F);
            this.ToDateLBL.StylePriority.UseFont = false;
            this.ToDateLBL.Text = "1400/10/18";
            this.ToDateLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ToDateLBL_BeforePrint);
            // 
            // FromDateLBL
            // 
            this.FromDateLBL.Font = new System.Drawing.Font("B Nazanin", 18F, System.Drawing.FontStyle.Bold);
            this.FromDateLBL.LocationFloat = new DevExpress.Utils.PointFloat(116.0132F, 94.91671F);
            this.FromDateLBL.Multiline = true;
            this.FromDateLBL.Name = "FromDateLBL";
            this.FromDateLBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.FromDateLBL.SizeF = new System.Drawing.SizeF(139.5F, 43.83332F);
            this.FromDateLBL.StylePriority.UseFont = false;
            this.FromDateLBL.Text = "1400/10/18";
            this.FromDateLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.FromDateLBL_BeforePrint);
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("B Nazanin", 18F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(27.55493F, 94.91671F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(88.375F, 43.83332F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "از تاریخ: ";
            // 
            // OldMachineCodeLBL
            // 
            this.OldMachineCodeLBL.Font = new System.Drawing.Font("B Nazanin", 26F, System.Drawing.FontStyle.Bold);
            this.OldMachineCodeLBL.LocationFloat = new DevExpress.Utils.PointFloat(317.9134F, 10.00002F);
            this.OldMachineCodeLBL.Multiline = true;
            this.OldMachineCodeLBL.Name = "OldMachineCodeLBL";
            this.OldMachineCodeLBL.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.OldMachineCodeLBL.SizeF = new System.Drawing.SizeF(189.9102F, 55.83332F);
            this.OldMachineCodeLBL.StylePriority.UseFont = false;
            this.OldMachineCodeLBL.StylePriority.UseTextAlignment = false;
            this.OldMachineCodeLBL.Text = "03-03-03";
            this.OldMachineCodeLBL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.OldMachineCodeLBL.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.OldMachineCodeLBL_BeforePrint);
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("B Nazanin", 26F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(141.1099F, 10.00002F);
            this.xrLabel3.Multiline = true;
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(176.8035F, 55.83332F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "کد دستگاه: ";
            // 
            // ReportImageURL
            // 
            this.ReportImageURL.AllowNull = true;
            this.ReportImageURL.Name = "ReportImageURL";
            // 
            // OeeChartReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(64, 58, 100, 45);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.ReportImageURL});
            this.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.RightToLeftLayout = DevExpress.XtraReports.UI.RightToLeftLayout.Yes;
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void EmployeePicture_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            EmployeePicture.ImageUrl = Parameters["ReportImageURL"].Value.ToString();
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

    private void FromDateLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("FromDate") != null)
            {
                cellObj.Text = GetCurrentColumnValue("FromDate").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void ToDateLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("ToDate") != null)
            {
                cellObj.Text = GetCurrentColumnValue("ToDate").ToString();
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

    private void OEELBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("OEE") != null)
            {
                cellObj.Text = GetCurrentColumnValue("OEE").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void EALBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("EquipmentAvailablity") != null)
            {
                cellObj.Text = GetCurrentColumnValue("EquipmentAvailablity").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void PELBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("Performance") != null)
            {
                cellObj.Text = GetCurrentColumnValue("Performance").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void QALBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("Quality") != null)
            {
                cellObj.Text = GetCurrentColumnValue("Quality").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void OEEResultLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("OEE") != null)
            {
                cellObj.Text = GetCurrentColumnValue("OEE").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void LineNameLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

    private void OPLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

    private void EmployeeNameLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("FullName") != null)
            {
                cellObj.Text = GetCurrentColumnValue("FullName").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void EIDLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("EID") != null)
            {
                cellObj.Text = GetCurrentColumnValue("EID").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void AvailablityHourLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("AvailablityHour") != null)
            {
                cellObj.Text = GetCurrentColumnValue("AvailablityHour").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void WorkOrderCountLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("WorkOrderCount") != null)
            {
                cellObj.Text = GetCurrentColumnValue("WorkOrderCount").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void MeanTimeToRepairLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("MeanTimeToRepair") != null)
            {
                cellObj.Text = GetCurrentColumnValue("MeanTimeToRepair").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void MeanTimeBetweenFailureLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("MeanTimeBetweenFailure") != null)
            {
                cellObj.Text = GetCurrentColumnValue("MeanTimeBetweenFailure").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private void EquipmentAvailablityLBL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        try
        {
            var cellObj = sender as XRTableCell;
            if(GetCurrentColumnValue("EquipmentAvailablity") != null)
            {
                cellObj.Text = GetCurrentColumnValue("EquipmentAvailablity").ToString();
            }
        }
        catch(Exception)
        {
            throw;
        }
    }



    private void xrTable3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {

    }
}
