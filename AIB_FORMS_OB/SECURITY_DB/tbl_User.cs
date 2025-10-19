using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class Tbl_User
    {
        /// <Summary>
        /// declare and instantiate LinqDataContext Object
        /// </Summary>
      FORMS_DB_DBMLDataContext User_LinqDataContext = new FORMS_DB_DBMLDataContext();
        /// <Summary>
        /// declare and instantiate LinqDataContext Object
        /// </Summary>
        public int AddNewUser()
        {
            int result = User_LinqDataContext.spCreate_User(_UserName, _Password, _First_Name, _Middle_Name, _Last_Name, _Dept_ID, _Security_Question, _Security_Answer, _Status, _RoleID, _Email, _IsNew);

            if (result > 0)
                return result;
            else
                return -2;
        }
        /// <Summary>
        /// Update Password
        /// </Summary>
        public bool Update_password()
        {
            int result = User_LinqDataContext.spUpdate_Password(_UserID, _Password);

            if (result > 0)
                return true;
            else
                return false;
        }
        /// <Summary>
        ///Update First time access
        /// </Summary>
        public bool Update_FirstTimeAccess()
        {
            int result = User_LinqDataContext.spUpdate_FirstTimeAccess(_UserID, _IsNew);

            if (result > 0)
                return true;
            else
                return false;
        }
        ///// <Summary>
        ///// Update userInfo
        ///// </Summary>
        public void UpdateUser()
        {
            User_LinqDataContext.spUpdate_User(_UserID, _UserName, _First_Name, _Middle_Name, _Last_Name, _Dept_ID, _Status, _RoleID, _Email);


        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        public bool UpdateUserStatus()
        {
            int result = User_LinqDataContext.spUpdate_UserStatus(_UserID, _Status);

            if (result > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserSecurityQA()
        {
            int result = User_LinqDataContext.spUpdate_SecurityQuestion(_UserID, _Security_Question, _Security_Answer);

            if (result > 0)
                return true;
            else
                return false;
        }
        /// <Summary>
        /// search all users where security flag disabled
        /// </Summary>
        //public Tbl_User[] SearchUserSecurityFlagDisabled()
        //{

        //    var UserInfos = (from a in
        //                        (from c in User_LinqDataContext.tbl_Users
        //                          where c.Register_UserID == Register_UserID
        //                          select c)
        //                     where a.Security_Flag == _Security_Flag || a.Deleted_Status == _Deleted_Status
        //                    select a);
        //    return UserInfos.ToArray<tbl_User>();
        //}
        ///// <Summary>
        ///// search all users where security flag enabled
        ///// </Summary>
        //public Tbl_User[] SearchUserSecurityFlagEnabled()
        //{

        //    var UserInfos = (from a in
        //                         (from c in User_LinqDataContext.tbl_Users
        //                          where c.Register_UserID == Register_UserID
        //                          select c)
        //                     where a.Security_Flag == _Security_Flag && a.Deleted_Status == _Deleted_Status
        //                     select a);
        //    return UserInfos.ToArray<tbl_User>();
        //}
        /// <Summary>
        /// search all User info by RegisterUserID and deleted status
        /// </Summary>
        public Tbl_User[] SearchAllUserInfo()
        {
            var UserInfos = from c in User_LinqDataContext.Tbl_Users
                            where c.Status == _Status
                            select c;
            return UserInfos.ToArray<Tbl_User>();
        }
        /// <Summary>
        ///  search user by User Name and password and deleted status
        /// </Summary>
        public Tbl_User[] SearchAllUserLogin()
        {

            var UserInfos = from c in User_LinqDataContext.Tbl_Users
                            where c.UserName == _UserName && c.Password == _Password && c.Status == 0
                            select c;
            return UserInfos.ToArray<Tbl_User>();
        }
        /// <Summary>
        /// search user by User Name
        /// </Summary>
        public Tbl_User[] SearchUserLogin()
        {

            var UserInfos = from c in User_LinqDataContext.Tbl_Users
                            where c.UserName == _UserName
                            select c;

            return UserInfos.ToArray<Tbl_User>();
        }
        /// <Summary>
        /// search user by ID
        /// </Summary>
        public Tbl_User[] SearchUserID()
        {

            var UserInfos = from c in User_LinqDataContext.Tbl_Users
                            where c.UserID == _UserID
                            select c;
            return UserInfos.ToArray<Tbl_User>();
        }
      

    }
}
