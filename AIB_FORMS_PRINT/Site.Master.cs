using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIB_FORMS_PRINT
{
    public partial class SiteMaster : MasterPage
    {
        LinkButton btnLogOuts;


        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;


        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] != null) lbluser.Text = Session["Name"].ToString();
            checkSession();
        }
        /// <summary>
        /// 
        /// </summary>
        public void MasterPageLabels()
        {
            btnLogOuts = (LinkButton)this.FindControl("btnLogOut");
            MasterPageLabel = "logout";
        }
        /// <summary>
        /// get and set text property
        /// </summary>
        public string MasterPageLabel
        {
            get { return btnLogOuts.Text; }
            set { btnLogOuts.Text = value; }
        }

        public void checkSession()
        {
            LinkButton btnLogOuts = (LinkButton)this.FindControl("btnLogOut");
            if (Session["UserID"] != null)
            {
                btnLogOuts.Text = "Log Out";
            }
            else
            {
                btnLogOuts.Text = "";
            }
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

        protected void RequestLanguageChange_Click(object sender, EventArgs e)
        {
            LinkButton senderLink = sender as LinkButton;

            //store requested language as new culture in the session
            Session["MyCulture"] = senderLink.CommandArgument;

            //reload last requested page with new culture
            Server.Transfer(Request.Path);
        }
    }
}