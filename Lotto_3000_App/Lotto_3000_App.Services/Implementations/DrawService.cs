using Lotto_3000_App.DataAccess.Interfaces;
using Lotto_3000_App.Domain;
using Lotto_3000_App.Domain.Models;
using Lotto_3000_App.Models.Lotto;
using Lotto_3000_App.Services.Interfaces;
using Lotto_3000_App.Shared.Enums;
using Lotto_3000_App.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Lotto_3000_App.Services.Implementations
{
    public class DrawService : IDrawService
    {

        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Winner> _winnersRepository;
        private readonly IUserRepository _userRepository;
        public DrawService(IUserRepository userRepository, IRepository<Session> sessionRepository, IRepository<Ticket> ticketRepository, IRepository<Winner> winnersRepository)
        {
            _sessionRepository = sessionRepository;
            _ticketRepository = ticketRepository;
            _winnersRepository = winnersRepository;
            _userRepository = userRepository;
        }

        public void AddWinnersByThisSession(int sessionId)
        {
            var sessionNumbers = _sessionRepository.GetAll().SingleOrDefault(x => x.Id == sessionId).WinningCombination;



            var winners = GetWinningTickets(sessionId)
                .Select(x =>
                new Winner
                {
                    Fullname = GetUserName(x.UserId),
                    TicketCombination = x.TicketCombination,
                    SessionId = x.SessionId

                }); ;
            if(winners == null)
                throw new NotFoundException($"There are no winners in this session");

            foreach (var item in winners)
            {

                var prize = SetPrize(item.TicketCombination.Select(int.Parse).ToList().Intersect(sessionNumbers.Select(int.Parse).ToList()).Count());
                item.Prize = (int)prize;
                _winnersRepository.Add(item);

            }
        }

        public void CloseSession(int currentSessionId)
        {


            var currentSession = _sessionRepository.GetAll().SingleOrDefault(x => x.Id == currentSessionId);
            var random = new Random();
            var winningCombo = Enumerable.Range(1, 37).OrderBy(x => random.Next()).Take(8).ToList(); /*new List<string> { "1", "2", "3", "4", "5", "6", "7" };*/
            currentSession.WinningCombination = winningCombo.Select(x => x.ToString()).ToList();
            currentSession.NotActive = true;
            _sessionRepository.Update(currentSession);
        }

        public void CreateSession(SessionModel model)
        {

            Session newSession = new Session
            {   
           
                TimeCreated = DateTime.Now,
                NotActive = false
                
            };
            _sessionRepository.Add(newSession);
        }

        public int GetCurrentSession()
        {
            var currentSession = _sessionRepository.GetAll().LastOrDefault(x => x.NotActive == false);
            return currentSession.Id;
        }

        public string GetUserName(int userId)
        {
            var user = _userRepository.GetAll().SingleOrDefault(x => x.Id == userId);
            return $"{user.Firstname} {user.Lastname}";
        }

        public List<TicketModel> GetWinningTickets(int sessionId)
        {
            var allSessionTickets = _ticketRepository.GetAll();
            var sessionTickets = allSessionTickets
            .Where(x => x.SessionId == sessionId)
            .Select(x =>
            new TicketModel()
            {
                TicketCombination = x.TicketCombination,
                UserId = x.UserId,
                SessionId = x.SessionId
            }
            );
            var sessionNumbers = _sessionRepository.GetAll().SingleOrDefault(x => x.Id == sessionId).WinningCombination;
            return sessionTickets.Where(x => x.TicketCombination.Intersect(sessionNumbers).Count() > 2).ToList();
        }


        public Prize SetPrize(int count)
        {
            switch (count)
            {
                case 7:
                    return Prize.Car;
                case 6:
                    return Prize.Vacation;
                case 5:
                    return Prize.Tv;
                case 4:
                    return Prize._100_dollars_GiftCard;
                default:
                    return Prize._50_dollars_GiftCard;

            };
        }

        public List<WinnerModel> GetWinners(int sessionId)
        {
            return _winnersRepository.GetAll().Where(x => x.SessionId == sessionId)
                 .Select(x =>
                     new WinnerModel
                     {
                      
                         SessionId = x.SessionId,
                         Fullname = x.Fullname,
                         TicketCombination = x.TicketCombination
                     }).ToList();
        }


    }
}
