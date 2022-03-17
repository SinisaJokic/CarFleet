using AutoMapper;
using CarFleetAPI.Models;
using CarFleetAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CarFleetAPI.Controllers
{
    [ApiController]
    [Authorize(Policy = "EditorAdmin")]
    [Route("api/driver")]
    [Produces("application/json")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverInfoRepository _driverInfoRepository;
        private readonly IMapper _mapper;
        public DriverController(IDriverInfoRepository driverInfoRepository,
            IMapper mapper)
        {
            _driverInfoRepository = driverInfoRepository ??
                throw new ArgumentNullException(nameof(driverInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetDrivers()
        {
            var driversEntities = await _driverInfoRepository.GetDriversAsync();
            return Ok(_mapper.Map<IEnumerable<DriverDto>>(driversEntities));
        }
        [HttpGet("{id}", Name = "GetDriver")]
        public async Task<IActionResult> GetDriver(int id)
        {
            var driver = await _driverInfoRepository.GetDriverAsync(id);

            if (driver == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<DriverDto>(driver));
        }
        [HttpPost]
        public async Task<ActionResult<DriverDto>> CreateDriver(DriverDto driver)
        {
            //if (!await _vehicleInfoRepository.VehicleExistsAsync(Id))
            //{
            //    return NotFound();
            //}

            var finalDriver = _mapper.Map<Entities.Driver>(driver);

            await _driverInfoRepository.AddDriverAsync(finalDriver);

            await _driverInfoRepository.SaveChangesAsync();

            var createdDriverToReturn =
                _mapper.Map<Models.DriverDto>(finalDriver);

            return CreatedAtRoute("GetDriver",
                 new
                 {
                     id = createdDriverToReturn.Id
                 },
                 createdDriverToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDriver(int id, DriverDto driver)
        {
            if (!await _driverInfoRepository.DriverExistsAsync(id))
            {
                return NotFound();
            }

            var driverEntity = await _driverInfoRepository.GetDriverAsync(id);
            if (driverEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(driver, driverEntity);

            await _driverInfoRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateVehicle(int id,
            JsonPatchDocument<DriverDto> patchDocument)
        {
            if (!await _driverInfoRepository.DriverExistsAsync(id))
            {
                return NotFound();
            }
            var driverEntity = await _driverInfoRepository.GetDriverAsync(id);
            if (driverEntity == null)
            {
                return NotFound();
            }

            var driverToPatch = _mapper.Map<DriverDto>(driverEntity);

            patchDocument.ApplyTo(driverToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(driverToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(driverToPatch, driverEntity);
            await _driverInfoRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDriver( int id)
        {
            if (!await _driverInfoRepository.DriverExistsAsync(id))
            {
                return NotFound();
            }

            var driverEntity = await _driverInfoRepository.GetDriverAsync(id);
            if (driverEntity == null)
            {
                return NotFound();
            }

            _driverInfoRepository.DeleteDriver(driverEntity);
            await _driverInfoRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
