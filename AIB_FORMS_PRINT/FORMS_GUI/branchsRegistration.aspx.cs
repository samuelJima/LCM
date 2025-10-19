using AIB_FORMS_LOGIC;
using AIB_FORMS_OB;
using AIB_FORMS_VALIDATION;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIB_FORMS_PRINT.FORMS_GUI
{
    public partial class branchsRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                this.populateBranch();
               
            }

        }

        private void populateBranch() {

            LC_Setting_Logic settLogic= new LC_Setting_Logic();
            List<TBL_Branch> branchs = new List<TBL_Branch>();
            branchs = settLogic.findAllBranchs();
            comboBranchName.DataValueField = "branchid";
            comboBranchName.DataTextField = "branchName";
            comboBranchName.DataSource = branchs;
            comboBranchName.DataBind();
        
        }
        protected void searchBranchByName(object sender, EventArgs e)
        {
            int branchid = Convert.ToInt32(comboBranchName.SelectedValue);
            Session["branchid"] = branchid.ToString();
            LC_Setting_Logic settLogic = new LC_Setting_Logic();
            List<TBL_Branch> branchs = new List<TBL_Branch>();
            branchs = settLogic.findBranchsById(branchid);
            if (branchs.Count == 1)
            {
                txtBranchName.Text = branchs[0].branchName;
                txtBranchAbrivation.Text = branchs[0].branchAbrivation;
                txtBranchCode.Text = branchs[0].branchCode;
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                btnReset.Visible = true;
            }
            else {

                this.errorMessage("There is no branch find by the given name");
            }
          
        }

        protected void saveBranch(object sender, EventArgs e) { 
             
             ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtBranchName, ComponentValidator.TEXT_A_TO_Z_ONLY, true);
            valids.addComponent(txtBranchAbrivation, ComponentValidator.TEXT_A_TO_Z_ONLY, true);
            valids.addComponent(txtBranchCode, ComponentValidator.TEXT_AND_NUMBER, true);
            if (valids.isAllComponenetValid())
            {
                LC_Setting_Logic settLogic= new LC_Setting_Logic();
                bool result = false;

                result = settLogic.addBranch(txtBranchName.Text, txtBranchAbrivation.Text,txtBranchCode.Text);
                if (result)
                {

                    this.postiveMessage("The Branch Saved Sucessfuly");

                }

                else {
                    this.errorMessage("The is not Saved Sucessfuly");
                }
            }
        }

        protected void updateBranch(object sender, EventArgs e)
        { 
            ComponentValidator valids = new ComponentValidator();
            valids.addComponent(txtBranchName, ComponentValidator.TEXT_A_TO_Z_ONLY, true);
            valids.addComponent(txtBranchAbrivation, ComponentValidator.TEXT_A_TO_Z_ONLY, true);

            if (valids.isAllComponenetValid())
            {
                if (Session["branchid"] != null)
                {
                    LC_Setting_Logic settLogic = new LC_Setting_Logic();
                    bool result = false;
                    int branchid = Convert.ToInt32(Session["branchid"]);
                    result = settLogic.editBranch(branchid,txtBranchName.Text, txtBranchAbrivation.Text,txtBranchCode.Text);
                    if (result)
                    {

                        this.postiveMessage("The Branch Detail Updated Sucessfuly");

                    }

                    else
                    {
                        this.errorMessage("The Branch is not updated Sucessfuly");
                    }
                }
                else
                {

                    this.errorMessage("A branch must be selected  for change");
                }
            }
           
        }
        protected void resetBranchInfo(object sender, EventArgs args) {
            txtBranchName.Text = "";
            txtBranchAbrivation.Text = "";
            txtBranchCode.Text = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnReset.Visible = false;

        }
        public void errorMessage(string message)
        {

            this.MessageDivComfirmation = true;
            this.MessageRadTickerComf = true;
            this.MessageConfirmationBg = Color.Red;
            this.MessageLabel = message;

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
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void postiveMessage(string message)
        {

            this.MessageDivComfirmation = true;
            this.MessageRadTickerComf = true;
            this.MessageConfirmationBg = Color.LightGreen;
            this.MessageLabel = message;

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