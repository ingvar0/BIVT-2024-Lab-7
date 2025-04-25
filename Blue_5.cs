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
            private bool _placeSet;


            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int Place { get { return _place; } }
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
                _placeSet = false;
            }
            public void SetPlace(int place)
            {
                if (_place != 0) return;
                _place = place;
                _placeSet = true;
            }
            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;

            public string Name { get { return _name; } }
            public Sportsman[] Sportsmen { get { return _sportsmen; } }

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
                    if (_sportsmen == null || _count == 0)
                        return 0;

                    int Score = 0;
                    for (int i = 0; i < _count; i++)
                    {
                        int place = _sportsmen[i].Place;

                        if (place == 1)
                        {
                            Score += 5;
                        }
                        else if (place == 2)
                        {
                            Score += 4;
                        }
                        else if (place == 3)
                        {
                            Score += 3;
                        }
                        else if (place == 4)
                        {
                            Score += 2;
                        }
                        else if (place == 5)
                        {
                            Score += 1;
                        }

                    }
                    return Score;
                }
            }
            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null)
                        return 0;

                    int TPlace = 18;
                    for (int i = 0; i < _count; i++)
                    {
                        if (_sportsmen[i].Place < TPlace)
                        {
                            TPlace = _sportsmen[i].Place;
                        }
                    }
                    return TPlace;
                }
            }
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null)
                {
                    _sportsmen = new Sportsman[6];
                }
                if (_count < 6)
                {
                    _sportsmen[_count] = sportsman;
                    _count++;
                }
            }
            public void Add(Sportsman[] sportsmen)
            {
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }
            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            if (teams[j].TopPlace > teams[j + 1].TopPlace)
                            {
                                Team temp = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = temp;
                            }
                        }
                    }
                }

            }
            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                if (teams == null) return null;
                double Maxstrenght = double.MinValue;
                Team Chempion = null;
                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i] == null) continue;
                    double nstrength = teams[i].GetTeamStrength();
                    if (nstrength > Maxstrenght)
                    {
                        Maxstrenght = nstrength;
                        Chempion = teams[i];
                    }
                }
                return Chempion;
            }
            public void Print()
            {
                Console.WriteLine($"{_name}: {SummaryScore}  {TopPlace}");
            }
        }
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }
            protected override double GetTeamStrength()
            {
                double Sumplace = 0;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    Sumplace += Sportsmen[i].Place;
                }
                double strength = 100 / (Sumplace / Sportsmen.Length);
                return strength;
            }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {
            }
            protected override double GetTeamStrength()
            {

                double Sumplace = 0;
                double Multplace = 1;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    Sumplace += Sportsmen[i].Place;
                    Multplace *= Sportsmen[i].Place;
                }
                double strength = 100 * ((Sumplace * Sportsmen.Length) / Multplace);
                return strength;
            }
        }
    }
}