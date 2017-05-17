using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace MathematicLib
{
    /// <summary>
    /// Динамическое выполнение кода
    /// </summary>
    public class FormulaInvoker:IDisposable
    {
        /// <summary>
        /// Код для выполнения
        /// </summary>
        private string[] Invokers;
        private CSharpCodeProvider provider = new CSharpCodeProvider();
        private CompilerParameters parameters = new CompilerParameters()
        {
            GenerateInMemory = true
        };
        private CompilerResults results;
        private Type cls;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="expression">Выражение для вычисления</param>
        public FormulaInvoker(string expression)
        {
            Invokers = GetCode(ExpressionHelper.ParseFunction(expression));
            parameters.ReferencedAssemblies.Add("System.dll");
            results = provider.CompileAssemblyFromSource(parameters, Invokers);
            cls = results.CompiledAssembly.GetType("DynamicNS.DynamicCode");
        }
        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            Invokers = null;
            provider.Dispose();
            parameters = null;
            results = null;
            cls = null;
        }
        /// <summary>
        /// Возвращает результат вычислений при X
        /// </summary>
        /// <param name="x">Значение X</param>
        /// <returns></returns>
        public string GetResult(double x)
        {
            try
            {
                return cls.GetMethod("DynamicMethod", BindingFlags.Static | BindingFlags.Public).Invoke(null, new object[] { x }).ToString();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Обработка кода перед выполнением
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string[] GetCode(string expression)
        {
            return new string[]
            {
        @"using System;
 
        namespace DynamicNS
        {
            public static class DynamicCode
            {
                public static object DynamicMethod(double x)
                {
                    return " + expression + @";
                }
            }
        }"
            };
        }
    }
}
