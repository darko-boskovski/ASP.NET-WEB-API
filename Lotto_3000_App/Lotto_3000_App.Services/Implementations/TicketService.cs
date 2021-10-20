using Lotto_3000_App.DataAccess.Interfaces;
using Lotto_3000_App.Domain;
using Lotto_3000_App.Domain.Models;
using Lotto_3000_App.Models.Lotto;
using Lotto_3000_App.Services.Interfaces;
using Lotto_3000_App.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto_3000_App.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Session> _sessionRepository;


        public TicketService(IRepository<Ticket> ticketRepository, IRepository<Session> sessionRepository)
        {
            _ticketRepository = ticketRepository;
            _sessionRepository = sessionRepository;
        }
        public void CreateTicket(TicketModel model)
        {
            
            var combination = model.TicketCombination.Select(int.Parse).ToList();


            //var allUnique = winingCombination.GroupBy(x => x).All(g => g.Count() == 1);       /// User can enter duplicate numbers
            //if (!allUnique) throw new Exception("There are duplicate numbers!");

            var validNumbers = combination.Where(x => x >= 1 && x <= 37);
            if (validNumbers.Count() != 7) throw new TicketException("invalid random numbers, pick numbers between 1 and 37!");

            var currentSession = _sessionRepository.GetAll().LastOrDefault(x => x.NotActive == false);

           

            var ticket = new Ticket()
            {
                SessionId = currentSession.Id,
                TicketCombination = combination.Select(x => x.ToString()).ToList(),
                UserId = model.UserId
            };
            
            _ticketRepository.Add(ticket);

            model.SessionId = currentSession.Id;

        }

        public List<TicketModel> GetAll()
        {
            return _ticketRepository.GetAll().Select(x =>
               new TicketModel()
               {
                   TicketCombination = x.TicketCombination,
                   UserId = x.UserId,
                   SessionId = x.SessionId
               }
               ).ToList();
        }
    }
}
