using Lotto_3000_App.DataAccess.Interfaces;
using Lotto_3000_App.Domain;
using Lotto_3000_App.Domain.Models;
using Lotto_3000_App.Models.Lotto;
using Lotto_3000_App.Services.Interfaces;
using Lotto_3000_App.Shared.Enums;
using Lotto_3000_App.Shared.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto_3000_App.Services.Implementations
{
    public class WinnersService : IWinnersService
    {
       

        private readonly IRepository<Winner> _winnersRepository;

        public WinnersService(IRepository<Winner> winnersRepository) 
        { 

            _winnersRepository = winnersRepository;
        }
        public List<WinnerModel> GetWinners(int sessionId)
        {
           var winnerList = _winnersRepository.GetAll().Where(x => x.SessionId == sessionId)
                 .Select(x =>
                     new WinnerModel
                     {
                         SessionId = x.SessionId,
                         SessionCombination = x.Session.WinningCombination,
                         Fullname = x.Fullname,
                         TicketCombination = x.TicketCombination,
                         WiningPrize = WinnerModel.SetPrize(x.Prize)
                     

                     }).ToList();
            if (winnerList == null || winnerList.Count == 0) throw new NotFoundException($"There are no Winners in this session Keep Playing...");
            
            return winnerList;
        }

        public List<WinnerModel> GetAllWinners()
        {
            var allSessions = _winnersRepository.GetAll()
                 .Select(x =>
                     new WinnerModel
                     {
                         SessionId = x.SessionId,
                         SessionCombination = x.Session.WinningCombination,
                         Fullname = x.Fullname,
                         TicketCombination = x.TicketCombination,
                         WiningPrize = WinnerModel.SetPrize(x.Prize)
                                       
                     }).ToList();

            if (allSessions == null || allSessions.Count == 0)
            {
                throw new NotFoundException($"There are no Winners so far Keep Playing...");
            }
            return allSessions;
        }

    }
}
