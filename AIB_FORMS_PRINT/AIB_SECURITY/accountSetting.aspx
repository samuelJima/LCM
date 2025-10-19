<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="accountSetting.aspx.cs" Inherits="AIB_FORMS_PRINT.AIB_SECURITY.accountSetting" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">
    <div style="margin-left:150px">
            <table  style="width: 100%;position:initial; margin-top:-150px">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblErr0" runat="server" Text="" Style="color: #CC3300; font-weight: 700"></asp:Label>
                          
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 34px">
                            <asp:Label ID="lblRoleName0" runat="server" 
                                style="font-family: Arial, Helvetica, sans-serif" Text="Name"></asp:Label>
                        </td>
                        <td style="height: 34px">
                            <asp:Label ID="lblName" runat="server" 
                                style="font-family: Arial, Helvetica, sans-serif; font-weight: 700;"></asp:Label>
                            &nbsp;&nbsp;</td>
                        <td style="height: 34px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 34px">
                            <asp:Label ID="lblRoleName1" runat="server" 
                                style="font-family: Arial, Helvetica, sans-serif" Text="Email"></asp:Label>
                        </td>
                        <td style="height: 34px">
                            <asp:Label ID="Email" runat="server" 
                                style="font-family: Arial, Helvetica, sans-serif; font-weight: 700;" ></asp:Label>
                        </td>
                        <td style="height: 34px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 34px">
                            <asp:Label ID="lblRoleName2" runat="server" 
                                style="font-family: Arial, Helvetica, sans-serif" Text="User Name"></asp:Label>
                        </td>
                        <td style="height: 34px">
                            <asp:Label ID="lblUSerName" runat="server" 
                                style="font-family: Arial, Helvetica, sans-serif; font-weight: 700;"></asp:Label>
                        </td>
                        <td style="height: 34px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 34px">
                            <asp:Label ID="Label1" runat="server" style="color: #003399; font-weight: 700" 
                                Text="Change Password"></asp:Label>
                        </td>
                        <td style="height: 34px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 34px">
                            <asp:Label ID="lblRoleName" runat="server" CssClass="LabelStyle" 
                                style="font-family: Arial, Helvetica, sans-serif" Text="Old Password"></asp:Label>
                        </td>
                        <td style="height: 34px">
                            <telerik:RadTextBox ID="txtOldPassword" runat="server" Skin="Default" 
                                Width="200px" TextMode="Password">
                            </telerik:RadTextBox>
                        </td>
                        <td style="height: 34px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRoleRemark" runat="server" CssClass="LabelStyle" 
                                Text="New Password"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtNewPassword" runat="server" Skin="Default" 
                                Width="200px" TextMode="Password">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRoleRemark0" runat="server" CssClass="LabelStyle" 
                                Text="Confirm Password"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtConfirmPassword" runat="server" Skin="Default" 
                                Width="200px" TextMode="Password">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btnChangePassword" runat="server" Text="Change" Width="100px" 
                                onclick="btnChangePassword_Click" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server" style="color: #003399; font-weight: 700" 
                                Text="Change Security Settings"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRoleRemark1" runat="server" 
                                Text="Security Question"></asp:Label>
                        </td>
                        <td>
                             <telerik:radcombobox id="CboSecQuestion" runat="server" zindex = "100000000" maxheight = "150px"
                allowcustomtext="True" width="200px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" 
                                                Text="What was your childhood nickname?" 
                                                Value="What was your childhood nickname?" />
                                            <telerik:RadComboBoxItem runat="server" 
                                                Text="What is the name of your favorite childhood friend?" 
                                                Value="What is the name of your favorite childhood friend?" />
                                            <telerik:RadComboBoxItem runat="server" 
                                                Text="What is your oldest sibling’s birthday month and year? (e.g., January 1900)" 
                                                Value="What is your oldest sibling’s birthday month and year? (e.g., January 1900)" />
                                            <telerik:RadComboBoxItem runat="server" 
                                                Text="What is the middle name of your oldest child?" 
                                                Value="What is the middle name of your oldest child?" />
                                            <telerik:RadComboBoxItem runat="server" 
                                                Text="What school did you attend for sixth grade?" 
                                                Value="What school did you attend for sixth grade?" />
                                            <telerik:RadComboBoxItem runat="server" 
                                                Text="What was the name of your first stuffed animal?" 
                                                Value="What was the name of your first stuffed animal?" />
                                            <telerik:RadComboBoxItem runat="server" 
                                                Text="In what city does your nearest sibling live?" 
                                                Value="In what city does your nearest sibling live?" />
                                            <telerik:RadComboBoxItem runat="server" 
                                                Text="In what city or town was your first job?" 
                                                Value="In what city or town was your first job?" />
                                            <telerik:RadComboBoxItem runat="server" 
                                                Text="What is the name of a college you applied to but didn't attend?" 
                                                Value="What is the name of a college you applied to but didn't attend?" />
                                        </Items>
                                        </telerik:radcombobox></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRoleRemark2" runat="server" CssClass="LabelStyle" 
                                Text="Security Answer"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSecurityAnswer" runat="server" Skin="Default" 
                                Width="200px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnChangeSecurity" runat="server" Text="Change"
                               Width="100px" onclick="btnChangeSecurity_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
</asp:Content>
