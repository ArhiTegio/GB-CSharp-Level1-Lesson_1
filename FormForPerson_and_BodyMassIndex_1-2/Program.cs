using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _1_2_FormForPerson_and_BodyMassIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayRusSymbol = new HashSet<char>()
            {
                'й','ц','у','к','е','н','г','ш','щ','з','х','ъ','ф','ы','в','а','п','р','о','л','д','ж','э','я','ч','с','м','и','т','ь','б','ю',
                'Й','Ц','У','К','Е','Н','Г','Ш','Щ','З','Х','Ъ','Ф','Ы','В','А','П','Р','О','Л','Д','Ж','Э','Я','Ч','С','М','И','Т','Ь','Б','Ю',
            };

            var arrayNum = new HashSet<char>()
            {
                '1','2','3','4','5','6','7','8','9','0',
            };
            var person = new Person();
            var questions = new Questions();
            var text = new StringBuilder();
            WriteLine("С# - Уровень 1. Задание 1.1-1.2");
            WriteLine("Кузнецов");
            WriteLine("1.1. Написать программу «Анкета». Последовательно задаются вопросы (имя, фамилия, возраст, рост, вес). В результате вся информация выводится в одну строчку: \n\r" +
                        "а) используя склеивание;\n\r"+
                        "б) используя форматированный вывод;\n\r" +
                        "в) используя вывод со знаком $.");
            WriteLine("1.2. Ввести вес и рост человека. Рассчитать и вывести индекс массы тела (ИМТ) по формуле I=m/(h*h); где m — масса тела в килограммах, h — рост в метрах.");
            WriteLine("Анкета для GeekBrains");
            WriteLine("Заполните анкету русским символами и числами где это необходимо.");
            WriteLine("Нажмите клавишу Enter чтобы перейти к следующему вопросу.");
            text.Append("Здравствуйте, ");
            text.Append(person.Name = questions.FirstUpper(questions.Question("Какое у ваше имя?", arrayRusSymbol).ToLower()));
            text.Append(" ");
            text.Append(person.Surname = questions.FirstUpper(questions.Question("Какое у вас отчество?", arrayRusSymbol).ToLower()));
            text.Append(" ");
            text.Append(person.MiddleName = questions.FirstUpper(questions.Question("Какая у вас фамилия?", arrayRusSymbol).ToLower()));
            text.Append(". Вам ");
            person.Age_years = int.Parse(questions.Question("Сколько вам лет?", arrayNum));
            text.Append(person.Age_years);
            text.Append(" ");
            text.Append(questions.DeclinationForAge(person.Age_years));
            text.Append(". У вас рост ");
            text.Append(person.Height_cm = int.Parse(questions.Question("Какой у вас рост в см?", arrayNum)));
            text.Append(" см, масса составляет ");
            text.Append(person.Mass_kg = int.Parse(questions.Question("Какой у вас вес в кг?", arrayNum)));
            text.Append(" кг. ");

            //склеивание
            //Console.WriteLine("Здравствуйте, " + person.Name + " " + person.Surname + " " + person.MiddleName + 
            //    ". Вам " + person.Age_years + " " + questions.DeclinationForAge(person.Age_years) + 
            //    ". У вас рост " + person.Height_cm + " см, масса составляет " + person.Mass_kg + " кг. "); 
            //используя форматированный вывод
            WriteLine(text.ToString());
            //используя вывод со знаком $
            WriteLine($"Ваш индекс массы тела (ИМТ) составляет { person.BodyMassIndex()}");
            ReadLine();
        }
    }

    class Questions
    {
        public string FirstUpper(string text) => text.Substring(0, 1).ToUpper() + (text.Length > 1 ? text.Substring(1) : "");

        public string DeclinationForAge(int num)
        {
            if (num > 100)
            {
                num = num % 100;
            }
            if (num >= 0 && num <= 20)
            {
                if (num == 0)
                    return "лет";
                else if (num == 1)
                    return "год";
                else if (num >= 2 && num <= 4)
                    return "года";
                else if (num >= 5 && num <= 20)
                    return "лет";
            }
            else if (num > 20)
            {
                string str;
                str = num.ToString();
                string n = str[str.Length - 1].ToString();
                int m = Convert.ToInt32(n);
                if (m == 0)
                    return "лет";
                else if (m == 1)
                    return "год";
                else if (m >= 2 && m <= 4)
                    return "года";
                else
                    return "лет";
            }
            return null;
        }

        public string Question(string text, HashSet<char> arraySym)
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

                
                if (symbol.Key == ConsoleKey.Enter && textAnswer.Length > 0)
                    break;
            }
            Console.WriteLine("");
            return textAnswer.ToString();
        }
    }

    class Person
    {
        private string surname = "";
        private string name = "";
        private string middleName = "";
        private int mass_kg = 0;
        private int age_years = 0;
        private int height_cm = 0;

        public string Surname { get => surname; set => surname = value; }
        public string Name { get => name; set => name = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public int Mass_kg { get => mass_kg; set => mass_kg = value; }
        public int Age_years { get => age_years; set => age_years = value; }
        public int Height_cm { get => height_cm; set => height_cm = value; }

        public int BodyMassIndex() => mass_kg / ((height_cm / 100) * (height_cm / 100));
    }
}
