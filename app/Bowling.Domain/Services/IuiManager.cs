using System;
using System.Collections.Generic;
using System.Text;
using Bowling.Domain.Interfaces;
using Bowling.Domain.Models;
using Microsoft.Extensions.Configuration;
using NLog;

namespace Bowling.Domain.Services
{
    public class UiManager : IUiManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly int _pinInFrame;

        public UiManager(IConfiguration config)
        {
            _pinInFrame = config.GetSection("Bowling").GetValue("PinInFrame", Frame.DefaultPinNumber);
        }

        public void Welcome()
        {
            Logger.Info("Welcome to Bowling center");
        }

        public int RequestRoll()
        {

                var number = -1;
                Logger.Info("Insert pin shot (greater than zero):");
                while (number < 0)
                {
                    try
                    {
                        number = int.Parse(Console.ReadLine() ?? string.Empty);
                    }
                    catch (Exception _)
                    {
                        
                    }

                    if (number < 0)
                    {
                        Logger.Info("You inserted a non valid number! Try again!");
                        Logger.Info("Insert pin shot (greater than zero):");
                    }
                }

                return number;
        }

        public void PrintCurrentScore(int? currentScore, bool isGameFinished = false)
        {
            var builder = new StringBuilder();
            if (currentScore == null)
            {
                Logger.Info("[Score]: -");
                return;
            }

            builder.Append($"[Score]={currentScore}");
            if (isGameFinished)
                builder.Append("\nGame over! Thanks");
            Logger.Info(builder.ToString);
        }

        public void AlertWrongRoll()
        {
            Logger.Info("[Roll]: You entered a wrong value for roll");
        }

        public void PrintFramesResults(Dictionary<int, ResultDto> dto)
        {
            var builder = new StringBuilder();
            builder.Append("\n[Results]\n");
            builder.Append("**********\n");
            foreach (var el in dto)
            {
                builder.Append('[');
                if (el.Value.FrameType == FrameType.Strike)
                {
                    builder.Append('X');
                } else if (el.Value.FrameType == FrameType.Spare)
                {
                    builder.Append(el.Value.StruckDownPins1);
                    builder.Append(" /");
                }
                else
                {
                    builder.Append(el.Value.StruckDownPins1);
                    builder.Append(' ');
                    builder.Append(el.Value.StruckDownPins2);
                }
                
                var score = el.Value.Score != null ? $" | {el.Value.Score} " : " * ";
                builder.Append(score);
                builder.Append("] ");
                Logger.Info(builder.ToString());
            }
        }
    }
}