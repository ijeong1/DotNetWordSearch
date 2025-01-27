using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordSearch
{
    /// <summary>
    /// Finds words in a character matrix that appear horizontally (left to right) 
    /// or vertically (top to bottom).
    /// </summary>
    public class WordFinder
    {
        private readonly HashSet<string> horizontalWords = new();
        private readonly HashSet<string> verticalWords = new();
        private readonly int rows;
        private readonly int cols;

        /// <summary>
        /// Initializes the WordFinder with a character matrix.
        /// </summary>
        /// <param name="matrix">Collection of strings representing rows in the matrix</param>
        /// <exception cref="ArgumentException">Thrown when matrix is invalid</exception>
        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null || !matrix.Any())
                throw new ArgumentException("Matrix cannot be null or empty");

            var matrixArray = matrix.ToArray();
            rows = matrixArray.Length;
            cols = matrixArray[0].Length;

            // Validate matrix dimensions
            if (rows > 64 || cols > 64)
                throw new ArgumentException("Matrix cannot exceed 64x64");

            if (matrixArray.Any(row => row.Length != cols))
                throw new ArgumentException("All rows must have the same length");

            // Pre-process horizontal words
            foreach (var row in matrixArray)
            {
                horizontalWords.Add(row);
            }

            // Pre-process vertical words
            for (int j = 0; j < cols; j++)
            {
                var verticalWord = new StringBuilder(rows);
                for (int i = 0; i < rows; i++)
                {
                    verticalWord.Append(matrixArray[i][j]);
                }
                verticalWords.Add(verticalWord.ToString());
            }
        }

        /// <summary>
        /// Finds the top 10 most repeated words from the word stream that exist in the matrix.
        /// </summary>
        /// <param name="wordstream">Collection of words to search for</param>
        /// <returns>Top 10 most frequent words found in the matrix</returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            if (wordstream == null || !wordstream.Any())
                return Enumerable.Empty<string>();

            return wordstream
                .Where(word => !string.IsNullOrEmpty(word))
                .Distinct() // Count each word only once even if repeated in stream
                .Select(word => new
                {
                    Word = word,
                    Count = CountOccurrences(word)
                })
                .Where(x => x.Count > 0)
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.Word) // Secondary sort by word for stable ordering
                .Take(10)
                .Select(x => x.Word);
        }

        private int CountOccurrences(string word)
        {
            int count = 0;

            // Check horizontal words
            foreach (var horizontalWord in horizontalWords)
            {
                if (horizontalWord.Contains(word))
                    count++;
            }

            // Check vertical words
            foreach (var verticalWord in verticalWords)
            {
                if (verticalWord.Contains(word))
                    count++;
            }

            return count;
        }
    }
}