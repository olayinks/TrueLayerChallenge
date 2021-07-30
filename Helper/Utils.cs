using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrueLayerChallenge.Helper
{
    public class Utils
    {
        public const string SHAKESPEARE = "shakespeare";
        public const string YODA = "yoda";

        public static string RemoveTabs(string text)
        {
            if (text == null || String.IsNullOrEmpty(text))
                return "";
            return text.Replace("\n", " ").Replace("\r", " ").Replace("\f", " ");
        }
    }
}
