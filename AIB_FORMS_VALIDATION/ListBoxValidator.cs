using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace AIB_FORMS_VALIDATION
{
    class ListBoxValidator : Validator
    {
        private RadListBox listBox = null;

        public ListBoxValidator(RadListBox lbo)
        {
            listBox = lbo;
        }

        override
        public void clearErrorIndication()
        {

            String styleClass = listBox.CssClass;

            styleClass = styleClass.Replace(EMPTY_ERROR_STYLE, "").Trim();
            styleClass = styleClass.Replace(FORMAT_ERROR_STYLE, "").Trim();
            styleClass = styleClass.Replace(OUT_OF_RANGE_ERROR_STYLE, "").Trim();

            listBox.CssClass = styleClass;
        }

        override
        public void setErrorIndication(String styleClass, String message)
        {
            styleClass = listBox.CssClass + " " + styleClass;
            //comboBox.CssClass=styleClass;


            if (styleClass.Equals("emptyErrorIndicator"))
            {

                listBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                listBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                listBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#fa0101");
            }

            else if (styleClass.Equals("formatErrorIndicator"))
            {

                listBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                listBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                listBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#0066ff");
            }

            else if (styleClass.Equals("outOffRangeErrorIndicator"))
            {

                listBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                listBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                listBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#0066ff");
            }
            else
            {
                listBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BackgroundColor, "purple");
            }
            listBox.ToolTip = message;
        }

        override
         public String getComponentData()
        {
            if (listBox.Items.Count == 0)
                return string.Empty;
            else if (listBox.Items.Count > 0)
                return listBox.Items.Count.ToString();
            return string.Empty;
        }
    }
}
