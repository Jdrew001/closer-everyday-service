using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.utils
{
    public static class ServiceConstants
    {
        public static readonly Dictionary<int, string> MONTHS_OF_YEAR = new Dictionary<int, string>()
        {
            {1, "JANUARY"},
            {2, "FEBRUARY"},
            {3, "MARCH"},
            {4, "APRIL"},
            {5, "MAY"},
            {6, "JUNE"},
            {7, "JULY"},
            {8, "AUGUST"},
            {9, "SEPTEMBER"},
            {10, "OCTOBER"},
            {11, "NOVEMBER"},
            {12, "DECEMBER"}
        };

        public static readonly string VALIDATION_EMAIL_KEY = "VALIDATION_EMAIL";
    }
}
