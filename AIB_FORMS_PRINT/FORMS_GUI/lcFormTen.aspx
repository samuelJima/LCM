<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="lcFormTen.aspx.cs" Inherits="AIB_FORMS_PRINT.FORMS_GUI.lcFormTen" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">
    <asp:MultiView  ID="MultiPage1" runat="server" ActiveViewIndex="0"  > 
       <asp:View ID="PageView1" runat="server"    >
              <telerik:RadTicker ID="RadTicker1" runat="server" autoadvance="False" 
                backcolor="White" borderstyle="None" 
                style="text-align: center;position:absolute; margin-top:-180px" visible="False" width="53%">
                <Items>
                    <telerik:RadTickerItem ID="item1" runat="server" Font-Size="XX-Small" 
                       NavigateUrl="" 
                        Style="text-align: center; color : Green; font-weight:bold"></telerik:RadTickerItem>
                </Items>
            </telerik:RadTicker>
   <div id="outerDiv"style="margin-left:70px;position:initial; margin-top:-160px">
        <table>
            <tr>
                <td style="text-align:left; font-size:smaller">
                    <h2>L/C-CANCELATION</h2> <br />
                   <%-- <label>1.DEBIT TICKET</label> --%>
                </td>
                <td colspan="4" style="text-align:center">
                     <label></label> <br />
                    <label></label> 
                </td>
            </tr>
        
        </table>
    </div>
    <div style="margin-left:70px">
      <table>
             <tr>
                <td colspan="3" style="text-align:right;font-weight:normal;">
                    <label>Date</label>  
                </td>
                <td  >
                   
                    <asp:Label ID="lblCurrentDate" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
           <tr>
              <td>
                  
                  <label style="font-size:12px; font-weight:normal;width:100px" >Branch Name</label>
              </td>
               <td>
                   <telerik:RadTextBox ID="txtBranchName" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
                 
               </td>
               <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">Branch Code</label>
              </td>
               <td>
                   <telerik:RadTextBox ID="txtBranchCode" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
               </td>
          </tr>
          <tr>
             
               <td>
                  <label style="font-size:12px; font-weight:normal;width:100px" >L/C Number</label>
              </td>
               <td>
                  <telerik:RadTextBox ID="txtLCNumber" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
               </td>
           
              <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">Permit Number</label>
              </td>
              <td>
                   <telerik:RadTextBox ID="txtPermitNUmber" runat="server"  Width="120px" ReadOnly="true"  BackColor="Gray"></telerik:RadTextBox>
              </td>
               </tr>
           <tr>
              <td>  <label style="font-size:12px; font-weight:normal; width:100px">Opening Periods</label></td>
              <td>
                 <telerik:RadTextBox ID="txtOpeningPeriod" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
               
              </td>
              <td>  <label style="font-size:12px; font-weight:normal; width:100px">User limitation Days</label></td>
              <td>
                    <telerik:RadNumericTextBox ID="numUserLimitaition" runat="server" Width="120"  AutoPostBack="true" 
                        MinValue="0"   
                        >
                          <NumberFormat GroupSeparator="" DecimalDigits="0"  /> 
                     </telerik:RadNumericTextBox> 
              </td>
          </tr>
          <tr>
             <td>
                       <label style="font-size:12px; font-weight:normal;width:100px" > LC Value </label>
                 </td>
                 <td>
                      <telerik:RadNumericTextBox ID="numLcValue" runat="server" Width="120" ReadOnly="true" ></telerik:RadNumericTextBox> 
                 </td>
               
        
                 <td>
                     <label style="font-size:12px; font-weight:normal;width:100px" >Value Date</label>
              </td>
               <td>
                   <telerik:RadDatePicker ID="dateValueDate" runat="server" Enabled="false"></telerik:RadDatePicker>
                 
               </td>
                </tr>

             <tr>
                <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">In favour of</label>
              </td>
               <td>
                     <telerik:RadTextBox ID="txtSupplyer" runat="server" Width="120px" ReadOnly="true"></telerik:RadTextBox>
               </td>
         
              <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">Reimbursing Bank</label>
              </td>
               <td>
                  
                   <asp:DropDownList  ID="combRembursing" runat="server" Width="120px" Enabled="false"> 
                   </asp:DropDownList>

                   <ajaxToolkit:ListSearchExtender runat="server" BehaviorID="combRembursing_ListSearchExtender" TargetControlID="combRembursing" ID="combRembursing_ListSearchExtender"></ajaxToolkit:ListSearchExtender>
               </td>
                  </tr>
       
          <tr>
                 <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">Opened Through</label>
              </td>
               <td>
                
                   <asp:DropDownList ID="combOpThrough" runat="server" Width="120px" Enabled="false">
                       <asp:ListItem > please select Open through bank</asp:ListItem>
                   </asp:DropDownList>

                   <ajaxToolkit:ListSearchExtender runat="server" BehaviorID="combOpThrough_ListSearchExtender" TargetControlID="combOpThrough" ID="combOpThrough_ListSearchExtender"></ajaxToolkit:ListSearchExtender>
               </td>
          
                           <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">Customer Account</label>
              </td>
               <td>
                     <telerik:RadTextBox ID="txtAccountNO" runat="server" Width="120px" ReadOnly="true" ></telerik:RadTextBox>
               </td>
              </tr>
            <tr>
                   <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">For Account Of</label>
              </td>
               <td>
                 <telerik:RadTextBox ID="txtCustomerName" runat="server" Width="120px" ReadOnly="true"></telerik:RadTextBox>
               </td>
        
           
               <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">Currency</label>
              </td>
               <td>
                  <telerik:RadTextBox ID="txtCurrency" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
               </td>
                  </tr>
          <tr>
              <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">Currency Rate</label>
              </td>
              <td>
                     <telerik:RadNumericTextBox ID="numCurrencyRate" runat="server" Width="120" ReadOnly="true">
                          <NumberFormat GroupSeparator="" DecimalDigits="4"  />
                     </telerik:RadNumericTextBox> 
              </td> 
              
        
           
               <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">Unutilized Amount</label>
              </td>
               <td>
                   <telerik:RadNumericTextBox ID="numUnutilizedAmount" runat="server" Width="120" ReadOnly="true">
                          <NumberFormat GroupSeparator="" DecimalDigits="2"  />
                     </telerik:RadNumericTextBox> 
               </td>
              </tr> 
         <tr>
              <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">Post this Amount</label>
              </td>
              <td>
                     <telerik:RadNumericTextBox ID="numPostThisAmount" runat="server" Width="120" ReadOnly="true">
                          <NumberFormat GroupSeparator="" DecimalDigits="2"  />
                     </telerik:RadNumericTextBox> 
              </td> 
                <td>
                     <label style="font-size:12px ; font-weight:normal;width:100px" >LC Opening Date</label>
              </td>
               <td>
                   <telerik:RadDatePicker ID="dateOpeningDate" runat="server" Enabled="false"></telerik:RadDatePicker>
                 
               </td>
        </tr> 
          <tr>
              <td colspan="4" style="text-align:right">
                  <asp:Button ID="btnViewTicket" runat="server" Text="View Ticket" Onclick="bindOpeningReport"  />
              </td>
          </tr>   
      </table>
    </div>
    
 </asp:View> 
         
 </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
       
    <div style="margin-left:-15px; position:initial; margin-top:-310px">
                    <asp:Table ID="Table1" runat="server" >
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                <h3>Search Area</h3>
                            </asp:TableCell>                
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label1" runat="server" Text="Enter LC No" Width="80px" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <telerik:RadTextBox ID="txtSearch" runat="server" Width="80px"></telerik:RadTextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                                   <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="resetLCAdviceSite" Visible="true"/>
                               <asp:Button ID="Button5" runat="server" Text="search" OnClick="searchLCBylcNumber" Visible="false" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                   </div>

</asp:Content>

