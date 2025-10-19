<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="banksRegistration.aspx.cs" Inherits="AIB_FORMS_PRINT.FORMS_GUI.banksRegistration" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
      <telerik:RadTicker ID="RadTicker1" runat="server" autoadvance="False" 
                backcolor="White" borderstyle="None" 
                style="text-align: center;position:absolute; margin-top:-330px" visible="False" width="53%">
                <Items>
                    <telerik:RadTickerItem ID="item1" runat="server" Font-Size="XX-Small" 
                       NavigateUrl="" 
                        Style="text-align: center; color : Green; font-weight:bold"></telerik:RadTickerItem>
                </Items>
            </telerik:RadTicker>
     <asp:MultiView  ID="MultiPage1" runat="server" ActiveViewIndex="0"  > 

       <asp:View ID="PageView1" runat="server"    >
           
    <div id="regContet"  style="margin-left:70px;position:initial; margin-top:-330px">
        <table>
            <tr>
                <td colspan="4">
                       <h2>Banks Registration</h2> <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label style="font-size:12px ; font-weight:normal;width:100px" ID="Label1" runat="server" Text="Bank Name"></asp:Label></td>
                <td>
                   <telerik:RadTextBox ID ="txtBankName" runat="server" Width="120px"></telerik:RadTextBox>
                   
                </td>
                <td>
                     <asp:Label style="font-size:12px ; font-weight:normal;width:100px" ID="Label2" runat="server" Text="Bank Type"></asp:Label>
                </td>
                <td>
                     <asp:DropDownList  ID="combbankType" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="populateCorespCurrency"  > 
                          <asp:ListItem Value="0">Reimbursing</asp:ListItem>
                          <asp:ListItem Value="1">Correspondent</asp:ListItem>
                     </asp:DropDownList>
                </td>
            </tr>
            <tr id="rm1" runat="server">
               
                
                  <td> <asp:Label style="font-size:12px ; font-weight:normal;width:100px" ID="Label3" runat="server" Text="Telephone No"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtTelephoneNo" runat="server" Width="120px"></telerik:RadTextBox>
                </td>
                 <td>
                     <asp:Label style="font-size:12px ; font-weight:normal;width:100px" ID="Label9" runat="server" Text="Fax No"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtFaxNo" runat="server" Width="120px"></telerik:RadTextBox>
                    
                </td>
            </tr>
            
              <tr id="op1"  runat="server">
                <td>
                      <asp:Label style="font-size:12px ; font-weight:normal;width:100px" ID="Label8" runat="server" Text="BIC Code"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtBICCode" runat="server" Width="120px"></telerik:RadTextBox>
                    
                </td>
                <td> <asp:Label style="font-size:12px ; font-weight:normal;width:100px" ID="Label5" runat="server" Text="Country"></asp:Label>
                </td>
                <td>
                       <asp:DropDownList  ID="ComboCountry" runat="server" Width="120px" AutoPostBack="true"> 
                   </asp:DropDownList>
                </td>
            </tr>
                <tr id="op2" runat="server">
                
                <td>
                     <asp:Label style="font-size:12px ; font-weight:normal;width:100px" ID="Label6" runat="server" Text="City"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtCity" runat="server"  Width="120px"></telerik:RadTextBox>
                    
                </td>
                    <td></td>
                    <td></td>
            </tr>
          
            
              <tr>
                <td colspan="4"style="text-align:right">
                     <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="nextClick" visible="true"/>
                     <asp:Button ID="btnOPSave" runat="server" Text="Save" visible="false" OnClick="saveOpenthroughBank"/>
                     <asp:Button ID="btnOpUpdate" runat="server" Text="Update"  visible="false" OnClick="updateOpenthroughBank"/>

                  
                </td>
            </tr>
          
        </table>
    </div  >
           </asp:View>
           <asp:View ID="PageView2" runat="server"   >

               <div style="margin-left:70px;position:initial; margin-top:-330px">
                   <table>
                         <tr>
                <td colspan="4">
                       <h2>Supported Currency</h2> <br />
                </td>
            </tr>
                 <tr>
                     <td>
                         <asp:Label ID="Label10" runat="server" Text="Currency"></asp:Label>
                     </td>
                     <td>
                          <asp:DropDownList  ID="comboCurrency" runat="server" Width="120px" AutoPostBack="true"> 
                   </asp:DropDownList>
                     </td>
                      <td>
                          <asp:Label ID="Label11" runat="server" Text="AIB Account"></asp:Label>
                     </td>
                     <td>
                         <telerik:RadTextBox ID="txtAibAccount" runat="server" Width="120px"></telerik:RadTextBox>
                         
                     </td>
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
                      <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource" Skin="Outlook">
                          <MasterTableView DataSourceID="" DataKeyNames="">
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
                <td colspan="4"style="text-align:right">
                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="backClick"  Visible="false"/>
                  <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="saveRimbursingBank" />
                  <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick="updateRimbursingBank"  />
                </td>
            </tr>
                   </table>

                   
               </div>
           </asp:View>

     </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
       <div id="rightDiv" style="margin-right:-40px ;position:initial; margin-top:-290px" runat="server">
                    <asp:Table ID="Table1" runat="server"  >
                        <asp:TableRow >
                            <asp:TableCell ColumnSpan="2">
                                <h3>Search Area</h3>
                            </asp:TableCell>                
                        </asp:TableRow>
                       
                        
                          <asp:TableRow>
                              <asp:TableCell>
                       <asp:Label style="font-size:12px ; font-weight:normal;width:100px" ID="Label4" runat="server" Text="Bank Type"></asp:Label>
                       </asp:TableCell>
                              <asp:TableCell>
                
                        <asp:DropDownList  ID="comboRembSearch" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="BanksByTypeIndexChanged"  > 
                          <asp:ListItem Value="0" Selected="True">Reimbursing</asp:ListItem>
                          <asp:ListItem Value="1">Correspondent</asp:ListItem>
                      </asp:DropDownList>
                          </asp:TableCell>
                              </asp:TableRow>
                          <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label7" runat="server" Text="Bank Name" Width="80px" />
                            </asp:TableCell>
                            <asp:TableCell>
                                  <asp:DropDownList  ID="comboBankNameSearch" runat="server" Width="120px" AutoPostBack="true"   > 
                       
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                                 <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="reset"  />
                               <asp:Button ID="Button5" runat="server" Text="search" OnClick="searchBankDetail"  />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                   </div>
</asp:Content>
