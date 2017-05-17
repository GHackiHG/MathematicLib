using System;
using System.Collections.Generic;

namespace MathematicLib.Integration
{
    /// <summary>
    /// Нахождение определенного интеграла методом трапеций
    /// </summary>
    public class TrapezoidIntegration : IMathResult<IntegrationResults>
    {
        private FormulaInvoker Invoker;
        private double a;
        private double b;
        private int n;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="expression">Подинтегральное выражение</param>
        /// <param name="_a">Начало отрезка</param>
        /// <param name="_b">Конкц отрезка</param>
        /// <param name="_n">Количество интервалов</param>
        public TrapezoidIntegration(string expression, double _a, double _b, int _n)
        {
            if (_n <= 0)
                throw new ArgumentException("N can't be equal to 0 or less then 0");

            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentNullException("expression");

            Invoker = new FormulaInvoker(expression);
            if (_a < _b)
            {
                a = _a;
                b = _b;
            }
            else
            {
                a = _b;
                b = _a;
            }
            n = _n;
        }
        /// <summary>
        /// Вовращает результат вычисления и набор пар - (X;Y)
        /// </summary>
        /// <returns></returns>
        public IntegrationResults GetResult()
        {
            ChangeState("Происходит вычисление");
            double h = (b - a) / n, s = 0, TEMP = a;
            double[] x = new double[n];
            List<(double X, double Y)> Pairs = new List<(double, double)>();
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = TEMP;
                if ((x[i] == a) || (x[i] == b))
                {
                    if (double.TryParse(Invoker.GetResult(x[i]), out double sum4))
                    {
                        s = s + 1 / 2 * sum4;
                        Pairs.Add((X: x[i], Y: 1 / 2 * sum4));
                    }
                    else
                    {
                        ChangeState("Прозошла ошибка");
                        return new IntegrationResults { Result = 0, Values = new List<(double X, double Y)>() };
                    }
                }
                else
                {
                    if (double.TryParse(Invoker.GetResult(x[i]), out double sum))
                    {
                        s = s + sum;
                        Pairs.Add((X: x[i], Y: sum));
                    }
                    else
                    {
                        ChangeState("Прозошла ошибка");
                        return new IntegrationResults { Result = 0, Values = new List<(double X, double Y)>() };
                    }
                }
                TEMP = TEMP + h;
            }
            ChangeState("Успешно");
            return new IntegrationResults { Result = (h * s), Values = Pairs };
        }
        /// <summary>
        /// Обработчик изменения статуса
        /// </summary>
        public StateHandler OnChangeState;
        /// <summary>
        /// Изменяет статус вычислений
        /// </summary>
        /// <param name="message"></param>
        private void ChangeState(string message)
        {
            OnChangeState?.Invoke(message);
        }
    }
}
