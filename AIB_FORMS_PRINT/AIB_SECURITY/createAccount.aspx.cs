using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIB_FORMS_LOGIC;
using System.Text;
using AIB_FORMS_VALIDATION;
using System.Security.Cryptography;
using AIB_FORMS_OB;
using Telerik.Web.UI;
using System.Drawing;

namespace AIB_FORMS_PRINT.AIB_SECURITY
{
    public partial class createAccount : System.Web.UI.Page
    {
       
        private const int CAN_SAVE = 1;
        private const int CAN_UPDATE = 2;
        private const int CAN_DELETE = 3;
        private const int CAN_VIEW = 4;
        private const int CAN_APPROVE = 5;
        Role_Management role;
        /// <summary>
        /// Bindes all components
        /// </summary>
        protected void BindStaticcomponents()
        {

            role = new Role_Management();
          cboRole.DataSource =  role.SearchAllRoles();
          cboRole.DataTextField = "Role_Name";
          cboRole.DataValueField = "Role_ID";
          cboRole.DataBind();

          role = new Role_Management();
          cmbDepartment.DataSource = role.SearchAllBranchs();
          cmbDepartment.DataTextField = "branchName";
          cmbDepartment.DataValueField = "branchid";
          cmbDepartment.DataBind();
            
        }
       
        /// <summary>
        /// 
        /// </summary>
        protected void BindDynamiccomponents()
        {
            UserManagment user = new UserManagment();
            cmbSearchUser.DataSource = user.SearchAllUserInfo(0);
            cmbSearchUser.DataTextField = "UserName";
            cmbSearchUser.DataValueField = "UserID";
            cmbSearchUser.DataBind();
            cmbDisabledAcc.DataSource = user.SearchAllUserInfo(1);
            cmbDisabledAcc.DataTextField = "UserName";
            cmbDisabledAcc.DataValueField = "UserID";
            cmbDisabledAcc.DataBind();

        }
        /// <summary>
        /// page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            
                if (!Page.IsPostBack)
                {
                     
                    if (Session["UserId"] != null)
                    {
                      BindStaticcomponents();
                      BindDynamiccomponents();
                      SetPermission();
                    }
                    else
                    {
                      // Response.Redirect("~/LoginPage.aspx");
                    }
                }
           
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
        /// <summary>
        /// the method Sets available Permission for the current User
        /// </summary>
        public void SetPermission()
        {
                if (Session["Permissions"] != null)
                {
                    List<string> Permissions = new List<string>();
                    Permissions = (List<string>) Session["Permissions"];
                    if (Permissions != null)
                    {
                        foreach (string x in Permissions)
                        {
                            if (x.StartsWith("1"))
                            {
                               int SinglePermission = Convert.ToInt32(x.Split('/')[1]);
                               if (SinglePermission == CAN_SAVE)
                                {
                                    save.Enabled = true;
                                }
                               if (SinglePermission == CAN_UPDATE)
                               {
                                   btnUpdate.Enabled = true;
                               }
                               if (SinglePermission == CAN_DELETE)
                               {
                                   btnDelete.Enabled = true;
                               }
                            }
                        }
                    }
                }
        }
        /// <summary>
        /// btnDelete Event Handler: delete a record from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)//Activate
        {
            
            try
            {

                UserManagment users = new UserManagment();
                users.DeleteUserInfo(Convert.ToInt32(SelectedUser.Value), 0);
                BindDynamiccomponents();
                this.MessageDivComfirmation = true;
                this.MessageRadTickerComf = true;
                this.MessageConfirmationBg = Color.LightGreen;
                this.MessageLabel = "The user account has been activated successfully!";
                ResetControls();
              //  Master.updateLeftPanel = true;
            }
            catch(Exception)
            {
            }
            
        }
        protected static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }



        protected static string CreatePasswordHash(string pwd, string salt)
        {
            //string saltAndPwd = String.Concat(pwd, salt);

            /////////////////////////////////////////////////////////////////////////////////////
            //string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;
            //sSourceData = "MySourceData";
            //Create a byte array from source data
            tmpSource = ASCIIEncoding.ASCII.GetBytes(pwd);
            //tmpSource = ASCIIEncoding.ASCII.GetBytes(pwd);

            //Compute hash based on source data
            //tmpHash = new System.Security.Cryptography.HMACSHA1().ComputeHash(tmpSource);
            tmpHash = new SHA1CryptoServiceProvider().ComputeHash(tmpSource);
            //tmpHash=new SHA512CryptoServiceProvider().ComputeHash(tmpSource);
            //tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            string hashedPwd = ByteArrayToString(tmpHash);
            //hashedPwd = String.Concat(hashedPwd, salt);


            /////////////////////////////////////////////////////////////////////////////////////
            //string hashedPwd = saltAndPwd.GetHashCode().ToString();
            //string hashedPwd =
            //      FormsAuthentication.HashPasswordForStoringInConfigFile(
            //                                           saltAndPwd, "SHA1");

            //string hashedPwd = new MD5CryptoServiceProvider().ComputeHash(saltAndPwd);



            //hashedPwd = String.Concat(hashedPwd, salt);

            return hashedPwd;
        }


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
        /// the method sets all controls to the default
        /// </summary>
        private void ResetControls()
        {
            btnUpdate.Visible = false;
            save.Visible = true;
            btnDelete.Visible = false;
            btnActivate.Visible = false;
            txtConfirmPassword.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtUserFName.Text = "";
            txtUserLName.Text = "";
            txtUserName.Text = "";
            txtUserMName.Text = "";
            cboRole.SelectedIndex = -1;
            cboRole.Text = "";
            cmbDepartment.SelectedIndex = -1;
            cmbDepartment.Text = "";
            txtPassword.Enabled = true;
            cmbDisabledAcc.SelectedIndex = -1;
            cmbSearchUser.SelectedIndex = -1;
            cmbDisabledAcc.Text = "";
            cmbSearchUser.Text = "";
            txtConfirmPassword.Enabled = true;
        }
        /// <summary>
        ///  save event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void save_Click(object sender, EventArgs e)
        {
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtEmail, ComponentValidator.EMAIL, true);
            valids.addComponent(txtUserFName, ComponentValidator.FULL_NAME, true);
            valids.addComponent(txtUserMName, ComponentValidator.FULL_NAME, true);
            valids.addComponent(txtUserLName, ComponentValidator.FULL_NAME, true);
            valids.addComponent(cboRole, ComponentValidator.NO_CUSTOM_TEXT_ALLOWED, true);
            valids.addComponent(txtUserName, ComponentValidator.NO_FORMAT,2,15, true);
            valids.addComponent(txtPassword, ComponentValidator.PASSWORD, true);
            valids.addComponent(txtConfirmPassword, ComponentValidator.PASSWORD, true);
            if (valids.isAllComponenetValid())
            {
                if (txtConfirmPassword.Text == txtPassword.Text)
                {
                    try
                    {

                        UserManagment users = new UserManagment();
                        int saltSize = 5;
                        string salt = CreateSalt(saltSize);
                        string passwordHash = CreatePasswordHash(txtPassword.Text, salt);
                        users.CreateNewUser(Convert.ToInt32(cmbDepartment.SelectedValue), txtEmail.Text, txtUserFName.Text, txtUserMName.Text, txtUserLName.Text, passwordHash, "", "", txtUserName.Text, Convert.ToInt32(cboRole.SelectedValue));
                        BindDynamiccomponents();
                        ResetControls();
                        this.MessageDivComfirmation = true;
                        this.MessageRadTickerComf = true;
                        this.MessageConfirmationBg = Color.Green;
                        this.MessageLabel = "User Account sucessfully created";
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("UNIQUE"))
                        {
                            this.MessageDivComfirmation = true;
                            this.MessageRadTickerComf = true;
                            this.MessageConfirmationBg = Color.Red;
                            this.MessageLabel = "User Name already exists!";
                        }
                    }
                    }

            }
           
           // Master.updateLeftPanel = true;
        }    
        /// <summary>
        /// cmbSearchUser event Handler: Searches Active user and populates on the form
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        protected void cmbSearchUser_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbSearchUser.SelectedValue != "")
            {

                UserManagment users = new UserManagment();
                Tbl_User[] Users = users.SearchUserById(Convert.ToInt32(cmbSearchUser.SelectedValue));
                SelectedUser.Value = cmbSearchUser.SelectedValue;
                ResetControls();
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                txtEmail.Text = Users[0].Email;
                txtUserName.Text = Users[0].UserName;
                txtUserLName.Text = Users[0].Last_Name;
                txtUserFName.Text = Users[0].First_Name;
                txtUserMName.Text = Users[0].Middle_Name;
                cboRole.SelectedValue = Users[0].RoleID.ToString();
                cmbDepartment.SelectedValue = Users[0].Dept_ID.ToString();
                btnUpdate.Visible = true;
                btnUpdate.Enabled = true;
                save.Visible = false;
                btnDelete.Visible = true;
                Master.UpdateMessageMethod = true;
            }
        }
        /// <summary>
        /// cmbDisabledSearchUser event Handler: Searches Inactive user and populates on the form
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        /// <summary>
        /// Resets all components to the default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            //ResetControls();
            //Master.UpdateMessageMethod = true;
        }
       /// <summary>
        /// btnDelete Event handler: removes all privillages from the user
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void btnDelete_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            Session["btnText"] = e.Item.Text;
            //Session["DeleteConfirm"] = 0;
            ttpConfirm.Show();
            
        }
        /// <summary>
        /// removes all privillages from the user
        /// </summary>
        public void DeleteChild()
        {
            
            try
            {
                if (Session["btnText"].ToString() == "Permanently")
                {
                    UserManagment users = new UserManagment();
                    users.DeleteUserInfo(Convert.ToInt32(SelectedUser.Value),2);
                    BindDynamiccomponents();
                    this.MessageDivComfirmation = true;
                    this.MessageRadTickerComf = true;
                    this.MessageConfirmationBg = Color.LightGreen;
                    this.MessageLabel = "The user account has been deactivated permanently.";
                }
                else if (Session["btnText"].ToString() == "Temporary")
                {
                    UserManagment users = new UserManagment();
                    users.DeleteUserInfo(Convert.ToInt32(SelectedUser.Value), 1);
                    BindDynamiccomponents();
                    this.MessageDivComfirmation = true;
                    this.MessageRadTickerComf = true;
                    this.MessageConfirmationBg = Color.LightGreen;
                    this.MessageLabel = "The user account has been deactivated temporarly.";
                }
                ResetControls();
                Master.updateLeftPanel = true;
            }
            catch (Exception )
            {
            }
            
        }

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            ResetControls();
          //  Master.UpdateMessageMethod = true;
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            DeleteChild();
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            ResetControls();
           // Master.UpdateMessageMethod = true;
        }

        protected void cmbDisabledAcc_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbDisabledAcc.SelectedValue != "")
            {
                SelectedUser.Value = cmbDisabledAcc.SelectedValue;
                UserManagment users = new UserManagment();
                Tbl_User[] Users = users.SearchUserById(Convert.ToInt32(SelectedUser.Value));
                ResetControls();
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                txtEmail.Text = Users[0].Email;
                txtUserName.Text = Users[0].UserName;
                txtUserLName.Text = Users[0].Last_Name;
                txtUserFName.Text = Users[0].First_Name;
                txtUserMName.Text = Users[0].Middle_Name;
                cboRole.SelectedValue = Users[0].RoleID.ToString();
                cmbDepartment.SelectedValue = Users[0].Dept_ID.ToString();
                btnUpdate.Visible = true;
                save.Visible = false;
                btnActivate.Visible = true;
                btnDelete.Visible = false;
                Master.UpdateMessageMethod = true;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtEmail, ComponentValidator.EMAIL, true);
            valids.addComponent(txtUserFName, ComponentValidator.FULL_NAME, true);
            valids.addComponent(txtUserMName, ComponentValidator.FULL_NAME, true);
            valids.addComponent(txtUserLName, ComponentValidator.FULL_NAME, true);
            valids.addComponent(cboRole, ComponentValidator.NO_CUSTOM_TEXT_ALLOWED, true);
            valids.addComponent(txtUserName, ComponentValidator.NO_FORMAT, true);
            if (valids.isAllComponenetValid())
            {
                if (txtConfirmPassword.Text == txtPassword.Text)
                {
                    try
                    {

                        UserManagment users = new UserManagment();
                        users.UpdateUserInfo(long.Parse(SelectedUser.Value), txtEmail.Text, txtUserFName.Text, txtUserMName.Text, txtUserLName.Text, Convert.ToInt32(cmbDepartment.SelectedValue), txtUserName.Text, Convert.ToInt32(cboRole.SelectedValue));
                        BindDynamiccomponents();
                        ResetControls();
                        this.MessageDivComfirmation = true;
                        this.MessageRadTickerComf = true;
                        this.MessageConfirmationBg = Color.LightGreen;
                        this.MessageLabel = "User Account is Successfully Updated!";
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("UNIQUE"))
                        {
                            this.MessageDivComfirmation = true;
                            this.MessageRadTickerComf = true;
                            this.MessageConfirmationBg = Color.Red;
                            this.MessageLabel = "User Name already exists!";
                        }
                    }
                }
               
               // Master.updateLeftPanel = true;
            }
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