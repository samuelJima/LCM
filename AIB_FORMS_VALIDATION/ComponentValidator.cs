using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AIB_FORMS_VALIDATION
{
    public class ComponentValidator
    {
        public const int NO_FORMAT = 0;
        public const int PERSON_NAME = 1;
        public const int FULL_NAME = 2;
        public const int ETH_DATE = 3;
        public const int GC_DATE = 4;
        public const int EMAIL = 5;
        public const int PHONE_NUMBER = 6;
        public const int WEB_SITE = 7;
        public const int INTEGER_NUMBER = 8;
        public const int FLOAT_NUMBER = 9;
        public const int REMARK_COMMENT = 10;
        public const int TEXT_A_TO_Z_ONLY = 11;
        public const int TEXT_AND_NUMBER = 12;
        public const int TIME = 13;
        public const int PASSWORD = 14;
        public const int NO_CUSTOM_TEXT_ALLOWED = 15;
        public const int CUSTOM_TEXT_ALLOWED = 16;
        public const int RESOURCE_PATH = 17;
        private List<Validator> componentValidationList = new List<Validator>();

        private Validator addValidator(RadInputControl component)
        {
            Validator validator = new TextBoxValidator(component);
            componentValidationList.Add(validator);
            return validator;
        }

        private Validator addValidator(RadComboBox component)
        {
            Validator validator = new ComboBoxValidator(component);
            componentValidationList.Add(validator);
            return validator;
        }
        private Validator addValidator(TextBox component)
        {
            Validator validator = new AspTextValidator(component);
            componentValidationList.Add(validator);
            return validator;
        }

        private Validator addValidator(RadListBox component)
        {
            Validator validator = new ListBoxValidator(component);
            componentValidationList.Add(validator);
            return validator;
        }
        private Validator addValidator(RadDatePicker component)
        {
            Validator validator = new RadDatePickerValidator(component);
            componentValidationList.Add(validator);
            return validator;
        }
        private Validator addValidator(RadTimePicker component)
        {
            Validator validator = new RadTimePickerValidator(component);
            componentValidationList.Add(validator);
            return validator;
        }



        public void addComponent(RadInputControl component, int pattern, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(RadInputControl component, int pattern, String min, String max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max);
            validator.setMinValue(min);
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(RadInputControl component, int pattern, int min, int max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max.ToString());
            validator.setMinValue(min.ToString());
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(RadInputControl component, int pattern, float min, float max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max.ToString());
            validator.setMinValue(min.ToString());
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }



        /// <summary>
        /// ////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        public void addComponent(RadComboBox component, int pattern, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(RadComboBox component, int pattern, String min, String max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max);
            validator.setMinValue(min);
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(RadComboBox component, int pattern, int min, int max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max.ToString());
            validator.setMinValue(min.ToString());
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(RadComboBox component, int pattern, float min, float max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max.ToString());
            validator.setMinValue(min.ToString());
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }




        /// <summary>
        /// Asp Text Box
        /// </summary>
        /// <returns></returns>
        /// 

        public void addComponent(TextBox component, int pattern, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(TextBox component, int pattern, String min, String max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max);
            validator.setMinValue(min);
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(TextBox component, int pattern, int min, int max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max.ToString());
            validator.setMinValue(min.ToString());
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(TextBox component, int pattern, float min, float max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max.ToString());
            validator.setMinValue(min.ToString());
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        /// <summary>
        /// ////////////////
        /// </summary>
        /// <returns></returns>
        public void addComponent(RadListBox component, int pattern, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }
        public void addComponent(RadDatePicker component, int pattern, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }
        public void addComponent(RadTimePicker component, int pattern, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }
        public void addComponent(RadListBox component, int pattern, String min, String max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max);
            validator.setMinValue(min);
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(RadListBox component, int pattern, int min, int max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max.ToString());
            validator.setMinValue(min.ToString());
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }

        public void addComponent(RadListBox component, int pattern, float min, float max, bool isRequired, params String[] messages)
        {
            Validator validator = addValidator(component);
            validator.setPattern(pattern);
            validator.setRequired(isRequired);
            validator.setMaxValue(max.ToString());
            validator.setMinValue(min.ToString());
            if (messages != null)
            {
                if (messages.Length > 0)
                {
                    if (messages[0].Trim().Length > 0)
                    {
                        validator.setRequiredErrorMessage(messages[0]);
                    }
                }
                if (messages.Length > 1)
                {
                    if (messages[1].Trim().Length > 0)
                    {
                        validator.setFormatErrorMessage(messages[1]);
                    }
                }
                if (messages.Length > 2)
                {
                    if (messages[2].Trim().Length > 0)
                    {
                        validator.setOutOfRangeErrorMessage(messages[2]);
                    }
                }
            }
        }


        public bool isAllComponenetValid()
        {
            bool result = false;
            int counter = 0;
            foreach (Validator validator in componentValidationList)
            {
                counter += validator.validateData();
            }

            result = (counter == 0);
            return result;
        }
        public void clearErrorIndication()
        {

            foreach (Validator validator in componentValidationList)
            {
                validator.clearErrorIndication();
            }

        }


    }
}
