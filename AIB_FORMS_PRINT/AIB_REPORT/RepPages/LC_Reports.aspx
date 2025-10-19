<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LC_Reports.aspx.cs" Inherits="AIB_FORMS_PRINT.AIB_REPORT.RepPages.LC_Reports" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../../crystalreportviewers13/js/crviewer/crv.js"></script>
    
</head>
      
    
   
<body >
    <form id="form1" runat="server">
         <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
           
             </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" Runat="server"/> 
         <header style="background-color: #00b1ff">
        <div style="float:left; margin-left:40px">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/aibLogo.png" Width="60px" />
        </div>
      
      <div style="float: right; margin-left: 10px; text-decoration: none">
            <asp:LinkButton  ID="btnLogOut" ForeColor="White" runat="server" OnClick="btnLogOut_Click"></asp:LinkButton>
        </div>
        <div style="float: right; margin-left: 10px; text-decoration: none">
            <asp:HyperLink ID="hprHelp" ForeColor="White" runat="server" Target="_blank" Text="Help|"></asp:HyperLink>
        </div>
               <div style="float: right; margin-left: 10px; text-align: right; text-decoration: none">
            <asp:HyperLink ID="Back" ForeColor="White" runat="server" NavigateUrl="~/FORMS_GUI/lcFormSevenNew.aspx"
                Text="Back|"></asp:HyperLink>
        </div>
          
       
        <div style="float: right; margin-left: 10px; text-align: right; text-decoration: none">
            <asp:HyperLink ID="hprHome" ForeColor="White" runat="server" NavigateUrl="~/Default.aspx"
                Text="Home|"></asp:HyperLink>
        </div>
       
       <%--  <div style="float: right; margin-left: 10px; text-decoration: none">
            <asp:LinkButton ID="btnAmharic" CommandArgument="am-ET" ForeColor="White" runat="server" OnClick="RequestLanguageChange_Click" Text="አማርኛ|"></asp:LinkButton>
        </div>
        <div style="float: right; margin-left: 10px; text-decoration: none;">
            <asp:LinkButton ID="btnEnglish"  CommandArgument="en-US" ForeColor="White" runat="server" OnClick="RequestLanguageChange_Click" Text="English|"></asp:LinkButton>
        </div>--%>
        <div style="height: 54px">
            <asp:Image ID="Image3" runat="server" Height="20px" ImageUrl="~/Images/admin.jpeg" />
            <asp:Label ID="lbluser" runat="server" Style="color: White; font-weight: 700"></asp:Label>
        </div>
          <div style=" margin-left:300PX; margin-top:-40px" >
            <h2 style="color:white"> AWASH INTERNATIONAL BANK </h2>
        </div>
    </header>
    <div id="body" style="background-color:white;margin-left:40px">
      <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" HasToggleGroupTreeButton="false"
             AutoDataBind="true" HasCrystalLogo="False" ReuseParameterValuesOnRefresh="false"
             Height="700px" BestFitPage="False" BorderStyle="None"
             Width="903px" PrintMode="ActiveX" HasPrintButton="True" EnableParameterPrompt="False" ToolPanelView="None"
             EnableDatabaseLogonPrompt="False" HasExportButton="False" HasZoomFactorList="False" HasSearchButton="False" HasRefreshButton="False"
             HasToggleParameterPanelButton="false"
           />
    </div>
          <footer  style="background-color: #00b1ff; height:40px">
        <div class="content-wrapper" >
            <div class="float-left">
                <p style="color:White">&copy; <%: DateTime.Now.Year %> - LC Forms Printing Application</p>
            </div>
        </div>
    </footer>
    </form>
</body>
</html>
