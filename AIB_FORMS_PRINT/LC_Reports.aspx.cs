using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace AIB_FORMS_PRINT.AIB_REPORT.RepPages
{
    public partial class LC_Reports : System.Web.UI.Page
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

        }
    }
}