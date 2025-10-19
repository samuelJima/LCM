using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIB_FORMS_VALIDATION
{
    public class Class1
    {
       
            double width = 34; double l = 34; double perimeter;
            public double area()
            {


                return width * l;

            }
            private double Calperimeter()
            {
                perimeter = 2 * (l * width);
                return perimeter;
            }

            private string check()
            {
                string str = "working";
                return str;
            }
        }
    }

