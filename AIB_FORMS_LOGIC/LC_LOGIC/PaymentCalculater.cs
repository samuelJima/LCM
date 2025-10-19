using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIB_FORMS_OB;

namespace AIB_FORMS_LOGIC
{
   public class PaymentCalculater
    {
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public double calculateExchangeCommission(Double LCValue) {
           try
           {
               TBL_LC_PAYMENT_TYPE paymentTypeOb = new TBL_LC_PAYMENT_TYPE();
               List<TBL_LC_PAYMENT_TYPE> payment = new List<TBL_LC_PAYMENT_TYPE>();
               double tarifValue;
               double commissionValue = -1;
               paymentTypeOb.typeCode = "EC";
               payment = paymentTypeOb.findPaymentTypeBycode();
               if (payment.Count == 1)
               {
                   tarifValue = Convert.ToDouble(payment[0].lowerTarifPercent);
                   commissionValue = (LCValue * tarifValue) / 100;
               }
               return commissionValue;
           }
           catch(Exception exp) {
               return -1;
           }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public double calculateServiceCharge(Double LCValue, string currencyCode, string curencyType, double invoiceRate, DateTime valueDate)
       {
           TBL_LC_PAYMENT_TYPE paymentTypeOb = new TBL_LC_PAYMENT_TYPE();
           List<TBL_LC_PAYMENT_TYPE> payment = new List<TBL_LC_PAYMENT_TYPE>();
           double lowertarifValue;
           double uppertarifValue;
           double lowertarifpercent;
           double middletarifpercent;
           double uppertarifpercent;
           double serviceCharge = -1;
           if (currencyCode == "USD")
           {
               paymentTypeOb.typeCode = "SC";
               payment = paymentTypeOb.findPaymentTypeBycode();
               if (payment.Count == 1)
               {
                   lowertarifValue = Convert.ToDouble(payment[0].minimumTarif);
                   uppertarifValue = Convert.ToDouble(payment[0].maximumTarif);
                   lowertarifpercent = Convert.ToDouble(payment[0].lowerTarifPercent);
                   middletarifpercent = Convert.ToDouble(payment[0].middleTarifPercent);
                   uppertarifpercent = Convert.ToDouble(payment[0].upperTarifPercent);
                   if (LCValue <= lowertarifValue)
                   {
                       serviceCharge = (LCValue * lowertarifpercent) / 100;
                   }
                   else if (lowertarifValue < LCValue && LCValue <= uppertarifValue)
                   {
                       serviceCharge = (LCValue * middletarifpercent) / 100;
                   }
                   else
                   {
                       serviceCharge = (LCValue * uppertarifpercent) / 100;
                   }
               }

           }
           else {

               LcOpeningLogic openingLogic = new LcOpeningLogic();

               List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();

               currencyRate = openingLogic.findCurrencyByValueDat(curencyType, "USD", valueDate);

               if (currencyRate.Count == 1)
               {
                   paymentTypeOb.typeCode = "SC";
                   payment = paymentTypeOb.findPaymentTypeBycode();

                   if (payment.Count == 1)
                   {
                       // minimum and maximum tarif values for currencies other than USD
                       lowertarifValue = (Convert.ToDouble(currencyRate[0].currencyRate) / invoiceRate)* Convert.ToDouble(payment[0].minimumTarif);
                       uppertarifValue = (Convert.ToDouble(currencyRate[0].currencyRate) / invoiceRate) * Convert.ToDouble(payment[0].maximumTarif);
                     
                       //tarif percents for each interval
                       lowertarifpercent = Convert.ToDouble(payment[0].lowerTarifPercent);
                       middletarifpercent = Convert.ToDouble(payment[0].middleTarifPercent);
                       uppertarifpercent = Convert.ToDouble(payment[0].upperTarifPercent);

                       //Service Charges Calculated based on tarif percents 
                       if (LCValue <= lowertarifValue)
                       {
                           serviceCharge = (LCValue * lowertarifpercent) / 100;
                       }
                       else if (lowertarifValue < LCValue && LCValue <= uppertarifValue)
                       {
                           serviceCharge = (LCValue * middletarifpercent) / 100;
                       }
                       else
                       {
                           serviceCharge = (LCValue * uppertarifpercent) / 100;
                       }
                   }
               }
           }
           return serviceCharge;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public double calculateOpeningCommission(Double LCValue, string currencyCode, string curencyType, double invoiceRate, DateTime valueDate)
       {
           TBL_LC_PAYMENT_TYPE paymentTypeOb = new TBL_LC_PAYMENT_TYPE();
           List<TBL_LC_PAYMENT_TYPE> payment = new List<TBL_LC_PAYMENT_TYPE>();
           double lowertarifValue;
           double uppertarifValue;
           double lowertarifpercent;
           double middletarifpercent;
           double uppertarifpercent;
           double openingValueValue = -1;
           if (currencyCode == "USD")
           {
               paymentTypeOb.typeCode = "OC";
               payment = paymentTypeOb.findPaymentTypeBycode();
               if (payment.Count == 1)
               {
                   lowertarifValue = Convert.ToDouble(payment[0].minimumTarif);
                   uppertarifValue = Convert.ToDouble(payment[0].maximumTarif);
                   lowertarifpercent = Convert.ToDouble(payment[0].lowerTarifPercent);
                   middletarifpercent = Convert.ToDouble(payment[0].middleTarifPercent);
                   uppertarifpercent = Convert.ToDouble(payment[0].upperTarifPercent);

                   if (LCValue <= lowertarifValue)
                   {
                       openingValueValue = (LCValue * lowertarifpercent) / 100;
                   }
                   else if (lowertarifValue < LCValue && LCValue <= uppertarifValue)
                   {
                       openingValueValue = (LCValue * middletarifpercent) / 100;
                   }
                   else
                   {
                       openingValueValue = (LCValue * uppertarifpercent) / 100;
                   }
               }
           }
           else {

                LcOpeningLogic openingLogic = new LcOpeningLogic();

               List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
               currencyRate = openingLogic.findCurrencyByValueDat(curencyType, "USD", valueDate);

                if (currencyRate.Count == 1)
                 {
                   paymentTypeOb.typeCode = "OC";
                   payment = paymentTypeOb.findPaymentTypeBycode();
                   if (payment.Count == 1)
                   {
                       // minimum and maximum tarif values for currencies other than USD

                       lowertarifValue = (Convert.ToDouble(currencyRate[0].currencyRate) / invoiceRate) * Convert.ToDouble(payment[0].minimumTarif);
                       uppertarifValue = (Convert.ToDouble(currencyRate[0].currencyRate) / invoiceRate) * Convert.ToDouble(payment[0].maximumTarif);

                       //tarif percents for each interval

                       lowertarifpercent = Convert.ToDouble(payment[0].lowerTarifPercent);
                       middletarifpercent = Convert.ToDouble(payment[0].middleTarifPercent);
                       uppertarifpercent = Convert.ToDouble(payment[0].upperTarifPercent);

                       //Service Charges Calculated based on tarif percents 
                       if (LCValue <= lowertarifValue)
                       {
                           openingValueValue = (LCValue * lowertarifpercent) / 100;
                       }
                       else if (lowertarifValue < LCValue && LCValue <= uppertarifValue)
                       {
                           openingValueValue = (LCValue * middletarifpercent) / 100;
                       }
                       else
                       {
                           openingValueValue = (LCValue * uppertarifpercent) / 100;
                       }
                   }
               }
           }
           return openingValueValue;

       }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public double calculateSWIFT() {
           TBL_LC_PAYMENT_TYPE paymentTypeOb = new TBL_LC_PAYMENT_TYPE();
           List<TBL_LC_PAYMENT_TYPE> payment = new List<TBL_LC_PAYMENT_TYPE>();
           double swiftValue;
           double swiftConstValue = -1;
           paymentTypeOb.typeCode = "ST";
           payment = paymentTypeOb.findPaymentTypeBycode();
           if (payment.Count == 1)
           {
               swiftValue = Convert.ToDouble(payment[0].minimumTarif);
               swiftConstValue = swiftValue;
           }
           return swiftConstValue;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public double calculateExtentionSWIFT()
       {
           TBL_LC_PAYMENT_TYPE paymentTypeOb = new TBL_LC_PAYMENT_TYPE();
           List<TBL_LC_PAYMENT_TYPE> payment = new List<TBL_LC_PAYMENT_TYPE>();
           double swiftValue;
           double swiftConstValue = -1;
           paymentTypeOb.typeCode = "ENS";
           payment = paymentTypeOb.findPaymentTypeBycode();
           if (payment.Count == 1)
           {
               swiftValue = Convert.ToDouble(payment[0].minimumTarif);
               swiftConstValue = swiftValue;
           }
           return swiftConstValue;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="LCValue"></param>
       /// <returns></returns>
       public double calculateConfirmationCommission(Double LCValue)
       {

           TBL_LC_PAYMENT_TYPE paymentTypeOb = new TBL_LC_PAYMENT_TYPE();
           List<TBL_LC_PAYMENT_TYPE> payment = new List<TBL_LC_PAYMENT_TYPE>();
           double tarifValue;
           double confirmatinValue = -1;
           paymentTypeOb.typeCode = "CC";
           payment = paymentTypeOb.findPaymentTypeBycode();
           if (payment.Count == 1)
           {
               tarifValue = Convert.ToDouble(payment[0].lowerTarifPercent);
               confirmatinValue = (LCValue * tarifValue) / 100;
           }
           return confirmatinValue;

       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="LCValue"></param>
       /// <returns></returns>
       public double calculateExpiration(Double invoiceValue, int numberOfDays)
       {

           TBL_LC_PAYMENT_TYPE paymentTypeOb = new TBL_LC_PAYMENT_TYPE();
           List<TBL_LC_PAYMENT_TYPE> payment = new List<TBL_LC_PAYMENT_TYPE>();
           double tarifValue;
           double confirmatinValue = -1;
           paymentTypeOb.typeCode = "SE";
           payment = paymentTypeOb.findPaymentTypeBycode();
           if (payment.Count == 1)
           {
               if (numberOfDays <= 5)
               {
                   tarifValue = Convert.ToDouble(payment[0].lowerTarifPercent);
                   confirmatinValue = (invoiceValue * tarifValue) / 100;
               }
               else if (numberOfDays > 5 && numberOfDays <= 30)
               {
                   tarifValue = Convert.ToDouble(payment[0].middleTarifPercent);
                   confirmatinValue = (invoiceValue * tarifValue) / 100;
               }
               else {
                   tarifValue = Convert.ToDouble(payment[0].upperTarifPercent);
                   confirmatinValue = (invoiceValue * tarifValue) / 100;
               }
               
           }
           return confirmatinValue;

       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="invoiceValue"></param>
       /// <param name="invoiceRate"></param>
       /// <returns></returns>
       public double calculculateBillAmount(double invoiceValue, double invoiceRate) {

           return invoiceValue;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="invoiceValue"></param>
       /// <param name="lcOpeningCurrencyRate"></param>
       /// <returns></returns>
       public double calculculateUtilizedAmmountAmount(double invoiceValue, double lcOpeningCurrencyRate)
       {
           return (invoiceValue);
       }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="discripancyState"></param>
       /// <returns></returns>
       public double calculculateDiscripancy(string discripancyState,string currencyCode, string curencyType, double invoiceRate,DateTime valueDate)
       {
           TBL_LC_PAYMENT_TYPE paymentTypeOb = new TBL_LC_PAYMENT_TYPE();
           List<TBL_LC_PAYMENT_TYPE> payment = new List<TBL_LC_PAYMENT_TYPE>();
           double discripancyValue = 0;

           if (discripancyState.Equals("Yes"))
           {
               if (currencyCode == "USD")
               {
                   paymentTypeOb.typeCode = "DY";
                   payment = paymentTypeOb.findPaymentTypeBycode();
                   if (payment.Count == 1)
                   {
                       discripancyValue = Convert.ToDouble(payment[0].minimumTarif);

                   }
               }
               else {

                   LcOpeningLogic openingLogic = new LcOpeningLogic();
                   
                   List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
                   currencyRate = openingLogic.findCurrencyByValueDat(curencyType, "USD", valueDate);

                   if (currencyRate.Count == 1) {

                       paymentTypeOb.typeCode = "DY";
                       payment = paymentTypeOb.findPaymentTypeBycode();

                       if (payment.Count == 1)
                       {
                           discripancyValue = (Convert.ToDouble(currencyRate[0].currencyRate) / invoiceRate) * Convert.ToDouble(payment[0].minimumTarif);
                       }
                   }
                  
               }

                 return discripancyValue;
           }

           else {

               return discripancyValue;
           }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="lcValue"></param>
       /// <param name="invoiceValue"></param>
       /// <returns></returns>
       public double calculculateExessAmount(double lcValue, double invoiceValue, string lcNumber)
       {
           double excessAmount = 0;
           double sum = 0;
           TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();

           sum = lc.sumOfInvoiceByLC_Number(lcNumber);

           if (invoiceValue + sum > lcValue)
           {
               excessAmount = (invoiceValue + sum) - lcValue;
           }

           return excessAmount;

       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="lcValue"></param>
       /// <param name="invoiceValue"></param>
       /// <returns></returns>
       public double calculculateExessAmount(double lcValue, double invoiceValue,string lcNumber,int invoiceid)
       {
           double excessAmount = 0;
           double sum = 0;
           TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();

           sum = lc.sumOfInvoiceByLC_Number(lcNumber, invoiceid);

           if (invoiceValue + sum > lcValue)
           {
               excessAmount = (invoiceValue + sum )- lcValue;
           }

           return excessAmount;

       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="lcValue"></param>
       /// <param name="invoiceValue"></param>
       /// <returns></returns>
       public double calculculateExpiration(double lcValue, string lcNumber, int invoiceid)
       {
           double excessAmount = 0;
           double sum = 0;
           TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();

           sum = lc.sumOfInvoiceByLC_Number(lcNumber, invoiceid);

         
              excessAmount =  lcValue-sum ;
        

           return excessAmount;

       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="margien"></param>
       /// <param name="invoiceValue"></param>
       /// <param name="lcValue"></param>
       /// <returns></returns>
       public double calculateMargenHeld(double margien, double invoiceValue, double lcValue)
       {
           double margienValue = 0;
              // margienValue=((margien*invoiceValue)*invoiceValue)/(lcValue*100);

            margienValue = ((margien * invoiceValue) / 100);

           return margienValue;

       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="margien"></param>
       /// <param name="invoiceValue"></param>
       /// <param name="lcValue"></param>
       /// <returns></returns>
       public double calculateNetAdvance(double margien, double invoiceValue, double openingCurrencyRate, double invoiceRate)
       {

           double netAdvance = 0;
           double margienValue = 0;
          // margienValue = ((margien * invoiceValue) * invoiceValue) / (lcValue * 100);
           margienValue = ((margien * invoiceValue) / 100);

           netAdvance = (invoiceValue * invoiceRate) - (margienValue * openingCurrencyRate);

           return netAdvance;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="beginDate"></param>
       /// <param name="endDate"></param>
       /// <param name="netTotal"></param>
       /// <returns></returns>
       public double calculateInterest(DateTime? beginDate, DateTime? endDate, double netAdvance)
       {
           TBL_LC_PAYMENT_TYPE paymentTypeOb = new TBL_LC_PAYMENT_TYPE();
           List<TBL_LC_PAYMENT_TYPE> payment = new List<TBL_LC_PAYMENT_TYPE>();

           double interestValue = 0;

           paymentTypeOb.typeCode = "IT";
          
           payment = paymentTypeOb.findPaymentTypeBycode();

           if (payment.Count == 1)
           { 
               TimeSpan difference = (DateTime)endDate - (DateTime)beginDate;
               int days= Convert.ToInt32(difference.TotalDays)-1;

               interestValue = Convert.ToDouble((Convert.ToDouble(payment[0].lowerTarifPercent) / 100) * (Convert.ToDouble(days) / 365) * (netAdvance));
           }

           return interestValue;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="LcValue"></param>
       /// <param name="lcNumber"></param>
       /// <returns></returns>
       public double calculateUnutlizedAmount(double LcValue, string lcNumber) {
           double unutlizedAmount = 0;
           double sum = 0;
           TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();

           sum = lc.sumOfInvoiceByLC_Number(lcNumber);
           if (sum < LcValue && sum !=0) { 
               unutlizedAmount= LcValue-sum;
           }
           if (sum == 0) {
               unutlizedAmount = LcValue;
           }

           return unutlizedAmount;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="LcValue"></param>
       /// <param name="lcNumber"></param>
       /// <returns></returns>
       public double calculatePostThisAmount(double marginpaid,double LcValue, string lcNumber)
       {

           double postThisAmount = 0;
           double sum = 0;
           TBL_LETTER_OF_CREDIT lc = new TBL_LETTER_OF_CREDIT();

           sum = lc.sumOfInvoiceByLC_Number(lcNumber);
           if (sum == 0)
           {
               postThisAmount = (marginpaid/100)*LcValue;
           }

           return postThisAmount;
       }

       public double usdRate( string curencyType,  DateTime valueDate)
       { 
           LcOpeningLogic openingLogic = new LcOpeningLogic();

               List<TBL_AIB_CURRENCY_RATE> currencyRate = new List<TBL_AIB_CURRENCY_RATE>();
               currencyRate = openingLogic.findCurrencyByValueDat(curencyType, "USD", valueDate);
               if (currencyRate.Count == 1)
               {
                   return Convert.ToDouble(currencyRate[0].currencyRate);
               }
               else {
                   return -1;
               }
       }

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public double calculateExtentionCommission(Double LCValue)
       {
           try
           {
               TBL_LC_PAYMENT_TYPE paymentTypeOb = new TBL_LC_PAYMENT_TYPE();
               List<TBL_LC_PAYMENT_TYPE> payment = new List<TBL_LC_PAYMENT_TYPE>();
               double tarifValue;
               double extentionValue = -1;
               paymentTypeOb.typeCode = "ET";
               payment = paymentTypeOb.findPaymentTypeBycode();
               if (payment.Count == 1)
               {
                   tarifValue = Convert.ToDouble(payment[0].lowerTarifPercent);
                   extentionValue = (LCValue * tarifValue) / 100;
               }
               return extentionValue;
           }
           catch (Exception exp)
           {
               return -1;
           }
       }
       public DateTime calculateExpirationDueDate( string lcNumber,DateTime lcopeningDate, int oppenigDuration) {
           TBL_LC_EXTENTION extention = new TBL_LC_EXTENTION();
           List<TBL_LC_EXTENTION> lcExtentions = new List<TBL_LC_EXTENTION>();
           DateTime dueDat = new DateTime();
           int extendedPeriods = oppenigDuration;

           int NumberOfDaysPerPeriod = 90;

           lcExtentions = extention.findExtentionsForLC(lcNumber);
           foreach (var item in lcExtentions){
               extendedPeriods = extendedPeriods + Convert.ToInt32(item.extentionPeriod);
           }

           dueDat = lcopeningDate.AddDays(extendedPeriods * NumberOfDaysPerPeriod);

           return dueDat;

       
       }
    }
}
