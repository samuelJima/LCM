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

namespace AIB_FORMS_PRINT.FORMS_GUI
{
    public partial class lcExtension : System.Web.UI.Page
    {
        LcOpeningLogic openingLogic;
        LC_Advice_logic adviceLogic;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            openingLogic = new LcOpeningLogic();
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

          
                //popullating rembursing bank
                this.populateRembursingBank();
                combRembursing.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                //populatecurrency first Time 
                this.populateCurencyFirstTime();



                //popullating rembursing bank
                this.populateOpenedThrough();
                combOpThrough.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                lblCurrentDate.Text = DateTime.Now.ToShortDateString();
            }

        }

     

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
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void caluculatePayments_TextChanged(Object sender, EventArgs args) {

            PaymentCalculater calc = new PaymentCalculater();
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(numExtentionLcValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numNumberOfPeriod, ComponentValidator.INTEGER_NUMBER, true);
            valids.addComponent(numCurrencyRate, ComponentValidator.FLOAT_NUMBER, true);
            if (valids.isAllComponenetValid())
            {

                   int periods = Convert.ToInt32(numNumberOfPeriod.Value);
             
                    string confirmationStatus = RadioLCConfirmed.SelectedValue;
                    numExhCommission.Value = 0; 
                     numOpenCommission.Value = 0;
                     numServiceCharge.Text = (periods * (calc.calculateServiceCharge(Convert.ToDouble(numExtentionLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value))).ToString();
                     numExtentionCommission.Text = (periods * calc.calculateExtentionCommission(Convert.ToDouble(numExtentionLcValue.Value)) * numCurrencyRate.Value).ToString();
                     numSwift.Text = (calc.calculateExtentionSWIFT()).ToString();
                

                    if (confirmationStatus.Equals("1"))
                    {
                        double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numExtentionLcValue.Text));
                        if (forienConfirmationComm > 0)
                        {
                            numConfCommission.Text = (periods*forienConfirmationComm * (numCurrencyRate.Value)).ToString();
                        }
                        else {
                            this.errorMessage("Confirmation commision tarif can not be accessed please contact Administrater");
                        }
                    }
                    else
                    {
                        numConfCommission.Value = 0;
                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void saveLCOpeningExtention(object sender, EventArgs args)
        {

            openingLogic = new LcOpeningLogic();
            ComponentValidator valids = new ComponentValidator();

        
            valids.addComponent(numExtentionLcValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numNumberOfPeriod, ComponentValidator.INTEGER_NUMBER, true);
         

            if (valids.isAllComponenetValid())
            {
               

                if (Session["lcSatatus"] != null)
                {
                    string lcStatus = Session["lcSatatus"].ToString();
                    int maxInvSequence = 0;
                    if (lcStatus.Equals("Advised"))
                    {
                        if (Session["maxinvSequence"] != null)
                        {
                            maxInvSequence = Convert.ToInt32(Session["maxinvSequence"].ToString());
                        }
                        else
                        {
                            this.errorMessage("There must be at list one invoice for partial extention.");
                        }
                    }
                    
                    DateTime extentionDate = DateTime.Now.Date;
                    if (!openingLogic.extentionExists(extentionDate, txtLCNumber.Text))
                    {

                        bool result = openingLogic.saveLCExtention(Convert.ToDouble(numExtentionLcValue.Value), Convert.ToInt32(numNumberOfPeriod.Value), extentionDate, maxInvSequence, txtLCNumber.Text);

                        if (result)
                        {

                            btnSave.Visible = false;
                            btnReport.Visible = true;
                            this.postiveMessage("Letter Of Credit Extended Succefully.");

                        }
                        else
                        {
                            this.errorMessage("Letter Of Credit not extended Succefully, Please Contact System Adminstrator");
                        }
                    }
                    else {
                        this.errorMessage("LC can not be extended more than once in a day");
                    }
                }
                else {
                    this.errorMessage("LC condition must be either opened or Advised");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="arg"></param>
        protected void updateLCExtention(object source, EventArgs arg)
        {

            openingLogic = new LcOpeningLogic();
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(numNumberOfPeriod, ComponentValidator.FLOAT_NUMBER, true);
            
            if (valids.isAllComponenetValid())
            {
                if (Session["Branch"] != null)
                {

                    
                    DateTime openingDate = DateTime.Now;
                  
                        if (Session["incId"] != null)
                        {
                            int id = Convert.ToInt32(Session["incId"].ToString());
                            int periods = Convert.ToInt32(numNumberOfPeriod.Value); 
                            bool result = openingLogic.changeLC_Extention(id,Convert.ToInt32(numNumberOfPeriod.Value),txtLCNumber.Text);
                            if (result)
                            {

                                this.postiveMessage("The LC Extention updated succesfully"); 
                               
                            }
                            else
                            {
                                this.errorMessage("Letter Of Credit Extention not Updated Succefully, Please Contact Adminstrator");
                            }
                        }
                        else
                        {
                            this.errorMessage("Extention Id can not be null,please contact administrator");

                        }
                    
                 


                }
                else
                {
                    this.errorMessage("Letter Of Credit Not Updated Succefully, Please Contact System Adminstrator");
                }
            }
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        private void populateCurencyFirstTime()
        {

            List<TBL_CURRENCY> currency = new List<TBL_CURRENCY>();
            openingLogic = new LcOpeningLogic();
            // int corsId = Convert.ToInt32(combRembursing.SelectedValue);
            int corsId = 0;
            currency = openingLogic.findCurrencyByCorrespondenceID(corsId);
            combCurrency.DataValueField = "currencyId";
            combCurrency.DataTextField = "currencyName";
            combCurrency.DataSource = currency;
            combCurrency.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void bindOpeningReport(object sender, EventArgs args)
        {


            openingLogic = new LcOpeningLogic();
            ReportDocument crt = new ReportDocument();
            PaymentCalculater calc = new PaymentCalculater();
            //
            double Usrate = calc.usdRate(radioCurrencyType.SelectedValue.ToString(), Convert.ToDateTime(dateValueDate.SelectedDate));
            Session["usdRate"] = Usrate.ToString();
            Session["ExtentionCom"] = numExtentionCommission.Value.ToString();

            //adding crystal report path to the report
            string conectionString = ConfigurationManager.ConnectionStrings["AIB_LC_DBConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(conectionString);
            conn.Open();
            crt.Load(Server.MapPath("~/AIB_REPORT/LC_Extention.rpt"));
            // crt.Load("C:/db/AIB_REPORT/LC_Opening.rpt");
            //crt.SetDatabaseLogon("da", "pass@2123","10.34.30.20","AIB_LC_DB");
            crt.SetDatabaseLogon("da", "pass@2123");
            // crt.SetDatabaseLogon("Administrator", "aibServer@1", "10.34.30.20", "AIB_LC_DB");
            //List<VW_DETAIL_LC> lclist = openingLogic.getLCbyLcNumber(txtLCNumber.Text.ToString());
            // crt.SetDataSource(lclist.AsEnumerable());
            Session["Report"] = crt;
            Session["reportCode"] = "lcextention";
            Session["lcNumber"] = txtLCNumber.Text.ToString();
            //Server.TransferRequest("~/AIB_REPORT/RepPages/LC_Reports.aspx");
            Response.Redirect("~/AIB_REPORT/RepPages/LC_Reports.aspx");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void cancelLC(object sender, EventArgs args)
        {

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
                openingLogic = new LcOpeningLogic();
                PaymentCalculater calc = new PaymentCalculater();
             
                string branchAbrivation = Session["BranchAbrivation"].ToString();

                string lcNumber = txtSearch.Text;
                string lcSatatus = comboLcStatus.SelectedValue;
                Session["lcSatatus"] = lcSatatus.ToString();
                string lcStart = lcNumber.Substring(0, 3);
                if (lcStart.Equals(branchAbrivation))
                {
                    if (lcSatatus == "Opened")
                    {
                        List<VW_DETAIL_LC> lcOpend = new List<VW_DETAIL_LC>();
                        lcOpend = openingLogic.searchLcByLC_Number(lcNumber);
                        if (lcOpend.Count == 1)
                        {
                            bool result = false;
                                DateTime dudate = calc.calculateExpirationDueDate(lcOpend[0].lcNumber.ToString(), Convert.ToDateTime(lcOpend[0].lcOpeningDate), Convert.ToInt32(lcOpend[0].openingPeriods)).Date;
                                DateTime currentDate = DateTime.Now.Date;
                                if (DateTime.Compare(dudate, currentDate) < 0)
                                {
                                    result = openingLogic.changeLCExpirationStatus(txtLCNumber.Text.ToString());
                                }
                                else {
                                    result = true;
                                }
                                if (result)
                                {
                                    txtLCNumber.Text = lcOpend[0].lcNumber;
                                    txtBranchCode.Text = lcOpend[0].branchCode;
                                    txtBranchName.Text = lcOpend[0].branchName;
                                    txtCustomerName.Text = lcOpend[0].customerName;
                                    txtPermitNUmber.Text = "AIB/" + lcOpend[0].branchAbrivation + "/01-" + lcOpend[0].permitCode + "/" + lcOpend[0].permitYear.ToString().Substring(2, 2);
                                    txtSupplyer.Text = lcOpend[0].supplyerName;
                                    txtAccountNO.Text = lcOpend[0].customerAccount;
                                    numLcValue.Value = lcOpend[0].lcValue;
                                    dateOpeningDate.SelectedDate = lcOpend[0].lcOpeningDate;
                                    comboPeriod.SelectedIndex = comboPeriod.FindItemIndexByValue(lcOpend[0].openingPeriods.ToString());
                                    numUserLimitaition.Value = lcOpend[0].userExpirationDates;
                                    numExtentionLcValue.Value = lcOpend[0].lcValue; ;
                                    int end = lcOpend[0].lcNumber.ToString().Length - 1;
                                    int index = lcOpend[0].lcNumber.ToString().LastIndexOf('/', end);

                                    combOpThrough.SelectedValue = lcOpend[0].openThroughId.ToString();
                                    combRembursing.SelectedValue = lcOpend[0].corespondenceID.ToString();
                                    if (combRembursing.SelectedValue.Equals(string.Empty))
                                    {
                                        this.populateCurencyFirstTime();
                                    }
                                    else
                                    {
                                        List<VW_CORRESPONDNCE_CURRENCY> currency = new List<VW_CORRESPONDNCE_CURRENCY>();
                                        currency = openingLogic.findCurrencyByCorrespondenceIDNew(Convert.ToInt32(lcOpend[0].corespondenceID));
                                        combCurrency.DataValueField = "currencyId";
                                        combCurrency.DataTextField = "currencyName";
                                        combCurrency.DataSource = currency;
                                        combCurrency.DataBind();
                                    }
                                    combCurrency.SelectedIndex = combCurrency.FindItemIndexByText(lcOpend[0].openingCurrency.ToString());
                                    numMarginpaid.Value = Convert.ToDouble(lcOpend[0].marginPaid);
                                    dateValueDate.SelectedDate = lcOpend[0].valueDate;
                                    numCurrencyRate.Value = Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate);
                                    //
                                    btnSave.Visible = true;
                                    btnUpdate.Visible = false;
                                    btnReport.Visible = false;
                                    //btnRemove.Visible = false;
                                    txtSearch.ReadOnly = true;
                                    comboLcStatus.Enabled = false;
                                    Master.UpdateMessageMethod = true;

                                }
                                else {
                                    this.errorMessage("LC Expiration status can not be changed,please contact administrators");
                                }
                        }
                        else
                        {
                            this.errorMessage("The letter of credit does not exist or not created.");
                        }
                        Master.UpdateMessageMethod = true;
                    }
                    else {
                        List<VW_LC_WITH_ADVICE_SIGHT> lcOpend = new List<VW_LC_WITH_ADVICE_SIGHT>();
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
                            else {
                                result = true;
                            }
                            if (result)
                            {
                                double partialInvoiceValue = 0;
                                int maxinvSequence = 0;
                                bool islastInvoice = false;
                                foreach (var Item in lcOpend)
                                {
                                    partialInvoiceValue = partialInvoiceValue + Convert.ToDouble(Item.invoiceValue);
                                    if (Item.invoiceValue > maxinvSequence)
                                    {
                                        maxinvSequence = Convert.ToInt32(Item.invoiceSequence);
                                    }
                                    if (Item.sequenceStatus.Equals("Yes")) {
                                        islastInvoice = true;
                                    }
                                }
                                if (!islastInvoice)
                                {
                                    Session["maxinvSequence"] = maxinvSequence.ToString();

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
                                    dateOpeningDate.SelectedDate = lcOpend[0].lcOpeningDate;
                                    comboPeriod.SelectedIndex = comboPeriod.FindItemIndexByValue(lcOpend[0].openingPeriods.ToString());
                                    numUserLimitaition.Value = lcOpend[0].userExpirationDates;
                                    this.populateRembursingBank();
                                    this.populateOpenedThrough();
                                    combOpThrough.SelectedValue = lcOpend[0].openThroughId.ToString();
                                    combRembursing.SelectedValue = lcOpend[0].corespondenceID.ToString();
                                    this.populateCurencyFirstTime();
                                    combCurrency.SelectedIndex = combCurrency.FindItemIndexByText(lcOpend[0].openingCurrency);
                                    dateValueDate.SelectedDate = lcOpend[0].valueDate;
                                    numCurrencyRate.Value = Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate);
                                    totalInvoice = Convert.ToDouble(lcOpend[0].totalInvoiceValue);
                                    Session["TotalInvoice"] = totalInvoice.ToString();
                                    //binding invoices for a given Lc number

                                    numExtentionLcValue.Value = numLcValue.Value - partialInvoiceValue;

                                    btnSave.Visible = true;
                                    btnUpdate.Visible = false;
                                    btnReport.Visible = false;
                                    //btnRemove.Visible = false;
                                    txtSearch.ReadOnly = true;
                                    comboLcStatus.Enabled = false;
                                    Master.UpdateMessageMethod = true;
                                }
                                else {
                                    this.errorMessage("All LC invoices are advised you cant extened the LC.");
                                }
                             }
                                else {
                                    this.errorMessage("LC Expiration status can not be changed,please contact administrators");
                                }
                        }
                        else
                        {
                            this.errorMessage("The letter of credit does not exist or not created.");
                        }
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
        protected void searchLCExtention(object sender, EventArgs args)
        {
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtSearchLcInc, ComponentValidator.FULL_NAME, true);
            valids.addComponent(dateExtentionDate, ComponentValidator.GC_DATE, true);
          
            if (valids.isAllComponenetValid())
            {
                openingLogic = new LcOpeningLogic();
                PaymentCalculater calc = new PaymentCalculater();
                List<VW_LC_EXTENTION> lcOpend = new List<VW_LC_EXTENTION>();
                string branchAbrivation = Session["BranchAbrivation"].ToString();

                string SearchLcInc = txtSearchLcInc.Text;
                string lcStart = SearchLcInc.Substring(0, 3);
                if (lcStart.Equals(branchAbrivation))
                {
                        DateTime extentionDate =Convert.ToDateTime(dateExtentionDate.SelectedDate);
                        lcOpend = openingLogic.searchLcExtention(extentionDate, SearchLcInc);
                        if (lcOpend.Count == 1)
                        {
                            Session["incId"] = lcOpend[0].id.ToString();
                            txtLCNumber.Text = lcOpend[0].lcNumber;
                            txtBranchCode.Text = lcOpend[0].bankCode;
                            txtBranchName.Text = lcOpend[0].branchName;
                            txtCustomerName.Text = lcOpend[0].customerName;
                            txtPermitNUmber.Text = "AIB/" + lcOpend[0].branchAbrivation + "/01-" + lcOpend[0].permitCode + "/" + lcOpend[0].permitYear.ToString().Substring(2, 2);
                            txtSupplyer.Text = lcOpend[0].supplyerName;
                            txtAccountNO.Text = lcOpend[0].customerAccount;
                            numLcValue.Value = lcOpend[0].lcValue;
                            int end = lcOpend[0].lcNumber.ToString().Length - 1;
                            int index = lcOpend[0].lcNumber.ToString().LastIndexOf('/', end);

                         
                            combOpThrough.SelectedValue = lcOpend[0].openThroughId.ToString();
                            combRembursing.SelectedValue = lcOpend[0].corespondenceID.ToString();
                            if (combRembursing.SelectedValue.Equals(string.Empty))
                            {
                                this.populateCurencyFirstTime();
                            }
                            else
                            {
                                List<VW_CORRESPONDNCE_CURRENCY> currency = new List<VW_CORRESPONDNCE_CURRENCY>();
                                currency = openingLogic.findCurrencyByCorrespondenceIDNew(Convert.ToInt32(lcOpend[0].corespondenceID));
                                combCurrency.DataValueField = "currencyId";
                                combCurrency.DataTextField = "currencyName";
                                combCurrency.DataSource = currency;
                                combCurrency.DataBind();
                            }
                            combCurrency.SelectedIndex = combCurrency.FindItemIndexByText(lcOpend[0].openingCurrency.ToString());
                            numMarginpaid.Value = Convert.ToDouble(lcOpend[0].marginPaid);
                            dateValueDate.SelectedDate = lcOpend[0].valueDate;

                            //
                            numCurrencyRate.Value = Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate);
                            numExtentionLcValue.Value = lcOpend[0].extentionAmount;
                            numNumberOfPeriod.Value = lcOpend[0].extentionPeriod;

                            numServiceCharge.Text = (lcOpend[0].extentionPeriod * (calc.calculateServiceCharge(Convert.ToDouble(numExtentionLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value))).ToString();
                            numExtentionCommission.Text = (lcOpend[0].extentionPeriod * (numExtentionLcValue.Value) * numCurrencyRate.Value).ToString();
                            numSwift.Text = (calc.calculateExtentionSWIFT()).ToString();
                            numOpenCommission.Value = 0;
                            numExhCommission.Value = 0;
                            string confirmationStatus = RadioLCConfirmed.SelectedValue;
                            if (confirmationStatus.Equals("1"))
                            {
                                double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numExtentionLcValue.Text));
                                if (forienConfirmationComm > 0)
                                {
                                    numConfCommission.Text = (lcOpend[0].extentionPeriod * forienConfirmationComm * (numCurrencyRate.Value)).ToString();
                                }
                                else
                                {
                                    this.errorMessage("Confirmation commision tarif can not be accessed please contact Administrater");
                                }
                            }
                            else
                            {
                                numConfCommission.Value = 0;
                            }

                            //
                            btnSave.Visible = false;
                            btnUpdate.Visible = true;
                            btnReport.Visible = false;
                            //btnRemove.Visible = true;
                            txtSearch.ReadOnly = true;
                            txtSearchLcInc.ReadOnly = true;
                            dateExtentionDate.Enabled = false;
                            Master.UpdateMessageMethod = true;

                        }
                        else
                        {
                            this.errorMessage("The letter of credit does not exist or not created.");
                        }
                        Master.UpdateMessageMethod = true;
                   
                }
                else
                {
                    this.errorMessage("The letter of credit does not belong to this branch.");

                }
            }
        }


        protected void deactivateLCOpening(object sender, EventArgs args)
        {

            openingLogic = new LcOpeningLogic();
            string lcNumber = txtLCNumber.Text;
            bool result = openingLogic.changeLCStatusToRemove(lcNumber);
            if (result)
            {
                this.clearLCOpening();
                this.postiveMessage("LC sucessfuly removed.");
            }
            else
            {
                this.errorMessage("LC can not be Removed! Please contact the administrator.");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void nextClick(object sender, EventArgs args)
        {
            MultiPage1.SetActiveView(PageView2);
            MultiPage1.ActiveViewIndex = 1;
            //btnReset.Visible = false;
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
            //btnReset.Visible = true;
            Master.UpdateMessageMethod = true;

        }
        public void clearLCOpening()
        {

            txtLCNumber.Text = "";
            txtBranchCode.Text = "";
            txtBranchName.Text = "";
            txtCustomerName.Text = "";
            txtPermitNUmber.Text = "";
          
            txtSupplyer.Text = "";
            txtAccountNO.Text = "";
            numLcValue.Text = "";
            numCurrencyRate.Text = "";
            numMarginpaid.Text = "";
            numExhCommission.Text = "";
            numServiceCharge.Text = "";
            numOpenCommission.Text = "";
            numSwift.Text = "";
            numConfCommission.Text = "";
            combOpThrough.SelectedIndex = 0;
            combRembursing.SelectedIndex = 0;
          
            numNumberOfPeriod.Text = "";
            numExtentionLcValue.Text = "";
            dateValueDate.Clear();
            this.populateCurencyFirstTime();
            txtSearch.ReadOnly = false;
          
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnReport.Visible = false;
            //btnRemove.Visible = false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void clearLcdetail(object sender, EventArgs args)
        {

            txtLCNumber.Text = "";
            txtBranchCode.Text = "";
            txtBranchName.Text = "";
            txtCustomerName.Text = "";
            txtPermitNUmber.Text = "";         
            txtSupplyer.Text = "";
            txtAccountNO.Text = "";
            numLcValue.Text = "";
            numCurrencyRate.Text = "";
            numMarginpaid.Text = "";
            numExhCommission.Text = "";
            numServiceCharge.Text = "";
            numOpenCommission.Text = "";
            numSwift.Text = "";
            numConfCommission.Text = "";
            numExhCommission.Text = "";
            txtSearchLcInc.Text = "";
            txtSearch.Text = "";
            dateExtentionDate.Clear();
            numNumberOfPeriod.Text = "";
            numExtentionLcValue.Text = "";
            combOpThrough.SelectedIndex = 0;
            combRembursing.SelectedIndex = 0;
            dateValueDate.Clear();
            comboLcStatus.Enabled = true;
            this.populateCurencyFirstTime();
            txtSearch.ReadOnly = false;
            txtSearchLcInc.ReadOnly = true;
            dateExtentionDate.Enabled = false;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnReport.Visible = false;

            //btnRemove.Visible = false;
            Button5.Visible = true;
            Button3.Visible = false;


            Master.UpdateMessageMethod = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void clearLcdetailforUpdate(object sender, EventArgs args)
        {

            txtLCNumber.Text = "";
            txtBranchCode.Text = "";
            txtBranchName.Text = "";
            txtCustomerName.Text = "";
            txtPermitNUmber.Text = "";

            txtSupplyer.Text = "";
            txtAccountNO.Text = "";
            numLcValue.Text = "";
            numCurrencyRate.Text = "";
            numMarginpaid.Text = "";
            numExhCommission.Text = "";
            numServiceCharge.Text = "";
            numOpenCommission.Text = "";
            numSwift.Text = "";
            numConfCommission.Text = "";
            numExhCommission.Text = "";
            txtSearchLcInc.Text = "";
            txtSearch.Text = "";
            dateExtentionDate.Clear();
            numNumberOfPeriod.Text = "";
            numExtentionLcValue.Text = "";
            combOpThrough.SelectedIndex = 0;
            combRembursing.SelectedIndex = 0;
            dateValueDate.Clear();
            comboLcStatus.Enabled = false;
            this.populateCurencyFirstTime();
            txtSearch.ReadOnly = true;
            txtSearchLcInc.ReadOnly = false;
            dateExtentionDate.Enabled = true;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnReport.Visible = false;
            //btnRemove.Visible = false;
            Button5.Visible = false;
            Button3.Visible = true;


            Master.UpdateMessageMethod = true;
        }
        private void initalizeDataTable()
        {

            DataTable lcTable = new DataTable();

            //lcTable.Columns.Add()


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void errorMessage(string message)
        {

            this.MessageDivComfirmation = true;
            this.MessageDivComfirmationTwo = true;
            this.MessageRadTickerComf = true;
            this.MessageRadTickerComfTwo = true;
            this.MessageConfirmationBg = Color.Red;
            this.MessageConfirmationBgTwo = Color.Red;
            this.MessageLabel = message;
            this.MessageLabelTwo = message;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void postiveMessage(string message)
        {

            this.MessageDivComfirmation = true;
            this.MessageDivComfirmationTwo = true;
            this.MessageRadTickerComf = true;
            this.MessageRadTickerComfTwo = true;
            this.MessageConfirmationBg = Color.LightGreen;
            this.MessageConfirmationBgTwo = Color.LightGreen;
            this.MessageLabel = message;
            this.MessageLabelTwo = message;

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
        /// get and set the Visible property of RadTicker1
        /// </summary>
        public bool MessageDivComfirmationTwo
        {
            get { return RadTicker2.Visible; }
            set { RadTicker2.Visible = value; }
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
        /// get and set the autostart property of RadTicker1
        /// </summary>
        public bool MessageRadTickerComfTwo
        {
            get { return RadTicker2.AutoStart; }
            set { RadTicker2.AutoStart = value; }
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
        /// get and set the Text property of item1
        /// </summary>
        public string MessageLabelTwo
        {
            get { return item2.Text; }
            set { item2.Text = value; }
        }
        /// <summary>
        /// get and set the BackColor property of RadTicker1
        /// </summary>
        public System.Drawing.Color MessageConfirmationBg
        {
            get { return RadTicker1.BackColor; }
            set { RadTicker1.BackColor = value; }
        }
        /// <summary>
        /// get and set the BackColor property of RadTicker1
        /// </summary>
        public System.Drawing.Color MessageConfirmationBgTwo
        {
            get { return RadTicker2.BackColor; }
            set { RadTicker2.BackColor = value; }
        }
        
    }
}