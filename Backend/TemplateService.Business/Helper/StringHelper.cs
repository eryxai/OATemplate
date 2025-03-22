using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateService.Business.Helper
{
    public static class StringHelper
    {
        static Dictionary<char, char> charDictionary = new Dictionary<char, char>();

        static StringHelper()
        {
            charDictionary['ض'] = 'q';
            charDictionary['ص'] = 'w';
            charDictionary['ث'] = 'e';
            charDictionary['ق'] = 'r';
            charDictionary['ف'] = 't';
            charDictionary['غ'] = 'y';
            charDictionary['ع'] = 'u';
            charDictionary['ه'] = 'i';
            charDictionary['خ'] = 'o';
            charDictionary['ح'] = 'p';
            charDictionary['ش'] = 'a';
            charDictionary['س'] = 's';
            charDictionary['ي'] = 'd';
            charDictionary['ب'] = 'f';
            charDictionary['ل'] = 'g';
            charDictionary['ا'] = 'h';
            charDictionary['ت'] = 'j';
            charDictionary['ن'] = 'k';
            charDictionary['م'] = 'l';
            charDictionary['ئ'] = 'z';
            charDictionary['ء'] = 'x';
            charDictionary['ؤ'] = 'c';
            charDictionary['ر'] = 'v';
            charDictionary['ى'] = 'n';
            charDictionary['ة'] = 'm';


            charDictionary['أ'] = 'h';
            charDictionary['['] = 'f';
            charDictionary['‘'] = 'u';
            charDictionary['÷'] = 'i';
            charDictionary['×'] = 'o';
            charDictionary['؛'] = 'p';
            charDictionary[']'] = 'd';
            charDictionary['ـ'] = 'j';
            charDictionary['،'] = 'k';
            charDictionary['/'] = 'l';
            charDictionary['~'] = 'z';
            charDictionary['ْ'] = 'x';
            charDictionary['}'] = 'c';
            charDictionary['{'] = 'v';
            charDictionary['آ'] = 'n';
            charDictionary['’'] = 'm';

            charDictionary['ً'] = 'w';
            charDictionary['ِ'] = 'a';
            charDictionary['َ'] = 'q';
            charDictionary['ُ'] = 'e';
            charDictionary['ٌ'] = 'r';
            charDictionary['ٍ'] = 's';

        }

        public static string ConvertKeyCodeToEnglish(string keyCode)
        {
            keyCode = keyCode.ToLower();
            keyCode = keyCode.Replace("لا", "b");
            keyCode = keyCode.Replace("لإ", "t");
            keyCode = keyCode.Replace("لأ", "g");
            keyCode = keyCode.Replace("لإ", "y");
            keyCode = keyCode.Replace("لآ", "b");

            char[] keyCodeChar = keyCode.ToCharArray();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in keyCodeChar)
            {
                try
                {
                    stringBuilder.Append(charDictionary[item]);
                }
                catch (Exception)
                {
                    stringBuilder.Append(item);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
