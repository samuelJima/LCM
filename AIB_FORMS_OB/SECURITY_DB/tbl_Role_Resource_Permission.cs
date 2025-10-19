using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class Tbl_RoleResourcePermission
    {
        FORMS_DB_DBMLDataContext User_NLMSLinqDataContext = new FORMS_DB_DBMLDataContext();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int AddNew_Resource_Permission()
        {
            int result = User_NLMSLinqDataContext.spCreate_RoleResourcePermission(_Resource_ID, _Role_ID, _Permission_ID, _Status);

            if (result > 0)
                return result;
            else
                return -2;
        }

        public List<long?> SearchDistinctResourceByRole()
        {
            var UserInfos = (from c in User_NLMSLinqDataContext.Tbl_RoleResourcePermissions
                             where c.Role_ID == _Role_ID
                             select c.Resource_ID).Distinct();
            return UserInfos.ToList<long?>();
        }
        public bool DeleteResource_Permission()
        {
            var UserInfos = from c in User_NLMSLinqDataContext.Tbl_RoleResourcePermissions
                            where c.Role_ID == _Role_ID
                            select c;

            foreach (var detail in UserInfos)
            {
                User_NLMSLinqDataContext.Tbl_RoleResourcePermissions.DeleteOnSubmit(detail);
            }

            try
            {
                User_NLMSLinqDataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }

            return true;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Tbl_RoleResourcePermission[] SearchAllResource_PermissionByRole()
        {
            var UserInfos = from c in User_NLMSLinqDataContext.Tbl_RoleResourcePermissions
                            where c.Role_ID == _Role_ID
                            select c;
            return UserInfos.ToArray<Tbl_RoleResourcePermission>();
        }
        //public Tbl_RoleResourcePermission[] SearchAllResource_PermissionByRole()
        //{
        //    var UserInfos = from c in User_NLMSLinqDataContext.Tbl_RoleResourcePermissions
        //                    where c.Role_ID == _Role_ID
        //                    select c;
        //    return UserInfos.ToArray<Tbl_RoleResourcePermission>();
        //}
        public Tbl_RoleResourcePermission[] SearchAllResource_PermissionByRoleResource()
        {
            var UserInfos = from c in User_NLMSLinqDataContext.Tbl_RoleResourcePermissions
                            where c.Role_ID == _Role_ID && c.Resource_ID == _Resource_ID
                            select c;
            return UserInfos.ToArray<Tbl_RoleResourcePermission>();
        }
    }
}
