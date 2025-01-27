using System;
using System.Collections.Generic;
using WordSearch;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Find the top 10 most repeated words from the word stream.");
        Console.WriteLine("---------------------");
        
        // Create the character matrix
        var matrix = new[]
        {
            "abcdc",
            "fgwio",
            "chill",
            "pqnsd",
            "uvdxy",
        };

        // Create word stream
        var wordstream = new[]
        {
            "cold",
            "wind",
            "snow",
            "chill"
        };

        // Initialize WordFinder with the matrix
        var finder = new WordFinder(matrix);

        // Search for words
        var foundWords = finder.Find(wordstream);

        // Print results
        Console.WriteLine("Found words:");
        foreach (var word in foundWords)
        {
            Console.WriteLine($"- {word}");
        }
    }
}