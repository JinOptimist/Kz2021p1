using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Restorans : BaseModel
    {

        public string Name { get; set; }  // Название Ресторана
        public bool Access { get; set; } // Доступ, открыто/закрыто
        public string Address { get; set; } // Адресс
        public string PhoneNumber { get; set; } // Номер телефона
        public int AverageCheck { get; set; } // Средний чек
        public int NumberOfSeats { get; set; } // Количество мест
        public int NumberOfTables { get; set; } // Количество столов
        public string Сuisine { get; set; } // Кухня, кокого вида
        public bool WiFi { get; set; }
        public bool DanceFloor { get; set; } // Танцпол
        public bool Karaoke { get; set; }
        public bool Parking { get; set; }
        public virtual List<BronResto> Brons { get; set; }
        public virtual List<AdminResto> AdminResto { get; set; }
    }
}
