<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AIB_FORMS_PRINT.AIB_SECURITY.login" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <h2 style="margin-left:480px">
        Login</h2>
    <div style="margin-left:340px ">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
          
             <ContentTemplate>
                 <table style="background-repeat:no-repeat;background-size:cover;background-image:url('../Images/logoNew.png');width:400px;height:400px" >
                     <tr>
                         <td style="text-align:center;font-size:larger">
                             User Name
                         </td>
                     </tr>
                      <tr>
                         <td style="text-align:center">
                              <telerik:RadTextBox ID="UserName" runat="server" EmptyMessage="Enter Username"
                        Width="200px" LabelCssClass="" meta:resourcekey="UserNameResource1">
                    </telerik:RadTextBox>
                         </td>
                     </tr>
                     <tr>
                         <td style="text-align:center;font-size:larger">
                             Password
                         </td>
                     </tr>
                    <tr>
                         <td style="text-align:center;font-size:larger">
                            <telerik:RadTextBox ID="Password" runat="server" EmptyMessage="Enter Password" 
                        onpaste="return false" TextMode="Password" Width="200px" LabelCssClass="" >
                        </telerik:RadTextBox>
                         </td>
                     </tr>
                    <tr>
                         <td style="text-align:center" >
                               <asp:Button ID="btnLogin" runat="server" Text="Login" Width="200px"
                               onclick="btnLogin_Click1" />
                         </td>
                     </tr>
                     <tr>
                         <td style="text-align:center;font-size:larger">
                             <asp:Label ID="lblFailureText" runat="server" 
                        style="color: #CC0000; font-weight: 700" 
                        meta:resourcekey="lblFailureTextResource1"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td style="text-align:center;font-size:large">
                             <asp:LinkButton ID="btnForgotMyPassword" runat="server" CssClass="LabelStyle" ForeColor="Black"
                        PostBackUrl="~/AIB_SECURITY/Forgot_Password.aspx">Forgot My Password</asp:LinkButton>
                         </td>
                     </tr>
               
                </table>     
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click" />
            </Triggers>
           
             </asp:UpdatePanel>
    </div>
</asp:Content>
