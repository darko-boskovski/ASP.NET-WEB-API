using Lotto_3000_App.Helpers;
using Lotto_3000_App.Models.Lotto;
using Lotto_3000_App.Services.Interfaces;
using Lotto_3000_App.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Lotto_3000_App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IDrawService _drawService;

        public SessionController(IDrawService drawService)
        {
            _drawService = drawService;

        }

        // GET: api/session/id

        // POST: api/Session
        [AllowAnonymous]
        [HttpPost("create")]
        public ActionResult CreateSession([FromBody] SessionModel model)
        {
            int userRole = GetAuthorizedUserRole();
            try
            {

                if (userRole != 1)
                    return StatusCode(StatusCodes.Status401Unauthorized);

                _drawService.CreateSession(model);
                return StatusCode(StatusCodes.Status200OK, "New session created!");
            }

            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (SessionException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong, try again...");
            }
        }

        // POST: api/Session
        [AllowAnonymous]
        [HttpPost("close")]
        public ActionResult CloseSession([FromBody] SessionModel model)
        {
            int userRole = GetAuthorizedUserRole();
            try
            {

                if (userRole != 1)
                    return StatusCode(StatusCodes.Status401Unauthorized);

                var currentSessionId = _drawService.GetCurrentSession();

                _drawService.CloseSession(currentSessionId);

                _drawService.AddWinnersByThisSession(currentSessionId);


                return StatusCode(StatusCodes.Status200OK, "Current session closed!");
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (SessionException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong, try again...");
            }

        }
        private int GetAuthorizedUserRole()
        {
            if (!int.TryParse(User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var userRole))
            {
                throw new TicketException("Name identifier claim does not exist.");
            }
            return userRole;
        }

    }
}
