using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = await _userManager.FindByEmailAsync(dto.Email);
            if (userExists != null)
                return BadRequest(new { message = "Email is already in use." });

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                Infix = dto.Infix,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                PostalCode = dto.PostalCode,
                City = dto.City,
                Country = dto.Country,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { errors });
            }

            // Automatically assign the 'User' role to newly registered users
            await _userManager.AddToRoleAsync(user, "User");

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized(new { message = "Invalid email or password." });

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            var token = GenerateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new 
            { 
                message = "Login successful.",
                token = token,
                firstName = user.FirstName,
                infix = user.Infix,
                email = user.Email,
                roles = roles
            });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var roles = await _userManager.GetRolesAsync(user);

            var profile = new UserProfileDto
            {
                FirstName = user.FirstName,
                Infix = user.Infix,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                PostalCode = user.PostalCode,
                City = user.City,
                Country = user.Country
            };

            return Ok(profile);
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            if (dto.Email != null && !string.Equals(user.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var existingUser = await _userManager.FindByEmailAsync(dto.Email);
                if (existingUser != null && existingUser.Id != user.Id)
                {
                    return BadRequest(new { message = "Email is already in use by another account." });
                }

                user.Email = dto.Email;
                user.UserName = dto.Email;
            }

            if (dto.FirstName != null) user.FirstName = dto.FirstName;
            if (dto.Infix != null) user.Infix = dto.Infix;
            if (dto.LastName != null) user.LastName = dto.LastName;
            if (dto.PhoneNumber != null) user.PhoneNumber = dto.PhoneNumber;
            if (dto.Address != null) user.Address = dto.Address;
            if (dto.PostalCode != null) user.PostalCode = dto.PostalCode;
            if (dto.City != null) user.City = dto.City;
            if (dto.Country != null) user.Country = dto.Country;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { errors });
            }

            return Ok(new { message = "Profile updated successfully." });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // For a JWT-based API, logging out is handled purely by the client discarding the token.
            return Ok(new { message = "Logged out successfully." });
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.CurrentPassword);
            if (!isPasswordValid)
            {
                return BadRequest(new { message = "Incorrect current password." });
            }

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { errors });
            }

            return Ok(new { message = "Password changed successfully." });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var keyString = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key not configured");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keyString));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FirstName)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
