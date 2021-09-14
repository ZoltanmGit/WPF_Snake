using System;
using System.Threading.Tasks;

namespace WPF_Snake.Persistence
{
    public interface ISnakeDataAccess
    {
        /// <summary>
        /// Load file
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>A table loaded form the file</returns>
        Task<SnakeTable> LoadAsync(String path);
    }
}
