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
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lotto_3000_App.Controllers

{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketsService;
        public TicketsController(ITicketService ticketsService)
        {
            _ticketsService = ticketsService;
        }
        // POST: api/Tickets
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromBody] TicketModel model)
        {
            var userId = GetAuthorizedUserId();
            try
            {

                model.UserId = userId;
            _ticketsService.CreateTicket(model);

            return StatusCode(StatusCodes.Status200OK, $"Ticket successfully created! Wait for the draw to end and then enter the following address in the url bar \"http://localhost:24260/api/players/winners\" + your ticket session id `{model.SessionId}`. If you won a prize you will see your name on the board. Good Luck!");
            }

            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (TicketException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong, try again...");
            }
        }
    
        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var userId))
            {
                throw new Exception("Name identifier claim does not exist.");
            }
            return userId;
        }


    }
}
