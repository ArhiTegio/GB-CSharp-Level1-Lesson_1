using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _4_ExchangeTwoValues
{
    class Program
    {
        static void Main(string[] args)
        {
            var exchange = new Exchange();
            var q = new Questions();
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            WriteLine("С# - Уровень 1. Задание 1.4");
            WriteLine("Кузнецов");
            WriteLine("Написать программу обмена значениями двух переменных:\n\r" +
                        "а) с использованием третьей переменной;\n\r" +
                        "б) *без использования третьей переменной.");
            var t = new int[2];
            t[0] = q.Question<int>("Задайте первое число ", arrayNumForOnlyNum).Parse<int>();
            t[1] = q.Question<int>("Задайте второе число ", arrayNumForOnlyNum).Parse<int>();

            exchange.TwoValuesUsingThridValue(t);
            WriteLine("После обмена значениями с использованием третьей переменной:");
            WriteLine("Первое число равно {0}. Второе число равно {1}.", t[0], t[1]);

            exchange.TwoValuesNotUsingThridValue(t);
            WriteLine("После обмена значениями без использования третьей переменной:");
            WriteLine("Первое число равно {0}. Второе число равно {1}.", t[0], t[1]);
            ReadKey();
        }        
    }

    public class Exchange
    {
        public void TwoValuesUsingThridValue<T>(T[] x)
        {
            T temp;
            temp = x[1];
            x[1] = x[0];
            x[0] = temp;
        }

        public void TwoValuesNotUsingThridValue<T>(T[] x) => (x[0], x[1]) = (x[1], x[0]);
    }

    public static class StringExtension
    {
        public static T Parse<T>(this string str) where T : struct
        {
            T obj = new T();
            if (obj.GetType() == typeof(int))
                obj = (dynamic)int.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else if (obj.GetType() == typeof(long))
                obj = (dynamic)long.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else if (obj.GetType() == typeof(double))
                obj = (dynamic)double.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else if (obj.GetType() == typeof(float))
                obj = (dynamic)float.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else if (obj.GetType() == typeof(short))
                obj = (dynamic)short.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else if (obj.GetType() == typeof(ushort))
                obj = (dynamic)ushort.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else if (obj.GetType() == typeof(uint))
                obj = (dynamic)uint.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else if (obj.GetType() == typeof(ulong))
                obj = (dynamic)ulong.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else if (obj.GetType() == typeof(byte))
                obj = (dynamic)byte.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else if (obj.GetType() == typeof(byte))
                obj = (dynamic)byte.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            return (T)obj;
        }
    }

    class Questions
    {
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
