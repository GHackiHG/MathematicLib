using System;
using System.Collections.Generic;

namespace MathematicLib.Integration
{
    /// <summary>
    /// Нахождение определенного интеграла методом Симсона
    /// </summary>
    public class SimpsonsIntegration : IMathResult<IntegrationResults>
    {
        FormulaInvoker Invoker;
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
        public SimpsonsIntegration(string expression,double _a,double _b,int _n)
        {
            if(_n<=0)
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
            ChangeState("Вычисление");
            double sum = 0;
            double h = (b - a) / n;
            double x = a + h;
            List<(double X, double Y)> Pairs = new List<(double X, double Y)>();
            while (x < b)
            {
                if (double.TryParse(Invoker.GetResult(x), out double sum4))
                {
                    sum += 4 * sum4;
                    Pairs.Add((X:x,Y: 4 * sum4));
                    x += h;
                }
                else
                {
                    ChangeState("Прозошла ошибка");
                    return new IntegrationResults { Result = 0, Values = new List<(double X, double Y)>() };
                }

                if (double.TryParse(Invoker.GetResult(x), out double sum2))
                {
                    sum += 2 * sum2;
                    Pairs.Add((X: x, Y: 2 * sum2));
                    x += h;
                }
                else
                {
                    ChangeState("Прозошла ошибка");
                    return new IntegrationResults { Result = 0, Values = new List<(double X, double Y)>() };
                }
            }
            sum = (h / 3) * (sum + Convert.ToDouble(Invoker.GetResult(x)) - Convert.ToDouble(Invoker.GetResult(x)));
            ChangeState("Успешно");
            return new IntegrationResults { Result = sum, Values = Pairs };
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
