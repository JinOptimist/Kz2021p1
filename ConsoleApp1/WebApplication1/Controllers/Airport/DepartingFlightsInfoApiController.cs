using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.Models.Airport;

namespace WebApplication1.Controllers.Airport
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartingFlightsInfoApiController : ControllerBase
    {
        private DepartingFlightsRepository _departingFlightsRepository { get; set; }
        private IMapper _mapper { get; set; }

        public DepartingFlightsInfoApiController(DepartingFlightsRepository departingFlightsRepository, IMapper mapper)
        {
            _departingFlightsRepository = departingFlightsRepository;
            _mapper = mapper;
        }

        // GET: api/DepartingFlightsInfo
        [HttpGet]
        public ActionResult<IEnumerable<DepartingFlightInfoViewModel>> GetDepartingFlightsInfo()
        {
            return _departingFlightsRepository.GetAll().Select(flight => _mapper.Map<DepartingFlightInfoViewModel>(flight)).ToList();
        }

        // GET: api/DepartingFlightsInfo/5
        [HttpGet("{id}")]
        public ActionResult<DepartingFlightInfo> GetDepartingFlightInfo(int id)
        {
            var departingFlightInfo = _departingFlightsRepository.Get(id);

            if (departingFlightInfo == null)
            {
                return NotFound();
            }

            return departingFlightInfo;
        }

        // PUT: api/DepartingFlightsInfo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutDepartingFlightInfo(int id, DepartingFlightInfo departingFlightInfo)
        {
            if (!_departingFlightsRepository.PutEntity(id, departingFlightInfo))
            {
                return NotFound();
            }
            return NoContent();

        }

        // POST: api/DepartingFlightsInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<DepartingFlightInfo> PostDepartingFlightInfo(DepartingFlightInfo departingFlightInfo)
        {
            _departingFlightsRepository.Save(departingFlightInfo);

            return CreatedAtAction("GetDepartingFlightInfo", new { id = departingFlightInfo.Id }, departingFlightInfo);
        }

        // DELETE: api/DepartingFlightsInfo/5
        [HttpDelete("{id}")]
        public ActionResult<DepartingFlightInfo> DeleteDepartingFlightInfo(int id)
        {
            //var departingFlightInfo = await _context.DepartingFlightsInfo.Find(id);
            var departingFlightInfo = _departingFlightsRepository.Get(id);
            if (departingFlightInfo == null)
            {
                return NotFound();
            }
            _departingFlightsRepository.Remove(departingFlightInfo);

            return departingFlightInfo;
        }
    }
}
