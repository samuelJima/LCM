using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIB_FORMS_LOGIC;
using AIB_FORMS_VALIDATION;

namespace AIB_FORMS_PRINT.AIB_SECURITY
{
    public partial class ResourceManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private const string m_DefaultCulture = "am-ET";
        protected override void InitializeCulture()
        {
            //retrieve culture information from session
            string culture = Convert.ToString(Session["MyCulture"]);

            //check whether a culture is stored in the session
            if (!string.IsNullOrEmpty(culture)) Culture = culture;
            else Culture = m_DefaultCulture;

            //set culture to current thread
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            //call base class
            base.InitializeCulture();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtResourceName, ComponentValidator.FULL_NAME, true);
            valids.addComponent(txtPath, ComponentValidator.REMARK_COMMENT, true);
            valids.addComponent(txtDescription, ComponentValidator.REMARK_COMMENT, false);
            if (valids.isAllComponenetValid())
            {
                ResourceManagements res = new ResourceManagements();
               int result = res.AddNewResource(txtResourceName.Text,txtPath.Text,txtDescription.Text);
               if (result > 0)
               {
                   reset();
                   lblMessage.Text = "New Resource is Successfully saved!";
                   lblMessage.Visible = true;
               }
            }
        }
        void reset()
        {
            
            txtResourceName.Text = "";
            txtPath.Text = "";
            txtDescription.Text = "";
            lblMessage.Visible = false;
        }
    }
}