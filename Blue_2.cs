using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name { get { return _name; } }
            public int Bank { get { return _bank; } }
            public Participant[] Participants
            {
                get
                { 
                    return _participants; 
                }
            }
            public abstract double[] Prize { get; }
            public WaterJump(string Name, int Bank)
            {
                _name = Name;
                _bank = Bank;
                _participants = new Participant[0];
            }
            public void Add(Participant participant)
            {
                Participant[] nparticipants = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++) { nparticipants[i] = _participants[i]; }
                nparticipants[nparticipants.Length - 1] = participant;
                _participants = nparticipants;
            }
            public void Add(Participant[] participants)
            {
                if (participants == null) return;
                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }
        }
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }
            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3) return null;

                    double[] prizes = new double[3];
                    prizes[0] = 0.5 * Bank;
                    prizes[1] = 0.3 * Bank;
                    prizes[2] = 0.2 * Bank;
                    return prizes;
                }
            }

        }
        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }
            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3) return null;
                    int qp = 10;
                    if (Participants.Length / 2 < qp)
                    {
                        qp = Participants.Length / 2;
                    }
                    double n = 20.0 / qp;
                    double[] prizes = new double[qp];
                    for (int i = 0; i < qp; i++)
                    {
                        prizes[i] = (n / 100) * Bank;
                    }
                    prizes[0] += 0.4 * Bank;
                    prizes[1] += 0.25 * Bank;
                    prizes[2] += 0.15 * Bank;
                    return prizes;

                }
            }
        }
        public struct Participant
        {
            private string _Name;
            private string _Surname;
            private int[,] _Marks;
            private int _JNumber;

            public string Name { get { return _Name; } }
            public string Surname { get { return _Surname; } }

            public int[,] Marks
            {
                get
                {
                    if (_Marks == null || _Marks.GetLength(0) < 1 || _Marks.GetLength(1) < 1) return null;
                    int[,] NMarks = new int[_Marks.GetLength(0), _Marks.GetLength(1)];
                    for (int i = 0; i < _Marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _Marks.GetLength(1); j++)
                        {
                            NMarks[i, j] = _Marks[i, j];
                        }
                    }
                    return NMarks;
                }
            }

            public Participant(string Name1, string Surname1)
            {
                _Name = Name1;
                _Surname = Surname1;
                _Marks = new int[2, 5];
                _JNumber = 0;
            }

            public int TotalScore
            {
                get
                {
                    if (_Marks == null || _Marks.GetLength(0) < 1 || _Marks.GetLength(1) < 1)
                        return 0;
                    int sum = 0;
                    for (int i = 0; i < _Marks.GetLength(0); i++)
                        for (int j = 0; j < _Marks.GetLength(1); j++)
                        {
                            sum += _Marks[i, j];
                        }
                    return sum;
                }
            }

            public void Jump(int[] result)
            {
                if (_Marks == null || _Marks.GetLength(0) == 0 || _Marks.GetLength(1) == 0 || result == null || result.Length == 0) return;
                if (_JNumber == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _Marks[0, i] = result[i];
                    }
                    _JNumber++;
                }
                else if (_JNumber == 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _Marks[1, i] = result[i];
                    }
                    _JNumber++;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array.Length < 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                            
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine();
                Console.WriteLine($"Participant: {_Name} {_Surname}");
                for (int i = 0; i < _Marks.GetLength(0); i++)
                {
                    Console.WriteLine();
                    for (int j = 0; j < _Marks.GetLength(1); j++)
                    {
                        Console.Write($"{_Marks[i, j]} ");
                    }

                }
                Console.WriteLine();
                Console.WriteLine(TotalScore);
            }
        }
    }
}