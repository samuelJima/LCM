using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIB_FORMS_OB;

namespace AIB_FORMS_LOGIC
{
    public class PermissionManagment
    {
        /// <summary>
        /// Create  Permission object 
        /// </summary>
        Tbl_Permission permission;
        /// <summary>
        /// Search all Permissions 
        /// </summary>
        public Tbl_Permission[] SearchAllPermission()
        {
            permission = new Tbl_Permission();

            Tbl_Permission[] arry = permission.SearchAllPermissionInfo();

            return arry;


        }
        /// <summary>
        /// Save the Permission to the database
        /// </summary>
        /// <param name="Name"></param>
        public void AddNewPermission(string name)
        {
            permission = new Tbl_Permission();
            permission.Permission_Name = name;
            permission.AddNewPermission();

        }
    }
}
