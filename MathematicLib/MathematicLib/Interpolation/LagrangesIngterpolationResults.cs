using System.Collections.Generic;

namespace MathematicLib.Interpolation
{
    /// <summary>
    /// Результаты вычисления полинома Лагранжа
    /// </summary>
    public class LagrangesIngterpolationResults
    {
        /// <summary>
        /// Таблица значений
        /// </summary>
        public List<List<string>> Table { get; set; }
        /// <summary>
        /// NotSimpled - полином, Simplied - полином, где приведены подобные
        /// </summary>
        public (string NotSimpled,string Simplied) Polinoms { get; set; }
    }
}
