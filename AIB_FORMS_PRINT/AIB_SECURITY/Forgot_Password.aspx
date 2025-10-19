<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forgot_Password.aspx.cs" Inherits="AIB_FORMS_PRINT.AIB_SECURITY.Forgot_Password" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <h2 style="margin-left:250px">Forget Password</h2>
    <div style="text-align:center; margin-left:200px"> 
     <table style="width: 100%">
        <tr>
            <td style="height: 106px;" class="LabelStyle" colspan="2">
                <asp:Label ID="lblErr" runat="server" 
                    Style="color: #CC0000; font-weight: 700; font-size: large" 
                    meta:resourcekey="lblErrResource1"></asp:Label>
            </td>
            <td style="height: 106px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 101px; height: 52px;" class="LabelStyle">
                <asp:Label ID="Label12" runat="server" Text="User Name" 
                    meta:resourcekey="Label12Resource1"></asp:Label>
            </td>
            <td style="height: 52px; width: 252px;">
                <telerik:radtextbox id="txtUserName" runat="server" width="200px" 
                    meta:resourcekey="txtUserNameResource1">
                            </telerik:radtextbox>
            </td>
            <td style="height: 52px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 101px; height: 52px;" class="LabelStyle">
                <asp:Label ID="Label8" runat="server" Text="Security Question" 
                    meta:resourcekey="Label8Resource1"></asp:Label>
            </td>
            <td style="height: 52px; width: 252px;">
                <telerik:radcombobox id="cboSecurityQuestions" runat="server" autopostback="True"
                    width="200px" maxheight="150px" 
                    meta:resourcekey="cboSecurityQuestionsResource1">
                     <Items>
                        <telerik:RadComboBoxItem runat="server" 
                            Text="What was your childhood nickname?" 
                            Value="What was your childhood nickname?" 
                             meta:resourcekey="RadComboBoxItemResource1" />
                        <telerik:RadComboBoxItem runat="server" 
                            Text="What is the name of your favorite childhood friend?" 
                            Value="What is the name of your favorite childhood friend?" 
                             meta:resourcekey="RadComboBoxItemResource2" />
                        <telerik:RadComboBoxItem runat="server" 
                            Text="What is your oldest sibling’s birthday month and year? (e.g., January 1900)" 
                            
                             Value="What is your oldest sibling’s birthday month and year? (e.g., January 1900)" 
                             meta:resourcekey="RadComboBoxItemResource3" />
                        <telerik:RadComboBoxItem runat="server" 
                            Text="What is the middle name of your oldest child?" 
                            Value="What is the middle name of your oldest child?" 
                             meta:resourcekey="RadComboBoxItemResource4" />
                        <telerik:RadComboBoxItem runat="server" 
                            Text="What school did you attend for sixth grade?" 
                            Value="What school did you attend for sixth grade?" 
                             meta:resourcekey="RadComboBoxItemResource5" />
                        <telerik:RadComboBoxItem runat="server" 
                            Text="What was the name of your first stuffed animal?" 
                            Value="What was the name of your first stuffed animal?" 
                             meta:resourcekey="RadComboBoxItemResource6" />
                        <telerik:RadComboBoxItem runat="server" 
                            Text="In what city does your nearest sibling live?" 
                            Value="In what city does your nearest sibling live?" 
                             meta:resourcekey="RadComboBoxItemResource7" />
                        <telerik:RadComboBoxItem runat="server" 
                            Text="In what city or town was your first job?" 
                            Value="In what city or town was your first job?" 
                             meta:resourcekey="RadComboBoxItemResource8" />
                        <telerik:RadComboBoxItem runat="server" 
                            Text="What is the name of a college you applied to but didn't attend?" 
                            Value="What is the name of a college you applied to but didn't attend?" 
                             meta:resourcekey="RadComboBoxItemResource9" />
                    </Items>
                </telerik:radcombobox>
            </td>
            <td style="height: 52px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 101px; height: 58px;" class="LabelStyle">
                <asp:Label ID="Label9" runat="server" Text="Security Answer" 
                    ></asp:Label>
            </td>
            <td style="height: 58px; width: 252px;">
                <telerik:radtextbox id="txtSecurityAnswer" runat="server" width="200px"
                    textmode="Password" meta:resourcekey="txtSecurityAnswerResource1">
                            </telerik:radtextbox>
            </td>
            <td style="height: 58px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 101px" css="LabelStyle" class="LabelStyle">
                &nbsp;
            </td>
            <td style="width: 252px">
                <asp:Button ID="Submit" runat="server" CommandName="Submit" Text="Submit" Width="100px"
                   />
            </td>
            <td>
                <asp:LinkButton ID="btnHiddn" runat="server" 
                    meta:resourcekey="btnHiddnResource1"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="width: 101px" css="LabelStyle" class="LabelStyle">
                <telerik:radtooltip id="ttpConfirm" runat="server" showevent="FromCode" manualclose="True"
                    modal="True" position="Center" relativeto="BrowserWindow" skin="Forest" 
                    HideEvent="ManualClose" meta:resourcekey="ttpConfirmResource1">
                <table style="width:100%;" cellspacing="10" >
                        <tr >
                            <td class="style3" colspan="2" >
                                <asp:Label ID="lblErrPassword" runat="server" 
                                    style="color: #CC0000; font-weight: 700; font-size: medium" 
                                    meta:resourcekey="lblErrPasswordResource1"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr >
                            <td class="style3" >
                                <asp:Label ID="Label10" runat="server" Text="New Password" 
                                    meta:resourcekey="Label10Resource1"></asp:Label>
                            </td>
                            <td class="style2" >
                                <telerik:RadTextBox ID="txtNewPassword" runat="server" 
                                    TextMode="Password" Width="200px" LabelCssClass="" 
                                    meta:resourcekey="txtNewPasswordResource1">
                                </telerik:RadTextBox>
                            </td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        <tr >
                            <td class="style3" >
                                <asp:Label ID="Label11" runat="server" Text="Confirm Password" 
                                    meta:resourcekey="Label11Resource1"></asp:Label>
                            </td>
                            <td class="style2" >
                                <telerik:RadTextBox ID="txtConfirmPassword" runat="server" 
                                    TextMode="Password" Width="200px" LabelCssClass="" 
                                    meta:resourcekey="txtConfirmPasswordResource1">
                                </telerik:RadTextBox>
                            </td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        <tr >
                            <td class="style3" >
                                &nbsp;</td>
                            <td class="style1" >
                                <asp:Button ID="btnOk" runat="server" Text="Submit" 
                                    Width="100px" />
                            </td>
                            <td >
                                &nbsp;</td>
                        </tr>
                    </table>
            </telerik:radtooltip>
            </td>
            <td style="width: 252px">
                <asp:LinkButton ID="btnLogin" runat="server" PostBackUrl="~/AIB_SECURITY/login.aspx"
                    meta:resourcekey="btnLoginResource1">&lt; Back To Login Page</asp:LinkButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
       </div> 
</asp:Content>
