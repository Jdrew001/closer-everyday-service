using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Services.utils
{
    public static class Utils
    {
        public static int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}