using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_4;
using static Lab_7.Blue_5;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            // поля
            private string _name;
            private int[] _scores;

            // свойства
            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] newScores = new int[_scores.Length];
                    Array.Copy(_scores, newScores, _scores.Length);
                    return newScores;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int s = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        s += _scores[i];
                    }
                    return s;
                }
            }

            // конструктор 
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            // методы
            public void PlayMatch(int result)
            {
                if (result == null) return;
                int[] newScores = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                {
                    newScores[i] = _scores[i];
                }
                newScores[newScores.Length - 1] = result;
                _scores = newScores;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {TotalScore}");
            }
        }


        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }


        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }


        public class Group
        {
            private string _name;
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;

            private int _manCount;
            private int _womanCount;

            public string Name => _name;
            public ManTeam[] ManTeams => _manTeams;
            public WomanTeam[] WomanTeams => _womanTeams;

            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _manCount = 0;
                _womanCount = 0;
            }

            public void Add(Team team)
            {
                if (team == null) return;

                if (team is ManTeam && _manCount < 12)
                {
                    _manTeams[_manCount] = (ManTeam)team;
                    _manCount++;
                }
                else if (team is WomanTeam womanTeam && _womanCount < 12)
                {
                    _womanTeams[_womanCount++] = womanTeam;
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;

                foreach (Team team in teams)
                {
                    Add(team);
                }
            }

            private static Team[] Sort(Team[] presortedteams)
            {
                Team[] sortedteams = new Team[presortedteams.Length];
                if (sortedteams == null) return null;
                for (int i = 0; i < presortedteams.Length; i++)
                {
                    sortedteams[i] = presortedteams[i];
                }
                for (int i = 0; i < sortedteams.Length; i++)
                {
                    for (int j = 0; j < sortedteams.Length - i - 1; j++)
                    {
                        if (sortedteams[j].TotalScore < sortedteams[j + 1].TotalScore)
                        {
                            Team tempteam = sortedteams[j];
                            sortedteams[j] = sortedteams[j + 1];
                            sortedteams[j + 1] = tempteam;
                        }
                    }
                }
                return sortedteams;
            }
            public void SortManWoman(Team[] _teams)
            {
                if (_teams == null) return;
                for (int i = 0; i < _teams.Length; i++)
                {
                    for (int j = 0; j < _teams.Length - i - 1; j++)
                    {
                        if (_teams[j].TotalScore < _teams[j + 1].TotalScore)
                        {
                            Team tempteam = _teams[j];
                            _teams[j] = _teams[j + 1];
                            _teams[j + 1] = tempteam;
                        }
                    }
                }
            }
            public void Sort()
            {
                SortManWoman(_manTeams);
                SortManWoman(_womanTeams);
            }
            public static Team[] Mergeteam(Team[] teams1, Team[] teams2, int size)
            {
                if (teams1 == null || teams2 == null) return null;
                Team[] nteams = new Team[size];
                teams1 = Sort(teams1);
                teams2 = Sort(teams2);
                int j = 0;
                for (int i = 0; i < size / 2; i++)
                {
                    nteams[j] = teams1[i];
                    j++;
                }
                for (int i = 0; i < size / 2; i++)
                {
                    nteams[j] = teams2[i];
                    j++;
                }
                nteams = Sort(nteams);
                return nteams;

            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group finalteam = new Group("Финалисты");
                finalteam.Add(Mergeteam(group1._manTeams, group2._manTeams, size));
                finalteam.Add(Mergeteam(group1._womanTeams, group2._womanTeams, size));
                return finalteam;
            }

            private static void AddTeamToGroup(Group group, Team team, bool isManTeam)
            {
                if (team == null) return;

                if (isManTeam && team is ManTeam manTeam)
                {
                    if (group._manCount < 12)
                    {
                        group._manTeams[group._manCount++] = manTeam;
                    }
                }
                else if (!isManTeam && team is WomanTeam womanTeam)
                {
                    if (group._womanCount < 12)
                    {
                        group._womanTeams[group._womanCount++] = womanTeam;
                    }
                }
            }

            private static void AddTeam(Group group, Team team, bool isManTeam)
            {
                if (isManTeam && team is ManTeam manTeam)
                {
                    group.Add(manTeam);
                }
                else if (!isManTeam && team is WomanTeam womanTeam)
                {
                    group.Add(womanTeam);
                }
            }

            public void Print()
            {
                Console.WriteLine($"Группа: {Name}");
                Console.WriteLine("Мужские команды:");
                for (int i = 0; i < _manCount; i++) _manTeams[i].Print();

                Console.WriteLine("\nЖенские команды:");
                for (int i = 0; i < _womanCount; i++) _womanTeams[i].Print();

                Console.WriteLine();
            }
        }
    }
}
