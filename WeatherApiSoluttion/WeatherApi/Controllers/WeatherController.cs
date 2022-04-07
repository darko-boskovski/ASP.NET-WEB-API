using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;
using WeatherApp.Mappers;
using WeatherApp.Models;
using WeatherApp.Services.interfaces;
using WeatherApp.Shared.Exceptions;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private ICityInterface _cityService;
        private IWeekDayInterface _weekDayService;
        private IWeatherDataInterface _weatherDataService;

        public WeatherController(ICityInterface cityService, IWeekDayInterface weekDayService, IWeatherDataInterface weatherDataService)
        {
            _cityService = cityService;
            _weekDayService = weekDayService;
            _weatherDataService = weatherDataService;
        }

        // GET api/Weather

        [HttpGet]
        public ActionResult<WeatherData> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _weatherDataService.fetchCurrentWeatherData());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // GET api/Weather/all
        [HttpGet("all")]
        public ActionResult<WeatherData> GetAll()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _weatherDataService.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // GET api/Weather/date/apr 04 2022

        [HttpGet("date/{dateInput}")]
        public ActionResult<WeatherData> GetByDate(string dateInput, City city)
        {
            try
            {
                var parsedDate = DateTime.Parse(dateInput);

                return StatusCode(StatusCodes.Status200OK, _weatherDataService.GetByDate(parsedDate, city));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // GET api/Weather/Skopje

        [HttpGet("{name}")]
        public ActionResult<List<WeatherDataModel>> GetByName(string name, string language, string date)
        {

            try
            {
                return _weatherDataService.fetchWeatherData(name, language, date);

            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message + " " + " We did not find what you are looking for, please check your input ");
            }
            catch (WeatherDataException e)
            {
               
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch(Exception e)
            {
           
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message + "Something went wrong, Please try Again!");
            }
        }
        // POST api/weather
        [HttpPost]
        public IActionResult Post([FromBody] WeatherDataModel weatherModel)
        {
            try
            {
                _weatherDataService.Add(weatherModel);
                return StatusCode(StatusCodes.Status201Created, "Weather data Created");
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (WeatherDataException e)
            {
                //log
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong, Please try Again!");
            }
        }

        // PUT api/weather/5
        [HttpPut]
        public IActionResult Put([FromBody] WeatherDataModel weatherModel)
        {
            try
            {
               _weatherDataService.Update(weatherModel);
                return StatusCode(StatusCodes.Status204NoContent, "Weather data updated!");
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (WeatherDataException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong, Please try Again!");
            }
        }

        // DELETE api/weather/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _weatherDataService.Delete(id);
                return StatusCode(StatusCodes.Status204NoContent, "Weather data Deleted!");
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception)
            {
           
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

    }
}
