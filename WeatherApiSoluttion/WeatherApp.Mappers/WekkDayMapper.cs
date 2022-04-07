using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;
using WeatherApp.Models;

namespace WeatherApp.Mappers
{
    public static class WekkDayMapper
    {
        public static WeekDay ToWeekDay(this WeekDayModel weekDayModel)
        {
            return new WeekDay
            {
                Id = weekDayModel.Id,
                MkName = weekDayModel.MkName,
                EngName = weekDayModel.EngName,
            };
        }

        public static WeekDayModel ToWeekDayModel(this WeekDay weekDay)
        {
            return new WeekDayModel
            {
                Id = weekDay.Id,
                MkName = weekDay.MkName,
                EngName = weekDay.EngName,
            };
        }

    }
}
