using MoreLinq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.DataAccess.Interfaces;
using WeatherApp.Domain.Models;
using WeatherApp.Mappers;
using WeatherApp.Models;
using WeatherApp.Services.interfaces;
using WeatherApp.Shared.Exceptions;

namespace WeatherApp.Services.Implementations
{
    public class WeatherDataService : IWeatherDataInterface
    {
        private IRepository<City> _cityRepository;
        private IRepository<WeekDay> _weekDayRepository;
        private IRepository<WeatherData> _weatherDataRepository;


        private string _currentWeather = "https://api.openweathermap.org/data/2.5/weather?";
        private string _externalWeatherApiBaseURL = "https://api.openweathermap.org/data/2.5/forecast?";
        private string _apiKey = "9ca87d469790526260ee8f81ba7477e0";
        private string _sevenDaysWeatherBaseApi = "https://api.openweathermap.org/data/2.5/onecall?";

        public WeatherDataService(IRepository<City> cityRepository, IRepository<WeekDay> weekDayRepository, IRepository<WeatherData> weatherDataRepository)
        {
            _cityRepository = cityRepository;
            _weekDayRepository = weekDayRepository;
            _weatherDataRepository = weatherDataRepository;
        }

        DateTime convertDateTime(long millisecond)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddMilliseconds(millisecond).ToLocalTime();

            return day;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public void Add(WeatherDataModel weatherDataModel)
        {
            if (string.IsNullOrEmpty(weatherDataModel.City.Name))
            {
                throw new WeatherDataException("The property Name for city is required");
            }
            if (weatherDataModel.City.Name.Length > 100)
            {
                throw new WeatherDataException("The property Name can't contain more then 100 characters");
            }
            if (weatherDataModel.Id != 0)
            {
                throw new WeatherDataException("Id must not be set!");
            }

            WeatherData weatherDataForDb = weatherDataModel.ToWeatherData();
            _weatherDataRepository.Add(weatherDataForDb);

        }

        public void Delete(int id)
        {

            WeatherData weatherDb = _weatherDataRepository.GetById(id);
            if (weatherDb == null)
            {
                throw new NotFoundException($"Weather Data with id {id} was not found");
            }
            _weatherDataRepository.Delete(weatherDb);

        }

        public List<WeatherDataModel> fetchWeatherData(string cityName, string language, string date)
        {



            List<WeatherDataModel> currentDay = new List<WeatherDataModel>();

            List<WeatherDataModel> weatherDataModelList = new List<WeatherDataModel>();

     


            string url = "";

            if (string.IsNullOrEmpty(language)) language = "mk";

            url = _externalWeatherApiBaseURL + "q=" + cityName + "&units=metric&lang=" + language + "&appid=" + _apiKey + "&exclude=minutely";



            string longitude = "";
            string latitude = "";

            City city = new City();


            //get longitude and latitude
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadString(url);
                }
                catch (Exception)
                {
                     throw new NotFoundException($"There is no city by the name of {cityName}");
                }

                string json = client.DownloadString(url);


                if (string.IsNullOrEmpty(json)) throw new NotFoundException($"There is no city by the name of {cityName}");

                JObject obj = JObject.Parse(json);

                JObject cityObj = (JObject)obj.GetValue("city");

                JObject coord = (JObject)cityObj.GetValue("coord");

                longitude = coord.GetValue("lon").ToString();

                latitude = coord.GetValue("lat").ToString();
            }

            if (string.IsNullOrEmpty(longitude) || string.IsNullOrEmpty(longitude))
            {
                throw new NotFoundException($"There is no city by the name of {cityName}");
            }


            string currentWeather = _currentWeather + "lat=" + latitude + "&lon=" + longitude + "&units=metric&lang=mk&appid=" + _apiKey;

            //get seven days
            string sevenDaysUrl = _sevenDaysWeatherBaseApi + "lat=" + latitude + "&lon=" + longitude + "&units=metric&lang=" + language + "&appid=" + _apiKey + "&exclude=minutely";



            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(sevenDaysUrl);


                dynamic obj = JObject.Parse(json);



                //store city model
                CityModel cityModel = new CityModel();
                cityModel.Name = cityName;
                cityModel.Long = longitude;
                cityModel.Lat = latitude;

                city = _cityRepository.Add(cityModel.ToCity());

                WeatherDataModel weatherDataModel = new WeatherDataModel();
                //store data for 3 days
                for (int i = 0; i < 7; i++)
                {
                    DateTime current = UnixTimeStampToDateTime((double)obj.daily[i].dt);
                    //get WeekDay

                    WeekDay weekDay;
                    if ((int)current.DayOfWeek == 0)
                    {
                        weekDay = _weekDayRepository.GetById(7);
                    }
                    else
                    {
                        weekDay = _weekDayRepository.GetById((int)current.DayOfWeek);
                    }

                 

                    //store weather data
                    weatherDataModel = new WeatherDataModel();
                    weatherDataModel.Date = current.Date;
                    weatherDataModel.CityId = city.Id;
                    weatherDataModel.City = city;
                    weatherDataModel.Precipitation = obj.daily[i].pop;
                    weatherDataModel.TemperatureC = (int)obj.daily[i].temp.day;
                    weatherDataModel.TemperatureMinC = (int)obj.daily[i].temp.min;
                    weatherDataModel.TemperatureMaxC = (int)obj.daily[i].temp.max;
                    weatherDataModel.Language = language;
                    weatherDataModel.WeatherDescription = obj.daily[i].weather[0].description;
                    weatherDataModel.WindSpeed = obj.daily[i].wind_speed;
                    weatherDataModel.Icon = obj.daily[i].weather[0].icon;
                    weatherDataModel.WeekDayId = weekDay.Id;



                    WeatherData weatherData = weatherDataModel.ToWeatherData();

                    weatherDataModel = _weatherDataRepository.Add(weatherData).ToWeatherDataModel();

                    weatherDataModelList.Add(weatherDataModel);
                }
            }



            if (string.IsNullOrEmpty(date)) return weatherDataModelList;


            if (DateTime.TryParse(date, out DateTime parsedDate))
            {
                
                currentDay.Add(GetByDate(parsedDate, city));
            }

            if (currentDay.Count == 0) throw new NotFoundException($"Weather Data with date: {date} was not found. ");
            return currentDay;

        }






        public List<WeatherDataModel> GetAll()
        {
            List<WeatherData> weatherDb = _weatherDataRepository.GetAll();
            List<WeatherDataModel> weatherModels = new List<WeatherDataModel>();
            foreach (WeatherData data in weatherDb)
            {
                weatherModels.Add(data.ToWeatherDataModel());
            }
            return weatherModels;
        }

        public WeatherDataModel GetByDate(DateTime date, City city)
        {
            var weatherDb = _weatherDataRepository.GetByDate(date, city);
            if (weatherDb == null || weatherDb.ToString() == "")
            {
                throw new WeatherDataException($"Weather Data with date: {date} was not found");
            }

            return weatherDb.ToWeatherDataModel();
        }

        public WeatherDataModel GetById(int id)
        {
            WeatherData weatherDb = _weatherDataRepository.GetById(id);
            if (weatherDb == null)
            {
                throw new NotFoundException($"Weather Data with id: {id} was not found");
            }

            return weatherDb.ToWeatherDataModel();
        }

        public WeatherDataModel GetByName(string name)
        {
            WeatherData weatherDb = _weatherDataRepository.GetByName(name);
            if (weatherDb == null)
            {
                throw new NotFoundException($"Weather Data with city name {name} was not found");
            }

            return weatherDb.ToWeatherDataModel();
        }

        public void Update(WeatherDataModel weatherDataModel)
        {
            WeatherData weatherDb = _weatherDataRepository.GetById(weatherDataModel.Id);
            if (weatherDb == null)
            {
                throw new NotFoundException($"Weather Data with id {weatherDataModel.Id} was not found!");
            }

            if (string.IsNullOrEmpty(weatherDataModel.City.Name))
            {
                throw new WeatherDataException("The property Name for city is required");
            }
            if (weatherDataModel.City.Name.Length > 100)
            {
                throw new WeatherDataException("The property Name can not contain more than 100 characters");
            }

            weatherDb = weatherDataModel.ToWeatherData();

            _weatherDataRepository.Update(weatherDb);
        }

        public List<WeatherDataModel> GetThreeDays()
        {
            List<WeatherData> weatherDb = _weatherDataRepository.GetAll();
            List<WeatherDataModel> weatherModels = new List<WeatherDataModel>();
            for (int i = 0; i < weatherDb.Count; i++)
            {
                weatherModels.Add(weatherDb[i].ToWeatherDataModel());
            }
            return weatherModels;
        }

        public WeatherDataModel fetchCurrentWeatherData()

        {
            List<WeatherDataModel> currentDay = new List<WeatherDataModel>();

            string url = "";

            string cityName = "Skopje";
            string language = "mk";


            url = _externalWeatherApiBaseURL + "q=" + cityName + "&units=metric&lang=" + language + "&appid=" + _apiKey + "&exclude=minutely";



            string longitude = "";
            string latitude = "";

            //get longitude and latitude
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                JObject obj = JObject.Parse(json);

                JObject city = (JObject)obj.GetValue("city");

                JObject coord = (JObject)city.GetValue("coord");

                longitude = coord.GetValue("lon").ToString();

                latitude = coord.GetValue("lat").ToString();
            }

            if (string.IsNullOrEmpty(longitude) || string.IsNullOrEmpty(longitude))
            {
                throw new NotFoundException($"There is no city by the name of {cityName}");
            }


            string currentWeather = _currentWeather + "lat=" + latitude + "&lon=" + longitude + "&units=metric&lang=mk&appid=" + _apiKey;


            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(currentWeather);


                dynamic obj = JObject.Parse(json);

                WeatherDataModel weatherDataModel = new WeatherDataModel();

                //store city model
                CityModel cityModel = new CityModel();
                cityModel.Name = cityName;
                cityModel.Long = longitude;
                cityModel.Lat = latitude;

                City city = _cityRepository.Add(cityModel.ToCity());




                DateTime current = UnixTimeStampToDateTime((double)obj.dt);

                //get WeekDay
                WeekDay weekDay;
                if ((int)current.DayOfWeek == 0)
                {
                    weekDay = _weekDayRepository.GetById(7);
                }
                else
                {
                    weekDay = _weekDayRepository.GetById((int)current.DayOfWeek);
                }


                weatherDataModel = new WeatherDataModel();
                weatherDataModel.Date = current.Date;
                weatherDataModel.CityId = city.Id;
                weatherDataModel.TemperatureC = (int)obj.main.temp;
                weatherDataModel.TemperatureMinC = (int)obj.main.temp_min;
                weatherDataModel.TemperatureMaxC = (int)obj.main.temp_max;
                weatherDataModel.WeatherDescription = obj.weather[0].description;
                weatherDataModel.Icon = obj.weather[0].icon;
                weatherDataModel.WindSpeed = obj.wind_speed;
                weatherDataModel.WeekDayId = weekDay.Id;


                return _weatherDataRepository.Add(weatherDataModel.ToWeatherData()).ToWeatherDataModel();
            }
        }
        
    }
}
