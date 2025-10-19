<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LC_Reports.aspx.cs" Inherits="AIB_FORMS_PRINT.AIB_REPORT.RepPages.LC_Reports" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     

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
</asp:Content>
 