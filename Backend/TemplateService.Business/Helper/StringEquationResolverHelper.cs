using System;
using System.Collections.Generic;
using System.Linq;

namespace TemplateService.Business.Helper
{
    public static class StringEquationResolverHelper
    {

        public static decimal Evaluate(string exp)
        {
            string str4 = "(" + exp.Replace(" ", "") + ")";
            str4 = str4.Replace(")(", ")*(");
            while (str4.Contains('('))
            {
                string sub1 = str4.Substring(str4.LastIndexOf("(") + 1);
                string sub = sub1.Substring(0, sub1.IndexOf(")"));
                string sub2 = sub;
                string str21 = sub2.Replace("^", "~^~").Replace("/", "~/~").Replace("*", "~*~").Replace("+", "~+~").Replace("-", "~-~");
                List<string> str31 = str21.Split('~').ToList();
                while (str31.Count > 1)
                {
                    while (str31.Contains(""))
                    {
                        for (int i = 0; i < str31.Count; i++)
                        {
                            if (str31[i] == "")
                            {
                                var val = str31[i+1] + str31[i + 2];
                                str31.RemoveRange(i, 3);
                                str31.Insert(i , val);
                            }
                        }
                    }
                     while (str31.Contains("/"))
                    {
                        for (int i = 0; i < str31.Count; i++)
                        {
                            if (str31[i] == "/")
                            {
                                var val = Convert.ToDouble(str31[i - 1]) / Convert.ToDouble(str31[i + 1] == "0" ? 1 : str31[i + 1] == "" ? 1 : str31[i + 1]);
                                str31.RemoveRange(i - 1, 3);
                                str31.Insert(i - 1, val.ToString());
                            }
                        }
                    }
                    while (str31.Contains("*"))
                    {
                        for (int i = 0; i < str31.Count; i++)
                        {
                            if (str31[i] == "*")
                            {
                                var val = Convert.ToDouble(str31[i - 1]) * Convert.ToDouble(str31[i + 1] == "" ? "1" : str31[i + 1]);
                                str31.RemoveRange(i - 1, 3);
                                str31.Insert(i - 1, val.ToString());
                            }
                        }
                    }                  
                    while (str31.Contains("-"))
                    {
                        for (int i = 0; i < str31.Count; i++)
                        {
                            if (str31[i] == "-")
                            {
                                var val = Convert.ToDouble(str31[i - 1] == "" ? "0" : str31[i - 1]) - Convert.ToDouble(str31[i + 1]);
                                str31.RemoveRange(i - 1, 3);
                                str31.Insert(i - 1, val.ToString());
                            }
                        }
                    }
                    while (str31.Contains("+"))
                    {
                        for (int i = 0; i < str31.Count; i++)
                        {
                            if (str31[i] == "+")
                            {
                                var val = Convert.ToDouble(str31[i - 1]) + Convert.ToDouble(str31[i + 1]);
                                str31.RemoveRange(i - 1, 3);
                                str31.Insert(i - 1, val.ToString());
                            }
                        }
                    }
                }
                str4 = str4.Replace("(" + sub + ")", str31[0].ToString());
            }
            string sum = str4;
            if (sum == "" || sum == null)
                return 0;
            return decimal.Parse(sum);
        }


    }
}
