using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Domain.Models;

namespace WeatherApp.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        T GetByName(string name);
        T GetByDate(DateTime date,City city);
        T Add(T entity);
        void Delete(T entity);
        void Update(T entity);

    }
}
