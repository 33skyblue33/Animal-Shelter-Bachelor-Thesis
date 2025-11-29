using AnimalShelter.Dto;
using AnimalShelter.Mappers;
using Domain.Entities;
using Domain.Services;
using Domain.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(long id)
        {
            User? user = await userService.GetByIdAsync(id);

            return user != null ? Ok(user.ToDto()) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UserRequest request)
        {
            UserUpdateCommand command = request.ToCommand();

            return await userService.UpdateByIdAsync(id, command) ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await userService.DeleteByIdAsync(id) ? NoContent() : NotFound();
        }
    }
}
