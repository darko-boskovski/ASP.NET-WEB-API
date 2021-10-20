using Lotto_3000_App.Models.Lotto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Services.Interfaces
{
    public interface ITicketService
    {
        void CreateTicket(TicketModel model);
        List<TicketModel> GetAll();
    }
}
