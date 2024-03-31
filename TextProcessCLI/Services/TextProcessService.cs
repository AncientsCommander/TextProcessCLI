using System.Text.RegularExpressions;

namespace TextProcessCLI.Services;

public static class TextProcessService
{
    public static string FindWordWithMaxNumbers(string text)
    {
        var wordsWithDigits = SplitToWords(text).Where(w => w.Any(char.IsDigit))
            .DistinctBy(d => d.ToLower()).ToArray();

        string[] wordsWithMaxDigits = [];

        if (wordsWithDigits.Length != 0)
        {
            var maxDigits = wordsWithDigits.Max(m => m.Count(char.IsDigit));
            wordsWithMaxDigits = wordsWithDigits.Where(w => w.Count(char.IsDigit) == maxDigits).ToArray();
        }

        return $"""
                Слова с максимальным количеством цифр:

                {string.Join('\n', wordsWithMaxDigits)}
                """;
    }

    public static string GetLongestWordAndRepetitions(string text)
    {
        var words = SplitToWords(text);
        var longestWord = words.OrderByDescending(o => o.Length).First();
        var repetitions = words.Count(c => c == longestWord);

        return $"""
                Самое длинное слово: {longestWord}

                Количество повторений в тексте: {repetitions}
                """;
    }

    public static string ReplaceNumbers(string text)
    {
        var words = SplitToWords(text);

        for (var i = 0; i < words.Length; i++)
        {
            (string oldWord, string newWord) replace;
            switch (words[i].Trim())
            {
                case "0":
                    replace = ("0", "ноль");
                    break;
                case "1":
                    replace = ("1", "один");
                    break;
                case "2":
                    replace = ("2", "два");
                    break;
                case "3":
                    replace = ("3", "три");
                    break;
                case "4":
                    replace = ("4", "четыре");
                    break;
                case "5":
                    replace = ("5", "пять");
                    break;
                case "6":
                    replace = ("6", "шесть");
                    break;
                case "7":
                    replace = ("7", "семь");
                    break;
                case "8":
                    replace = ("8", "восемь");
                    break;
                case "9":
                    replace = ("9", "девять");
                    break;
                default:
                    continue;
            }

            if (i == 0 || words[i].StartsWith(Environment.NewLine))
                words[i] = FixCases(words[i].Replace(replace.oldWord, replace.newWord));
        }

        return $"""
                Текст с заменёнными цифрами на письменные варианты:

                {string.Concat(words)}
                """;
    }

    public static string GetInterrogativeAndExclamationSentences(string text)
    {
        var sentences = SplitToSentences(text).Where(w => !w.EndsWith('.') && (w.Contains('!') || w.Contains('?')))
            .OrderBy(o => !o.EndsWith('?'))
            .ThenBy(t => !t.EndsWith('!')).ToArray();

        return $"""
                Отсортированные предложения, где сначала вопросительные, а затем восклицательные:

                {string.Join('\n', sentences)}
                """;
    }

    public static string GetSentencesWoCommas(string text)
    {
        var sentences = SplitToSentences(text).Where(w => !w.Contains(',')).ToArray();

        return $"""
                Все предложения, в которых нет запятых:

                {string.Join('\n', sentences)}
                """;
    }

    public static string GetWordsWithSameStartEndLetters(string text)
    {
        var words = SplitToWords(text).DefaultIfEmpty()
            .Where(w => !string.IsNullOrWhiteSpace(w) && w.Length > 1 && w.ToLower().First() == w.ToLower().Last())
            .DistinctBy(d => d?.ToLower()).ToArray();

        return $"""
                Все слова начинающиеся и заканчивающиеся на одну букву:

                {string.Join('\n', words)}
                """;
    }

    private static string[] SplitToWords(string text)
    {
        return Regex.Split(text, @"([^\w\n\r])");
    }

    private static string[] SplitToSentences(string text)
    {
        return Regex.Split(text, @"(?<=[\.!\?])\s+");
    }

    private static string FixCases(string sentence)
    {
        return Regex.Replace(sentence, @"(?<=\A|\s|\r\n\r\n)\w", m => m.Value.ToUpper());
    }
}