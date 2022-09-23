using System;
using ApprovalForm.Modal;
using ApprovalForm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApprovalForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsers(int id)
        { 
            var user = await _userRepository.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUsers([FromBody] User user)
        {
            var newUser = await _userRepository.Create(user);
            return CreatedAtAction(nameof(GetUsers), new { id = newUser.UserId }, newUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var userToDelete = await _userRepository.Get(id);

            if (userToDelete == null)
            {
                return NotFound();
            }
            await _userRepository.Delete(userToDelete.UserId);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<User>> PutUser(int id, [FromBody] User user) 
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            await _userRepository.Update(user);
            return NoContent();
        }
    }
}

