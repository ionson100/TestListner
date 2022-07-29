namespace TesyHttpServer.models
{
    /// <summary>
    /// Коды ошибок 
    /// </summary>
    public enum ErrorCode
    {
        None = 0,
        /// <summary>
        ///  Указанная гостиница отсутствует в конфигурации
        /// </summary>
        HotelIsNotConfiguration = 1,
        /// <summary>
        /// Указанный идентификатор POS-терминала отсутствует в конфигурации
        /// </summary>
        PosTerminalIsNotConfiguration = 2,
        /// <summary>
        /// Не заполнен обязательный реквизит ФД
        /// </summary>
        RequiredFDocFilled = 3,
        /// <summary>
        /// POS-терминал недоступен
        /// </summary>
        PosTerminalNotAvailable = 4,
        /// <summary>
        /// Ошибка печати
        /// </summary>
        PrintError = 5

    }
}