using System.Collections.Generic;

namespace MathematicLib.Integration
{
    /// <summary>
    /// Результаты интегрирования
    /// </summary>
    public class IntegrationResults
    {
        /// <summary>
        /// Значение интеграла
        /// </summary>
        public double Result { get; set; }
        /// <summary>
        /// Таблица значений
        /// </summary>
        public List<(double X,double Y)> Values { get; set; }
    }
}
