using AIB_FORMS_LOGIC;
using AIB_FORMS_OB;
using AIB_FORMS_VALIDATION;
using CrystalDecisions.CrystalReports.Engine;
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
    public partial class lcFormNine : System.Web.UI.Page
    {
        LcOpeningLogic openingLogic;
        LC_Advice_logic adviceLogic;
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
                RadGrid1.MasterTableView.GetColumn("descripancyStatus").Visible = false;
                lblCurrentDate.Text = DateTime.Now.ToShortDateString();
                foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                {
                    if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled")
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
                RadGrid1.MasterTableView.GetColumn("descripancyStatus").Visible = false;
                lblCurrentDate.Text = DateTime.Now.ToShortDateString();
                foreach (GridDataItem items in RadGrid1.MasterTableView.Items)
                {
                    if (items["status"].Text == "INACTIVE" || items["status"].Text == "Setteled")
                    {
                        int index = items.ItemIndex;
                        RadGrid1.MasterTableView.Items[index].Visible = false;

                    }
                }
            
            }
        }
        protected void searchLCBylcNumber(object sender, EventArgs args)
        {
              ComponentValidator valids = new ComponentValidator();
               
               valids.addComponent(txtSearch, ComponentValidator.FULL_NAME, true);
               if (valids.isAllComponenetValid())
               {
                   adviceLogic = new LC_Advice_logic();
                   PaymentCalculater calc = new PaymentCalculater();
                   List<VW_LC_WITH_ADVICE_SIGHT> lcOpend = new List<VW_LC_WITH_ADVICE_SIGHT>();
                   string branchAbrivation = Session["BranchAbrivation"].ToString();

                   string lcNumber = txtSearch.Text;
                   string lcStart = lcNumber.Substring(0, 3);
                   if (lcStart.Equals(branchAbrivation))
                   {

                       lcOpend = adviceLogic.searchLcSetteledByLC_Number(lcNumber);
                       if (lcOpend.Count >= 1)
                       {
                           if (!lcOpend[0].lcStatus.Equals("OutDated"))
                           {
                               DateTime dudate = calc.calculateExpirationDueDate(lcOpend[0].lcNumber.ToString(), Convert.ToDateTime(lcOpend[0].lcOpeningDate), Convert.ToInt32(lcOpend[0].openingPeriods)).Date;
                               DateTime currentDate = DateTime.Now.Date;
                               if (DateTime.Compare(dudate, currentDate) >= 0)
                               {
                                   Session["MarginPercent"] = lcOpend[0].marginPaid.ToString();
                                   txtLCNumber.Text = lcOpend[0].lcNumber;
                                   Session["lcNumber"] = lcOpend[0].lcNumber;
                                   txtBranchCode.Text = lcOpend[0].branchCode.ToString();
                                   txtBranchName.Text = lcOpend[0].branchName;
                                   txtCustomerName.Text = lcOpend[0].customerName;
                                   txtPermitNUmber.Text = "AIB/" + lcOpend[0].branchAbrivation + "/01-" + lcOpend[0].permitCode + "/" + lcOpend[0].permitYear.ToString().Substring(2, 2);

                                   txtSupplyer.Text = lcOpend[0].supplyerName;
                                   txtAccountNO.Text = lcOpend[0].customerAccount;
                                   numLcValue.Value = lcOpend[0].lcValue;


                                   this.populateRembursingBank();
                                   this.populateOpenedThrough();
                                   combOpThrough.SelectedValue = lcOpend[0].openThroughId.ToString();
                                   combRembursing.SelectedValue = lcOpend[0].corespondenceID.ToString();
                                  // this.populateCurencyFirstTime();
                                   txtCurrency.Text = lcOpend[0].openingCurrency;
                                   dateValueDate.SelectedDate = lcOpend[0].valueDate;
                                   numCurrencyRate.Value = Convert.ToDouble(lcOpend[0].lcOpeningCurrencyRate);

                                   //binding invoices for a given Lc number
                                   Button5.Visible = false;
                                   DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                                   invoiceTable.Clear();
                                   foreach (var Item in lcOpend)
                                   {

                                       invoiceTable.Rows.Add(Item.id, Item.referenceNo, Item.invoiceDate, Item.invoiceValue, Item.invoiceRate, Item.openThroughBank, Item.invOpenThroughId, Item.status, Item.lcNumber, Item.numberOfSwift, Item.shipmentDate, Item.invoiceSequence, Item.sequenceStatus, Item.descripancyStatus);
                                   }

                                   RadGrid1.DataSource = invoiceTable;
                                   RadGrid1.DataBind();
                               }
                               else
                               {
                                   this.errorMessage("LC is expired, can not be updated.");
                               }
                           }
                           else
                           {
                               openingLogic.changeLCExpirationStatus(txtLCNumber.Text.ToString());
                               this.errorMessage("LC is expired, can not be updated.");
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
        /// <param name="e"></param>
        protected void invoice_SelectedIndexChanged(object sender, CommandEventArgs e)
        {
            PaymentCalculater calc = new PaymentCalculater();
            int itemIndex = Convert.ToInt32(e.CommandArgument);
            GridDataItem item = RadGrid1.Items[itemIndex];
            item.Selected = true;
            txtReferenceNumber.Text = item["Reference Number"].Text.ToString();
            dateInvoiceDate.SelectedDate = Convert.ToDateTime(item["Invoice Date"].Text);
            numInvoiceValue.Value = Convert.ToDouble(item["Invoice Value"].Text);
            numInvoiceRate.Value = Convert.ToDouble(item["Invoice Rate"].Text);
            RadioSequenceStatus.SelectedValue = item["sequenceStatus"].Text;
            numNumberOfSwift.Value = Convert.ToInt32(item["Number Of Swift"].Text);
            dateSheepmentDate.SelectedDate = Convert.ToDateTime(item["Sheepment Date"].Text);
            Session["Id"] = itemIndex.ToString();
            Session["InvoiceId"] = item["id"].Text;

            // finding margin value of LC
            double margien = 0;           
            if (Session["MarginPercent"] != null)
             {
                 margien = Convert.ToDouble(Session["MarginPercent"]);
             }

            // the selected invoice is not last invoice 
            if (item["sequenceStatus"].Text == "No")
            {
                   DateTime duedate = calc.calculateExpirationDueDate(txtLCNumber.Text, Convert.ToDateTime(dateOpeningDate.SelectedDate), Convert.ToInt32(txtOpeningPeriod.Text));
                   double netAdvane  = Convert.ToDouble(calc.calculateNetAdvance(margien, Convert.ToDouble(item["Invoice Value"].Text), Convert.ToDouble(numCurrencyRate.Value), Convert.ToDouble(numInvoiceRate.Value)));

                   int invoiceId = Convert.ToInt32(Session["InvoiceId"]);

                   // is the selected invoice is not discripant 
                        if (item["descripancyStatus"].Text == "No")
                        {
                            //check if the selected invoice is expired or not, if expired it cant be discripant 
                            if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), duedate) > 0)
                            {
                                this.errorMessage("The Invoice must be discripant to be settled.");
                            }
                            else
                            {
                                //dateIntereseBegin.DbSelectedDate = item["Interest Begin Date"].Text;
                                numInterest.Value = calc.calculateInterest(Convert.ToDateTime(dateInvoiceDate.SelectedDate), DateTime.Now, netAdvane);
                                Session["LocalInterest"] = (numInterest.Value).ToString();
                                Session["InterestStatus"]="Yes";
                            }
                        }
                        else {
                             // if the selected invoice is discripant, interest must be Zero and expiration amount is based on
                             // the invoice value if there is expiration 
                            if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), duedate) > 0)
                            {
                                double remainingLCValue =   Convert.ToDouble(numInvoiceValue.Value);
                                TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)duedate;
                                int days = Convert.ToInt32(difference.TotalDays) - 1;
                                numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                                Session["LocalInterest"] = (numExpirationAmount.Value).ToString();
                                Session["InterestStatus"] = "No";
                                numInterest.Value = 0.0;
                            }
                            else
                            {
                              
                                numExpirationAmount.Value = 0;
                                numInterest.Value = 0.0;
                                Session["LocalInterest"] = (numInterest.Value).ToString();
                                Session["InterestStatus"] = "Yes";
                            }
                          
                        }
                            //numInterest.Value = calc.calculateInterest(Convert.ToDateTime(dateInvoiceDate.DbSelectedDate), Convert.ToDateTime(item["Invoice Date"].Text), Convert.ToDouble(numNetAdvance.Value));
                        numNetTotal.Value = netAdvane  + numInterest.Value;
                       // Session["LocalInterest"] = (numInterest.Value).ToString();                                            
                        numNetAdvance.Value = netAdvane ;
                        numMarginHeld.Value = Convert.ToDouble(calc.calculateMargenHeld(margien, Convert.ToDouble(item["Invoice Value"].Text), Convert.ToDouble(numLcValue.Value))) * numCurrencyRate.Value;
                        //double localMarginHeld = Convert.ToDouble(numMarginHeld.Value * numCurrencyRate.Value);
                        Session["LocalMarginHeld"] = numMarginHeld.Value.ToString();

                
                        numExcessDrawing.Value = 0;
                
                       // numExcessDrawing.Value = calc.calculculateExessAmount(Convert.ToDouble(numLcValue.Value), Convert.ToDouble(item["Invoice Value"].Text),txtLCNumber.Text) * numInvoiceRate.Value;
                        Session["LocalExcessDrawing"] = numExcessDrawing.Value.ToString();
                        // numNetTotal.Value = (Convert.ToDouble(calc.calculateNetAdvance(margien, Convert.ToDouble(item["Invoice Value"].Text), Convert.ToDouble(numLcValue.Value)))) + calc.calculateInterest();
            }
            else
            {
                //The Selected is last invoice 
                DateTime duedate = calc.calculateExpirationDueDate(txtLCNumber.Text, Convert.ToDateTime(dateOpeningDate.SelectedDate), Convert.ToInt32(txtOpeningPeriod.Text));
                DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
                int count = 0;
                double totalSettledMarginHeld = 0;
                foreach (GridDataItem itemenew in RadGrid1.Items)
                {
                    //DataRow row1 = invoiceTable.Rows[Convert.ToInt32(itemenew.ID)];
                    if (Convert.ToInt32(itemenew["id"].Text) != Convert.ToInt32(Session["InvoiceId"]) && itemenew["status"].Text == "ACTIVE")
                    {
                        count = count + 1;

                    }
                   if (Convert.ToInt32(itemenew["id"].Text) != Convert.ToInt32(Session["InvoiceId"]) && itemenew["status"].Text == "Setteled")
                   {
                       totalSettledMarginHeld = totalSettledMarginHeld + Convert.ToDouble(calc.calculateMargenHeld(margien, Convert.ToDouble(itemenew["Invoice Value"].Text), Convert.ToDouble(numLcValue.Value)));

                   }
                }
                if (count == 0)
                {
                    double netAdvane=Convert.ToDouble(numInvoiceValue.Value*numInvoiceRate.Value)-(Convert.ToDouble((((numLcValue.Value * margien) / 100) - totalSettledMarginHeld)*numCurrencyRate.Value));
                  
                  
                    // calculating the total invoice values 
                 
                            double newInvoice = 0;

                            foreach (GridDataItem item1 in RadGrid1.MasterTableView.Items)
                             {
                                if ((item1["status"].Text == "NEW" || item1["status"].Text == "ACTIVE" || item1["status"].Text == "Setteled" || item1["status"].Text == "Advised") && item1.ItemIndex != itemIndex)
                                {
                                    newInvoice = newInvoice + Convert.ToDouble(item1["Invoice Value"].Text);
                                }
                              }
                            newInvoice = newInvoice + Convert.ToDouble(item["Invoice Value"].Text);
                            int invoiceId = Convert.ToInt32(Session["InvoiceId"]);                   
                            //if the selected last invoice is not discripant 
                            if (item["descripancyStatus"].Text == "No")
                            {
                                //check if the selected invoice is expired, if so it must be discripant 
                                if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), duedate) > 0)
                                {
                                    this.errorMessage("The Invoice must be discripant to be settled.");
                                }
                                else
                                {
                                    numInterest.Value = calc.calculateInterest(Convert.ToDateTime(dateInvoiceDate.SelectedDate), DateTime.Now, netAdvane);
                                    Session["LocalInterest"] = (numInterest.Value).ToString();
                                    Session["InterestStatus"] = "Yes";
                                }                                                       
                            }
                            else
                            {
                                if (DateTime.Compare(Convert.ToDateTime(dateSheepmentDate.SelectedDate), duedate) > 0)
                                {
                                    if (Convert.ToDouble(numLcValue.Value) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value)) > 0)
                                    {
                                        double remainingLCValue = Convert.ToDouble(numLcValue.Value) - (newInvoice - Convert.ToDouble(numInvoiceValue.Value));
                                        TimeSpan difference = Convert.ToDateTime(dateSheepmentDate.SelectedDate) - (DateTime)duedate;
                                        int days = Convert.ToInt32(difference.TotalDays) - 1;
                                        numExpirationAmount.Value = calc.calculateExpiration(remainingLCValue, days) * numCurrencyRate.Value;
                                        Session["LocalInterest"] = (numExpirationAmount.Value).ToString();
                                        Session["InterestStatus"] = "No";
                                        numInterest.Value = 0;
                                    }
                                    else
                                    {
                                        numExpirationAmount.Value = 0;
                                        numInterest.Value = 0;
                                        Session["LocalInterest"] = (numInterest.Value).ToString();
                                        Session["InterestStatus"] = "Yes";
                                    }
                                }
                                else
                                {
                                    numExpirationAmount.Value = 0;
                                    numInterest.Value = 0;
                                    Session["LocalInterest"] = (numInterest.Value).ToString();
                                    Session["InterestStatus"] = "Yes";
                                }
                              
                              
                            }
                            //numInterest.Value = calc.calculateInterest(Convert.ToDateTime(dateInvoiceDate.DbSelectedDate), Convert.ToDateTime(item["Invoice Date"].Text), Convert.ToDouble(numNetAdvance.Value));
                            numNetTotal.Value = (netAdvane ) + numInterest.Value;
                           // Session["LocalInterest"] = (numInterest.Value).ToString();                                                                                
                            numNetAdvance.Value = netAdvane ;
                            double forienManrginHeld = Convert.ToDouble(((numLcValue.Value * margien) / 100) - totalSettledMarginHeld);//-totalSettledMarginHeld

                            double localMarginHeld = Convert.ToDouble(forienManrginHeld * numCurrencyRate.Value);
                            numMarginHeld.Value = localMarginHeld;
                            Session["LocalMarginHeld"] = localMarginHeld.ToString();


                            numExcessDrawing.Text = (calc.calculculateExessAmount(Convert.ToDouble(numLcValue.Value),Convert.ToDouble(item["Invoice Value"].Text), txtLCNumber.Text, Convert.ToInt32(item["id"].Text)) * numInvoiceRate.Value).ToString();
                       
                        //numExcessDrawing.Value = calc.calculculateExessAmount(Convert.ToDouble(numLcValue.Value), Convert.ToDouble(item["Invoice Value"].Text), txtLCNumber.Text) * numInvoiceRate.Value;
                        Session["LocalExcessDrawing"] = numExcessDrawing.Value.ToString();
                    }
                    else
                    {
                        Session["InvoiceId"]=null;
                        this.resetInveForSettelment();
                        this.errorMessage("Invoices befor the last invoice must be settled first.");
                    }
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        public void resetInveForSettelment() {
            txtReferenceNumber.Text = "";
            dateInvoiceDate.Clear();
            numInvoiceValue.Value = 0;
            numInvoiceRate.Value = 0;
            RadioSequenceStatus.SelectedValue = "Yes";
            numMarginHeld.Value = 0;
            numNetAdvance.Value = 0;
            numExcessDrawing.Value = 0;
            numNetTotal.Value = 0;
            numInterest.Value = 0;
            numExpirationAmount.Value = 0;
            dateSheepmentDate.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void calculateInterest(object sender, EventArgs args) { 
        
             ComponentValidator valids = new ComponentValidator();
             openingLogic = new LcOpeningLogic();
             valids.addComponent(dateInvoiceDate, ComponentValidator.GC_DATE, true);
             valids.addComponent(numNetAdvance, ComponentValidator.FLOAT_NUMBER, true);
            if (valids.isAllComponenetValid())
            {
                if (DateTime.Compare(Convert.ToDateTime(dateInvoiceDate.SelectedDate), Convert.ToDateTime(DateTime.Now)) <= 0)
                {
                    int invoiceId = Convert.ToInt32(Session["InvoiceId"]);
                    bool result = openingLogic.addinterestBeginDate(dateInvoiceDate.SelectedDate, invoiceId);
                    if (result)
                    {
                        PaymentCalculater calc = new PaymentCalculater();
                        numInterest.Value = calc.calculateInterest(dateInvoiceDate.SelectedDate, DateTime.Now, Convert.ToDouble(numNetAdvance.Value));
                        numNetTotal.Value = Convert.ToDouble(numNetAdvance.Value + numInterest.Value);
                    }
                    else
                    {

                    }
                }
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
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void bindOpeningReport(object sender, EventArgs args)
        {
            if (Session["InvoiceId"] != null && Session["InterestStatus"]!=null)
            {
                               
                openingLogic = new LcOpeningLogic();
                ReportDocument crt = new ReportDocument();
                //adding crystal report path to the report
               
                    if (RadioSequenceStatus.SelectedValue == "Yes")
                    {
                        int count = 0;
                        //counting invoices other than the selected invoice
                        foreach (GridDataItem itemenew in RadGrid1.Items)
                        {
                            //DataRow row1 = invoiceTable.Rows[Convert.ToInt32(itemenew.ID)];
                            if (Convert.ToInt32(itemenew["id"].Text) != Convert.ToInt32(Session["InvoiceId"]) && itemenew["status"].Text == "ACTIVE")
                            {
                                count = count + 1;

                            }
                            
                        }
                        // if there is no other invoice 
                        if (count == 0)
                        { List<TBL_CURRENCY> currency = new List<TBL_CURRENCY>();
                    string currencyCode = txtCurrency.Text;
                    currency = openingLogic.findCurrencyByCurrencyCode(currencyCode);
                    if (currency.Count == 1)
                    {
                            // change the status of the selected invoice to setteled
                            bool result1 = openingLogic.changeadviceStatus("Setteled", Convert.ToInt32(Session["InvoiceId"].ToString()));
                            if (result1)
                            {

                             // Change the status of the parent LC    
                               bool result = openingLogic.changeLCStatus(txtLCNumber.Text.ToString());
                               
                                if (result)
                                {

                                    string aibCorespondentAccount = openingLogic.findCorspondentAccount(Convert.ToInt32(combRembursing.SelectedValue), currency[0].currencyId);
                                    Session["AIBACCOUNT"] = aibCorespondentAccount.ToString();
                                    //if there is no discripancy there is interest and if there is expiration there is 
                                    //no interest
                                    if (Session["InterestStatus"].Equals("Yes"))
                                    {
                                        crt.Load(Server.MapPath("~/AIB_REPORT/LC_Settlement.rpt"));
                                    }
                                    else {
                                        crt.Load(Server.MapPath("~/AIB_REPORT/LC_Expiration_Settlement.rpt"));
                                    }
                                    // logon detail of the report database
                                    crt.SetDatabaseLogon("da", "pass@2123");
                                  
                                    Session["Report"] = crt;
                                    Session["reportCode"] = "lcSettlment";
                                    Session["lcNumber"] = txtLCNumber.Text.ToString();
                                    //Server.TransferRequest("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                                    Response.Redirect("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                           
                                }
                                else
                                {
                                    this.errorMessage("LC Status Can not be updated please contact administrater");
                                }
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
                        else {

                            this.errorMessage("You cant settele the last invoice befor Other active invoices. please settele invoices befor the last invoice");
                        }
                    }
                    else
                    {
                        List<TBL_CURRENCY> currency = new List<TBL_CURRENCY>();
                         string currencyCode = txtCurrency.Text;
                    currency = openingLogic.findCurrencyByCurrencyCode(currencyCode);
                    if (currency.Count == 1)
                    {
                        // changing the status of the selected invoice which is not the last invoice
                        bool result1 = openingLogic.changeadviceStatus("Setteled", Convert.ToInt32(Session["InvoiceId"].ToString()));
                        if (result1)
                        {
                            string aibCorespondentAccount = openingLogic.findCorspondentAccount(Convert.ToInt32(combRembursing.SelectedValue), Convert.ToInt32(currency[0].currencyId));
                            Session["AIBACCOUNT"] = aibCorespondentAccount.ToString();
                            //if there is no discripancy there is interest and if there is expiration there is 
                            //no interest
                            if (Session["InterestStatus"].Equals("Yes"))
                            {
                                crt.Load(Server.MapPath("~/AIB_REPORT/LC_Settlement.rpt"));
                            }
                            else
                            {
                                crt.Load(Server.MapPath("~/AIB_REPORT/LC_Expiration_Settlement.rpt"));
                            }
                            // logon detail of the report database
                            crt.SetDatabaseLogon("da", "pass@2123");

                            Session["Report"] = crt;
                            Session["reportCode"] = "lcSettlment";
                            Session["lcNumber"] = txtLCNumber.Text.ToString();
                            //Server.TransferRequest("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                            Response.Redirect("~/AIB_REPORT/RepPages/LC_Reports.aspx");
                         }
                         else
                         {
                             this.errorMessage("The Correct Invoice must be selected to be settled");
                         }
                    }
                    else
                    {
                        this.errorMessage("Currency Code must be uniqe.please contact Administrator.");
                    }
                }
                }
            else
            {
                this.errorMessage("At list one invoice must be selected to view a report.");
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
            txtSearch.ReadOnly = false;
            txtReferenceNumber.Text = "";
          
            numCurrencyRate.Text = "";
           
            txtOpeningPeriod.Text = "";
            txtCurrency.Text = "";
            combOpThrough.ClearSelection();
            combRembursing.ClearSelection();
            dateInvoiceDate.Clear();
            dateValueDate.Clear();
            numInvoiceValue.Text = "";
            numInvoiceRate.Text = "";
            numExcessDrawing.Text = "";
            numInterest.Text = "";
            numExpirationAmount.Text = "";
            numMarginHeld.Text = "";
            numNetAdvance.Text = "";
            numNetTotal.Text = "";
            numNumberOfSwift.Text = "";
            numUserLimitaition.Text = "";
            dateOpeningDate.Clear();
            
            numLcValue.Text = "";
          
            DataTable invoiceTable = (DataTable)ViewState["InvoiceTable"];
            invoiceTable.Clear();
            RadGrid1.DataSource = invoiceTable;
            RadGrid1.DataBind();
            txtSearch.Text = "";
            txtSearch.ReadOnly = false;
            Button5.Visible = true;
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
            //combCurrency.DataSource = currency;
           // combCurrency.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void initializeInvoiceDataTable()
        {


            DataTable invoiceTable = new DataTable();
            invoiceTable.Columns.Add("id", typeof(Int32));
            invoiceTable.Columns.Add("Reference Number", typeof(String));
            invoiceTable.Columns.Add("Invoice Date", typeof(DateTime));
            invoiceTable.Columns.Add("Invoice Value", typeof(Double));
            invoiceTable.Columns.Add("Invoice Rate", typeof(Double));
            invoiceTable.Columns.Add("Opening Bank", typeof(String));
            invoiceTable.Columns.Add("openingBankId", typeof(Int32));
            invoiceTable.Columns.Add("status", typeof(String));
            invoiceTable.Columns.Add("lcNumber", typeof(String));
            invoiceTable.Columns.Add("Number Of Swift", typeof(Int32));
            invoiceTable.Columns.Add("Sheepment Date", typeof(DateTime));
            invoiceTable.Columns.Add("Sequence", typeof(Int32));
            invoiceTable.Columns.Add("sequenceStatus", typeof(String));
            invoiceTable.Columns.Add("descripancyStatus", typeof(String));
            ViewState["InvoiceTable"] = invoiceTable;
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