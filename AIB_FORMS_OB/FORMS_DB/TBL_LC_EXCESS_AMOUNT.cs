using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_OB
{
  public partial class TBL_LC_EXCESS_AMOUNT
    {
      FORMS_DB_DBMLDataContext User_LinqDataContext;

      public bool saveExcessAmount(double excessAmount, int permitYear, string permitcode,DateTime invoiceDate, double invoiceRate, string lcNumber)
      {
          using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
          {
              var result = User_LinqDataContext.saveLCExcessAmount(excessAmount, permitYear, permitcode,invoiceDate,invoiceRate,lcNumber);
             if(result==0){
               return true;
             }
             else{
               return false;
             }
          }
      }

      public bool updateExcessAmount(double excessAmount, string lcNumber)
      {
         using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
          {
              var result = User_LinqDataContext.updateExcessAmount(excessAmount, lcNumber);
             if(result==0){
               return true;
             }
             else{
               return false;
             }
          }
      }

      public bool selectExcessAmount(string lcNumber)
      {
          using (User_LinqDataContext = new FORMS_DB_DBMLDataContext())
          {
              var result = from brch in User_LinqDataContext.TBL_LC_EXCESS_AMOUNTs
                           where brch.excessLCNumber == lcNumber
                           select brch;
              if (result.ToList<TBL_LC_EXCESS_AMOUNT>().Count > 0)
              {
                  return true;
              }
              else {
                  return false;
              }
          }
      }
    }
}
