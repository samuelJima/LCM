using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIB_FORMS_LOGIC;
using AIB_FORMS_OB;
using AIB_FORMS_VALIDATION;

namespace AIB_FORMS_PRINT.AIB_SECURITY
{
    public partial class login : BasePage
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       private Tbl_User[] VerifyPassword(string suppliedUserName,
                         string suppliedPassword)
        {
            bool passwordMatch = false;

            UserManagment user = new UserManagment();
            Tbl_User[] users = user.SearchUserByLoginDetails(UserName.Text);


            if (users.Length != 0)
            {

                if (users[0].Status == 1)
                {
                    lblFailureText.Text = "The user with username  " + users[0].UserName + "cannot " +
                    "access this system , so please consult the system admin.";
                    return null;

                }
                else if (users[0].Status == 2)
                {
                    lblFailureText.Text = "This user account has been deleted and is no longer available.";
                    return null;
                }
                else
                {
                    string dbPasswordHash = users[0].Password;
                    int saltSize = 5;
                    string salt = dbPasswordHash.Substring(dbPasswordHash.Length - saltSize);
                    string hashedPasswordAndSalt =CreatePasswordHash(suppliedPassword, salt);
                    passwordMatch = hashedPasswordAndSalt.Equals(dbPasswordHash);
                    if (passwordMatch)
                    {
                        //recordAuthenticationLog(users[0].User_Id, "0", "granted");
                        return users;

                    }
                    else
                    {
                        lblFailureText.Text = "Invalid password ! ";
                        //     recordAuthenticationLog(users[0].User_Id, "1", "denied");
                        //     if (isUserProtected(users[0].User_Id) == true)
                        //     {
                        //         setSecurityFlag(users[0].User_Id);
                        //         lblFailureText.Text = "The user with username  " + users[0].User_Name + "cannot " +
                        //"access this system , so please consult the system admin.";
                        //     }
                        return null;
                    }

                }
            }
            else
            {
                lblFailureText.Text = "Invalid user name! ";

                return null;

            }
        }
        /// <summary>
        /// btnForgotPassword event Handler: redirects to Forgetpassword.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ComponentValidator valid = new ComponentValidator();
                valid.addComponent(UserName, ComponentValidator.NO_FORMAT, true);
                valid.addComponent(Password, ComponentValidator.NO_FORMAT, true);
                if (valid.isAllComponenetValid())
                {
                    Tbl_User[] UserResult = VerifyPassword(UserName.Text, Password.Text);
                    if (UserResult != null)
                    {
                        RoleManagement rol = new RoleManagement();
                        Session["IsNew"] = UserResult[0].IsNew;
                        Session["UserID"] = UserResult[0].UserID;
                        Session["UserRole"] = UserResult[0].RoleID;
                        Session["Branch"] = UserResult[0].Dept_ID;
                        Session["Name"] = UserResult[0].First_Name + " " + UserResult[0].Middle_Name + " " + UserResult[0].Last_Name;
                        Session["Email"] = UserResult[0].Email;
                        Session["UserName"] = UserResult[0].UserName;
                        Response.Redirect("~/Default.aspx");
                    }
                }
            }
        }
    }
}