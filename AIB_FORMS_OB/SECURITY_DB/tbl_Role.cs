using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class Tbl_Role
    {
        /// <Summary>
        ///declare and instantiate LinqDataContext Object
        /// </Summary>
       FORMS_DB_DBMLDataContext User_LinqDataContext;
        /// <Summary>
        ///Insert Role in to Database
        /// </Summary>
        public int AddNewRole()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                int result = User_LinqDataContext.spCreat_Role(_Role_Name, _Status);

                if (result > 0)
                    return result;
                else
                    return -2;
            }
        }
        /// <Summary>
        ///Delete Role from Database 
        /// </Summary>
        public bool DeactivateRole()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                int result = User_LinqDataContext.spUpdate_Role(_Role_ID, _Role_Name, _Status);

                if (result > 0)
                    return true;
                else
                    return false;
            }
        }
        ///// <Summary>
        /////Update Role from Database 
        ///// </Summary>
        public bool UpdateRole()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                int result = User_LinqDataContext.spUpdate_Role(_Role_ID, _Role_Name, _Status);

                if (result > 0)
                    return true;
                else
                    return false;
            }
        }
        /// <Summary>
        ///Search all Role from Database
        /// </Summary>
        public Tbl_Role[] SearchAllRoleInfo()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var UserInfos = from c in User_LinqDataContext.Tbl_Roles
                                select c;
                return UserInfos.ToArray<Tbl_Role>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TBL_Branch[] SearchAllBranchInfo()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var UserInfos = from c in User_LinqDataContext.TBL_Branches
                                select c;
                return UserInfos.ToArray<TBL_Branch>();
            }
        }
        /// <Summary>
        ///Search  Role from Database
        /// </Summary>
        public Tbl_Role[] SearchRoleInfoByID()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var UserInfos = from c in User_LinqDataContext.Tbl_Roles
                                where c.Role_ID == _Role_ID
                                select c;
                return UserInfos.ToArray<Tbl_Role>();
            }
        }
    }
}
