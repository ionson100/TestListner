using System;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace TestListener.models
{
    /// <summary>
    /// Иодель принтера
    /// </summary>
    [MapTableName("printer")]
    public class MPrinter:BasePgHotel
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [MapPrimaryKey("id", Generator.Assigned)]
        public Guid PkId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Ссылка на отель
        /// </summary>
        [MapColumnName("hotel_id")]
        public Guid HotelPkId { get; set; }

        /// <summary>
        /// Идентификатор принтера
        /// </summary>
        [MapColumnName("id_core")]
        public int Id { get; set; }

        /// <summary>
        /// Тип принтера
        /// </summary>
        [MapColumnName("type_p")]
        public int Type { get; set; }

        /// <summary>
        /// Версия принтера
        /// </summary>
        [MapColumnName("version_p")]
        public string Version { get; set; }
    }
}