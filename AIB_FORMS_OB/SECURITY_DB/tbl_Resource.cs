using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class Tbl_Resource
    {
        /// <Summary>
        /// declare and instantiate LinqDataContext Object
        /// </Summary>
       FORMS_DB_DBMLDataContext User_NLMSLinqDataContext = new FORMS_DB_DBMLDataContext();
        /// <Summary>
        ///Insert Resource  in to Databa
        /// </Summary>
        public int AddNewResource()
        {
            int result = User_NLMSLinqDataContext.spCreate_Resource(_Resource_Name, _Resource_Path, _Description, _Status);

            if (result > 0)
                return result;
            else
                return -2;
        }
        /// <Summary>
        /// Delete Resource from Database
        /// </Summary>
        public bool DeleteResource()
        {
            int result = User_NLMSLinqDataContext.spUpdate_Resource(_Resource_ID, _Resource_Name, _Resource_Path, _Description, _Status);

            if (result > 0)
                return true;
            else
                return false;
        }
        /// <Summary>
        /// Update Resource from Database
        /// </Summary>
        public bool UpdateResource()
        {
            int result = User_NLMSLinqDataContext.spUpdate_Resource(_Resource_ID, _Resource_Name, _Resource_Path, _Description, _Status);

            if (result > 0)
                return true;
            else
                return false;
        }
        /// <Summary>
        /// Search all Resource 
        /// </Summary>
        public Tbl_Resource[] SearchAllResourceInfo()
        {
            var UserInfos = from c in User_NLMSLinqDataContext.Tbl_Resources
                            select c;
            return UserInfos.ToArray<Tbl_Resource>();
        }
        /// <Summary>
        /// Search all Resource by ResourceID
        /// </Summary>
        public Tbl_Resource[] SearchResourceInfoByID()
        {
            var UserInfos = from c in User_NLMSLinqDataContext.Tbl_Resources
                            where c.Resource_ID == _Resource_ID
                            select c;
            return UserInfos.ToArray<Tbl_Resource>();
        }
    }
}
