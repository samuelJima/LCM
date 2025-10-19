<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="RoleManagement.aspx.cs" Inherits="AIB_FORMS_PRINT.AIB_SECURITY.RoleManagement" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">

     <telerik:RadTicker ID="RadTicker1" runat="server" autoadvance="False" 
                backcolor="White" borderstyle="None" 
                style="text-align: center;position:absolute; margin-top:-130px ;margin-left:35px" visible="False" width="53%">
                <Items>
                    <telerik:RadTickerItem ID="item1" runat="server" Font-Size="XX-Small" 
                       NavigateUrl="" 
                        Style="text-align: center; color : Green; font-weight:bold"></telerik:RadTickerItem>
                </Items>
            </telerik:RadTicker>

      <h2 style="position:initial; margin-top:-100px;margin-left:35px">Role Management</h2>
   
        <table style="width: 100%;margin-left:35px" >
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="height: 24px">
                                       
                                        Role:
                                        <asp:Label ID="lblRole" runat="server" style="font-weight: 700" 
                                         ></asp:Label>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:HiddenField ID="SelectedRole" runat="server" Value="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gridAccessLevels" runat="server" Width="100%"
                                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="4" ForeColor="Black" GridLines="Horizontal"
                                            ShowHeaderWhenEmpty="True" OnDataBound="gridAccessLevels_DataBound"
                                            OnRowDataBound="GridView2_RowDataBound">
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#0095dd" ForeColor="White"
                                                HorizontalAlign="Left" />
                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CC3333" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                 <tr>
                                    <td >
                                       
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <div>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: right;">
                        <asp:Button ID="save"  Width="100px" runat="server" Text="Update" 
                            onclick="save_Click" Visible="False" Enabled="true" 
                          />
                    </td>
                </tr>
            </table>
          
        </div>
    
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
     <h4  style="position:initial; margin-top:-280px">
        Search Area
    </h4>
     <table>
    <tr>
    <td>
     <telerik:radcombobox emptymessage="Select Active Roles Here" id="cboRole" runat="server"
                allowcustomtext="True" autopostback="True" width="120px" 
                onselectedindexchanged="cboRole_SelectedIndexChanged" 
                meta:resourcekey="cboRoleResource1">
                                        </telerik:radcombobox>
    </td>
    </tr>
     <tr>
    <td>
       <asp:LinkButton ID="btnAddNewRole" runat="server" onclick="btnAddNewRole_Click1" 
                style="font-weight: 700" meta:resourcekey="btnAddNewRoleResource1">Add New Role</asp:LinkButton>
    </td>
    </tr>
         <tr>
             <td>
                 <telerik:radtooltip id="ttpRole" runat="server" enableshadow="True" manualclose="True"
                showevent="OnClick" targetcontrolid="btnAddNewRole" HideEvent="ManualClose" 
                  meta:resourcekey="ttpRoleResource1">
                <table cellspacing="10" style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblErr0" runat="server" Style="color: #FF3300" 
                                meta:resourcekey="lblErr0Resource1"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 34px">
                            <asp:Label ID="lblRoleName" runat="server" CssClass="LabelStyle" 
                                Text="Role Name" style="font-family: Arial, Helvetica, sans-serif" 
                                meta:resourcekey="lblRoleNameResource1"></asp:Label>
                        </td>
                        <td style="height: 34px">
                            <telerik:RadTextBox ID="txtRole" runat="server" Skin="Windows7" Width="200px" 
                                LabelCssClass="" meta:resourcekey="txtRoleResource1">
                            </telerik:RadTextBox>
                        </td>
                        <td style="height: 34px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRoleRemark" runat="server" CssClass="LabelStyle" 
                                Text="Remark" meta:resourcekey="lblRoleRemarkResource1"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtRemark" runat="server" Skin="Windows7" TextMode="MultiLine"
                                Width="100px" LabelCssClass="" meta:resourcekey="txtRemarkResource1">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnAddRole" runat="server" OnClick="btnAddRole_Click" Text="Add"
                                CssClass="buttonstyle" Width="120px" Enabled="true" 
                                />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </telerik:radtooltip>
             </td>
         </tr>
    </table>
   
</asp:Content>
