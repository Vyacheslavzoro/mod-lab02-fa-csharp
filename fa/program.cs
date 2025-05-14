using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteAutomata
{
    public class State
    {
        public string Id { get; }
        public bool IsFinal { get; }
        public Dictionary<char, State> Moves { get; }

        public State(string id, bool isFinal = false)
        {
            Id = id;
            IsFinal = isFinal;
            Moves = new Dictionary<char, State>();
        }

        public void AddTransition(char symbol, State next)
        {
            Moves[symbol] = next;
        }

        public State Next(char input)
        {
            return Moves.ContainsKey(input) ? Moves[input] : null;
        }
    }


    public class FA1
    {
        private readonly State _start;

        public FA1()
        {
            var s0 = new State("Start");
            var s1 = new State("ZeroSeen");
            var s2 = new State("Accept", true);
            var sE = new State("Error");
            var s1n = new State("OneSeen");

            s0.AddTransition('0', s1);
            s0.AddTransition('1', s1n);

            s1.AddTransition('0', sE);
            s1.AddTransition('1', s2);

            s1n.AddTransition('0', s2);
            s1n.AddTransition('1', s1n);

            s2.AddTransition('0', sE);
            s2.AddTransition('1', s2);

            sE.AddTransition('0', sE);
            sE.AddTransition('1', sE);

            _start = s0;
        }

        public bool? Run(string input)
        {
            var current = _start;
            foreach (var symbol in input)
            {
                current = current.Next(symbol);
                if (current == null) return null;
            }
            return current.IsFinal;
        }

    }

  public class FA2
  {
        private readonly State _start;

        public FA2()
        {
            var evenEven = new State("00");
            var oddEven = new State("10");
            var evenOdd = new State("01");
            var oddOdd = new State("11", true);

            evenEven.AddTransition('0', oddEven);
            evenEven.AddTransition('1', evenOdd);

            oddEven.AddTransition('0', evenEven);
            oddEven.AddTransition('1', oddOdd);

            evenOdd.AddTransition('0', oddOdd);
            evenOdd.AddTransition('1', evenEven);

            oddOdd.AddTransition('0', evenOdd);
            oddOdd.AddTransition('1', oddEven);

            _start = evenEven;
        }

        public bool? Run(string input)
        {
            var state = _start;
            foreach (var ch in input)
            {
                state = state.Next(ch);
                if (state == null) return null;
            }
            return state.IsFinal;
        }
    }
  
  public class FA3
  {
        private readonly State _start;

        public FA3()
        {
            var q0 = new State("Init");
            var q1 = new State("Seen1");
            var q2 = new State("Seen11", true);

            q0.AddTransition('0', q0);
            q0.AddTransition('1', q1);

            q1.AddTransition('0', q0);
            q1.AddTransition('1', q2);

            q2.AddTransition('0', q2);
            q2.AddTransition('1', q2);

            _start = q0;
        }

        public bool? Run(string input)
        {
            var state = _start;
            foreach (var ch in input)
            {
                state = state.Next(ch);
                if (state == null) return null;
            }
            return state.IsFinal;
        }
    }

  class Program
  {
        static void Main(string[] args)
        {
            var inputs = new[] { "011", "10", "1101", "001", "111" };

            var fa1 = new FA1();
            var fa2 = new FA2();
            var fa3 = new FA3();

            foreach (var input in inputs)
            {
                Console.WriteLine($"Input: {input}");
                Console.WriteLine($"FA1 (one 0, at least one 1): {fa1.Accepts(input)}");
                Console.WriteLine($"FA2 (odd 0s and 1s): {fa2.Accepts(input)}");
                Console.WriteLine($"FA3 (contains '11'): {fa3.Accepts(input)}");
                Console.WriteLine("-----");
            }
        }
    }
}