using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class Program
{
    public static bool IsVowel(char c)
    {
        char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

        if (!char.IsLetter(c))
        {
            throw new ArgumentException("The input must be a letter.");
        }

        return vowels.Contains(c);
    }

    public static string[] GetUniqueVowelWords(string fileContent)
    {
        // Очищення вмісту файлу та розділення на слова
        return fileContent
            .Split(new char[] { ' ', '\t', '\n', '\r', ',', '.', '!', '?', '|', '(', ')', '$', '=', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(word => Regex.Replace(word, "[^a-zA-Z]", "")) // Залишити тільки букви
            .Where(word => !string.IsNullOrEmpty(word) && word.All(IsVowel)) // Перевірити, чи всі букви - голосні
            .GroupBy(word => word) // Групування за словом
            .Where(group => group.Count() == 1) // Вибрати лише унікальні слова
            .Select(group => group.Key)
            .ToArray();
    }

    static void Main(string[] args)
    {
        string filePath = "/Users/nataliiagricisin/Documents/3 курс/II семестр/Lab2_MSTest/Lab2_MSTest/input.txt";

        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist: " + filePath);
                return;
            }

            string fileContent = File.ReadAllText(filePath);
            Console.WriteLine("File Content:");
            Console.WriteLine(fileContent);

            string[] words = GetUniqueVowelWords(fileContent);

            Console.WriteLine("Filtered Words:");
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Unable to read the file: " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }
    }

}
