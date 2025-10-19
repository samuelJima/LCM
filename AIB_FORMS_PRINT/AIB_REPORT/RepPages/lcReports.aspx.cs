using AIB_FORMS_LOGIC;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIB_FORMS_PRINT.AIB_REPORT.RepPages
{
    public partial class lcReports : System.Web.UI.Page
    {
       

        ReportDocument crt = new ReportDocument();
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

                    }
                }
                if (parameter == 0)
                {
                    CrystalReportViewer1.ParameterFieldInfo = this.getLCNumber();
                }
                crt.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                crt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4Small;
                crt.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(0, 0, 9, 0));
                CrystalReportViewer1.ReportSource = crt;

                // CrystalReportViewer1.DisplayToolbar = true;

                CrystalReportViewer1.RefreshReport();
                CrystalReportViewer1.DataBind();

            }



        }
        protected void CrystalReportViewer1_PreRender(object sender, EventArgs e)
        {

        }


        private ParameterFields getLCNumber()
        {

            ParameterFields paramFields = new ParameterFields();

            if (Session["lcNumber"] != null)
            {

                PaymentCalculater calc = new PaymentCalculater();
                //double lcCode = Convert.ToDouble(Session["lcCode"].ToString());

                ParameterField paramField = new ParameterField();
                paramField.Name = "lcNumber";
                ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = Session["lcNumber"].ToString();
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
                return paramFields;
            }
            else
            {

                return paramFields;
            }


        }

    }
}