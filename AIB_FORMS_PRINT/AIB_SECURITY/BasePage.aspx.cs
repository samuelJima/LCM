using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIB_FORMS_PRINT.AIB_SECURITY
{
    public partial class BasePage : System.Web.UI.Page
    {
        
             /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}



        /// <summary>
        /// The name of the culture selection dropdown list in the common header.
        /// </summary>
        //public const string LanguageDropDownName = "ctl00$cphHeader$Header1$ddlLanguage";


        public const string LanguageDropDownName = "ctl00$ddlLanguage";
        public const string LanguageLinkButtonAmharicName = "ctl00$ctl00$LinkButton1";
        public const string LanguageLinkButtonEnglishName = "ctl00$ctl00$LinkButton2";


        /// <summary>
        /// The name of the PostBack event target field in a posted form.  You can use this to see which
        /// control triggered a PostBack:  Request.Form[PostBackEventTarget] .
        /// </summary>
        public const string PostBackEventTarget = "__EVENTTARGET";

        /// <SUMMARY>
        /// Overriding the InitializeCulture method to set the user selected
        /// option in the current thread. Note that this method is called much
        /// earlier in the Page lifecycle and we don't have access to any controls
        /// in this stage, so have to use Form collection.
        /// </SUMMARY>
        protected override void InitializeCulture()
        {
            ///<remarks><REMARKS>
            ///Check if PostBack occured. Cannot use IsPostBack in this method
            ///as this property is not set yet.
            ///</remarks>
            ///
            //if (Session["UserId"] == null)
            //{
            //    Server.Transfer("~/Account/Login.aspx");
            //}
            if (Request[PostBackEventTarget] != null)
            {
                string controlID = Request[PostBackEventTarget];


                //if (controlID.Equals(LanguageDropDownName))
                //Session["Culture"] = "am-ET";
                SetCulture("am-ET", "am-ET");

                if (controlID.Equals(LanguageLinkButtonAmharicName))
                {
                    //Session["Culture"] = "am-ET";
                    //string selectedValue =
                    //       Request.Form[Request[PostBackEventTarget]].ToString();

                    //switch (selectedValue)
                    //{
                    //    case "0": SetCulture("hi-IN", "hi-IN");
                    //        break;
                    //    case "1": SetCulture("en-US", "en-US");
                    //        break;
                    //    case "2": SetCulture("en-GB", "en-GB");
                    //        break;
                    //    case "3": SetCulture("fr-FR", "fr-FR");
                    //        break;
                    //    case "4": SetCulture("am-ET", "am-ET");
                    //        break;
                    //    default: break;
                    //}

                    SetCulture("am-ET", "am-ET");


                }
                else if (controlID.Equals(LanguageLinkButtonEnglishName))
                {
                    //SetCulture("en-US", "en-US");
                    Session["Culture"] = "am-ET";
                }
            }
            ///<remarks>
            ///
            ///Get the culture from the session if the control is tranferred to a
            ///new page in the same application.
            ///</remarks>
            if (Session["MyUICulture"] != null && Session["MyCulture"] != null)
            {
                Thread.CurrentThread.CurrentUICulture = (CultureInfo)Session["MyUICulture"];
                Thread.CurrentThread.CurrentCulture = (CultureInfo)Session["MyCulture"];
            }
            base.InitializeCulture();
        }


        /// <Summary>
        /// Sets the current UICulture and CurrentCulture based on
        /// the arguments
        /// </Summary>
        /// <PARAM name="name"></PARAM>
        /// <PARAM name="locale"></PARAM>
        protected void SetCulture(string name, string locale)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(name);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(locale);
            ///<remarks>
            ///Saving the current thread's culture set by the User in the Session
            ///so that it can be used across the pages in the current application.
            ///</remarks>
            Session["MyUICulture"] = Thread.CurrentThread.CurrentUICulture;
            Session["MyCulture"] = Thread.CurrentThread.CurrentCulture;
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
        }
    }
