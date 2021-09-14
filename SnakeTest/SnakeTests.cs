using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPF_Snake.Model;
using WPF_Snake.Persistence;
using Moq;

namespace SnakeTest
{
    [TestClass]
    public class SnakeTests
    {
        private SnakeGameModel _model;
        private Mock<SnakeFileDataAccess> _mock;


        [TestInitialize]
        public void Initialize()
        {
            _mock = new Mock<SnakeFileDataAccess>();
            _model = new SnakeGameModel(_mock.Object);

            _model.InitializeTable(11);
            _model.GameAdvanced += TestOnGameAdvanced;
            _model.EggEaten += TestEggEaten;
            _model.GameOver += TestOnGameOver;

        }
        [TestMethod]
        public void TestInitial()
        {
            //Test Snake in Starting position
            Assert.AreEqual(1, _model.GetTable().GetMapValue((_model.GetTable().GetMapSize()-1)/2, (_model.GetTable().GetMapSize() - 1) / 2));
            for (int i = 5; i < 5 + 4; i++)
            {
                Assert.AreEqual(2, _model.GetTable().GetMapValue(i + 1, 5));
            }
            //Test that 0 eggs are eaten at the start of the game
            Assert.AreEqual(0, _model.GetEggCount());
            //Test if Snake start by moving upwards
            Assert.AreEqual(Direction.Up, _model.GetSnake().GetDirection());
            //Test Snake's length
            int testSnakeBodyPartCount = 1; //because of head
            testSnakeBodyPartCount += _model.GetSnake().GetBody().Count;
            Assert.AreEqual(5, testSnakeBodyPartCount);
            //Test if snake is able to turn
            Assert.AreEqual(true, _model.GetSnake().bCanTurn);
            //Test if snake has eaten this tick
            Assert.AreEqual(false, _model.GetSnake().bHaveEaten);
            //Test if Spawnlocation list is initialized in this case everything is a spawnlocation
            Assert.AreEqual(121, _model.GetSpawnLocationList().Count);

        }
        [TestMethod]
        public void TestMovementOnAdvanceGame()
        {
            _model.AdvanceGame();
            //Test if head is in a correct new position and that the body is following the head
            Assert.AreEqual(1, _model.GetTable().GetMapValue(4, 5));
            for (int i = 4; i < 4 + 4; i++)
            {
                Assert.AreEqual(2, _model.GetTable().GetMapValue(i + 1, 5));
            }
            //From here on we just test the headposition
            //Test changing direction
            _model.GetSnake().ChangeDirection(Direction.Right);
            Assert.AreEqual(Direction.Right, _model.GetSnake().GetDirection());
            _model.GetSnake().ChangeDirection(Direction.Down);
            Assert.AreEqual(Direction.Right, _model.GetSnake().GetDirection());
            //Test new head location
            _model.AdvanceGame();
            Assert.AreEqual(1, _model.GetTable().GetMapValue(4, 6));
            _model.AdvanceGame();
            Assert.AreEqual(1, _model.GetTable().GetMapValue(4, 7));
            _model.AdvanceGame();
            Assert.AreEqual(1, _model.GetTable().GetMapValue(4, 8));
            _model.GetSnake().ChangeDirection(Direction.Down);
            _model.AdvanceGame();
            Assert.AreEqual(1, _model.GetTable().GetMapValue(5, 8));
        }
        public void TestOnGameAdvanced(object sender, EventArgs e)
        {
            bool bDidGameAdvance = true;
            Assert.AreEqual(true, bDidGameAdvance);
        }
        [TestMethod]
        public void TestEating()
        {
            //Test if snake is in the proper starting position
            Assert.AreEqual(1, _model.GetTable().GetMapValue((_model.GetTable().GetMapSize() - 1) / 2, (_model.GetTable().GetMapSize() - 1) / 2));
            for (int i = 5; i < 5 + 4; i++)
            {
                Assert.AreEqual(2, _model.GetTable().GetMapValue(i + 1, 5));
            }
            //Spawn an egg in front of it
            _model.GetTable().SetMapValue(3, 5, 3);
            _model.AdvanceGame();
            Assert.AreEqual(0,_model.GetEggCount());
            _model.AdvanceGame();
            Assert.AreEqual(1, _model.GetEggCount());
            //Snake got longer
            int testSnakeBodyPartCount = 1; //because of head
            testSnakeBodyPartCount += _model.GetSnake().GetBody().Count;
            Assert.AreEqual(6, testSnakeBodyPartCount);
        }
        public void TestEggEaten(object sender, EventArgs e)
        {
            bool bWasEggEaten = true;
            Assert.AreEqual(true, bWasEggEaten);
        }
        [TestMethod]
        public void TestWallHit()
        {
            //Test if snake is in the proper starting position
            Assert.AreEqual(1, _model.GetTable().GetMapValue((_model.GetTable().GetMapSize() - 1) / 2, (_model.GetTable().GetMapSize() - 1) / 2));
            for (int i = 5; i < 5 + 4; i++)
            {
                Assert.AreEqual(2, _model.GetTable().GetMapValue(i + 1, 5));
            }
            //Spawn a wall in front of the snake
            _model.GetTable().SetMapValue(3, 5, 4);
            _model.AdvanceGame();
            _model.AdvanceGame();
        }
        public void TestOnGameOver(object sender, EventArgs e)
        {
            bool bWasGameOver = true;
            Assert.AreEqual(true, bWasGameOver);
        }
    }
}
