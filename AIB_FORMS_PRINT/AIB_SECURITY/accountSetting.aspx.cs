using AIB_FORMS_LOGIC;
using AIB_FORMS_OB;
using AIB_FORMS_VALIDATION;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace AIB_FORMS_PRINT.AIB_SECURITY
{
    public partial class accountSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblErr0.Text = "";
                lblName.Text = Session["Name"].ToString();
                Email.Text = Session["Email"].ToString();
                lblUSerName.Text = Session["UserName"].ToString();
            }
        }
        protected void btnChangeSecurity_Click(object sender, EventArgs e)
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


               // ttpUserSetting.Show();
            }
            else
            {
               // ttpUserSetting.Show();
            }
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
                       // ttpUserSetting.Show();
                    }
                    else
                    {
                        lblErr0.Text = "The new password doesnt match with the confirm passowrd field.";
                        //ttpUserSetting.Show();
                    }
                }
                else
                {
                    lblErr0.Text = "The old password doesn't match with the stored password.";
                    //ttpUserSetting.Show();
                }
            }
            else
            {
                //ttpUserSetting.Show();
            }
        }
        private Tbl_User[] VerifyPassword(string suppliedPassword)
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
        protected static string CreatePasswordHash(string pwd, string salt)
        {
            byte[] tmpSource;
            byte[] tmpHash;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(pwd);
            tmpHash = new SHA1CryptoServiceProvider().ComputeHash(tmpSource);
            string hashedPwd = ByteArrayToString(tmpHash);
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
        void reset()
        {
            txtConfirmPassword.Text = "";
            txtNewPassword.Text = "";
            txtOldPassword.Text = "";
            txtSecurityAnswer.Text = "";
            CboSecQuestion.SelectedIndex = 1;
            CboSecQuestion.Text = "";
        }
    }
}