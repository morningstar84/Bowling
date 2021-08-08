using System;
using NLog;

namespace Bowling.Domain.Models
{
    public class BallThrow
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public BallThrow(int pins)
        {
            Pins = pins;
        }

        public int Pins { get; }

        public static BallThrow GetRandomBallThrow()
        {
            var r = new Random();
            var integer = r.Next(0, 10);
            Logger.Debug($"[RANDOM]: {integer}");
            return new BallThrow(integer);
        }
    }
}