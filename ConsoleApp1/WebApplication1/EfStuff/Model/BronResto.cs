using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class BronResto : BaseModel
    {
        /// <summary>
     /// Номер брони клиента
     /// </summary>
        public int BronRespNumber { get; set; }

        /// <summary>
        /// Количество забронированных столов
        /// </summary>
        public int NumberOfTables { get; set; }

        /// <summary>
        /// омер телефона клиента
        /// </summary>
        public string PhUserNumber { get; set; }

        /// <summary>
        /// Email клиента
        /// </summary>
        public string EmailUser { get; set; } 

        /// <summary>
        /// Количество посетителей для одной брони
        /// </summary>
        public int CountOfVisitors { get; set; }

        /// <summary>
        /// Время бронирование
        /// </summary>
        public DateTime DateOfVisitors { get; set; } 

        /// <summary>
        /// Состояние брони
        /// </summary>
        public bool StateReservation { get; set; } 

        public virtual Restorans Restoranses { get; set; }

    }
}
