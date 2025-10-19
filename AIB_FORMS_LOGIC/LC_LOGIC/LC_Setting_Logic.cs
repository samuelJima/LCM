using AIB_FORMS_OB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_LOGIC
{
   public class LC_Setting_Logic
    {

        public void findCurrencyByValueDate(System.Data.DataTable curuncyList, DateTime valuedate)
        {
            TBL_CURRENCY curency = new TBL_CURRENCY();
            curency.selectCurrencyByValueDate(curuncyList, valuedate);
       
        }

        public bool addCurrencyRate(DateTime valueDate, DataTable curuncyList)
        {
            TBL_CURRENCY curency = new TBL_CURRENCY();
            int result= curency.insertNewCurrencyRate(valueDate, curuncyList);
            if (result == 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public bool changeCurrencyRate(DateTime valueDate, DataTable curuncyList)
        {
            TBL_CURRENCY curency = new TBL_CURRENCY();
            int result = curency.updateNewCurrencyRate(valueDate, curuncyList);
            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool addOpenThroughBank(string bankName, string BICCode, int countryId, string city)
        {
            TBL_OPEN_THROUGH_BANK opBank = new TBL_OPEN_THROUGH_BANK();
            return opBank.saveOpenThroughBank(bankName, BICCode, countryId, city);
        }

        public bool openthroughBankExists(string bankName)
        {
            TBL_OPEN_THROUGH_BANK opBank = new TBL_OPEN_THROUGH_BANK();
            int result= opBank.searchOPbankByName(bankName);
            if(result>0){
              return true;
            }
            else{
              return false;
            }
        }

        public bool editOpenThroughBank(string bankName, string BICCode, int countryId, string city, int bankId)
        {
            TBL_OPEN_THROUGH_BANK opBank = new TBL_OPEN_THROUGH_BANK();
            return opBank.updatOpenThroughBank(bankName, BICCode, countryId, city,bankId);
        }

        public bool openthroughBankExists(string bankName, int bankId)
        {
            TBL_OPEN_THROUGH_BANK opBank = new TBL_OPEN_THROUGH_BANK();
            int result = opBank.searchOPbankByName(bankName, bankId);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool rembursingBankExists(string bankName)
        {
            TBL_LC_CORRESPONDENT rmBank = new TBL_LC_CORRESPONDENT();
            int result = rmBank.searchrmbankByName(bankName);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool addRimbersingBank(string bankName, string telephonNo, string faxNo, DataTable BankCurrency)
        {
            TBL_LC_CORRESPONDENT rmBank = new TBL_LC_CORRESPONDENT();
            return rmBank.saveRembursinghBank(bankName, telephonNo, faxNo, BankCurrency);
        }

        public bool rembursingBankExists(string bankName, int bankId)
        {
            TBL_LC_CORRESPONDENT rmBank = new TBL_LC_CORRESPONDENT();
            int result = rmBank.searchrmbankByName(bankName, bankId);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool editRimbersingBank(string bankName, string telephonNo, string faxNo, DataTable BankCurrency, int bankId)
        {
            TBL_LC_CORRESPONDENT rmBank = new TBL_LC_CORRESPONDENT();
            return rmBank.updatRimbursingBank(bankName, telephonNo, faxNo, BankCurrency, bankId);
        }

        public List<TBL_LC_CORRESPONDENT> findsAllCorespondentBanks()
        {
            TBL_LC_CORRESPONDENT rmBank = new TBL_LC_CORRESPONDENT();
            return rmBank.selectAllCorespondent();
        }

        public List<TBL_OPEN_THROUGH_BANK> findsAllOpenThroughBanks()
        {
            TBL_OPEN_THROUGH_BANK opBank = new TBL_OPEN_THROUGH_BANK();
            return opBank.selectAllOpenThrough();
        }

        public List<TBL_OPEN_THROUGH_BANK> findOpById(int bankId)
        {
            TBL_OPEN_THROUGH_BANK opBank = new TBL_OPEN_THROUGH_BANK();
            return opBank.selectOpenThroughBYId(bankId);
        }

        public List<VW_CORRESPONDNCE_CURRENCY> findRMBankdById(int bankId)
        {
            TBL_LC_CORRESPONDENT rmBank = new TBL_LC_CORRESPONDENT();
            return rmBank.selectCorespondentBankById(bankId);
        }

        public bool addBranch(string branchName, string branchAbrivation,string branchCode)
        {
            TBL_Branch banch = new TBL_Branch();
            return banch.saveBranch(branchName, branchAbrivation,branchCode);
        }

        public bool editBranch(int brancid, string branchName, string branchAbrivation,string branchCode)
        {
            TBL_Branch banch = new TBL_Branch();
            return banch.updateBranch(brancid, branchName, branchAbrivation, branchCode);
        }

        public List<TBL_Branch> findAllBranchs()
        {
            TBL_Branch banch = new TBL_Branch();
            return banch.selectAllBranch();
        }

        public List<TBL_Branch> findBranchsById(int branchid)
        {
            TBL_Branch banch = new TBL_Branch();
            banch.branchid = branchid;
            return banch.selectBranchByVode();
        }
    }
}
