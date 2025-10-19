using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIB_FORMS_LOGIC;
using AIB_FORMS_OB;
using AIB_FORMS_VALIDATION;


namespace AIB_FORMS_PRINT.AIB_SECURITY
{
    public partial class Forgot_Password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
          private bool Verifyanswer(string suppliedUserName, string suppliedSQuetion, string suppliedSAnswer)
          {
              Tbl_User[] usernames;
              UserManagment user = new UserManagment();
              usernames = user.SearchUserByLoginDetails(suppliedUserName.Trim());
              if (usernames.Length != 0)
              {
                  if (suppliedSQuetion.StartsWith(usernames[0].Security_Question))
                  {

                      if (suppliedSAnswer == usernames[0].Security_Answer)
                      {
                          Session["User_Id"] = usernames[0].UserID;
                          return true;
                      }
                  }
              }
              return false;
          
          } 

          /// <summary>
          /// Creates Salt for password 
          /// </summary>
          /// <param name="size"></param>
          /// <returns></returns>
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
          /// <summary>
          /// Creates password hash
          /// </summary>
          /// <param name="pwd"></param>
          /// <param name="salt"></param>
          /// <returns></returns>
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
          /// converts Byte array to string
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

          protected void Submit_Click1(object sender, EventArgs e)
          {
              lblErr.Text = "";
              string pass1 = txtNewPassword.Text.Trim();
              string pass2 = txtConfirmPassword.Text.Trim();
              ComponentValidator valid2 = new ComponentValidator();
              valid2.addComponent(txtNewPassword, ComponentValidator.PASSWORD, true);
              valid2.addComponent(txtConfirmPassword, ComponentValidator.PASSWORD, true);
              if (valid2.isAllComponenetValid())
              {

                  if (pass1 == pass2)
                  {
                      string hashPass = CreatePasswordHash(pass1, " ");
                      UserManagment usr = new UserManagment();
                      usr.update_password(hashPass, Convert.ToInt32(Session["User_Id"].ToString()));
                      lblErr.Text = "The password has changed successfully!";
                      Reset();
                  }
                  else
                  {
                      lblErrPassword.Text = "Password Missmatch";
                      ttpConfirm.Show();
                  }
              }
              else
              {
                  ttpConfirm.Show();
              }
           
          } 

        protected void Reset()
        {
            txtUserName.Text = "";
            cboSecurityQuestions.SelectedIndex = -1;
            cboSecurityQuestions.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }

        protected void Submit_Click2(object sender, EventArgs e)
        {
           
            lblErr.Text = "";
            string username = txtUserName.Text;
            ComponentValidator valid = new ComponentValidator();
            valid.addComponent(txtUserName, ComponentValidator.NO_FORMAT, true);
            valid.addComponent(cboSecurityQuestions, ComponentValidator.NO_CUSTOM_TEXT_ALLOWED, true);
            valid.addComponent(txtSecurityAnswer, ComponentValidator.NO_FORMAT, true);
            if (valid.isAllComponenetValid())
            {
                if (username != "")
                {
                    if (Verifyanswer(username, cboSecurityQuestions.Text, txtSecurityAnswer.Text))
                    {
                        ttpConfirm.Show();
                    }
                    else
                    {
                        lblErr.Text = "Invalid User Name or Security Answer!";
                    }
                }
            }
        }
    }
}