using AutoMapper;
using CarFleetAPI.Models;
using CarFleetAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarFleetAPI.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserModelInfoRepository _userModelInfoRepository;
        private readonly IMapper _mapper;

        public AuthenticationController(IConfiguration configuration, IUserModelInfoRepository userModelInfoRepository,
            IMapper mapper)
        {
            _configuration = configuration ?? 
                throw new ArgumentNullException(nameof(configuration));
            _userModelInfoRepository = userModelInfoRepository ??
                throw new ArgumentNullException(nameof(userModelInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public  IActionResult Authenticate([FromBody] LoginModel userData)
        {
            if (userData != null && userData.UserName != null && userData.Password != null)
            {
                var usertemp = _userModelInfoRepository.GetUserPassAsync(userData.UserName, userData.Password);
                UserModelDto user = _mapper.Map<UserModelDto>(usertemp.Result);

                //var user = GetUser(userData.UserName, userData.Password);

                if (user == null)
                {
                    return BadRequest("Invalid credentials");
                }

                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("PkUser", user.PkUser.ToString()),
                        new Claim("FirstName", user.FirstName),
                        new Claim("LastName", user.LastName),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("Roles", user.Roles),
						//new Claim("Email", user.Email)
					};

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                user.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);


                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        private UserModelDto GetUser(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return users.Find((u) => u.UserName == userName && u.Password == password);
        }
        private readonly List<UserModelDto> users = new List<UserModelDto>
        {
            new UserModelDto {PkUser = 1, UserName = "admin", Password = "securePassword", FirstName = "Admin", LastName = "Admin", Roles = "Administrator"},
            new UserModelDto {PkUser = 2, UserName = "mate", Password = "matepass", FirstName = "Mate", LastName = "Matić", Roles = "Viewer"},
            new UserModelDto {PkUser = 3, UserName = "ana", Password = "anapass", FirstName = "Ana", LastName = "Anić", Roles = "Editor"}
        };


    }
}
