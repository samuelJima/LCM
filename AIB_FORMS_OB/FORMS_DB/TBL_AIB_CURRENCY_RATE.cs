using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
   public partial class TBL_AIB_CURRENCY_RATE
    {

       FORMS_DB_DBMLDataContext User_LinqDataContext;

       public List<TBL_AIB_CURRENCY_RATE> selectRateByValueDate(string currencytype)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                List<TBL_AIB_CURRENCY_RATE> result1 = new List<TBL_AIB_CURRENCY_RATE>();
               // var result =User_LinqDataContext.TBL_AIB_CURRENCY_RATEs.Where(x => x.valueDate.Value.Date == _valueDate.Value.Date && (x.fromCurrencyCode == _fromCurrencyCode) &&(x.exchangeRateType== _exchangeRateType));
                if (currencytype.Equals("SPOT"))
                {
                     var res= from rate in User_LinqDataContext.TBL_AIB_CURRENCY_RATEs
                                  where (rate.fromCurrencyCode =="ETB")&& (rate.toCurrencyCode ==_fromCurrencyCode) && (rate.exchangeRateType == "SPOT") && (rate.valueDate.Value.Year == _valueDate.Value.Year) && (rate.valueDate.Value.Month == _valueDate.Value.Month) &&
                                       (rate.valueDate.Value.Day == _valueDate.Value.Day)
                                  select rate;
                    result1 =res.ToList<TBL_AIB_CURRENCY_RATE>();
                }
                else {
                    var res = from rate in User_LinqDataContext.TBL_AIB_CURRENCY_RATEs
                              where (rate.fromCurrencyCode == _fromCurrencyCode) && (rate.toCurrencyCode == "ETB") && (rate.exchangeRateType == "SPOT") && (rate.valueDate.Value.Year == _valueDate.Value.Year) && (rate.valueDate.Value.Month == _valueDate.Value.Month) &&
                                       (rate.valueDate.Value.Day == _valueDate.Value.Day)
                                  select rate;
                      result1 =res.ToList<TBL_AIB_CURRENCY_RATE>();
                }
                return result1;
            }
        }
    }
}
