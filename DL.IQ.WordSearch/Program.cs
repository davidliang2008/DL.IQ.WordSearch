using System;
using System.Collections.Generic;
using System.Linq;

namespace DL.IQ.WordSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] data = {
                { 'k', 'c', 'o', 'l', 'o', 'r', 'f', 'l', 'j', 'n', 'p', 's', 'm', 't', 'b' },
                { 'p', 'c', 'v', 'c', 'e', 'x', 'd', 'r', 'i', 'h', 't', 'd', 'e', 'k', 'a' },
                { 'r', 'r', 'o', 'p', 'a', 'e', 'u', 'f', 'c', 'r', 'e', 'a', 't', 'e', 'c' },
                { 'o', 'g', 'a', 'l', 's', 'b', 'h', 't', 't', 'v', 'x', 'n', 'a', 'y', 'k' },
                { 'c', 'p', 'n', 'i', 'b', 'y', 'l', 'a', 'r', 'z', 't', 'c', 'l', 'm', 'g' },
                { 'e', 'z', 'g', 'i', 's', 'e', 'm', 'a', 'g', 'a', 'z', 'i', 'n', 'e', 'r' },
                { 's', 'n', 'n', 'g', 't', 'e', 'c', 'b', 'k', 'e', 'n', 't', 'e', 'u', 'o' },
                { 's', 'g', 'm', 't', 'o', 'n', 'd', 'n', 'o', 'd', 'o', 's', 'r', 'y', 'u' },
                { 'x', 's', 'e', 'r', 'u', 'g', 'i', 'f', 'e', 'l', 'i', 'i', 'f', 'r', 'n' },
                { 'b', 'r', 't', 'c', 'e', 'j', 'o', 'r', 'p', 'l', 's', 't', 'l', 'e', 'd' },
                { 's', 'o', 'h', 'y', 'a', 'a', 'k', 'a', 'p', 'e', 'u', 'r', 'r', 'e', 'r' },
                { 's', 'b', 'o', 'k', 'l', 'f', 'g', 'd', 'g', 'p', 'l', 'a', 't', 'e', 's' },
                { 'e', 'l', 'd', 'k', 'f', 'e', 'r', 'a', 'x', 'o', 'l', 'a', 'k', 'b', 's' },
                { 'r', 'b', 's', 'p', 's', 'd', 'm', 'u', 'p', 'c', 'i', 'r', 'b', 'a', 'f' },
                { 'p', 't', 's', 'i', 'u', 'i', 'j', 'h', 's', 'l', 'o', 'm', 'f', 'e', 'u' }
            };

            IEnumerable<string> hintList = new List<string> {
                "rubbing",
                "methods",
                "magazine",
                "surface",
                "symbols",
                "color",
                "fabric",
                "figures",
                "images",
                "block",
                "text",
                "background"
            };

            int rowCount = data.GetLength(0);
            int columnCount = data.GetLength(1);

            List<string> result = new List<string>();

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    bool[,] visited = new bool[rowCount, columnCount];
                    result.AddRange(SearchAround(data, hintList, visited, i, j, ""));
                }
            }

            Console.WriteLine("There are " + result.Count + " results: ");
            Console.WriteLine(String.Join(", ", result));

            Console.ReadKey();
        }

        private static List<string> SearchAround(char[,] data, IEnumerable<string> hintList, bool[,] visited,
            int rowIndex, int colIndex, string prefix)
        {
            List<string> result = new List<string>();
            if (!hintList.Any() || !IsOKToSearch(data, rowIndex, colIndex, visited))
            {
                return result;
            }

            int rowCount = data.GetLength(0);
            int columnCount = data.GetLength(1);
            visited[rowIndex, colIndex] = true;
            prefix += data[rowIndex, colIndex];

            if (hintList.Contains(prefix))
            {
                result.Add(prefix);
            }
            else
            {
                // Try to generate a new hint list with the words starting with the prefix
                hintList = hintList
                    .Where(x => x.StartsWith(prefix))
                    .ToList();
            }

            // Right
            result.AddRange(SearchAround(data, hintList, visited, rowIndex, colIndex + 1, prefix));
            // Bottom
            result.AddRange(SearchAround(data, hintList, visited, rowIndex + 1, colIndex, prefix));
            // Left
            result.AddRange(SearchAround(data, hintList, visited, rowIndex, colIndex - 1, prefix));
            // Top
            result.AddRange(SearchAround(data, hintList, visited, rowIndex - 1, colIndex, prefix));

            return result;
        }

        private static bool IsOKToSearch(char[,] data, int rowIndex, int colIndex, bool[,] visited)
        {
            int rowCount = data.GetLength(0);
            int columnCount = data.GetLength(1);

            return
                rowIndex >= 0 && rowIndex < rowCount &&
                colIndex >= 0 && colIndex < columnCount &&
                !visited[rowIndex, colIndex];
        }
    }
}
