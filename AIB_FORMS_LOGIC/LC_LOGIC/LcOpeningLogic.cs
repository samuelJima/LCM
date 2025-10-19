using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIB_FORMS_OB;
using System.Data.Entity;

namespace AIB_FORMS_LOGIC
{
    public class LcOpeningLogic
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public List<CUSTOMERBRANCH> findCustomerByAccountNumber(string accountNumber)
        {
            using (AIBSITEntities cont = new AIBSITEntities())
            {
                List<CUSTOMERBRANCH> ACCOUNT = new List<CUSTOMERBRANCH>();

                try
                {
                    var result = (from c in cont.CUSTOMERBRANCHes
                                  where c.ACCOUNTID == accountNumber
                                  select c);
                    ACCOUNT = result.ToList<CUSTOMERBRANCH>();
                    //return ACCOUNT[0].ACCOUNTNAME.ToString();
                    return ACCOUNT;
                }
                catch {
                    ACCOUNT = null;
                    return ACCOUNT;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BranchCode"></param>
        /// <returns></returns>
       public List<TBL_Branch> findBranchByBranchCode(int BranchCode)
        {
            TBL_Branch branch = new TBL_Branch();
            branch.branchid = BranchCode;
            return branch.selectBranchByVode();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TBL_LC_CORRESPONDENT> findCorespondentBanks()
        {
            TBL_LC_CORRESPONDENT lcBanks = new TBL_LC_CORRESPONDENT();
            //lcBanks.lcRelationType = "Correspondence";
            return lcBanks.selectAllCorespondent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TBL_OPEN_THROUGH_BANK> findOpenThroughBanks()
        {
            TBL_OPEN_THROUGH_BANK lcBanks = new TBL_OPEN_THROUGH_BANK();
            return lcBanks.selectAllOpenThrough();
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="corsId"></param>
        /// <returns></returns>
        public List<TBL_CURRENCY> findCurrencyByCorrespondenceID(int coreId)
        {
            TBL_LC_CORRESPONDENT correspondent = new TBL_LC_CORRESPONDENT();
            //correspondent.id = corsId;
            return correspondent.selsectCurrencyByCorrespondentId();
        }

        public List<VW_CORRESPONDNCE_CURRENCY> findCurrencyByCorrespondenceIDNew(int corsId)
        {
            TBL_LC_CORRESPONDENT correspondent = new TBL_LC_CORRESPONDENT();
            correspondent.id = corsId;
            return correspondent.selsectCurrencyByCorrespondentIdNew();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public string findMaximumLc_Code(int year)
        {
            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();
            return lc.selectMaximumLC_Code(year);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permitCode"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public bool checkPermitCode(string permitCode, int year)
        {
            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();

            return lc.countPermitCode(permitCode, year);

        }

        /// 
        /// </summary>
        /// <param name="permitCode"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public bool checkPermitCoddFroUpdate(string permitCode, int year, string lcNumber)
        {
            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();

            return lc.countPermitCodeforUpdate(permitCode, year, lcNumber);

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
        public bool saveLCDetail(string lcNumber, string LCcode, DateTime? vluedate, string permitCode, int permitYear, double lcValue, string currencyId, double currencyRate, double marginPaid, int CorrspId, int openThroughId, string supplyer, int branchCode, string CustomerAccount, string lcStatus, DateTime openingDate, string customerName, string confirmationStatus,string curencyType, double tollerance,string accountBranch,int periods, int userLimitationDays,int userId)
        {
            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();
            return lc.insertLCDetail(lcNumber, LCcode, vluedate, permitCode, permitYear, lcValue, currencyId, currencyRate, marginPaid, CorrspId, openThroughId, supplyer, branchCode, CustomerAccount, lcStatus, openingDate, customerName, confirmationStatus, curencyType, tollerance, accountBranch, periods,userLimitationDays,userId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lcNumber"></param>
        /// <returns></returns>
       public List<VW_DETAIL_LC> getLCbyLcNumber(string lcNumber)
        {
            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();

            return lc.selectLcByLC_Number(lcNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lcNumber"></param>
        /// <returns></returns>
        public List<VW_DETAIL_LC> searchLcByLC_Number(string lcNumber)
        {
            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();
            return lc.selectLcByLC_Number(lcNumber);
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
        public bool changeLC_Detail(string lcNumber, DateTime? valueDate, string permitCode, int permitYear, double lcValue, string currencyID, double rate, double margenPaid, int rembBankId, int openThroughId, string supplyer, string accountNo, string customerName, string confirmation, string curencyType, double tollerance, string accountBranch, int periods, int limitedDays)
        {
            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();
            return lc.updateLcDetail(lcNumber, valueDate, permitCode, permitYear, lcValue, currencyID, rate, margenPaid, rembBankId, openThroughId, supplyer, accountNo, customerName, confirmation, curencyType, tollerance, accountBranch, periods,limitedDays);
        }



        public List<VW_DETAIL_LC> searchLcCancelationByLC_Number(string lcNumber)
        {
            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();
            return lc.selectLcCancelationByLC_Number(lcNumber);
        }

        public List<TBL_COUNTRY> findAllCountry()
        {
            TBL_COUNTRY cunt = new TBL_COUNTRY();
            return cunt.selectAllCountry();
        }

        public List<TBL_AIB_CURRENCY_RATE> findCurrencyByValueDat(string currencytype, string currencyCode, DateTime valueDate)
        {
            TBL_AIB_CURRENCY_RATE rate = new TBL_AIB_CURRENCY_RATE();
            //rate.exchangeRateType = currencytype;
            rate.fromCurrencyCode = currencyCode;
            rate.valueDate = valueDate;
            return rate.selectRateByValueDate(currencytype);
        }

        public bool addinterestBeginDate(DateTime? beginDate, int invoiceId)
        {
            TBL_INVOICE invoice = new TBL_INVOICE();
            return invoice.updateInterestBeginDate(beginDate, invoiceId);
        }

        public bool changeLCStatus(string lcNumber)
        {
            TBL_LETTER_OF_CREDIT lct = new TBL_LETTER_OF_CREDIT();
            lct.lcStatus ="Setteled";
            lct.lcNumber = lcNumber;
            return lct.updateLcStatus();
        }
        public bool changeLCExpirationStatus(string lcNumber)
        {
            TBL_LETTER_OF_CREDIT lct = new TBL_LETTER_OF_CREDIT();
            lct.lcStatus = "OutDated";
            lct.lcNumber = lcNumber;
            return lct.updateLcStatus();
        }
        public bool changeLCCancelationStatus(string lcNumber)
        {
            TBL_LETTER_OF_CREDIT lct = new TBL_LETTER_OF_CREDIT();
            lct.lcStatus = "Cancelled";
            lct.lcNumber = lcNumber;
            return lct.updateLcStatus();
        }



        public bool changeadviceStatus(string status, int invoiceId)
        {
            TBL_INVOICE invoice = new TBL_INVOICE();
            return invoice.updateInterestStatus(status, invoiceId);
        }

        public bool changeLCStatusToRemove(string lcNumber)
        {
            TBL_LETTER_OF_CREDIT lct = new TBL_LETTER_OF_CREDIT();
            lct.lcStatus = "Removed";
            lct.lcNumber = lcNumber;
            return lct.updateLcStatus();
        }



        public bool checkExcessPermitCode(string permitCode, int year)
        {
            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();

            return lc.countExcessPermitCode(permitCode, year);
        }

        public bool isThereEcessAmount(string lcnumber)
        {

            TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();

            return lc.countExcessPermitCode(lcnumber);
        }

        public string findCorspondentAccount(int corespondentId, int currencyId)
        {
            VW_CORRESPONDNCE_CURRENCY cocu = new VW_CORRESPONDNCE_CURRENCY();
            return cocu.selectCorspondentAccount(corespondentId, currencyId);
            
        }

        public bool saveLCIncrement(double incrementValue, int year, string permitcode, string lcNumber, DateTime nullable2, double nullable3)
        {
            TBL_LC_INCREMENT increment = new TBL_LC_INCREMENT();
            return increment.saveLCIncrement(incrementValue, year, permitcode, lcNumber, nullable2, nullable3);
        }

        public bool changeParentLCValue(double incrementedAmount, string lcNumber)
        {
            TBL_LC_INCREMENT increment = new TBL_LC_INCREMENT();
            return increment.incrementParentLcValue(incrementedAmount, lcNumber);

        }

        public List<VW_LC_INCREMENTAMOUNT> searchLcincrement(string txtSearchLcInc, int year, string permitCode)
        {
            TBL_LC_INCREMENT increment = new TBL_LC_INCREMENT();
            return increment.selectLCIncrement(txtSearchLcInc, year, permitCode);

        }

        public bool changeLC_increment(double incrementAmount, int permitYear, string permitcode, string lcNumber, DateTime incValueDate, double incrate, int id)
        {
            TBL_LC_INCREMENT increment = new TBL_LC_INCREMENT();
            return increment.updateLCIncrement( incrementAmount, permitYear, permitcode,lcNumber,incValueDate,incrate,id);
        }

        public bool saveLCExtention(double extentionValue, int periods, DateTime extentionDate, int maxInvSequence, string lcNumber)
        {
            TBL_LC_EXTENTION extention = new TBL_LC_EXTENTION();
            return extention.saveExtention(extentionValue, periods, extentionDate, maxInvSequence, lcNumber);
        }

        public List<VW_LC_EXTENTION> searchLcExtention(DateTime extentionDate, string SearchLcInc)
        {
            TBL_LC_EXTENTION extention = new TBL_LC_EXTENTION();
            return extention.selectLCExtention(extentionDate, SearchLcInc);
        }

        public bool changeLC_Extention(int id, int periods, string lcNumber)
        {
            TBL_LC_EXTENTION extention = new TBL_LC_EXTENTION();
            return extention.updateLCExtention(id, periods,lcNumber);
        }

        public bool extentionExists(DateTime extentionDate, string lcNumber)
        {
            TBL_LC_EXTENTION extention = new TBL_LC_EXTENTION();
            return extention.countLCExtention(extentionDate, lcNumber);
        }

        public List<TBL_CURRENCY> findCurrencyByCurrencyCode(string currencyCode)
        {
            VW_CORRESPONDNCE_CURRENCY cocu = new VW_CORRESPONDNCE_CURRENCY();
            return cocu.selectCurrencybyCode(currencyCode);
        }
    }
}
