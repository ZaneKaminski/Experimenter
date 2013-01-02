using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experimenter
{
    public static class ScriptHandler
    {
        // Methods
        public static string GetArgumentValue(string arg, Slide slide)
        {
            string[] substrings = arg.Split(new char[] { '+' }, StringSplitOptions.None);
            StringBuilder retVal = new StringBuilder();
            foreach (string str in substrings)
            {
                string substring = str.Trim();
                if (substring.StartsWith("'") && substring.EndsWith("'"))
                {
                    substring = substring.Substring(1, substring.Length - 2);
                }
                else if (substring.StartsWith("#"))
                {
                    substring = substring.Substring(1);
                    string val = "";
                    for (int i = 0; i < slide.SlideObjects.Count; i++)
                    {
                        if (substring == slide.SlideObjects[i].Name)
                        {
                            val = slide.SlideObjects[i].Value;
                            i = slide.SlideObjects.Count;
                        }
                    }
                    substring = val;
                }
                retVal.Append(substring);
            }
            return retVal.ToString();
        }
    }

}
