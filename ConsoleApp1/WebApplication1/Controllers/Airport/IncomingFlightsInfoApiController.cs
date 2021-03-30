using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies.Airport;

namespace WebApplication1.Controllers.Airport
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomingFlightsInfoApiController : ControllerBase
    {
        private IncomingFlightsRepository _incomingFlightInfoRepository;

        public IncomingFlightsInfoApiController(IncomingFlightsRepository incomingFlightInfoRepository)
        {
            _incomingFlightInfoRepository = incomingFlightInfoRepository;
        }
        // GET: api/DepartingFlightsInfo
        [HttpGet]
        public ActionResult<IEnumerable<IncomingFlightInfo>> GetDepartingFlightsInfo()
        {
            //return await _context.DepartingFlightsInfo.ToListAsync();
            return _incomingFlightInfoRepository.GetAll();
        }

        // GET: api/DepartingFlightsInfo/5
        [HttpGet("{id}")]
        public ActionResult<IncomingFlightInfo> GetDepartingFlightInfo(int id)
        {
            var incomingFlightInfo = _incomingFlightInfoRepository.Get(id);

            if (incomingFlightInfo == null)
            {
                return NotFound();
            }

            return incomingFlightInfo;
        }

        // PUT: api/DepartingFlightsInfo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutDepartingFlightInfo(int id, IncomingFlightInfo incomingFlightInfo)
        {
            if (!_incomingFlightInfoRepository.PutEntity(id, incomingFlightInfo))
            {
                return NotFound();
            }
            return NoContent();

        }

        // POST: api/DepartingFlightsInfo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<IncomingFlightInfo> PostDepartingFlightInfo(IncomingFlightInfo incomingFlightInfo)
        {
            _incomingFlightInfoRepository.Save(incomingFlightInfo);

            return CreatedAtAction("GetDepartingFlightInfo", new { id = incomingFlightInfo.Id }, incomingFlightInfo);
        }

        // DELETE: api/DepartingFlightsInfo/5
        [HttpDelete("{id}")]
        public ActionResult<IncomingFlightInfo> DeleteDepartingFlightInfo(int id)
        {
            //var departingFlightInfo = await _context.DepartingFlightsInfo.Find(id);
            var incomingFlightInfo = _incomingFlightInfoRepository.Get(id);
            if (incomingFlightInfo == null)
            {
                return NotFound();
            }
            _incomingFlightInfoRepository.Remove(incomingFlightInfo);

            return incomingFlightInfo;
        }
    }
}
