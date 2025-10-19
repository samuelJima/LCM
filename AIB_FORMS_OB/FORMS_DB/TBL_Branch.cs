using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class TBL_Branch
    {
        /// <Summary>
        ///declare and instantiate LinqDataContext Object
        /// </Summary>
        FORMS_DB_DBMLDataContext User_LinqDataContext;


        public List<TBL_Branch> selectBranchByVode()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext()) {
                var result = from brch in User_LinqDataContext.TBL_Branches
                             where brch.branchid == _branchid
                             select brch;
                return result.ToList<TBL_Branch>();
            }
        }

        public bool saveBranch(string branchName, string branchAbrivation, string branchCode)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext()) {
                int result = User_LinqDataContext.saveBranchs(branchName, branchAbrivation,branchCode);
                if (result == 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public bool updateBranch(int branchId, string branchName, string branchAbrivation,string branchCode)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                int result = User_LinqDataContext.updateBranchs(branchId, branchName, branchAbrivation, branchCode);
                if (result == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<TBL_Branch> selectAllBranch()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from brch in User_LinqDataContext.TBL_Branches
                             select brch;
                return result.ToList<TBL_Branch>();
            }
        }

       
    }
}
