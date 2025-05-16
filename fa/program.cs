using System;
using System.Collections.Generic;

namespace fans
{
    /// <summary>
    /// Класс состояния конечного автомата.
    /// </summary>
    public class State
    {
        /// <summary>
        /// Имя состояния.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Словарь переходов из данного состояния по символам.
        /// </summary>
        public Dictionary<char, State> Transitions { get; } = new();

        /// <summary>
        /// Флаг, указывающий, является ли состояние принимающим.
        /// </summary>
        public bool IsAcceptState { get; set; }
    }

    /// <summary>
    /// Класс конечного автомата, строящегося через явные состояния и переходы.
    /// </summary>
    public class FA
    {
        /// <summary>
        /// Начальное состояние автомата.
        /// </summary>
        private State InitialState;

        /// <summary>
        /// Конструктор автомата, принимает параметр режима (тип задачи).
        /// </summary>
        /// <param name="type">Тип автомата: "FA1", "FA2", "FA3".</param>
        public FA(string type)
        {
            // Создаем состояния
            var a = new State() { Name = "a", IsAcceptState = false };
            var b = new State() { Name = "b", IsAcceptState = false };
            var c = new State() { Name = "c", IsAcceptState = false };
            var d = new State() { Name = "d", IsAcceptState = false };
            var e = new State() { Name = "e", IsAcceptState = false };

            switch (type)
            {
                case "FA1":
                    // FA1: строка содержит ровно один '0' и хотя бы один '1'
                    a.Transitions['0'] = b;
                    a.Transitions['1'] = a;

                    b.Transitions['0'] = c;
                    b.Transitions['1'] = b;

                    c.Transitions['0'] = c;
                    c.Transitions['1'] = c;

                    b.IsAcceptState = true;
                    InitialState = a;
                    break;

                case "FA2":
                    // FA2: кол-во '0' и '1' нечётно
                    // Состояния по парности (чет/нечет): (0,0)->a, (1,0)->b, (0,1)->c, (1,1)->d
                    a.IsAcceptState = false;
                    b.IsAcceptState = false;
                    c.IsAcceptState = false;
                    d.IsAcceptState = true;

                    a.Transitions['0'] = b; // (0,0) -> (1,0)
                    a.Transitions['1'] = c; // (0,0) -> (0,1)

                    b.Transitions['0'] = a; // (1,0) -> (0,0)
                    b.Transitions['1'] = d; // (1,0) -> (1,1)

                    c.Transitions['0'] = d; // (0,1) -> (1,1)
                    c.Transitions['1'] = a; // (0,1) -> (0,0)

                    d.Transitions['0'] = c; // (1,1) -> (0,1)
                    d.Transitions['1'] = b; // (1,1) -> (1,0)

                    InitialState = a;
                    break;

                case "FA3":
                    // FA3: содержит подстроку "11"
                    a.IsAcceptState = false;
                    b.IsAcceptState = true;
                    c.IsAcceptState = false;

                    a.Transitions['0'] = a;
                    a.Transitions['1'] = c;

                    c.Transitions['0'] = a;
                    c.Transitions['1'] = b;

                    b.Transitions['0'] = b;
                    b.Transitions['1'] = b;

                    InitialState = a;
                    break;

                default:
                    throw new ArgumentException("Unknown FA type");
            }
        }

        /// <summary>
        /// Запуск автомата на входной последовательности символов.
        /// </summary>
        /// <param name="s">Входная строка из символов '0' и '1'.</param>
        /// <returns>True, если достигнуто принимающее состояние; иначе false.</returns>
        public bool Run(IEnumerable<char> s)
        {
            State current = InitialState;
            foreach (var c in s)
            {
                if (!current.Transitions.TryGetValue(c, out var nextState))
                    return false; // нет перехода — строка не принимается
                current = nextState;
            }
            return current.IsAcceptState;
        }
    }

    /// <summary>
    /// Конечный автомат FA1, реализующий поведение типа "FA1".
    /// </summary>
    public class FA1 : FA
    {
        /// <summary>
        /// Конструктор FA1.
        /// </summary>
        public FA1() : base("FA1") { }
    }

    /// <summary>
    /// Конечный автомат FA2, реализующий поведение типа "FA2".
    /// </summary>
    public class FA2 : FA
    {
        /// <summary>
        /// Конструктор FA2.
        /// </summary>
        public FA2() : base("FA2") { }
    }

    /// <summary>
    /// Конечный автомат FA3, реализующий поведение типа "FA3".
    /// </summary>
    public class FA3 : FA
    {
        /// <summary>
        /// Конструктор FA3.
        /// </summary>
        public FA3() : base("FA3") { }
    }

    /// <summary>
    /// Главный класс программы. Запускает 3 автомата на наборе входных данных.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            var inputs = new List<string> { "101", "1100", "1111", "0", "11", "0011", "1", "0001" };

            var fa1 = new FA("FA1");
            var fa2 = new FA("FA2");
            var fa3 = new FA("FA3");

            foreach (var input in inputs)
            {
                Console.WriteLine($"Input: {input}");
                Console.WriteLine($"FA1: {fa1.Run(input)}");
                Console.WriteLine($"FA2: {fa2.Run(input)}");
                Console.WriteLine($"FA3: {fa3.Run(input)}");
                Console.WriteLine();
            }
        }
    }
}
