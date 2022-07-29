using System;
using ORM_1_21_.Attribute;

namespace TesyHttpServer.models
{
    public class BasePgHotel
    {
        /// <summary>
        /// Описание
        /// </summary>
        [MapColumnType("TEXT")]
        [MapColumnName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Включен ли объект в оборот
        /// </summary>
        [MapColumnName("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Дата последней модификации
        /// </summary>
        [MapColumnName("last_modif")]
        public DateTime LastModificationDateTime { get; set; }=DateTime.Now;
    }
}