using System.Text.RegularExpressions;

namespace MathematicLib
{
    public static class ExpressionHelper
    {
        public static string ProcessFuncs(string text)
        {
            return ReplaceExp(ReplaceFExp(ReplaceATan(ReplaceTan(ReplaceCos(ReplaceACos(ReplaceASin(ReplaceSin(text))))))));
        }

        private static string ReplaceACos(string text)
        {
            string pattern = @"[^\W+](cos)";
            string target = "Math.Acos";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }
        private static string ReplaceCos(string text)
        {
            string pattern = @"\b(cos)";
            string target = "Math.Сos";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }

        private static string ReplaceFExp(string text)
        {
            string pattern = @"[^\W+](exp)";
            string target = "Math.Exp";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }
        private static string ReplaceExp(string text)
        {
            string pattern = @"\b(exp)";
            string target = "Math.E";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }

        private static string ReplaceATan(string text)
        {
            string pattern = @"[^\W+](tan)";
            string target = "Math.Atan";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }
        private static string ReplaceTan(string text)
        {
            string pattern = @"\b(tan)";
            string target = "Math.Tan";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }

        private static string ReplaceASin(string text)
        {
            string pattern = @"[^\W+](sin)";
            string target = "Math.Asin";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }
        private static string ReplaceSin(string text)
        {
            string pattern = @"\b(sin)";
            string target = "Math.Sin";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }
    }
}
