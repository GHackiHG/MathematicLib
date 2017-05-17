using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace MathematicLib
{
    public class FormulaInvoker
    {
        private string[] Invokers;
        private CSharpCodeProvider provider = new CSharpCodeProvider();
        private CompilerParameters parameters = new CompilerParameters()
        {
            GenerateInMemory = true
        };
        private CompilerResults results;
        private Type cls;
        public FormulaInvoker(string expression)
        {
            Invokers = GetCode(ParseFunction(expression));
            parameters.ReferencedAssemblies.Add("System.dll");
            results = provider.CompileAssemblyFromSource(parameters, Invokers);
            cls = results.CompiledAssembly.GetType("DynamicNS.DynamicCode");
        }
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
        private string ParseFunction(string text)
        {
            return ExpressionHelper.ProcessFuncs(text).Replace("pow", "Math.Pow").Replace("abs", "Math.Abs").Replace("ln", "Math.Log").Replace("log", "Math.Log10").Replace("sqrt", "Math.Sqrt").Replace("pi", "Math.PI");
        }
    }
}
