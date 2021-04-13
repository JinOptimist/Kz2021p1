using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Repositoryies.Airport;

namespace WebApplication1.ApplicationLogic.Airport
{
    public class AirportLogic
    {
        private PassengersRepository _passengersRepository { get; set; }

        public AirportLogic(PassengersRepository passengersRepository)
        {
            _passengersRepository = passengersRepository;
        }

        public void AdmitPassengers()
        {
            foreach (var passenger in _passengersRepository.GetAll().Where(passenger => passenger.Flight.FlightType == EfStuff.Model.Airport.FlightType.IncomingFlight))
            {
                passenger.Citizen.IsOutOfCity = false;
                _passengersRepository.Save(passenger);
            }
        }
    }
}
