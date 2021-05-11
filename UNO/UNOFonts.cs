using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace UNO
{
    class UNOFonts
    {
        private static FontFamily family = null;
        public static Font WriteFont10 = null;
        public static Font WriteFont12 = null;
        public static Font WriteFont16 = null;
        public static Font WriteFont18 = null;

        public static FontFamily Family
        {
            get
            {
                return family;
            }
            set
            {
                family = value;
                WriteFont10 = GetUNOFont(10f);
                WriteFont12 = GetUNOFont(12f);
                WriteFont16 = GetUNOFont(16f);
                WriteFont18 = GetUNOFont(18f);
            }
        }

        public static Font GetUNOFont(float size)
        {
            return new Font(Family, size, FontStyle.Bold);
        }
    }
}
