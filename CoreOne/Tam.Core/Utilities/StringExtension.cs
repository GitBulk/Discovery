using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tam.Core.Utilities
{
    public static class StringExtension
    {
        public static string GetStringUtf8(this byte[] input)
        {
            if (input != null && input.Length > 0)
            {
                return Encoding.UTF8.GetString(input);
            }
            return string.Empty;
        }
    }
}
