using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesAPI.Domain.Models;
using SEDC.NotesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, _userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            return StatusCode(StatusCodes.Status200OK, _userService.GetUserById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _userService.AddUser(user);
            return StatusCode(StatusCodes.Status201Created, "User created!");
        }

        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            _userService.UpdateUser(user);
            return StatusCode(StatusCodes.Status204NoContent, "User updated!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return StatusCode(StatusCodes.Status204NoContent, "User deleted!");
        }
    }
}
