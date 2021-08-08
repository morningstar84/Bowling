using System.Linq;
using Bowling.Domain.Models;
using NUnit.Framework;

namespace Bowling.Test
{
    [TestFixture]
    public class GameTest
    {
        [Test]
        public void GameFinishedWithTwoStrikes()
        {
            var g = new Game(1);
            g.Roll(10, 10);
            g.Roll(10, 10);
            g.Roll(10, 10);
            Assert.True(g.IsGameOver);
        }
        
        [Test]
        public void GameFinishedWithSpareAndStrikes()
        {
            var g = new Game(2);
            g.Roll(6, 10);
            g.Roll(4, 10);
            g.Roll(10, 10);
            g.Roll(4, 10);
            g.Roll(4, 10);
            Assert.True(g.IsGameOver);
        }
        
        [Test]
        public void GameFinishedWithTwoSpares()
        {
            var g = new Game(2);
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            g.Roll(5, 10);
            g.Roll(5, 10);
            
            g.Roll(5, 10);
            Assert.True(g.IsGameOver);
        }
        
        [Test]
        public void CalculateScoreWithTwoSpares()
        {
            var g = new Game(1);
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            g.Roll(5, 10);

            Assert.AreEqual(15, g.GetCurrentScore());
        }
        
        [Test]
        public void CalculateScoreWithStrike()
        {
            var g = new Game(1);
            g.Roll(10, 10);

            g.Roll(5, 10);
            g.Roll(5, 10);

            Assert.AreEqual(20, g.GetCurrentScore());
        }
        
        [Test]
        public void GameNotFinished()
        {
            var g = new Game(2);
            g.Roll(6, 10);
            g.Roll(4, 10);
            g.Roll(5, 10);
            Assert.True(!g.IsGameOver);
        }
        
        [Test]
        public void ScoreCalculationAtFrame()
        {
            var g = new Game(2);
            g.Roll(1, 10);
            g.Roll(4, 10);
            var score = g.GetCurrentScore();
            Assert.AreEqual(5, score);
        }
        
        [Test]
        public void ScoreCalculationAtFrame1()
        {
            var g = new Game(2);
            g.Roll(1, 10);
            g.Roll(4, 10);
            g.Roll(4, 10);
            g.Roll(5, 10);
        
            Assert.AreEqual(14, g.GetCurrentScore());
        }
        
        [Test]
        public void ScoreCalculationAtFrame2()
        {
            var g = new Game();
            // 1
            g.Roll(1, 10);
            g.Roll(4, 10);
            
            // 2
            g.Roll(4, 10);
            g.Roll(5, 10);
            
            // 3
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 4
            g.Roll(5, 10);
            Assert.AreEqual(29, g.GetCurrentScore());
        }
        
        [Test]
        public void ScoreCalculationAtFrame3()
        {
            var g = new Game();
            // 1
            g.Roll(1, 10);
            g.Roll(4, 10);
            
            // 2
            g.Roll(4, 10);
            g.Roll(5, 10);
            
            // 3
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 4
            g.Roll(5, 10);
            g.Roll(5, 10);
            
            // 5
            g.Roll(10, 10);
        
            Assert.AreEqual(49, g.GetCurrentScore());
        }
        
        [Test]
        public void ScoreCalculationAtFrame4()
        {
            var g = new Game();
            // 1
            g.Roll(1, 10);
            g.Roll(4, 10);
            
            // 2
            g.Roll(4, 10);
            g.Roll(5, 10);
            
            // 3
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 4
            g.Roll(5, 10);
            g.Roll(5, 10);
            
            // 5
            g.Roll(10, 10);
            
            // 6
            g.Roll(0, 10);
            g.Roll(1, 10);
        
            Assert.AreEqual(60, g.GetCurrentScore());
        }
        
        [Test]
        public void ScoreCalculationAtFrame5()
        {
            var g = new Game();
            // 1
            g.Roll(1, 10);
            g.Roll(4, 10);
            
            // 2
            g.Roll(4, 10);
            g.Roll(5, 10);
            
            // 3
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 4
            g.Roll(5, 10);
            g.Roll(5, 10);
            
            // 5
            g.Roll(10, 10);
            
            // 6
            g.Roll(0, 10);
            g.Roll(1, 10);
            
            //7
            g.Roll(7, 10);
            g.Roll(3, 10);
            
            Assert.AreEqual(61, g.GetCurrentScore());
        }
        
        [Test]
        public void ScoreCalculationAtFrame6()
        {
            var g = new Game();
            // 1
            g.Roll(1, 10);
            g.Roll(4, 10);
            
            // 2
            g.Roll(4, 10);
            g.Roll(5, 10);
            
            // 3
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 4
            g.Roll(5, 10);
            g.Roll(5, 10);
            
            // 5
            g.Roll(10, 10);
            
            // 6
            g.Roll(0, 10);
            g.Roll(1, 10);
            
            // 7
            g.Roll(7, 10);
            g.Roll(3, 10);
            
            // 8 
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            Assert.AreEqual(77, g.GetCurrentScore());
        }
        
        [Test]
        public void ScoreCalculationAtFrame7()
        {
            var g = new Game();
            // 1
            g.Roll(1, 10);
            g.Roll(4, 10);
            
            // 2
            g.Roll(4, 10);
            g.Roll(5, 10);
            
            // 3
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 4
            g.Roll(5, 10);
            g.Roll(5, 10);
            
            // 5
            g.Roll(10, 10);
            
            // 6
            g.Roll(0, 10);
            g.Roll(1, 10);
            
            // 7
            g.Roll(7, 10);
            g.Roll(3, 10);
            
            // 8 
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 9
            g.Roll(10, 10);

            Assert.AreEqual(97, g.GetCurrentScore());
        }
        
        [Test]
         public void ScoreCalculationAtFrame8()
         {
             var g = new Game();
             // 1
             g.Roll(1, 10);
             g.Roll(4, 10);
             
             // 2
             g.Roll(4, 10);
             g.Roll(5, 10);
             
             // 3
             g.Roll(6, 10);
             g.Roll(4, 10);
             
             // 4
             g.Roll(5, 10);
             g.Roll(5, 10);
             
             // 5
             g.Roll(10, 10);
             
             // 6
             g.Roll(0, 10);
             g.Roll(1, 10);
             
             // 7
             g.Roll(7, 10);
             g.Roll(3, 10);
             
             // 8 
             g.Roll(6, 10);
             g.Roll(4, 10);
             
             // 9
             g.Roll(10, 10);
             
             // 10
             g.Roll(2, 10);
             g.Roll(8, 10);

             Assert.AreEqual(117, g.GetCurrentScore());
         }
         
         [Test]
         public void ScoreCalculationAtFrame9()
         {
             var g = new Game();
             // 1
             g.Roll(1, 10);
             g.Roll(4, 10);
             
             // 2
             g.Roll(4, 10);
             g.Roll(5, 10);
             
             // 3
             g.Roll(6, 10);
             g.Roll(4, 10);
             
             // 4
             g.Roll(5, 10);
             g.Roll(5, 10);
             
             // 5
             g.Roll(10, 10);
             
             // 6
             g.Roll(0, 10);
             g.Roll(1, 10);
             
             // 7
             g.Roll(7, 10);
             g.Roll(3, 10);
             
             // 8 
             g.Roll(6, 10);
             g.Roll(4, 10);
             
             // 9
             g.Roll(10, 10);
             
             // 10
             g.Roll(2, 10);
             g.Roll(8, 10);
             g.Roll(6, 10);
        
             Assert.AreEqual(133, g.GetCurrentScore());
         }
        
        [Test]
        public void FrameCountWithSpareAdditionalThrows()
        {
            var g = new Game();
            // 1
            g.Roll(1, 10);
            g.Roll(4, 10);
            
            // 2
            g.Roll(4, 10);
            g.Roll(5, 10);
            
            // 3
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 4
            g.Roll(5, 10);
            g.Roll(5, 10);
            
            // 5
            g.Roll(10, 10);
            
            // 6
            g.Roll(0, 10);
            g.Roll(1, 10);
            
            // 7
            g.Roll(7, 10);
            g.Roll(3, 10);
            
            // 8 
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 9
            g.Roll(10, 10);
            
            // 10
            g.Roll(2, 10);
            g.Roll(8, 10);
            //g.Roll(6, 10);
        
            Assert.AreEqual(10, g.CurrentFrameNumber);
        }
        
        
        [Test]
        public void FrameCountWithStrikeAdditionalThrows()
        {
            var g = new Game();
            // 1
            g.Roll(1, 10);
            g.Roll(4, 10);
            
            // 2
            g.Roll(4, 10);
            g.Roll(5, 10);
            
            // 3
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 4
            g.Roll(5, 10);
            g.Roll(5, 10);
            
            // 5
            g.Roll(10, 10);
            
            // 6
            g.Roll(0, 10);
            g.Roll(1, 10);
            
            // 7
            g.Roll(7, 10);
            g.Roll(3, 10);
            
            // 8 
            g.Roll(6, 10);
            g.Roll(4, 10);
            
            // 9
            g.Roll(10, 10);
            
            // 10
            g.Roll(2, 10);
            g.Roll(8, 10);
            g.Roll(6, 10);
        
            Assert.AreEqual(10, g.CurrentFrameNumber);
        }
    
    }
}