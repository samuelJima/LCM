<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LCOpeningReport.aspx.cs" Inherits="AIB_FORMS_PRINT.AIB_REPORT.RepPages.LCOpeningReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script src="../crystalreportviewers13/js/crviewer/crv.js"></script> 
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server"
            AutoDataBind="True" EnableDatabaseLogonPrompt="true"
            Height="1202px" BestFitPage="False"
            ReportSourceID="CrystalReportSource1" 
            ToolPanelView="None" ToolPanelWidth="200px" Width="903px" ToolbarImagesFolderUrl="../crystalreportviewers13/js/crviewer/images/"
           EnableDrillDown="false" DisplayToolbar="True" GroupTreeImagesFolderUrl="../crystalreportviewers13/js/crviewer/images/"  HasGotoPageButton="True"
           HasPrintButton="True"  HasToggleGroupTreeButton="false" PageZoomFactor="80"
             />
      
        
    </div>
    </form>
</body>
</html>
