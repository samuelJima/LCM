using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class TBL_LC_PAYMENT_TYPE
    {
        /// <Summary>
        ///declare and instantiate LinqDataContext Object
        /// </Summary>
        FORMS_DB_DBMLDataContext User_LinqDataContext;



        public List<TBL_LC_PAYMENT_TYPE> findPaymentTypeBycode()
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext()) {

                var result = from lcType in User_LinqDataContext.TBL_LC_PAYMENT_TYPEs
                             where lcType.typeCode == _typeCode
                             select lcType;
                return result.ToList<TBL_LC_PAYMENT_TYPE>();
            }
        }
    }
}
