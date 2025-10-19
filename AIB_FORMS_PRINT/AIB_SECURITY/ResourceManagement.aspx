<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="ResourceManagement.aspx.cs" Inherits="AIB_FORMS_PRINT.AIB_SECURITY.ResourceManagement" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">

     <table style="width:100%;position:initial; margin-top:-320px" >
           <tr>
        <td class="RadPanelBar_rtl" colspan="2" style="text-align: center">
         
            <asp:Label ID="lblMessage" runat="server" 
                style="font-weight: 700; background-color: #CCFFCC" 
                ></asp:Label>
         
        </td>
    </tr>
    <tr>
        <td class="RadPanelBar_rtl">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="RadPanelBar_rtl">
            <asp:Label ID="Label3" runat="server" Text="Resource Name" 
               ></asp:Label>
        </td>
        <td>
            <telerik:RadTextBox ID="txtResourceName" Runat="server" Width="120px" 
              >
            </telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="RadPanelBar_rtl">
            <asp:Label ID="Label4" runat="server" Text="Path" 
              ></asp:Label>
        </td>
        <td>
            <telerik:RadTextBox ID="txtPath" Runat="server" Width="120px" 
               >
            </telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="RadPanelBar_rtl">
            <asp:Label ID="lblDescription" runat="server" Text="Description" 
               ></asp:Label>
        </td>
        <td>
            <telerik:RadTextBox ID="txtDescription" Runat="server" Width="120px" 
                TextMode="MultiLine" >
            </telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="RadPanelBar_rtl">
         
        </td>
        <td>
            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" 
                Width="100px"/>
        </td>
    </tr>
  
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
</asp:Content>
