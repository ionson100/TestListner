namespace TestListener.models
{
    /// <summary>
    /// Статус обработки запроса
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// получен
        /// </summary>
        None = 0,
        /// <summary>
        ///  ошибка
        /// </summary>
        Error = 1,
        /// <summary>
        /// напечатан
        /// </summary>
        Printed = 2

    }
}