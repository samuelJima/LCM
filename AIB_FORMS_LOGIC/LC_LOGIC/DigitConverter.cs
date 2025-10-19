using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_LOGIC.LC_LOGIC
{
   public class DigitConverter
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="number"></param>
       /// <param name="digit"></param>
       /// <returns></returns>
       public string convertTo_N_digit_String(int number, int digit) {
           string result;
           string DN= "D"+digit.ToString();
           result = number.ToString(DN);
           return result;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="nDigit"></param>
       /// <returns></returns>
       public int conver_N_DigitString_ToNumber(string nDigit) {
           int number;
           number = Convert.ToInt32(nDigit);
           return number;
       }
    }
}
