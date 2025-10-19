using AIB_FORMS_LOGIC;
using AIB_FORMS_OB;
using AIB_FORMS_VALIDATION;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace AIB_FORMS_PRINT.FORMS_GUI
{
    public partial class lcFormEight : System.Web.UI.Page
    {
        LC_Advice_logic adviceLogic;
        LcOpeningLogic openingLogic;
        protected void Page_Load(object sender, EventArgs e)
        {
            openingLogic = new LcOpeningLogic();
            if (!IsPostBack)
            {
                List<TBL_Branch> branch = new List<TBL_Branch>();
               
                if (Session["Branch"] != null)
                {

                    int BranchCode = Convert.ToInt32(Session["Branch"].ToString());
                    branch = openingLogic.findBranchByBranchCode(BranchCode);
                    if (branch.Count == 1)
                    {
                       
                        Session["BranchAbrivation"] = branch[0].branchAbrivation.ToString();
                      
                    }
                }
                this.initializeInvoiceDataTable();
                RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                RadGrid1.MasterTableView.GetColumn("status").Visible = false;
                RadGrid1.MasterTableView.GetColumn("openingBankId").Visible = false;
                RadGrid1.MasterTableView.GetColumn("lcNumber").Visible = false;
                RadGrid1.MasterTableView.GetColumn("sequenceStatus").Visible = false;
                lblCurrentDate.Text = DateTime.Now.ToShortDateString();
                foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                {
                    if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled" || items["status"].Text == "Advised")
                    {
                        int index = items.ItemIndex;
                        RadGrid1.MasterTableView.Items[index].Visible = false;

                    }
                }

            }
            else {
                 List<TBL_Branch> branch = new List<TBL_Branch>();
                 if (Session["Branch"] != null)
                 {

                     int BranchCode = Convert.ToInt32(Session["Branch"].ToString());
                     branch = openingLogic.findBranchByBranchCode(BranchCode);
                     if (branch.Count == 1)
                     {

                         Session["BranchAbrivation"] = branch[0].branchAbrivation.ToString();

                     }
                 }
                RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                RadGrid1.MasterTableView.GetColumn("status").Visible = false;
                RadGrid1.MasterTableView.GetColumn("openingBankId").Visible = false;
                RadGrid1.MasterTableView.GetColumn("lcNumber").Visible = false;
                RadGrid1.MasterTableView.GetColumn("sequenceStatus").Visible = false;
                lblCurrentDate.Text = DateTime.Now.ToShortDateString();
                foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                {
                    if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled" || items["status"].Text == "Advised")
                    {
                        int index = items.ItemIndex;
                        RadGrid1.MasterTableView.Items[index].Visible = false;

                    }
                }
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
        /// <param name="args"></param>
        protected void generatePermitNumber_TextChanged(object sender, EventArgs args)
        {

            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtExcessAmountPermit, ComponentValidator.INTEGER_NUMBER, true);
            if (valids.isAllComponenetValid())
            {
                openingLogic = new LcOpeningLogic();
                string branchAbrivation = Session["BranchAbrivation"].ToString();
                string permitCode = txtExcessAmountPermit.Text;
                int permitYear = Convert.ToInt32(radioExcessPermityear.SelectedValue);
                if (permitCode.Length == 5)
                {

                    int year;
                    if (permitYear == 1)
                    {
                        year = DateTime.Now.Year;
                    }
                    else
                    {
                        year = DateTime.Now.Year - 1;
                    }
                    if (openingLogic.checkPermitCode(permitCode, year) && openingLogic.checkExcessPermitCode(permitCode, year))
                    {

                        txtExcessPermitNumber.Text = "AIB/" + branchAbrivation + "/01-" + permitCode + "/" + year.ToString().Substring(2, 2);
                        string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        lblpoperoor.Text = "permit Code already exist for given year";
                       // this.errorMessage("permit Code already exist for given year");
                        string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                    }
                }
                else
                {
                    lblpoperoor.Text = "permit Code must be five digit";
                    //this.errorMessage("permit Code must be five digit");
                    string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void populateCurrencybyValueDate(object sender, EventArgs args)
        {
            PaymentCalculater calc = new PaymentCalculater();
            openingLogic = new LcOpeningLogic();
            string currencytype = radioCurrencyType.SelectedValue;
            string currencyCode = txtCurrency.Text;
            DateTime valueDate = Convert.ToDateTime(dateInvoiceDate.SelectedDate);
            List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
            currencyRate = openingLogic.findCurrencyByValueDat(currencytype, currencyCode, valueDate);
            if (currencyRate.Count == 1)
            {
                numInvoiceRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                if (!numInvoiceValue.Text.Equals(string.Empty))
                {
                    numBillAmount.Text = (calc.calculculateBillAmount(Convert.ToDouble(numInvoiceValue.Text), Convert.ToDouble(numInvoiceRate.Text))*numInvoiceRate.Value).ToString();
                    numDescripancy.Text = (calc.calculculateDiscripancy(RadioDescripancyChek.SelectedValue.ToString(), currencyCode, currencytype, Convert.ToDouble(numInvoiceRate.Text), Convert.ToDateTime(dateInvoiceDate.SelectedDate))*numInvoiceRate.Value).ToString();
                    numTotal.Text = ((calc.calculculateBillAmount(Convert.ToDouble(numInvoiceValue.Text), Convert.ToDouble(numInvoiceRate.Text)) - (calc.calculculateDiscripancy(RadioDescripancyChek.SelectedValue.ToString(), currencyCode, currencytype, Convert.ToDouble(numInvoiceRate.Text), Convert.ToDateTime(dateInvoiceDate.SelectedDate))))*numInvoiceRate.Value).ToString();
                    DateTime duedate = calc.calculateExpirationDueDate(txtLCNumber.Text, Convert.ToDateTime(dateOpeningDate.SelectedDate), Convert.ToInt32(txtOpeningPeriod.Text));
                    int invoiceId = 0;
                   // double newInvoice = 0;
                    if (ViewState["InvoiceId"] != null)
                    {
                        //
                        string status = ViewState["sequenceStatus"].ToString();
                        invoiceId = Convert.ToInt32(Session["InvoiceId"].ToString());
                        DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                        this.tasksForSelectedInvoices(invoiceId, status, duedate, invoiceTable);
                    }
                    else
                    {
                        this.tasksForNewInvoices(duedate);
                    }                             
                 

                   // numExcessAmount.Text = ((newInvoice - (numLcValue.Value + numTollerance.Value)) * numInvoiceRate.Value).ToString();
                    numUtlizedAmount.Text = (calc.calculculateUtilizedAmmountAmount(Convert.ToDouble(numInvoiceValue.Text), Convert.ToDouble(numCurrencyRate.Text))*numCurrencyRate.Value).ToString();
                }
            }
            else
            {
                this.clearbyValueDate();
                this.errorMessage("There must be only one currency Rate.please contact the administrator.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void clearbyValueDate(){

            numInvoiceValue.Text = "";
            numInvoiceRate.Text = "";
            numBillAmount.Text = "";
            numExcessAmount.Text = "";
            numTotal.Text = "";
            numUtlizedAmount.Text = "";
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void caluculatePayments_TextChanged(Object sender, EventArgs args)
        {

            PaymentCalculater calc = new PaymentCalculater();
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(numInvoiceValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numInvoiceRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numCurrencyRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(dateInvoiceDate, ComponentValidator.GC_DATE, true);
            valids.addComponent(dateSheepmentDate, ComponentValidator.GC_DATE, true);

            if (valids.isAllComponenetValid())
            {
                string curencyType = radioCurrencyType.SelectedValue;
                numBillAmount.Text = (calc.calculculateBillAmount(Convert.ToDouble(numInvoiceValue.Text),Convert.ToDouble(numInvoiceRate.Text))* numInvoiceRate.Value).ToString();
                string currencyCode = txtCurrency.Text;
                numDescripancy.Text = (calc.calculculateDiscripancy(RadioDescripancyChek.SelectedValue.ToString(), currencyCode,curencyType, Convert.ToDouble(numInvoiceRate.Text), Convert.ToDateTime(dateInvoiceDate.SelectedDate))* numInvoiceRate.Value).ToString();
                numTotal.Text = ((calc.calculculateBillAmount(Convert.ToDouble(numInvoiceValue.Text), Convert.ToDouble(numInvoiceRate.Text)) - (calc.calculculateDiscripancy(RadioDescripancyChek.SelectedValue.ToString(), currencyCode, curencyType, Convert.ToDouble(numInvoiceRate.Text), Convert.ToDateTime(dateInvoiceDate.SelectedDate)))) * numInvoiceRate.Value).ToString();
                DateTime duedate = calc.calculateExpirationDueDate(txtLCNumber.Text, Convert.ToDateTime(dateOpeningDate.SelectedDate), Convert.ToInt32(txtOpeningPeriod.Text));              
                int invoiceId = 0;              
                //double newInvoice = 0;
                if (ViewState["InvoiceId"] != null)
                {
                    //
                    string status = ViewState["sequenceStatus"].ToString();
                    invoiceId = Convert.ToInt32(Session["InvoiceId"].ToString());
                    DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                    this.tasksForSelectedInvoices(invoiceId, status, duedate, invoiceTable);
                }
                else
                {
                    this.tasksForNewInvoices(duedate);
                }                             
                numUtlizedAmount.Text = (calc.calculculateUtilizedAmmountAmount(Convert.ToDouble(numInvoiceValue.Text) , Convert.ToDouble(numCurrencyRate.Text))* numCurrencyRate.Value).ToString();               
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void searchLCBylcNumber(object sender, EventArgs args)
        {
              ComponentValidator valids = new ComponentValidator();
               
               valids.addComponent(txtSearch, ComponentValidator.FULL_NAME, true);
               if (valids.isAllComponenetValid())
               {
                   adviceLogic = new LC_Advice_logic();
                   PaymentCalculater calc = new PaymentCalculater();
                   List<VW_LC_WITH_ADVICE_SIGHT> lcOpend = new List<VW_LC_WITH_ADVICE_SIGHT>();
                   List<VW_DETAIL_LC> newL = new List<VW_DETAIL_LC>();
                   string branchAbrivation = Session["BranchAbrivation"].ToString();

                   string lcNumber = txtSearch.Text;
                   string lcStart = lcNumber.Substring(0, 3);
                   if (lcStart.Equals(branchAbrivation))
                   {
                       lcOpend = adviceLogic.searchLcAdviceByLC_Number(lcNumber);
                       double totalInvoice = 0;
                       if (lcOpend.Count >= 1)
                       {

                           bool result = false;
                           DateTime dudate = calc.calculateExpirationDueDate(lcOpend[0].lcNumber.ToString(), Convert.ToDateTime(lcOpend[0].lcOpeningDate), Convert.ToInt32(lcOpend[0].openingPeriods)).Date;
                           DateTime currentDate = DateTime.Now.Date;
                           if (DateTime.Compare(dudate, currentDate) < 0)
                           {
                               result = openingLogic.changeLCExpirationStatus(txtLCNumber.Text.ToString());
                           }
                           else
                           {
                               result = true;
                           }
                           if (result)
                           {
                               Session["OpeningDate"] = lcOpend[0].lcOpeningDate.ToString();
                               txtLCNumber.Text = lcOpend[0].lcNumber;
                               Session["lcNumber"] = lcOpend[0].lcNumber;
                               txtBranchCode.Text = lcOpend[0].branchCode.ToString();
                               txtBranchName.Text = lcOpend[0].branchName;
                               txtCustomerName.Text = lcOpend[0].customerName;
                               txtPermitNUmber.Text = "AIB/" + lcOpend[0].branchAbrivation + "/01-" + lcOpend[0].permitCode + "/" + lcOpend[0].permitYear.ToString().Substring(2, 2);

                               txtSupplyer.Text = lcOpend[0].supplyerName;
                               txtAccountNO.Text = lcOpend[0].customerAccount;
                               numLcValue.Value = lcOpend[0].lcValue;
                               radioCurrencyType.SelectedValue = lcOpend[0].currencyType;
                               numTollerance.Value = lcOpend[0].tollerance;
                               txtOpeningPeriod.Text = lcOpend[0].openingPeriods.ToString();
                               numUserLimitaition.Value = lcOpend[0].userExpirationDates;
                               this.populateRembursingBank();
                               this.populateOpenedThrough();
                               combOpThrough.SelectedValue = lcOpend[0].openThroughId.ToString();
                               combRembursing.SelectedValue = lcOpend[0].corespondenceID.ToString();
                               //this.populateCurencyFirstTime();
                               txtCurrency.Text = lcOpend[0].openingCurrency;
                               dateValueDate.SelectedDate = lcOpend[0].valueDate;
                               dateOpeningDate.SelectedDate = lcOpend[0].lcOpeningDate;
                               numCurrencyRate.Value = Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate);
                               totalInvoice = Convert.ToDouble(lcOpend[0].totalInvoiceValue);
                               Session["TotalInvoice"] = totalInvoice.ToString();
                               //binding invoices for a given Lc number
                               if (radioCurrencyType.SelectedValue == "BUYING")
                               {
                                   numInvoiceRate.Value = Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate);
                                   dateInvoiceDate.SelectedDate = lcOpend[0].valueDate;
                                   dateInvoiceDate.Enabled = false;

                               }

                               DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                               foreach (var Item in lcOpend)
                               {

                                   invoiceTable.Rows.Add(Item.id, Item.referenceNo, Item.invoiceDate, Item.invoiceValue, Item.invoiceRate, Item.openThroughBank, Item.invOpenThroughId, Item.status, Item.descripancyStatus, Item.lcNumber, Item.numberOfSwift, Item.shipmentDate, Item.invoiceSequence, Item.sequenceStatus);
                               }

                               btnSearch.Visible = false;

                               RadGrid1.DataSource = invoiceTable;
                               RadGrid1.DataBind();
                           }
                           else
                           {
                               this.errorMessage("LC Expiration status can not be changed,please contact administrators");
                           }

                       }
                       else if (lcOpend.Count == 0)
                       {

                           newL = adviceLogic.searchLcByLCNumber(lcNumber);

                           if (newL.Count == 1)
                           {
                               bool result = false;
                               DateTime dudate = calc.calculateExpirationDueDate(lcOpend[0].lcNumber.ToString(), Convert.ToDateTime(lcOpend[0].lcOpeningDate), Convert.ToInt32(lcOpend[0].openingPeriods)).Date;
                               DateTime currentDate = DateTime.Now.Date;
                               if (DateTime.Compare(dudate, currentDate) < 0)
                               {
                                   result = openingLogic.changeLCExpirationStatus(txtLCNumber.Text.ToString());
                               }
                               if (result)
                               {
                                   txtLCNumber.Text = newL[0].lcNumber;
                                   Session["lcNumber"] = newL[0].lcNumber;
                                   txtBranchCode.Text = newL[0].branchCode.ToString();
                                   txtBranchName.Text = newL[0].branchName;
                                   txtCustomerName.Text = newL[0].customerName;
                                   txtPermitNUmber.Text = "AIB/" + newL[0].branchAbrivation + "/01-" + newL[0].permitCode + "/" + newL[0].permitYear.ToString().Substring(2, 2);

                                   txtSupplyer.Text = newL[0].supplyerName;
                                   txtAccountNO.Text = newL[0].customerAccount;
                                   numLcValue.Value = newL[0].lcValue;
                                   radioCurrencyType.SelectedValue = newL[0].currencyType;
                                   numTollerance.Value = newL[0].tollerance;
                                   txtOpeningPeriod.Text = newL[0].openingPeriods.ToString();
                                   numUserLimitaition.Value = newL[0].userExpirationDates;
                                   this.populateRembursingBank();
                                   this.populateOpenedThrough();
                                   combOpThrough.SelectedValue = newL[0].openThroughId.ToString();
                                   combRembursing.SelectedValue = newL[0].corespondenceID.ToString();
                                   this.populateCurencyFirstTime();
                                   txtCurrency.Text = newL[0].openingCurrency;
                                   dateValueDate.SelectedDate = newL[0].valueDate;
                                   dateOpeningDate.SelectedDate = newL[0].lcOpeningDate;
                                   numCurrencyRate.Value = Convert.ToDouble(newL[0].lcOpeningCurrencyRate);

                                   Session["TotalInvoice"] = totalInvoice.ToString();

                                   if (radioCurrencyType.SelectedValue == "BUYING")
                                   {
                                       numInvoiceRate.Value = Convert.ToDouble(newL[0].lcOpeningCurrencyRate);
                                       dateInvoiceDate.SelectedDate = newL[0].valueDate;
                                       dateInvoiceDate.Enabled = false;

                                   }
                                   btnSearch.Visible = false;
                                   DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                                   invoiceTable.Clear();

                                   RadGrid1.DataSource = invoiceTable;
                                   RadGrid1.DataBind();
                               }
                               else
                               {
                                   this.errorMessage("LC Expiration status can not be changed,please contact administrators");
                               }
                           }
                           else
                           {
                               this.errorMessage("No LC found for a give LC Number");
                           }

                       }
                       else
                       {
                           this.errorMessage("The letter LC Advice Sighet can not find");
                       }
                   }
                   else
                   {
                       this.errorMessage("The letter of credit does not belong to this branch.");

                   }
               }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void saveLAdviceSight(object sender , EventArgs args) {
            Button1.Visible = false;
            adviceLogic = new LC_Advice_logic();
            string lcNumber = txtLCNumber.Text;
            string lcStatus = "Adviced";
            bool result = false;
            if (Session["TotalInvoice"] != null)
            {
                if (Session["ExcessAmount"] == null)
                {
                    double totalInvoice = Convert.ToDouble(Session["TotalInvoice"]);
                    DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                    result = adviceLogic.addAdviceSight(lcNumber, lcStatus, totalInvoice, invoiceTable);
                    if (result)
                    {
                        List<TBL_INVOICE> invoice = new List<TBL_INVOICE>();
                        invoice = adviceLogic.searchInvoiceByLCNumber(lcNumber);
                        if (invoice.Count > 0)
                        {
                            invoiceTable.Clear();
                            foreach (var item in invoice)
                            {

                                invoiceTable.Rows.Add(item.id, item.referenceNo, item.invoiceDate, item.invoiceValue, item.invoiceRate, item.openThroughBank, item.openThroughId, item.status, item.descripancyStatus, item.invoiceLCNumber,item.numberOfSwift,item.shipmentDate, item.invoiceSequence, item.sequenceStatus);
                            }
                            this.clearbyValueDate();
                            RadGrid1.DataSource = invoiceTable;
                            RadGrid1.DataBind();
                        }
                        this.postiveMessage("LC Advice Sight Saved sucessfuly");
                    }
                    else
                    {
                        this.errorMessage("LC Advice Sight Saved sucessfuly please contact the system administrator ");
                    }
                }
                else {
                   
                        double totalInvoice = Convert.ToDouble(Session["TotalInvoice"]);
                     
                        DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                        var rows = invoiceTable.Select("sequenceStatus ='Yes'");
                        if (rows.Count() == 1)
                        {
                            result = adviceLogic.addAdviceSight(lcNumber, lcStatus, totalInvoice, invoiceTable);
                            if (result)
                            {
                                bool resultNew = false;
                                if (Session["ExcessPermitYear"] != null && Session["ExcessPermitCode"] != null)
                                {
                                    resultNew = adviceLogic.addExcessAmount(Convert.ToDouble(Session["ExcessAmount"].ToString()), Convert.ToInt32(Session["ExcessPermitYear"].ToString()), Session["ExcessPermitCode"].ToString(), Convert.ToDateTime(rows[0]["Invoice Date"]), Convert.ToDouble(rows[0]["Invoice Rate"]), txtLCNumber.Text);
                                }
                                if (Session["ExcessPermitYear"] == null || Session["ExcessPermitCode"] == null)
                                {
                                    resultNew = adviceLogic.updateExcessAmount(Convert.ToDouble(Session["ExcessAmount"].ToString()), txtLCNumber.Text);
                                }
                                if (resultNew)
                                {
                                    List<TBL_INVOICE> invoice = new List<TBL_INVOICE>();
                                    invoice = adviceLogic.searchInvoiceByLCNumber(lcNumber);
                                    if (invoice.Count > 0)
                                    {
                                        invoiceTable.Clear();
                                        foreach (var item in invoice)
                                        {

                                           // invoiceTable.Rows.Add(item.id, item.referenceNo, item.invoiceDate, item.invoiceValue, item.invoiceRate, item.openThroughBank, item.openThroughId, item.status, item.descripancyStatus, item.invoiceLCNumber, item.invoiceSequence, item.numberOfSwift, item.sequenceStatus);
                                            invoiceTable.Rows.Add(item.id, item.referenceNo, item.invoiceDate, item.invoiceValue, item.invoiceRate, item.openThroughBank, item.openThroughId, item.status, item.descripancyStatus, item.invoiceLCNumber, item.numberOfSwift, item.shipmentDate, item.invoiceSequence, item.sequenceStatus);
                                        }

                                        this.clearbyValueDate();
                                        RadGrid1.DataSource = invoiceTable;
                                        RadGrid1.DataBind();
                                    }
                                    this.postiveMessage("LC Advice Sight Saved sucessfuly");

                                }
                                else
                                {

                                    this.errorMessage("Excess amount can not be saved. please contact Administrator");
                                }


                            }
                            else
                            {
                                this.errorMessage("LC Advice Sight Can not be Saved . please contact the system administrator ");
                            }
                        }
                        else {
                            this.errorMessage("LC Advice Sight with more than one last invoice Can not be Saved . please contact the system administrator ");
                        }
                   
                }
            }
            else {
                this.errorMessage("At list one invoice must be added");
            }
        
        }

        /// <summary>
        /// 
        /// </summary>
        private void populateOpenedThrough()
        {
            openingLogic = new LcOpeningLogic();
            List<TBL_OPEN_THROUGH_BANK> lcBanks = new List<TBL_OPEN_THROUGH_BANK>();
            lcBanks = openingLogic.findOpenThroughBanks();
            combOpThrough.DataValueField = "opId";
            combOpThrough.DataTextField = "bankName";
            combOpThrough.DataSource = lcBanks;
            combOpThrough.DataBind();
        }

      

        /// <summary>
        /// 
        /// </summary>
        private void populateRembursingBank()
        {
            openingLogic = new LcOpeningLogic();
            List<TBL_LC_CORRESPONDENT> lcBanks = new List<TBL_LC_CORRESPONDENT>();
            lcBanks = openingLogic.findCorespondentBanks();
            combRembursing.DataValueField = "id";
            combRembursing.DataTextField = "correspondentName";
            combRembursing.DataSource = lcBanks;
            combRembursing.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void populateCurencyFirstTime()
        {

            List<TBL_CURRENCY> currency = new List<TBL_CURRENCY>();
            openingLogic = new LcOpeningLogic();
            int corsId = Convert.ToInt32(combRembursing.SelectedValue);
            currency = openingLogic.findCurrencyByCorrespondenceID(corsId);
            //combCurrency.DataValueField = "currencyId";
            //combCurrency.DataTextField = "currencyName";
           // combCurrency.DataSource = currency;
            //combCurrency.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void nextClick(object sender, EventArgs args) {
            MultiPage1.SetActiveView(PageView2);
            MultiPage1.ActiveViewIndex = 1;
            btnReset.Visible = false;
            Master.UpdateMessageMethod = true;
          
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
            btnReset.Visible = true;
            Master.UpdateMessageMethod = true;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void addNewInvoiceClick(object sender , EventArgs args) {

             ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtReferenceNumber, ComponentValidator.FULL_NAME, true);
            valids.addComponent(dateInvoiceDate, ComponentValidator.GC_DATE, true);
            valids.addComponent(dateSheepmentDate, ComponentValidator.GC_DATE, true);
            valids.addComponent(numInvoiceValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numInvoiceRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numExcessAmount, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numNumberOfSwift, ComponentValidator.INTEGER_NUMBER, true);

            if (valids.isAllComponenetValid())
            {
                Button1.Visible = false;
                ViewState["InvoiceId"] = null;
                ViewState["sequenceStatus"] = null;
                if (numExcessAmount.Value == 0)
                {
                    Session["ExcessAmount"] = null;
                    int invoiceSequence = 0;
                    string sequenceStatus = "No";
                    DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                    if (invoiceTable.Rows.Count == 0)
                    {
                        invoiceSequence = 1;
                        //sequenceStatus = RadioSequenceStatus.SelectedValue;
                    }
                    else
                    {
                        var row = invoiceTable.Select("Sequence = MAX(Sequence)");
                        invoiceSequence = Convert.ToInt32(row[0]["Sequence"]) + 1;

                        var rows = invoiceTable.Select("sequenceStatus ='Yes'");
                        if (rows.Count() >= 1)
                        {
                            sequenceStatus = "Yes";
                        }
                    }


                    if (sequenceStatus == "No")
                    {

                        double totalInvoice = Convert.ToDouble(Session["TotalInvoice"]);
                        double lcvalue = Convert.ToDouble(numLcValue.Value);
                        string lcNumber = Session["lcNumber"].ToString();
                        double tolleranceValue = Convert.ToDouble((numTollerance.Value / 100) * numLcValue.Value);
                        if ((lcvalue + tolleranceValue) > totalInvoice)
                        {
                            if ((lcvalue + tolleranceValue) >= (totalInvoice + numInvoiceValue.Value))
                            {
                                sequenceStatus = RadioSequenceStatus.SelectedValue;
                                invoiceTable.Rows.Add(0, txtReferenceNumber.Text, dateInvoiceDate.SelectedDate, numInvoiceValue.Value, numInvoiceRate.Value, txtOpThrough.Text, 0, "NEW", RadioDescripancyChek.SelectedValue, lcNumber,Convert.ToInt32(numNumberOfSwift.Value),dateSheepmentDate.SelectedDate, invoiceSequence, RadioSequenceStatus.SelectedValue);
                                RadGrid1.DataSource = invoiceTable;
                                RadGrid1.DataBind();

                                RadGrid1.MasterTableView.GetColumn("id").Visible = false;

                                totalInvoice = Convert.ToDouble(totalInvoice + numInvoiceValue.Value);
                                Session["TotalInvoice"] = totalInvoice.ToString();
                                this.resetInvoice();
                                foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                                {
                                    if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled" || items["status"].Text == "Advised")
                                    {
                                        int index = items.ItemIndex;
                                        RadGrid1.MasterTableView.Items[index].Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                //Excess Amount Will be handled here 
                                this.errorMessage("There is excess amount to be handled.");
                            }
                        }
                        else
                        {
                            string errorMessage = "LC Value = " + lcvalue.ToString() + " must be greater than The total Invoice value = " + totalInvoice.ToString();
                            this.errorMessage(errorMessage);
                        }
                    }
                    else
                    {

                        this.errorMessage("The last invoice was added.You Can't add aditional invoice ");
                    }

                }
                else { 
                    int invoiceSequence = 0;
                    string sequenceStatus = "No";
                    DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                    if (invoiceTable.Rows.Count == 0)
                    {
                        invoiceSequence = 1;
                        Session["invoiceSequence"] = invoiceSequence.ToString();
                        //sequenceStatus = RadioSequenceStatus.SelectedValue;
                    }
                    else
                    {
                        var row = invoiceTable.Select("Sequence = MAX(Sequence)");
                        invoiceSequence = Convert.ToInt32(row[0]["Sequence"]) + 1;
                        Session["invoiceSequence"] = invoiceSequence.ToString();
                        var rows = invoiceTable.Select("sequenceStatus ='Yes'");
                        if (rows.Count() >= 1)
                        {
                            sequenceStatus = "Yes";
                        }
                    }


                    if (sequenceStatus == "No")
                    {
                       
                        if (RadioSequenceStatus.SelectedValue == "Yes")
                        {
                            Session["ExcessStatus"] = "New";
                            string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                        else
                        {
                            this.errorMessage("The Invoice Must be the last invoice.");
                        }
                    }
                    else {
                        this.errorMessage("The last invoice was added.You Can't add aditional invoice ");
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void EditInvoiceClick(object sender, EventArgs args)
        {   adviceLogic = new LC_Advice_logic();
             ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtReferenceNumber, ComponentValidator.FULL_NAME, true);
            valids.addComponent(dateInvoiceDate, ComponentValidator.GC_DATE, true);
            valids.addComponent(dateSheepmentDate, ComponentValidator.GC_DATE, true);
            valids.addComponent(numInvoiceValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numInvoiceRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numNumberOfSwift, ComponentValidator.INTEGER_NUMBER, true);

            if (valids.isAllComponenetValid())
            {
                Button1.Visible = false;
                Session["InvoiceId"] = Session["InvoiceId"].ToString();
                DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                double totalInvoice = Convert.ToDouble(Session["TotalInvoice"]);
                double invoiceValue = Convert.ToDouble(Session["CurrentInvoiceValue"]);
                double diff = Convert.ToDouble(numInvoiceValue.Value - invoiceValue);
                totalInvoice = totalInvoice + diff;
                double tolleranceValue = Convert.ToDouble((numTollerance.Value / 100) * numLcValue.Value);

                if (totalInvoice <= (numLcValue.Value + tolleranceValue))
                {
                    Session["ExcessAmount"] = null;
                    string edu = Session["Id"].ToString();
                    int eduId = Convert.ToInt32(edu);
                    DataRow row = invoiceTable.Rows[eduId];
                    GridDataItem item = RadGrid1.Items[eduId];
                    string status = "No";
                    if (RadioSequenceStatus.SelectedValue == "Yes")
                    {
                        var rows = invoiceTable.Select("sequenceStatus ='Yes'");
                        if (rows.Count() >= 1 && Convert.ToInt32(row["Sequence"]) != Convert.ToInt32(rows[0]["Sequence"]))
                        {
                            status = "Yes";
                        }

                        if (status == "No")
                        {
                            Session["TotalInvoice"] = totalInvoice.ToString();
                            row["Reference Number"] = txtReferenceNumber.Text.ToString();
                            row["Invoice Date"] = dateInvoiceDate.SelectedDate;
                            row["Invoice Value"] = numInvoiceValue.Value;
                            row["Invoice Rate"] = numInvoiceRate.Value;
                            row["Document Recived From"] = txtOpThrough.Text;
                            row["openingBankId"] = 0;
                            row["Descripancy Exists"] = RadioDescripancyChek.SelectedValue;
                            row["sequenceStatus"] = RadioSequenceStatus.SelectedValue;
                            row["Number Of Swift"] = numNumberOfSwift.Value.ToString();
                            row["Sheepment Date"] = dateSheepmentDate.SelectedDate;
                            ViewState["InvoiceTable"] = invoiceTable;
                            RadGrid1.DataSource = invoiceTable;
                            RadGrid1.DataBind();
                            RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                            this.resetInvoice();
                            btnEdit.Visible = false;
                            btnRemove.Visible = false;
                            btnAdd.Visible = true;
                            foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                            {
                                if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled" || items["status"].Text == "Advised")
                                {
                                    int index = items.ItemIndex;
                                    RadGrid1.MasterTableView.Items[index].Visible = false;
                                }
                            }
                        }
                        else
                        {
                            this.errorMessage("More than one invoice can't be last invoice.");
                        }

                    }
                    else
                    {
                        Session["TotalInvoice"] = totalInvoice.ToString();
                        row["Reference Number"] = txtReferenceNumber.Text.ToString();
                        row["Invoice Date"] = dateInvoiceDate.SelectedDate;
                        row["Invoice Value"] = numInvoiceValue.Value;
                        row["Invoice Rate"] = numInvoiceRate.Value;
                        row["Document Recived From"] = txtOpThrough.Text;
                        row["openingBankId"] = 0;
                        row["Descripancy Exists"] = RadioDescripancyChek.SelectedValue;
                        row["sequenceStatus"] = RadioSequenceStatus.SelectedValue;
                        row["Number Of Swift"] = numNumberOfSwift.Value.ToString();
                        row["Sheepment Date"] = dateSheepmentDate.SelectedDate;
                        ViewState["InvoiceTable"] = invoiceTable;
                        RadGrid1.DataSource = invoiceTable;
                        RadGrid1.DataBind();
                        RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                        this.resetInvoice();
                        btnEdit.Visible = false;
                        btnRemove.Visible = false;
                        btnAdd.Visible = true;
                        foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                        {
                            if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled" || items["status"].Text == "Advised")
                            {
                                int index = items.ItemIndex;
                                RadGrid1.MasterTableView.Items[index].Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    // Excess Amount to be handled 
                    bool excessResult = adviceLogic.CheckEcessAmountExistance(txtLCNumber.Text);
                    if (excessResult)
                    {
                       
                        string edu = Session["Id"].ToString();
                        int eduId = Convert.ToInt32(edu);
                        DataRow row = invoiceTable.Rows[eduId];
                        GridDataItem item = RadGrid1.Items[eduId];
                        string status = "No";
                        if (RadioSequenceStatus.SelectedValue == "Yes")
                        {
                            var rows = invoiceTable.Select("sequenceStatus ='Yes'");

                            if (rows.Count() >= 1 && Convert.ToInt32(row["Sequence"]) != Convert.ToInt32(rows[0]["Sequence"]))
                            {
                                status = "Yes";
                            }

                            if (status == "No")
                            {
                                Session["TotalInvoice"] = totalInvoice.ToString();
                                Session["ExcessAmount"] = (totalInvoice - (numLcValue.Value + tolleranceValue)).ToString();
                                row["Reference Number"] = txtReferenceNumber.Text.ToString();
                                row["Invoice Date"] = dateInvoiceDate.SelectedDate;
                                row["Invoice Value"] = numInvoiceValue.Value;
                                row["Invoice Rate"] = numInvoiceRate.Value;
                                row["Document Recived From"] = txtOpThrough.Text;
                                row["openingBankId"] = 0;
                                row["Descripancy Exists"] = RadioDescripancyChek.SelectedValue;
                                row["sequenceStatus"] = RadioSequenceStatus.SelectedValue;
                                row["Number Of Swift"] = numNumberOfSwift.Value.ToString();
                                row["Sheepment Date"] = dateSheepmentDate.SelectedDate;
                                ViewState["InvoiceTable"] = invoiceTable;
                                RadGrid1.DataSource = invoiceTable;
                                RadGrid1.DataBind();
                                RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                                this.resetInvoice();
                                btnEdit.Visible = false;
                                btnRemove.Visible = false;
                                btnAdd.Visible = true;
                                foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                                {
                                    if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled" || items["status"].Text == "Advised")
                                    {
                                        int index = items.ItemIndex;
                                        RadGrid1.MasterTableView.Items[index].Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                this.errorMessage("More than one invoice can't be last invoice.");
                            }

                        }
                        else
                        {
                             var rows = invoiceTable.Select("sequenceStatus ='Yes'");

                             if (rows.Count() == 1 && rows[0]["id"].ToString()!=Session["InvoiceId"].ToString())
                             {
                                 Session["TotalInvoice"] = totalInvoice.ToString();
                                 Session["ExcessAmount"] = (totalInvoice - (numLcValue.Value + tolleranceValue)).ToString();
                                 row["Reference Number"] = txtReferenceNumber.Text.ToString();
                                 row["Invoice Date"] = dateInvoiceDate.SelectedDate;
                                 row["Invoice Value"] = numInvoiceValue.Value;
                                 row["Invoice Rate"] = numInvoiceRate.Value;
                                 row["Document Recived From"] = txtOpThrough.Text;
                                 row["openingBankId"] = 0;
                                 row["Descripancy Exists"] = RadioDescripancyChek.SelectedValue;
                                 row["sequenceStatus"] = RadioSequenceStatus.SelectedValue;
                                 row["Number Of Swift"] = numNumberOfSwift.Value.ToString();
                                 row["Sheepment Date"] = dateSheepmentDate.SelectedDate;
                                 ViewState["InvoiceTable"] = invoiceTable;
                                 RadGrid1.DataSource = invoiceTable;
                                 RadGrid1.DataBind();
                                 RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                                 this.resetInvoice();
                                 btnEdit.Visible = false;
                                 btnRemove.Visible = false;
                                 btnAdd.Visible = true;
                                 foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                                 {
                                     if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled" || items["status"].Text == "Advised")
                                     {
                                         int index = items.ItemIndex;
                                         RadGrid1.MasterTableView.Items[index].Visible = false;
                                     }
                                 }
                             }
                             else
                             {
                                
                                 this.errorMessage("There must be at list one last invoice.");
                             }

                        }
                    }
                    else
                    {
                        Session["ExcessStatus"] = "Edit";
                        string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                }
               
            }
            ViewState["InvoiceId"] = null;
            ViewState["sequenceStatus"] = null;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void removeNewInvoiceClick(object sender, EventArgs args) {
            Button1.Visible = false;
            DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
            double tolleranceValue = Convert.ToDouble((numTollerance.Value / 100) * numLcValue.Value);
            int count = RadGrid1.MasterTableView.Items.Count;
             string edu = Session["Id"].ToString();
             int eduId = Convert.ToInt32(edu);
             GridDataItem item = RadGrid1.Items[eduId];
             string edcStatus = item["status"].Text;
             if (edcStatus == "NEW")
              {
                  double totalInvoice = Convert.ToDouble(Session["TotalInvoice"]);
                  totalInvoice = Convert.ToDouble(totalInvoice - numInvoiceValue.Value);
                  Session["TotalInvoice"] = totalInvoice.ToString();
                  if (totalInvoice > (numLcValue.Value + tolleranceValue))
                  {
                      Session["ExcessAmount"] = ((numLcValue.Value + tolleranceValue) - totalInvoice).ToString();
                  }
                  else {
                      Session["ExcessAmount"] = null;
                  }
                  invoiceTable.Rows[eduId].Delete();
              }
             else
             {
                 double totalInvoice = Convert.ToDouble(Session["TotalInvoice"]);
                 totalInvoice = Convert.ToDouble(totalInvoice - numInvoiceValue.Value);
                 Session["TotalInvoice"] = totalInvoice.ToString();
                 if (totalInvoice > (numLcValue.Value + tolleranceValue))
                 {
                     Session["ExcessAmount"] = ((numLcValue.Value + tolleranceValue) - totalInvoice).ToString();
                 }
                 else
                 {
                     Session["ExcessAmount"] = null;
                 }
                  DataRow row = invoiceTable.Rows[eduId];
                  row["status"] = "INACTIVE";
                  
              }

             ViewState["InvoiceTable"] = invoiceTable;
             RadGrid1.DataSource = ViewState["InvoiceTable"];
             RadGrid1.DataBind();
             foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
             {
                 if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled" || items["status"].Text == "Advised")
                 {
                     int index = items.ItemIndex;
                     RadGrid1.MasterTableView.Items[index].Visible = false;
                 }
             }
            btnEdit.Visible = false;
            btnRemove.Visible = false;
           btnAdd.Visible = true;
           this.resetInvoice();
           ViewState["InvoiceId"] = null;
           ViewState["sequenceStatus"] = null;
        }

        protected void invoice_SelectedIndexChanged(object sender, CommandEventArgs e){
            Button1.Visible = false;
            this.resetInvoice();
            PaymentCalculater calc = new PaymentCalculater();
            int itemIndex = Convert.ToInt32(e.CommandArgument);
            GridDataItem item = RadGrid1.Items[itemIndex];
            item.Selected = true;
            btnAdd.Visible = false;
            btnEdit.Visible = true;
            btnRemove.Visible = true;
            txtReferenceNumber.Text = item["Reference Number"].Text.ToString();
            dateInvoiceDate.SelectedDate = Convert.ToDateTime(item["Invoice Date"].Text);
            numInvoiceValue.Value = Convert.ToDouble(item["Invoice Value"].Text);
            numInvoiceRate.Value = Convert.ToDouble(item["Invoice Rate"].Text);
            txtOpThrough.Text = item["Document Recived From"].Text;
            RadioDescripancyChek.SelectedValue = item["Descripancy Exists"].Text;
            RadioSequenceStatus.SelectedValue = item["sequenceStatus"].Text;
            numNumberOfSwift.Value =Convert.ToInt32(item["Number Of Swift"].Text);
            dateSheepmentDate.SelectedDate= Convert.ToDateTime(item["Sheepment Date"].Text)  ;
            numBillAmount.Text = (calc.calculculateBillAmount(Convert.ToDouble(item["Invoice Value"].Text), Convert.ToDouble(item["Invoice Rate"].Text))*numInvoiceRate.Value).ToString();
            DateTime duedate = calc.calculateExpirationDueDate(txtLCNumber.Text, Convert.ToDateTime(dateOpeningDate.SelectedDate), Convert.ToInt32(txtOpeningPeriod.Text));
            string currencyCode = txtCurrency.Text;
            string curencyType = radioCurrencyType.SelectedValue;
            numDescripancy.Text = (calc.calculculateDiscripancy(item["Descripancy Exists"].Text, currencyCode, curencyType, Convert.ToDouble(item["Invoice Rate"].Text), Convert.ToDateTime(item["Invoice Date"].Text)) * numInvoiceRate.Value).ToString();
            numTotal.Text = ((calc.calculculateBillAmount(Convert.ToDouble(item["Invoice Value"].Text), Convert.ToDouble(item["Invoice Rate"].Text)) - (calc.calculculateDiscripancy(item["Descripancy Exists"].Text, currencyCode, curencyType, Convert.ToDouble(item["Invoice Rate"].Text), Convert.ToDateTime(item["Invoice Date"].Text))))*numInvoiceRate.Value).ToString();
           // if the selected Item is last invoice 
            if (item["sequenceStatus"].Text == "Yes")
            {
                double newInvoice = 0;
               
                //clculating the total invoice other than the last invoice 
                foreach (GridDataItem item1 in RadGrid1.MasterTableView.Items)
                {
                    if ((item1["status"].Text == "NEW" || item1["status"].Text == "ACTIVE" || item1["status"].Text == "Setteled" || item1["status"].Text == "Advised") && item1.ItemIndex != itemIndex)
                    {
                        newInvoice = newInvoice + Convert.ToDouble(item1["Invoice Value"].Text);
                    }
                }
                newInvoice = newInvoice + Convert.ToDouble(item["Invoice Value"].Text);

                if (newInvoice > (numLcValue.Value + numTollerance.Value))
                {
                    numExcessAmount.Text = ((newInvoice - (numLcValue.Value + numTollerance.Value)) * numInvoiceRate.Value).ToString();
                    Button1.Visible = true;
                }
                else {
                    numExcessAmount.Value = 0;
                }
               
                    if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), duedate) > 0)
                    {
                        if (Convert.ToDouble(numLcValue.Value) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value)) > 0)
                            {
                                double remainingLCValue = Convert.ToDouble(numLcValue.Value) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value));
                                TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)duedate;
                                int days = Convert.ToInt32(difference.TotalDays) - 1;
                                numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                                if (RadioDescripancyChek.SelectedValue == "No")
                                {
                                    string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                                    string currencyCode1 = txtCurrency.Text;
                                    string curencyType1 = radioCurrencyType.SelectedValue;
                                    DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                                    double rate = Convert.ToDouble(numInvoiceRate.Value);
                                    RadioDescripancyChek.SelectedValue = "Yes";
                                    numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode1, curencyType1, rate, valueDate) * numInvoiceRate.Value).ToString();
                                    numTotal.Value = numTotal.Value - numDescripancy.Value;
                                   
                                }
                            }
                            else {
                                numExpirationAmount.Value = 0;
                            }
                    }
                    else
                    {
                        numExpirationAmount.Value = 0;
                    }
               
                  
               
                //numExcessAmount.Text = (calc.calculculateExessAmount(Convert.ToDouble(numLcValue.Value), newInvoice, txtLCNumber.Text, Convert.ToInt32(item["id"].Text)) * numInvoiceRate.Value).ToString();
            }
            else {

                numExcessAmount.Value = 0;
                if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), duedate) > 0)
                {
                    double remainingLCValue = Convert.ToDouble(numInvoiceValue.Value);
                    TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)duedate;
                    int days = Convert.ToInt32(difference.TotalDays) - 1;
                    numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                    if (RadioDescripancyChek.SelectedValue == "No")
                    {
                        string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                        string currencyCode1 = txtCurrency.Text;
                        string curencyType1 = radioCurrencyType.SelectedValue;
                        DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                        double rate = Convert.ToDouble(numInvoiceRate.Value);
                        RadioDescripancyChek.SelectedValue = "Yes";
                        numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode1, curencyType1, rate, valueDate) * numInvoiceRate.Value).ToString();
                        numTotal.Value = numTotal.Value - numDescripancy.Value;

                    }
                }
                else
                {
                    numExpirationAmount.Value = 0;
                }
            }

            numUtlizedAmount.Text = (calc.calculculateUtilizedAmmountAmount(Convert.ToDouble(item["Invoice Value"].Text), Convert.ToDouble(item["Invoice Rate"].Text))*numCurrencyRate.Value).ToString();
            
            Session["Id"] = itemIndex.ToString();
            Session["InvoiceId"] = item["id"].Text;
            Session["CurrentInvoiceValue"] = numInvoiceValue.Value.ToString();
            Session["sequenceStatus"] = item["sequenceStatus"].Text;
            ViewState["InvoiceId"] = item["id"].Text;
            ViewState["sequenceStatus"] = item["sequenceStatus"].Text;
            ViewState["Sequence"] = item["Sequence"].Text;

            double invrate = calc.usdRate(radioCurrencyType.Text, Convert.ToDateTime(dateInvoiceDate.SelectedDate));
            if (invrate != -1)
            {
                Session["usRate"] = invrate.ToString();
            }
            else {
                Session["usRate"] = "0";
            }
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void descripancySelectedEndexChanged(object sender, EventArgs e) {
             ComponentValidator valids = new ComponentValidator();
           
            valids.addComponent(dateInvoiceDate, ComponentValidator.GC_DATE, true);
            valids.addComponent(numInvoiceRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numDescripancy, ComponentValidator.FLOAT_NUMBER, true);

            if (valids.isAllComponenetValid())
            {
                
                PaymentCalculater calc = new PaymentCalculater();
                string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                string currencyCode = txtCurrency.Text;
                string curencyType = radioCurrencyType.SelectedValue;
                DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                double rate = Convert.ToDouble(numInvoiceRate.Value);
             
                if (RadioDescripancyChek.SelectedValue == "No")
                {
                    numTotal.Value = numTotal.Value + numDescripancy.Value;
                    numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode, curencyType, rate, valueDate) * numInvoiceRate.Value).ToString();
                }
                else
                {
                    numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode, curencyType, rate, valueDate)* numInvoiceRate.Value).ToString();
                    numTotal.Value = numTotal.Value - numDescripancy.Value;
                   
                }
               
            }
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void bindOpeningReport(object sender, EventArgs args)
        {
           
            openingLogic = new LcOpeningLogic();
            if (RadioSequenceStatus.SelectedValue == "Yes")
            {
                int count = 0;
                foreach (GridDataItem itemenew in RadGrid1.Items)
                {
                    //DataRow row1 = invoiceTable.Rows[Convert.ToInt32(itemenew.ID)];
                    if (Convert.ToInt32(itemenew["id"].Text) != Convert.ToInt32(Session["InvoiceId"]) && itemenew["status"].Text == "ACTIVE")
                    {
                        count = count + 1;

                    }

                }
                if (count == 0)
                {
                    List<TBL_CURRENCY> currency = new List<TBL_CURRENCY>();
                    string currencyCode = txtCurrency.Text;
                    currency = openingLogic.findCurrencyByCurrencyCode(currencyCode);
                    if (currency.Count == 1)
                    {
                        bool result1 = openingLogic.changeadviceStatus("Advised", Convert.ToInt32(Session["InvoiceId"].ToString()));
                        if (result1)
                        {
                            ReportDocument crt = new ReportDocument();
                            //adding crystal report path to the report
                            string aibCorespondentAccount = openingLogic.findCorspondentAccount(Convert.ToInt32(combRembursing.SelectedValue), currency[0].currencyId);
                            Session["AIBACCOUNT"] = aibCorespondentAccount.ToString();
                            // crt.Load(Server.MapPath("~/AIB_REPORT/LC_Advice_Sight.rpt"));

                            string conectionString = ConfigurationManager.ConnectionStrings["AIB_LC_DBConnectionString"].ConnectionString;
                            SqlConnection conn = new SqlConnection(conectionString);
                            conn.Open();

                            crt.Load(Server.MapPath("~/AIB_REPORT/LC_Advice_Sight.rpt"));

                            crt.SetDatabaseLogon("da", "pass@2123");
                            List<VW_DETAIL_LC> lclist = openingLogic.getLCbyLcNumber(txtLCNumber.Text.ToString());
                            // crt.SetDataSource(lclist.AsEnumerable());
                            Session["Report"] = crt;
                            Session["reportCode"] = "lcAdvice";
                            Session["lcNumber"] = txtLCNumber.Text.ToString();
                            //Server.TransferRequest("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                            Response.Redirect("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                        }
                        else
                        {
                            this.errorMessage("The Correct Invoice must be selected");
                        }
                    }
                    else {
                        this.errorMessage("Currency Code must be uniqe.please contact Administrator.");
                    }
                }
                else
                {

                    this.errorMessage("You cant Advice the last invoice befor Other active invoices. please settele invoices befor the last invoice");
                }

            }
            else {

                   List<TBL_CURRENCY> currency = new List<TBL_CURRENCY>();
                    string currencyCode = txtCurrency.Text;
                    currency = openingLogic.findCurrencyByCurrencyCode(currencyCode);
                    if (currency.Count == 1)
                    {
                    bool result1 = openingLogic.changeadviceStatus("Advised", Convert.ToInt32(Session["InvoiceId"].ToString()));
                    if (result1)
                    {
                        ReportDocument crt = new ReportDocument();
                        //adding crystal report path to the report
                        string aibCorespondentAccount = openingLogic.findCorspondentAccount(Convert.ToInt32(combRembursing.SelectedValue), currency[0].currencyId);
                        Session["AIBACCOUNT"] = aibCorespondentAccount.ToString();
                        // crt.Load(Server.MapPath("~/AIB_REPORT/LC_Advice_Sight.rpt"));

                        string conectionString = ConfigurationManager.ConnectionStrings["AIB_LC_DBConnectionString"].ConnectionString;
                        SqlConnection conn = new SqlConnection(conectionString);
                        conn.Open();

                        crt.Load(Server.MapPath("~/AIB_REPORT/LC_Advice_Sight.rpt"));

                        crt.SetDatabaseLogon("da", "pass@2123");
                        List<VW_DETAIL_LC> lclist = openingLogic.getLCbyLcNumber(txtLCNumber.Text.ToString());
                        // crt.SetDataSource(lclist.AsEnumerable());
                        Session["Report"] = crt;
                        Session["reportCode"] = "lcAdvice";
                        Session["lcNumber"] = txtLCNumber.Text.ToString();
                        //Server.TransferRequest("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                        Response.Redirect("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                }
                else
                {
                    this.errorMessage("The Correct Invoice must be selected");
                }
                }
                else
                {
                    this.errorMessage("Currency Code must be uniqe.please contact Administrator.");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void bindExcessReport(object sender, EventArgs args)
        {

            openingLogic = new LcOpeningLogic();
            ReportDocument crt = new ReportDocument();
            PaymentCalculater calc = new PaymentCalculater();
            //
            double Usrate = calc.usdRate(radioCurrencyType.SelectedValue.ToString(), Convert.ToDateTime(dateInvoiceDate.SelectedDate));
            Session["usdRate"] = Usrate.ToString();


            //adding crystal report path to the report
            string conectionString = ConfigurationManager.ConnectionStrings["AIB_LC_DBConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conectionString);
            conn.Open();
            crt.Load(Server.MapPath("~/AIB_REPORT/LC_Opening_Excess.rpt"));
            // crt.Load("C:/db/AIB_REPORT/LC_Opening.rpt");
            //crt.SetDatabaseLogon("da", "pass@2123","10.34.30.20","AIB_LC_DB");
            crt.SetDatabaseLogon("da", "pass@2123");
            // crt.SetDatabaseLogon("Administrator", "aibServer@1", "10.34.30.20", "AIB_LC_DB");
            //List<VW_DETAIL_LC> lclist = openingLogic.getLCbyLcNumber(txtLCNumber.Text.ToString());
            // crt.SetDataSource(lclist.AsEnumerable());
            Session["Report"] = crt;
            Session["reportCode"] = "lcNumber";
            Session["lcNumber"] = txtLCNumber.Text.ToString();
            Response.Redirect("~/AIB_REPORT/RepPages/LC_Reports.aspx");
        }
          
        /// <summary>
        /// 
        /// </summary>
        private void resetInvoice() {
            txtReferenceNumber.Text = "";
            if (radioCurrencyType.SelectedValue == "SPOT")
            {
                dateInvoiceDate.Clear();
                numInvoiceRate.Text = "";
            }           
            numBillAmount.Text = "";
            numDescripancy.Text = "";
            numTotal.Text = "";
            numExcessAmount.Text = "";
            numUtlizedAmount.Text = "";
             numInvoiceValue.Text = "";
             dateSheepmentDate.Clear();
             txtOpThrough.Text = "";
             numExcessAmount.Text = "";
             numNumberOfSwift.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void resetLCAdviceSite(object sender, EventArgs args)
        {

            txtAccountNO.Text = "";
            txtBranchCode.Text = "";
            txtBranchName.Text = "";
            txtCustomerName.Text = "";
            txtLCNumber.Text = "";
            txtOpThrough.Text = "";
            txtPermitNUmber.Text = "";
            txtSupplyer.Text = "";
            txtSearch.Text = "";
            txtReferenceNumber.Text = "";
            numBillAmount.Text = "";
            numCurrencyRate.Text = "";
            numDescripancy.Text = "";
           txtOpeningPeriod.Text="";
            txtCurrency.Text="";
            combOpThrough.ClearSelection();
            combRembursing.ClearSelection();
            dateInvoiceDate.Clear();
            dateValueDate.Clear();
            numInvoiceValue.Text = "";
            numInvoiceRate.Text = "";
            numBillAmount.Text = "";
            numDescripancy.Text = "";
            numTotal.Text = "";
            numLcValue.Text = "";
            numExcessAmount.Text = "";
            numUtlizedAmount.Text = "";
            DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
            invoiceTable.Clear();
            RadGrid1.DataSource = invoiceTable;
            RadGrid1.DataBind();
            txtSearch.Text = "";
            txtSearch.ReadOnly = false;
            btnSearch.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void initializeInvoiceDataTable() {
          

            DataTable invoiceTable = new DataTable();
            invoiceTable.Columns.Add("id", typeof(Int32));
            invoiceTable.Columns.Add("Reference Number", typeof(String));
            invoiceTable.Columns.Add("Invoice Date",typeof(DateTime));
            invoiceTable.Columns.Add("Invoice Value",typeof(Double));
            invoiceTable.Columns.Add("Invoice Rate",typeof(Double));
            invoiceTable.Columns.Add("Document Recived From", typeof(String));
            invoiceTable.Columns.Add("openingBankId", typeof(Int32));
            invoiceTable.Columns.Add("status", typeof(String));
            invoiceTable.Columns.Add("Descripancy Exists", typeof(String));
            invoiceTable.Columns.Add("lcNumber", typeof(String));
            invoiceTable.Columns.Add("Number Of Swift", typeof(Int32));
            invoiceTable.Columns.Add("Sheepment Date", typeof(DateTime));
            invoiceTable.Columns.Add("Sequence", typeof(Int32));
            invoiceTable.Columns.Add("sequenceStatus", typeof(String));
            ViewState["InvoiceTable"] = invoiceTable;
            RadGrid1.DataSource = invoiceTable;
            RadGrid1.DataBind();
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void saveExcessAmountPermit(object sender, EventArgs args)
        {
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtExcessAmountPermit, ComponentValidator.INTEGER_NUMBER, true);
            valids.addComponent(numExcessAmount, ComponentValidator.FLOAT_NUMBER, true);
            if (valids.isAllComponenetValid())
            {
                
                string statusExe = "New";
                if (Session["ExcessStatus"] != null && Convert.ToString(Session["ExcessStatus"].ToString()).Equals(statusExe.ToString()))
                {
                    if (Session["invoiceSequence"] != null)
                    {
                        double tolleranceValue = Convert.ToDouble((numTollerance.Value / 100) * numLcValue.Value);
                        double totalInvoice = Convert.ToDouble(Session["TotalInvoice"]);
                        int invoiceSequence = Convert.ToInt32(Session["invoiceSequence"]);
                        totalInvoice = Convert.ToDouble(totalInvoice + numInvoiceValue.Value);
                        Session["ExcessAmount"] = (totalInvoice-(numLcValue.Value + tolleranceValue)).ToString();
                     
                        //bool result = openingLogic.addExcessAmount(numExcessAmount.Value, radioExcessPermityear.SelectedValue, txtExcessAmountPermit.Text, txtLCNumber.Text);
                        if (Session["ExcessAmount"] != null)
                        {
                            if (radioExcessPermityear.SelectedValue.Equals("1"))
                            {
                                Session["ExcessPermitYear"] = (DateTime.Now.Year).ToString();
                            }
                            else
                            {

                                Session["ExcessPermitYear"] = (DateTime.Now.Year - 1).ToString();
                            }
                            Session["ExcessPermitCode"] = txtExcessAmountPermit.Text.ToString();
                            DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                            invoiceTable.Rows.Add(0, txtReferenceNumber.Text, dateInvoiceDate.SelectedDate, numInvoiceValue.Value, numInvoiceRate.Value, txtOpThrough.Text, 0, "NEW", RadioDescripancyChek.SelectedValue, txtLCNumber.Text,Convert.ToInt32(numNumberOfSwift.Value), invoiceSequence, RadioSequenceStatus.SelectedValue);
                            RadGrid1.DataSource = invoiceTable;
                            RadGrid1.DataBind();

                            RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                          
                            Session["TotalInvoice"] = totalInvoice.ToString();
                            this.resetInvoice();
                            foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                            {
                                if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled" || items["status"].Text == "Advised")
                                {
                                    int index = items.ItemIndex;
                                    RadGrid1.MasterTableView.Items[index].Visible = false;
                                }
                            }

                            string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                        else
                        {
                            string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            this.errorMessage("There must be excess amount for this invoice.");
                        }
                    }
                    else
                    {
                        string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        this.errorMessage("The invoice must have sequence number.");
                    }
                }
                else
                {
                    Session["InvoiceId"] = Session["InvoiceId"].ToString();

                    DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                    double totalInvoice = Convert.ToDouble(Session["TotalInvoice"]);
                    double invoiceValue = Convert.ToDouble(Session["CurrentInvoiceValue"]);
                    double diff = Convert.ToDouble(numInvoiceValue.Value - invoiceValue);
                    totalInvoice = totalInvoice + diff;
                    double tolleranceValue = Convert.ToDouble((numTollerance.Value / 100) * numLcValue.Value);

                    Session["CurrentInvoiceValue"] = invoiceValue.ToString(); 
                   

                    string edu = Session["Id"].ToString();
                    int eduId = Convert.ToInt32(edu);
                    DataRow row = invoiceTable.Rows[eduId];
                    GridDataItem item = RadGrid1.Items[eduId];
                    string status = "No";
                    if (RadioSequenceStatus.SelectedValue == "Yes")
                    {
                        var rows = invoiceTable.Select("sequenceStatus ='Yes'");

                        if (rows.Count() == 1 && rows[0]["Sequence"].ToString() != row["Sequence"].ToString())
                        {
                            status = "Yes";
                        }
                        if (rows.Count() > 1)
                        {
                            this.errorMessage("Last Invoice must be only one");
                        }
                        {
                        }

                        if (status == "No")
                        {

                            if (radioExcessPermityear.SelectedValue.Equals("1"))
                            {
                                Session["ExcessPermitYear"] = (DateTime.Now.Year).ToString();
                            }
                            else
                            {

                                Session["ExcessPermitYear"] = (DateTime.Now.Year - 1).ToString();
                            }
                           
                            Session["ExcessPermitCode"] = txtExcessAmountPermit.Text.ToString();
                            Session["TotalInvoice"] = totalInvoice.ToString();
                            Session["ExcessAmount"] = (totalInvoice - (numLcValue.Value + tolleranceValue)).ToString();
                            row["Reference Number"] = txtReferenceNumber.Text.ToString();
                            row["Invoice Date"] = dateInvoiceDate.SelectedDate;
                            row["Invoice Value"] = numInvoiceValue.Value;
                            row["Invoice Rate"] = numInvoiceRate.Value;
                            row["Document Recived From"] = txtOpThrough.Text;
                            row["openingBankId"] = 0;
                            row["Descripancy Exists"] = RadioDescripancyChek.SelectedValue;
                            row["sequenceStatus"] = RadioSequenceStatus.SelectedValue;
                            row["Number Of Swift"] = numNumberOfSwift.Value.ToString();
                            ViewState["InvoiceTable"] = invoiceTable;
                            RadGrid1.DataSource = invoiceTable;
                            RadGrid1.DataBind();
                            RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                            this.resetInvoice();
                            btnEdit.Visible = false;
                            btnRemove.Visible = false;
                            btnAdd.Visible = true;
                            foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                            {
                                if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled"||items["status"].Text == "Advised")
                                {
                                    int index = items.ItemIndex;
                                    RadGrid1.MasterTableView.Items[index].Visible = false;
                                }
                            }
                            string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                        else
                        {
                            
                            string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            this.errorMessage("More than one invoice can't be last invoice.");
                        }

                    }
                    else
                    {
                        var rows = invoiceTable.Select("sequenceStatus ='Yes'");

                        if (rows.Count() == 1 && Session["InvoiceId"].ToString() != rows[0]["id"].ToString())
                        {
                            if (radioExcessPermityear.SelectedValue.Equals("1"))
                            {
                                Session["ExcessPermitYear"] = (DateTime.Now.Year).ToString();
                            }
                            else
                            {

                                Session["ExcessPermitYear"] = (DateTime.Now.Year - 1).ToString();
                            }
                            Session["ExcessPermitCode"] = txtExcessAmountPermit.Text.ToString();
                            Session["TotalInvoice"] = totalInvoice.ToString();
                            Session["ExcessAmount"] = (totalInvoice - (numLcValue.Value + tolleranceValue)).ToString();
                            row["Reference Number"] = txtReferenceNumber.Text.ToString();
                            row["Invoice Date"] = dateInvoiceDate.SelectedDate;
                            row["Invoice Value"] = numInvoiceValue.Value;
                            row["Invoice Rate"] = numInvoiceRate.Value;
                            row["Document Recived From"] = txtOpThrough.Text;
                            row["openingBankId"] = 0;
                            row["Descripancy Exists"] = RadioDescripancyChek.SelectedValue;
                            row["sequenceStatus"] = RadioSequenceStatus.SelectedValue;
                            row["Number Of Swift"] = numNumberOfSwift.Value.ToString();
                            ViewState["InvoiceTable"] = invoiceTable;
                            RadGrid1.DataSource = invoiceTable;
                            RadGrid1.DataBind();
                            RadGrid1.MasterTableView.GetColumn("id").Visible = false;
                            this.resetInvoice();
                            btnEdit.Visible = false;
                            btnRemove.Visible = false;
                            btnAdd.Visible = true;
                            foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                            {
                                if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled" || items["status"].Text == "Advised")
                                {
                                    int index = items.ItemIndex;
                                    RadGrid1.MasterTableView.Items[index].Visible = false;
                                }
                            }
                            string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                        else {
                            string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            this.errorMessage("There must be at list one last invoice.");
                        }
                    }
                }

            }
            else {
                string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void sheepmentDateIndexChanged(object sender, EventArgs args) {
            PaymentCalculater calc = new PaymentCalculater();
         ComponentValidator valids = new ComponentValidator();
            valids.addComponent(dateInvoiceDate, ComponentValidator.GC_DATE, true);
            valids.addComponent(numInvoiceRate, ComponentValidator.FLOAT_NUMBER, true);
            if (valids.isAllComponenetValid())
            {
                if (numInvoiceValue.Text != "" && numInvoiceValue.Value != null) {
                    DateTime duedate = calc.calculateExpirationDueDate(txtLCNumber.Text, Convert.ToDateTime(Session["OpeningDate"]), Convert.ToInt32(txtOpeningPeriod.Text));
                   // double newInvoice = 0;
                    int invoiceId;
                    if (ViewState["InvoiceId"] != null)
                    {
                        //
                        string status = ViewState["sequenceStatus"].ToString();
                        invoiceId = Convert.ToInt32(Session["InvoiceId"].ToString());
                        DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                        this.tasksForSelectedInvoices(invoiceId, status, duedate, invoiceTable);
                    }
                    else
                    {
                        this.tasksForNewInvoices(duedate);
                    }                          
                }
            
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void sequenceStausIndexChanged(object sender, EventArgs args)
        { 
           PaymentCalculater calc = new PaymentCalculater();
         ComponentValidator valids = new ComponentValidator();
            valids.addComponent(dateInvoiceDate, ComponentValidator.GC_DATE, true);
            valids.addComponent(numInvoiceRate, ComponentValidator.FLOAT_NUMBER, true);
              valids.addComponent(numInvoiceValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(dateSheepmentDate, ComponentValidator.GC_DATE, true);
            if (valids.isAllComponenetValid())
            {
                    DateTime duedate = calc.calculateExpirationDueDate(txtLCNumber.Text, Convert.ToDateTime(Session["OpeningDate"]), Convert.ToInt32(txtOpeningPeriod.Text));
                    double newInvoice = 0;
                    if (ViewState["InvoiceId"] != null)
                    {
                        foreach (GridDataItem item1 in RadGrid1.MasterTableView.Items)
                        {
                            if ((item1["status"].Text == "NEW" || item1["status"].Text == "ACTIVE" || item1["status"].Text == "Setteled" || item1["status"].Text == "Advised") && item1.ItemIndex != RadGrid1.SelectedItems[0].ItemIndex)
                            {
                                newInvoice = newInvoice + Convert.ToDouble(item1["Invoice Value"].Text);
                            }
                        }
                        newInvoice = newInvoice + Convert.ToDouble(numInvoiceValue.Value);

                        if (RadioSequenceStatus.SelectedValue.Equals("Yes"))
                        {
                            if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), duedate) > 0)
                            {
                                if (Convert.ToDouble(numLcValue.Value) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value)) > 0)
                                {
                                    double remainingLCValue = Convert.ToDouble(numLcValue.Value) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value));
                                    TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)duedate;
                                    int days = Convert.ToInt32(difference.TotalDays) - 1;
                                    numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                                    if (RadioDescripancyChek.SelectedValue == "No")
                                    {
                                        string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                                        string currencyCode = txtCurrency.Text;
                                        string curencyType = radioCurrencyType.SelectedValue;
                                        DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                                        double rate = Convert.ToDouble(numInvoiceRate.Value);
                                        RadioDescripancyChek.SelectedValue = "Yes";
                                        numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode, curencyType, rate, valueDate) * numInvoiceRate.Value).ToString();
                                        numTotal.Value = numTotal.Value - numDescripancy.Value;
                                    }
                                }
                                else
                                {

                                    numExpirationAmount.Value = 0;
                                }
                            }
                            else
                            {
                                numExpirationAmount.Value = 0;
                            }
                        }
                        else
                        {
                            if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), duedate) > 0)
                            {
                                double remainingLCValue = Convert.ToDouble(numInvoiceValue.Value);
                                TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)duedate;
                                int days = Convert.ToInt32(difference.TotalDays) - 1;
                                numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                                if (RadioDescripancyChek.SelectedValue == "No")
                                {
                                    string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                                    string currencyCode = txtCurrency.Text;
                                    string curencyType = radioCurrencyType.SelectedValue;
                                    DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                                    double rate = Convert.ToDouble(numInvoiceRate.Value);
                                    RadioDescripancyChek.SelectedValue = "Yes";
                                    numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode, curencyType, rate, valueDate) * numInvoiceRate.Value).ToString();
                                    numTotal.Value = numTotal.Value - numDescripancy.Value;
                                }
                            }
                            else
                            {
                                numExpirationAmount.Value = 0;
                            }
                        }
                    }
                }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="sequenceStatus"></param>
        public void tasksForSelectedInvoices(int invoiceId, string sequenceStatus, DateTime dueDate, DataTable invoiceTable)
        {
            PaymentCalculater calc = new PaymentCalculater();

            // Calculating tollerance value for tollerance percent 
            double tollranceValue = Convert.ToDouble(numLcValue.Value * numTollerance.Value) / 100;

            //Calculating total Invoices values Other than the selected invoice 
            double newInvoice=0;

            foreach (GridDataItem item1 in RadGrid1.MasterTableView.Items)
            {
                if ((item1["status"].Text == "NEW" || item1["status"].Text == "ACTIVE" || item1["status"].Text == "Setteled" || item1["status"].Text == "Advised") && item1.ItemIndex != RadGrid1.SelectedItems[0].ItemIndex)
                {
                    newInvoice = newInvoice + Convert.ToDouble(item1["Invoice Value"].Text);
                }
            }
            //adding the selected invoice value with rest of invoice values 
            newInvoice = newInvoice + Convert.ToDouble(numInvoiceValue.Value);

            // If the selected invoice is last invoice            
            if (sequenceStatus == "Yes")
            {
                // Calculating Excess Amount if totla invoice value grater than lc value plus tollerance value 

                if (newInvoice > (numLcValue.Value + tollranceValue))
                {
                    numExcessAmount.Text = ((newInvoice - (numLcValue.Value + numTollerance.Value)) * numInvoiceRate.Value).ToString();
                }
                else
                {
                    numExcessAmount.Value = 0;
                }
              
                // caluculating expiration amount if LC Sheepment date is outDated
                if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), dueDate) > 0)
                {
                    //checking that the total invoce other than the selected invoice is less than LC value
                    if (Convert.ToDouble(numLcValue.Value + tollranceValue) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value)) > 0)
                    {
                        double remainingLCValue = Convert.ToDouble(numLcValue.Value + tollranceValue) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value));
                        TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)dueDate;
                        int days = Convert.ToInt32(difference.TotalDays) - 1;
                        numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                        if (RadioDescripancyChek.SelectedValue == "No")
                        {
                            string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                            string currencyCode1 = txtCurrency.Text;
                            string curencyType1 = radioCurrencyType.SelectedValue;
                            DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                            double rate = Convert.ToDouble(numInvoiceRate.Value);
                            RadioDescripancyChek.SelectedValue = "Yes";
                            numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode1, curencyType1, rate, valueDate) * numInvoiceRate.Value).ToString();
                            numTotal.Value = numTotal.Value - numDescripancy.Value;
                        }
                    }
                    else
                    {
                        numExpirationAmount.Value = 0;
                    }
                }
                else
                {
                    numExpirationAmount.Value = 0;
                }
            }

            else
            {
                //if the selected invoice is not last invoice 

                //check that there is other last invoice in the table
                var rows = invoiceTable.Select("sequenceStatus ='Yes'");

                //if there is other invoice which is last invoice in the invoice table
                if (rows.Count() >= 1)
                {
                    numExcessAmount.Value = 0;
                     // caluculating expiration amount if LC Sheepment date is outDated
                    if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), dueDate) > 0)
                    {
                        //check that the selected invoice value less than lc value plus tollerance
                        
                            double remainingLCValue = Convert.ToDouble(numInvoiceValue.Value);
                            TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)dueDate;
                            int days = Convert.ToInt32(difference.TotalDays) - 1;
                            numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                            if (RadioDescripancyChek.SelectedValue == "No")
                            {
                                string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                                string currencyCode2 = txtCurrency.Text;
                                string curencyType2 = radioCurrencyType.SelectedValue;
                                DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                                double rate = Convert.ToDouble(numInvoiceRate.Value);
                                RadioDescripancyChek.SelectedValue = "Yes";
                                numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode2, curencyType2, rate, valueDate) * numInvoiceRate.Value).ToString();
                                numTotal.Value = numTotal.Value - numDescripancy.Value;
                           }
                       
                    }
                    else
                    {
                        numExpirationAmount.Value = 0;
                    }
                }
                else
                {

                    // if there is not last invoice in the table 
                    var row = invoiceTable.Select("Sequence = MAX(Sequence)");

                    //if the selected invoice is the maximum sequence invoice 
                    if (Convert.ToInt32(row[0]["id"]) == Convert.ToInt32(ViewState["InvoiceId"]) && Convert.ToInt32(row[0]["Sequence"]) == Convert.ToInt32(ViewState["Sequence"]))
                    {
                        invoiceId = Convert.ToInt32(Session["InvoiceId"].ToString());

                        //calculating excess Amount if total invoice is greater than lc value plus tollerance
                        if (newInvoice > (numLcValue.Value + numTollerance.Value))
                        {
                            numExcessAmount.Text = ((newInvoice - (numLcValue.Value + numTollerance.Value)) * numInvoiceRate.Value).ToString();

                            //calculating repiration amount for remaining amounts except excess amount 
                            if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), dueDate) > 0)
                            {
                                if (Convert.ToDouble(numLcValue.Value) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value)) > 0)
                                {
                                    double remainingLCValue = Convert.ToDouble(numLcValue.Value) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value));
                                    TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)dueDate;
                                    int days = Convert.ToInt32(difference.TotalDays) - 1;
                                    numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                                    if (RadioDescripancyChek.SelectedValue == "No")
                                    {
                                        string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                                        string currencyCode2 = txtCurrency.Text;
                                        string curencyType2 = radioCurrencyType.SelectedValue;
                                        DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                                        double rate = Convert.ToDouble(numInvoiceRate.Value);
                                        RadioDescripancyChek.SelectedValue = "Yes";
                                        numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode2, curencyType2, rate, valueDate) * numInvoiceRate.Value).ToString();
                                        numTotal.Value = numTotal.Value - numDescripancy.Value;
                                    }
                                }
                                else
                                {

                                    numExpirationAmount.Value = 0;
                                }
                            }
                            else
                            {
                                numExpirationAmount.Value = 0;
                            }

                        }
                        else
                        {
                            numExcessAmount.Value = 0;
                            // calculating expiration when total invoce less than lc value plus tollerance 
                            if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), dueDate) > 0)
                            {
                                double remainingLCValue = Convert.ToDouble(numInvoiceValue.Value);
                                TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)dueDate;
                                int days = Convert.ToInt32(difference.TotalDays) - 1;
                                numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                                if (RadioDescripancyChek.SelectedValue == "No")
                                {
                                    string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                                    string currencyCode2 = txtCurrency.Text;
                                    string curencyType2 = radioCurrencyType.SelectedValue;
                                    DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                                    double rate = Convert.ToDouble(numInvoiceRate.Value);
                                    RadioDescripancyChek.SelectedValue = "Yes";
                                    numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode2, curencyType2, rate, valueDate) * numInvoiceRate.Value).ToString();
                                    numTotal.Value = numTotal.Value - numDescripancy.Value;
                                }

                            }
                            else
                            {
                                numExpirationAmount.Value = 0;
                            }

                        }


                        // numExcessAmount.Text = (calc.calculculateExessAmount(Convert.ToDouble(numLcValue.Value), newInvoice, txtLCNumber.Text, invoiceId) * numInvoiceRate.Value).ToString();
                    }
                    else
                    {

                        if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), dueDate) > 0)
                        {
                            //check that the selected invoice value less than lc value plus tollerance

                            double remainingLCValue = Convert.ToDouble(numInvoiceValue.Value);
                            TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)dueDate;
                            int days = Convert.ToInt32(difference.TotalDays) - 1;
                            numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                            if (RadioDescripancyChek.SelectedValue == "No")
                            {
                                string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                                string currencyCode2 = txtCurrency.Text;
                                string curencyType2 = radioCurrencyType.SelectedValue;
                                DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                                double rate = Convert.ToDouble(numInvoiceRate.Value);
                                RadioDescripancyChek.SelectedValue = "Yes";
                                numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode2, curencyType2, rate, valueDate) * numInvoiceRate.Value).ToString();
                                numTotal.Value = numTotal.Value - numDescripancy.Value;
                            }
                        }
                        else
                        {
                            numExpirationAmount.Value = 0;
                        }
                    }


                }


            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="sequenceStatus"></param>
        /// <param name="dueDate"></param>
        /// <param name="invoiceTable"></param>
        public void tasksForNewInvoices( DateTime dueDate)
        {
            PaymentCalculater calc = new PaymentCalculater();

            // Calculating tollerance value for tollerance percent 
            double tollranceValue = Convert.ToDouble(numLcValue.Value * numTollerance.Value) / 100;

            //Calculating total Invoices values Other than the selected invoice 
            double newInvoice=0;
         
            foreach (GridDataItem item1 in RadGrid1.MasterTableView.Items)
            {
                if ((item1["status"].Text == "NEW" || item1["status"].Text == "ACTIVE" || item1["status"].Text == "Setteled" || item1["status"].Text == "Advised"))
                {
                    newInvoice = newInvoice + Convert.ToDouble(item1["Invoice Value"].Text);
                }
            }

            newInvoice = newInvoice + Convert.ToDouble(numInvoiceValue.Value);

            //calculating excess amount if total invoice value is greater than LC Value pluse tollerance
            if (newInvoice > (numLcValue.Value + numTollerance.Value))
            {
                numExcessAmount.Text = ((newInvoice - (numLcValue.Value + numTollerance.Value)) * numInvoiceRate.Value).ToString();
            }
            else
            {
                numExcessAmount.Value = 0;
            }

            //if the new invoice is last invoice 
            if (RadioSequenceStatus.SelectedValue == "Yes")
            {
                if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), dueDate) > 0)
                {
                    if (Convert.ToDouble(numLcValue.Value) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value)) > 0)
                    {
                        double remainingLCValue = Convert.ToDouble(numLcValue.Value) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value));
                        TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)dueDate;
                        int days = Convert.ToInt32(difference.TotalDays) - 1;
                        numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                        if (RadioDescripancyChek.SelectedValue == "No")
                        {
                            string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                            string currencyCode1 = txtCurrency.Text;
                            string curencyType1 = radioCurrencyType.SelectedValue;
                            DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                            double rate = Convert.ToDouble(numInvoiceRate.Value);
                            RadioDescripancyChek.SelectedValue = "Yes";
                            numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode1, curencyType1, rate, valueDate) * numInvoiceRate.Value).ToString();
                            numTotal.Value = numTotal.Value - numDescripancy.Value;
                        }
                    }
                    else
                    {
                        numExpirationAmount.Value = 0;
                    }
                }
                else
                {
                    numExpirationAmount.Value = 0;
                }
            }
            else
            {
                // if the new invoice is not last invoice
                if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), dueDate) > 0)
                {
                    double remainingLCValue = Convert.ToDouble(numInvoiceValue.Value);
                    TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)dueDate;
                    int days = Convert.ToInt32(difference.TotalDays) - 1;
                    numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                    if (RadioDescripancyChek.SelectedValue == "No")
                    {
                        string discripancyState = RadioDescripancyChek.SelectedValue.ToString();
                        string currencyCode2 = txtCurrency.Text;
                        string curencyType2 = radioCurrencyType.SelectedValue;
                        DateTime valueDate = (DateTime)dateInvoiceDate.SelectedDate;
                        double rate = Convert.ToDouble(numInvoiceRate.Value);
                        RadioDescripancyChek.SelectedValue = "Yes";
                        numDescripancy.Text = (calc.calculculateDiscripancy(discripancyState, currencyCode2, curencyType2, rate, valueDate) * numInvoiceRate.Value).ToString();
                        numTotal.Value = numTotal.Value - numDescripancy.Value;
                    }
                }
                else
                {
                    numExpirationAmount.Value = 0;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void closeRadWindow(object sender, EventArgs args)
        {
            string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseModal();", true);
            //RadWindow1.DestroyOnClose = true;
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