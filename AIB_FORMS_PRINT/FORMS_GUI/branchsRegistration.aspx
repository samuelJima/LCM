<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="branchsRegistration.aspx.cs" Inherits="AIB_FORMS_PRINT.FORMS_GUI.branchsRegistration" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">
    <br />
            <telerik:RadTicker ID="RadTicker1" runat="server" autoadvance="False" 
                backcolor="White" borderstyle="None" 
                style="text-align: center;position:absolute; margin-top:-330px" visible="False" width="53%">
                <Items>
                    <telerik:RadTickerItem ID="item1" runat="server" Font-Size="XX-Small" 
                       NavigateUrl="" 
                        Style="text-align: center; color : Green; font-weight:bold"></telerik:RadTickerItem>
                </Items>
            </telerik:RadTicker>
    
    <div  style="margin-right:-40px;position:initial; margin-top:-320px;margin-left:30px">
        <table>
            <tr>
                <td colspan="4">
                     <h2>Branchs Registration</h2> <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Branch Name" Width="100px"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtBranchName" runat="server" Width="120px"></telerik:RadTextBox>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Branch Abrivation" Width="120px"></asp:Label>
                </td>
                <td>
                   <telerik:RadTextBox ID="txtBranchAbrivation" runat="server" Width="120px"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                   <td>
                    <asp:Label ID="Label4" runat="server" Text="Branch Code" Width="100px"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtBranchCode" runat="server"></telerik:RadTextBox>
                </td>
                <td>

                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="saveBranch" />
                    <asp:Button ID="btnUpdate" runat="server" Text="update" OnClick="updateBranch" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
     <div style="margin-left:35px;position:initial; margin-top:-300px">
                    <asp:Table ID="Table1" runat="server" >
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                <h3>Search Area</h3>
                            </asp:TableCell>                
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label3" runat="server" Text="Select Branch Name" Width="120px" />
                            </asp:TableCell>
                            <asp:TableCell>
                                  <asp:DropDownList  ID="comboBranchName" runat="server" Width="120px" AutoPostBack="true"> 
                                  </asp:DropDownList>
                               
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                                  <asp:Button ID="btnReset" runat="server" Text="search" OnClick="resetBranchInfo" Visible="false" />
                               <asp:Button ID="btnSearch" runat="server" Text="search" OnClick="searchBranchByName" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                   </div>
</asp:Content>
