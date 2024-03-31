using System.Text;
using System.Text.RegularExpressions;

namespace TextProcessCLI.Services;

public static class MenuUiService
{
    private static readonly Encoding FileEncoding = Encoding.UTF8;

    public static int ShowMainMenu(string text)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"""
                               Оригинал текста:

                               {text}

                               -----------------------------------------------

                               Меню:
                               1. Показать слова, содержащие максимальное количество цифр
                               2. Показать самое длинное слово и сколько раз оно встречается в тексте
                               3. Заменить в тексте цифры (0-9) на слома "ноль"..."девять"
                               4. Показать сначала вопросительные, а затем восклицательные предложения
                               5. Показать предложения, не содержащие запятые
                               6. Показать слова, начинающиеся и заканцивающиеся на одну и ту же букву

                               9. Перечитать текст из файла
                               0. Завершить программу

                               """);
            Console.Write("Выберите необходимое действие: ");

            if (!int.TryParse(Console.ReadLine(), out var choice)) continue;

            return choice;
        }
    }

    public static void ShowResultMenu(string text, string resultMessage)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"""
                               Текст из файла:

                               {text}

                               -----------------------------------------------

                               {resultMessage}

                               Меню:
                               0. Вернуться в предыдущее меню

                               """);
            Console.Write("Выберите необходимое действие: ");

            if (int.TryParse(Console.ReadLine(), out var choice) && choice == 0) break;
        }
    }

    public static string GetTextMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("""
                              Выберите метод вставки текста:

                              1. Вставить текст в консоль
                              2. Считать текст из файла
                               
                              """);
            Console.Write("Выберите необходимое действие: ");

            if (!int.TryParse(Console.ReadLine(), out var choice)) continue;

            string? text;
            switch (choice)
            {
                case 1:
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("""
                                          Считывание текста из консоли.

                                          Вставить текст в консоль:
                                          """);

                        text = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(text)) continue;

                        break;
                    }

                    break;
                case 2:
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("""
                                          Считывание текста с файла.

                                          Укажите путь к файлу:
                                          """);

                        var path = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(path)) continue;

                        try
                        {
                            text = File.ReadAllText(Regex.Replace(path, @"[<>""""/|?*\x00-\x1F]", ""), FileEncoding)
                                .Trim();
                        }
                        catch (FileNotFoundException)
                        {
                            Console.WriteLine("""

                                              Ошибка при считывании файла. Проверьте путь, наименование файла и его наличие.

                                              Нажмите любую кнопку, чтобы повторить
                                              """);
                            Console.ReadKey();
                            continue;
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine($"""

                                               Ошибка при считывании файла. Сообщение ошибки: {exception.Message}.

                                               Нажмите любую кнопку, чтобы повторить
                                               """);
                            Console.ReadKey();
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(text)) continue;

                        break;
                    }

                    break;
                default:
                    continue;
            }

            // if (Regex.IsMatch(text, "^[А-Яа-я]+$"))
            // {
            //     Console.WriteLine(
            //         "Текст содержит недопустимые символы. Исправьте текст и нажмите любую кнопку для повтора");
            //                 
            // }

            return text;
        }
    }
}