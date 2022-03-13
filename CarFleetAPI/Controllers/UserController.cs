using AutoMapper;
using CarFleetAPI.Models;
using CarFleetAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CarFleetAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize(Policy = "MustBeAdmin")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserModelInfoRepository _userModelInfoRepository;
        private readonly IMapper _mapper;
        public UserController(IUserModelInfoRepository userModelInfoRepository,
            IMapper mapper)
        {
            _userModelInfoRepository = userModelInfoRepository ??
                throw new ArgumentNullException(nameof(userModelInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModelDto>>> GetUserModels()
        {
            var userModelsEntities = await _userModelInfoRepository.GetUserModelsAsync();
            return Ok(_mapper.Map<IEnumerable<UserModelDto>>(userModelsEntities));
        }
        [HttpGet("{pkUser}", Name = "GetUserModel")]
        public async Task<IActionResult> GetUserModel(int pkUser)
        {
            var userModel = await _userModelInfoRepository.GetUserModelAsync(pkUser);

            if (userModel == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<UserModelDto>(userModel));
        }
        [HttpPost]
        public async Task<ActionResult<UserModelDto>> CreateDriver(UserModelDto userModule)
        {
            //if (!await _vehicleInfoRepository.VehicleExistsAsync(Id))
            //{
            //    return NotFound();
            //}

            var finalUserModule = _mapper.Map<Entities.UserModel>(userModule);

            await _userModelInfoRepository.AddUserModelAsync(finalUserModule);

            await _userModelInfoRepository.SaveChangesAsync();

            var createdUserModuleToReturn =
                _mapper.Map<Models.UserModelDto>(finalUserModule);

            return CreatedAtRoute("GetUserModel",
                 new
                 {
                     id = createdUserModuleToReturn.PkUser
                 },
                 createdUserModuleToReturn);
        }

        [HttpPut("{pkUser}")]
        public async Task<ActionResult> UpdateDriver(int pkUser, UserModelDto userModule)
        {
            if (!await _userModelInfoRepository.UserModelExistsAsync(pkUser))
            {
                return NotFound();
            }

            var userModuleEntity = await _userModelInfoRepository.GetUserModelAsync(pkUser);
            if (userModuleEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(userModule, userModuleEntity);

            await _userModelInfoRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpPatch("{pkUser}")]
        public async Task<ActionResult> PartiallyUpdateVehicle(int pkUser,
            JsonPatchDocument<UserModelDto> patchDocument)
        {
            if (!await _userModelInfoRepository.UserModelExistsAsync(pkUser))
            {
                return NotFound();
            }
            var userModuleEntity = await _userModelInfoRepository.GetUserModelAsync(pkUser);
            if (userModuleEntity == null)
            {
                return NotFound();
            }

            var userModuleToPatch = _mapper.Map<UserModelDto>(userModuleEntity);

            patchDocument.ApplyTo(userModuleToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(userModuleToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(userModuleToPatch, userModuleEntity);
            await _userModelInfoRepository.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{pkUser}")]
        public async Task<ActionResult> DeleteDriver( int pkUser)
        {
            if (!await _userModelInfoRepository.UserModelExistsAsync(pkUser))
            {
                return NotFound();
            }

            var userModuleEntity = await _userModelInfoRepository.GetUserModelAsync(pkUser);
            if (userModuleEntity == null)
            {
                return NotFound();
            }

            _userModelInfoRepository.DeleteUserModel(userModuleEntity);
            await _userModelInfoRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
