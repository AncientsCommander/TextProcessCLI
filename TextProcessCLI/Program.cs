using TextProcessCLI.Services;

namespace TextProcessCLI;

internal static class Program
{
    public static void Main()
    {
        while (true)
        {
            var text = MenuUiService.GetTextMenu();

            var isReReadRequested = false;
            while (true)
            {
                var choice = MenuUiService.ShowMainMenu(text);

                switch (choice)
                {
                    case 1:
                        MenuUiService.ShowResultMenu(text, TextProcessService.FindWordWithMaxNumbers(text));
                        break;
                    case 2:
                        MenuUiService.ShowResultMenu(text, TextProcessService.GetLongestWordAndRepetitions(text));
                        break;
                    case 3:
                        MenuUiService.ShowResultMenu(text, TextProcessService.ReplaceNumbers(text));
                        break;
                    case 4:
                        MenuUiService.ShowResultMenu(text,
                            TextProcessService.GetInterrogativeAndExclamationSentences(text));
                        break;
                    case 5:
                        MenuUiService.ShowResultMenu(text, TextProcessService.GetSentencesWoCommas(text));
                        break;
                    case 6:
                        MenuUiService.ShowResultMenu(text, TextProcessService.GetWordsWithSameStartEndLetters(text));
                        break;
                    case 9:
                        isReReadRequested = true;
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Программа завершена");
                        Environment.Exit(0);
                        break;
                    default:
                        continue;
                }

                if (isReReadRequested) break;
            }
        }
    }
}