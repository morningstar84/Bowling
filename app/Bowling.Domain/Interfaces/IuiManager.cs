using System.Collections.Generic;
using Bowling.Domain.Models;

namespace Bowling.Domain.Interfaces
{
    public interface IUiManager
    {
        public void Welcome();
        public int RequestRoll();
        public void PrintCurrentScore(int? currentScore, bool isGameFinished = false);

        public void AlertWrongRoll();

        public void PrintFramesResults(Dictionary<int, ResultDto> dto);
    }
}