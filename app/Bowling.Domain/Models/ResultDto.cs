using System.Collections.Generic;

namespace Bowling.Domain.Models
{
    public class ResultDto
    {
        
        public int? StruckDownPins1 { get; set; }

        public int? StruckDownPins2 { get; set; }

        public int? Score { get; set; }

        public FrameType? FrameType { get; set; }
        
    }
}