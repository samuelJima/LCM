using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
    public partial class TBL_LETTER_OF_CREDIT
    {
        FORMS_DB_DBMLDataContext context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public string selectMaximumLC_Code(int BranchCode)
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                string forDigit;
                string query = "select * from TBL_LETTER_OF_CREDIT where  lcCode = (SELECT MAX(lcCode) FROM TBL_LETTER_OF_CREDIT) and branchCode = " + BranchCode;
                var result = context.ExecuteQuery<TBL_LETTER_OF_CREDIT>(query);
                List<TBL_LETTER_OF_CREDIT> lccods = result.ToList<TBL_LETTER_OF_CREDIT>();
                if (lccods.Count == 1)
                {
                    forDigit = lccods[0].lcCode.ToString();
                }
                else
                {
                    int num = 0;
                    forDigit = num.ToString();
                    //forDigit = num.ToString("D4");
                }
                return forDigit;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permitCode"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public bool countPermitCode(string permitCode, int year)
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                bool ispermitCodeNotExist = false;
                string query = "select * from TBL_LETTER_OF_CREDIT where permitYear =" + year + " and permitCode = " + permitCode;
                var result = context.ExecuteQuery<TBL_LETTER_OF_CREDIT>(query);
                if (result.ToList<TBL_LETTER_OF_CREDIT>().Count == 0)
                {
                    ispermitCodeNotExist = true;
                }
                else
                {
                    ispermitCodeNotExist = false;
                }
                return ispermitCodeNotExist;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="permitCode"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public bool countPermitCodeforUpdate(string permitCode, int year, string lcNumber)
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                bool ispermitCodeNotExist = false;
                string query = "select * from TBL_LETTER_OF_CREDIT where permitYear =" + year + " and permitCode = " + permitCode;
                List<TBL_LETTER_OF_CREDIT> result = context.ExecuteQuery<TBL_LETTER_OF_CREDIT>(query).ToList<TBL_LETTER_OF_CREDIT>();
                if (result.Count == 0)
                {
                    ispermitCodeNotExist = true;
                }
                else if (result.Count == 1) 
                {
                    if (result[0].lcNumber.Equals(lcNumber))
                    {
                        ispermitCodeNotExist = true;
                    }
                  
                }
                else
                {
                    ispermitCodeNotExist = false;
                }
                return ispermitCodeNotExist;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="permitCode"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public bool countExcessPermitCode(string permitCode, int year)
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                bool ispermitCodeExist = false;
                string query = "select * from TBL_LC_EXCESS_AMOUNT where excessPermitYear =" + year + " and excessPermitCode = " + permitCode;
                var result = context.ExecuteQuery<TBL_LC_EXCESS_AMOUNT>(query);
                if (result.ToList<TBL_LC_EXCESS_AMOUNT>().Count == 0)
                {
                    ispermitCodeExist = true;
                }
                else
                {
                    ispermitCodeExist = false;
                }
                return ispermitCodeExist;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lcNumber"></param>
        /// <param name="LCcode"></param>
        /// <param name="vluedate"></param>
        /// <param name="permitCode"></param>
        /// <param name="permitYear"></param>
        /// <param name="lcValue"></param>
        /// <param name="currencyId"></param>
        /// <param name="currencyRate"></param>
        /// <param name="marginPaid"></param>
        /// <param name="CorrspId"></param>
        /// <param name="openThroughId"></param>
        /// <param name="supplyer"></param>
        /// <param name="branchCode"></param>
        /// <param name="CustomerAccount"></param>
        /// <param name="lcStatus"></param>
        /// <param name="openingDate"></param>
        /// <param name="customerName"></param>
        /// <param name="confirmationStatus"></param>
        /// <returns></returns>
        public bool insertLCDetail(string lcNumber, string LCcode, DateTime? vluedate, string permitCode, int permitYear, double lcValue, string currencyId, double currencyRate, double marginPaid, int CorrspId, int openThroughId, string supplyer, int branchCode, string CustomerAccount, string lcStatus, DateTime openingDate, string customerName, string confirmationStatus, string curencyType, double tollerance, string accountBranch, int periods, int userLimitationDays, int userId)
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                int result = context.saveLCOpening(lcNumber, LCcode, vluedate, permitCode, permitYear, lcValue, currencyId, currencyRate, marginPaid, CorrspId, openThroughId, supplyer, branchCode, CustomerAccount, lcStatus, openingDate, customerName, confirmationStatus, curencyType, tollerance, accountBranch, periods,userLimitationDays,userId);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lcNumber"></param>
        /// <returns></returns>
        public List<VW_DETAIL_LC> selectLcByLC_Number(string lcNumber)
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                var result = from lc in context.VW_DETAIL_LCs
                             where lc.lcNumber.Equals(lcNumber) && (lc.lcStatus.Equals("Opened") || lc.lcStatus.Equals("OutDated"))
                             select lc;
                return result.ToList<VW_DETAIL_LC>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lcNumber"></param>
        /// <param name="valueDate"></param>
        /// <param name="permitCode"></param>
        /// <param name="permitYear"></param>
        /// <param name="lcValue"></param>
        /// <param name="currencyID"></param>
        /// <param name="rate"></param>
        /// <param name="margenPaid"></param>
        /// <param name="rembBankId"></param>
        /// <param name="openThroughId"></param>
        /// <param name="supplyer"></param>
        /// <param name="accountNo"></param>
        /// <param name="customerName"></param>
        /// <param name="confirmation"></param>
        /// <returns></returns>
        public bool updateLcDetail(string lcNumber, DateTime? valueDate, string permitCode, int permitYear, double lcValue, string currencyID, double rate, double margenPaid, int rembBankId, int openThroughId, string supplyer, string accountNo, string customerName, string confirmation, string curencyType, double tollerance, string accountBranch, int periods, int limitedDays)
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                int result = context.updateLCOpening(lcNumber, valueDate, permitCode, permitYear, lcValue, currencyID, rate, margenPaid, rembBankId, openThroughId, supplyer, accountNo, customerName, confirmation, curencyType, tollerance, accountBranch, periods,limitedDays);

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


        public double sumOfInvoiceByLC_Number(string lcNumber)
        {

            using (context = new FORMS_DB_DBMLDataContext())
            {
                double sum;

                var count = context.TBL_INVOICEs.Count(me => me.invoiceLCNumber == lcNumber && (me.status == "ACTIVE" || me.status == "Advised" || me.status == "Setteled"));
                    if (count > 0)
                    {

                        var value = context.TBL_INVOICEs
                                .Where(t => t.invoiceLCNumber == lcNumber && (t.status == "Setteled" || t.status == "ACTIVE" || t.status == "Advised"))
                                 .Select(t => t.invoiceValue ?? 0).Sum();


                        sum = Convert.ToDouble(value);

                    }
                    else
                    {
                        sum = 0;
                    }
               
                return sum;
            }
        }
        public double sumOfInvoiceByLC_Number(string lcNumber,int invoiceId)
        {

            using (context = new FORMS_DB_DBMLDataContext())
            {
                double sum;
                if (invoiceId == 0)
                {
                    var count = context.TBL_INVOICEs.Count(me => me.invoiceLCNumber == lcNumber && (me.status == "ACTIVE" || me.status == "Advised" || me.status == "Setteled"));
                    if (count > 0)
                    {

                        var value = context.TBL_INVOICEs
                                .Where(t => t.invoiceLCNumber == lcNumber && (t.status == "Setteled" || t.status == "ACTIVE" || t.status == "Advised"))
                                 .Select(t => t.invoiceValue ?? 0).Sum();


                        sum = Convert.ToDouble(value);

                    }
                    else
                    {
                        sum = 0;
                    }
                }
                else {
                    var count = context.TBL_INVOICEs.Count(me => me.invoiceLCNumber == lcNumber && me.id != invoiceId && (me.status == "ACTIVE" || me.status == "Setteled" || me.status == "Advised"));
                    if (count > 0)
                    {

                        var value = context.TBL_INVOICEs
                                .Where(t => t.invoiceLCNumber == lcNumber && t.id != invoiceId && (t.status == "Setteled" || t.status == "ACTIVE" || t.status == "Advised"))
                                 .Select(t => t.invoiceValue ?? 0).Sum();


                        sum = Convert.ToDouble(value);

                    }
                    else
                    {
                        sum = 0;
                    }
                }
                return sum;
            }
        }

        public List<VW_DETAIL_LC> selectLcCancelationByLC_Number(string lcNumber)
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                var result = from lc in context.VW_DETAIL_LCs
                             where lc.lcNumber == lcNumber && (lc.lcStatus == "Opened" || lc.lcStatus == "Setteled")
                             select lc;
                return result.ToList<VW_DETAIL_LC>();
            }
        }

        public bool updateLcStatus()
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                int result = context.updateLCStatus(_lcNumber, _lcStatus);
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

        public bool countExcessPermitCode(string lcnumber)
        {
            using (context = new FORMS_DB_DBMLDataContext())
            {
                var result = from lc in context.TBL_LC_EXCESS_AMOUNTs
                             where lc.excessLCNumber == lcnumber
                             select lc;
                int count = result.ToList<TBL_LC_EXCESS_AMOUNT>().Count;
                if (count > 0)
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
