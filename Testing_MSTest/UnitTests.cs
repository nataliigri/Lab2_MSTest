using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using static Program;

namespace Testing_MSTest
{
    [TestClass]
    public class UnitTests
    {

        private string inputFilePath = "";

        [ClassInitialize]
        public static void BeforeClass(TestContext context)
        {
            // Code to run once before any tests in this class
        }

        [TestInitialize]
        public void BeforeEach()
        {
            // Code to run before each test method
            inputFilePath = "/Users/nataliiagricisin/Documents/3 курс/II семестр/lab2_MSTest/lab2_MSTest/input.txt";
        }

        [TestMethod]
        [TestCategory("Group1")] // Додаємо цей тест до групи "Group1"
        public void IsVowel_ReturnsTrueForVowels()
        {
            // Arrange
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

            // Act and Assert
            foreach (char vowel in vowels)
            {
                Assert.IsTrue(IsVowel(vowel));
            }
        }

        [TestMethod]
        [TestCategory("Group1")] 
        public void IsVowel_ReturnsFalse_ForConsonant()
        {
            // Arrange
            char consonant = 'b';

            // Act
            bool result = Program.IsVowel(consonant);

            // Assert
            Assert.IsFalse(result, "Expected 'b' to be a consonant.");
        }

        [TestMethod]
        [TestCategory("Group2")]
        public void IsVowel_IsCaseInsensitive()
        {
            // Arrange
            char upperCaseVowel = 'A';

            // Act
            bool result = Program.IsVowel(upperCaseVowel);

            // Assert
            Assert.IsTrue(result, "Expected 'A' to be recognized as a vowel.");
        }

        [TestMethod]
        [TestCategory("Group1")]
        public void FilteredUniqueVowelWords()
        {
            // Arrange
            string fileContent = File.ReadAllText(inputFilePath);
            string[] expectedUniqueWords = { "Iuea", "ooooo", "ii", "uuu", "iuea"}; // Введіть очікувані унікальні слова з голосних літер

            // Act
            string[] words = GetUniqueVowelWords(fileContent);

            // Assert
            Assert.AreEqual(expectedUniqueWords.Length, words.Length, "Unexpected number of unique vowel words found.");

            foreach (var word in expectedUniqueWords)
            {
                Assert.IsTrue(words.Contains(word), $"Expected unique word '{word}' not found in the filtered list.");
            }
        }

        [TestMethod]
        [TestCategory("Group2")]
        public void IsVowel_ThrowsException_ForInvalidInput()
        {
            // Arrange
            char invalidInput = '1';

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => Program.IsVowel(invalidInput));
        }

        public static IEnumerable<object[]> TestData()
        {
            // Перелік тестових даних: рядки тексту, очікуваний результат
            yield return new object[] { "hello", false };
            yield return new object[] { "a", true };
            yield return new object[] { "aeiou", true };
            yield return new object[] { "xyz", false };
        }

        [TestMethod]
        [TestCategory("Group2")]
        [DynamicData(nameof(TestData), DynamicDataSourceType.Method)]
        public void TestIsVowel(string input, bool expectedResult)
        {
            // Очікуваний результат
            bool result = Program.IsVowel(input[0]);

            // Перевірка
            Assert.AreEqual(expectedResult, result);
        }

    }
}
