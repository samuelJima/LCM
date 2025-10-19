using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
   public partial class VW_CORRESPONDNCE_CURRENCY
    {

       FORMS_DB_DBMLDataContext User_LinqDataContext;

        public string selectCorspondentAccount(int corespondentId, int currencyId)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from cocu in User_LinqDataContext.VW_CORRESPONDNCE_CURRENCies
                             where cocu.id == corespondentId && cocu.currencyId == currencyId
                             select cocu;
              List<VW_CORRESPONDNCE_CURRENCY>CoCu= result.ToList<VW_CORRESPONDNCE_CURRENCY>();
              if (CoCu.Count == 1)
              {
                  return CoCu[0].aibAccountNumber.ToString();
              }
              else {
                  return string.Empty;
              }
            }
        }

        public List<TBL_CURRENCY> selectCurrencybyCode(string currencyCode)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from cocu in User_LinqDataContext.TBL_CURRENCies
                             where cocu.currencyName == currencyCode 
                             select cocu;
                List<TBL_CURRENCY> CoCu = result.ToList<TBL_CURRENCY>();

                return CoCu;
               
            }
        }
    }
}
