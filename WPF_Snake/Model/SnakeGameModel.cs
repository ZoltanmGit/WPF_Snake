using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WPF_Snake.Persistence;

namespace WPF_Snake.Model
{
    public class SnakeGameModel
    {
        #region Properties
        private SnakeTable _table;
        private Snake _snake;
        private ISnakeDataAccess _dataAccess;
        private List<Coordinate> _spawnLocationList;
        #endregion

        #region Constructor
        public SnakeGameModel(SnakeFileDataAccess argDataAccess) { _dataAccess = argDataAccess; }
        #endregion

        #region Private Methods
        private void RepresentSnake()
        {
            _table.SetMapValue(_snake.GetHead().x, _snake.GetHead().y, 1);
            for (int i = 0; i < _snake.GetBody().Count; i++)
            {
                _table.SetMapValue(_snake.GetBody()[i].x, _snake.GetBody()[i].y, 2);
            }
        }
        private void ExecuteMovement()
        {
            Coordinate newHeadLocation = new Coordinate();
            switch (_snake.GetDirection())
            {
                case Direction.Left:
                    newHeadLocation.x = _snake.GetHead().x;
                    newHeadLocation.y = _snake.GetHead().y - 1;
                    break;
                case Direction.Up:
                    newHeadLocation.x = _snake.GetHead().x - 1;
                    newHeadLocation.y = _snake.GetHead().y;
                    break;
                case Direction.Right:
                    newHeadLocation.x = _snake.GetHead().x;
                    newHeadLocation.y = _snake.GetHead().y + 1;
                    break;
                case Direction.Down:
                    newHeadLocation.x = _snake.GetHead().x + 1;
                    newHeadLocation.y = _snake.GetHead().y;
                    break;
                default:
                    break;
            }
            if ((newHeadLocation.x < 0 || newHeadLocation.x == _table.GetMapSize()) || (newHeadLocation.y < 0 || newHeadLocation.y == _table.GetMapSize()) || (_table.GetMapValue(newHeadLocation.x, newHeadLocation.y) == 4) || (_table.GetMapValue(newHeadLocation.x, newHeadLocation.y) == 2))
            {
                GameOver(this, EventArgs.Empty);
            }
            else
            {
                if (_table.GetMapValue(newHeadLocation.x, newHeadLocation.y) == 3)
                {
                    _snake.EggConsumed();
                    EggEaten(this, EventArgs.Empty);
                }
                _snake.MoveBody();
                _snake.MoveHead(newHeadLocation);
                if (!_snake.bHaveEaten)
                {
                    _table.SetMapValue(_snake.GetTailCollection().x, _snake.GetTailCollection().y, 0);
                }
                else
                {
                    SpawnEgg();
                }
                _snake.bHaveEaten = false;
            }
        }
        private void InitializeSpawnLocations()
        {
            _spawnLocationList = new List<Coordinate>();
            for (int i = 0; i < _table.GetMapSize(); i++)
            {
                for (int j = 0; j < _table.GetMapSize(); j++)
                {
                    if (_table.GetMapValue(i, j) != 4)
                    {
                        Coordinate tempCoordinate = new Coordinate();
                        tempCoordinate.x = i;
                        tempCoordinate.y = j;
                        _spawnLocationList.Add(tempCoordinate);
                    }
                }
            }
        }
        private void SpawnEgg()
        {
            if (_spawnLocationList.Count != 0)
            {
                Random rnd = new Random();
                bool bIsValid = false;
                do
                {
                    Int32 randomIndex = rnd.Next(0, _spawnLocationList.Count);
                    if (_table.GetMapValue(_spawnLocationList[randomIndex].x, _spawnLocationList[randomIndex].y) == 0)
                    {
                        bIsValid = true;
                        _table.SetMapValue(_spawnLocationList[randomIndex].x, _spawnLocationList[randomIndex].y, 3);
                    }
                } while (!bIsValid);
            }
        }
        #endregion

        #region Public Methods
        public void InitializeTable(Int32 argMapSize)
        {
            _table = new SnakeTable(argMapSize);
            for (int i = 0; i < _table.GetMapSize(); i++)
            {
                for (int j = 0; j < argMapSize; j++)
                {
                    GetTable().SetMapValue(i, j, 0);
                }
            }
            _snake = new Snake((_table.GetMapSize() - 1) / 2, (_table.GetMapSize() - 1) / 2);
            RepresentSnake();
            InitializeSpawnLocations();
            SpawnEgg();
        }
        public void AdvanceGame()
        {
            _snake.bCanTurn = true;
            ExecuteMovement();//Mozgatja a kígyót
            RepresentSnake();//_table-en megjeleníti a kígyót
            GameAdvanced(this,EventArgs.Empty);//Eseményt küld a viewModel-nek, hogy frissítsen
        }
        public async Task LoadGameAsync(String path)
        {
            if (_dataAccess == null)
            {
                throw new InvalidOperationException("No data access is provided.");
            }

            _table = await _dataAccess.LoadAsync(path);
            _snake = new Snake((_table.GetMapSize() - 1) / 2, (_table.GetMapSize() - 1) / 2);
            RepresentSnake();
            InitializeSpawnLocations();
            SpawnEgg();
        }
        #endregion

        #region Events
        public event EventHandler<EventArgs> GameOver;
        public event EventHandler<EventArgs> EggEaten;
        public event EventHandler<EventArgs> GameAdvanced;
        #endregion

        #region Gets
        public SnakeTable GetTable()
        {
            if(_table != null)
            {
                return _table;
            }
            return null;
        }
        public Int32 GetEggCount()
        {
            if (_snake != null)
            {
                return _snake.GetEggCount();
            }
            return 0;
        }
        public Snake GetSnake()
        {
            return _snake;
        }
        public List<Coordinate> GetSpawnLocationList()
        {
            return _spawnLocationList;
        }
        #endregion

        #region Sets
        #endregion
    }
}