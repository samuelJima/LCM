using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AIB_FORMS_VALIDATION
{
    class TextBoxValidator : Validator
    {
        private RadInputControl textBox = null;
        public TextBoxValidator(RadInputControl txt)
        {
            textBox = txt;
        }

        override
        public void clearErrorIndication()
        {

            String styleClass = textBox.CssClass;
            textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
            textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
            textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#d8e9fb");
            textBox.ToolTip = string.Empty;
            styleClass = styleClass.Replace(EMPTY_ERROR_STYLE, "").Trim();
            styleClass = styleClass.Replace(FORMAT_ERROR_STYLE, "").Trim();
            styleClass = styleClass.Replace(OUT_OF_RANGE_ERROR_STYLE, "").Trim();

            textBox.CssClass = styleClass;
        }

        override
        public void setErrorIndication(String styleClass, String message)
        //public void setErrorIndication(int styleClass, String message)
        {
            //styleClass = textBox.CssClass + " " + styleClass;
            textBox.CssClass = styleClass;

            if (styleClass.Equals("emptyErrorIndicator"))
            {

                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#fa0101");

            }

            else if (styleClass.Equals("formatErrorIndicator"))
            {

                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "2px");
                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "yellow");
                //textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BackgroundColor, "yellow");
            }

            else if (styleClass.Equals("outOffRangeErrorIndicator"))
            {

                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#7e01fa");
            }
            else
            {
                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BackgroundColor, "purple");
            }

            textBox.ToolTip = message;
            textBox.Attributes.Add("onmouseover", "displayMessage(this)");
            textBox.Attributes.Add("onmouseout", "hideMessage(this)");



        }
        override
        public String getComponentData()
        {
            return textBox.Text.Trim();
        }
         
    }
}
