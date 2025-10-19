<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="lcFormEight.aspx.cs" Inherits="AIB_FORMS_PRINT.FORMS_GUI.lcFormEight" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">
    <div>
        <telerik:RadWindow ID="RadWindow1" runat="server" Width="250px" Modal="true" VisibleTitlebar="false" Behaviors="Default" InitialBehaviors="None">
            <ContentTemplate>
                <updatepanel ID="Updatepanel1" runat="server" UpdateMode="Conditional">
                 <ContentTemplate>  
                <table>
                     <tr>
                        <td colspan="2"> <asp:Label ID="lblpoperoor" runat="server" Text="" ForeColor="Red" ></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2"><p>There is excess amount. please add permit number for the excess amount?</p></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Permit Year"></asp:Label>
                         </td>
                        <td>
                         <asp:RadioButtonList ID="radioExcessPermityear" runat="server" RepeatDirection="Horizontal"  >
                           <asp:ListItem Text="This Year" Value="1" Selected="True"></asp:ListItem>
                         <asp:ListItem Text="Last Year" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                        </td>
                         </tr>
                         <tr>
                        <td>
                              <asp:Label ID="Label12" runat="server" Text="Permit Code"></asp:Label>
                          </td>
                        <td>
                            <telerik:RadTextBox ID="txtExcessAmountPermit" runat="server" OnTextChanged="generatePermitNumber_TextChanged" AutoPostBack="true"></telerik:RadTextBox>
                         </td>
                    </tr>
                     <tr>
                        <td>
                              <asp:Label ID="Label13" runat="server" Text="Permit Number"></asp:Label>
                          </td>
                        <td>
                            <telerik:RadTextBox ID="txtExcessPermitNumber" runat="server" OnTextChanged="generatePermitNumber_TextChanged" AutoPostBack="true"></telerik:RadTextBox>
                         </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="saveExcessAmountPermit"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="closeRadWindow" />

                        </td>
                    </tr>
                </table>
                 </ContentTemplate>
                </updatepanel>
            </ContentTemplate>
    </telerik:RadWindow>
    </div>
    <div>
          <telerik:RadTicker ID="RadTicker1" runat="server" autoadvance="False" 
                backcolor="White" borderstyle="None" 
                style="text-align: center;position:absolute; margin-top:-110px" visible="False" width="53%">
                <Items>
                    <telerik:RadTickerItem ID="item1" runat="server" Font-Size="XX-Small" 
                       NavigateUrl="" 
                        Style="text-align: center; color : Green; font-weight:bold"></telerik:RadTickerItem>
                </Items>
            </telerik:RadTicker>
    </div>
    <asp:MultiView  ID="MultiPage1" runat="server" ActiveViewIndex="0"  > 
       <asp:View ID="PageView1" runat="server"    >
          
   <div id="outerDiv"style="margin-left:30px;position:initial; margin-top:-110px">
        <table>
            <tr>
                <td style="text-align:left; font-size:smaller">
                    <h2>L/C-ADVICE-SIGHT</h2> <br />
                   <%-- <label>1.DEBIT TICKET</label> --%>
                </td>
                <td colspan="4" style="text-align:center">
                     <label></label> <br />
                    <label></label> 
                </td>
            </tr>
        
        </table>
    </div>
    <div style="margin-left:30px">
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
                  <label style="font-size:12px ; font-weight:normal;width:100px">Branch Code</label>
              </td>
               <td>
                   <telerik:RadTextBox ID="txtBranchCode" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
               </td>
          </tr>
          <tr>
            
               <td>
                  <label style="font-size:12px ; font-weight:normal;width:100px" >L/C Number</label>
              </td>
               <td>
                  <telerik:RadTextBox ID="txtLCNumber" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
               </td>
           
              <td>
                  <label style="font-size:12px ; font-weight:normal;width:100px">Permit Number</label>
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
                     <label style="font-size:12px ; font-weight:normal;width:100px" >Value Date</label>
              </td>
               <td>
                   <telerik:RadDatePicker ID="dateValueDate" runat="server" Enabled="false"></telerik:RadDatePicker>
                 
               </td>
            </tr>

             <tr>
                <td>
                  <label style="font-size:12px ; font-weight:normal;width:100px">In favour of</label>
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
                  <label style="font-size:12px ; font-weight:normal;width:100px">Opened Through</label>
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
                  <label style="font-size:12px ; font-weight:normal;width:100px">For Account Of</label>
              </td>
               <td>
                 <telerik:RadTextBox ID="txtCustomerName" runat="server" Width="120px" ReadOnly="true"></telerik:RadTextBox>
               </td>
        
           
               <td>
                  <label style="font-size:12px ; font-weight:normal;width:100px">Currency</label>
              </td>
                <td>
                <telerik:RadTextBox ID="txtCurrency" runat="server" ReadOnly="true" BackColor="Gray" Width="120px"></telerik:RadTextBox>
               
               </td>
              </tr>
             <tr>
              <td>
                  <label style="font-size:12px ; font-weight:normal;width:100px">Currency Rate</label>
              </td>
              <td>
                     <telerik:RadNumericTextBox ID="numCurrencyRate" runat="server" Width="120" ReadOnly="true">
                          <NumberFormat GroupSeparator="" DecimalDigits="4"  />
                     </telerik:RadNumericTextBox> 
              </td>               
         
                    <td>
                   <label style="font-size:12px; font-weight:normal">CurrencyType</label>
              </td>
              <td>
                  <asp:RadioButtonList ID="radioCurrencyType" runat="server" RepeatDirection="Horizontal" Enabled="false" >
                      <asp:ListItem Text="SELLING" Value="SPOT" Selected="True"></asp:ListItem>
                      <asp:ListItem Text="BUYING" Value="BUYING"></asp:ListItem>
                  </asp:RadioButtonList>
              </td>
                    </tr> 
             <tr>
                     <td>
                  <label style="font-size:12px; font-weight:normal ;width:100px" > Tollerance (%) </label>
                 </td>
                 <td>
                      <telerik:RadNumericTextBox ID="numTollerance" runat="server" Width="120" ReadOnly="true"
                        MinValue="0"   
                        MaxValue="100" Value="0">
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
          <table style="margin-left:20px;position:initial; margin-top:0px" >
              <tr>
                  <td colspan="4">
                      <h2> Invoice Detail</h2>
                  </td>
              </tr>
              <tr>
                  <td>
                      <asp:Label ID="Label2" runat="server" Text="Reference No" style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                      <telerik:RadTextBox ID="txtReferenceNumber" runat="server"></telerik:RadTextBox>
                  </td>
                  <td>
                      <asp:Label ID="Label3" runat="server" Text="Value Date"  style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                     <telerik:RadDatePicker ID="dateInvoiceDate" runat="server" AutoPostBack="true" OnSelectedDateChanged="populateCurrencybyValueDate"></telerik:RadDatePicker>
                  </td>
              </tr>
               <tr>
                   <td>
                      <asp:Label ID="Label5" runat="server" Text="Invoice Rate"  style="font-size:12px ; font-weight:normal;width:100px">
                            
                      </asp:Label>
                  </td>
                  <td>
                       <telerik:RadNumericTextBox ID="numInvoiceRate" runat="server" ReadOnly="true" AutoPostBack="true">
                           <NumberFormat GroupSeparator="" DecimalDigits="4" /> 
                       </telerik:RadNumericTextBox>
                  </td>
                   <td> 
                     <label  style="font-size:12px ; font-weight:normal;width:100px">Shipment Date</label>
                   </td>
                   <td>   
                     <telerik:RadDatePicker ID="dateSheepmentDate" runat="server" AutoPostBack="true" OnSelectedDateChanged="sheepmentDateIndexChanged" ></telerik:RadDatePicker>
                   </td>
                  
              </tr>
              <tr>
                   <td>
                  <label  style="font-size:12px ; font-weight:normal;width:100px">Document Recived From</label>
              </td>
               <td>

                <telerik:RadTextBox ID="txtOpThrough" runat="server" Width="120px" ></telerik:RadTextBox>
                 
               </td>
                 
                   <td>
                       <asp:Label ID="Label14" runat="server" Text="Number Of SWIFT"  style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                       <telerik:RadNumericTextBox ID="numNumberOfSwift" runat="server" >
                         <NumberFormat DecimalDigits="0" />
                       </telerik:RadNumericTextBox>
                  </td>   
              </tr>
              <tr>
                     <td>
                      <asp:Label ID="Label4" runat="server" Text="Invoice Value" style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                       <telerik:RadNumericTextBox ID="numInvoiceValue" runat="server" OnTextChanged="caluculatePayments_TextChanged" AutoPostBack="true"></telerik:RadNumericTextBox>
                  </td>
                  <td>
                       <asp:Label ID="Label6" runat="server" Text="Bill Amount"  style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                       <telerik:RadNumericTextBox ID="numBillAmount" runat="server" ReadOnly="true"></telerik:RadNumericTextBox>
                  </td>
                  
              </tr>
               <tr>
                   
                    <td>
                        <asp:Label ID="lblDedcripancyCheck" runat="server" Text="Descripancy Exists"  style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                        <asp:RadioButtonList ID="RadioDescripancyChek" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="descripancySelectedEndexChanged" AutoPostBack="true" >
                         <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                         <asp:ListItem Text="No" Value="No"></asp:ListItem>
                       </asp:RadioButtonList>
                  </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Descripancy"  style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                       <telerik:RadNumericTextBox ID="numDescripancy" runat="server" ReadOnly="true"></telerik:RadNumericTextBox>
                  </td>
              </tr>
               <tr>
                    <td>
                       <asp:Label ID="Label8" runat="server" Text="Total"  style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                       <telerik:RadNumericTextBox ID="numTotal" runat="server" ReadOnly="true"></telerik:RadNumericTextBox>
                  </td>

                   <td>
                        <asp:Label ID="Label9" runat="server" Text="Excess Amount Paid"  style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                       <telerik:RadNumericTextBox ID="numExcessAmount" runat="server" ReadOnly="true"></telerik:RadNumericTextBox>
                  </td> 
              </tr>
              <tr>
                    <td>
                       <asp:Label ID="Label10" runat="server" Text="Utlized Amount"  style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                       <telerik:RadNumericTextBox ID="numUtlizedAmount" runat="server" ReadOnly="true"></telerik:RadNumericTextBox>
                  </td>
                  <td>
                        <asp:Label ID="Label15" runat="server" Text="Expiration Amount"  style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>

                  <td>
                       <telerik:RadNumericTextBox ID="numExpirationAmount" runat="server" ReadOnly="true"></telerik:RadNumericTextBox>
                  </td>
                      
              </tr>
              <tr>
                  <td>
                     <asp:Label ID="lblInvoiceStatus" runat="server" Text="Is Last Invoice"  style="font-size:12px ; font-weight:normal;width:100px"></asp:Label>
                  </td>
                  <td>
                       <asp:RadioButtonList ID="RadioSequenceStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="sequenceStausIndexChanged" >
                         <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                         <asp:ListItem Text="No" Value="No"></asp:ListItem>
                       </asp:RadioButtonList>
                  </td>
                  <td></td>
                   <td></td>
              </tr>
              <tr>
                  <td colspan="4" style="text-align:right">
                      <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="addNewInvoiceClick" />
                      <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="false" OnClick="EditInvoiceClick" />
                      <asp:Button ID="btnRemove" runat="server" Text="Remove" Visible="false" OnClick="removeNewInvoiceClick"/>
                  </td>
              </tr>
              <tr>
                  <td colspan="4">
                      <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource" Skin="Outlook" Width="650px" Height="200px" >
                             <ClientSettings>
                               <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                               </ClientSettings>
                          <MasterTableView DataSourceID="" DataKeyNames="" >
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                   <Columns>
                                  <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" >
                                            <ItemTemplate >
                                           <asp:linkButton ID="Button9" runat="server" Text="Select" CommandArgument='<%#Container.ItemIndex%>'
                                            OnCommand="invoice_SelectedIndexChanged" />
                                            </ItemTemplate>
                                          </telerik:GridTemplateColumn>      
                                  </Columns>
                           
                          </MasterTableView>
                      </telerik:RadGrid>
                  </td>
              </tr>
               <tr>
                  <td colspan="4" style="text-align:right">
                      <asp:Button ID="Button4" runat="server" Text="Back" OnClick="backClick" />
                      <asp:Button ID="Button6" runat="server" Text="Update" Visible="true" OnClick="saveLAdviceSight" />
                       <asp:Button ID="Button1" runat="server" Text="View Excess Tiket" Visible="false" OnClick="bindExcessReport"/>
                      <asp:Button ID="Button7" runat="server" Text="View Tiket" Visible="true" OnClick="bindOpeningReport"/>
                  </td>
              </tr>
          </table>
          </asp:View>

 </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
       
    <div style="margin-left:-40px;position:initial; margin-top:-310px"" >
                    <asp:Table ID="Table1" runat="server"  >
                        <asp:TableRow >
                            <asp:TableCell ColumnSpan="2">
                                <h3>Search Area</h3>
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
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                                 <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="resetLCAdviceSite" Visible="true"/>
                               <asp:Button ID="btnSearch" runat="server" Text="search" OnClick="searchLCBylcNumber" visible="false"/>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                   </div>

</asp:Content>
