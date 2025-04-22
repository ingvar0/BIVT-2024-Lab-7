using System;
using Lab_7;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string _name;
            protected int _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public string Name => _name;
            public int Votes => _votes;

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null) { return 0; }

                int k = 0;
                foreach (Response response in responses)
                {
                    if (response.Name == _name)
                    {
                        k++;
                    }
                }
                _votes = k;
                return k;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{_name} {_votes}");
            }
        }

        public class HumanResponse : Response
        {
            private string _surname;

            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null) { return 0; }

                int k = 0;
                foreach (Response response in responses)
                {
                    if (response is HumanResponse humanResponse)
                    {
                        if (humanResponse.Name == Name && humanResponse.Surname == _surname)
                        {
                            k++;
                        }
                    }
                }
                _votes = k;
                return k;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name} {_surname} {_votes}");
            }
        }
    }
}