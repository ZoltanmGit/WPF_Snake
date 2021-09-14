using System;
using System.Collections.Generic;

namespace WPF_Snake.Model
{
    public enum Direction
    {
        Left, Up, Right, Down
    }
    public class Coordinate
    {
        public Int32 x;
        public Int32 y;
    }
    public class Snake
    {
        #region Private Variables
        private Direction _direction;
        private Coordinate _head;
        private Coordinate _tailCollection;
        private List<Coordinate> _body;
        private Int32 _eggCount;
        #endregion

        #region Public Variables
        public bool bCanTurn;
        public bool bHaveEaten;
        #endregion

        #region Constructor
        public Snake(Int32 headXCoordinate, Int32 headYCoordinate)
        {
            _eggCount = 0;
            bCanTurn = true;
            bHaveEaten = false;
            _tailCollection = new Coordinate();
            _head = new Coordinate();
            _head.x = headXCoordinate;
            _head.y = headYCoordinate;
            _body = new List<Coordinate>();
            for (int i = 0; i < 4; i++)
            {
                Coordinate temp = new Coordinate();
                if (i == 0)
                {
                    temp.x = _head.x + 1;
                    temp.y = _head.y;
                }
                else
                {
                    temp.x = _body[i - 1].x + 1;
                    temp.y = _body[i - 1].y;
                }
                _body.Add(temp);
                _direction = Direction.Up;
            }
        }
        #endregion

        #region Public Methods
        public void ChangeDirection(Direction newDirection)
        {
            if (bCanTurn)
            {
                if ((_direction == Direction.Left || _direction == Direction.Right) && (newDirection == Direction.Up || newDirection == Direction.Down))
                {
                    bCanTurn = false;
                    _direction = newDirection;
                }
                else if ((_direction == Direction.Up || _direction == Direction.Down) && (newDirection == Direction.Right || newDirection == Direction.Left))
                {
                    bCanTurn = false;
                    _direction = newDirection;
                }
            }
        }
        public void MoveBody()
        {
            _tailCollection.x = _body[_body.Count - 1].x;
            _tailCollection.y = _body[_body.Count - 1].y;
            for (int i = _body.Count - 1; i > 0; i--)
            {
                _body[i].x = _body[i - 1].x;
                _body[i].y = _body[i - 1].y;
            }
            _body[0] = _head;
        }
        public void MoveHead(Coordinate newHeadCoordinate)
        {
            _head = newHeadCoordinate;
        }
        public void EggConsumed()
        {
            bHaveEaten = true;
            _eggCount += 1;
            Coordinate newBodyPart = new Coordinate();
            newBodyPart.x = _tailCollection.x;
            newBodyPart.y = _tailCollection.y;
            _body.Add(newBodyPart);
        }
        public Int32 GetEggCount()
        {
            return _eggCount;
        }
        public Direction GetDirection()
        {
            return _direction;
        }
        public Coordinate GetHead()
        {
            return _head;
        }
        public List<Coordinate> GetBody()
        {
            return _body;
        }
        public Coordinate GetTailCollection()
        {
            return _tailCollection;
        }
        #endregion
    }
}
