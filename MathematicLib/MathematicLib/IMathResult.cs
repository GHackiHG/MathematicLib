namespace MathematicLib
{
    /// <summary>
    /// Интерфейс для вычислений
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMathResult<out T>
    {
        /// <summary>
        /// Возвращает результат вычислений
        /// </summary>
        /// <returns></returns>
        T GetResult();
    }
}
