namespace TestListener.models
{
    /// <summary>
    /// Ответ в систему опраления гостиниц
    /// </summary>
    public class MDataOut
    {

        public MDataOut(MDataIn mDataIn)
        {
           HotelId = mDataIn.HotelId;
           PosId = mDataIn.PosId;
           MessageID = mDataIn.MessageId;
        }

        /// <summary>
        /// Идентификатор запроса
        /// </summary>
        public int MessageID { get; set; }
        /// <summary>
        /// Идентификатор гостиницы
        /// </summary>
        public int HotelId { get; set; }
        /// <summary>
        /// Идентификатор POS терминала
        /// </summary>
        public int PosId { get; set; }
        /// <summary>
        /// Статус запроса
        /// </summary>
        public Status Status { get; set; } = Status.None;
        /// <summary>
        /// Код ошибки
        /// </summary>
        public ErrorCode ErrorCode { get; set; } = ErrorCode.None;
        /// <summary>
        /// Заполняется при наличии
        /// </summary>
        public string ErrorText { get; set; }//

        public MDataOut SetErrorData(ErrorCode code, string errorText)
        {
            Status = Status.Error;
            ErrorCode = code;
            ErrorText = errorText;
            return this;
        }
        public InnerRunner InnerRunner=new InnerRunner();
        
    }

    public class InnerRunner
    {
        public bool IsError { get; set; } = true;
        public string ErrorText { get; set; }
    }

}