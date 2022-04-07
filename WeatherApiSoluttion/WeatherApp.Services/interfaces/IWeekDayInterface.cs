using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services.interfaces
{
    public interface IWeekDayInterface
    {
        List<WeekDayModel> GetAll();
        WeekDayModel GetById(int id);
        WeekDayModel GetByName(string name);
        WeekDayModel GetByDate(DateTime date);
        void Add(WeekDayModel entity);
        void Delete(int id);
        void Update(WeekDayModel entity);
    }
}
