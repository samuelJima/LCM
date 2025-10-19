using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Drawing;

namespace AIB_FORMS_VALIDATION
{
    class ComboBoxValidator : Validator
    {
        private RadComboBox comboBox = null;

        public ComboBoxValidator(RadComboBox cbo)
        {
            comboBox = cbo;
        }

        override
        public void clearErrorIndication()
        {
            comboBox.BackColor = Color.White;
            String styleClass = comboBox.CssClass;

            styleClass = styleClass.Replace(EMPTY_ERROR_STYLE, "").Trim();
            styleClass = styleClass.Replace(FORMAT_ERROR_STYLE, "").Trim();
            styleClass = styleClass.Replace(OUT_OF_RANGE_ERROR_STYLE, "").Trim();

            comboBox.CssClass = styleClass;
        }

        override
        public void setErrorIndication(String styleClass, String message)
        {
            styleClass = comboBox.CssClass + " " + styleClass;
            //comboBox.CssClass=styleClass;


            if (styleClass.Equals("emptyErrorIndicator"))
            {

                comboBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                comboBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                comboBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#CC0000");
                comboBox.BackColor = Color.Red;
            }

            else if (styleClass.Equals("formatErrorIndicator"))
            {

                comboBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                comboBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                comboBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#CC0000");
                comboBox.BackColor = Color.Red;
            }

            else if (styleClass.Equals("outOffRangeErrorIndicator"))
            {

                comboBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderWidth, "1px");
                comboBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderStyle, "solid");
                comboBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#CC0000");
                comboBox.BackColor = Color.Red;
            }
            else
            {
                comboBox.Style.Add(System.Web.UI.HtmlTextWriterStyle.BorderColor, "#CC0000");
                comboBox.BackColor = Color.Red;
            }
            comboBox.ToolTip = message;
        }

        override
        public String getComponentData()
        {
            return comboBox.SelectedValue.Trim();
        }
    }

}
