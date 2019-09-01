using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _3_DistanceBetweenTwoPoints
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = new Questions();
            var question1 = new HashSet<char>() { '1', '2', '3', '4', };
            WriteLine("С# - Уровень 1. Задание 1.3");
            WriteLine("Кузнецов");
            WriteLine("а) Написать программу, которая подсчитывает расстояние между точками с координатами x1, y1 и x2,y2 по формуле r=Math.Sqrt(Math.Pow(x2-x1,2)+Math.Pow(y2-y1,2). Вывести результат, используя спецификатор формата .2f (с двумя знаками после запятой);\n\r" +
                      "б) * Выполнить предыдущее задание, оформив вычисления расстояния между точками в виде метода.");
            WriteLine("Подсчет расстояния между двумя точками");
            WriteLine("Нажмите клавишу Enter чтобы перейти к следующему вопросу.");
            var typeNum = (NumberType)int.Parse(q.QuestionForOneSymbol("В какой формат координат использовать (1 - int, 2 - long, 3 - double, 4 - float)?", question1));
            var t = IsType(typeNum);
            ReadLine();
        }
        

        private static object IsType(NumberType num)
        {
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };
            var arrayNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ',', '.', '-' };
            var type = new object();
            if (num == NumberType.INT)
                Quest<int>(arrayNumForOnlyNum, num);
            else if (num == NumberType.LONG)
                Quest<long>(arrayNumForOnlyNum, num);
            else if (num == NumberType.DOUBLE)
                Quest<double>(arrayNum, num);
            else
                Quest<float>(arrayNum, num);
            return type;
        }

        private static void Quest<M>(HashSet<char> arraySym, NumberType nt) where M : struct
        {
            var p1 = new Point<M>();
            var p2 = new Point<M>();
            var q = new Questions();
            p1.X = q.Question("Точка 1:\n\rХ = ", arraySym).Parse<M>(nt);
            p1.Y = q.Question("Y = ", arraySym).Parse<M>(nt);
            WriteLine("");
            p2.X = q.Question("Точка 2:\n\rХ = ", arraySym).Parse<M>(nt);
            p2.Y = q.Question("Y = ", arraySym).Parse<M>(nt);

            WriteLine("Длина между двумя точками состват {0:F2}", DistanceBetweenTwoPoints<M>(p1.X, p1.Y, p2.X, p2.Y));
        }

        private static T DistanceBetweenTwoPoints<T>(T x1, T y1, T x2, T y2) where T : struct =>
            NewtonSqrt<T>((((dynamic)x2 - (dynamic)x1) * ((dynamic)x2 - (dynamic)x1)) +
                          (((dynamic)y2 - (dynamic)y1) * ((dynamic)y2 - (dynamic)y1)));

        private static T NewtonSqrt<T>(T x)
        {
            T g = x;
            while (true)
            {
                T t = ((dynamic)x / g + g) / 2;
                if (Math.Abs(g - (dynamic)t) < 1E-10)                
                    return g;                
                g = (dynamic)t;
            }
        }
    }

    public static class StringExtension
    {
        public static T Parse<T>(this string str, NumberType nt) where T : struct
        {
            var obj = new object();
            if (nt == NumberType.INT)
                obj = int.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);      
            else if (nt == NumberType.LONG)
                obj = long.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else if (nt == NumberType.DOUBLE)
                obj = double.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            else
                obj = float.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
            return (T)obj;
        }
    }

    public enum NumberType
    {
        INT = 1,
        LONG,
        DOUBLE,
        FLOAT 
    }

    public class Questions
    {
        public string QuestionForOneSymbol(string text, HashSet<char> arraySym)
        {

            WriteLine(text);
            var textAnswer = new StringBuilder();
            while (true)
            {
                var symbol = ReadKey(true);
                if (arraySym.Contains(symbol.KeyChar))
                {
                    textAnswer.Append(symbol.KeyChar.ToString());
                    Write(symbol.KeyChar.ToString());
                    break;
                }
            }
            WriteLine("");
            return textAnswer.ToString();
        }

        public string Question(string text, HashSet<char> arraySym)
        {
            Write(text);
            var textAnswer = new StringBuilder();
            while (true)
            {
                var symbol = ReadKey(true);
                if (arraySym.Contains(symbol.KeyChar))
                {
                    textAnswer.Append(symbol.KeyChar.ToString());
                    Write(symbol.KeyChar.ToString());
                }

                if (symbol.Key == ConsoleKey.Backspace && textAnswer.Length > 0)
                {
                    textAnswer.Remove(textAnswer.Length - 1, 1);
                    Write(symbol.KeyChar.ToString());
                    Write(" ");
                    Write(symbol.KeyChar.ToString());
                }
           
                if (symbol.Key == ConsoleKey.Enter &&
                          double.TryParse(textAnswer.ToString()
                              .Replace(".", ","),
                              out var number))
                    break;
            }
            WriteLine("");
            return textAnswer.ToString();
        }
    }

    public class Point<V>
    {
        V x;
        V y;

        public V X { get => x; set => x = value; }
        public V Y { get => y; set => y = value; }
    }

}
