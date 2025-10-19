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
    public partial class TBL_LC_CORRESPONDENT
    {
        FORMS_DB_DBMLDataContext User_LinqDataContext;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TBL_LC_CORRESPONDENT> selectAllCorespondent()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext()) {

                var result = from lcBank in User_LinqDataContext.TBL_LC_CORRESPONDENTs
                          
                             select lcBank;
                return result.ToList<TBL_LC_CORRESPONDENT>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TBL_CURRENCY> selsectCurrencyByCorrespondentId()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from cur in User_LinqDataContext.TBL_CURRENCies
                             select cur;
                //where cur.id == _id
                return result.ToList<TBL_CURRENCY>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         public List<VW_CORRESPONDNCE_CURRENCY> selsectCurrencyByCorrespondentIdNew()
           {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from cur in User_LinqDataContext.VW_CORRESPONDNCE_CURRENCies
                             where cur.id == _id
                             select cur;
              
                return result.ToList<VW_CORRESPONDNCE_CURRENCY>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankName"></param>
        /// <returns></returns>
         public int searchrmbankByName(string bankName)
         {
             using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
             {
                 var result = from rmbank in User_LinqDataContext.TBL_LC_CORRESPONDENTs
                              where rmbank.correspondentName.Equals(bankName)
                              select rmbank;
                 return result.ToList<TBL_LC_CORRESPONDENT>().Count;
             }
         }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankName"></param>
        /// <param name="telephonNo"></param>
        /// <param name="faxNo"></param>
        /// <param name="BankCurrency"></param>
        /// <returns></returns>
         public bool saveRembursinghBank(string bankName, string telephonNo, string faxNo, System.Data.DataTable BankCurrency)
         {
             using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AIB_LC_DBConnectionString"].ConnectionString))
             {
                 SqlCommand cmd = new SqlCommand("[saveRembursingBank]", connection);
                 cmd.CommandType = CommandType.StoredProcedure;
                 SqlParameter p1 = cmd.Parameters.AddWithValue("@bankName", bankName);
                 p1.SqlDbType = SqlDbType.NVarChar;
                 SqlParameter p2 = cmd.Parameters.AddWithValue("@telephoneNo", telephonNo);
                 p2.SqlDbType = SqlDbType.NVarChar;
                 SqlParameter p4 = cmd.Parameters.AddWithValue("@faxNumber", faxNo);
                 p4.SqlDbType = SqlDbType.NVarChar;
                 SqlParameter p3 = cmd.Parameters.AddWithValue("@currencyTable", BankCurrency);
                 p3.SqlDbType = SqlDbType.Structured;
                 p3.TypeName = "DT_REMBURCING_CURRENCY";
                 SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                 returnValue.Direction = ParameterDirection.ReturnValue;
                 cmd.Parameters.Add(returnValue);
                 connection.Open();
                 //    SqlDataReader reader = cmd.ExecuteReader();
                 cmd.ExecuteReader();
                 connection.Close();

                 int result = Convert.ToInt32(returnValue.Value);
                 if (result == 0)
                 {
                     return true;
                 }
                 else { 
                   return false;
                 }
                 
             }
         }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankName"></param>
        /// <param name="bankId"></param>
        /// <returns></returns>
         public int searchrmbankByName(string bankName, int bankId)
         {
             using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
             {
                 var result = from rmbank in User_LinqDataContext.TBL_LC_CORRESPONDENTs
                              where rmbank.correspondentName.Equals(bankName) && rmbank.id != bankId
                              select rmbank;
                 return result.ToList<TBL_LC_CORRESPONDENT>().Count;
             }
         }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankName"></param>
        /// <param name="telephonNo"></param>
        /// <param name="faxNo"></param>
        /// <param name="BankCurrency"></param>
        /// <param name="bankId"></param>
        /// <returns></returns>
         public bool updatRimbursingBank(string bankName, string telephonNo, string faxNo, DataTable BankCurrency, int bankId)
         {
             using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AIB_LC_DBConnectionString"].ConnectionString))
             {
                 SqlCommand cmd = new SqlCommand("[updateRembursingBank]", connection);
                 cmd.CommandType = CommandType.StoredProcedure;
                 SqlParameter p1 = cmd.Parameters.AddWithValue("@bankName", bankName);
                 p1.SqlDbType = SqlDbType.NVarChar;
                 SqlParameter p2 = cmd.Parameters.AddWithValue("@telephoneNo", telephonNo);
                 p2.SqlDbType = SqlDbType.NVarChar;
                 SqlParameter p4 = cmd.Parameters.AddWithValue("@faxNumber", faxNo);
                 p4.SqlDbType = SqlDbType.NVarChar;
                 SqlParameter p5 = cmd.Parameters.AddWithValue("@bankId", bankId);
                 p5.SqlDbType = SqlDbType.Int;
                 SqlParameter p3 = cmd.Parameters.AddWithValue("@currencyTable", BankCurrency);
                 p3.SqlDbType = SqlDbType.Structured;
                 p3.TypeName = "DT_REMBURCING_CURRENCY";
                 SqlParameter returnValue = new SqlParameter("returnVal", SqlDbType.Int);
                 returnValue.Direction = ParameterDirection.ReturnValue;
                 cmd.Parameters.Add(returnValue);
                 connection.Open();
                 //    SqlDataReader reader = cmd.ExecuteReader();
                 cmd.ExecuteReader();
                 connection.Close();

                 int result = Convert.ToInt32(returnValue.Value);
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

         public List<VW_CORRESPONDNCE_CURRENCY> selectCorespondentBankById(int bankId)
         {
             using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
             {
                 var result = from rmbank in User_LinqDataContext.VW_CORRESPONDNCE_CURRENCies
                              where rmbank.id==bankId
                              select rmbank;
                 return result.ToList<VW_CORRESPONDNCE_CURRENCY>();
             }
         }
    }
}
