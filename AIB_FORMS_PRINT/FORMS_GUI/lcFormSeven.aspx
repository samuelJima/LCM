<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="lcFormSeven.aspx.cs" Inherits="AIB_FORMS_PRINT.FORMS_GUI.lcFormSeven" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">
    <div id="outerDiv">
        <table>
            <tr>
                <td style="text-align:left; font-size:smaller">
                    <label>L/C OPENING FORM</label> <br />
                    <label>1.DEBIT TICKET</label> 
                </td>
                <td colspan="4" style="text-align:center">
                     <label></label> <br />
                    <label>AWASH INTERNATIONAL BANK S.C</label> 
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td>
                    <asp:TextBox runat="server" Width="100px"/>
                </td>
                <td>
                    Branch
                </td>
                <td>
                    <asp:TextBox runat="server" Width="100px"/>
                </td>
                <td>
                    Date
                </td>
            </tr>
        </table>
    </div>
    <div style="border:2px solid red">
       <table>
           <tr>
               <td>
                   Branch code
               </td>
               <td>
                   L/C No
               </td>
               <td>
                   AIB Code
               </td>
               <td>
                   Value Date
               </td>
               <td>
                   Exchange Cont.Permit No
               </td>
           </tr>
            <tr>
               <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="60px"/>
               </td>
               <td>
                  <asp:TextBox ID="TextBox2" runat="server" Width="100px"/>
               </td>
               <td>
                    <asp:TextBox ID="TextBox3" runat="server" Width="60px"/>
               </td>
               <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="100px"/>
               </td>
               <td>
                    <asp:TextBox ID="TextBox5" runat="server" Width="100px"/>
               </td>
           </tr> 
       </table>
        <table style="width:100%">
            <tr>
                <td>
                    <table style="border:2px solid red; width:120%">
                        <tr>
                            <td>
                                Reimbursing Bank:
                            </td>
                            <td>

                            </td>
                        </tr>
                          <tr>
                            <td colspan="2">

                            </td>
                        </tr>
                         <tr>
                            <td>
                                Opened Through:
                            </td>
                            <td>

                            </td>
                        </tr>
                          <tr>
                            <td colspan="2">

                            </td>
                        </tr>
                         <tr>
                            <td>
                                In favour of:
                            </td>
                            <td>

                            </td>
                        </tr>
                          <tr>
                            <td colspan="2">

                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                     <table style="border:2px solid red; width:75%; margin-left:125px">
                         <tr>
                             <td>
                                 DEBIT
                             </td>
                              <td style="border:2px solid red">
                                 
                             </td>
                         </tr>
                         <tr >
                             <td colspan="2">

                             </td>
                         </tr>
                     </table>
                    <table  style="border:2px solid red; width:75%; margin-left:125px"">
                        <tr>
                            <td>

                            </td>
                            <td>
                                 Foregn/Cy
                            </td>
                            <td>
                                Rate
                            </td>
                            <td>
                                Local/Cy
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
   
</asp:Content>
