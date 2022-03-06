using AutoMapper;
using CarFleetAPI.Models;
using CarFleetAPI.Services;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CarFleetAPI.Controllers
{
    [ApiController]
    [Route("api/vehicleAssign")]
    [Produces("application/json")]
    public class VehicleAssignController : ControllerBase
    {
        private readonly IVehicleAssignRepository _vehicleAssignInfoRepository;
        private readonly IMapper _mapper;
        public VehicleAssignController(IVehicleAssignRepository vehicleAssignRepository,
            IMapper mapper)
        {
            _vehicleAssignInfoRepository = vehicleAssignRepository ??
                throw new ArgumentNullException(nameof(vehicleAssignRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleAssignDto>>> GetVehicleAssigns()
        {
            var vehicleAssignEntities = await _vehicleAssignInfoRepository.GetVehicleAssignAsync();
            return Ok(_mapper.Map<IEnumerable<VehicleAssignDto>>(vehicleAssignEntities));
        }
        [HttpGet("{id}", Name = "GetVehicleAssign")]
        public async Task<IActionResult> GetVehicleAssign(int id)
        {
            var vehicleAssign = await _vehicleAssignInfoRepository.GetVehicleAssignAsync(id);

            if (vehicleAssign == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<VehicleAssignDto>(vehicleAssign));
        }
        [HttpPost]
        public async Task<ActionResult<VehicleAssignDto>> CreateVehicleAssign(VehicleAssignDto vehicleAssign)
        {
            //if (!await _vehicleInfoRepository.VehicleExistsAsync(Id))
            //{
            //    return NotFound();
            //}

            var finalVehicalAssign = _mapper.Map<Entities.VehicleAssign>(vehicleAssign);

            await _vehicleAssignInfoRepository.AddVehicleAssignAsync(finalVehicalAssign);

            await _vehicleAssignInfoRepository.SaveChangesAsync();

            var createdVehicleAssignToReturn =
                _mapper.Map<VehicleAssignDto>(finalVehicalAssign);

            return CreatedAtRoute("GetVehicleAssign",
                 new
                 {
                     id = createdVehicleAssignToReturn.Id
                 },
                 createdVehicleAssignToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicleAssign(int id, VehicleAssignDto vehicleAssign)
        {
            if (!await _vehicleAssignInfoRepository.VehicleAssignExistsAsync(id))
            {
                return NotFound();
            }

            var vehicleAssignEntity = await _vehicleAssignInfoRepository.GetVehicleAssignAsync(id);
            if (vehicleAssignEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(vehicleAssign, vehicleAssignEntity);

            await _vehicleAssignInfoRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdateVehicleAssign(int id,
            JsonPatchDocument<VehicleAssignDto> patchDocument)
        {
            if (!await _vehicleAssignInfoRepository.VehicleAssignExistsAsync(id))
            {
                return NotFound();
            }
            var vehicleAssignEntity = await _vehicleAssignInfoRepository.GetVehicleAssignAsync(id);
            if (vehicleAssignEntity == null)
            {
                return NotFound();
            }

            var vehicleAssignToPatch = _mapper.Map<VehicleAssignDto>(vehicleAssignEntity);

            patchDocument.ApplyTo(vehicleAssignToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(vehicleAssignToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(vehicleAssignToPatch, vehicleAssignEntity);
            await _vehicleAssignInfoRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDriver( int id)
        {
            if (!await _vehicleAssignInfoRepository.VehicleAssignExistsAsync(id))
            {
                return NotFound();
            }

            var vehicleAssignEntity = await _vehicleAssignInfoRepository.GetVehicleAssignAsync(id);
            if (vehicleAssignEntity == null)
            {
                return NotFound();
            }

            _vehicleAssignInfoRepository.DeleteVehicleAssign(vehicleAssignEntity);
            await _vehicleAssignInfoRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
