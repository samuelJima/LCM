using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class Tbl_Permission
    {
        /// <Summary>
        ///declare and instantiate LinqDataContext Object
        /// </Summary>
        FORMS_DB_DBMLDataContext User_LinqDataContext = new FORMS_DB_DBMLDataContext();
        /// <Summary>
        ///Insert Permission in to Database
        /// </Summary>
        public int AddNewPermission()
        {
            int result = User_LinqDataContext.spCreate_Permission(_Permission_Name, _Status);

            if (result > 0)
                return result;
            else
                return -2;
        }
        public Tbl_Permission[] SearchAllPermissionInfo()
        {
            var UserInfos = from c in User_LinqDataContext.Tbl_Permissions
                            select c;
            return UserInfos.ToArray<Tbl_Permission>();
        }
    }
}
