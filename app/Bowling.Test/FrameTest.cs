using System;
using Bowling.Domain.Models;
using NUnit.Framework;

namespace Bowling.Test
{
    [TestFixture]
    public class FrameTest
    {
        [Test]
        [TestCase(typeof(ArgumentException))]
        public void IsValidThrowing(Type ex)
        {
            var frame = new Frame();
            var t1 = new BallThrow(15);
            Assert.Throws(ex, delegate { frame.AddBallThrow(t1); });
        }

        [Test]
        public void TestSpare()
        {
            var frame = new Frame();
            var t1 = new BallThrow(5);
            var t2 = new BallThrow(5);
            frame.AddBallThrow(t1);
            frame.AddBallThrow(t2);
            Assert.True(frame.GetFrameType() == FrameType.Spare);
        }

        [Test]
        public void TestStrike()
        {
            var frame = new Frame();
            var t1 = new BallThrow(10);
            frame.AddBallThrow(t1);
            Assert.True(frame.GetFrameType() == FrameType.Strike);
        }

        [Test]
        public void TestOpenFrame()
        {
            var frame = new Frame();
            var t1 = new BallThrow(7);
            var t2 = new BallThrow(1);
            frame.AddBallThrow(t1);
            frame.AddBallThrow(t2);
            Assert.True(frame.GetFrameType() == FrameType.OpenFrame);
        }

        [Test]
        [TestCase(typeof(ThrowingNotAllowedException))]
        public void TestThrowingException(Type ex)
        {
            var frame = new Frame();
            var t1 = new BallThrow(7);
            var t2 = new BallThrow(1);
            var t3 = new BallThrow(1);
            frame.AddBallThrow(t1);
            frame.AddBallThrow(t2);
            Assert.Throws(ex, delegate { frame.AddBallThrow(t3); });
        }

        [Test]
        [TestCase(typeof(ArgumentException))]
        public void WrongInit(Type ex)
        {
            Assert.Throws(ex, delegate
            {
                var frame = new Frame(-5);
            });
        }

        [Test]
        public void ScoreWithOpenFrame()
        {
            var frame = new Frame();
            var t1 = new BallThrow(7);
            var t2 = new BallThrow(1);

            var frame1 = new Frame();
            frame.AddBallThrow(t1);
            frame.AddBallThrow(t2);

            frame1.AddBallThrow(t1);
            frame1.AddBallThrow(t2);

            Assert.AreEqual(8, Frame.CalculateScore(frame, frame1));
        }

        [Test]
        public void ScoreWithOpenFrameTail()
        {
            var frame = new Frame();
            var t1 = new BallThrow(7);
            var t2 = new BallThrow(1);

            frame.AddBallThrow(t1);
            frame.AddBallThrow(t2);

            Assert.AreEqual(8, Frame.CalculateScore(frame, null));
        }

        [Test]
        public void ScoreWithStrike()
        {
            var frame = new Frame();
            var t1 = new BallThrow(10);
            frame.AddBallThrow(t1);

            var frame1 = new Frame();
            var t2 = new BallThrow(1);
            var t3 = new BallThrow(3);

            frame1.AddBallThrow(t2);
            frame1.AddBallThrow(t3);

            Assert.AreEqual(14, Frame.CalculateScore(frame, frame1));
        }

        [Test]
        public void ScoreWithStrikeWithoutTail()
        {
            var frame = new Frame();
            var t1 = new BallThrow(10);
            frame.AddBallThrow(t1);

            Assert.AreEqual(10, Frame.CalculateScore(frame, null));
        }

        [Test]
        public void ScoreWithDoubleStrike()
        {
            var frame = new Frame();
            var t1 = new BallThrow(10);
            frame.AddBallThrow(t1);

            var frame1 = new Frame();
            var t2 = new BallThrow(10);

            frame1.AddBallThrow(t2);

            Assert.AreEqual(20, Frame.CalculateScore(frame, frame1));
        }

        [Test]
        public void SpareWithDoubleDoubleSpare()
        {
            var frame = new Frame();
            var t1 = new BallThrow(7);
            var t2 = new BallThrow(3);
            frame.AddBallThrow(t1);
            frame.AddBallThrow(t2);

            var frame1 = new Frame();
            var t3 = new BallThrow(6);
            var t4 = new BallThrow(4);

            frame1.AddBallThrow(t3);
            frame1.AddBallThrow(t4);

            Assert.AreEqual(16, Frame.CalculateScore(frame, frame1));
        }

        [Test]
        public void SpareWithDoubleOpenFrame()
        {
            var frame = new Frame();
            var t1 = new BallThrow(7);
            var t2 = new BallThrow(3);
            frame.AddBallThrow(t1);
            frame.AddBallThrow(t2);

            var frame1 = new Frame();
            var t3 = new BallThrow(4);
            var t4 = new BallThrow(1);

            frame1.AddBallThrow(t3);
            frame1.AddBallThrow(t4);

            Assert.AreEqual(14, Frame.CalculateScore(frame, frame1));
        }

        [Test]
        public void OpenFrame()
        {
            var frame = new Frame();
            var t1 = new BallThrow(7);
            var t2 = new BallThrow(0);
            frame.AddBallThrow(t1);
            frame.AddBallThrow(t2);

            Assert.AreEqual(7, Frame.CalculateScore(frame, null));
        }

        [Test]
        public void OpenFrameWithSecondFrame()
        {
            var frame = new Frame();
            var t1 = new BallThrow(7);
            var t2 = new BallThrow(0);
            frame.AddBallThrow(t1);
            frame.AddBallThrow(t2);

            var frame1 = new Frame();
            var t3 = new BallThrow(4);
            var t4 = new BallThrow(1);

            frame1.AddBallThrow(t3);
            frame1.AddBallThrow(t4);

            Assert.AreEqual(7, Frame.CalculateScore(frame, frame1));
        }
    }
}