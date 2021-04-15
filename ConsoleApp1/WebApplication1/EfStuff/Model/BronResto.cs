using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class BronResto : BaseModel
    {
        //public string Name { get; set; }  // Название Ресторана
        // public string Address { get; set; } // Адресс
        // public string PhoneNumber { get; set; } // Номер телефона ресторана
        public int BronRespNumber { get; set; } // номер брони клиента
        public int NumberOfTables { get; set; } // Количество забронированных столов
        public string PhUserNumber { get; set; } // Номер телефона клиента
        public string EmailUser { get; set; } // email клиента
        public int CountOfVisitors { get; set; } // Количество посетителей для одной брони
        public DateTime DateOfVisitors { get; set; } // Время бронирование
        public bool StateReservation { get; set; } // состояние брони

        public virtual Restorans ObjectResto { get; set; }

    }
}
