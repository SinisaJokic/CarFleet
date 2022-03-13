using AutoMapper;
using CarFleetAPI.Models;
using CarFleetAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CarFleetAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/vehicle")]
    [Produces("application/json")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleInfoRepository _vehicleInfoRepository;
        private readonly IMapper _mapper;
        public VehicleController(IVehicleInfoRepository vehicleInfoRepository,
            IMapper mapper)
        {
            _vehicleInfoRepository = vehicleInfoRepository ??
                throw new ArgumentNullException(nameof(vehicleInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles()
        {
            var vehicleEntities = await _vehicleInfoRepository.GetVehiclesAsync();
            return Ok(_mapper.Map<IEnumerable<VehicleDto>>(vehicleEntities));
        }
        [HttpGet("{id}", Name = "GetVehicle")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _vehicleInfoRepository.GetVehicleAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<VehicleDto>(vehicle));
        }
        [HttpPost]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult<VehicleDto>> CreateVehicle(VehicleDto vehicle)
        {
            //if (!await _vehicleInfoRepository.VehicleExistsAsync(Id))
            //{
            //    return NotFound();
            //}

            var finalVehicle = _mapper.Map<Entities.Vehicle>(vehicle);

            await _vehicleInfoRepository.AddVehicleAsync(finalVehicle);

            await _vehicleInfoRepository.SaveChangesAsync();

            var createdVehicleToReturn =
                _mapper.Map<Models.VehicleDto>(finalVehicle);

            return CreatedAtRoute("GetVehicle",
                 new
                 {
                     id = createdVehicleToReturn.Id
                 },
                 createdVehicleToReturn);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult> UpdateVehicle(int id, VehicleDto vehicle)
        {
            if (!await _vehicleInfoRepository.VehicleExistsAsync(id))
            {
                return NotFound();
            }

            var vehicleEntity = await _vehicleInfoRepository.GetVehicleAsync(id);
            if (vehicleEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(vehicle, vehicleEntity);

            await _vehicleInfoRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateVehicle(int id,
            JsonPatchDocument<VehicleDto> patchDocument)
        {
            if (!await _vehicleInfoRepository.VehicleExistsAsync(id))
            {
                return NotFound();
            }
            var vehicleEntity = await _vehicleInfoRepository.GetVehicleAsync(id);
            if (vehicleEntity == null)
            {
                return NotFound();
            }

            var vehicleToPatch = _mapper.Map<VehicleDto>(vehicleEntity);

            patchDocument.ApplyTo(vehicleToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(vehicleToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(vehicleToPatch, vehicleEntity);
            await _vehicleInfoRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult> DeleteVehicle( int id)
        {
            if (!await _vehicleInfoRepository.VehicleExistsAsync(id))
            {
                return NotFound();
            }

            var vehicleEntity = await _vehicleInfoRepository.GetVehicleAsync(id);
            if (vehicleEntity == null)
            {
                return NotFound();
            }

            _vehicleInfoRepository.DeleteVehicle(vehicleEntity);
            await _vehicleInfoRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
