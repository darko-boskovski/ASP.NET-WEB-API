using System;
using System.Collections.Generic;
using System.Linq;
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
    public class WeekDayService : IWeekDayInterface
    {

        private IRepository<City> _cityRepository;
        private IRepository<WeekDay> _weekDayRepository;
        private IRepository<WeatherData> _weatherDataRepository;

        public WeekDayService(IRepository<City> cityRepository, IRepository<WeekDay> weekDayRepository, IRepository<WeatherData> weatherDataRepository)
        {
            _cityRepository = cityRepository;
            _weekDayRepository = weekDayRepository;
            _weatherDataRepository = weatherDataRepository;
        }


        public void Add(WeekDayModel weekDayModel)
        {
            if (string.IsNullOrEmpty(weekDayModel.MkName))
            {
                throw new WeekDayException("Градот мора да соджи име ");
            }
            else if (weekDayModel.MkName.Length > 100)
            {
                throw new WeekDayException("Полето неможе да има повеќе од 100 карактери");
            }
            if (string.IsNullOrEmpty(weekDayModel.EngName))
            {
                throw new WeekDayException("The property Name for city is required");
            }
            else if (weekDayModel.EngName.Length > 100)
            {
                throw new WeekDayException("The property Name can't contain more then 100 characters");
            }
            if (weekDayModel.Id != 0)
            {
                throw new WeekDayException("Id must not be set!");
            }

            WeekDay weekDayForDb = weekDayModel.ToWeekDay();
            _weekDayRepository.Add(weekDayForDb);
        }

        public void Delete(int id)
        {
            WeekDay weekDayDb = _weekDayRepository.GetById(id);
            if (weekDayDb == null)
            {
                throw new NotFoundException($"Week Day with id {id} was not found");
            }
            _weekDayRepository.Delete(weekDayDb);
        }


        public List<WeekDayModel> GetAll()
        {
            List<WeekDay> weekDayDb = _weekDayRepository.GetAll();
            List<WeekDayModel> weekDayModels = new List<WeekDayModel>();
            foreach (WeekDay weekDay in weekDayDb)
            {
                weekDayModels.Add(weekDay.ToWeekDayModel());
            }
            return weekDayModels;
        }

        public WeekDayModel GetByDate(DateTime date, City city)
        {
            WeekDay weekDayDb = _weekDayRepository.GetByDate(date, city);
            if (weekDayDb == null)
            {
                throw new NotFoundException($"Week Day with date: {date} was not found");
            }

            return weekDayDb.ToWeekDayModel();
        }

        public WeekDayModel GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public WeekDayModel GetById(int id)
        {
            WeekDay weekDayDb = _weekDayRepository.GetById(id);
            if (weekDayDb == null)
            {
                throw new NotFoundException($"Week Day with date: {id} was not found");
            }

            return weekDayDb.ToWeekDayModel();
        }

        public WeekDayModel GetByName(string name)
        {
            WeekDay weekDayDb = _weekDayRepository.GetByName(name);
            if (weekDayDb == null)
            {
                throw new NotFoundException($"The Week Day: {name} was not found");
            }

            return weekDayDb.ToWeekDayModel();
        }

        public void Update(WeekDayModel weekDayModel)
        {
            WeekDay weekDayDb = _weekDayRepository.GetById(weekDayModel.Id);
            if (weekDayDb == null)
            {
                throw new NotFoundException($"City with id {weekDayModel.Id} was not found!");
            }

            if (string.IsNullOrEmpty(weekDayModel.MkName))
            {
                throw new WeekDayException("Градот мора да содржи име");
            }
            else if (weekDayModel.MkName.Length > 100)
            {
                throw new WeekDayException("Полето неможе да има повеќе од 100 карактери");
            }
            if (string.IsNullOrEmpty(weekDayModel.EngName))
            {
                throw new WeekDayException("The property Name for city is required");
            }
            else if (weekDayModel.EngName.Length > 100)
            {
                throw new WeekDayException("The property Name can't contain more then 100 characters");
            }

            weekDayDb = weekDayModel.ToWeekDay();

            _weekDayRepository.Update(weekDayDb);
        }
    }
}
