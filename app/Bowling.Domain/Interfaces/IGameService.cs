using System.Collections.Generic;
using Bowling.Domain.Models;

namespace Bowling.Domain.Interfaces
{
    public interface IGameService
    {
        void StartNewGame();

        public bool Roll(int pins);

        bool IsGameFinished();

        int? GetCurrentScore();

        Dictionary<int, ResultDto>  GetResults();
    }
}