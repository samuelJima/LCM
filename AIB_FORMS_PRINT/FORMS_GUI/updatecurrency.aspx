<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeBehind="updatecurrency.aspx.cs" Inherits="AIB_FORMS_PRINT.FORMS_GUI.updatecurrency" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MiddleContent" runat="server">
      <br />
            <telerik:RadTicker ID="RadTicker1" runat="server" autoadvance="False" 
                backcolor="White" borderstyle="None" 
                style="text-align: center;position:absolute; margin-top:-180px" visible="False" width="53%">
                <Items>
                    <telerik:RadTickerItem ID="item1" runat="server" Font-Size="XX-Small" 
                       NavigateUrl="" 
                        Style="text-align: center; color : Green; font-weight:bold"></telerik:RadTickerItem>
                </Items>
            </telerik:RadTicker>
    <div style="position:initial; margin-top:-170px;margin-left:30px">
        <h2>Update Exchange Rate</h2> <br />
        <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource" Skin="Outlook"  GridLines="None">
            <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
            </ClientSettings>

            <MasterTableView DataSourceID="" DataKeyNames="">
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                   <Columns>
                                    
                                  </Columns>                        
           </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div style="text-align:right">
           <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="SaveNewCurrency" />
           <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick="updateExistingCurrency" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
       <div style="margin-right:-40px;position:initial; margin-top:-320px">
                    <asp:Table ID="Table1" runat="server"  >
                        <asp:TableRow >
                            <asp:TableCell ColumnSpan="2">
                                <h3>Search Area</h3>
                            </asp:TableCell>                
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label1" runat="server" Text="Value Date" Width="80px" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <telerik:RadDatePicker ID="dateValueDate" runat="server">
                                      <asp:Calendar>

                                      </asp:Calendar>
                                </telerik:RadDatePicker>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                               <asp:Button ID="Button5" runat="server" Text="search" OnClick="searchCurrencyByValuedate" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                   </div>
</asp:Content>
