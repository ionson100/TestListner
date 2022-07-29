using System;
using System.Collections.Generic;
using System.Linq;
using ORM_1_21_;
using ORM_1_21_.Attribute;

namespace TesyHttpServer.models
{
    /// <summary>
    /// Модель отеля
    /// </summary>
    [MapTableName("hotel")]
    public class MHotel:BasePgHotel
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [MapPrimaryKey("id",Generator.Assigned)]
        public Guid PkId { get; set; }=Guid.NewGuid();

        /// <summary>
        /// Идентификатор отеля
        /// </summary>
        [MapIndex]
        [MapColumnName("id_core")]
        public int Id { get; set; }

        /// <summary>
        /// Название отеля
        /// </summary>
        [MapColumnName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор страны
        /// </summary>
        [MapColumnName("country")]
        public int CountryId { get; set; }

        /// <summary>
        /// Тип принтера
        /// </summary>
        [MapColumnName("defprinter")]
        public int DefaultPrinterType { get; set; }

        /// <summary>
        /// Версия принтера
        /// </summary>
        [MapColumnName("defprinterver")]
        public string DefaultPrinterVersion { get; set; }

       


        public List<MPrinter> Printers
        {
            get
            {
                using var ses=Configure.GetSession();
                return ses.Querion<MPrinter>().Where(a => a.HotelPkId == PkId).ToList();
            }
        }
    }
}