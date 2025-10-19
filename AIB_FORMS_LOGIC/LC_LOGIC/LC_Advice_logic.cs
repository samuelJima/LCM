using AIB_FORMS_OB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_LOGIC
{
    public class LC_Advice_logic
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lcNumber"></param>
        /// <param name="lcStatus"></param>
        /// <param name="invoiceTable"></param>
        /// <returns></returns>
        public bool addAdviceSight(string lcNumber, string lcStatus, double totalInvoice, DataTable invoiceTable)
        {
            TBL_INVOICE lc = new TBL_INVOICE();

            int result;
            result = lc.saveAdvicesight(lcNumber, lcStatus,totalInvoice,invoiceTable);
            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="lcNumber"></param>
          /// <returns></returns>
   
        public List<VW_LC_WITH_ADVICE_SIGHT> searchLcAdviceByLC_Number(string lcNumber)
        {
            TBL_INVOICE  invOb = new TBL_INVOICE() ;
            return invOb.selectLcAdviceByLC_Number(lcNumber);
        }

        public List<VW_LC_WITH_ADVICE_SIGHT> searchLcSetteledByLC_Number(string lcNumber)
        {
            TBL_INVOICE invOb = new TBL_INVOICE();
            return invOb.selectLcsettledByLC_Number(lcNumber);
        }

        public List<VW_DETAIL_LC> searchLcByLCNumber(string lcNumber)
        {
            TBL_LETTER_OF_CREDIT lcCredit = new TBL_LETTER_OF_CREDIT();
            return lcCredit.selectLcByLC_Number(lcNumber);
        }

        public List<TBL_INVOICE> searchInvoiceByLCNumber(string lcNumber)
        {
            TBL_INVOICE invOb = new TBL_INVOICE();
            return invOb.selectInvoiceByLC_Number(lcNumber);
        }

        public bool addExcessAmount(double excessAmount, int permitYear, string permitcode,DateTime invoiceDate, double invoiceRate, string lcNumber)
        {
            TBL_LC_EXCESS_AMOUNT exxAmount = new TBL_LC_EXCESS_AMOUNT();
            return exxAmount.saveExcessAmount(excessAmount, permitYear, permitcode,invoiceDate,invoiceRate,lcNumber);
        }

      
        public bool updateExcessAmount(double excessAmount, string lcNumber)
        {
            TBL_LC_EXCESS_AMOUNT exxAmount = new TBL_LC_EXCESS_AMOUNT();
            return exxAmount.updateExcessAmount(excessAmount, lcNumber);
        }

        public bool CheckEcessAmountExistance(string lcNumber)
        {
            TBL_LC_EXCESS_AMOUNT exxAmount = new TBL_LC_EXCESS_AMOUNT();
            return exxAmount.selectExcessAmount(lcNumber);
        }
    }
}
