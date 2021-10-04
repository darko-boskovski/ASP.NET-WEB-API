using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Workshop.Domain.Models;
using WebApi_Workshop.Models;
using WebApi_Workshop.Models.Users;
using WebApi_Workshop.Services.Interfaces;
using WebApi_Workshop.Shared.Exceptions;

namespace WebApi_Workshop_Class05.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        // GET: api/users
        [HttpGet]
        public ActionResult<User> Get()
        {

            try
            {
                return StatusCode(StatusCodes.Status200OK, _userService.GetAllUsers());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // GET api/users/3
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {


            try
            {
                return StatusCode(StatusCodes.Status200OK, _userService.GetUserById(id));
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }

        }


        // POST api/users
        [HttpPost]
        public IActionResult Post([FromBody] UserModel userModel)
        {
            try
            {
                _userService.AddUser(userModel);
                return StatusCode(StatusCodes.Status201Created, "User Created");
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (MovieException e)
            {
                //log
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        // PUT api/users/3
        [HttpPut]
        public IActionResult Put([FromBody] UserModel userModel)
        {
            try
            {
                _userService.UpdateUser(userModel);
                return StatusCode(StatusCodes.Status204NoContent, "User updated");
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (MovieException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        // DELETE api/users/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return StatusCode(StatusCodes.Status204NoContent, "User Deleted");
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }


}
