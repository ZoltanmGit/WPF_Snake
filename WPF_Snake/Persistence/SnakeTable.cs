using System;

namespace WPF_Snake.Persistence
{
    public class SnakeTable
    {
        #region Variables
        /// <summary>
        /// Size of the playing field
        /// </summary>
        private Int32 _mapSize;
        /// <summary>
        /// 0-Empty |
        /// 1-SnakeHead |
        /// 2-SnakeBody |
        /// 3-Food |
        /// 4-Wall
        /// </summary>
        private Int32[,] _mapValues;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor, intializes the table according to the parameter
        /// </summary>
        /// <param name="argMapSize"></param>
        public SnakeTable(Int32 argMapSize)
        {
            _mapSize = argMapSize;
            _mapValues = new Int32[_mapSize, _mapSize];
        }
        #endregion

        #region GetFunctions
        /// <summary>
        /// Reutnrs the row and column number of the map since the map is n x n
        /// </summary>
        /// <returns>MapSize stored as Int32</returns>
        public Int32 GetMapSize()
        {
            return _mapSize;
        }
        /// <summary>
        /// Returns the numerical value of a koordinate
        /// </summary>
        /// <param name="x">Row</param>
        /// <param name="y">Column</param>
        /// <returns>The value at x and y in Int32</returns>
        public Int32 GetMapValue(Int32 x, Int32 y)
        {
            if (x < 0 || x > GetMapSize())
            {
                throw new ArgumentOutOfRangeException("x", "x coordinate is out of range");
            }
            if (y < 0 || y > GetMapSize())
            {
                throw new ArgumentOutOfRangeException("y", "y coordinate is out of range");
            }
            return _mapValues[x, y];
        }
        #endregion

        #region SetFunctions
        /// <summary>
        /// Sets the value at x and y coordinates to the value given as a parameter
        /// </summary>
        /// <param name="x">Rows</param>
        /// <param name="y">Columns</param>
        /// <param name="argValue">The new value</param>
        public void SetMapValue(Int32 x, Int32 y, Int32 argValue)
        {
            if (x < 0 || x > GetMapSize())
            {
                throw new ArgumentOutOfRangeException("x", "x coordinate is out of range");
            }
            if (y < 0 || y > GetMapSize())
            {
                throw new ArgumentOutOfRangeException("y", "y coordinate is out of range");
            }
            _mapValues[x, y] = argValue;
        }
        #endregion
    }
}
