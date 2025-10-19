using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB

{
   public partial class TBL_OPEN_THROUGH_BANK
    {

        FORMS_DB_DBMLDataContext User_LinqDataContext;

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<TBL_OPEN_THROUGH_BANK> selectAllOpenThrough()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {

                var result = from lcBank in User_LinqDataContext.TBL_OPEN_THROUGH_BANKs

                             select lcBank;
                return result.ToList<TBL_OPEN_THROUGH_BANK>();
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="bankName"></param>
       /// <returns></returns>
        public int searchOPbankByName(string bankName)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from opbank in User_LinqDataContext.TBL_OPEN_THROUGH_BANKs
                             where opbank.bankName.Equals(bankName)
                             select opbank;
                return result.ToList<TBL_OPEN_THROUGH_BANK>().Count;
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="bankName"></param>
       /// <param name="BICCode"></param>
       /// <param name="countryId"></param>
       /// <param name="city"></param>
       /// <returns></returns>
        public bool saveOpenThroughBank(string bankName, string BICCode, int countryId, string city)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = User_LinqDataContext.saveOpenThroughBank(bankName, BICCode, countryId, city);
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
       /// <param name="BICCode"></param>
       /// <param name="countryId"></param>
       /// <param name="city"></param>
       /// <param name="bankId"></param>
       /// <returns></returns>
        public bool updatOpenThroughBank(string bankName, string BICCode, int countryId, string city, int bankId)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = User_LinqDataContext.updateOpenThroughBank(bankName, BICCode, countryId, city, bankId);
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

        public int searchOPbankByName(string bankName, int bankId)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from opbank in User_LinqDataContext.TBL_OPEN_THROUGH_BANKs
                             where opbank.bankName.Equals(bankName) && opbank.opId != bankId
                             select opbank;
                return result.ToList<TBL_OPEN_THROUGH_BANK>().Count;
            }
        }

        public List<TBL_OPEN_THROUGH_BANK> selectOpenThroughBYId(int bankId)
        {
             using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from opbank in User_LinqDataContext.TBL_OPEN_THROUGH_BANKs
                             where opbank.opId == bankId
                             select opbank;
                return result.ToList<TBL_OPEN_THROUGH_BANK>();
            }
        }
    }
}
