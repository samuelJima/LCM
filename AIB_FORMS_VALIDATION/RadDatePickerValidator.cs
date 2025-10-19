using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;
using System.Drawing;

namespace AIB_FORMS_VALIDATION
{
   
    class RadDatePickerValidator : Validator
    {
        private RadDatePicker Picker = null;

        public RadDatePickerValidator(RadDatePicker cbo)
        {
            Picker = cbo;
        }

        override
        public void clearErrorIndication()
        {
            String styleClass = Picker.CssClass;
            styleClass = styleClass.Replace(EMPTY_ERROR_STYLE, "").Trim();
            styleClass = styleClass.Replace(FORMAT_ERROR_STYLE, "").Trim();
            styleClass = styleClass.Replace(OUT_OF_RANGE_ERROR_STYLE, "").Trim();
            Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
            Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
            Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#F0F8FF");
            Picker.BorderColor = Color.AliceBlue;
            Picker.ToolTip = "";
            Picker.CssClass = styleClass;
        }

        override
        public void setErrorIndication(String styleClass, String message)
        {
            styleClass = Picker.CssClass + " " + styleClass;
            if (styleClass.Contains("emptyErrorIndicator"))
            {

                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#FF0000");
            }

            else if (styleClass.Contains("formatErrorIndicator"))
            {

                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#00FF00");
            }

            else if (styleClass.Contains("outOffRangeErrorIndicator"))
            {

                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#0000FF");
            }
            else if (styleClass.Contains("customText"))
            {

                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#FF00FF");
            }
            else
            {
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#FFFF00");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
            }
            Picker.ToolTip = message;
        }

        override
       public String getComponentData()
        {

            string date = Picker.SelectedDate != null ? Picker.SelectedDate.ToString() : null;
            if (date != null)
            {
                return Convert.ToDateTime(date).ToShortDateString();
            }
            else
            {
                return null;
            }
        }
    }
}
