using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_4;

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

            public void Sort()
            {
                if (_manTeams == null || _womanTeams == null) return;

                void SortTeamArray<T>(T[] teams, int count) where T : Team
                {
                    for (int i = 0; i < count - 1; i++)
                    {
                        for (int j = 0; j < count - i - 1; j++)
                        {
                            if (teams[j].TotalScore < teams[j + 1].TotalScore)
                            {
                                T temp = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = temp;
                            }
                        }
                    }
                }

                SortTeamArray(_manTeams, _manCount);
                SortTeamArray(_womanTeams, _womanCount);
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                if (group1 == null || group2 == null || size <= 0)
                    return new Group("Финалисты");

                Group finalGroup = new Group("Финалисты");

                group1.Sort();
                group2.Sort();

                MergeTeamArrays(group1.ManTeams, group1._manCount,
                               group2.ManTeams, group2._manCount,
                               size / 2, finalGroup, isManTeam: true);

                MergeTeamArrays(group1.WomanTeams, group1._womanCount,
                               group2.WomanTeams, group2._womanCount,
                               size / 2, finalGroup, isManTeam: false);

                return finalGroup;
            }

            private static void MergeTeamArrays(Team[] teams1, int count1,
                                              Team[] teams2, int count2,
                                              int size, Group outputGroup, bool isManTeam)
            {
                int i = 0, j = 0, added = 0;

                while (added < size && i < count1 && j < count2)
                {
                    if (teams1[i] != null && teams2[j] != null)
                    {
                        if (teams1[i].TotalScore >= teams2[j].TotalScore)
                        {
                            AddTeamToGroup(outputGroup, teams1[i], isManTeam);
                            i++;
                        }
                        else
                        {
                            AddTeamToGroup(outputGroup, teams2[j], isManTeam);
                            j++;
                        }
                        added++;
                    }
                    else
                    {
                        if (teams1[i] == null) i++;
                        if (teams2[j] == null) j++;
                    }
                }

                while (added < size && i < count1)
                {
                    if (teams1[i] != null)
                    {
                        AddTeamToGroup(outputGroup, teams1[i], isManTeam);
                        added++;
                    }
                    i++;
                }

                while (added < size && j < count2)
                {
                    if (teams2[j] != null)
                    {
                        AddTeamToGroup(outputGroup, teams2[j], isManTeam);
                        added++;
                    }
                    j++;
                }
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
