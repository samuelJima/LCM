using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIB_FORMS_PRINT.AIB_REPORT.RepPages
{
    public partial class LCOpeningReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportDocument crt = new ReportDocument();
                crt.Load(Server.MapPath("~/AIB_REPORT/LC_Opening.rpt"));
                // Session.Add("report",report)  ;
                CrystalReportViewer1.ReportSource = crt;

            }
          
          
            //ReportDocument cr = new ReportDocument();
            //cr.Load(Server.MapPath("~/AIB_REPORT/LC_Opening.rpt"));
            //cr.SetDatabaseLogon("Administrator", "seyoumeScholar1", "10.34.10.99", "AIB_LC_DB", true);
            //cr = (ReportDocument)Session["CitySummaryReport"];
          //  LC_Opening_Report.ReportSource = cr;
           // LC_Opening_Report.RefreshReport();
          //  LC_Opening_Report.DataBind();
           // InitializeComponent();
        }
       // override protected void OnInit(EventArgs e)
       // {
            // CODEGEN: This call is required by the ASP.NET Web 
            // Form Designer
           // InitializeComponent();

          //  ReportDocument oRpt = new ReportDocument();
         //   oRpt.Load("D:/Pro/AIB_FORMS_PRINT/AIB_FORMS_PRINT/AIB_REPORT/LC_Opening.rpt");
           // LC_Opening_Report.ReportSource = oRpt;
       // }

        //private void InitializeComponent()
        //{
           // LC_Opening_Report.DataBind();
       // }

       // protected void LC_Opening_Report_Init(object sender, EventArgs e)
        //{

       // }
    }
}