using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Web;
using Telerik.Web.UI;

namespace AIB_FORMS_VALIDATION
{
    internal abstract class Validator
    {
        //Error types
        public  const int NO_ERROR = 0;
        public  const int REQUIRED_ERROR = 1;
        public  const int FORMAT_ERROR = 2;
        public  const int OUT_OF_RANGE_ERROR = 3;
        public  const int CUSTOM_PATTERN_MISMATCH = 4;
        public const int CUSTOM_TEXT_NOT_ALLOWED = 5;
    
        //Error indication style class
        protected  const String FORMAT_ERROR_STYLE = "formatErrorIndicator";
        protected  const String EMPTY_ERROR_STYLE = "emptyErrorIndicator";
        protected  const String OUT_OF_RANGE_ERROR_STYLE = "outOffRangeErrorIndicator";

        public const String I_FORMAT_ERROR_STYLE = "formatErrorIndicator";
        public const String I_EMPTY_ERROR_STYLE = "emptyErrorIndicator";
        public const String I_OUT_OF_RANGE_ERROR_STYLE = "outOffRangeErrorIndicator";
        public const String I_CUSTOME_TEXT_STYLY = "customText";
        //Validation criteria
        private int pattern = 0;
        private bool required = false;
        private String minValue = "-";
        private String maxValue = "-";
        private String requiredErrorMessage = "";
        private String formatErrorMessage = "";
        private String outOfRangeErrorMessage = "";
        private String customPattern = "";
        private String fieldTypeString = "";
        private String customText = "";

        public int validateData()
        {
            int result = Validator.NO_ERROR;
            String value = getComponentData();
            clearErrorIndication();

            if (value == "Custom")
            {
                if (pattern == ComponentValidator.CUSTOM_TEXT_ALLOWED)
                {
                    result = Validator.NO_ERROR;
                }
                else
                {
                    result = Validator.CUSTOM_TEXT_NOT_ALLOWED;
                }
            }
            else if (value == null)
            {
                if (required == true)
                {//It is required data but found empty
                    result = Validator.REQUIRED_ERROR;
                }
            }
            else if (value.Length == 0)
                {
                    if (required == true)
                    {//It is required data but found empty
                        result = Validator.REQUIRED_ERROR;
                    }
                }
            else if (pattern == ComponentValidator.NO_FORMAT)
                {
                   if (!minValue.Equals("-") && !maxValue.Equals("-"))
                {
                    if (isDataInAcceptableRange(value) == false)
                    {
                        result = Validator.OUT_OF_RANGE_ERROR;
                    }
                }
                }
            else if (pattern != ComponentValidator.NO_FORMAT && pattern != ComponentValidator.NO_CUSTOM_TEXT_ALLOWED && pattern != ComponentValidator.CUSTOM_TEXT_ALLOWED)
            {//Pattern match is required
                if (isPatternMatch(value) == false)
                {//Data doesnt match with the given pattern
                    result = Validator.FORMAT_ERROR;
                }
                else if (!minValue.Equals("-") && !maxValue.Equals("-"))
                {
                    if (isDataInAcceptableRange(value) == false)
                    {
                        result = Validator.OUT_OF_RANGE_ERROR;
                    }
                }
            }
            if (result == Validator.NO_ERROR)
            {//If still there is no error check the existence of custom pattern
                if (!(customPattern.Length == 0))
                {
                    Regex pat = new Regex(customPattern);
                    if (pat.IsMatch(value) == false)
                    {
                        result = Validator.CUSTOM_PATTERN_MISMATCH;
                    }
                }
            }
            //Finaly display error description to control and set error indication style class
            if (result != Validator.NO_ERROR)
            {
                displayAppropriateMessage(result);
            }
            return result;
        }

        private void displayAppropriateMessage(int errorType)
        {

            if (errorType == Validator.REQUIRED_ERROR)
            {
                if (this.requiredErrorMessage.Length == 0)
                {
                    this.requiredErrorMessage = this.fieldTypeString + " Can't be empty";
                }
                //this.setErrorIndication(Validator.EMPTY_ERROR_STYLE, this.requiredErrorMessage);
                this.setErrorIndication(I_EMPTY_ERROR_STYLE, this.requiredErrorMessage);
            }
            else if (errorType == Validator.FORMAT_ERROR)
            {
                if (this.formatErrorMessage.Length == 0)
                {
                    this.formatErrorMessage = "This doesn't look like valid " + this.fieldTypeString;
                }
                //TextBoxValidator tv = new TextBoxValidator();
                //this.setErrorIndication(Validator.FORMAT_ERROR_STYLE, this.formatErrorMessage);
                this.setErrorIndication(Validator.I_FORMAT_ERROR_STYLE, this.formatErrorMessage);
            }
            else if (errorType == Validator.OUT_OF_RANGE_ERROR)
            {
                if (this.outOfRangeErrorMessage.Length == 0)
                {
                    this.outOfRangeErrorMessage = "The value you entered is not in acceptable range.";
                }
                //this.setErrorIndication(Validator.OUT_OF_RANGE_ERROR_STYLE, this.outOfRangeErrorMessage);
                this.setErrorIndication(Validator.I_OUT_OF_RANGE_ERROR_STYLE, this.outOfRangeErrorMessage);
            }
            else if (errorType == Validator.CUSTOM_PATTERN_MISMATCH)
            {
                //this.setErrorIndication(Validator.FORMAT_ERROR_STYLE, this.formatErrorMessage);

                this.setErrorIndication(Validator.I_FORMAT_ERROR_STYLE, this.formatErrorMessage);
            }
            else if (errorType == Validator.CUSTOM_TEXT_NOT_ALLOWED)
            {
                //this.setErrorIndication(Validator.FORMAT_ERROR_STYLE, this.formatErrorMessage);

                this.customText = "Select only from the list. No Custom Text Allowed.";

                this.setErrorIndication(Validator.I_CUSTOME_TEXT_STYLY, this.customText);
            }

        }

        private bool isDataInAcceptableRange(String data)
        {
            bool result = true;
            switch (pattern)
            {
                case ComponentValidator.INTEGER_NUMBER:
                    int intData = Convert.ToInt32(data);
                    result = (intData >= Convert.ToInt32(minValue) && intData <= Convert.ToInt32(maxValue));
                    break;
                case ComponentValidator.FLOAT_NUMBER:
                    if (data.Length < 10)
                    {
                        double floatData = Convert.ToDouble(data);
                        result = (floatData >= Convert.ToDouble(minValue) && floatData <= Convert.ToDouble(maxValue));
                    }
                    else
                    {
                        result = false;
                    }
                    break;
                case ComponentValidator.ETH_DATE:
                    result = (data.CompareTo(minValue) >= 0 && data.CompareTo(maxValue) <= 0);
                    break;
                case ComponentValidator.GC_DATE:
                    break;
                default:
                    result = (data.Length >= Convert.ToInt32(minValue) && data.Length <= Convert.ToInt32(maxValue));
                    break;
            }
            return result;
        }

        private bool isPatternMatch(String data) {
        bool result = false;
        Regex patt=null;
        if (data != null) {
            //YYYY-MM-DD OR YYYY/MM/DD
            switch (pattern) {

                case ComponentValidator.ETH_DATE:
                    patt = new Regex("^[ሀ-ፖ]{2,6}[ ]?([1-2][0-9]?|30)\\,[ ]?(19[0-9][0-9]|20[0-9]{2})$");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Ethiopian Date";
                    break;

                case ComponentValidator.GC_DATE: 
                    //patt=new Regex("^([1-9]|[1-2][0-9]|3[0-1])\\/([1-9]|1[0-2])\\/(19[0-9]{2,2}|20[0-9]{2,2})$");
                    patt = new Regex("^([1-9]|1[0-2])\\/([1-9]|[1-2][0-9]|3[0-1])\\/(19[0-9]{2,2}|20[0-9]{2,2})$");
                    result = patt.IsMatch(data);

                    fieldTypeString = "Gregorian Date";
                    break;                  
                case ComponentValidator.EMAIL:
                    patt = new Regex("^[_A-Za-z0-9-]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Email Address";
                    break;

                case ComponentValidator.FLOAT_NUMBER:
                     patt=new Regex("^(?:[0-9]+|[0-9]*\\.[0-9]+)$");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Number";
                    break;

                case ComponentValidator.PERSON_NAME:

                    patt = new Regex("[A-Za-z ሀ-ፖ-\\.\\&\\/ ]{1,60}");
                     result = patt.IsMatch(data);
                    fieldTypeString = "Person Name";
                    break;
                case ComponentValidator.PHONE_NUMBER:

                     patt=new Regex("^\\+?251\\-?\\-?[1-9][0-9]{2}\\-?[0-9]{6}|$|" +
                            "^[0][1-9][0-9]{2}\\-?[0-9]{6}|$|" +
                            "^\\+?[0-9]{1,3}\\-?\\-?[0-9]{3}\\-?[0-9]{6}$");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Phone number";
                    break;
                case ComponentValidator.REMARK_COMMENT:

                    patt=new Regex("[a-zA-Z0-9 \\-]{0,200}");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Remark";
                    break;


                case ComponentValidator.TEXT_AND_NUMBER:

                    patt=new Regex("^[a-zA-Z0-9\\& \\/\\.#\\ _]{2,60}$");
                    result = patt.IsMatch(data);
                    fieldTypeString = "";
                    break;

                case ComponentValidator.WEB_SITE:

                    patt=new Regex("^[w]{3}+\\.[a-zA-Z0-9-]+\\.[a-zA-Z]{2,10}+\\.[a-zA-Z ]{2}+$|^[w]{3}+\\.[a-zA-Z0-9-]+\\.[a-zA-Z ]{2,10}+$");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Web Site Address";
                    break;

                case ComponentValidator.INTEGER_NUMBER:

                    patt = new Regex("^[0-9]*$");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Number";
                    break;

                case ComponentValidator.TIME:

                    patt=new Regex("^(1|01|2|02|3|03|4|04|5|05|6|06|7|07|8|08|9|09|10|11|12{1,2}):(([0-5]{1}[0-9]{1}\\s{0,1})([AM|PM|am|pm]{2,2}))\\W{0}$");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Time";
                    break;

                case ComponentValidator.FULL_NAME:
                    patt= new Regex("[A-Za-z ሀ-ፖ-\\.\\&\\/ ]{2,60}");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Full Name";
                    break;
                case ComponentValidator.PASSWORD:
                    patt = new Regex("^.*(?=.{8,})(?=.*)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$" );
                    result = patt.IsMatch(data);
                    fieldTypeString = "Password";
                    break;
                case ComponentValidator.TEXT_A_TO_Z_ONLY:
                    patt = new Regex("^[A-Za-z\\s]*$");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Text";
                    break;
                case ComponentValidator.RESOURCE_PATH:
                    patt = new Regex("^\\/[A-Za-z0-9_\\/]+\\.aspx$");
                    result = patt.IsMatch(data);
                    fieldTypeString = "Text";
                    break;
                default:
                    result = false;
                    break;
            }
            if (customPattern.Length > 0 && result == true) {//Check if custom pattern is given, and there is no error

                Regex cuspat= new Regex(customPattern);
                result = cuspat.IsMatch(data);
            }
        }
        return result;
    }


        public abstract String getComponentData();
        public abstract void clearErrorIndication();
        public abstract void setErrorIndication(String styleClass, String message);

        /**
         * @return the pattern
         */
        public int getPattern()
        {
            return pattern;
        }

        /**
         * @param pattern the pattern to set
         */
        public void setPattern(int pattern)
        {
            this.pattern = pattern;
        }

        /**
         * @return the required
         */
        public bool isRequired()
        {
            return required;
        }

        /**
         * @param required the required to set
         */
        public void setRequired(bool required)
        {
            this.required = required;
        }

        /**
         * @return the minValue
         */
        public String getMinValue()
        {
            return minValue;
        }

        /**
         * @param minValue the minValue to set
         */
        public void setMinValue(String minValue)
        {
            this.minValue = minValue;
        }

        /**
         * @return the maxValue
         */
        public String getMaxValue()
        {
            return maxValue;
        }

        /**
         * @param maxValue the maxValue to set
         */
        public void setMaxValue(String maxValue)
        {
            this.maxValue = maxValue;
        }

        /**
         * @return the requiredErrorMessage
         */
        public String getRequiredErrorMessage()
        {
            return requiredErrorMessage;
        }

        /**
         * @param requiredErrorMessage the requiredErrorMessage to set
         */
        public void setRequiredErrorMessage(String requiredErrorMessage)
        {
            this.requiredErrorMessage = requiredErrorMessage;
        }

        /**
         * @return the formatErrorMessage
         */
        public String getFormatErrorMessage()
        {
            return formatErrorMessage;
        }

        /**
         * @param formatErrorMessage the formatErrorMessage to set
         */
        public void setFormatErrorMessage(String formatErrorMessage)
        {
            this.formatErrorMessage = formatErrorMessage;
        }

        /**
         * @return the outOfRangeErrorMessage
         */
        public String getOutOfRangeErrorMessage()
        {
            return outOfRangeErrorMessage;
        }

        /**
         * @param outOfRangeErrorMessage the outOfRangeErrorMessage to set
         */
        public void setOutOfRangeErrorMessage(String outOfRangeErrorMessage)
        {
            this.outOfRangeErrorMessage = outOfRangeErrorMessage;
        }

        public String setCustomTextNotAllowedMessage()
        {
            return customText;
        }
        
        /**
         * @param CustomText the CustomText to set
         */
        
        public void setCustomTextNotAllowedMessage(String customText)
        {
            this.customText = customText;
        }

    }
}
