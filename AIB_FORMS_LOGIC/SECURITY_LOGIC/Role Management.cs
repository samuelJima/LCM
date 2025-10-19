using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIB_FORMS_OB;

namespace AIB_FORMS_LOGIC
{
    public class Role_Management
    {
        #region Fields
        /// <summary>
        /// Create  Role object 
        /// </summary>
        Tbl_Role role;
        TBL_Branch branch;
        #endregion
        /// <summary>
        /// Search all Roles 
        /// </summary>
        public Tbl_Role[] SearchAllRoles()
        {
            role = new Tbl_Role();
            Tbl_Role[] arry = role.SearchAllRoleInfo();
            return arry;

        }
        public TBL_Branch[] SearchAllBranchs()
        {
            role = new Tbl_Role();
            TBL_Branch[] arry = role.SearchAllBranchInfo();
            return arry;

        }
        public void AssignResourceAndPermission(long Role_ID, List<Dictionary<string, long>> Resource)
        {
            Tbl_RoleResourcePermission RoleResource = new Tbl_RoleResourcePermission();
            RoleResource.Role_ID = Role_ID;
            if (RoleResource.DeleteResource_Permission())
            {
                foreach (Dictionary<string, long> dic in Resource)
                {
                    if (dic.Count != 0)
                    {
                        RoleResource = new Tbl_RoleResourcePermission();
                        RoleResource.Role_ID = Role_ID;
                        RoleResource.Resource_ID = dic.Values.First();
                        foreach (string keys in dic.Keys)
                        {
                            RoleResource.Permission_ID = long.Parse(keys);
                            int ID = RoleResource.AddNew_Resource_Permission();
                        }
                    }


                }
            }

        }
        /// <summary>
        /// Search Roles by  ID
        /// </summary>
        /// <param name="RoleID"></param>
        public Tbl_RoleResourcePermission[] SearchRoleResource(int RoleID)
        {
            Tbl_RoleResourcePermission role = new Tbl_RoleResourcePermission();
            role.Role_ID = RoleID;
            Tbl_RoleResourcePermission[] arry = role.SearchAllResource_PermissionByRole();
            return arry;
        }
        //public Tbl_RoleResourcePermission[] SearchRoleResource(int RoleID)
        //{
        //    Tbl_RoleResourcePermission role = new Tbl_RoleResourcePermission();
        //    role.Role_ID = RoleID;
        //    Tbl_RoleResourcePermission[] arry = role.SearchAllResource_PermissionByRole();
        //    return arry;
        //}
        //public List<long?> searchDistinctResources(int RoleID)
        //{
        //    Tbl_RoleResourcePermission role = new Tbl_RoleResourcePermission();
        //    role.Role_ID = RoleID;
        //    List<long?> arry = role.SearchDistinctResourceByRole();
        //    return arry;
        //}
        /// <summary>
        /// Search Roles by  ID
        /// </summary>
        /// <param name="RoleID"></param>
        public Tbl_Role[] SearchRoleId(int RoleID)
        {
            role = new Tbl_Role();
            role.Role_ID = RoleID;
            Tbl_Role[] arry = role.SearchRoleInfoByID();
            return arry;
        }
        /// <summary>
        /// Search Roles by  ID
        /// </summary>
        /// <param name="RoleID"></param>
        public Tbl_RoleResourcePermission[] SearchRoleResourceId(long? ResourceID, long? RoleID)
        {
            Tbl_RoleResourcePermission role = new Tbl_RoleResourcePermission();
            role.Role_ID = RoleID;
            role.Resource_ID = ResourceID;
            Tbl_RoleResourcePermission[] arry = role.SearchAllResource_PermissionByRoleResource();
            return arry;
        }
        /// <summary>
        /// save the role to the database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="remark"></param>
        public void AddNewRole(string name, string remark)
        {
            role = new Tbl_Role();
            role.Role_Name = name;
            role.AddNewRole();

        }
        //public void UpdateRoleStatus(string name, string remark)
        //{
        //    role = new Tbl_Role();
        //    role.Role_Name = name;
        //    role.DeactivateRole();

        //}
    }
}
