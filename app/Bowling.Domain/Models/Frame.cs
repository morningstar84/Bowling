using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling.Domain.Models
{
    public class Frame
    {
        public const int DefaultPinNumber = 10;
        public const int MaxThrowNumber = 2;

        public int AdditionalThrow { get; set; }

        /// <param name="pinsNumber">Number of pins contained inside the frame</param>
        /// <param name="additionalThrow">if last frame, you'll have additional throw in the case of strike or spare</param>
        /// <param name="isLast">Specify if it's the last frame</param>
        /// <exception cref="ArgumentException">An exception is thwron when pins are less then zero</exception>
        public Frame(int pinsNumber = DefaultPinNumber, bool isLast = false)
        {
            if (pinsNumber <= 0) throw new ArgumentException("Pins can't be zero or negative");
            IsLastFrame = isLast;
            PinsNumber = pinsNumber;
        }

        public bool IsLastFrame { get; }

        public List<BallThrow> BallThrows { get; } = new();
        public int PinsNumber { get; }

        public int? Score { get; set; }

        public int KnockedDownPins => BallThrows.Select(x => x.Pins).Aggregate((x, y) => x + y);

        /// <exception cref="ThrowingNotAllowedException">the number of throw is limited</exception>
        public void AddBallThrow(BallThrow t)
        {
            if (!IsThrowingAllowed) throw new ThrowingNotAllowedException();
            IsValidThrowing(t);
            BallThrows.Add(t);
        }

        public void IsValidThrowing(BallThrow t)
        {
            if (t.Pins > PinsNumber || t.Pins < 0)
                throw new ArgumentException(
                    "A throw should have pin number greater than zero and less than PinsNumber");
        }

        public bool IsThrowingAllowed {
            get
            {
                if (IsLastFrame && BallThrows.Count < CalculateMaxThrowNumber() + CalculateAdditionalThrows())
                {
                    return true;
                }
                if (IsLastFrame && BallThrows.Count >= CalculateMaxThrowNumber() + CalculateAdditionalThrows())
                {
                    return false;
                }
                return GetFrameType() == null;
            }
        }

        private int CalculateMaxThrowNumber()
        {
            if (IsLastFrame && HasStrike())
            {
                return 1;
            }
            return MaxThrowNumber;
        }

        private int CalculateAdditionalThrows()
        {
            if (IsLastFrame && HasSpare())
            {
                return 1;
            }
            if (IsLastFrame && HasStrike())
            {
                return 2;
            }
            return 0;
        }

        public FrameType? GetFrameType()
        {
            if (HasOpenFrame()) return FrameType.OpenFrame;

            if (HasSpare()) return FrameType.Spare;

            if (HasStrike()) return FrameType.Strike;

            return null;
        }

        public bool HasStrike()
        {
            return BallThrows.Count(x => x.Pins == PinsNumber) > 0;
        }

        public bool HasSpare()
        {
            return BallThrows.Count == MaxThrowNumber + AdditionalThrow && KnockedDownPins == PinsNumber;
        }

        private bool HasOpenFrame()
        {
            return BallThrows.Count == MaxThrowNumber + AdditionalThrow && KnockedDownPins < PinsNumber;
        }

        public static int? CalculateScore(Frame f1, Frame? nextFrame, Frame? previousFrame=null)
        {
            var type = f1.GetFrameType();

            switch (type)
            {
                case FrameType.OpenFrame:
                    var score = f1.KnockedDownPins + (previousFrame?.Score ?? 0);
                    return score;

                case FrameType.Spare:
                    var a = nextFrame != null ? nextFrame.BallThrows[0].Pins : 0;
                    return f1.KnockedDownPins + a + (previousFrame?.Score ?? 0);

                case FrameType.Strike:
                    var next = nextFrame != null ? nextFrame.BallThrows[0].Pins + (nextFrame.BallThrows.Count > 1 ? nextFrame.BallThrows[1].Pins : 0)  : 0;
                    return f1.KnockedDownPins + next + (previousFrame?.Score ?? 0);

                default:
                    return f1.KnockedDownPins + (previousFrame?.Score ?? 0);;
            }
        }
    }
}