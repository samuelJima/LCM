<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AIB_FORMS_PRINT._Default" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MiddleContent">
  
    <div style="position:initial; margin-top:-280px;margin-left:70px">
        <br /><br />
      <table>
          <tr>
              
              <td>
                  <telerik:RadChart ID="RadChart1" runat="server" >
                      <ChartTitle  TextBlock-Text="Monthly LC Status"/>
                      <Series>
                          <telerik:ChartSeries Name="Adviced LC">
                              <Items>
                                  <telerik:ChartSeriesItem YValue="15" Name="Item 1"></telerik:ChartSeriesItem>
                              </Items>
                          </telerik:ChartSeries>
                          <telerik:ChartSeries Name="Setteled LC">
                              <Items>
                                  <telerik:ChartSeriesItem YValue="10" Name="Item 1"></telerik:ChartSeriesItem>
                              </Items>
                          </telerik:ChartSeries>
                          <telerik:ChartSeries Name="Cancelled LC">
                              <Items>
                                  <telerik:ChartSeriesItem YValue="5" Name="Item 1"></telerik:ChartSeriesItem>
                              </Items>
                          </telerik:ChartSeries>
                          <telerik:ChartSeries Name="Openend LC">
                              <Items>
                                  <telerik:ChartSeriesItem YValue="20" Name="Item 1"></telerik:ChartSeriesItem>
                              </Items>
                          </telerik:ChartSeries>
                          <telerik:ChartSeries Name="Removed"></telerik:ChartSeries>
                      </Series>
                  </telerik:RadChart>
                  
                       
              </td>
              <td>

              </td>
          </tr>
      </table>
    </div>
    
</asp:Content>
<asp:Content  runat="server" ID="RightContent" ContentPlaceHolderID="RightContent">
    <div style="margin-left:25px;position:initial; margin-top:-280px">
        <table>
            <tr>
                <td>
                      <div>
                          <h3>Todays Currency</h3>
                          <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource" Skin="Outlook"  GridLines="None" Height="300px" >
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
                    
                </td>
            </tr>
        </table>
                 
     </div>
</asp:Content>
