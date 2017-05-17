using System.Text.RegularExpressions;

namespace MathematicLib
{
    /// <summary>
    /// Обработка выражений перед вычислениями
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// Возвращает выражение, пригодное для вычисления
        /// </summary>
        /// <param name="text">Выражение для обработки</param>
        /// <returns></returns>
        public static string ParseFunction(string text)
        {
            return ProcessFuncs(text).Replace("pow", "Math.Pow").Replace("abs", "Math.Abs").Replace("ln", "Math.Log").Replace("log", "Math.Log10").Replace("sqrt", "Math.Sqrt").Replace("pi", "Math.PI");
        }
        /// <summary>
        /// Обрабатывает тригонометрические функции
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ProcessFuncs(string text)
        {
            return ReplaceExp(ReplaceFExp(ReplaceATan(ReplaceTan(ReplaceCos(ReplaceACos(ReplaceASin(ReplaceSin(text))))))));
        }

        /// <summary>
        /// Обработка арккосинуса
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ReplaceACos(string text)
        {
            string pattern = @"[^\W+](cos)";
            string target = "Math.Acos";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }
        /// <summary>
        /// Обработка косинуса
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ReplaceCos(string text)
        {
            string pattern = @"\b(cos)";
            string target = "Math.Cos";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }

        /// <summary>
        /// Обработка экспоненты
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ReplaceFExp(string text)
        {
            string pattern = @"[^\W+](exp)";
            string target = "Math.Exp";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }
        /// <summary>
        /// Обработка числа E
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ReplaceExp(string text)
        {
            string pattern = @"\b(exp)";
            string target = "Math.E";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }

        /// <summary>
        /// Обработка арктангенса
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ReplaceATan(string text)
        {
            string pattern = @"[^\W+](tan)";
            string target = "Math.Atan";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }
        /// <summary>
        /// Обработка тангенса
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ReplaceTan(string text)
        {
            string pattern = @"\b(tan)";
            string target = "Math.Tan";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }

        /// <summary>
        /// Обработка арксинуса
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ReplaceASin(string text)
        {
            string pattern = @"[^\W+](sin)";
            string target = "Math.Asin";
            Regex reg = new Regex(pattern);
            text = reg.Replace(text, target);
            return text;
        }
        /// <summary>
        /// Обработка синуса
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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
