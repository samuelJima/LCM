using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;
using System.Drawing;

namespace AIB_FORMS_VALIDATION
{
      class RadTimePickerValidator : Validator
    {
         private RadTimePicker Picker = null;

         public RadTimePickerValidator(RadTimePicker cbo)
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
            //Picker.CssClass=styleClass;


            if (styleClass.Contains("emptyErrorIndicator"))
            {

                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#FF0000");
                //Picker.BackColor = Color.Red;
            }

            else if (styleClass.Contains("formatErrorIndicator"))
            {

                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#00FF00");
                //Picker.BackColor = Color.Green;
            }

            else if (styleClass.Contains("outOffRangeErrorIndicator"))
            {

                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#0000FF");
                //Picker.BackColor = Color.Blue;
            }
            else if (styleClass.Contains("customText"))
            {

                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#FF00FF");
                //Picker.BackColor = Color.Yellow;
            }
            else
            {
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#FFFF00");
                Picker.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                //Picker.BackColor = Color.Purple;
            }
            Picker.ToolTip = message;
        }

        override
        public String getComponentData()
        {

            string date =  Picker.SelectedDate != null ? Picker.SelectedDate.ToString() : null;
            if (date != null)
            {
                return Convert.ToDateTime(date).ToShortTimeString();
            }
            else
            {
                return null;
            }
        }
    }
}
