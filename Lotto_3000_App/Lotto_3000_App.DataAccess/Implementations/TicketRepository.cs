using Lotto_3000_App.DataAccess.Interfaces;
using Lotto_3000_App.Domain;
using Lotto_3000_App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto_3000_App.DataAccess.Implementations
{
    public class TicketRepository : IRepository<Ticket>
    {

        private readonly LottoDbContext _lottoDbContext;

        public TicketRepository(LottoDbContext lottoDbContext)
        {
            _lottoDbContext = lottoDbContext;
        }
        public void Add(Ticket entity)
        {
            _lottoDbContext.Tickets.Add(entity);
            _lottoDbContext.SaveChanges();
        }

        public void Delete(Ticket entity)
        {
            _lottoDbContext.Tickets.Remove(entity);
            _lottoDbContext.SaveChanges();
        }

        public List<Ticket> GetAll()
        {
            return _lottoDbContext
                .Tickets
                .Include(x => x.User)
                .ToList();
        }

        public Ticket GetById(int id)
        {
            return _lottoDbContext
                .Tickets
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Ticket entity)
        {
            _lottoDbContext.Tickets.Update(entity);
            _lottoDbContext.SaveChanges();
        }

    }
}
