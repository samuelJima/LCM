<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="createAccount.aspx.cs" Inherits="AIB_FORMS_PRINT.AIB_SECURITY.createAccount" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">

    <div style="position:initial; margin-top:-240px;margin-left:70px">
          <telerik:RadTicker ID="RadTicker1" runat="server" autoadvance="False" 
                backcolor="White" borderstyle="None" 
                style="text-align: center;position:absolute; margin-top:-50px" visible="False" width="53%">
                <Items>
                    <telerik:RadTickerItem ID="item1" runat="server" Font-Size="XX-Small" 
                       NavigateUrl="" 
                        Style="text-align: center; color : Green; font-weight:bold"></telerik:RadTickerItem>
                </Items>
            </telerik:RadTicker>
    <h2>
        User Management</h2>
        </div>
   
        <asp:Panel ID="pnlUser" runat="server" meta:resourcekey="pnlUserResource1">
            <table style="width: 100%;margin-left:70px ">
                <tr>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="Label1" runat="server" Visible="False" 
                           ></asp:Label>
                        <asp:HiddenField ID="SelectedUser" runat="server" />
                    </td>
                    <td style="height: 21px">
                        &nbsp;
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="Label2" runat="server" Text="First Name" 
                          ></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtUserFName" runat="server" Width="120px" 
                            LabelCssClass="" >
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="lblRole" runat="server" Text="Role" 
                           ></asp:Label>
                    </td>
                    <td style="height: 26px">
                        <telerik:RadComboBox ID="cboRole" runat="server" AllowCustomText="True" 
                            AutoPostBack="True" Width="120px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name" 
                           ></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtUserMName" runat="server" Width="120px" 
                            LabelCssClass="" >
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 120px; text-align: left">
                        <asp:Label ID="lblEmail" runat="server" Text="Email" 
                            ></asp:Label>
                    </td>
                    <td style="height: 26px">
                        <telerik:RadTextBox ID="txtEmail" runat="server" Width="120px" LabelCssClass="" 
                          >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="lblUserName" runat="server" Text="Last Name" 
                          ></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtUserLName" runat="server" Width="120px" 
                            LabelCssClass="" >
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 100px; text-align: left">
                        <asp:Label ID="lblUsersName" runat="server" Text="UserName" 
                          ></asp:Label>
                    </td>
                    <td style="height: 26px">
                        <telerik:RadTextBox ID="txtUserName" runat="server" Width="120px" 
                            LabelCssClass="" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                   
                    <td style=" width:100px;text-align: left">
                        <asp:Label ID="lblPassword" runat="server" Text="Password" 
                            ></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <telerik:RadTextBox ID="txtPassword" runat="server" onpaste="return false" TextMode="Password"
                            Width="120px" LabelCssClass="">
                        </telerik:RadTextBox>
                    </td>
                     <td style="width:100px;text-align: left; height: 52px;">
                        <asp:Label ID="lblConfirmPassword" runat="server" CssClass="LabelStyle" 
                            Text="Confirm Password"></asp:Label>
                    </td>
                    <td class="RadPanelBar_rtl" style="text-align: left; height: 52px;">
                        <telerik:RadTextBox ID="txtConfirmPassword" onpaste="return false" 
                            runat="server" TextMode="Password" Width="120px" LabelCssClass="" 
                          >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style=" width:100px;text-align: left; height: 52px;">
                        <asp:Label ID="lblDepartment" runat="server" Text="Branch" 
                           ></asp:Label>
                    </td>
                    <td style="text-align: left; height: 52px;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <telerik:RadComboBox ID="cmbDepartment" runat="server" AllowCustomText="True" 
                                    MaxHeight="200px"  Skin="Windows7" 
                                    Width="120px">
                                </telerik:RadComboBox>
                            </ContentTemplate>
                           
                        </asp:UpdatePanel>
                    </td>
                    <td>
                   </td>
                    <td>

                    </td>
                </tr>
               
            </table>
        </asp:Panel>
   
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right;">

                    <telerik:RadToolBar ID="btnDelete" runat="server" OnButtonClick="btnDelete_ButtonClick"
                        Height="20px" Visible="False" Width="95px" Style="font-weight: 700" 
                        Skin="Black"   >
                        <Items>
                            <telerik:RadToolBarDropDown runat="server" Text="Deactivate" 
                               >
                                <Buttons>
                                    <telerik:RadToolBarButton runat="server" Text="Permanently" 
                                       >
                                    </telerik:RadToolBarButton>
                                    <telerik:RadToolBarButton runat="server" Text="Temporary" 
                                       >
                                    </telerik:RadToolBarButton>
                                </Buttons>
                            </telerik:RadToolBarDropDown>
                        </Items>
                    </telerik:RadToolBar>
                    <asp:Button ID="btnActivate" Width="100px" runat="server" Text="Activate" 
                        Visible="False" OnClick="btnDelete_Click"
                        />
                    <asp:Button ID="btnUpdate" runat="server" 
                        Text="Update" Visible="False" Width="100px" Enabled="False" 
                       OnClick="btnUpdate_Click" />
                    <asp:Button ID="save" runat="server"  Width="100px" Text="Save" 
                         OnClick="save_Click"  />
                </td>
            </tr>
        </table>
        <telerik:radtooltip id="ttpConfirm" runat="server" showevent="FromCode" manualclose="True"
            modal="True" position="Center" relativeto="BrowserWindow" Skin="Forest" 
            HideEvent="ManualClose" meta:resourcekey="ttpConfirmResource1" >
                <table cellspacing="10" style="width: 100%;">                
                    <tr>
                        <td style="font-size: medium; font-weight: 700">
                            Do you want to deactivate the user account?
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                            <asp:Button ID="btnYes" runat="server" Text="Yes" 
                                Width="100px" OnClick="btnYes_Click" />
                            <asp:Button ID="btnNo" runat="server"  Text="No" 
                                Width="100px"  OnClick="btnNo_Click" />
                        </td>
                    </tr>
                </table>
            </telerik:radtooltip>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
 <h4 style="position:initial; margin-top:-280px;margin-left:100px"">
        Search Area
    </h4>
    <br />
    <br />
    <table style="margin-left:100px">
        <tr>
            <td>
                  <asp:Label ID="lblSearchUser" runat="server" Text="Active User Accounts" 
                 Width="200px"></asp:Label>
            </td>
            </tr>
            <tr>
            <td>
                 <telerik:RadComboBox ID="cmbSearchUser" runat="server" Width="150px" AllowCustomText="True"
                MarkFirstMatch="True" AutoPostBack="True" EmptyMessage="Search By User Name"
                CssClass="LabelStyle" Filter="StartsWith" MaxHeight="200px" 
              onselectedindexchanged="cmbSearchUser_SelectedIndexChanged ">
            </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:Label ID="Label3" runat="server" Text="Inactive User Accounts" Width="200px"></asp:Label>
            </td>
             </tr>
            <tr>
            <td>
                  <telerik:RadComboBox ID="cmbDisabledAcc" runat="server" Width="150px" AllowCustomText="True"
                MarkFirstMatch="True" AutoPostBack="True" EmptyMessage="Search By User Name"
                CssClass="LabelStyle" Filter="StartsWith" MaxHeight="200px" 
                 onselectedindexchanged="cmbDisabledAcc_SelectedIndexChanged" >
            </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td  style="text-align:center">
                <asp:LinkButton ID="btnAddNewUser" runat="server" 
                style="font-weight: 700" onclick="btnAddNewUser_Click"  >Add New User</asp:LinkButton>
            </td>
        </tr>
    </table>
   
  </asp:Content>
