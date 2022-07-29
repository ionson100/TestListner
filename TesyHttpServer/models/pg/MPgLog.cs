using System;
using Newtonsoft.Json;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace TesyHttpServer.models.pg
{
    [MapTableName("mylog")]
    public class MPgLog
    {
        private MPgLog(){}
        public MPgLog(MDataIn dataIn, MDataOut dataOut)
        {
            this.Status = (int)dataOut.Status;
            this.ErrorCode = (int) dataOut.ErrorCode;
            this.DataJson = JsonConvert.SerializeObject(dataIn);
            this.DateTime=DateTime.Now;
            this.ErrorText = dataOut.ErrorText;
            this.FormCode = (int)dataIn.FormCode;
            this.HotelId = dataIn.HotelId;
            this.MessageId = dataIn.MessageId;
            this.PosId = dataIn.PosId;
            this.OperationType = (int)dataIn.OperationType;
        }

        public void SaveLog()
        {
            Configure.GetSession().Save(this);
        }
        [MapPrimaryKey("id",Generator.Assigned)]
        public Guid id { get; set; }=Guid.NewGuid();
        /// <summary>
        /// Время запроса
        /// </summary>
        [MapColumnName("datetime")]
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Идентификатор запроса
        /// </summary>

        [MapColumnName("messageid")]
        public int MessageId { get; set; }

        /// <summary>
        /// Идентификатор гостиницы
        /// </summary>
        [MapColumnName("hotelid")]
        public int HotelId { get; set; }

        /// <summary>
        /// Идентификатор POS терминала
        /// </summary>
        [MapColumnName("postid")]
        public int PosId { get; set; }

        /// <summary>
        /// Код формы ФФД 3 - кассовый чек
        /// </summary>
        [MapColumnName("formcode")]
        public int FormCode { get; set; }

        /// <summary>
        /// Признак расчета 1-приход
        /// </summary>
        [MapColumnName("oprationtype")]
        public int OperationType { get; set; }

        /// <summary>
        /// json запроса
        /// </summary>
        [MapColumnName("datajson")]
        [MapColumnType("TEXT")]
        public string DataJson { get; set; }

        /// <summary>
        /// Статус обработки
        /// </summary>
        [MapColumnName("status")]
        public int Status { get; set; }

        /// <summary>
        /// Код ошибки
        /// </summary>
        [MapColumnName("errorcode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Заполняется при наличии
        /// </summary>
        [MapColumnName("errortext")]
        public string ErrorText { get; set; }

    }
}
