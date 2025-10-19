<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="lcExtension.aspx.cs" Inherits="AIB_FORMS_PRINT.FORMS_GUI.lcExtension" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">
       
      <asp:MultiView  ID="MultiPage1" runat="server" ActiveViewIndex="0"  > 
       <asp:View ID="PageView1" runat="server"    >
            <telerik:RadTicker ID="RadTicker1" runat="server" autoadvance="False" 
                backcolor="White" borderstyle="None" 
                style="text-align: center;position:initial; margin-top:0px" visible="False" width="100%">
                <Items>
                    <telerik:RadTickerItem ID="item1" runat="server" Font-Size="XX-Small" 
                       NavigateUrl="" 
                        Style="text-align: center; color : Green; font-weight:bold"></telerik:RadTickerItem>
                </Items>
            </telerik:RadTicker>
          <div id="outerDiv"style="margin-left:30px; margin-top:-20px ;position:initial ">
        <table>
            <tr>
                <td style="text-align:left; font-size:smaller">
                    <h2>LC EXTENTION</h2>                   
                </td>
                </tr>    <a href="lcExtension.aspx">lcExtension.aspx</a>
        </table>
    </div>
    <div style="margin-left:30px">
      <table>
            <tr>
                <td colspan="4" >
                    <h3>LC information</h3><br />
                    <hr />
                </td>
            </tr>
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
                  
                  <label style="font-size:12px; font-weight:normal; width:100px" >Branch Name</label>
              </td>
               <td>
                   <telerik:RadTextBox ID="txtBranchName" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
                 
               </td>
               <td>
                  <label style="font-size:12px; font-weight:normal; width:100px">Branch Code</label>
              </td>
               <td>
                   <telerik:RadTextBox ID="txtBranchCode" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
               </td>
          </tr>
          <tr>
              
               <td>
                  <label style="font-size:12px ; font-weight:normal; width:100px" > L/C Number</label>
              </td>
               <td>
                  <telerik:RadTextBox ID="txtLCNumber" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
               </td>
              
                 <td>
                  <label style="font-size:12px; font-weight:normal; width:100px">Parent Permit Number</label>
              </td>
               <td>
                   <telerik:RadTextBox ID="txtPermitNUmber" runat="server"  Width="120px" ReadOnly="true"></telerik:RadTextBox>
              </td>
           </tr>
             <tr>
              <td>  <label style="font-size:12px; font-weight:normal; width:100px">Opening Periods</label></td>
              <td>
                  <telerik:RadComboBox ID="comboPeriod" runat="server"  Width="120" Enabled="false">
                      <Items>
                         
                             <telerik:RadComboBoxItem Value="1" Text="1" />
                             <telerik:RadComboBoxItem Value="2" Text="2" />
                             <telerik:RadComboBoxItem Value="3" Text="3" />
                             <telerik:RadComboBoxItem Value="4" Text="4" />
                             <telerik:RadComboBoxItem Value="5" Text="5" />
                             <telerik:RadComboBoxItem Value="6" Text="6" />
                             <telerik:RadComboBoxItem Value="7" Text="7" />
                             <telerik:RadComboBoxItem Value="8" Text="8" />
                             <telerik:RadComboBoxItem Value="9" Text="9" />
                           <telerik:RadComboBoxItem Value="10" Text="10" />
                      </Items>
                  </telerik:RadComboBox>
              </td>
              <td>  <label style="font-size:12px; font-weight:normal; width:100px">User limitation Days</label></td>
              <td>
                    <telerik:RadNumericTextBox ID="numUserLimitaition" runat="server" Width="120" ReadOnly="true"
                        MinValue="0"   
                        >
                          <NumberFormat GroupSeparator="" DecimalDigits="0"  /> 
                     </telerik:RadNumericTextBox> 
              </td>
          </tr>
           <tr>
               <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Customer Account</label>
              </td>
              <td>
                     <telerik:RadTextBox ID="txtAccountNO" runat="server" Width="120px"></telerik:RadTextBox>
               </td>
                 <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Debit</label>
              </td>
               <td>
                 <telerik:RadTextBox ID="txtAccountBranch" runat="server" Width="120px" Visible="false"></telerik:RadTextBox>
                 <telerik:RadTextBox ID="txtCustomerName" runat="server" Width="120px" ReadOnly="true"></telerik:RadTextBox>
               </td>
           </tr>
            <tr>
                <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Reimbursing Bank</label>
              </td>
               <td>
                  
                   <asp:DropDownList  ID="combRembursing" runat="server" Width="120px" 
                       EnableViewState="true" ViewStateMode="Enabled" Enabled="false"> 
                   </asp:DropDownList>
              </td>
                 <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Currency</label>
              </td>
               <td>
                  <telerik:RadComboBox ID="combCurrency" runat="server" Width="120px" ZIndex="1000000"  Enabled="false">
                   </telerik:RadComboBox>
               </td>
            </tr>
             <tr>
                 <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Opened Through</label>
              </td>
               <td>
                
                   <asp:DropDownList ID="combOpThrough" runat="server" Width="120px" Enabled="false">
                       <asp:ListItem > please select Open through bank</asp:ListItem>
                   </asp:DropDownList>
              </td>

                  <td>
                  <label style="font-size:12px; font-weight:normal;width:100px">In favour of</label>
              </td>
               <td>
                     <telerik:RadTextBox ID="txtSupplyer" runat="server" Width="120px" ReadOnly="true"></telerik:RadTextBox>
               </td>
             </tr>
              <tr>
                    <td>
                   <label style="font-size:12px; font-weight:normal">CurrencyType</label>
              </td>
              <td>
                  <asp:RadioButtonList ID="radioCurrencyType" runat="server" RepeatDirection="Horizontal">
                      <asp:ListItem Text="SELLING" Value="SPOT" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="BUYING" Value="BUYING"></asp:ListItem>
                  </asp:RadioButtonList>
              </td>
                      <td>
                     <label style="font-size:12px; font-weight:normal ;width:100px" > parent Value Date</label>
              </td>
               <td>
                   <telerik:RadDatePicker ID="dateValueDate" runat="server" Enabled="false">
                     <asp:Calendar></asp:Calendar>
                   </telerik:RadDatePicker>
                 
               </td>
                   </tr>
             <tr>
               <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Parrent Currency Rate</label>
              </td>
              <td>
                     <telerik:RadNumericTextBox ID="numCurrencyRate" runat="server" Width="120"  AutoPostBack="true" ReadOnly="true"
                        MinValue="0"   
                        MaxValue ="100" >
                          <NumberFormat GroupSeparator="" DecimalDigits="4"  /> 
                     </telerik:RadNumericTextBox> 
              </td>
                               
              <td>
                   <label style="font-size:12px; font-weight:normal">L/C confirmed</label>
              </td>
              <td>
                  <asp:RadioButtonList ID="RadioLCConfirmed" runat="server" RepeatDirection="Horizontal" enabled="false" >
                      <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                      <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                  </asp:RadioButtonList>
              </td>
                   </tr>
             <tr>

                <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px" > LC Value </label>
                 </td>
                 <td>
                      <telerik:RadNumericTextBox ID="numLcValue" runat="server" Width="120" ReadOnly="true"
                        MinValue="0"   
                        MaxValue="999999999"  >
                             <NumberFormat GroupSeparator="" DecimalDigits="4" /> 
                      </telerik:RadNumericTextBox> 
                 </td>
                   <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px" > Tollerance (%) </label>
                 </td>
                 <td>
                      <telerik:RadNumericTextBox ID="numTollerance" runat="server" Width="120"   ReadOnly="true"
                        MinValue="0"   
                        MaxValue="100" Value="0">
                             <NumberFormat GroupSeparator="" DecimalDigits="2" /> 
                      </telerik:RadNumericTextBox> 
                 </td>
                             
          </tr>
                     
            <tr>

              <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Margin Paid (%)</label>
              </td>
               <td>
                   <telerik:RadNumericTextBox ID="numMarginpaid" runat="server" Width="120" ReadOnly="true"
                        MinValue="0"   
                        MaxValue ="100" Value="0">
                       <NumberFormat GroupSeparator="" DecimalDigits="2" /> 
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
                  <asp:Button ID="btnNext" runat="server" Text="Next"  OnClick="nextClick"/>
              </td>
          </tr>   
           </table>
         </div>
        </asp:View> 
              <asp:View ID="PageView2" runat="server" >
                  <div  style="margin-top:-260px">
                     <telerik:RadTicker ID="RadTicker2" runat="server" autoadvance="False" 
                backcolor="White" borderstyle="None" 
                style="text-align: center;position:initial; margin-top:0px" visible="False" width="100%">
                <Items>
                    <telerik:RadTickerItem ID="item2" runat="server" Font-Size="XX-Small" 
                       NavigateUrl="" 
                        Style="text-align: center; color : Green; font-weight:bold"></telerik:RadTickerItem>
                </Items>
            </telerik:RadTicker>
                      </div>
                   <div style="margin-left:30px">
                       <table>
          <tr>
              <td colspan="4">
                   <h3>LC Extention Detail</h3><br />
                   <hr />
              </td>
          </tr>
         
              <tr>
                       
                 <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px" >Extended LC Value </label>
                 </td>
                 <td>
                      <telerik:RadNumericTextBox ID="numExtentionLcValue" runat="server" Width="120" ReadOnly="true"
                        MinValue="0"   
                        MaxValue="999999999"  >
                             <NumberFormat GroupSeparator="" DecimalDigits="4" /> 
                      </telerik:RadNumericTextBox> 
                 </td>
                   <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Number of Periods</label>
              </td>
              <td>
                     <telerik:RadNumericTextBox ID="numNumberOfPeriod" runat="server" Width="120"  
                        MinValue="1"   
                        MaxValue ="100"  OnTextChanged="caluculatePayments_TextChanged" AutoPostBack="true" >
                          <NumberFormat GroupSeparator="" DecimalDigits="0"  /> 
                     </telerik:RadNumericTextBox> 
              </td>
                  </tr>
            <tr>
               <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Exchange Commission  </label>
              </td>
               <td>
                  <telerik:RadNumericTextBox ID="numExhCommission" runat="server" Width="120" ReadOnly="true"></telerik:RadNumericTextBox> 
               </td>  
                                
              <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Service Charge</label>
              </td>
               <td>
                   <telerik:RadNumericTextBox ID="numServiceCharge" runat="server" Width="120" ReadOnly="true"></telerik:RadNumericTextBox> 
               </td>
           </tr>
              <tr>      
         
                 <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Opening Commission </label>
              </td>
               <td>
                  <telerik:RadNumericTextBox ID="numOpenCommission" runat="server" Width="120" ReadOnly="true"></telerik:RadNumericTextBox> 
               </td>
                 <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Confirmation Commission </label>
              </td>
               <td>
                  <telerik:RadNumericTextBox ID="numConfCommission" runat="server" Width="120" ReadOnly="true"></telerik:RadNumericTextBox> 
               </td>
            
               </tr>
                 <tr>
                 <td>
                        <label style="font-size:12px; font-weight:normal ;width:100px">Extention Commission </label>
                 </td>
                     <td>
                        <telerik:RadNumericTextBox ID="numExtentionCommission" runat="server" Width="120" ReadOnly="true"></telerik:RadNumericTextBox> 
                     </td>
                  <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px">Swift</label>
                </td>
               <td>
                    <telerik:RadNumericTextBox ID="numSwift" runat="server" Width="120" ReadOnly="true"></telerik:RadNumericTextBox> 
               </td>
             </tr>
       
          <tr>
              <td colspan="4" style="text-align:right">
                   <asp:Button ID="Button4" runat="server" Text="Back" OnClick="backClick" />
                  <asp:Button ID="btnReport" runat="server" Visible="false" Text="View Ticket"  OnClick="bindOpeningReport" />
                  <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="saveLCOpeningExtention" Visible="false"/>
                  <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false"  OnClick="updateLCExtention"/>
                 
              </td>
          </tr>
      </table>
    </div>
    </asp:View> 
 </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
       <div style="margin-left:-10px; position:initial; margin-top:-140px">
                    <asp:Table ID="Table1" runat="server" >
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" >
                                <h3>Search Area</h3>
                            </asp:TableCell>                
                        </asp:TableRow>
                          <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" >
                                <h4>Search LC for new extention</h4>
                            </asp:TableCell>  
                                </asp:TableRow>
                         <asp:TableRow> 
                            <asp:TableCell>
                                <asp:Label ID="Label1" runat="server" Text="Enter LC No" Width="80px" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <telerik:RadTextBox ID="txtSearch" runat="server" Width="80px" ReadOnly="true"></telerik:RadTextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                           <asp:TableRow> 
                            <asp:TableCell>
                                <asp:Label ID="Label2" runat="server" Text="Select LC Condition" Width="80px" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <telerik:RadComboBox ID="comboLcStatus" runat="server"  Width="80px" Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="Opened" Text="Opened"/>
                                        <telerik:RadComboBoxItem Value="Advised" Text="Advised"/>
                                    </Items>
                                </telerik:RadComboBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                                 <asp:Button ID="Button1" runat="server" Text="reset" OnClick="clearLcdetail" />
                               <asp:Button ID="Button5" runat="server" Text="search" OnClick="searchLCBylcNumber" Visible="false" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                   </div>
                      <div style="margin-left:-10px; position:initial; margin-top:50px">
                    <asp:Table ID="Table2" runat="server" >
                      
                          <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" >
                                <h4>Search LC for update Extention</h4>
                            </asp:TableCell> 
                             </asp:TableRow>
                         <asp:TableRow> 
                            <asp:TableCell>
                                
                             <asp:Label ID="Label3" runat="server" Text="Enter LC No" Width="80px" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <telerik:RadTextBox ID="txtSearchLcInc" runat="server" Width="80px" ReadOnly="true"></telerik:RadTextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                          <asp:TableRow> 
                            <asp:TableCell>               
                             <asp:Label ID="Label4" runat="server" Text="Extention date" Width="80px" />
                            </asp:TableCell>
                            <asp:TableCell>
                                 <telerik:RadDatePicker ID="dateExtentionDate" runat="server" Enabled="false"  Width="80px">
                                 
                                 </telerik:RadDatePicker>
                 
                            </asp:TableCell>
                        </asp:TableRow>
                         <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                                 <asp:Button ID="Button2" runat="server" Text="reset" OnClick="clearLcdetailforUpdate" />
                               <asp:Button ID="Button3" runat="server" Text="search" OnClick="searchLCExtention" Visible="false"/>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                   </div>
     
</asp:Content>