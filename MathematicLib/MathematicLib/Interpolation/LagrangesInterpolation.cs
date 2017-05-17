using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MathematicLib.Interpolation
{
    /// <summary>
    /// Интерполяция методом Лагранжа
    /// </summary>
    public class LagrangesIngterpolation : IMathResult<LagrangesIngterpolationResults>
    {
        private double[] _xValues;
        private double[] _yValues;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="xValues">Массив значений X</param>
        /// <param name="yValues">Массив значений Y</param>
        public LagrangesIngterpolation(double[] xValues, double[] yValues)
        {
            _xValues = xValues ?? throw new ArgumentNullException("xValues");
            _yValues = yValues ?? throw new ArgumentNullException("yValues");
            if (xValues.Count() != yValues.Count())
                throw new ArgumentException("The size of the X values isn't equal to the size of the Y values");
        }
        /// <summary>
        /// Вычисляет интерполяционный полином
        /// </summary>
        /// <returns>Возвращает таблицу, полином и полином в котором приведены подобные</returns>
        public LagrangesIngterpolationResults GetResult()
        {
            var results = new LagrangesIngterpolationResults();
            var Main = CreateMain();
            var DynamicMatrix = FillDownTriangle();
            var TransMatrix = Transpored(DynamicMatrix);
            var Tabl = FillTable(DynamicMatrix,TransMatrix,Main);
            var Multiplies = new List<string>();
            var Sums = new List<string>();
            var SumOfSums2 = string.Empty;
            var MainMupltiplies = string.Empty;
            var Functions = new List<string>();
            for (int i = 0; i < Tabl.Count(); i++)
            {
                double number = 1;
                string funcX = string.Empty;
                for (int j = 0; j < Tabl[i].Count(); j++)
                {
                    if (IsNumber(Tabl[i][j]))
                    {
                        number *= Convert.ToDouble(Tabl[i][j]);
                    }
                    else
                    {
                        funcX = Tabl[i][j];
                    }
                }
                MainMupltiplies += (funcX + ((i == Tabl.Count() - 1) ? string.Empty : "*"));
                Multiplies.Add(number + "*" + funcX);
                Functions.Add(funcX);
            }
            for (int i = 0; i < Multiplies.Count(); i++)
            {
                Sums.Add("((" + _yValues[i].ToString() + "*(" + WithOutX(i, Functions) + "))/(" + Multiplies[i].Split('*')[0] + "))");
                SumOfSums2 += (Sums[i] + ((i == Multiplies.Count() - 1) ? string.Empty : "+"));
            }
            SumOfSums2 = SumOfSums2.Replace(',', '.');
            results.Polinoms = (NotSimpled:SumOfSums2,Simplied:SimplifyPolinom(SumOfSums2));
            results.Table = Tabl;
            return results;
        }
        /// <summary>
        /// Вычисляет значение при заданном X
        /// </summary>
        /// <param name="x">Значение X</param>
        /// <returns></returns>
        public double GetResultWithX(double x)
        {
            var invokers = new FormulaInvoker(GetResult().Polinoms.NotSimpled);
            return Convert.ToDouble(invokers.GetResult(x));
        }
        /// <summary>
        /// Приводит полином к кононическому виду
        /// </summary>
        /// <param name="expr">Полином</param>
        /// <returns></returns>
        private string SimplifyPolinom(string expr)
        {
            return Infix.Format(Algebraic.Expand(Infix.ParseOrUndefined(expr)));
        }
        /// <summary>
        /// Возвращает произведение функций без указанной
        /// </summary>
        /// <param name="i">Индекс функции</param>
        /// <param name="funcs">Функции</param>
        /// <returns></returns>
        private string WithOutX(int i, List<string> funcs)
        {
            string result = "";
            for (int k = 0; k < funcs.Count(); k++)
            {
                if (k != i)
                    result += (funcs[k] + ((k == funcs.Count() - 1) ? string.Empty : "*"));
            }
            if (i == funcs.Count() - 1)
                result = result.Remove(result.Count() - 1, 1);
            return result;
        }
        /// <summary>
        /// Определяет, является ли параметр number числом
        /// </summary>
        /// <param name="number">Строка для проверки</param>
        /// <returns></returns>
        private bool IsNumber(string number)
        {
            return double.TryParse(number, out double test);
        }
        /// <summary>
        /// Возвращает главную диагональ таблицы
        /// </summary>
        /// <returns></returns>
        private List<string> CreateMain()
        {
            var result = new List<string>();
            for (int i = 0; i < _xValues.Count(); i++)
            {
                if (_xValues[i] < 0)
                {
                    result.Add($"(x+{Math.Abs(_xValues[i])})");
                }
                else
                {
                    if (_xValues[i] == 0)
                    {
                        result.Add("x");
                    }
                    else
                    {
                        result.Add($"(x-{_xValues[i]})");
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Возвращает заполненную таблицу
        /// </summary>
        /// <param name="DynamicMatrix">Значение ниже главной диагонали</param>
        /// <param name="TransMatrix">Значение выше главной диагонали</param>
        /// <param name="Main">Значение главной диагонали</param>
        /// <returns></returns>
        private List<List<string>> FillTable(List<List<double>> DynamicMatrix, List<List<double>> TransMatrix, List<string> Main)
        {
            var result = CreateTableRows();
            for (int i = 0; i < DynamicMatrix.Count(); i++)
            {
                for (int j = 0; j < DynamicMatrix[i].Count(); j++)
                {
                    result[i].Add(DynamicMatrix[i][j].ToString());
                }
            }
            for (int i = 0; i < Main.Count(); i++)
            {
                result[i].Add(Main[i]);
            }
            for (int i = 0; i < TransMatrix.Count(); i++)
            {
                for (int j = 0; j < TransMatrix[i].Count(); j++)
                {
                    result[i].Add(TransMatrix[i][j].ToString());
                }
            }
            return result;
        }
        /// <summary>
        /// Возвращает таблицу с выделенными строками
        /// </summary>
        /// <returns></returns>
        private List<List<string>> CreateTableRows()
        {
            var result = new List<List<string>>();
            for (int i = 0; i < _xValues.Count(); i++)
            {
                result.Add(new List<string>());
            }
            return result;
        }
        /// <summary>
        /// Возвращает значения ниже главной диагонали
        /// </summary>
        /// <returns></returns>
        private List<List<double>> FillDownTriangle()
        {
            var result = new List<List<double>>();
            for (int i = 0; i < _xValues.Count(); i++)
            {
                result.Add(new List<double>());
                for (int j = 0; j < _xValues.Count(); j++)
                {
                    if (i > j)
                    {
                        result[i].Add(_xValues[i] - _xValues[j]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Возвращает транспонированную матрицу
        /// </summary>
        /// <param name="matrix">Матрица для транспонирования</param>
        /// <returns></returns>
        private List<List<double>> Transpored(List<List<double>> matrix)
        {
            List<List<double>> TransMatrix = new List<List<double>>();
            for (int i = 0; i < matrix.Count(); i++)
            {
                TransMatrix.Add(new List<double>());
            }
            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j = 0; j < matrix[i].Count(); j++)
                {
                    TransMatrix[j].Add(-1 * (matrix[i][j]));
                }
            }
            return TransMatrix;
        }
    }
}
