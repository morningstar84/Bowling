using System.Collections.Generic;
using Bowling.Domain.Interfaces;
using Bowling.Domain.Models;
using Microsoft.Extensions.Configuration;
using NLog;

namespace Bowling.Domain.Services
{
    public class GameService : IGameService
    {
        
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly int _framesNumber;
        private readonly int _pinInFrame;
        
        private Game _game;

        public GameService(IConfiguration config)
        {
            _framesNumber = config.GetSection("Bowling").GetValue("FrameNumber", Game.DefaultFrameNumber);
            _pinInFrame = config.GetSection("Bowling").GetValue("PinInFrame", Frame.DefaultPinNumber);
            _game = new Game(_framesNumber);
        }

        public bool Roll(int pins)
        {
            return _game.Roll(pins, _pinInFrame);
        }

        public void StartNewGame()
        {
            Logger.Info("New game started");
            _game = new Game(_framesNumber);
        }

        public bool IsGameFinished()
        {
            return _game.IsGameOver;
        }

        public int? GetCurrentScore()
        {
            return _game.GetCurrentScore();
        }

        public Dictionary<int, ResultDto> GetResults()
        {
            return _game.GetResults();
        }
    }
}