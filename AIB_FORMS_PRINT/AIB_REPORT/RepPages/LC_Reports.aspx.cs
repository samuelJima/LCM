using AIB_FORMS_LOGIC;
using AIB_FORMS_LOGIC.LC_LOGIC;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIB_FORMS_PRINT.AIB_REPORT.RepPages
{
    public partial class LC_Reports : System.Web.UI.Page
    {
         ReportDocument crt = new ReportDocument();
         LCReportLogic repLogic;
         protected void Page_Load(object sender, EventArgs e)
         {

             int parameter = 0;
             if (IsPostBack)
             {
                 if (Session["Report"] != null)
                 {

                     crt = (ReportDocument)Session["Report"];
                     CrystalReportViewer1.ReportSource = crt;
                     CrystalReportViewer1.DataBind();
                 }
             }
             else
             {
                 if (Session["reportCode"] != null && Session["reportCode"].ToString().Equals("lcNumber"))
                 {
                     if (Session["Report"] != null)
                     {
                         crt = (ReportDocument)Session["Report"];
                         Session["Report"] = crt;
                         parameter = 0;
                     }
                 }
                 if (Session["reportCode"] != null && Session["reportCode"].ToString().Equals("lcAdvice"))
                 {
                     if (Session["Report"] != null)
                     {
                         crt = (ReportDocument)Session["Report"];
                         Session["Report"] = crt;
                         parameter = 1;

                     }
                 }

                 if (Session["reportCode"] != null && Session["reportCode"].ToString().Equals("lcSettlment"))
                 {
                     if (Session["Report"] != null)
                     {
                         crt = (ReportDocument)Session["Report"];
                         Session["Report"] = crt;
                         parameter = 3;

                     }
                 }

                 if (Session["reportCode"] != null && Session["reportCode"].ToString().Equals("lcCancelation"))
                 {
                     if (Session["Report"] != null)
                     {
                         crt = (ReportDocument)Session["Report"];
                         Session["Report"] = crt;
                         parameter = 2;

                     }
                 }
                 if (Session["reportCode"] != null && Session["reportCode"].ToString().Equals("lcextention"))
                 {
                     if (Session["Report"] != null)
                     {
                         crt = (ReportDocument)Session["Report"];
                         Session["Report"] = crt;
                         parameter = 4;

                     }
                 }
                 CrystalReportViewer1.ReportSource = crt;

             
                 CrystalReportViewer1.RefreshReport();
                 CrystalReportViewer1.DataBind();
                 if (parameter == 0)
                 {
                    CrystalReportViewer1.ParameterFieldInfo = this.getLCNumber();
                 }
                 if (parameter == 1) {

                     CrystalReportViewer1.ParameterFieldInfo = this.getLCNumberAndAdviceSight();
                 }
                 if (parameter == 2)
                 {

                     CrystalReportViewer1.ParameterFieldInfo = this.getLCNumberandInvoiceTotal();
                 }
                 if (parameter == 3)
                 {

                     CrystalReportViewer1.ParameterFieldInfo = this.getLCNumbeAndSettlment();
                 }
                 if (parameter == 4)
                 {

                     CrystalReportViewer1.ParameterFieldInfo = this.getextentionLCNumber();
                 }
             }
         }

         private ParameterFields getLCNumber()
         {

             ParameterFields paramFields = new ParameterFields();

             if (Session["lcNumber"] != null  && Session["usdRate"]!=null)
             {

                 //double lcCode = Convert.ToDouble(Session["lcCode"].ToString());

                 ParameterField paramField = new ParameterField();
                 paramField.Name = "lcNumber";
                 ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
                 paramDiscreteValue.Value = Session["lcNumber"].ToString();
                 paramField.CurrentValues.Add(paramDiscreteValue);
                 paramFields.Add(paramField); 

                 ParameterField paramField1 = new ParameterField();
                 paramField1.Name = "usRate";
                 ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();
                 paramDiscreteValue1.Value = Convert.ToDouble(Session["usdRate"]);
                 paramField1.CurrentValues.Add(paramDiscreteValue1);
                 paramFields.Add(paramField1);

                

              
                 return paramFields;
             }
             else
             {

                 return paramFields;
             }


         }
         private ParameterFields getextentionLCNumber()
         {

             ParameterFields paramFields = new ParameterFields();

             if (Session["lcNumber"] != null && Session["usdRate"] != null && Session["ExtentionCom"] != null)
             {

                 //double lcCode = Convert.ToDouble(Session["lcCode"].ToString());

                 ParameterField paramField = new ParameterField();
                 paramField.Name = "lcNumber";
                 ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
                 paramDiscreteValue.Value = Session["lcNumber"].ToString();
                 paramField.CurrentValues.Add(paramDiscreteValue);
                 paramFields.Add(paramField);

                 ParameterField paramField1 = new ParameterField();
                 paramField1.Name = "usRate";
                 ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();
                 paramDiscreteValue1.Value = Convert.ToDouble(Session["usdRate"]);
                 paramField1.CurrentValues.Add(paramDiscreteValue1);
                 paramFields.Add(paramField1);

                 ParameterField paramField2 = new ParameterField();
                 paramField2.Name = "extentionCommission";
                 ParameterDiscreteValue paramDiscreteValue2 = new ParameterDiscreteValue();
                 paramDiscreteValue2.Value = Convert.ToDouble(Session["ExtentionCom"]);
                 paramField2.CurrentValues.Add(paramDiscreteValue2);
                 paramFields.Add(paramField2);


                 return paramFields;
             }
             else
             {

                 return paramFields;
             }


         }
         private ParameterFields getLCNumberAndAdviceSight()
         {
             
             ParameterFields paramFields = new ParameterFields();

             if (Session["lcNumber"] != null && Session["usRate"] != null && Session["AIBACCOUNT"]!=null)
             {

                 //double lcCode = Convert.ToDouble(Session["lcCode"].ToString());

                 ParameterField paramField = new ParameterField();
                 paramField.Name = "lcNumber";
                 ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
                 paramDiscreteValue.Value = Session["lcNumber"].ToString();
                 paramField.CurrentValues.Add(paramDiscreteValue);
                 paramFields.Add(paramField);

                 ParameterField paramField1 = new ParameterField();
                 paramField1.Name = "adviceid";
                 ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();
                 paramDiscreteValue1.Value =Convert.ToInt32(Session["InvoiceId"].ToString());
                 paramField1.CurrentValues.Add(paramDiscreteValue1);
                 paramFields.Add(paramField1);

                 ParameterField paramField2 = new ParameterField();
                 paramField2.Name = "usRate";
                 ParameterDiscreteValue paramDiscreteValue2 = new ParameterDiscreteValue();
                 paramDiscreteValue2.Value = Convert.ToDouble(Session["usRate"].ToString());
                 paramField2.CurrentValues.Add(paramDiscreteValue2);
                 paramFields.Add(paramField2);
                
                 ParameterField paramField3 = new ParameterField();
                 paramField3.Name = "aibAccount";
                 ParameterDiscreteValue paramDiscreteValue3 = new ParameterDiscreteValue();
                 paramDiscreteValue3.Value = (Session["AIBACCOUNT"].ToString());
                 paramField3.CurrentValues.Add(paramDiscreteValue3);
                 paramFields.Add(paramField3);
                 
                 return paramFields;

             }
             else
             {

                 return paramFields;
             }


         }

         private ParameterFields getLCNumbeAndSettlment()
         {

             ParameterFields paramFields = new ParameterFields();

             if (Session["lcNumber"] != null && Session["InvoiceId"] != null && Session["AIBACCOUNT"] != null)
             {

               

                 ParameterField paramField = new ParameterField();
                 paramField.Name = "lcNumber";
                 ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
                 paramDiscreteValue.Value = Session["lcNumber"].ToString();
                 paramField.CurrentValues.Add(paramDiscreteValue);
                 paramFields.Add(paramField);

                 ParameterField paramField1 = new ParameterField();
                 paramField1.Name = "adviceid";
                 ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();
                 paramDiscreteValue1.Value = Convert.ToInt32(Session["InvoiceId"].ToString());
                 paramField1.CurrentValues.Add(paramDiscreteValue1);
                 paramFields.Add(paramField1);


                 ParameterField paramField2 = new ParameterField();
                 paramField2.Name = "interestLocal";
                 ParameterDiscreteValue paramDiscreteValue2 = new ParameterDiscreteValue();
                 paramDiscreteValue2.Value = Convert.ToDouble(Session["LocalInterest"].ToString());
                 paramField2.CurrentValues.Add(paramDiscreteValue2);
                 paramFields.Add(paramField2);

                 ParameterField paramField3 = new ParameterField();
                 paramField3.Name = "marginHeld";
                 ParameterDiscreteValue paramDiscreteValue3 = new ParameterDiscreteValue();
                 paramDiscreteValue3.Value = Convert.ToDouble(Session["LocalMarginHeld"].ToString());
                 paramField3.CurrentValues.Add(paramDiscreteValue3);
                 paramFields.Add(paramField3);

                 ParameterField paramField4 = new ParameterField();
                 paramField4.Name = "excessDrowing";
                 ParameterDiscreteValue paramDiscreteValue4 = new ParameterDiscreteValue();
                 paramDiscreteValue4.Value = Convert.ToDouble(Session["LocalExcessDrawing"].ToString());
                 paramField4.CurrentValues.Add(paramDiscreteValue4);
                 paramFields.Add(paramField4);

                 ParameterField paramField5 = new ParameterField();
                 paramField5.Name = "aibAccount";
                 ParameterDiscreteValue paramDiscreteValue5 = new ParameterDiscreteValue();
                 paramDiscreteValue5.Value = (Session["AIBACCOUNT"].ToString());
                 paramField5.CurrentValues.Add(paramDiscreteValue5);
                 paramFields.Add(paramField5);

                
                 return paramFields;
             }
             else
             {

                 return paramFields;
             }


         }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         private ParameterFields getLCNumberandInvoiceTotal()
         {
             repLogic = new LCReportLogic();
             ParameterFields paramFields = new ParameterFields();

             if (Session["lcNumber"] != null && Session["AIBACCOUNT"] != null)
             {

                 //double lcCode = Convert.ToDouble(Session["lcCode"].ToString());

                 ParameterField paramField = new ParameterField();
                 paramField.Name = "lcNumber";
                 ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
                 paramDiscreteValue.Value = Session["lcNumber"].ToString();
                 paramField.CurrentValues.Add(paramDiscreteValue);
                 paramFields.Add(paramField);

                 ParameterField paramField1 = new ParameterField();
                 paramField1.Name = "totalInvoice";
                 double sum =repLogic.findThesumOfInvoice(Session["lcNumber"].ToString());
                 ParameterDiscreteValue paramDiscreteValue1 = new ParameterDiscreteValue();
                 paramDiscreteValue1.Value = sum;
                 paramField1.CurrentValues.Add(paramDiscreteValue1);
                 paramFields.Add(paramField1);

                 ParameterField paramField5 = new ParameterField();
                 paramField5.Name = "aibAccount";
                 ParameterDiscreteValue paramDiscreteValue5 = new ParameterDiscreteValue();
                 paramDiscreteValue5.Value = (Session["AIBACCOUNT"].ToString());
                 paramField5.CurrentValues.Add(paramDiscreteValue5);
                 paramFields.Add(paramField5);


                 return paramFields;
             }
             else
             {

                 return paramFields;
             }


         }

         protected void CrystalReportViewer1_PreRender(object sender, EventArgs e)
         {

         }

         protected void btnLogOut_Click(object sender, EventArgs e)
         {
             LinkButton btnLogOut = (LinkButton)this.FindControl("btnLogOut");
             if (btnLogOut.Text == "Log Out")
             {
                 Session.Abandon();
                 Response.Redirect("~/AIB_SECURITY/login.aspx");
             }

         }

    }
}