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
    public class DepartingFlightsInfoApiController : ControllerBase
    {
        private IFlightsRepository _flightsRepository { get; set; }
        private IMapper _mapper { get; set; }

        public DepartingFlightsInfoApiController(IFlightsRepository flightsRepository, IMapper mapper)
        {
            _flightsRepository = flightsRepository;
            _mapper = mapper;
        }

        // GET: api/DepartingFlightsInfo
        [HttpGet]
        public ActionResult<IEnumerable<DepartingFlightInfoViewModel>> GetDepartingFlightsInfo()
        {
            return _flightsRepository.GetAll().Where(f => f.FlightType == FlightType.DepartingFlight).Select(flight => _mapper.Map<DepartingFlightInfoViewModel>(flight)).ToList();
        }

        // GET: api/DepartingFlightsInfo/5
        [HttpGet("{id}")]
        public ActionResult<Flight> GetDepartingFlightInfo(int id)
        {
            var departingFlightInfo = _flightsRepository.GetAll().SingleOrDefault(f => f.Id == id && f.FlightType == FlightType.DepartingFlight);

            if (departingFlightInfo == null)
            {
                return NotFound();
            }

            return departingFlightInfo;
        }

        // POST: api/DepartingFlightsInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Flight> PostDepartingFlightInfo(Flight departingFlightInfo)
        {
            _flightsRepository.Save(departingFlightInfo);

            return CreatedAtAction("GetDepartingFlightInfo", new { id = departingFlightInfo.Id }, departingFlightInfo);
        }

        // DELETE: api/DepartingFlightsInfo/5
        [HttpDelete("{id}")]
        public ActionResult<Flight> DeleteDepartingFlightInfo(int id)
        {
            var departingFlightInfo = _flightsRepository.Get(id);
            if (departingFlightInfo == null)
            {
                return NotFound();
            }
            _flightsRepository.Remove(departingFlightInfo);

            return departingFlightInfo;
        }
    }
}
