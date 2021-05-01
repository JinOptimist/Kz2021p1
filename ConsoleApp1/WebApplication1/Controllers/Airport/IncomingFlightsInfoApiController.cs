using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport.Intrefaces;
using WebApplication1.Models.Airport;

namespace WebApplication1.Controllers.Airport
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomingFlightsInfoApiController : ControllerBase
    {
        private IFlightsRepository _flightsRepository { get; set; }
        private IMapper _mapper { get; set; }

        public IncomingFlightsInfoApiController(IMapper mapper, IFlightsRepository flightsRepository)
        {
            _mapper = mapper;
            _flightsRepository = flightsRepository;
        }
        // GET: api/IncomingFlightsInfo
        [HttpGet]
        public ActionResult<IEnumerable<IncomingFlightInfoViewModel>> GetIncomingFlightsInfo()
        {
            return _flightsRepository.GetAll().Where(f => f.FlightType == FlightType.IncomingFlight).Select(flight => _mapper.Map<IncomingFlightInfoViewModel>(flight)).ToList();
        }

        // GET: api/IncomingFlightsInfo/5
        [HttpGet("{id}")]
        public ActionResult<Flight> GetIncomingFlightInfo(int id)
        {
            var incomingFlightInfo = _flightsRepository.GetAll().SingleOrDefault(f => f.Id == id && f.FlightType == FlightType.IncomingFlight);

            if (incomingFlightInfo == null)
            {
                return NotFound();
            }

            return incomingFlightInfo;
        }

        // POST: api/IncomingFlightsInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Flight> PostDepartingFlightInfo(Flight incomingFlightInfo)
        {
            _flightsRepository.Save(incomingFlightInfo);

            return CreatedAtAction("GetDepartingFlightInfo", new { id = incomingFlightInfo.Id }, incomingFlightInfo);
        }

        // DELETE: api/IncomingFlightsInfo/5
        [HttpDelete("{id}")]
        public ActionResult<Flight> DeleteDepartingFlightInfo(int id)
        {
            var incomingFlightInfo = _flightsRepository.Get(id);
            if (incomingFlightInfo == null)
            {
                return NotFound();
            }
            _flightsRepository.Remove(incomingFlightInfo);

            return incomingFlightInfo;
        }
    }
}
