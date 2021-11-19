using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.utils
{
    public static class MilestoneConstants
    {
        //TODO: Map the milestone reference data
        public static int[] VALUES = {
            /*
            1 day
            7 days - 1 week
            14 days - 2 weeks
            30 days - 1 month
            180 days - 6 months
            365 days - 1 year
            2 years
            5 years
            10 years
             */
            1, 7, 14, 30, 60, (30 * 6), (30 * 12) + 5, (30 * (12 * 2) + 5), (30 * (12 * 5) + 5), (30 * (12 * 10) + 5)
        };

        public static int[] FRIEND_VALUE =
        {
            1, 5, 10, 15, 30, 60, 100
        };
    }
}
