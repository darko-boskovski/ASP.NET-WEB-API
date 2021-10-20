using Lotto_3000_App.DataAccess.Interfaces;
using Lotto_3000_App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto_3000_App.DataAccess.Implementations
{
    public class SessionRepository : IRepository<Session>
    {
        private readonly LottoDbContext _lottoDbContext;
        public SessionRepository(LottoDbContext lottoDbContext)
        {
            _lottoDbContext = lottoDbContext;
        }

        public void Add(Session entity)
        {
            _lottoDbContext.Sessions.Add(entity);
            _lottoDbContext.SaveChanges();
        }

        public void Delete(Session entity)
        {
            _lottoDbContext.Sessions.Remove(entity);
            _lottoDbContext.SaveChanges();
        }

        public List<Session> GetAll()
        {
            return _lottoDbContext
                .Sessions
                .Include(x=>x.Winners)
                .ToList();
        }

        public Session GetById(int id)
        {
            return _lottoDbContext
                .Sessions
                .Include(x => x.Winners)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Session entity)
        {
            _lottoDbContext.Sessions.Update(entity);
            _lottoDbContext.SaveChanges();
        }
    }
}
