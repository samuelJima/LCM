using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Configuration;

namespace AIB_FORMS_OB
{
   public partial class TBL_CURRENCY
    {
        public void selectCurrencyByValueDate(System.Data.DataTable curuncyList, DateTime valuedate)
        {
            using (AIBSITEntities cont = new AIBSITEntities())
            {
                try
                {
                    List<EXCHANGERATESHISTORY> ExchangeRate = new List<EXCHANGERATESHISTORY>();
                    //var result = (from mds in cont.EXCHANGERATESHISTORies
                    //where EntityFunctions.TruncateTime(mds.DATELASTMODIFIED.Value.Day) == valuedate.Day
                    //            select mds);
                    var result1 = cont.EXCHANGERATESHISTORies.Where(x => EntityFunctions.TruncateTime(x.DATELASTMODIFIED) == valuedate.Date && (x.EXCHANGERATETYPE== "SPOT"));

                    if (result1.Count() > 0)
                    {
                        ExchangeRate = result1.ToList<EXCHANGERATESHISTORY>();
                    }
                    foreach (var item in ExchangeRate)
                    {
                        curuncyList.Rows.Add(0, item.FROMCURRENCYCODE, item.TOCURRENCYCODE, item.EXCHANGERATETYPE, item.RATE, item.DATELASTMODIFIED);
                    }
                }
                catch (Exception exp) { 
                   
                }
               
            }
        }

        public int insertNewCurrencyRate(DateTime valueDate, DataTable curuncyList)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AIB_LC_DBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("saveCurrencyRate", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter p1 = cmd.Parameters.AddWithValue("@valueDate", valueDate);
                p1.SqlDbType = SqlDbType.Date;
                SqlParameter p3 = cmd.Parameters.AddWithValue("@currencyTable", curuncyList);
                p3.SqlDbType = SqlDbType.Structured;
                p3.TypeName = "DT_CURRENCY";
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

        public int updateNewCurrencyRate(DateTime valueDate, DataTable curuncyList)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AIB_LC_DBConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("[upDateCurrencyRate]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter p1 = cmd.Parameters.AddWithValue("@valueDate", valueDate);
                p1.SqlDbType = SqlDbType.Date;
                SqlParameter p3 = cmd.Parameters.AddWithValue("@currencyTable", curuncyList);
                p3.SqlDbType = SqlDbType.Structured;
                p3.TypeName = "DT_CURRENCY1";
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
    }
}
