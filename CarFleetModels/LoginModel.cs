using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarFleetAPI.Models
{
	public class LoginModel
	{
		[Required]
		[JsonPropertyName("username")]
		public string UserName { get; set; }

		[Required]
		[JsonPropertyName("password")]
		public string Password { get; set; }
	}
}
