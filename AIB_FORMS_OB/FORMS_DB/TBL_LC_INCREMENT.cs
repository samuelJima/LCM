using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class TBL_LC_INCREMENT
    {
        FORMS_DB_DBMLDataContext User_LinqDataContext;

        public bool saveLCIncrement(double incrementValue, int year, string permitcode, string lcNumber, DateTime incrementDate, double incRate)
        {

            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                int result = User_LinqDataContext.saveLCIncrementAmount(incrementValue, year, permitcode, incrementDate, incRate, lcNumber);
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

        public bool incrementParentLcValue(double incrementedAmount, string lcNumber)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                int result = User_LinqDataContext.incrementLCValue(incrementedAmount, lcNumber);
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

        public List<VW_LC_INCREMENTAMOUNT> selectLCIncrement(string txtSearchLcInc, int year, string permitCode)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from incView in User_LinqDataContext.VW_LC_INCREMENTAMOUNTs
                             where incView.lcNumber == txtSearchLcInc && incView.incrementPermitYear == year && incView.incrementPermitCode == permitCode
                             select incView;
                return result.ToList<VW_LC_INCREMENTAMOUNT>();
            }
        }

        public bool updateLCIncrement(double incrementAmount, int permitYear, string permitcode, string lcNumber, DateTime incValueDate, double incrate, int id)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                int result = User_LinqDataContext.updatelcIncrementAmount(incrementAmount, permitYear, permitcode, incValueDate, incrate, lcNumber, id);
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
    }
}
