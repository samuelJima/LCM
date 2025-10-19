using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class TBL_COUNTRY
    {
        FORMS_DB_DBMLDataContext context;

       /* public List<TBL_COUNTRY> selectAllCurrencies()
        {
            using (context = new FORMS_DB_DBMLDataContext()) {
                var result = from curr in context.TBL_COUNTRies
                             where curr.Currency != null
                             select curr;
                return result.ToList<TBL_COUNTRY>();
            }
        }*/
        public List<TBL_COUNTRY> selectAllCountry()
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                var result = from curr in context.TBL_COUNTRies
                             select curr;
                return result.ToList<TBL_COUNTRY>();
            }
        }
    }
}
