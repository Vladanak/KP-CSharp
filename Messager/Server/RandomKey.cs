using System;
namespace Server
{
    class RandomKey
    {
        private readonly Random _rnd;
        public RandomKey()
        {
            _rnd  = new Random();
        }
        private readonly string[] _mass = {"q","w","e","r", "t", "y", "u", "i" , "o", "p", "a", "s" , "d", "f", "g", "h" , "j", "k", "l", "z" , "x", "c", "v", "b" , "n", "m", "1", "2" , "3", "4", "5", "6", "7", "8", "9" };
        public string GetRandomKey(int l)
        {
            string key = "";
            for (int i=0;i<l;i++)
            {
                int number = RandomNumberGenerator();
                if (number <= 25)
                {
                    key += LowerOrUpperCase(_mass[number]);
                }
                else
                {
                    key += _mass[number];
                }
            }
            return key;
        }

        private int RandomNumberGenerator()
        {
            int value = _rnd.Next(0, 34);
            return value;
        }

        private string LowerOrUpperCase(string value)
        {
            int number = _rnd.Next(0, 2);
            return number == 1 ? value.ToUpper() : value;
        }
    }
}