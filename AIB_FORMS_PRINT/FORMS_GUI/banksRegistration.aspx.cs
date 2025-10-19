using AIB_FORMS_LOGIC;
using AIB_FORMS_OB;
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
    public partial class banksRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //combbankType.Attributes.Add("onchange", "javascript:return changeControls();");
            if (!IsPostBack)
            {
       
                op1.Visible = false;
                op2.Visible = false;
                this.initializeInvoiceDataTable();
                RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                RadGrid1.MasterTableView.GetColumn("Bank Id").Visible = false;
                RadGrid1.MasterTableView.GetColumn("status").Visible = false;
                foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                {
                    if (items["status"].Text == "INACTIVE")
                    {
                        int index = items.ItemIndex;
                        RadGrid1.MasterTableView.Items[index].Visible = false;
                    }
                }
                this.populateCurencyFirstTime();
           
                this.populateBannksByType();

                this.populateCountry();
                ComboCountry.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void searchBankDetail(object sender, EventArgs args) {
            if (comboRembSearch.SelectedValue == "0")
            {
                int bankId = Convert.ToInt32(comboBankNameSearch.SelectedValue);
                List<VW_CORRESPONDNCE_CURRENCY> rmList = new List<VW_CORRESPONDNCE_CURRENCY>();
                LC_Setting_Logic oplogic = new LC_Setting_Logic();
                rmList = oplogic.findRMBankdById(bankId);
                if (rmList.Count >= 1)
                {
                    op1.Visible = false;
                    op2.Visible = false;
                    rm1.Visible = true;
                    txtBankName.Text = rmList[0].correspondentName;
                    txtTelephoneNo.Text = rmList[0].telephoneNo;
                    txtFaxNo.Text = rmList[0].faxNumber;
                    DataTable bankCurrecy = (DataTable)ViewState["BankCurrency"];
                    bankCurrecy.Clear();
                    foreach (var item in rmList)
                    {
                        bankCurrecy.Rows.Add(item.currencyId, item.currencyName, item.aibAccountNumber, "Active", item.id);
                    }
                    Session["bankId"] = bankId.ToString();
                    RadGrid1.DataSource = bankCurrecy;
                    RadGrid1.DataBind();
                    btnOpUpdate.Visible = false;
                    btnOPSave.Visible = false;
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    comboRembSearch.Enabled = false;
                    combbankType.SelectedValue = comboRembSearch.SelectedValue; 
                    combbankType.Enabled = false;
                    btnNext.Visible = true;
                }
                else {
                    this.errorMessage("There must be at list one Rimbursing Bank.");
                }
            }
            else {
                int bankId = Convert.ToInt32(comboBankNameSearch.SelectedValue);
                List<TBL_OPEN_THROUGH_BANK> opList = new List<TBL_OPEN_THROUGH_BANK>();
                LC_Setting_Logic oplogic = new LC_Setting_Logic();
                opList=oplogic.findOpById(bankId);
                if (opList.Count == 1)
                {
                    op1.Visible = true;
                    op2.Visible= true;
                    rm1.Visible = false;
                    txtBankName.Text = opList[0].bankName;
                    txtBICCode.Text = opList[0].bankCode;
                    ComboCountry.SelectedValue = opList[0].countryId.ToString();
                    txtCity.Text = opList[0].city;
                    btnOpUpdate.Visible = true;
                    btnOPSave.Visible=false;
                    btnSave.Visible = false;
                    btnUpdate.Visible = false;
                    btnNext.Visible = false;
                    comboRembSearch.Enabled = false;
                    combbankType.SelectedValue = comboRembSearch.SelectedValue;
                    combbankType.Enabled = false;
                    btnNext.Visible = false;
                    Session["bankId"] = bankId.ToString();
                }
                else {

                    this.errorMessage("There Must be a single open through bank for selected bank name");
                }
            
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        private void populateCurencyFirstTime()
        {
            LcOpeningLogic openingLogic;
            List<TBL_CURRENCY> currency = new List<TBL_CURRENCY>();
            openingLogic = new LcOpeningLogic();
            // int corsId = Convert.ToInt32(combRembursing.SelectedValue);
            int corsId = 0;
            currency = openingLogic.findCurrencyByCorrespondenceID(corsId);
            comboCurrency.DataValueField = "currencyId";
            comboCurrency.DataTextField = "currencyName";
            comboCurrency.DataSource = currency;
            comboCurrency.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        private void populateCountry()
        {
            LcOpeningLogic openingLogic;
            List<TBL_COUNTRY> Country = new List<TBL_COUNTRY>();
            openingLogic = new LcOpeningLogic();
            // int corsId = Convert.ToInt32(combRembursing.SelectedValue);
           // int corsId = 0;
            Country = openingLogic.findAllCountry();
            ComboCountry.DataValueField = "Country_Id";
            ComboCountry.DataTextField = "Country_Name";
            ComboCountry.DataSource = Country;
            ComboCountry.DataBind();
        }
        /// <summary>
        /// 
        /// </summary>
        private void populateBannksByType() {
            LC_Setting_Logic openingLogic;

            if (comboRembSearch.SelectedValue == "0")
            {
               
                List<TBL_LC_CORRESPONDENT> banks = new List<TBL_LC_CORRESPONDENT>();
                openingLogic = new LC_Setting_Logic();
                banks = openingLogic.findsAllCorespondentBanks();
                comboBankNameSearch.DataSource = null;
                comboBankNameSearch.DataValueField = "id";
                comboBankNameSearch.DataTextField = "correspondentName";
                comboBankNameSearch.DataSource = banks;
                comboBankNameSearch.DataBind();
            }
            else {
                List<TBL_OPEN_THROUGH_BANK> banks = new List<TBL_OPEN_THROUGH_BANK>();
                openingLogic = new LC_Setting_Logic();
                banks = openingLogic.findsAllOpenThroughBanks();
                comboBankNameSearch.DataSource = null;
                comboBankNameSearch.DataValueField = "opId";
                comboBankNameSearch.DataTextField = "bankName";
                comboBankNameSearch.DataSource = banks;
                comboBankNameSearch.DataBind();
            }
        }

        protected void BanksByTypeIndexChanged(object sender, EventArgs e) {

            this.populateBannksByType();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void populateCorespCurrency(object sender, EventArgs e) {

            int selValue = Convert.ToInt32(combbankType.SelectedValue);
            if (selValue == 0)
            {
                op1.Visible = false;
                op2.Visible = false;
                rm1.Visible = true;
                btnNext.Visible = true;
                btnOPSave.Visible = false;
            }
            else {
                rm1.Visible = false;
                op1.Visible = true;
                op2.Visible = true;
                btnNext.Visible = false;
                btnOPSave.Visible = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
            // RadGrid1.DataSource = invoiceTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void invoice_SelectedIndexChanged(object sender, CommandEventArgs e)
        {
            this.resetInvoice();
          
            int itemIndex = Convert.ToInt32(e.CommandArgument);
            GridDataItem item = RadGrid1.Items[itemIndex];
            item.Selected = true;
            btnAdd.Visible = false;
            btnEdit.Visible = true;
            btnRemove.Visible = true;
            comboCurrency.SelectedValue = item["id"].Text.ToString();
            txtAibAccount.Text = item["AIB Account No"].Text;
            Session["Id"] = itemIndex.ToString();
            if (!item["status"].Text.Equals("NEW")) {
                comboCurrency.Enabled = false;
            }
            
          

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void addNewInvoiceClick(object sender, EventArgs args)
        {

            ComponentValidator valids = new ComponentValidator();
           // valids.addComponent(txtAibAccount, ComponentValidator.FULL_NAME, true);
       
            if (valids.isAllComponenetValid())
            {
                DataTable bankCurrecy = (DataTable)ViewState["BankCurrency"];
                if (Session["bankId"] != null)
                {
                    int bankId = Convert.ToInt32(Session["bankId"].ToString());
                    bankCurrecy.Rows.Add(comboCurrency.SelectedValue, comboCurrency.SelectedItem.Text, txtAibAccount.Text, "NEW", bankId);
                }
                else {
                    bankCurrecy.Rows.Add(comboCurrency.SelectedValue, comboCurrency.SelectedItem.Text, txtAibAccount.Text, "NEW", 0);
                }
                RadGrid1.DataSource = bankCurrecy;
                RadGrid1.DataBind();
                RadGrid1.MasterTableView.GetColumn("id").Visible = false;

                this.resetcurrency();
            }
        }

        private void resetcurrency()
        {
            comboCurrency.SelectedIndex = 0;
            txtAibAccount.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void EditInvoiceClick(object sender, EventArgs args)
        {
            ComponentValidator valids = new ComponentValidator();
           // valids.addComponent(txtAibAccount, ComponentValidator.TEXT_AND_NUMBER, true);
           

            if (valids.isAllComponenetValid())
            {
                DataTable BankCurrency = (DataTable)ViewState["BankCurrency"];
                string edu = Session["Id"].ToString();
                int eduId = Convert.ToInt32(edu);
                DataRow row = BankCurrency.Rows[eduId];
                GridDataItem item = RadGrid1.Items[eduId];
                row["Currency Code"] = comboCurrency.SelectedItem.Text.ToString();
                row["AIB Account No"] = txtAibAccount.Text;


                ViewState["BankCurrency"] = BankCurrency;
                RadGrid1.DataSource = BankCurrency;
                RadGrid1.DataBind();
                RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                RadGrid1.MasterTableView.GetColumn("Bank Id").Visible = false;
                RadGrid1.MasterTableView.GetColumn("status").Visible = false;
                foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                {
                    if (items["status"].Text == "INACTIVE")
                    {
                        int index = items.ItemIndex;
                        RadGrid1.MasterTableView.Items[index].Visible = false;
                    }
                }

                btnEdit.Visible = false;
                btnRemove.Visible = false;
                btnAdd.Visible = true;
                comboCurrency.Enabled = true;
                this.resetcurrency();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void removeNewInvoiceClick(object sender, EventArgs args)
        {

            DataTable BankCurrency = (DataTable)ViewState["BankCurrency"];

            int count = RadGrid1.MasterTableView.Items.Count;
            string edu = Session["Id"].ToString();
            int eduId = Convert.ToInt32(edu);
            GridDataItem item = RadGrid1.Items[eduId];
            string edcStatus = item["status"].Text;
            if (edcStatus == "NEW")
            {
                BankCurrency.Rows[eduId].Delete();
            }
            else
            {
                DataRow row = BankCurrency.Rows[eduId];
                row["status"] = "INACTIVE";

            }

            ViewState["BankCurrency"] = BankCurrency;
            RadGrid1.DataSource = ViewState["BankCurrency"];
            RadGrid1.DataBind();
            this.resetInvoice();
            RadGrid1.MasterTableView.GetColumn("id").Visible = false;
            RadGrid1.MasterTableView.GetColumn("Bank Id").Visible = false;
            RadGrid1.MasterTableView.GetColumn("status").Visible = false;
            foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
            {
                if (items["status"].Text == "INACTIVE")
                {
                    int index = items.ItemIndex;
                    RadGrid1.MasterTableView.Items[index].Visible = false;
                }
            }

            btnEdit.Visible = false;
            btnRemove.Visible = false;
            btnAdd.Visible = true;
            comboCurrency.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void nextClick(object sender, EventArgs args)
        {
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtBankName, ComponentValidator.FULL_NAME, true);
           // valids.addComponent(txtBICCode, ComponentValidator.TEXT_AND_NUMBER, true);
            //valids.addComponent(txtCity, ComponentValidator.TEXT_A_TO_Z_ONLY, true);
            if (valids.isAllComponenetValid())
            {
                MultiPage1.SetActiveView(PageView2);
                MultiPage1.ActiveViewIndex = 1;
                btnBack.Visible = true;
                btnSave.Visible = true;
                comboRembSearch.SelectedValue = "0";
                btnReset.Visible = false;

                //rightDiv.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void backClick(object sender, EventArgs args)
        {
            MultiPage1.SetActiveView(PageView1);
            MultiPage1.ActiveViewIndex = 0;
            Table1.Visible = true;
            btnReset.Visible = true;
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void saveOpenthroughBank(object sender, EventArgs args)
        {
            LC_Setting_Logic setLogic = new LC_Setting_Logic();
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtBankName, ComponentValidator.FULL_NAME, true);
            valids.addComponent(txtBICCode, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(txtCity, ComponentValidator.TEXT_A_TO_Z_ONLY, false);
            if (valids.isAllComponenetValid()) {
                bool result = false;
                if(!(setLogic.openthroughBankExists(txtBankName.Text))){
                result = setLogic.addOpenThroughBank(txtBankName.Text, txtBICCode.Text, Convert.ToInt32(ComboCountry.SelectedValue), txtCity.Text);
                if (result) { 
                   
                    this.postiveMessage("Open through bank Saved sucessfully.");
                }
                else{
                   this.errorMessage("Open through bank not Saved sucessfully.please contact Adminstrator");
                 }
                }
                else{
                  this.errorMessage("OpenThrough Bank  already exists");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void updateOpenthroughBank(object sender, EventArgs args)
        {
            LC_Setting_Logic setLogic = new LC_Setting_Logic();
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtBankName, ComponentValidator.TEXT_A_TO_Z_ONLY, true);
            valids.addComponent(txtBICCode, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(txtCity, ComponentValidator.TEXT_A_TO_Z_ONLY, false);
            if (valids.isAllComponenetValid())
            {
                bool result = false;
                if (Session["BankID"] != null)
                {
                    int bankId = Convert.ToInt32(Session["BankID"].ToString());
                    if (!(setLogic.openthroughBankExists(txtBankName.Text, bankId)))
                    {


                        result = setLogic.editOpenThroughBank(txtBankName.Text, txtBICCode.Text, Convert.ToInt32(ComboCountry.SelectedValue), txtCity.Text, bankId);
                        if (result)
                        {

                            this.postiveMessage("Open through bank updated sucessfully.");
                        }
                        else
                        {
                            this.errorMessage("Open through bank not updated sucessfully.please contact Adminstrator");
                        }
                    }

                    else
                    {
                        this.errorMessage("This openThrough Bank already exists.");
                    }
                }
                else {
                    this.errorMessage("At list one openThrough bank must be selected.");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void saveRimbursingBank(object sender, EventArgs args) {
            LC_Setting_Logic setLogic = new LC_Setting_Logic();
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtBankName, ComponentValidator.TEXT_A_TO_Z_ONLY, true);
          //  valids.addComponent(txtTelephoneNo, ComponentValidator.FULL_NAME, false);
           // valids.addComponent(txtFaxNo, ComponentValidator.FULL_NAME, false);
            if (valids.isAllComponenetValid())
            {
                DataTable BankCurrency = (DataTable)ViewState["BankCurrency"];
                bool result = false;
               
                   
                    if (!(setLogic.rembursingBankExists(txtBankName.Text)))
                    {
                        if (BankCurrency.Rows.Count > 0)
                        {

                            result = setLogic.addRimbersingBank(txtBankName.Text, txtTelephoneNo.Text, txtFaxNo.Text, BankCurrency);
                            if (result)
                            {

                                this.postiveMessage("Rimbersing Bank Saved sucessfully.");
                            }
                            else
                            {
                                this.errorMessage("Rimbersing Bank not Saved sucessfully.please contact Adminstrator");
                            }
                        }
                        else {
                            this.errorMessage("There must be at list one supported currency");
                        }
                    }

                    else
                    {
                        this.errorMessage("This Rimbersing Bank is already exists");
                    }
                

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void updateRimbursingBank(object sender, EventArgs args)
        {
            LC_Setting_Logic setLogic = new LC_Setting_Logic();
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtBankName, ComponentValidator.TEXT_A_TO_Z_ONLY, true);
          //  valids.addComponent(txtTelephoneNo, ComponentValidator.INTEGER_NUMBER, false);
          //  valids.addComponent(txtFaxNo, ComponentValidator.TEXT_AND_NUMBER, false);
            if (valids.isAllComponenetValid())
            {
                if (Session["BankID"] != null)
                {
                    int bankId = Convert.ToInt32(Session["BankID"].ToString());
                    DataTable BankCurrency = (DataTable)ViewState["BankCurrency"];
                    bool result = false;


                    if (!(setLogic.rembursingBankExists(txtBankName.Text, bankId)))
                    {
                        if (BankCurrency.Rows.Count > 0)
                        {

                            result = setLogic.editRimbersingBank(txtBankName.Text, txtTelephoneNo.Text, txtFaxNo.Text, BankCurrency,bankId);
                            if (result)
                            {

                                this.postiveMessage("Rimbersing Bank bank Updated sucessfully.");
                            }
                            else
                            {
                                this.errorMessage("Rimbersing Bank not Updated sucessfully.please contact Adminstrator");
                            }
                        }
                        else
                        {
                            this.errorMessage("There must be at list one supported currency");
                        }
                    }

                    else
                    {
                        this.errorMessage("This Rimbersing Bank already exists");
                    }


                }
               
            }

        }
        /// <summary>updateOpenthroughBank
        /// 
        /// </summary>
        private void initializeInvoiceDataTable()
        {


            DataTable bankCurrency = new DataTable();
            bankCurrency.Columns.Add("id", typeof(Int32));
            bankCurrency.Columns.Add("Currency Code", typeof(String));
            bankCurrency.Columns.Add("AIB Account No", typeof(String));
            bankCurrency.Columns.Add("status", typeof(String));
            bankCurrency.Columns.Add("Bank Id", typeof(Int32));


            ViewState["BankCurrency"] = bankCurrency;
            RadGrid1.DataSource = bankCurrency;
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
        /// 
        /// </summary>
        private void resetInvoice()
        {
            txtAibAccount.Text = "";
            comboCurrency.SelectedIndex = 0;
            txtBankName.Text = "";
            txtTelephoneNo.Text = "";
            txtFaxNo.Text = "";
            txtCity.Text = "";
            ComboCountry.SelectedIndex = 0;
            combbankType.SelectedValue = "0";
            comboRembSearch.SelectedIndex = 0;
            this.populateBannksByType();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnOPSave.Visible = false;
            btnOpUpdate.Visible = false;
            btnNext.Visible = true;
            btnBack.Visible = false;
            DataTable bankCurrecy = (DataTable)ViewState["BankCurrency"];
            bankCurrecy.Clear();
            RadGrid1.DataSource = bankCurrecy;
            RadGrid1.DataBind();
            op1.Visible = false;
            op2.Visible = false;
            rm1.Visible = true;
            comboRembSearch.Enabled = true;
            comboRembSearch.Enabled = true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void reset(object sender, EventArgs args) {
            this.resetInvoice();
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