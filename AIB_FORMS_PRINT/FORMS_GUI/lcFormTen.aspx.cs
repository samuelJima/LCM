using AIB_FORMS_LOGIC;
using AIB_FORMS_OB;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIB_FORMS_PRINT.FORMS_GUI
{
    public partial class lcFormTen : System.Web.UI.Page
    {
        LcOpeningLogic openingLogic;
        protected void Page_Load(object sender, EventArgs e)
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
            lblCurrentDate.Text = DateTime.Now.ToShortDateString();
        }
        protected void searchLCBylcNumber(object sender, EventArgs args)
        {

            openingLogic = new LcOpeningLogic();
            PaymentCalculater calc = new PaymentCalculater();
            List<VW_DETAIL_LC> lcOpend = new List<VW_DETAIL_LC>();
            string branchAbrivation = Session["BranchAbrivation"].ToString();
           
            string lcNumber = txtSearch.Text;
            string lcStart = lcNumber.Substring(0, 3);
            if (lcStart.Equals(branchAbrivation))
            {
                lcOpend = openingLogic.searchLcCancelationByLC_Number(lcNumber);
                if (lcOpend.Count == 1)
                {
                    if (!openingLogic.isThereEcessAmount(lcOpend[0].lcNumber))
                    {
                        txtLCNumber.Text = lcOpend[0].lcNumber;
                        txtBranchCode.Text = lcOpend[0].branchCode;
                        txtBranchName.Text = lcOpend[0].branchName;
                        txtCustomerName.Text = lcOpend[0].customerName;
                        txtPermitNUmber.Text = "AIB/" + lcOpend[0].branchAbrivation + "/01-" + lcOpend[0].permitCode + "/" + lcOpend[0].permitYear.ToString().Substring(2, 2);
                        txtOpeningPeriod.Text = lcOpend[0].openingPeriods.ToString();
                        numUserLimitaition.Value = lcOpend[0].userExpirationDates;
                        dateOpeningDate.SelectedDate = lcOpend[0].lcOpeningDate;
                        txtSupplyer.Text = lcOpend[0].supplyerName;
                        txtAccountNO.Text = lcOpend[0].customerAccount;
                        numLcValue.Value = lcOpend[0].lcValue;


                        this.populateRembursingBank();
                        this.populateOpenedThrough();
                        combOpThrough.SelectedValue = lcOpend[0].openThroughId.ToString();
                        combRembursing.SelectedValue = lcOpend[0].corespondenceID.ToString();
                        //this.populateCurencyFirstTime();

                        txtCurrency.Text= lcOpend[0].openingCurrency;
                        dateValueDate.SelectedDate = lcOpend[0].valueDate;
                        numCurrencyRate.Value = Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate);
                        numUnutilizedAmount.Value = calc.calculateUnutlizedAmount(Convert.ToDouble(lcOpend[0].lcValue), lcOpend[0].lcNumber.ToString()) * numCurrencyRate.Value;
                        numPostThisAmount.Value = calc.calculatePostThisAmount(Convert.ToDouble(lcOpend[0].marginPaid), Convert.ToDouble(lcOpend[0].lcValue), lcOpend[0].lcNumber.ToString()) * numCurrencyRate.Value;

                       


                    }
                    else
                    {
                        this.errorMessage("The letter of credit with excess amount can not be cancelled.");
                    }
                }
                else
                {
                    this.errorMessage("The letter of credit can not be cancelled or doesn't exist");

                }
            }
            else
            {
                this.errorMessage("The letter of credit does not belong to this branch.");

            }
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
          
            txtPermitNUmber.Text = "";
            txtSupplyer.Text = "";
            txtSearch.Text = "";
        
            numCurrencyRate.Text = "";
     
            txtOpeningPeriod.Text = "";
            txtCurrency.Text = "";
            combOpThrough.ClearSelection();
            combRembursing.ClearSelection();
          
            dateValueDate.Clear();
        
            numLcValue.Text = "";
          
         
            txtSearch.Text = "";
            txtSearch.ReadOnly = false;
            Button5.Visible = true;
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
        private void populateCurencyFirstTime()
        {

            List<TBL_CURRENCY> currency = new List<TBL_CURRENCY>();
            openingLogic = new LcOpeningLogic();
            int corsId = Convert.ToInt32(combRembursing.SelectedValue);
            currency = openingLogic.findCurrencyByCorrespondenceID(corsId);
            //combCurrency.DataValueField = "currencyId";
            //combCurrency.DataTextField = "currencyName";
            //combCurrency.DataSource = currency;
            //combCurrency.DataBind();
        }
        protected void bindOpeningReport(object sender, EventArgs args)
        {
            openingLogic = new LcOpeningLogic();
            ReportDocument crt = new ReportDocument();
            //adding crystal report path to the report
            List<TBL_CURRENCY> currency = new List<TBL_CURRENCY>();
                    string currencyCode = txtCurrency.Text;
                    currency = openingLogic.findCurrencyByCurrencyCode(currencyCode);
                    if (currency.Count == 1)
                    {
                    bool result = openingLogic.changeLCCancelationStatus(txtLCNumber.Text.ToString());
                     if (result)
                     {
                         string aibCorespondentAccount = openingLogic.findCorspondentAccount(Convert.ToInt32(combRembursing.SelectedValue), Convert.ToInt32(currency[0].currencyId));
                         Session["AIBACCOUNT"] = aibCorespondentAccount.ToString(); 

                         crt.Load(Server.MapPath("~/AIB_REPORT/LC_Cancelation.rpt"));
                         crt.SetDatabaseLogon("da", "pass@2123");
                         // crt.SetDatabaseLogon("Administrator", "seyoumeScholar1");
                         List<VW_DETAIL_LC> lclist = openingLogic.getLCbyLcNumber(txtLCNumber.Text.ToString());
                         // crt.SetDataSource(lclist.AsEnumerable());
                         Session["Report"] = crt;
                         Session["reportCode"] = "lcCancelation";
                         Session["lcNumber"] = txtLCNumber.Text.ToString();
                         //Server.TransferRequest("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                         Response.Redirect("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                     }
                }
                 else
                    {
                        this.errorMessage("Currency Code must be uniqe.please contact Administrator.");
                    }
        }
        public void errorMessage(string message)
        {

            this.MessageDivComfirmation = true;
            this.MessageRadTickerComf = true;
            this.MessageConfirmationBg = Color.Red;
            this.MessageLabel = message;

        }

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