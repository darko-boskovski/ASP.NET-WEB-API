using Lotto_3000_App.Models.Lotto;
using Lotto_3000_App.Services.Implementations;
using Lotto_3000_App.Services.Interfaces;
using Lotto_3000_App.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lotto_3000_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinnersController : ControllerBase
    {
        private readonly IWinnersService _winnersService;
        private readonly IDrawService _drawService;

        public WinnersController(IWinnersService winnersService, IDrawService drawService)
        {
            _winnersService = winnersService;
            _drawService = drawService;
        }


        // GET: api/Winners
        [HttpGet("{id}")]
        public ActionResult<List<WinnerModel>> GetWinner(int id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _winnersService.GetWinners(id));
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
        [HttpGet]
        public ActionResult<List<WinnerModel>> GetAllWinners()
        {
          
            try
            {
               return StatusCode(StatusCodes.Status200OK, _winnersService.GetAllWinners());
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
    }
}
