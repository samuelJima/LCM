using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class TBL_LC_EXTENTION
    {
        FORMS_DB_DBMLDataContext User_LinqDataContext;
        public bool saveExtention(double extentionValue, int periods, DateTime extentionDate, int maxInvSequence, string lcNumber)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                int result = User_LinqDataContext.saveLCExtention(extentionValue, periods, extentionDate, maxInvSequence, lcNumber);
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

        public List<VW_LC_EXTENTION> selectLCExtention(DateTime extentionDate, string SearchLcInc)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from extView in User_LinqDataContext.VW_LC_EXTENTIONs
                             where extView.lcNumber == SearchLcInc && extView.extentionDate.Day == extentionDate.Day
                             && extView.extentionDate.Month == extentionDate.Month && extView.extentionDate.Year == extentionDate.Year
                             select extView;
                return result.ToList<VW_LC_EXTENTION>();
            }
        }

        public bool updateLCExtention(int id, int periods, string lcNumber)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                int result = User_LinqDataContext.updateExtention(id, periods, lcNumber);
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

        public bool countLCExtention(DateTime extentionDate, string lcNumber)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from extView in User_LinqDataContext.VW_LC_EXTENTIONs
                             where extView.lcNumber == lcNumber && extView.extentionDate.Day == extentionDate.Day
                             && extView.extentionDate.Month == extentionDate.Month && extView.extentionDate.Year == extentionDate.Year
                             select extView;
                if (result.ToList<VW_LC_EXTENTION>().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<TBL_LC_EXTENTION> findExtentionsForLC(string lcNumber)
        {
            using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
            {
                var result = from extView in User_LinqDataContext.TBL_LC_EXTENTIONs
                             where extView.extLcNumber == lcNumber 
                             select extView;
                return result.ToList<TBL_LC_EXTENTION>();
            }
        }
    }
}
