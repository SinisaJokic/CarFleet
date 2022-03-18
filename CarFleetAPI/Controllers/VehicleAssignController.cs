using AutoMapper;
using CarFleetAPI.Models;
using CarFleetAPI.Services;
using CarFleetAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CarFleetAPI.Controllers
{
    [ApiController]
    [Authorize(Policy = "EditorAdmin")]
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
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult<IEnumerable<VehicleAssignDto>>> GetVehicleAssigns()
        {
            var vehicleAssignEntities = await _vehicleAssignInfoRepository.GetVehicleAssignAsync();
            return Ok(_mapper.Map<IEnumerable<VehicleAssignDto>>(vehicleAssignEntities));
        }
        [HttpGet("{id}", Name = "GetVehicleAssign")]
        [Authorize(Policy = "MustBeAdmin")]
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
        [Authorize(Policy = "EditorAdmin")]
        public async Task<ActionResult<VehicleAssignDto>> CreateVehicleAssign(VehicleAssignWithoutVehicleDriverDto vehicleAssign)
        {
            //if (!await _vehicleInfoRepository.VehicleExistsAsync(Id))
            //{
            //    return NotFound();
            //}
            if (await _vehicleAssignInfoRepository.ExistsVehicleDateAsync(vehicleAssign.VehicleId, 
                vehicleAssign.FromDate, vehicleAssign.ToDate))
            {
                return ValidationProblem();
            }

            if (await _vehicleAssignInfoRepository.ExistsDriverDateAsync(vehicleAssign.DriverId,
                    vehicleAssign.FromDate, vehicleAssign.ToDate))
            {
                return ValidationProblem();
            }

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
        [Authorize(Policy = "EditorAdmin")]
        public async Task<ActionResult> UpdateVehicleAssign(int id, VehicleAssignWithoutVehicleDriverDto vehicleAssign)
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
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult> PartiallyUpdateVehicleAssign(int id,
            JsonPatchDocument<VehicleAssignWithoutVehicleDriverDto> patchDocument)
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

            var vehicleAssignToPatch = _mapper.Map<VehicleAssignWithoutVehicleDriverDto>(vehicleAssignEntity);

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
        [Authorize(Policy = "EditorAdmin")]
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
