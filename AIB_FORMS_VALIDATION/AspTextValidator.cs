using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using AIB_FORMS_VALIDATION;

namespace AIB_FORMS_VALIDATION
{
    class AspTextValidator : Validator
    {
        //private RadInputControl textBox = null;

        private TextBox textBox = null;


        public AspTextValidator(TextBox txt)
        {
            textBox = txt;
        }



        override
        public void clearErrorIndication()
        {

            String styleClass = textBox.CssClass;

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
                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#0066ff");
            }
            else
            {
                textBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BackgroundColor, "purple");
            }

            textBox.ToolTip = message;


        }
        override
        public String getComponentData()
        {
            return textBox.Text.Trim();
        }
         

    }
}
