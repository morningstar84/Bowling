using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace Bowling.Domain.Models
{
    public class Game
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public const int DefaultFrameNumber = 10;
        private readonly LinkedList<Frame> _frameList = new();

        public int CurrentFrameNumber => _frameList.Count;

        public Game(int frameNumber = DefaultFrameNumber)
        {
            FrameNumber = frameNumber;
        }

        public int FrameNumber { get; }

        public bool IsGameOver => _frameList.Count == FrameNumber && !(_frameList.Last?.Value.IsThrowingAllowed ?? true );


        public bool Roll(int pins, int pinInFrame)
        {
            try
            {
                if (IsGameOver)
                {
                    Logger.Error("Game is over -> you can't throw no more");
                    return false;
                }
                var lastFrame = _frameList.Last?.Value!;

                if (lastFrame is not { IsThrowingAllowed: true })
                {
                    lastFrame = AddNewFrame(pinInFrame);
                }

                lastFrame.BallThrows.Add(new BallThrow(pins));
                UpdateScores();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Could not roll with number {pins}");
            }
            return false;
        }
        

        private Frame AddNewFrame(int pinInFrame)
        {
            var isLast = _frameList.Count + 1 == FrameNumber;
            var f = new Frame(pinInFrame, isLast);
            _frameList.AddLast(f);
            return f;
        }

        private void UpdateScores()
        {
            if (_frameList.Count == 0)
            {
                return;
            }
            var currentNode = _frameList.First;
            while (currentNode != null)
            {
                if (!currentNode.Value.IsLastFrame && currentNode.Value.GetFrameType() == null)
                {
                    currentNode = currentNode.Next;
                    continue;
                }
                currentNode.Value.Score = Frame.CalculateScore(currentNode.Value, currentNode.Next?.Value,
                    currentNode.Previous?.Value) ?? 0;
                currentNode = currentNode.Next;
            }
        }

        public Dictionary<int, ResultDto> GetResults()
        {
            var resultsList = new Dictionary<int, ResultDto>();
            if (_frameList.Count == 0)
            {
                return resultsList;
            }
            var index = 0;
            var currentNode = _frameList.First;
            while (currentNode != null)
            {
                currentNode.Value.Score = Frame.CalculateScore(currentNode.Value, currentNode.Next?.Value,
                    currentNode.Previous?.Value) ?? 0;
                resultsList.Add(index, new ResultDto
                {
                    StruckDownPins1 = currentNode.Value.BallThrows[0].Pins,
                    StruckDownPins2 = currentNode.Value.BallThrows.Count == Frame.MaxThrowNumber ? currentNode.Value.BallThrows[1].Pins : null,
                    FrameType = currentNode.Value.GetFrameType(),
                    Score = currentNode.Next == null && (currentNode.Value.HasStrike() || currentNode.Value.HasSpare()) ? null : currentNode.Value.Score
                });
                currentNode = currentNode.Next;
                index++;
            }
            return resultsList;
        }

        public int? GetCurrentScore()
        {
            return GetFrameWithComputableStore()?.Score;
        }

        private Frame? GetFrameWithComputableStore()
        {
            if (_frameList.Last.Value.IsLastFrame && _frameList.Last.Value.IsThrowingAllowed)
            {
                return _frameList.Last?.Previous?.Value;
            }

            if ((_frameList.Last != null && _frameList.Last.Value.Score != null && IsGameOver)  || _frameList.Count == 1)
            {
                return _frameList.Last?.Value;
            }
            return _frameList.Last?.Previous?.Value;
        }
        
    }
}