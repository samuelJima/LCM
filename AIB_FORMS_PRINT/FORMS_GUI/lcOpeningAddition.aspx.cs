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
    public partial class lcOpeningAddition : System.Web.UI.Page
    {
       LcOpeningLogic openingLogic;
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
               // this.populateCurencyFirstTime();



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
        protected void generatePermitNumber_TextChanged(object sender, EventArgs args) {

            ComponentValidator valids = new ComponentValidator();
               valids.addComponent(txtPermitCode,ComponentValidator.INTEGER_NUMBER,true);
               if (valids.isAllComponenetValid())
               {
                   if (Session["BranchAbrivation"] != null)
                   {
                       openingLogic = new LcOpeningLogic();
                       string branchAbrivation = Session["BranchAbrivation"].ToString();
                       string permitCode = txtPermitCode.Text;
                       int permitYear = Convert.ToInt32(radioPermityear.SelectedValue);
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

                               txtIncPermitNumber.Text = "AIB/" + branchAbrivation + "/01-" + permitCode + "/" + year.ToString().Substring(2, 2);
                           }
                           else
                           {
                               this.errorMessage("permit Code already exist for given year");

                           }
                       }
                       else
                       {
                           this.errorMessage("permit Code must be five digit");

                       }
                   }
                   else {
                       this.errorMessage("Branch Abrivation Can not be null.please Contact administrator");
                   }
               }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void caluculatePayments_TextChanged(Object sender, EventArgs args) {

            PaymentCalculater calc = new PaymentCalculater();

            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(numIncremetLcValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numInccurencyRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(txtCurrency, ComponentValidator.TEXT_A_TO_Z_ONLY, true);
            if (valids.isAllComponenetValid())
            {
                if (txtAccountNO.Text.Substring(0, 2).Equals("01"))
                {
                    string confirmationStatus = RadioLCConfirmed.SelectedValue;
                    numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(numIncremetLcValue.Text)) * (numInccurencyRate.Value)).ToString();
                    numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numIncremetLcValue.Text), txtCurrency.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numInccurencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numInccurencyRate.Value)).ToString();
                    numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numIncremetLcValue.Text), txtCurrency.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numInccurencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numInccurencyRate.Value)).ToString();
                    numSwift.Text = (calc.calculateSWIFT()).ToString();

                    if (confirmationStatus.Equals("1"))
                    {
                        double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numIncremetLcValue.Text));
                        if (forienConfirmationComm > 0)
                        {
                            numConfCommission.Text = (forienConfirmationComm * (numInccurencyRate.Value)).ToString();
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

                else {
                    string confirmationStatus = RadioLCConfirmed.SelectedValue;
                    numExhCommission.Value = 0.0;
                    numExhCommission.Text ="0.0";
                    numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numIncremetLcValue.Text), txtCurrency.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numInccurencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                    numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numIncremetLcValue.Text), txtCurrency.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numInccurencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                    numSwift.Text = (calc.calculateSWIFT()).ToString();

                    if (confirmationStatus.Equals("1"))
                    {
                        double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numIncremetLcValue.Text));
                        if (forienConfirmationComm > 0)
                        {
                            numConfCommission.Text = (forienConfirmationComm * (numInccurencyRate.Value)).ToString();
                        }
                        else
                        {
                            this.errorMessage("Confirmation commision tarif can not be accessed, please contact Administrater");
                        }
                    }
                    else
                    {
                        numConfCommission.Value = 0;
                    }
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void saveLCOpeningIncrement(object sender, EventArgs args) {

            openingLogic = new LcOpeningLogic();
            ComponentValidator valids = new ComponentValidator();
          
            valids.addComponent(dateIncValueDate, ComponentValidator.GC_DATE, true);
            valids.addComponent(numIncremetLcValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numInccurencyRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(txtPermitCode, ComponentValidator.INTEGER_NUMBER, true);

            if (valids.isAllComponenetValid())
            {
                int year;
                if (radioPermityear.SelectedValue == "1")
                {
                    year = DateTime.Now.Year;
                }
                else {
                    year = DateTime.Now.Year-1;
                }

                bool result = openingLogic.saveLCIncrement(Convert.ToDouble(numIncremetLcValue.Value),year,txtPermitCode.Text,txtLCNumber.Text,Convert.ToDateTime(dateIncValueDate.SelectedDate),Convert.ToDouble(numInccurencyRate.Value));

                if (result)
                {
                    bool incResult = openingLogic.changeParentLCValue(Convert.ToDouble(numIncremetLcValue.Value), txtLCNumber.Text);
                     if(incResult){
                    btnSave.Visible = false;
                    btnReport.Visible = true;
                    this.postiveMessage("Letter Of Credit Increment Saved Succefully!");
                        }
                    else{
                           this.postiveMessage("Parent LC value can not be incremented, please contact adminiatrators.");
                     }
                }
                else
                {
                    this.errorMessage("Letter Of Credit Increment Not Saved Succefully, Please Contact System Adminstrator");
                }

            }
           }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="arg"></param>
        protected void updateLCIncrement(object source , EventArgs arg) { 
        
            openingLogic = new LcOpeningLogic();
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(dateIncValueDate, ComponentValidator.GC_DATE, true);
            valids.addComponent(numIncremetLcValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numInccurencyRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(txtPermitCode, ComponentValidator.INTEGER_NUMBER, true);

            if (valids.isAllComponenetValid())
            {
                if (Session["Branch"] != null)
                {
                   
                        int permitYear;
                        DateTime openingDate = DateTime.Now;
                        if (Convert.ToInt32(radioPermityear.SelectedValue) == 1)
                        {
                            permitYear = DateTime.Now.Year;
                        }
                        else
                        {
                            permitYear = DateTime.Now.Year - 1;
                        }
                        string permitCode = txtPermitCode.Text;
                        if (openingLogic.checkPermitCoddFroUpdate(permitCode, permitYear, txtLCNumber.Text))
                        {
                            if (Session["incId"] != null && Session["incAmount"]!=null)
                            {
                                int id = Convert.ToInt32(Session["incId"].ToString());
                                bool result = openingLogic.changeLC_increment(Convert.ToDouble(numIncremetLcValue.Value), permitYear, txtPermitCode.Text, txtLCNumber.Text, Convert.ToDateTime(dateIncValueDate.SelectedDate), Convert.ToDouble(numInccurencyRate.Value),id);
                                if (result)
                                {
                                     double incamount = Convert.ToDouble(numIncremetLcValue.Value) -  Convert.ToDouble(Session["incAmount"].ToString());
                                     bool incResult = openingLogic.changeParentLCValue(Convert.ToDouble(incamount), txtLCNumber.Text);
                                     if (incResult)
                                     {
                                         btnUpdate.Visible = false;
                                         btnReport.Visible = true;
                                         this.postiveMessage("Increment Updated Succefully!");
                                     }
                                     else
                                     {
                                         this.postiveMessage("Parent LC value can not be incremented, please contact adminiatrators.");
                                     }
                                }
                                else
                                {
                                    this.errorMessage("Letter Of Credit not Updated Succefully, Please Contact Adminstrator");
                                }
                            }
                            else {
                                this.errorMessage("Incremented amount id or incremented Amount can not be null");
                            }
                        }
                        else
                        {

                            this.errorMessage("Permit code already exists, Please Add another premic code.");
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
        /// <param name="sender"></param>
        /// <param name="args"></param>
            protected void populateCurrencybyValueDate(object sender, EventArgs args) {
                
                ComponentValidator valids = new ComponentValidator();
                valids.addComponent(txtAccountNO, ComponentValidator.TEXT_AND_NUMBER, true);
                valids.addComponent(txtPermitCode, ComponentValidator.TEXT_AND_NUMBER, true);
                valids.addComponent(txtCustomerName, ComponentValidator.FULL_NAME, true);
                valids.addComponent(txtCurrency, ComponentValidator.TEXT_A_TO_Z_ONLY, true);
               
                if (valids.isAllComponenetValid())
                {
                    PaymentCalculater calc = new PaymentCalculater();
                    openingLogic = new LcOpeningLogic();
                    string currencytype = radioCurrencyType.SelectedValue;
                    string currencyCode = txtCurrency.Text;
                    DateTime valueDate = Convert.ToDateTime(dateIncValueDate.SelectedDate);
                    List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
                    currencyRate = openingLogic.findCurrencyByValueDat(currencytype, currencyCode, valueDate);
                    if (currencyRate.Count == 1)
                    {
                        //numInccurencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                        numInccurencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                        if (!numIncremetLcValue.Text.Equals(string.Empty))
                        {
                            if (txtAccountNO.Text.Substring(0, 2).Equals("01"))
                            {
                                string confirmationStatus = RadioLCConfirmed.SelectedValue;
                                numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(numIncremetLcValue.Text)) * (numInccurencyRate.Value)).ToString();
                                numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numIncremetLcValue.Text), txtCurrency.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numInccurencyRate.Value)).ToString();
                                numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numIncremetLcValue.Text), txtCurrency.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numInccurencyRate.Value)).ToString();
                                numSwift.Text = (calc.calculateSWIFT()).ToString();

                                if (confirmationStatus.Equals("1"))
                                {
                                    double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numIncremetLcValue.Text));
                                    if (forienConfirmationComm > 0)
                                    {
                                        numConfCommission.Text = (forienConfirmationComm * (numInccurencyRate.Value)).ToString();
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
                            }

                            else
                            {
                                string confirmationStatus = RadioLCConfirmed.SelectedValue;
                                numExhCommission.Value = 0.0;
                                numExhCommission.Text = "0.0";
                                numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numIncremetLcValue.Text), txtCurrency.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numInccurencyRate.Value)).ToString();
                                numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numIncremetLcValue.Text), txtCurrency.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numInccurencyRate.Value)).ToString();
                                numSwift.Text = (calc.calculateSWIFT()).ToString();

                                if (confirmationStatus.Equals("1"))
                                {
                                    double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numIncremetLcValue.Text));
                                    if (forienConfirmationComm > 0)
                                    {
                                        numConfCommission.Text = (forienConfirmationComm * (numInccurencyRate.Value)).ToString();
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
                            }

                        }
                        else
                        {
                            numInccurencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                        }
                    }
                    else
                    {
                        this.clearByCurencyType();
                        this.errorMessage("There must be only one currency Rate.please contact the administrator.");
                    }
                }
            }
            public void clearByCurencyType() {

                numCurrencyRate.Text = "";
                numLcValue.Text = "";
                numOpenCommission.Text = "";
                numServiceCharge.Text = "";
                numConfCommission.Text = "";
                numExhCommission.Text = "";
            }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
            protected void permitYearIndexChange(object sender, EventArgs args) {
                if (!txtPermitCode.Text.Equals(string.Empty)) {
                    openingLogic = new LcOpeningLogic();
                    string branchAbrivation = Session["BranchAbrivation"].ToString();
                    string permitCode = txtPermitCode.Text;
                    int permitYear = Convert.ToInt32(radioPermityear.SelectedValue);
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
                        //if (openingLogic.checkPermitCode(permitCode, year))
                       // {

                            txtIncPermitNumber.Text = "AIB/" + branchAbrivation + "/01-" + permitCode + "/" + year.ToString().Substring(2, 2);
                      // }
                        //else
                        //{
                            //this.errorMessage("permit Code already exist for given year");

                        //}
                    }
                    else
                    {
                        this.errorMessage("permit Code must be five digit");

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
                //combCurrency.DataValueField = "currencyId";
                //combCurrency.DataTextField = "currencyName";
                //combCurrency.DataSource = currency;
                //combCurrency.DataBind();
            }
            
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
            protected void bindOpeningReport(object sender, EventArgs args) {
               

                openingLogic = new LcOpeningLogic();
                ReportDocument crt = new ReportDocument();
                PaymentCalculater calc = new PaymentCalculater();
                //
                double Usrate = calc.usdRate(radioCurrencyType.SelectedValue.ToString(), Convert.ToDateTime(dateValueDate.SelectedDate));
                Session["usdRate"] = Usrate.ToString();

               
                //adding crystal report path to the report
                string conectionString = ConfigurationManager.ConnectionStrings["AIB_LC_DBConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(conectionString);
                conn.Open();
                crt.Load(Server.MapPath("~/AIB_REPORT/LC_Opening_Increment.rpt"));
               // crt.Load("C:/db/AIB_REPORT/LC_Opening.rpt");
                //crt.SetDatabaseLogon("da", "pass@2123","10.34.30.20","AIB_LC_DB");
                crt.SetDatabaseLogon("da", "pass@2123");
               // crt.SetDatabaseLogon("Administrator", "aibServer@1", "10.34.30.20", "AIB_LC_DB");
                //List<VW_DETAIL_LC> lclist = openingLogic.getLCbyLcNumber(txtLCNumber.Text.ToString());
               // crt.SetDataSource(lclist.AsEnumerable());
                Session["Report"] = crt;
                Session["reportCode"] = "lcNumber";
                Session["lcNumber"] = txtLCNumber.Text.ToString();
               //Server.TransferRequest("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                Response.Redirect("~/AIB_REPORT/RepPages/LC_Reports.aspx");
            }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
            protected void cancelLC(object sender, EventArgs args) { 
              
            }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
          protected void searchLCBylcNumber(object sender, EventArgs args) {
                 ComponentValidator valids = new ComponentValidator();
                valids.addComponent(txtSearch, ComponentValidator.FULL_NAME, true);
              

                if (valids.isAllComponenetValid())
                {
                    openingLogic = new LcOpeningLogic();
                    PaymentCalculater calc = new PaymentCalculater();
                    List<VW_DETAIL_LC> lcOpend = new List<VW_DETAIL_LC>();
                    string branchAbrivation = Session["BranchAbrivation"].ToString();

                    string lcNumber = txtSearch.Text;
                    string lcStart = lcNumber.Substring(0, 3);
                    if (lcStart.Equals(branchAbrivation))
                    {
                        lcOpend = openingLogic.searchLcByLC_Number(lcNumber);
                        if (lcOpend.Count == 1)
                        {
                            if (!lcOpend[0].lcStatus.Equals("OutDated"))
                            {
                                DateTime dudate = calc.calculateExpirationDueDate(lcOpend[0].lcNumber.ToString(), Convert.ToDateTime(lcOpend[0].lcOpeningDate), Convert.ToInt32(lcOpend[0].openingPeriods)).Date;
                                DateTime currentDate = DateTime.Now.Date;
                                if (DateTime.Compare(dudate, currentDate) >= 0)
                                {
                                    txtLCNumber.Text = lcOpend[0].lcNumber;
                                    txtBranchCode.Text = lcOpend[0].branchCode;
                                    txtBranchName.Text = lcOpend[0].branchName;
                                    txtCustomerName.Text = lcOpend[0].customerName;
                                    txtPermitNUmber.Text = "AIB/" + lcOpend[0].branchAbrivation + "/01-" + lcOpend[0].permitCode + "/" + lcOpend[0].permitYear.ToString().Substring(2, 2);
                                    txtSupplyer.Text = lcOpend[0].supplyerName;
                                    txtAccountNO.Text = lcOpend[0].customerAccount;
                                    numLcValue.Value = lcOpend[0].lcValue;
                                    txtOpeningPeriod.Text = lcOpend[0].openingPeriods.ToString();
                                   
                                    RadioLCConfirmed.SelectedValue = lcOpend[0].confirmationStratus.ToString();
                                    numUserLimitaition.Value = lcOpend[0].userExpirationDates;
                                    dateOpeningDate.SelectedDate = lcOpend[0].lcOpeningDate;
                                    int end = lcOpend[0].lcNumber.ToString().Length - 1;
                                    int index = lcOpend[0].lcNumber.ToString().LastIndexOf('/', end);

                                    if (lcOpend[0].permitYear.ToString().Substring(2, 2).Equals(lcOpend[0].lcNumber.Substring(index + 1, 2)))
                                    {
                                        radioPermityear.SelectedValue = "1";
                                    }
                                    else
                                    {
                                        radioPermityear.SelectedValue = "0";
                                    }
                                    combOpThrough.SelectedValue = lcOpend[0].openThroughId.ToString();
                                    combRembursing.SelectedValue = lcOpend[0].corespondenceID.ToString();
                                    //if (combRembursing.SelectedValue.Equals(string.Empty))
                                    //{
                                        //this.populateCurencyFirstTime();
                                    //}
                                    //else
                                    //{
                                        List<VW_CORRESPONDNCE_CURRENCY> currency = new List<VW_CORRESPONDNCE_CURRENCY>();
                                        currency = openingLogic.findCurrencyByCorrespondenceIDNew(Convert.ToInt32(lcOpend[0].corespondenceID));
                                        //combCurrency.DataValueField = "currencyId";
                                        //combCurrency.DataTextField = "currencyName";
                                        //combCurrency.DataSource = currency;
                                        //combCurrency.DataBind();
                                    //}
                                    txtCurrency.Text = lcOpend[0].openingCurrency.ToString();
                                    numMarginpaid.Value = Convert.ToDouble(lcOpend[0].marginPaid);
                                    dateValueDate.SelectedDate = lcOpend[0].valueDate;
                                    numCurrencyRate.Value = Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate);
                                   
                                    btnSave.Visible = true;
                                    btnUpdate.Visible = false;
                                    btnReport.Visible = false;
                                    btnRemove.Visible = false;
                                    txtSearch.ReadOnly = true;
                                    Master.UpdateMessageMethod = true;
                              }
                               else {
                                   this.errorMessage("LC is expired, can not be updated.");
                               }
                           }
                           else {
                               openingLogic.changeLCExpirationStatus(txtLCNumber.Text.ToString());
                               this.errorMessage("LC is expired, can not be updated.");
                           }
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
          /// <summary>
          /// 
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="args"></param>
          protected void searchLCIncrement(object sender, EventArgs args)
          {
                ComponentValidator valids = new ComponentValidator();
                valids.addComponent(txtSearchLcInc, ComponentValidator.FULL_NAME, true);
                valids.addComponent(txtsearchPermitYear, ComponentValidator.INTEGER_NUMBER, true);
                valids.addComponent(txtSearchPermitcode, ComponentValidator.INTEGER_NUMBER, true);

                if (valids.isAllComponenetValid())
                {
                    openingLogic = new LcOpeningLogic();
                    PaymentCalculater calc = new PaymentCalculater();
                    List<VW_LC_INCREMENTAMOUNT> lcOpend = new List<VW_LC_INCREMENTAMOUNT>();
                    string branchAbrivation = Session["BranchAbrivation"].ToString();

                    string SearchLcInc = txtSearchLcInc.Text;
                    string lcStart = SearchLcInc.Substring(0, 3);
                    if (lcStart.Equals(branchAbrivation))
                    {
                        int year = Convert.ToInt32(txtsearchPermitYear.Text);
                        string permitCode = txtSearchPermitcode.Text;
                        if (permitCode.Length == 5)
                        {
                            lcOpend = openingLogic.searchLcincrement(SearchLcInc, year, permitCode);
                            if (lcOpend.Count == 1)
                            {
                                Session["incId"] = lcOpend[0].id.ToString();
                                Session["incAmount"] = lcOpend[0].incremetAmount.ToString();
                                txtLCNumber.Text = lcOpend[0].lcNumber;
                                txtBranchCode.Text = lcOpend[0].branchCode;
                                txtBranchName.Text = lcOpend[0].branchName;
                                txtCustomerName.Text = lcOpend[0].customerName;
                                txtPermitNUmber.Text = "AIB/" + lcOpend[0].branchAbrivation + "/01-" + lcOpend[0].permitCode + "/" + lcOpend[0].permitYear.ToString().Substring(2, 2);
                                txtSupplyer.Text = lcOpend[0].supplyerName;
                                txtAccountNO.Text = lcOpend[0].customerAccount;
                                numLcValue.Value = lcOpend[0].lcValue;
                                txtOpeningPeriod.Text = lcOpend[0].openingPeriods.ToString();
                                
                                RadioLCConfirmed.SelectedValue = lcOpend[0].confirmationStratus.ToString();
                                numUserLimitaition.Value = lcOpend[0].userExpirationDates;
                                dateOpeningDate.SelectedDate = lcOpend[0].lcOpeningDate;
                                    
                                int end = lcOpend[0].lcNumber.ToString().Length - 1;
                                int index = lcOpend[0].lcNumber.ToString().LastIndexOf('/', end);

                                if (lcOpend[0].incrementPermitYear.ToString().Substring(2, 2).Equals(lcOpend[0].lcNumber.Substring(index + 1, 2)))
                                {
                                    radioPermityear.SelectedValue = "1";
                                }
                                else
                                {
                                    radioPermityear.SelectedValue = "0";
                                }
                                combOpThrough.SelectedValue = lcOpend[0].openThroughId.ToString();
                                combRembursing.SelectedValue = lcOpend[0].corespondenceID.ToString();
                               // if (combRembursing.SelectedValue.Equals(string.Empty))
                                //{
                                   // this.populateCurencyFirstTime();
                                //}
                               // else
                               // {
                                    List<VW_CORRESPONDNCE_CURRENCY> currency = new List<VW_CORRESPONDNCE_CURRENCY>();
                                    currency = openingLogic.findCurrencyByCorrespondenceIDNew(Convert.ToInt32(lcOpend[0].corespondenceID));
                                    //combCurrency.DataValueField = "currencyId";
                                    //combCurrency.DataTextField = "currencyName";
                                    //.DataSource = currency;
                                    //.DataBind();
                               // }
                               // combCurrency.SelectedIndex = combCurrency.FindItemIndexByText(lcOpend[0].openingCurrency.ToString());
                                txtCurrency.Text = lcOpend[0].openingCurrency.ToString();
                                numMarginpaid.Value = Convert.ToDouble(lcOpend[0].marginPaid);
                                dateValueDate.SelectedDate = lcOpend[0].valueDate;
                                 //
                                numCurrencyRate.Value = Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate);
                                txtPermitCode.Text = lcOpend[0].incrementPermitCode;
                                txtIncPermitNumber.Text = "AIB/" + lcOpend[0].branchAbrivation + "/01-" + lcOpend[0].incrementPermitCode + "/" + lcOpend[0].incrementPermitYear.ToString().Substring(2, 2);
                                dateIncValueDate.SelectedDate = lcOpend[0].incrementAmountValueDate;
                                numInccurencyRate.Value = lcOpend[0].incrementAmountRate;
                                numIncremetLcValue.Value = lcOpend[0].incremetAmount;

                                numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(lcOpend[0].incremetAmount)) * Convert.ToDouble(lcOpend[0].incrementAmountRate)).ToString();
                                numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(lcOpend[0].incremetAmount), lcOpend[0].openingCurrency, lcOpend[0].currencyType, Convert.ToDouble(lcOpend[0].incrementAmountRate), Convert.ToDateTime(lcOpend[0].incrementAmountValueDate)) * Convert.ToDouble(lcOpend[0].incrementAmountRate)).ToString();
                                numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(lcOpend[0].incremetAmount), lcOpend[0].openingCurrency, lcOpend[0].currencyType, Convert.ToDouble(lcOpend[0].incrementAmountRate), Convert.ToDateTime(lcOpend[0].incrementAmountValueDate)) * Convert.ToDouble(lcOpend[0].incrementAmountRate)).ToString();
                                numSwift.Text = (calc.calculateSWIFT()).ToString();
                                if (lcOpend[0].confirmationStratus.Equals("1"))
                                {
                                    numConfCommission.Text = (calc.calculateConfirmationCommission(Convert.ToDouble(lcOpend[0].incremetAmount)) * Convert.ToDouble(lcOpend[0].incrementAmountRate)).ToString();
                                }
                                else
                                {
                                    numConfCommission.Text = "0";
                                }

                                //
                                btnSave.Visible = false;
                                btnUpdate.Visible = true;
                                btnReport.Visible = false;
                                btnRemove.Visible = false;
                                txtSearch.ReadOnly = true;
                                txtSearchLcInc.ReadOnly = true;
                                txtsearchPermitYear.ReadOnly = true;
                                txtSearchPermitcode.ReadOnly = true;
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
                            this.errorMessage("Permit code must be five degit.");
                        }
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
                else {
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
            public void clearLCOpening() {

                txtLCNumber.Text = "";
                txtBranchCode.Text = "";
                txtBranchName.Text = "";
                txtCustomerName.Text = "";
                txtPermitNUmber.Text = "";
                txtPermitCode.Text = "";
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
                txtIncPermitNumber.Text = "";
                dateIncValueDate.Clear();
                numInccurencyRate.Text = "";
                numIncremetLcValue.Text = "";
                dateValueDate.Clear();
                txtCurrency.Text = "";
                txtOpeningPeriod.Text = "";
               // this.populateCurencyFirstTime();
                txtSearch.ReadOnly = false;
                txtSearchLcInc.ReadOnly = false;
                txtsearchPermitYear.ReadOnly = false;
                txtSearchPermitcode.ReadOnly = false;
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnReport.Visible = false;
                btnRemove.Visible = false;
               
            }

            protected void clearLcdetail(object sender, EventArgs args) {

                txtLCNumber.Text ="";
                txtBranchCode.Text ="";
                txtBranchName.Text ="";
                txtCustomerName.Text ="";
                txtPermitNUmber.Text = "";
                txtPermitCode.Text = "";
                txtSupplyer.Text = "";
                txtAccountNO.Text = "";
                txtOpeningPeriod.Text="";
                numUserLimitaition.Text = "";
                txtSearchLcInc.Text = "";
                txtSearch.Text = "";
                txtSearchPermitcode.Text = "";
                txtsearchPermitYear.Text = "";
                numLcValue.Text = "";
                numCurrencyRate.Text = "";
                numMarginpaid.Text = ""; 
                numExhCommission.Text = "";
                numServiceCharge.Text = "";
                numOpenCommission.Text = "";
                numSwift.Text = "";
                numConfCommission.Text = "";
                txtIncPermitNumber.Text="";
                numInccurencyRate.Text = "";
                numIncremetLcValue.Text = "";
                combOpThrough.SelectedIndex = 0;
                combRembursing.SelectedIndex = 0;
                dateValueDate.Clear();
                dateIncValueDate.Clear();
               // this.populateCurencyFirstTime();
                txtCurrency.Text = "";
                txtOpeningPeriod.Text = "";
                txtSearch.ReadOnly = false;
                txtSearchLcInc.ReadOnly = true;
                txtsearchPermitYear.ReadOnly = true;
                txtSearchPermitcode.ReadOnly = true;
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnReport.Visible = false;
                btnRemove.Visible = false;

                Button3.Visible = false;
                Button5.Visible = true;

                Master.UpdateMessageMethod = true;
            }

            protected void clearLcdetailForUpdate(object sender, EventArgs args)
            {

                txtLCNumber.Text = "";
                txtBranchCode.Text = "";
                txtBranchName.Text = "";
                txtCustomerName.Text = "";
                txtPermitNUmber.Text = "";
                txtPermitCode.Text = "";
                txtSupplyer.Text = "";
                txtAccountNO.Text = "";
                txtOpeningPeriod.Text = "";
                numUserLimitaition.Text = "";
                txtSearchLcInc.Text="";
                txtSearch.Text = "";
                txtSearchPermitcode.Text = "";
                txtsearchPermitYear.Text = "";
                numLcValue.Text = "";
                numCurrencyRate.Text = "";
                numMarginpaid.Text = "";
                numExhCommission.Text = "";
                numServiceCharge.Text = "";
                numOpenCommission.Text = "";
                numSwift.Text = "";
                numConfCommission.Text = "";
                txtIncPermitNumber.Text = "";
                numInccurencyRate.Text = "";
                numIncremetLcValue.Text = "";
                combOpThrough.SelectedIndex = 0;
                combRembursing.SelectedIndex = 0;
                dateValueDate.Clear();
                dateIncValueDate.Clear();
                //this.populateCurencyFirstTime();
                txtCurrency.Text = "";
                txtOpeningPeriod.Text = "";
                txtSearch.ReadOnly = true;
                txtSearchLcInc.ReadOnly = false;
                txtsearchPermitYear.ReadOnly = false;
                txtSearchPermitcode.ReadOnly = false;
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnReport.Visible = false;
                btnRemove.Visible = false;

                Button3.Visible = true;
                Button5.Visible = false;

                Master.UpdateMessageMethod = true;
            }
            private void initalizeDataTable() {

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