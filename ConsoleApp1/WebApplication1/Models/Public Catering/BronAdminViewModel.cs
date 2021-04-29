using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class BronAdminViewModel
    {
        public int BronRespNumber { get; set; } // номер брони клиента
        public int NumberOfTables { get; set; } // Количество столов
        public string PhUserNumber { get; set; } // Номер телефона клиента
        public string EmailUser { get; set; } // email клиента
        public int CountOfVisitors { get; set; } // Количество посетителей для одной брони
        public DateTime DateOfVisitors { get; set; } // Время бронирование
        public bool StateReservation { get; set; } // состояние брони
    }
}
