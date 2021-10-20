using Lotto_3000_App.Models.Lotto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Services.Interfaces
{
    public interface IDrawService
    {
        void CreateSession(SessionModel model);
        int GetCurrentSession();
        void CloseSession(int currentSessionId);
        void AddWinnersByThisSession(int sessionId);
        string GetUserName(int userId);
        List<TicketModel> GetWinningTickets(int sessionId);
    }
}
