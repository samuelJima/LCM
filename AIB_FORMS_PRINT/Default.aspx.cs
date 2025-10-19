using AIB_FORMS_LOGIC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace AIB_FORMS_PRINT
{
    public partial class _Default : Page
    {
        LC_Setting_Logic settingLogic;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.initializeCurrencyDataTable();
                settingLogic = new LC_Setting_Logic();
                DataTable curuncyList = (DataTable)ViewState["CurrencyTable"];
                DateTime valuedate = DateTime.Now.Date.AddDays(-1);
                settingLogic.findCurrencyByValueDate(curuncyList, valuedate);
                RadGrid1.DataSource = curuncyList;
                RadGrid1.DataBind();
                RadGrid1.MasterTableView.GetColumn("Currency Rate").HeaderStyle.Width = 70;
                RadGrid1.MasterTableView.GetColumn("From Currency").HeaderStyle.Width = 70;
                RadGrid1.MasterTableView.GetColumn("To Currency").HeaderStyle.Width = 70;
                RadGrid1.MasterTableView.GetColumn("Exchange Type").HeaderStyle.Width = 70;
                RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                RadGrid1.MasterTableView.GetColumn("Value Date").Visible = false;
                
            }
           
        }

        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            settingLogic = new LC_Setting_Logic();
            DataTable curuncyList = (DataTable)ViewState["CurrencyTable"];
            DateTime valuedate = DateTime.Now.Date.AddDays(-1);
            settingLogic.findCurrencyByValueDate(curuncyList, valuedate);
            RadGrid1.DataSource = curuncyList;
        }

        private void initializeCurrencyDataTable()
        {
            DataTable invoiceTable = new DataTable();
            invoiceTable.Columns.Add("id", typeof(Int32));
            invoiceTable.Columns.Add("From Currency", typeof(String));
            invoiceTable.Columns.Add("To Currency", typeof(String));
            invoiceTable.Columns.Add("Exchange Type", typeof(String));
            invoiceTable.Columns.Add("Currency Rate", typeof(Double));
            invoiceTable.Columns.Add("Value Date", typeof(DateTime));

            ViewState["CurrencyTable"] = invoiceTable;
            RadGrid1.DataSource = invoiceTable;
            RadGrid1.DataBind();

        }
    }
}