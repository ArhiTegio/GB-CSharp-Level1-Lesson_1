using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using ConsoleExtension;
using System.Globalization;

namespace _5_PrintForWindows
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = new StringBuilder();
            var q = new Questions();
            var arrayRusSymbol = new HashSet<char>()
            {
                'й','ц','у','к','е','н','г','ш','щ','з','х','ъ','ф','ы','в','а','п','р','о','л','д','ж','э','я','ч','с','м','и','т','ь','б','ю',
                'Й','Ц','У','К','Е','Н','Г','Ш','Щ','З','Х','Ъ','Ф','Ы','В','А','П','Р','О','Л','Д','Ж','Э','Я','Ч','С','М','И','Т','Ь','Б','Ю',
            };

            WriteLine("С# - Уровень 1. Задание 1.5");
            WriteLine("Кузнецов");
            WriteLine("а) Написать программу, которая выводит на экран ваше имя, фамилию и город проживания.\n\r" +
                      "б) * Сделать задание, только вывод организовать в центре экрана.\n\r" +
                      "в) **Сделать задание б с использованием собственных методов(например, Print(string ms, int x, int y).");
            text.Append("Здравствуйте, ");
            text.Append(q.FirstUpper(q.Question<string>("Какое у ваше имя?", arrayRusSymbol).ToLower()));
            text.Append(" ");
            text.Append(q.FirstUpper(q.Question<string>("Какая у вас фамилия?", arrayRusSymbol).ToLower()));
            text.Append(". Вы находитесь в городе ");
            text.Append(q.FirstUpper(q.Question<string>("В каком городе вы находитесь?", arrayRusSymbol).ToLower()));
            text.Append(".");

            var ex = new Extension();
            ex.Print(text.ToString(), PositionForRow.Center, WindowHeight / 2);
            ex.Pause(2000);
        }
    }

    class Questions
    {
        public string FirstUpper(string text) => text.Substring(0, 1).ToUpper() + (text.Length > 1 ? text.Substring(1) : "");

        public string Question<T>(string text, HashSet<char> arraySym)
        {
            Console.WriteLine(text);
            var textAnswer = new StringBuilder();
            while (true)
            {
                var symbol = Console.ReadKey(true);
                if (arraySym.Contains(symbol.KeyChar))
                {
                    textAnswer.Append(symbol.KeyChar.ToString());
                    Console.Write(symbol.KeyChar.ToString());
                }

                if (symbol.Key == ConsoleKey.Backspace && textAnswer.Length > 0)
                {
                    textAnswer.Remove(textAnswer.Length - 1, 1);
                    Console.Write(symbol.KeyChar.ToString());
                    Console.Write(" ");
                    Console.Write(symbol.KeyChar.ToString());
                }

                if (typeof(T) == typeof(string))
                {
                    if (symbol.Key == ConsoleKey.Enter && textAnswer.Length > 0)
                        break;
                }
                else
                    if (symbol.Key == ConsoleKey.Enter &&
                        double.TryParse(textAnswer.ToString()
                            .Replace(".", ","),
                            out var number))
                    break;
            }
            Console.WriteLine("");
            return textAnswer.ToString();
        }
    }
}
