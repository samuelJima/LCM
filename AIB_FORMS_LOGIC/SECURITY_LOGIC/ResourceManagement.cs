using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIB_FORMS_OB;

namespace AIB_FORMS_LOGIC
{
   public class ResourceManagements
    {

        /// <summary>
        /// Create  resource object 
        /// </summary>
        Tbl_Resource resource;
        /// Search all Resources 
        /// </summary>
        public Tbl_Resource[] SearchAllResource()
        {
            resource = new Tbl_Resource();

            Tbl_Resource[] arry = resource.SearchAllResourceInfo();

            return arry;


        }
        /// <summary>
        /// Search Resources by Resource ID
        /// </summary>
        /// <param name="ResourceID"></param>
        public Tbl_Resource[] SearchResourceByResourceID(int ResourceID)
        {
            resource = new Tbl_Resource();
            resource.Resource_ID = ResourceID;
            Tbl_Resource[] arry = resource.SearchResourceInfoByID();
            return arry;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="RID"></param>
        /// <returns></returns>
        public int AddNewResource(string Name, string Path, string Remark)
        {
            Tbl_Resource Resource = new Tbl_Resource();
            Resource.Resource_Name = Name;
            Resource.Description = Remark;
            Resource.Resource_Path = Path;
            Resource.Status = 0;
            return Resource.AddNewResource();

        }
    }
}
