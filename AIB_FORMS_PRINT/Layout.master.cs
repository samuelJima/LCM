using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIB_FORMS_LOGIC;
using AIB_FORMS_OB;
using AIB_FORMS_VALIDATION;
using System.Security.Cryptography;
using System.Text;

namespace AIB_FORMS_PRINT
{
    public partial class Layout : System.Web.UI.MasterPage
    {
      
        Tbl_RoleResourcePermission[] arryRessources;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ManagePrivilage();
            }

        }
        void ManagePrivilage()
        {
            if (Session["UserRole"] != null)
            {
                Role_Management rol;
                int RoleID = Convert.ToInt32(Session["UserRole"]);
                rol = new Role_Management();
                arryRessources = rol.SearchRoleResource(RoleID);
                if (Session["IsNew"] != null)
                {
                    bool isnew = Convert.ToBoolean(Session["IsNew"]);
                    if (isnew)
                    {
                        //lblName.Text = Session["Name"].ToString();
                       // Email.Text = Session["Email"].ToString();
                       // lblUSerName.Text = Session["UserName"].ToString();
                       // ttpUserSetting.Show();
                        UserManagment usr = new UserManagment();
                        usr.update_FirstTimeAccess(false, Convert.ToInt32(Session["UserID"].ToString()));
                        Session["IsNew"] = false;
                    }
                }
            }
            else
            {
                Response.Redirect("~/AIB_SECURITY/login.aspx");
            }
        }

        protected void RadPanelBar1_ItemDataBound1(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        {
            List<string> Permissions = new List<string>();
            e.Item.Visible = false;
            ResourceManagements Resource = new ResourceManagements();
            foreach (Tbl_RoleResourcePermission resource in arryRessources)
            {
                Permissions.Add(resource.Resource_ID.ToString() + "/" + resource.Permission_ID.ToString());
                Session["Permissions"] = Permissions;
                Tbl_Resource[] res = Resource.SearchResourceByResourceID(Convert.ToInt32(resource.Resource_ID));
                if (res.Length > 0)
                {
                    if (e.Item.Value == res[0].Resource_Name)
                    {

                        e.Item.Visible = true;
                        e.Item.Parent.Visible = true;
                    }

                }

            }
        }
     /*   protected void btnAccountSetting_Click1(object sender, EventArgs e)
        {
            //reset();
            ComponentValidator valid = new ComponentValidator();
            valid.addComponent(txtOldPassword, ComponentValidator.PASSWORD, true);
            valid.addComponent(txtNewPassword, ComponentValidator.PASSWORD, true);
            valid.addComponent(txtConfirmPassword, ComponentValidator.PASSWORD, true);
            valid.addComponent(txtSecurityAnswer, ComponentValidator.REMARK_COMMENT, true);
           // valid.clearErrorIndication();
           // txtOldPassword.Text = "";
            lblErr0.Text = "";
            lblName.Text = Session["Name"].ToString();
            Email.Text = Session["Email"].ToString();
            lblUSerName.Text = Session["UserName"].ToString();
            ttpUserSetting.Show();
        }
        void reset()
        {
            txtConfirmPassword.Text = "";
            txtNewPassword.Text = "";
            txtOldPassword.Text = "";
            txtSecurityAnswer.Text = "";
            CboSecQuestion.SelectedIndex = 1;
            CboSecQuestion.Text = "";
        }
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            ComponentValidator valid = new ComponentValidator();
            valid.addComponent(txtOldPassword, ComponentValidator.PASSWORD, true);
            valid.addComponent(txtNewPassword, ComponentValidator.PASSWORD, true);
            valid.addComponent(txtConfirmPassword, ComponentValidator.PASSWORD, true);
            if (valid.isAllComponenetValid())
            {
                if (VerifyPassword(txtOldPassword.Text) != null)
                {
                    if (txtNewPassword.Text == txtConfirmPassword.Text)
                    {
                        UserManagment user = new UserManagment();
                        int saltSize = 5;
                        string salt = CreateSalt(saltSize);
                        string hashedPwd = CreatePasswordHash(txtNewPassword.Text, salt);
                        user.update_password(hashedPwd, Convert.ToInt32(Session["UserID"].ToString()));
                        lblErr0.Text = "Updated Successfully.";
                        reset();
                        ttpUserSetting.Show();
                    }
                    else
                    {
                        lblErr0.Text = "The new password doesnt match with the confirm passowrd field.";
                        ttpUserSetting.Show();
                    }
                }
                else
                {
                    lblErr0.Text = "The old password doesn't match with the stored password.";
                    ttpUserSetting.Show();
                }
            }
            else
            {
                ttpUserSetting.Show();
            }
        }*/
      /*  private Tbl_User[] VerifyPassword(string suppliedPassword)
        {
            bool passwordMatch = false;

            UserManagment user = new UserManagment();
            Tbl_User[] users = user.SearchUserByLoginDetails(Session["UserName"].ToString());


            if (users.Length != 0)
            {

                if (users[0].Status == 1)
                {
                    return null;

                }
                else if (users[0].Status == 2)
                {
                    return null;
                }
                else
                {
                    string dbPasswordHash = users[0].Password;
                    int saltSize = 5;
                    string salt = dbPasswordHash.Substring(dbPasswordHash.Length - saltSize);
                    string hashedPasswordAndSalt =
                    CreatePasswordHash(suppliedPassword, salt);
                    passwordMatch = hashedPasswordAndSalt.Equals(dbPasswordHash);
                    if (passwordMatch)
                    {
                        return users;

                    }
                    else
                    {
                        lblErr0.Text = "Invalid password ! ";
                        return null;
                    }

                }
            }
            return null;
        }*/
      /*  protected void btnChangeSecurity_Click(object sender, EventArgs e)
        {
            ComponentValidator valid = new ComponentValidator();
            valid.addComponent(CboSecQuestion, ComponentValidator.NO_CUSTOM_TEXT_ALLOWED, true);
            valid.addComponent(txtSecurityAnswer, ComponentValidator.NO_FORMAT, true);
            if (valid.isAllComponenetValid())
            {
                UserManagment user = new UserManagment();
                user.update_SecurityQnA(CboSecQuestion.Text, txtSecurityAnswer.Text, Convert.ToInt32(Session["UserID"].ToString()));
                lblErr0.Text = "Updated Successfully.";
                reset();
                ttpUserSetting.Show();
            }
            else
            {
                ttpUserSetting.Show();
            }
        }*/
        protected static string CreatePasswordHash(string pwd, string salt)
        {
            byte[] tmpSource;
            byte[] tmpHash;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(pwd);
            tmpHash = new SHA1CryptoServiceProvider().ComputeHash(tmpSource);
            string hashedPwd = ByteArrayToString(tmpHash);
            return hashedPwd;
        }
        /// <summary>
        /// Create a byte array from source data
        /// </summary>
        /// <param name="arrInput"></param>
        /// <returns></returns>
        static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
        /// <summary>
        /// updates the update panel.
        /// </summary>
        public bool UpdateMessageMethod
        {
            set
            {
                UpdateMessage.Visible = true;
                UpdateMessage.Update();
            }
        }
        /// <summary>
        /// Displays the title of each node.
        /// </summary>
        public string SiteMapMethod
        {
            set
            {
                SiteMap.CurrentNode.Title = value;

            }
        }
        /// <summary>
        ///  updates the update panel.
        /// </summary>
        public bool updateLeftPanel
        {
            set
            {
                //((UpdatePanel)RightContent.FindControl("UpdateLeftPanel")).Visible=true;
                //((UpdatePanel)RightContent.FindControl("UpdateLeftPanel")).Update();
               
                UpdateLeftPanel.Visible = true;
                UpdateLeftPanel.Update();
            }
        }
  
        protected static string CreateSalt(int size)
        {
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
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