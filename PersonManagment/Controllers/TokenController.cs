using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using PersonManagment.Data.Models;

namespace PersonManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly Authentication.Authentication _auth;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _auth = new Authentication.Authentication(userManager, signInManager);
            _userManager = userManager;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]string username, [FromForm] string password)
        {
            try
            {
                var result = await _auth.Auth(username, password);


                if (result != null)
                {
                    return Ok(result);

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [AllowAnonymous]
        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationForm data)
        {
            var resultRegistration = await _auth.Registration(data);
            //проверка данных
            if (resultRegistration)
            {
                return Ok(200);
            }
            else
            {
                return BadRequest();
            }
        }
        [Route("changePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromForm] string userName, [FromForm] string password, [FromForm] string confirm, [FromForm] string currentPassword)
        {
            if (password != confirm) return Conflict("Пароли не совпадают");

            var user = await _auth.ChangePassword(userName, password, currentPassword);

            if (user == null) return NotFound($"\"{userName}\" не найден");

            return Ok(user);
        }




    }

    public class RegistrationForm
    {
        public string userName { get; set; }
        // public string? normalizedUserName { get; set; }
        public string? email { get; set; }
        // public string? normalizedEmail { get; set; }
        //  public bool emailConfirmed { get; set; } = false;
        public string password { get; set; }
        // public string? phone { get; set; }
        // public bool phoneNumberConfirmed { get; set; } = false;
        //  public bool twoFactorEnabled { get; set; } = false;
        //  public bool lockoutEnd { get; set; } = false;
        //  public int accessFailedCount { get; set; } = 0;

    }
    // Ключ для создания подписи (приватный)
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }

    // Ключ для проверки подписи (публичный)
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
    public class SigningSymmetricKey : IJwtSigningEncodingKey, IJwtSigningDecodingKey
    {
        private readonly SymmetricSecurityKey _secretKey;

        public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;

        public SigningSymmetricKey(string key)
        {
            this._secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public SecurityKey GetKey() => this._secretKey;
    }
}
