using System;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;
            private bool _placeSet = false;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            public void SetPlace(int place)
            {
                if (!_placeSet && place > 0)
                {
                    _place = place;
                    _placeSet = true;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} - место: {Place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            protected Sportsman[] _sportsmen; // Changed to protected
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            public int Count => _count;

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _count = 0;
            }

            public int SummaryScore
            {
                get
                {
                    int score = 0;
                    for (int i = 0; i < _count; i++)
                    {
                        switch (_sportsmen[i].Place)
                        {
                            case 1: score += 5; break;
                            case 2: score += 4; break;
                            case 3: score += 3; break;
                            case 4: score += 2; break;
                            case 5: score += 1; break;
                        }
                    }
                    return score;
                }
            }

            public int TopPlace
            {
                get
                {
                    int topPlace = int.MaxValue;
                    for (int i = 0; i < _count; i++)
                    {
                        if (_sportsmen[i].Place > 0 && _sportsmen[i].Place < topPlace)
                            topPlace = _sportsmen[i].Place;
                    }
                    return topPlace == int.MaxValue ? 0 : topPlace;
                }
            }

            public void Add(Sportsman sportsman)
            {
                if (_count < 6 && sportsman != null)
                {
                    _sportsmen[_count++] = sportsman;
                }
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;

                foreach (var s in sportsmen)
                {
                    if (_count >= 6) break;
                    Add(s);
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team champion = teams[0];
                double maxStrength = champion.GetTeamStrength();

                for (int i = 1; i < teams.Length; i++)
                {
                    double currentStrength = teams[i].GetTeamStrength();
                    if (currentStrength > maxStrength)
                    {
                        champion = teams[i];
                        maxStrength = currentStrength;
                    }
                }
                return champion;
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null) return;

                Array.Sort(teams, (t1, t2) =>
                {
                    int scoreCompare = t2.SummaryScore.CompareTo(t1.SummaryScore);
                    return scoreCompare != 0 ? scoreCompare : t1.TopPlace.CompareTo(t2.TopPlace);
                });
            }

            public virtual void Print()
            {
                Console.WriteLine($"Команда: {Name}");
                Console.WriteLine("Состав:");
                for (int i = 0; i < _count; i++)
                {
                    _sportsmen[i].Print();
                }
                Console.WriteLine($"Баллы: {SummaryScore}");
                Console.WriteLine($"Лучшее место: {TopPlace}");
                Console.WriteLine($"Сила команды: {GetTeamStrength():F2}\n");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Count == 0) return 0;

                double sum = 0;
                int validPlaces = 0;

                for (int i = 0; i < Count; i++)
                {
                    if (_sportsmen[i].Place > 0)
                    {
                        sum += _sportsmen[i].Place;
                        validPlaces++;
                    }
                }

                return validPlaces > 0 ? 100.0 / (sum / validPlaces) : 0;
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Count == 0) return 0;

                double sum = 0;
                double product = 1;
                int validPlaces = 0;

                for (int i = 0; i < Count; i++)
                {
                    if (_sportsmen[i].Place > 0)
                    {
                        sum += _sportsmen[i].Place;
                        product *= _sportsmen[i].Place;
                        validPlaces++;
                    }
                }

                return validPlaces > 0 ? (100.0 * sum * validPlaces) / product : 0;
            }
        }
    }
}