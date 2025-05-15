using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteAutomata
{
    public class State
    {
        public string Label { get; }
        public bool Accepting { get; }
        private Dictionary<char, State> _transitions;

        public State(string label, bool accepting = false)
        {
            Label = label;
            Accepting = accepting;
            _transitions = new Dictionary<char, State>();
        }

        public void Set(char symbol, State target)
        {
            _transitions[symbol] = target;
        }

        public State Go(char input)
        {
            return _transitions.ContainsKey(input) ? _transitions[input] : null;
        }
    }


    public class FA1
    {
        private readonly State _entry;

        public FA1()
        {
            var start = new State("start");
            var gotZero = new State("gotZero");
            var valid = new State("valid", true);
            var fail = new State("fail");
            var seenOne = new State("seenOne");

            start.Set('0', gotZero);
            start.Set('1', seenOne);

            gotZero.Set('0', fail);
            gotZero.Set('1', valid);

            seenOne.Set('0', valid);
            seenOne.Set('1', seenOne);

            valid.Set('0', fail);
            valid.Set('1', valid);

            fail.Set('0', fail);
            fail.Set('1', fail);

            _entry = start;
        }

        public bool? Run(string input)
        {
            var current = _entry;
            foreach (var ch in input)
            {
                current = current.Go(ch);
                if (current == null) return null;
            }
            return current.Accepting;
        }
    }

    public class FA2
    {
        private readonly State _start;

        public FA2()
        {
            var evenEven = new State("evenEven");
            var oddEven = new State("oddEven");
            var evenOdd = new State("evenOdd");
            var oddOdd = new State("oddOdd", true);

            evenEven.Set('0', oddEven);
            evenEven.Set('1', evenOdd);

            oddEven.Set('0', evenEven);
            oddEven.Set('1', oddOdd);

            evenOdd.Set('0', oddOdd);
            evenOdd.Set('1', evenEven);

            oddOdd.Set('0', evenOdd);
            oddOdd.Set('1', oddEven);

            _start = evenEven;
        }

        public bool? Run(string input)
        {
            var current = _start;
            foreach (var ch in input)
            {
                current = current.Go(ch);
                if (current == null) return null;
            }
            return current.Accepting;
        }
    }

    public class FA3
    {
        private readonly State _initial;

        public FA3()
        {
            var qStart = new State("start");
            var qOne = new State("one");
            var qMatch = new State("match", true);

            qStart.Set('0', qStart);
            qStart.Set('1', qOne);

            qOne.Set('0', qStart);
            qOne.Set('1', qMatch);

            qMatch.Set('0', qMatch);
            qMatch.Set('1', qMatch);

            _initial = qStart;
        }

        public bool? Run(string input)
        {
            State current = _initial;
            foreach (char symbol in input)
            {
                current = current.Go(symbol);
                if (current == null) return null;
            }
            return current.Accepting;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var testCases = new List<string>
        {
            "011", "10", "1101", "001", "111"
        };

            var automata = new Dictionary<string, Func<string, bool?>>
        {
            { "FA1 (exactly one 0, at least one 1)", new FA1().Run },
            { "FA2 (odd 0s and 1s)", new FA2().Run },
            { "FA3 (contains '11')", new FA3().Run }
        };

            foreach (var input in testCases)
            {
                Console.WriteLine($"\nInput: \"{input}\"");
                foreach (var kvp in automata)
                {
                    var label = kvp.Key;
                    var runner = kvp.Value;
                    var result = runner.Invoke(input);
                    Console.WriteLine($"{label}: {result}");
                }
            }
        }
    }
}