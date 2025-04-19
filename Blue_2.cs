using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class Blue_2
    {
        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;


            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;


            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = [];
            }


            public abstract double[] Prize { get; }


            public void Add(Participant p)
            {
                if (_participants == null) return;
                Participant[] new_p = new Participant[_participants.Length + 1];
                Array.Copy(_participants, new_p, _participants.Length);
                new_p[_participants.Length] = p;
                _participants = new_p;
            }


            public void Add(Participant[] p)
            {
                if (p == null || p.Length < 1 || _participants == null) return;
                int index = 0;
                Participant[] new_p = new Participant[_participants.Length + p.Length];
                Array.Copy(_participants, new_p, _participants.Length);
                for (int i = _participants.Length; i < new_p.Length; i++)
                {
                    new_p[i] = p[index];
                    index++;
                }
                _participants = new_p;
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
                    prizes[0] = Bank * 0.5;
                    prizes[1] = Bank * 0.3;
                    prizes[2] = Bank * 0.2;
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

                    Participant.Sort(Participants);

                    double[] prizes = new double[Participants.Length];
                    prizes[0] = Bank * 0.4;
                    prizes[1] = Bank * 0.25;
                    prizes[2] = Bank * 0.15;

                    int countUpMiddle = Participants.Length / 2;
                    if (countUpMiddle < 3 || countUpMiddle > 10) return prizes;

                    double remainingPrize = Bank * 0.2; // 20% от банка
                    double prizePerPerson = remainingPrize / countUpMiddle;

                    for (int i = 0; i < countUpMiddle; i++)
                    {
                        prizes[i] += prizePerPerson;
                    }
                    return prizes;
                }
            }
        }


        public struct Participant
        {
            private readonly string _name;
            private readonly string _surname;
            private readonly int[,] marks;
            private int jumpNumber;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                marks = new int[2, 5];
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        marks[i, j] = 0;
                    }
                }
                jumpNumber = 0;
            }

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (marks == null)
                    {
                        return null;
                    }

                    int[,] copy = new int[marks.GetLength(0), marks.GetLength(1)];
                    Array.Copy(marks, copy, marks.Length);
                    return copy;
                }
            }

            public void Jump(int[] results)
            {
                if (results == null || results.Length < marks.GetLength(1)) return;
                {
                    for (int i = 0; i < marks.GetLength(1); ++i)
                    {
                        marks[jumpNumber, i] = results[i];
                    }
                }
                jumpNumber += 1;
                if (jumpNumber == 2) jumpNumber = 0;
            }

            public int TotalScore
            {
                get
                {
                    if (marks == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < marks.GetLength(1); j++)
                        {
                            sum += marks[i, j];
                        }
                    }
                    return sum;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;

                for (int i = 0; i < array.Length - 1; i++)
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
                Console.WriteLine($"{_name} {_surname} {TotalScore}");
            }
        }
    }
}
