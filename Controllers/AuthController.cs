using Microsoft.AspNetCore.Mvc;
using DevExpress.Xpo;
using TRVLR.Models;
using TRVLR.Utils;
using BCrypt.Net;
using System.Linq;

namespace TRVLR.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public AuthController()
        {
            _unitOfWork = new UnitOfWork();
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string email, string password)
        {
            if (_unitOfWork.Query<User>().Any(u => u.Email == email))
            {
                return BadRequest("User with this email already exists.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User(_unitOfWork)
            {
                Username = username,
                Email = email,
                Password = hashedPassword
            };
            _unitOfWork.Save(user);
            _unitOfWork.CommitChanges();

            // Add email verification logic

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            var user = _unitOfWork.Query<User>().FirstOrDefault(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = JwtHelper.GenerateToken(user.Email, user.Username);
            return Ok(new { Token = token });
        }

        [HttpPost("forget-password")]
        public IActionResult ForgetPassword(string email)
        {
            var user = _unitOfWork.Query<User>().FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound("User with this email does not exist.");
            }

            // Add logic to send reset password email

            return Ok("Reset password email sent.");
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(string code, string newPassword)
        {
            // Add logic to verify code and reset the password

            return Ok("Password has been reset successfully.");
        }
    }
}