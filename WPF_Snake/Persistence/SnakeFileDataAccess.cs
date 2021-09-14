using System;
using System.IO;
using System.Threading.Tasks;

namespace WPF_Snake.Persistence
{
    public class SnakeFileDataAccess : ISnakeDataAccess
    {
        public async Task<SnakeTable> LoadAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                {
                    String line = await reader.ReadLineAsync();
                    String[] numbers = line.Split(' '); // beolvasunk egy sort, és a szóköz mentén széttöredezzük
                    Int32 mapSize = Int32.Parse(numbers[0]); // beolvassuk a tábla méretét
                    SnakeTable loadedTable = new SnakeTable(mapSize); // létrehozzuk a táblát
                    for (Int32 i = 0; i < mapSize; i++)
                    {
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');

                        for (Int32 j = 0; j < mapSize; j++)
                        {
                            loadedTable.SetMapValue(i, j, Int32.Parse(numbers[j]));
                        }
                    }
                    return loadedTable;
                }
            }
            catch
            {
                throw new SnakeDataException();
            }
        }
    }
}
