using System;
using System.Collections.Generic;
using System.Windows.Media;
using Messager.Client;

namespace Messager
{
    class ServerWorksHelper
    {
        private readonly Random _rnd = new Random();
        public int GetIdSecondUser(LastMessageC message)
        {
            return message.idUserMessage == message.idSender ? message.idReseiver : message.idSender;
        }

        public SolidColorBrush RandomColor()
        {
            int k = _rnd.Next(0, 10);
            switch (k)
            {
                case 0:
                {
                    return Brushes.BlueViolet;
                }
                case 1:
                {
                    return Brushes.Aquamarine;
                }
                case 2:
                {
                    return Brushes.Chocolate;
                }
                case 3:
                {
                    return Brushes.Chartreuse;
                }
                case 4:
                {
                    return Brushes.DeepPink;
                }
                case 5:
                {
                    return Brushes.Salmon;
                }
                case 6:
                {
                    return Brushes.YellowGreen;
                }
                case 7:
                {
                    return Brushes.DarkCyan;
                }
                case 8:
                {
                    return Brushes.OrangeRed;
                }
                case 9:
                {
                    return Brushes.IndianRed;
                }
            }
            return null;
        }

        public static void GetMessageLastId(List<MessageC> list)
        {
            int max = -1;
            foreach (var m in list)
            {
                if (m.id>max)
                {
                    max = m.id;
                }
            }
            Const.LastId = max;
        }
    }
}