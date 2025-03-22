using NPOI.SS.Util;
using System;
using System.Linq;

namespace TemplateService.Business.Helper
{
    public static class GeneratCode
    {
        static Random random = new Random();

        public static string GeneratNumbersAscending(int length, string lastCode)
        {
            string code;
            string decimalFormat = GetDecimalFormat(length);
            if (lastCode == null)
            {
                DecimalFormat df = new DecimalFormat(decimalFormat);
                code = df.Format(int.Parse(decimalFormat) + 1).ToString();
                return code;
            }
            else
            {
                DecimalFormat df = new DecimalFormat(decimalFormat);
                code = df.Format(int.Parse(lastCode) + 1).ToString();
                return code;
            }
        }
        public static string GeneratNumbersAscending(int length, string lastCode,int skip)
        {
            string code;
            string decimalFormat = GetDecimalFormat(length);
            if (lastCode == null)
            {
                DecimalFormat df = new DecimalFormat(decimalFormat);
                code = df.Format(int.Parse(decimalFormat) + 1).ToString();
                return code;
            }
            else
            {
                DecimalFormat df = new DecimalFormat(decimalFormat);
                code = df.Format(int.Parse(lastCode) + skip).ToString();
                return code;
            }
        }
        public static string GetDecimalFormat(int? lengthCode)
        {
            string code = null;

            for (int i = 0; i < lengthCode; i++)
            {
                code += "0";

            }

            return code;
        }
        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }
    }



}
