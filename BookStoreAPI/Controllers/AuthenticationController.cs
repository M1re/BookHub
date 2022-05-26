using AutoMapper;
using BookStore.Domain.Models;
using BookStoreAPI.MapperModels.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> manager;

        public AuthenticationController(ILogger<AuthenticationController> logger, IMapper mapper, UserManager<ApiUser> manager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.manager = manager;
        }
        [HttpPost,Route("Register")]
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
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
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

        [HttpPost,Route("Login")]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            logger.LogInformation($"Login attempt for {user.Email}");
            try
            {
                var logUser = await manager.FindByEmailAsync(user.Email);
                var validPassword = await manager.CheckPasswordAsync(logUser, user.Password);

                if (user == null || validPassword == false)
                {
                    return NotFound();
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                return Problem($"Something is wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

    }
}
