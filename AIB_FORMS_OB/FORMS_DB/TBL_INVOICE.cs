using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class TBL_INVOICE
    {
        FORMS_DB_DBMLDataContext User_LinqDataContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lcNumber"></param>
        /// <param name="lcStatus"></param>
        /// <param name="invoiceTable"></param>
        /// <returns></returns>
        public int saveAdvicesight(string lcNumber, string lcStatus, double totalInvoice, DataTable invoiceTable)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AIB_LC_DBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("saveLCAdviceSight", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter p1 = cmd.Parameters.AddWithValue("@lcNumber", lcNumber);
                p1.SqlDbType = SqlDbType.NVarChar;
                SqlParameter p2 = cmd.Parameters.AddWithValue("@lcStatus", lcStatus);
                p2.SqlDbType = SqlDbType.NVarChar;
                SqlParameter p4 = cmd.Parameters.AddWithValue("@totalInvoice", totalInvoice);
                p4.SqlDbType = SqlDbType.Float;
                SqlParameter p3 = cmd.Parameters.AddWithValue("@invoiceTable", invoiceTable);
                p3.SqlDbType = SqlDbType.Structured;
                p3.TypeName = "DT_INVOICE4";
                SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);
                connection.Open();
                //    SqlDataReader reader = cmd.ExecuteReader();
                cmd.ExecuteReader();
                connection.Close();
                
                int result = Convert.ToInt32(returnValue.Value);
                return result;
            }
        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lcNumber"></param>
    /// <returns></returns>
     public List<VW_LC_WITH_ADVICE_SIGHT> selectLcAdviceByLC_Number(string lcNumber){
         using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
         {
             var result = from lc in User_LinqDataContext.VW_LC_WITH_ADVICE_SIGHTs
                          where lc.lcNumber == lcNumber && (lc.lcStatus.Equals("Opened") || lc.lcStatus.Equals("Adviced") || lc.lcStatus.Equals("OutDated")) && (lc.status.Equals("ACTIVE") || lc.status.Equals("Setteled") || lc.status.Equals("Advised"))
                     select lc;
            return result.ToList<VW_LC_WITH_ADVICE_SIGHT>();
         }
      }

     public List<VW_LC_WITH_ADVICE_SIGHT> selectLcsettledByLC_Number(string lcNumber)
     {
         using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
         {
             var result = from lc in User_LinqDataContext.VW_LC_WITH_ADVICE_SIGHTs
                          where lc.lcNumber == lcNumber && (lc.lcStatus.Equals("Adviced") || lc.lcStatus.Equals("OutDated")) && (lc.status.Equals("Advised") || lc.status.Equals("Setteled"))
                          select lc;
             return result.ToList<VW_LC_WITH_ADVICE_SIGHT>();
         }
     }

     public bool updateInterestBeginDate(DateTime? beginDate,int invoiceId)
     {
         using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
         {
             int result = User_LinqDataContext.updateInterestbeginDate(beginDate, invoiceId);
             if (result == 0)
             {
                 return true;
             }
             else {
                 return false;
             }
         }
     }

     public bool updateInterestStatus(string status, int invoiceId)
     {
         using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
         {
             int result = User_LinqDataContext.updateInvoiceStatus(status, invoiceId);
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

     public List<TBL_INVOICE> selectInvoiceByLC_Number(string lcNumber)
     {
         using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
         {
             var result = from lc in User_LinqDataContext.TBL_INVOICEs
                          where lc.invoiceLCNumber == lcNumber && (lc.status.Equals("ACTIVE"))
                          select lc;
             return result.ToList<TBL_INVOICE>();
         }
     }
    }
}
