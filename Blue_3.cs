using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_3;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltyTimes;

            public string Name => _name;
            public string Surname => _surname;
            public virtual int[] Penalties
            {
                get
                {
                    if (_penaltyTimes == null) return null;
                    int[] _newpenaltytimes = new int[_penaltyTimes.Length];
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        _newpenaltytimes[i] = _penaltyTimes[i];
                    }
                    return _newpenaltytimes;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];
            }

            public virtual int Total
            {
                get
                {
                    if (_penaltyTimes == null) return 0;
                    int s = 0;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        s += _penaltyTimes[i];
                    }
                    return s;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public virtual void PlayMatch(int time)
            {
                if (_penaltyTimes == null) return;
                Array.Resize(ref _penaltyTimes, _penaltyTimes.Length + 1);
                _penaltyTimes[_penaltyTimes.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length < 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
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
                Console.WriteLine($"{_name} {_surname} {Total} {IsExpelled}");
            }
        }


        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;

                    int countFiveFoul = 0;
                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 5) countFiveFoul++;
                    }

                    bool tooManyFiveFouls = countFiveFoul > (_penaltyTimes.Length * 0.1);
                    bool totalFoulsTooHigh = Total > (2 * _penaltyTimes.Length);

                    return tooManyFiveFouls || totalFoulsTooHigh;
                }
            }

            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 5) return;

                base.PlayMatch(fouls);
            }

        }


        public class HockeyPlayer : Participant
        {
            private static int totalCount = 0;
            private static double totalPenaltyTime = 0;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                totalCount += 1;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltyTimes == null || _penaltyTimes.Length == 0) return false;

                    for (int i = 0; i < _penaltyTimes.Length; i++)
                    {
                        if (_penaltyTimes[i] == 10) return true;
                    }

                    if (totalCount == 0) return false;
                    double averagePenaltyTime = totalPenaltyTime / totalCount;
                    return Total > (0.1 * averagePenaltyTime);
                }
            }

            public override void PlayMatch(int time)
            {
                if (time < 0 && time != 0 && time != 2 && time != 5 && time != 10) return; 

                totalPenaltyTime += time;

                base.PlayMatch(time);
            }
        }
    }
}
