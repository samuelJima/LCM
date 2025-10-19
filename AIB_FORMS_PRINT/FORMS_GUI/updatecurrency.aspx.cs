using AIB_FORMS_LOGIC;
using AIB_FORMS_VALIDATION;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace AIB_FORMS_PRINT.FORMS_GUI
{
    public partial class updatecurrency : System.Web.UI.Page
    {
        LC_Setting_Logic settingLogic;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.initializeCurrencyDataTable();
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            settingLogic = new LC_Setting_Logic();
            DataTable curuncyList = (DataTable)ViewState["CurrencyTable"];
            DateTime valuedate = DateTime.Now.Date;
             settingLogic.findCurrencyByValueDate(curuncyList, valuedate);
             RadGrid1.DataSource = curuncyList;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void searchCurrencyByValuedate(object sender, EventArgs args) {
            try
            {
                settingLogic = new LC_Setting_Logic();
                DataTable curuncyList = (DataTable)ViewState["CurrencyTable"];
                curuncyList.Clear();
                DateTime valueDate = (DateTime)dateValueDate.SelectedDate;
                settingLogic.findCurrencyByValueDate(curuncyList, valueDate.Date);
                RadGrid1.DataSource = curuncyList;
                RadGrid1.DataBind();
                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
            catch {

                this.errorMessage("Connection Can not be established with data base please contact the administrator.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void SaveNewCurrency(object sender, EventArgs args) {
            
            settingLogic = new LC_Setting_Logic();
            DataTable curuncyList = (DataTable)ViewState["CurrencyTable"];
            bool result = false;
            DateTime valueDate=DateTime.Now.Date;
            if (curuncyList.Rows.Count > 0)
            {
                result = settingLogic.addCurrencyRate(valueDate, curuncyList);
                if (result)
                {
                    this.postiveMessage("Currency rate sucessfuly saved");
                }
                else
                {
                    this.postiveMessage("Currency rate not sucessfuly saved.Please contact the Adminstrator");
                }
            }
            else {
                this.errorMessage("There must be atlist one currency");
             }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void updateExistingCurrency(object sender, EventArgs args) {

            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(dateValueDate, ComponentValidator.GC_DATE, true);
            if (valids.isAllComponenetValid())
            {
                settingLogic = new LC_Setting_Logic();
                DataTable curuncyList = (DataTable)ViewState["CurrencyTable"];
                bool result = false;
                DateTime valueDate = dateValueDate.SelectedDate.Value.Date;
                if (curuncyList.Rows.Count > 0)
                {
                    result = settingLogic.changeCurrencyRate(valueDate.Date, curuncyList);
                    if(result){
                      this.postiveMessage("Currency rate sucessfuly updated");
                    }
                    else{
                     this.postiveMessage("Currency rate not sucessfuly updated.Please contact the Adminstrator");
                    }
                }
                else
                {
                    this.errorMessage("There must be atlist one currency");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void errorMessage(string message)
        {

            this.MessageDivComfirmation = true;
            this.MessageRadTickerComf = true;
            this.MessageConfirmationBg = Color.Red;
            this.MessageLabel = message;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void postiveMessage(string message)
        {

            this.MessageDivComfirmation = true;
            this.MessageRadTickerComf = true;
            this.MessageConfirmationBg = Color.LightGreen;
            this.MessageLabel = message;

        }
        /// <summary>
        /// get and set the Visible property of RadTicker1
        /// </summary>
        public bool MessageDivComfirmation
        {
            get { return RadTicker1.Visible; }
            set { RadTicker1.Visible = value; }
        }
        /// <summary>
        /// get and set the autostart property of RadTicker1
        /// </summary>
        public bool MessageRadTickerComf
        {
            get { return RadTicker1.AutoStart; }
            set { RadTicker1.AutoStart = value; }
        }
        /// <summary>
        /// get and set the Text property of item1
        /// </summary>
        public string MessageLabel
        {
            get { return item1.Text; }
            set { item1.Text = value; }
        }
        /// <summary>
        /// get and set the BackColor property of RadTicker1
        /// </summary>
        public System.Drawing.Color MessageConfirmationBg
        {
            get { return RadTicker1.BackColor; }
            set { RadTicker1.BackColor = value; }
        }
    }
}