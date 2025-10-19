using AIB_FORMS_LOGIC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIB_FORMS_OB;
using AIB_FORMS_VALIDATION;
using AIB_FORMS_PRINT;
using System.Drawing;


namespace AIB_FORMS_PRINT.AIB_SECURITY
{
    public partial class RoleManagement : System.Web.UI.Page
    {
        private const int CAN_SAVE = 1;
        private const int CAN_UPDATE = 2;
        private const int CAN_VIEW = 4;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                bindRoles();
                SetPermission();

            }
            BindGridView();
        }
        public void SetPermission()
        {
            if (Session["Permissions"] != null)
            {
                List<string> Permissions = new List<string>();
                Permissions = (List<string>)Session["Permissions"];
                if (Permissions != null)
                {
                    foreach (string x in Permissions)
                    {
                        if (x.StartsWith("3"))
                        {
                            int SinglePermission = Convert.ToInt32(x.Split('/')[1]);
                            if (SinglePermission == CAN_SAVE)
                            {
                                btnAddRole.Enabled = true;
                            }
                            if (SinglePermission == CAN_UPDATE)
                            {
                                save.Enabled = true;
                            }
                        }
                    }
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
        /// GridView2 Event Handler: makes enable and disable checkboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkBox;
                e.Row.Cells[0].Visible = false;
                if (e.Row.Visible == false)
                {
                }
                else
                {
                    int i;
                    for (i = 2; i < e.Row.Cells.Count; i++)
                    {
                        chkBox = new CheckBox();
                        chkBox.Enabled = true;
                        chkBox.ID = "id" + i.ToString();
                        e.Row.Cells[i].Controls.Add(chkBox);
                    }

                }

            }
        }

        protected void gridAccessLevels_DataBound(object sender, EventArgs e)
        {
            gridAccessLevels.HeaderRow.Cells[0].Visible = false;
        }

        /// <summary>
        /// Adds checkBox for GridView2's each cell
        /// </summary>
        /// <param name="StakHolderID"></param>
        private void BindGridView()
        {

            ResourceManagements resource = new ResourceManagements();
            PermissionManagment Permission = new PermissionManagment();
            Tbl_Resource[] Resources = resource.SearchAllResource();
            Tbl_Permission[] permission = Permission.SearchAllPermission();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.String"));
            dt.Columns.Add("Resources", Type.GetType("System.String"));
            DataColumn dc;
            dt.Rows.Add();
            for (int i = 0; i < permission.Length; i++)
            {
                dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = permission[i].Permission_Name;
                dt.Columns.Add(dc);
                dt.Rows[0][permission[i].Permission_Name] = permission[i].Permission_ID;
            }
            for (int r = 0; r < Resources.Length; r++)
            {
                dt.Rows.Add();
                dt.Rows[r + 1]["Resources"] = Resources[r].Resource_Name;
                dt.Rows[r + 1]["ID"] = Resources[r].Resource_ID;
            }
            gridAccessLevels.DataSource = dt;
            gridAccessLevels.DataBind();
            gridAccessLevels.Rows[0].Visible = false;
           
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            
            try
            {
                ComponentValidator valid = new ComponentValidator();
                valid.addComponent(txtRole, ComponentValidator.FULL_NAME, true);
                valid.addComponent(txtRemark, ComponentValidator.REMARK_COMMENT, false);
                if (valid.isAllComponenetValid())
                {
                    Role_Management rol = new Role_Management();
                    rol.AddNewRole(txtRole.Text, txtRemark.Text);
                    bindRoles();
                }
                else
                {
                    ttpRole.Show();
                }
            }
            catch (Exception)
            {
            }
        }
        void bindRoles()
        {

            Role_Management rol = new Role_Management();
            cboRole.DataSource = rol.SearchAllRoles();
            cboRole.DataTextField = "Role_Name";
            cboRole.DataValueField = "Role_ID";
            cboRole.DataBind();
        }
        void Reset()
        {
            
            save.Visible = false;
            cboRole.SelectedIndex = -1;
            cboRole.Text = "";
            txtRemark.Text = "";
            txtRole.Text = "";

            BindGridView();
        }

        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                Role_Management ObjRole = new Role_Management();
                GridViewRowEventArgs eRow;
                List<Dictionary<string, long>> ResourcePermit = new List<Dictionary<string, long>>();
                for (int k = 1; k < gridAccessLevels.Rows.Count; k++)
                {
                    Dictionary<string, long> dic = new Dictionary<string, long>();
                    eRow = new GridViewRowEventArgs(gridAccessLevels.Rows[k]);
                    for (int i = 2; i < eRow.Row.Cells.Count; i++)
                    {
                        CheckBox gv = (CheckBox)gridAccessLevels.Rows[k].FindControl("id" + i.ToString());
                        if (gv != null)
                        {

                            if (gv.Checked)
                            {

                                dic.Add(gridAccessLevels.Rows[0].Cells[i].Text, Convert.ToInt32(gridAccessLevels.Rows[k].Cells[0].Text));
                            }
                        }
                    }
                    ResourcePermit.Add(dic);
                }
                ObjRole.AssignResourceAndPermission(long.Parse(SelectedRole.Value), ResourcePermit);
                Reset();
                lblRole.Text = "";
                this.MessageDivComfirmation = true;
                this.MessageRadTickerComf = true;
                this.MessageConfirmationBg = Color.LightGreen;
                this.MessageLabel = "New Role Information is successfully saved!";
            }
            catch (Exception)
            {
            }
        }

        protected void cboRole_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            
            if (cboRole.SelectedValue != "")
            {
                SelectedRole.Value = cboRole.SelectedValue;
                lblRole.Text = cboRole.Text;
                Reset();

                save.Visible = true;
                Role_Management ObjRole = new Role_Management();
                GridViewRowEventArgs eRow;
                Tbl_RoleResourcePermission[] RolsResources = ObjRole.SearchRoleResource(int.Parse(SelectedRole.Value));
                if (RolsResources.Length > 0)
                {
                    for (int j = 1; j < gridAccessLevels.Rows.Count; j++)
                    {
                        eRow = new GridViewRowEventArgs(gridAccessLevels.Rows[j]);
                        ObjRole = new Role_Management();
                        Tbl_RoleResourcePermission[] RolsResourcesPemission = ObjRole.SearchRoleResourceId(long.Parse(gridAccessLevels.Rows[j].Cells[0].Text), long.Parse(SelectedRole.Value));
                        for (int l = 0; l < RolsResourcesPemission.Length; l++)
                        {
                            for (int i = 2; i < eRow.Row.Cells.Count; i++)
                            {
                                if (gridAccessLevels.Rows[0].Cells[i].Text == RolsResourcesPemission[l].Permission_ID.ToString())
                                {
                                    CheckBox gv = (CheckBox)gridAccessLevels.Rows[j].Cells[i].FindControl("id" + i.ToString());
                                    if (gv != null)
                                    {
                                        gv.Checked = true;
                                    }
                                    else
                                    {
                                        gv.Checked = false;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    BindGridView();
                }
                Master.UpdateMessageMethod = true;
            }
            
        }

        protected void btnAddNewRole_Click1(object sender, EventArgs e)
        {
            Reset();
            lblRole.Text = "";
            Master.UpdateMessageMethod = true;
            ComponentValidator valid = new ComponentValidator();
            valid.addComponent(txtRole, ComponentValidator.FULL_NAME, true);
            valid.addComponent(txtRemark, ComponentValidator.REMARK_COMMENT, false);
            valid.clearErrorIndication();
            ttpRole.Show();

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