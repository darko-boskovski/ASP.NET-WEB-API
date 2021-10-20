using Lotto_3000_App.Models.Lotto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto_3000_App.Services.Interfaces
{
    public interface IWinnersService
    {
        List<WinnerModel> GetWinners(int sessionId);
        List<WinnerModel> GetAllWinners();
    }
}
