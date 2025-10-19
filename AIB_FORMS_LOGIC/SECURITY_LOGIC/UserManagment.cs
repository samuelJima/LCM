using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIB_FORMS_OB;

namespace AIB_FORMS_LOGIC
{
    public class UserManagment
    {
        #region field
        Tbl_User users;
        #endregion
        # region methods
        /// <summary>
        /// Inserts New User
        /// </summary>
        /// <param name="Company_Branch_ID"></param>
        /// <param name="Company_ID"></param>
        /// <param name="Email"></param>
        /// <param name="First_Name"></param>
        /// <param name="First_Time_Access"></param>
        /// <param name="Last_name"></param>
        /// <param name="password"></param>
        /// <param name="securityQuestion"></param>
        /// <param name="SecurityAnswer"></param>
        /// <param name="Stacholder"></param>
        /// <param name="UserName"></param>
        /// <param name="RUser"></param>
        /// <param name="roles"></param>
        /// <param name="Resource"></param>
        public void CreateNewUser(int DepartID, string Email,
            string First_Name, string MiddleName, string Last_name, string password,
            string securityQuestion, string SecurityAnswer, string UserName,
           int RoleID)
        {
            users = new Tbl_User();
            users.Email = Email;
            users.First_Name = First_Name;
            users.Middle_Name = MiddleName;
            users.IsNew = true;
            users.Last_Name = Last_name;
            users.Password = password;
            users.Security_Answer = SecurityAnswer;
            users.Security_Question = securityQuestion;
            users.Dept_ID = DepartID;
            users.UserName = UserName;
            users.RoleID = RoleID;
            users.Status = 0;
            int UserID = users.AddNewUser();



        }
        /// <summary>
        /// Updates User Info
        /// </summary>
        /// <param name="UsersID"></param>
        /// <param name="Company_Branch_ID"></param>
        /// <param name="Company_ID"></param>
        /// <param name="RUser"></param>
        /// <param name="Email"></param>
        /// <param name="First_Name"></param>
        /// <param name="Last_name"></param>
        /// <param name="securityQuestion"></param>
        /// <param name="SecurityAnswer"></param>
        /// <param name="Stacholder"></param>
        /// <param name="UserName"></param>
        /// <param name="roles"></param>
        /// <param name="Resource"></param>
        /// <param name="securityFlag"></param>
        /// <param name="deletedStatus"></param>
        public void UpdateUserInfo(long UserID, string Email, string First_Name, string MiddleName, string Last_name, int? DeptID, string UserName, int RoleID)
        {
            users = new Tbl_User();
            users.Email = Email;
            users.UserID = UserID;
            users.First_Name = First_Name;
            users.Middle_Name = MiddleName;
            users.Last_Name = Last_name;
            users.IsNew = true;
            users.Dept_ID = DeptID;
            users.RoleID = RoleID;
            users.UserName = UserName;
            users.Status = 0;
            users.UpdateUser();


        }
        /// <summary>
        /// Deletes User Info
        /// </summary>
        /// <param name="UsersID"></param>
        /// <param name="deletedStatus"></param>
        public void DeleteUserInfo(int UsersID, int deletedStatus)
        {
            users = new Tbl_User();
            users.UserID = UsersID;
            users.Status = deletedStatus;
            users.UpdateUserStatus();
        }
        public Tbl_User[] SearchUserByLoginDetails(string UserName)
        {
            users = new Tbl_User();

            users.UserName = UserName;
            return users.SearchUserLogin();
        }

        public Tbl_User[] SearchAllUserInfo(int deletedStatus)
        {
            users = new Tbl_User();
            users.Status = deletedStatus;
            return users.SearchAllUserInfo();
        }
        public Tbl_User[] SearchUserById(int ID)
        {
            users = new Tbl_User();

            users.UserID = ID;

            return users.SearchUserID();
        }
        public bool update_password(string password, int userID)
        {
            users = new Tbl_User();
            users.Password = password;
            users.UserID = userID;
            return users.Update_password();
        }
        public bool update_FirstTimeAccess(bool FirstTimeAccess, int userID)
        {
            users = new Tbl_User();
            users.UserID = userID;
            users.IsNew = FirstTimeAccess;
            return users.Update_FirstTimeAccess();
        }
        public bool update_SecurityQnA(string SecurityQuestion, string SecurityAnswer, int userID)
        {
            users = new Tbl_User();
            users.UserID = userID;
            users.Security_Answer = SecurityAnswer;
            users.Security_Question = SecurityQuestion;
            return users.UpdateUserSecurityQA();
        }

        /// <summary>
        /// //
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Status"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        //#region Method AddAuthenticatedLog;
        //public int addAuthenticationLog(int UserId, String Status, String Remark)
        //{
        //    userAuthenticationLog = new Tbl_User_Authentication_Log();
        //    userAuthenticationLog.User_Id = UserId;
        //    userAuthenticationLog.Status = Status;
        //    userAuthenticationLog.Remark = Remark;

        //    return userAuthenticationLog.AddNewAuthenticationLog();
        //}
        //public Tbl_User_Authentication_Log[] SelectAuthenticationLog(int UserId)
        //{
        //    userAuthenticationLog = new Tbl_User_Authentication_Log();
        //    userAuthenticationLog.User_Id = UserId;
        //    return userAuthenticationLog.SelectUserAuthenticationLogRecord();
        //}
        //#region Method SelectFirstUserAuthenticatedRecord;
        //public Tbl_User_Authentication_Log[] SelectFirstAuthenticationLog(int UserId)
        //{
        //    userAuthenticationLog = new Tbl_User_Authentication_Log();
        //    userAuthenticationLog.User_Id = UserId;
        //    return userAuthenticationLog.SelectFirstUserAuthenticatedRecord();
        //}
        //#endregion
        //public Tbl_User_Authentication_Log[] SelectLastAuthenticationLog(int UserId)
        //{
        //    userAuthenticationLog = new Tbl_User_Authentication_Log();
        //    userAuthenticationLog.User_Id = UserId;
        //    return userAuthenticationLog.SelectLastUserAuthenticatedRecord();
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <returns></returns>
        //public bool UpdateFirstRow(int UserId)
        //{
        //    userAuthenticationLog = new Tbl_User_Authentication_Log();
        //    userAuthenticationLog.User_Id = UserId;
        //    return userAuthenticationLog.UpdateFirstRowOfAUser();
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <param name="CircularLogCount"></param>
        ///// <returns></returns>
        //public bool UpdateFirstRowWithLessNo(int UserId, int? CircularLogCount)
        //{
        //    userAuthenticationLog = new Tbl_User_Authentication_Log();
        //    userAuthenticationLog.User_Id = UserId;
        //    userAuthenticationLog.Circular_Login_No = CircularLogCount;
        //    return userAuthenticationLog.UpdateFirstRowofAUserWithLessRecord();
        //}
        //#endregion

        /// <summary>
        /// New 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        //#region Method SelectAuthenticationLogByUserId;
        //public Tbl_User_Authentication_Log[] SelectAuthenticationLogByUserId(int UserId)
        //{
        //    userAuthenticationLog = new Tbl_User_Authentication_Log();
        //    userAuthenticationLog.User_Id = UserId;
        //    return userAuthenticationLog.SelectUserAuthenticationLogRecordByUserId();
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="UsersID"></param>
        ///// <param name="securityFlag"></param>
        ///// <returns></returns>
        //public bool UpdateUserSecurityFlag(int UsersID, int securityFlag)
        //{
        //    users = new Tbl_User();
        //    users.User_Id = UsersID;
        //    users.Security_Flag = securityFlag;
        //    return users.UpdateUserProtected();
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="securityFlag"></param>
        ///// <param name="deletedStatus"></param>
        ///// <param name="RegId"></param>
        ///// <returns></returns>
        //public Tbl_User[] SearchUsersBySecurityFlagEnabled(int securityFlag, bool deletedStatus, int RegId)
        //{
        //    users = new Tbl_User();
        //    users.Security_Flag = securityFlag;
        //    users.Deleted_Status = deletedStatus;
        //    users.Register_UserID = RegId;
        //    return users.SearchUserSecurityFlagEnabled();
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="securityFlag"></param>
        ///// <param name="deletedStatus"></param>
        ///// <param name="RegId"></param>
        ///// <returns></returns>
        //public Tbl_User[] SearchUsersBySecurityFlagDisabled(int securityFlag, bool deletedStatus, int RegId)
        //{
        //    users = new Tbl_User();
        //    users.Security_Flag = securityFlag;
        //    users.Deleted_Status = deletedStatus;
        //    users.Register_UserID = RegId;
        //    return users.SearchUserSecurityFlagDisabled();
        //}
        //#endregion
        #endregion
    }
}
