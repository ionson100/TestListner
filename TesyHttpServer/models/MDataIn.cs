using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ORM_1_21_;

namespace TesyHttpServer.models
{
    public class MDataIn
    {
        /// <summary>
        /// Идентификатор запроса
        /// </summary>
        [DefaultValue(102034)]
        public int MessageId { get; set; }

        /// <summary>
        /// Идентификатор гостиницы
        /// </summary>
        [DefaultValue(100)]
        public int HotelId { get; set; }

        /// <summary>
        /// Идентификатор POS терминала
        /// </summary>
        [DefaultValue(1)]
        public int PosId { get; set; }

        /// <summary>
        /// Код формы ФФД 3 - кассовый чек
        /// </summary>
        public int FormCode { get; set; }

        /// <summary>
        /// Признак расчета 1-приход
        /// </summary>
        [DefaultValue(1)]
        public int OperationType { get; set; } 

        /// <summary>
        /// Сумма чека
        /// </summary>
        [DefaultValue("444230,00")]
        public string TotalSum { get; set; }

        /// <summary>
        /// Тип оплаты, нал=1 безнал=2
        /// </summary>
        [DefaultValue(1)] 
        public int PaymentMode { get; set; }
        private static Dictionary<int, MHotel> hotel = new Dictionary<int, MHotel>();
        List<MPrinter> _printers=new List<MPrinter>();





        public async Task Validate()
        {
           await Task.Run(() =>
            {
                if (HotelId == 0)
                {
                    throw new Exception("Идентификатор отеля равен 0, что не допустимо");
                }

                if (PosId == 0)
                {
                    throw new Exception("Идентификатор принтра равен 0, что не допустимо");
                }

                using var ses = Configure.GetSession();
                if (hotel.Count == 0)
                {
                    var totalH = ses.Querion<MHotel>().ToList();
                    foreach (MHotel mHotel in totalH)
                    {
                        hotel.Add(mHotel.Id,mHotel);
                    }
                }

                if (_printers.Count == 0)
                {
                    _printers = ses.Querion<MPrinter>().ToList();
                }

                var h = hotel[HotelId];
                
                if (h == null)
                {
                    throw new Exception($"Отель {HotelId} не найден в базе данны");
                }

                if (h.IsActive == false)
                {
                    throw new Exception($"Отель {HotelId} Исключен из оборота");
                }

                var p = _printers.SingleOrDefault(a => a.HotelPkId == h.PkId);

                
                if (p == null)
                {
                    throw new Exception($"Отель {HotelId} не имеет принтера {PosId}");
                }

                if (p.IsActive == false)
                {
                    throw new Exception($"Отель {HotelId} Принтре {PosId} исключен из оборота");
                }
            });

        }

    }
}
