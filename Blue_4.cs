using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
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


        public class ManTeam: Team
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

                for (int i = 0; i < _manCount - 1; i++)
                {
                    for (int j = 0; j < _manCount - i - 1; j++)
                    {
                        if (_manTeams[j].TotalScore < _manTeams[j + 1].TotalScore)
                        {
                            ManTeam dop1= _manTeams[j];
                            _manTeams[j] = _manTeams[j + 1];
                            _manTeams[j] = dop1;
                        }
                    }
                }

                for (int i = 0; i < _womanCount - 1; i++)
                {
                    for (int j = 0; j < _womanCount - i - 1; j++)
                    {
                        if (_womanTeams[j].TotalScore < _womanTeams[j + 1].TotalScore)
                        {
                            WomanTeam dop2 = _womanTeams[j];
                            _womanTeams[j] = _womanTeams[j + 1];
                            _womanTeams[j] = dop2;
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group finalGroup = new Group("Финалисты");

                int manSize = size / 2;
                MergeTeamArraysIntoGroup(
                    group1.ManTeams, group1.ManTeams.Length,
                    group2.ManTeams, group2.ManTeams.Length,
                    manSize, finalGroup, isManTeam: true);

                int womanSize = size / 2;
                MergeTeamArraysIntoGroup(
                    group1.WomanTeams, group1.WomanTeams.Length,
                    group2.WomanTeams, group2.WomanTeams.Length,
                    womanSize, finalGroup, isManTeam: false);

                return finalGroup;
            }

            private static void MergeTeamArraysIntoGroup(
                Team[] arr1, int count1,
                Team[] arr2, int count2,
                int size, Group outputGroup, bool isManTeam)
            {
                int i = 0, j = 0, added = 0;

                while (added < size && i < count1 && j < count2)
                {
                    if (arr1[i].TotalScore >= arr2[j].TotalScore)
                    {
                        AddTeamToGroup(outputGroup, arr1[i++], isManTeam);
                    }
                    else
                    {
                        AddTeamToGroup(outputGroup, arr2[j++], isManTeam);
                    }
                    added++;
                }

                while (added < size && i < count1)
                {
                    AddTeamToGroup(outputGroup, arr1[i++], isManTeam);
                    added++;
                }

                while (added < size && j < count2)
                {
                    AddTeamToGroup(outputGroup, arr2[j++], isManTeam);
                    added++;
                }
            }

            private static void AddTeamToGroup(Group group, Team team, bool isManTeam)
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
