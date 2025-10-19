<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="lcReports.aspx.cs" Inherits="AIB_FORMS_PRINT.AIB_REPORT.RepPages.lcReports" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">
    
   
     <div>
         
         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" HasToggleGroupTreeButton="false"
             AutoDataBind="True" HasCrystalLogo="False" ReuseParameterValuesOnRefresh="false"
             Height="590px" BestFitPage="False" BorderStyle="None"
             Width="740px" PrintMode="ActiveX" HasPrintButton="True" EnableParameterPrompt="False" ToolPanelView="None"
             
             EnableDatabaseLogonPrompt="False" ToolbarImagesFolderUrl=".~/crystalreportviewers13/js/crviewer/images/"
             GroupTreeImagesFolderUrl="~/crystalreportviewers13/images/"
           />
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
    
</asp:Content>
