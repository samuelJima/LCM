using AIB_FORMS_OB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_LOGIC.LC_LOGIC
{
    public class LCReportLogic
    {
        public double findThesumOfInvoice(string lcNumber)
        {
            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();
            return lc.sumOfInvoiceByLC_Number(lcNumber);
        }
    }
}
