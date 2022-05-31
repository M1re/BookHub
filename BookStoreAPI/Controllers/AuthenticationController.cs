using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BookStore.API.MapperModels.UserModels;
using BookStore.API.Static;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly ILogger<AuthenticationController> logger;
    private readonly UserManager<ApiUser> manager;
    private readonly IMapper mapper;

    public AuthenticationController(ILogger<AuthenticationController> logger, IMapper mapper,
        IConfiguration configuration, UserManager<ApiUser> manager)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.configuration = configuration;
        this.manager = manager;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register(UserDTO user)
    {
        logger.LogInformation($"Registration attempt for {user.Email}");
        try
        {
            var createdUser = mapper.Map<ApiUser>(user);
            createdUser.UserName = user.Email;
            var result = await manager.CreateAsync(createdUser, user.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) ModelState.AddModelError(error.Code, error.Description);
                return BadRequest(ModelState);
            }

            await manager.AddToRoleAsync(createdUser, "User");
            return Accepted();
        }
        catch (Exception ex)
        {
            return Problem($"Something is wrong in the {nameof(Register)}", statusCode: 500);
        }
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<UserAuthResponse>> Login(UserLoginDTO user)
    {
        logger.LogInformation($"Login attempt for {user.Email}");
        try
        {
            var logUser = await manager.FindByEmailAsync(user.Email);
            var validPassword = await manager.CheckPasswordAsync(logUser, user.Password);

            if (user == null || validPassword == false) return Unauthorized();
            var token = await GenerateToken(logUser);

            var response = new UserAuthResponse
            {
                Email = user.Email,
                Token = token,
                UserId = logUser.Id
            };

            return Accepted(response);
        }
        catch (Exception ex)
        {
            return Problem($"Something is wrong in the {nameof(Register)}", statusCode: 500);
        }
    }

    private async Task<string> GenerateToken(ApiUser logUser)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roles = await manager.GetRolesAsync(logUser);
        var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
        var userClaims = await manager.GetClaimsAsync(logUser);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, logUser.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, logUser.Email),
            new(CustomClaimTypes.UID, logUser.Id)
        }.Union(userClaims).Union(roleClaims);

        var token = new JwtSecurityToken(
            configuration["JwtSettings:Issuer"],
            configuration["JwtSettings:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["JwtSettings:Duration"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}