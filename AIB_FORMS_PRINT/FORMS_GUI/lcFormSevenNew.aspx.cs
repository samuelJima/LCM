using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIB_FORMS_LOGIC;
using AIB_FORMS_OB;
using AIB_FORMS_VALIDATION;
using Telerik.Web.UI;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace AIB_FORMS_PRINT.FORMS_GUI
{
    public partial class lcFormSevenNew : System.Web.UI.Page
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
                    txtBranchName.Text = branch[0].branchName.ToString();
                    txtBranchCode.Text = branch[0].branchCode.ToString();
                    DateTime current = DateTime.Now;
                    int year = current.Year;
                    string lastDigit = year.ToString().Substring(2, 2);
                    string lcCode = this.generateFourDigitNumber(BranchCode);
                    txtLCNumber.Text = branch[0].branchAbrivation + "/" + lcCode + "/" + lastDigit;
                    Session["BranchAbrivation"] = branch[0].branchAbrivation.ToString();
                    Session["lcCode"] = lcCode.ToString();
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

        private string generateFourDigitNumber(int BranchCode)
        {
            openingLogic = new LcOpeningLogic();
            int year = DateTime.Now.Year;
            string maxdigit = openingLogic.findMaximumLc_Code( BranchCode);
            //if (maxdigit == "9999")
            //{
              //  return maxdigit;
           // }
           // else { 
               int number = Convert.ToInt32(maxdigit) + 1;
                maxdigit= number.ToString();
                return maxdigit;
           /// }

        }

      /*  private void populateCurrency()
        {

            List<TBL_COUNTRY> currency = new List<TBL_COUNTRY>();
            currency = openingLogic.findAllCurrencies();
            combCurrency.DataValueField ="Country_Id";
            combCurrency.DataTextField = "Currency";
            combCurrency.DataSource = currency;
        }*/

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
        protected void radDisplayCustomer_TextChanged(object sender, EventArgs args)
        {

            openingLogic= new LcOpeningLogic();
            List<CUSTOMERBRANCH> ACCOUNT = new List<CUSTOMERBRANCH>();
            string accountNumber = txtAccountNO.Text;
            if (accountNumber != null && accountNumber != "")
            {
                if (txtAccountNO.Text.Substring(0, 2).Equals("01"))
                {
                    ACCOUNT = openingLogic.findCustomerByAccountNumber(accountNumber);

                    if (ACCOUNT == null)
                    {
                        this.errorMessage("Customer Account is not Accesible.Please contact administrator");
                    }
                    else if (ACCOUNT.Count == 1)
                    {
                        txtCustomerName.Text = ACCOUNT[0].CUSTOMERNAME.ToString();
                        txtAccountBranch.Text = ACCOUNT[0].BRANCHNAME.ToString();
                    }
                    else {
                        this.errorMessage("The Customer Account Does not exist or is not unique.please contact administrator");
                    }
                }
                else
                {

                    ACCOUNT = openingLogic.findCustomerByAccountNumber(accountNumber);
                    radioCurrencyType.SelectedValue = "BUYING";
                    if (ACCOUNT.Count == 0 || ACCOUNT == null)
                    {
                        this.errorMessage("The Customer Account Can not be found.please contact administrator");
                    }
                    else if (ACCOUNT.Count == 1)
                    {
                        txtCustomerName.Text = ACCOUNT[0].CUSTOMERNAME.ToString();
                        txtAccountBranch.Text = ACCOUNT[0].BRANCHNAME.ToString();
                    }
                    else
                    {
                        this.errorMessage("The Customer Account is not unique.please contact administrator");
                    }

                }
            }
            else {
                this.errorMessage("Customer Account Can not be empty");
            }
          

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

                               txtPermitNUmber.Text = "AIB/" + branchAbrivation + "/01-" + permitCode + "/" + year.ToString().Substring(2, 2);
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
            valids.addComponent(numLcValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numCurrencyRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(txtAccountNO, ComponentValidator.INTEGER_NUMBER, true);
            valids.addComponent(txtCustomerName, ComponentValidator.FULL_NAME, true);
            if (valids.isAllComponenetValid())
            {
                if (txtAccountNO.Text.Substring(0, 2).Equals("01"))
                {
                    string confirmationStatus = RadioLCConfirmed.SelectedValue;
                    numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(numLcValue.Text))*(numCurrencyRate.Value)).ToString();
                    numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)*(Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                    numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                    numSwift.Text = (calc.calculateSWIFT()).ToString();

                    if (confirmationStatus.Equals("1"))
                    {
                        double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                        if (forienConfirmationComm > 0)
                        {
                            numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                    numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                    numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                    numSwift.Text = (calc.calculateSWIFT()).ToString();

                    if (confirmationStatus.Equals("1"))
                    {
                        double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                        if (forienConfirmationComm > 0)
                        {
                            numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
        protected void saveLCOpening(object sender, EventArgs args) {

            openingLogic = new LcOpeningLogic();
            ComponentValidator valids = new ComponentValidator();
             valids.addComponent(txtLCNumber, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(dateValueDate, ComponentValidator.GC_DATE, true);
            //valids.addComponent(combRembursing, ComponentValidator.INTEGER_NUMBER, true);
            //valids.addComponent(combOpThrough, ComponentValidator.INTEGER_NUMBER, true);
            valids.addComponent(txtSupplyer, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(combCurrency, ComponentValidator.INTEGER_NUMBER, true);
            valids.addComponent(txtAccountNO, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(txtCustomerName, ComponentValidator.FULL_NAME, true);
            valids.addComponent(numLcValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numMarginpaid, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numCurrencyRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(txtPermitCode, ComponentValidator.INTEGER_NUMBER, true);
            valids.addComponent(numTollerance, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numUserLimitaition, ComponentValidator.INTEGER_NUMBER, true);

            if (valids.isAllComponenetValid())
            {
                if (Session["Branch"] != null && Session["UserID"]!=null)
                {
                    string LCcode =string.Empty;
                    List<TBL_Branch> branch = new List<TBL_Branch>();
                    int BranchCode = Convert.ToInt32(Session["Branch"].ToString());
                    branch = openingLogic.findBranchByBranchCode(BranchCode);
                    if (branch.Count == 1)
                    {
                        txtBranchName.Text = branch[0].branchName.ToString();
                        txtBranchCode.Text = branch[0].branchCode.ToString();
                        DateTime current = DateTime.Now;
                        int year = current.Year;
                        string lastDigit = year.ToString().Substring(2, 2);
                        string lcCode = this.generateFourDigitNumber(BranchCode);
                        txtLCNumber.Text = branch[0].branchAbrivation + "/" + lcCode + "/" + lastDigit;
                        Session["BranchAbrivation"] = branch[0].branchAbrivation.ToString();
                        LCcode = lcCode.ToString();


                        // string LCcode = Session["lcCode"].ToString();
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
                        if (!LCcode.Equals(string.Empty))
                        {
                            string permitCode = txtPermitCode.Text;
                            if (openingLogic.checkPermitCode(permitCode, permitYear))
                            {
                                bool result = openingLogic.saveLCDetail(txtLCNumber.Text, LCcode, dateValueDate.SelectedDate, txtPermitCode.Text, permitYear, Convert.ToDouble(numLcValue.Text),
                                                                        combCurrency.SelectedItem.Text, Convert.ToDouble(numCurrencyRate.Text), Convert.ToDouble(numMarginpaid.Text),
                                                                        Convert.ToInt32(combRembursing.SelectedValue), Convert.ToInt32(combOpThrough.SelectedValue), txtSupplyer.Text,Convert.ToInt32(branch[0].branchid), txtAccountNO.Text, "Opened", openingDate, txtCustomerName.Text
                                                                        , RadioLCConfirmed.SelectedValue.ToString(), radioCurrencyType.SelectedValue, Convert.ToDouble(numTollerance.Value), txtAccountBranch.Text, Convert.ToInt32(comboPeriod.SelectedValue), Convert.ToInt32(numUserLimitaition.Value), Convert.ToInt32(Session["UserID"].ToString()));

                                if (result)
                                {
                                    btnSave.Visible = false;
                                    btnReport.Visible = true;
                                    this.postiveMessage("Letter Of Credit Saved Succefully!");
                                }
                                else
                                {
                                    this.errorMessage("Letter Of Credit Not Saved Succefully, Please Contact System Adminstrator");
                                }
                            }
                            else
                            {

                                this.errorMessage("Permit code already exists, Please Add another premic code.");
                            }
                        }
                        else
                        {
                            this.errorMessage("Letter Of Credit Can not be empty, Please Contact System Adminstrator");
                        }
                    }
                    else {

                        this.errorMessage("User must have a branch, Please Contact System Adminstrator");
                    }
                 
                }
                else {
                    this.errorMessage("Letter Of Credit Not Saved Succefully, Please Contact System Adminstrator");
                }
            }
           }

        protected void updateLCOpening(object source , EventArgs arg) { 
        
            openingLogic = new LcOpeningLogic();
            ComponentValidator valids = new ComponentValidator();
             valids.addComponent(txtLCNumber, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(dateValueDate, ComponentValidator.GC_DATE, true);
            //valids.addComponent(combRembursing, ComponentValidator.INTEGER_NUMBER, true);
            //valids.addComponent(combOpThrough, ComponentValidator.INTEGER_NUMBER, true);
            valids.addComponent(txtSupplyer, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(combCurrency, ComponentValidator.INTEGER_NUMBER, true);
            valids.addComponent(txtAccountNO, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(txtCustomerName, ComponentValidator.FULL_NAME, true);
            valids.addComponent(numLcValue, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numMarginpaid, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numCurrencyRate, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(txtPermitCode, ComponentValidator.INTEGER_NUMBER, true);
            valids.addComponent(numTollerance, ComponentValidator.FLOAT_NUMBER, true);
            valids.addComponent(numUserLimitaition, ComponentValidator.INTEGER_NUMBER, true);
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
                            bool result = openingLogic.changeLC_Detail(txtLCNumber.Text, dateValueDate.SelectedDate, txtPermitCode.Text, permitYear, Convert.ToDouble(numLcValue.Text),
                                                                   combCurrency.SelectedItem.Text, Convert.ToDouble(numCurrencyRate.Text), Convert.ToDouble(numMarginpaid.Text),
                                                                   Convert.ToInt32(combRembursing.SelectedValue), Convert.ToInt32(combOpThrough.SelectedValue), txtSupplyer.Text, txtAccountNO.Text, txtCustomerName.Text, RadioLCConfirmed.SelectedValue.ToString(), radioCurrencyType.SelectedValue, Convert.ToDouble(numTollerance.Value), txtAccountBranch.Text,
                                                                    Convert.ToInt32(comboPeriod.SelectedValue), Convert.ToInt32(numUserLimitaition.Value));
                            if (result)
                            {
                                btnUpdate.Visible = false;
                                btnReport.Visible = true;
                                this.postiveMessage("Letter Of Credit Updated Succefully!");
                            }
                            else
                            {
                                this.errorMessage("Letter Of Credit not Updated Succefully, Please Contact Adminstrator");
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
        /// <param name="e"></param>
        protected void populateCorespCurrency(object sender, EventArgs e)
        {

            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtAccountNO, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(txtPermitCode, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(txtCustomerName, ComponentValidator.FULL_NAME, true);
            //valids.addComponent(combRembursing, ComponentValidator.INTEGER_NUMBER, true);
            //valids.addComponent(combOpThrough, ComponentValidator.INTEGER_NUMBER, true);
            if (valids.isAllComponenetValid())
            {
                List<VW_CORRESPONDNCE_CURRENCY> currency = new List<VW_CORRESPONDNCE_CURRENCY>();
                openingLogic = new LcOpeningLogic();
                PaymentCalculater calc = new PaymentCalculater();
                //foreach (ListItem item in combRembursing.Items)
               // {
                    ListItem item = combRembursing.Items[combRembursing.SelectedIndex];
                    bool breakStatus = false;
                    if (item.Selected)
                    {
                        if (item.Value.Equals(string.Empty))
                        {
                            this.populateCurencyFirstTime();

                            string currencyCode = combCurrency.SelectedItem.Text;
                            if (!currencyCode.Equals(string.Empty))
                            {
                                if (dateValueDate.SelectedDate != null)
                                {
                                    string currencytype = radioCurrencyType.SelectedValue;
                                    openingLogic = new LcOpeningLogic();
                                    DateTime valueDate = Convert.ToDateTime(dateValueDate.SelectedDate);
                                    List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
                                    currencyRate = openingLogic.findCurrencyByValueDat(currencytype, currencyCode, valueDate);
                                    if (currencyRate.Count == 1)
                                    {
                                        numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                                        if (!numLcValue.Text.Equals(string.Empty))
                                        {
                                            if (txtAccountNO.Text.Substring(0, 2).Equals("01"))
                                            {
                                                string confirmationStatus = RadioLCConfirmed.SelectedValue;
                                                numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(numLcValue.Text)) * (numCurrencyRate.Value)).ToString();
                                                numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                                numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                                numSwift.Text = (calc.calculateSWIFT()).ToString();

                                                if (confirmationStatus.Equals("1"))
                                                {
                                                    double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                                    if (forienConfirmationComm > 0)
                                                    {
                                                        numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                                                numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                                numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                                numSwift.Text = (calc.calculateSWIFT()).ToString();

                                                if (confirmationStatus.Equals("1"))
                                                {
                                                    double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                                    if (forienConfirmationComm > 0)
                                                    {
                                                        numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                                        else
                                        {
                                            numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                                        }
                                    }
                                    else
                                    {
                                        this.clearByCurencyType();
                                        this.errorMessage("There must be only one currency Rate,please contact the administrator.");
                                    }
                                }
                                else
                                {

                                    ComponentValidator valids1 = new ComponentValidator();
                                    valids1.addComponent(dateValueDate, ComponentValidator.GC_DATE, true);
                                    if (valids1.isAllComponenetValid())
                                    {

                                    }
                                }

                            }
                            else {
                                this.errorMessage("At list one currency must be selected");
                            }
                        }
                        else
                        {
                            int corsId = Convert.ToInt32(item.Value);
                            currency = openingLogic.findCurrencyByCorrespondenceIDNew(corsId);
                            combCurrency.DataValueField = "currencyId";
                            combCurrency.DataTextField = "currencyName";
                            combCurrency.DataSource = currency;
                            combCurrency.DataBind();

                            string currencyCode = combCurrency.SelectedItem.Text;
                            if (!currencyCode.Equals(string.Empty))
                            {
                                if (dateValueDate.SelectedDate != null)
                                {
                                    string currencytype = radioCurrencyType.SelectedValue;
                                    openingLogic = new LcOpeningLogic();
                                    DateTime valueDate = Convert.ToDateTime(dateValueDate.SelectedDate);
                                    List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
                                    currencyRate = openingLogic.findCurrencyByValueDat(currencytype, currencyCode, valueDate);
                                    if (currencyRate.Count == 1)
                                    {
                                        numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                                        if (!numLcValue.Text.Equals(string.Empty))
                                        {
                                            if (txtAccountNO.Text.Substring(0, 2).Equals("01"))
                                            {
                                                string confirmationStatus = RadioLCConfirmed.SelectedValue;
                                                numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(numLcValue.Text)) * (numCurrencyRate.Value)).ToString();
                                                numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                                numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                                numSwift.Text = (calc.calculateSWIFT()).ToString();

                                                if (confirmationStatus.Equals("1"))
                                                {
                                                    double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                                    if (forienConfirmationComm > 0)
                                                    {
                                                        numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                                    }
                                                    else
                                                    {
                                                        this.errorMessage("Confirmation commision tarif can not be accessed please contact Administrater.");
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
                                                numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                                numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                                numSwift.Text = (calc.calculateSWIFT()).ToString();

                                                if (confirmationStatus.Equals("1"))
                                                {
                                                    double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                                    if (forienConfirmationComm > 0)
                                                    {
                                                        numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                                    }
                                                    else
                                                    {
                                                        this.errorMessage("Confirmation commision tarif can not be accessed please contact Administrater.");
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
                                            this.clearByCurencyType();
                                            numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                                        }
                                    }
                                    else
                                    {
                                        this.clearByCurencyType();
                                        this.errorMessage("There must be only one currency Rate.please contact the administrator.");
                                    }
                                }
                            
                            }
                            else
                            {
                                this.errorMessage("At list one currency must be selected");
                            }
                            
                        }
                        breakStatus = true;
                                              
                    }

                 
                //}
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void openingPeriodSelectedIndexChanged(object sender, EventArgs e)
        {

            ComponentValidator valids = new ComponentValidator();           
            valids.addComponent(txtPermitCode, ComponentValidator.TEXT_AND_NUMBER, true);          
            if (valids.isAllComponenetValid())
            {
                List<VW_CORRESPONDNCE_CURRENCY> currency = new List<VW_CORRESPONDNCE_CURRENCY>();
                openingLogic = new LcOpeningLogic();
                PaymentCalculater calc = new PaymentCalculater();
                string currencyCode = combCurrency.SelectedItem.Text;
                        if (!currencyCode.Equals(string.Empty))
                        {
                            if (dateValueDate.SelectedDate != null)
                            {
                                string currencytype = radioCurrencyType.SelectedValue;
                                openingLogic = new LcOpeningLogic();
                                DateTime valueDate = Convert.ToDateTime(dateValueDate.SelectedDate);
                                List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
                                currencyRate = openingLogic.findCurrencyByValueDat(currencytype, currencyCode, valueDate);
                                if (currencyRate.Count == 1)
                                {
                                    numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                                    if (!numLcValue.Text.Equals(string.Empty))
                                    {
                                        if (txtAccountNO.Text.Substring(0, 2).Equals("01"))
                                        {
                                            string confirmationStatus = RadioLCConfirmed.SelectedValue;
                                            numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(numLcValue.Text)) * (numCurrencyRate.Value)).ToString();
                                            numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                            numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                            numSwift.Text = (calc.calculateSWIFT()).ToString();

                                            if (confirmationStatus.Equals("1"))
                                            {
                                                double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                                if (forienConfirmationComm > 0)
                                                {
                                                    numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                                            numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                            numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                            numSwift.Text = (calc.calculateSWIFT()).ToString();

                                            if (confirmationStatus.Equals("1"))
                                            {
                                                double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                                if (forienConfirmationComm > 0)
                                                {
                                                    numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                                    else
                                    {
                                        numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                                    }
                                }
                                else
                                {
                                    this.clearByCurencyType();
                                    this.errorMessage("There must be only one currency Rate,please contact the administrator.");
                                }
                            }
                            else
                            {

                                ComponentValidator valids1 = new ComponentValidator();
                                valids1.addComponent(dateValueDate, ComponentValidator.GC_DATE, true);
                                if (valids1.isAllComponenetValid())
                                {

                                }
                            }

                        }
                        else
                        {
                            this.errorMessage("At list one currency must be selected");
                        }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void curencyTypeSelectedIndexChanged(object sender, EventArgs args)
        {
            PaymentCalculater calc = new PaymentCalculater();
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtAccountNO, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(txtPermitCode, ComponentValidator.TEXT_AND_NUMBER, true);
            valids.addComponent(txtCustomerName, ComponentValidator.FULL_NAME, true);
            if (valids.isAllComponenetValid())
            {
                string currencyCode = combCurrency.SelectedItem.Text;
                if (!currencyCode.Equals(string.Empty))
                {
                    if (dateValueDate.SelectedDate != null)
                    {
                        string currencytype = radioCurrencyType.SelectedValue;
                        openingLogic = new LcOpeningLogic();
                        DateTime valueDate = Convert.ToDateTime(dateValueDate.SelectedDate);
                        List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
                        currencyRate = openingLogic.findCurrencyByValueDat(currencytype, currencyCode, valueDate);
                        if (currencyRate.Count == 1)
                        {
                            numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                            if (!numLcValue.Text.Equals(string.Empty))
                            {
                                if (txtAccountNO.Text.Substring(0, 2).Equals("01"))
                                {
                                    string confirmationStatus = RadioLCConfirmed.SelectedValue;
                                    numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(numLcValue.Text)) * (numCurrencyRate.Value)).ToString();
                                    numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                    numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                    numSwift.Text = (calc.calculateSWIFT()).ToString();

                                    if (confirmationStatus.Equals("1"))
                                    {
                                        double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                        if (forienConfirmationComm > 0)
                                        {
                                            numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                                    numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                    numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                    numSwift.Text = (calc.calculateSWIFT()).ToString();

                                    if (confirmationStatus.Equals("1"))
                                    {
                                        double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                        if (forienConfirmationComm > 0)
                                        {
                                            numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                                numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                            }
                        }
                        else
                        {
                            this.clearByCurencyType();
                            this.errorMessage("There must be only one currency Rate.please contact the administrator.");
                        }
                    }
                  
                }
                else
                {
                    this.errorMessage("At list one currency must be selected");
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
            protected void currencySelectedIndexChanged(object sender, EventArgs args)
            {

                PaymentCalculater calc = new PaymentCalculater();
                ComponentValidator valids = new ComponentValidator();
                valids.addComponent(txtAccountNO, ComponentValidator.TEXT_AND_NUMBER, true);
                valids.addComponent(txtPermitCode, ComponentValidator.TEXT_AND_NUMBER, true);
                valids.addComponent(txtCustomerName, ComponentValidator.FULL_NAME, true);
                if (valids.isAllComponenetValid())
                {
                    string currencyCode = combCurrency.SelectedItem.Text;
                    if (!currencyCode.Equals(string.Empty))
                    {
                        if (dateValueDate.SelectedDate != null)
                        {
                            string currencytype = radioCurrencyType.SelectedValue;
                            openingLogic = new LcOpeningLogic();
                            DateTime valueDate = Convert.ToDateTime(dateValueDate.SelectedDate);
                            List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
                            currencyRate = openingLogic.findCurrencyByValueDat(currencytype, currencyCode, valueDate);
                            if (currencyRate.Count == 1)
                            {
                                numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                                if (!numLcValue.Text.Equals(string.Empty))
                                {
                                    if (txtAccountNO.Text.Substring(0, 2).Equals("01"))
                                    {
                                        string confirmationStatus = RadioLCConfirmed.SelectedValue;
                                        numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(numLcValue.Text)) * (numCurrencyRate.Value)).ToString();
                                        numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                        numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                        numSwift.Text = (calc.calculateSWIFT()).ToString();

                                        if (confirmationStatus.Equals("1"))
                                        {
                                            double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                            if (forienConfirmationComm > 0)
                                            {
                                                numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                                        numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                        numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                        numSwift.Text = (calc.calculateSWIFT()).ToString();

                                        if (confirmationStatus.Equals("1"))
                                        {
                                            double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                            if (forienConfirmationComm > 0)
                                            {
                                                numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                                    numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                                }
                            }
                            else
                            {
                                this.clearByCurencyType();
                                this.errorMessage("There must be only one currency Rate.please contact the administrator.");
                            }
                        }
                       
                    }
                    else
                    {
                        this.errorMessage("At list one currency must be selected");
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
                valids.addComponent(txtSupplyer, ComponentValidator.TEXT_AND_NUMBER, true);
                if (valids.isAllComponenetValid())
                {
                    PaymentCalculater calc = new PaymentCalculater();
                    openingLogic = new LcOpeningLogic();
                    string currencytype = radioCurrencyType.SelectedValue;
                    string currencyCode = combCurrency.SelectedItem.Text;
                    DateTime valueDate = Convert.ToDateTime(dateValueDate.SelectedDate);
                    List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
                    currencyRate = openingLogic.findCurrencyByValueDat(currencytype, currencyCode, valueDate);
                    if (currencyRate.Count == 1)
                    {
                        numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                        numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
                        if (!numLcValue.Text.Equals(string.Empty))
                        {
                            if (txtAccountNO.Text.Substring(0, 2).Equals("01"))
                            {
                                string confirmationStatus = RadioLCConfirmed.SelectedValue;
                                numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(numLcValue.Text)) * (numCurrencyRate.Value)).ToString();
                                numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                numSwift.Text = (calc.calculateSWIFT()).ToString();

                                if (confirmationStatus.Equals("1"))
                                {
                                    double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                    if (forienConfirmationComm > 0)
                                    {
                                        numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                                numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                                numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(numLcValue.Text), combCurrency.SelectedItem.Text, radioCurrencyType.SelectedValue, Convert.ToDouble(numCurrencyRate.Value), Convert.ToDateTime(dateValueDate.SelectedDate)) * (numCurrencyRate.Value)).ToString();
                                numSwift.Text = (calc.calculateSWIFT()).ToString();

                                if (confirmationStatus.Equals("1"))
                                {
                                    double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                                    if (forienConfirmationComm > 0)
                                    {
                                        numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
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
                            numCurrencyRate.Value = Convert.ToDouble(currencyRate[0].currencyRate);
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
            protected void confiramationInsexChanged(object sender, EventArgs args) {

                PaymentCalculater calc = new PaymentCalculater();
                string confirmationStatus = RadioLCConfirmed.SelectedValue;
                if (confirmationStatus.Equals("1"))
                {
                    if (!numLcValue.Text.Equals(string.Empty))
                    {
                        double forienConfirmationComm = calc.calculateConfirmationCommission(Convert.ToDouble(numLcValue.Text));
                        if (forienConfirmationComm > 0)
                        {
                            numConfCommission.Text = (forienConfirmationComm * (numCurrencyRate.Value) * (Convert.ToInt32(comboPeriod.SelectedValue))).ToString();
                        }
                        else
                        {
                            this.errorMessage("Confirmation commision tarif can not be accessed, please contact Administrater");
                        }
                    }
                }
                else
                {
                    numConfCommission.Value = 0;
                }
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

                            txtPermitNUmber.Text = "AIB/" + branchAbrivation + "/01-" + permitCode + "/" + year.ToString().Substring(2, 2);
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
                crt.Load(Server.MapPath("~/AIB_REPORT/LC_Opening.rpt"));
               // crt.Load("C:/db/AIB_REPORT/LC_Opening.rpt");
                //crt.SetDatabaseLogon("da", "pass@2123","10.34.30.20","AIB_LC_DB");
                crt.SetDatabaseLogon("da", "pass@2123");
               // crt.SetDatabaseLogon("Administrator", "aibServer@1", "10.34.30.20", "AIB_LC_DB");
                //List<VW_DETAIL_LC> lclist = openingLogic.getLCbyLcNumber(txtLCNumber.Text.ToString());
               // crt.SetDataSource(lclist.AsEnumerable());
                Session["Report"] = crt;
                Session["reportCode"] = "lcNumber";
                Session["lcNumber"] = txtLCNumber.Text.ToString();
               //Server.Transfer("~/AIB_REPORT/RepPages/LC_Reports.aspx");
               /* string url = "~/AIB_REPORT/RepPages/LC_Reports.aspx";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(),"script", sb.ToString());
             */ 
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
        /// <param name="message"></param>
            public void errorMessage(string message) {

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
                                       txtPermitCode.Text = lcOpend[0].permitCode;
                                       txtSupplyer.Text = lcOpend[0].supplyerName;
                                       txtAccountNO.Text = lcOpend[0].customerAccount;
                                       numLcValue.Value = lcOpend[0].lcValue;
                                       comboPeriod.SelectedIndex = comboPeriod.FindItemIndexByValue(lcOpend[0].openingPeriods.ToString());
                                       numUserLimitaition.Value = lcOpend[0].userExpirationDates;
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
                                       RadioLCConfirmed.SelectedValue = lcOpend[0].confirmationStratus.ToString();
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
                                       combCurrency.SelectedItem.Text = lcOpend[0].openingCurrency;
                                       numMarginpaid.Value = Convert.ToDouble(lcOpend[0].marginPaid);
                                       numExhCommission.Text = (calc.calculateExchangeCommission(Convert.ToDouble(lcOpend[0].lcValue)) * Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate)).ToString();
                                       numServiceCharge.Text = (calc.calculateServiceCharge(Convert.ToDouble(lcOpend[0].lcValue), lcOpend[0].openingCurrency, lcOpend[0].currencyType, Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate), Convert.ToDateTime(lcOpend[0].valueDate)) * Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate)).ToString();
                                       numOpenCommission.Text = (calc.calculateOpeningCommission(Convert.ToDouble(lcOpend[0].lcValue), lcOpend[0].openingCurrency, lcOpend[0].currencyType, Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate), Convert.ToDateTime(lcOpend[0].valueDate)) * Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate)).ToString();
                                       numSwift.Text = (calc.calculateSWIFT()).ToString();
                                       if (lcOpend[0].confirmationStratus.Equals("1"))
                                       {
                                           numConfCommission.Text = (calc.calculateConfirmationCommission(Convert.ToDouble(lcOpend[0].lcValue)) * Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate) * lcOpend[0].openingPeriods).ToString();
                                       }
                                       else
                                       {
                                           numConfCommission.Text = "0";
                                       }

                                       dateValueDate.SelectedDate = lcOpend[0].valueDate;
                                       numCurrencyRate.Value = Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate);
                                       //http://localhost:4659/FORMS_GUI/lcFormSevenNew.aspx.cs
                                       btnSave.Visible = false;
                                       btnUpdate.Visible = true;
                                       btnReport.Visible = true;
                                       btnAddnew.Visible = false;
                                       btnRemove.Visible = true;
                                       txtSearch.ReadOnly = true;
                                       Master.UpdateMessageMethod = true;
                                   }
                                   else
                                   {
                                       openingLogic.changeLCExpirationStatus(txtLCNumber.Text.ToString());
                                       this.errorMessage("LC is expired, can not be updated.");
                                   }
                               }
                               else
                               {
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
            protected void addNewLc(object sender, EventArgs args)
            { 
                   openingLogic = new LcOpeningLogic();
            List<TBL_Branch> branch = new List<TBL_Branch>();
            if (Session["Branch"] != null)
            {

                int BranchCode = Convert.ToInt32(Session["Branch"].ToString());
                branch = openingLogic.findBranchByBranchCode(BranchCode);
                if (branch.Count == 1)
                {
                    txtBranchName.Text = branch[0].branchName.ToString();
                    txtBranchCode.Text = branch[0].branchCode.ToString();
                    DateTime current = DateTime.Now;
                    int year = current.Year;
                    string lastDigit = year.ToString().Substring(2, 2);
                    string lcCode = this.generateFourDigitNumber(BranchCode);
                    txtLCNumber.Text = branch[0].branchAbrivation + "/" + lcCode + "/" + lastDigit;
                    Session["BranchAbrivation"] = branch[0].branchAbrivation.ToString();
                    Session["lcCode"] = lcCode.ToString();
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
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnReport.Visible = false;
                btnAddnew.Visible = false;
            }
            else {
                this.errorMessage("New LC can not be generated,Please Contact System Adminstrator");
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
                dateValueDate.Clear();
                this.populateCurencyFirstTime();
                txtSearch.ReadOnly = false;
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                btnReport.Visible = false;
                btnRemove.Visible = false;
                btnAddnew.Visible = true;
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
                numLcValue.Text = "";
                numCurrencyRate.Text = "";
                numMarginpaid.Text = ""; 
                numExhCommission.Text = "";
                numServiceCharge.Text = "";
                numOpenCommission.Text = "";
                numSwift.Text = "";
                txtSearch.Text = "";
                comboPeriod.SelectedIndex = 0;
                numUserLimitaition.Text = "";
                numConfCommission.Text = "";
                combOpThrough.SelectedIndex = 0;
                combRembursing.SelectedIndex = 0;
                dateValueDate.Clear();
                this.populateCurencyFirstTime();
                txtSearch.ReadOnly = false;
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                btnReport.Visible = false;
                btnRemove.Visible = false;
                btnAddnew.Visible = true;


                Master.UpdateMessageMethod = true;
            }
            private void initalizeDataTable() {

                DataTable lcTable = new DataTable();

                //lcTable.Columns.Add()

            
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