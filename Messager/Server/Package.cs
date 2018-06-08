using System.Collections;
using Server.Client;

namespace Server
{
    internal class Package
    {
        public static ArrayList PackageClass(object obj)
        {
            ArrayList list = new ArrayList();
            switch (obj)
            {
                case UserC c:
                    list.Add("UserC");
                    list.Add(c.email);
                    list.Add(c.name);
                    list.Add(c.phone);
                    list.Add(c.surname);
                    list.Add(c.userName);
                    return list;

                case MessageC messageC:
                    list.Add("MessageC");
                    list.Add(messageC.message);
                    list.Add(messageC.id);
                    list.Add(messageC.idReseiver);
                    list.Add(messageC.idSender);
                    list.Add(messageC.idUserMessage);
                    list.Add(messageC.status);
                    list.Add(messageC.time);
                    return list;

                case LastMessageC lastMessageC:
                    list.Add("LastMessageC");
                    list.Add(lastMessageC.message);
                    list.Add(lastMessageC.id);
                    list.Add(lastMessageC.idReseiver);
                    list.Add(lastMessageC.idSender);
                    list.Add(lastMessageC.idUserMessage);
                    list.Add(lastMessageC.status);
                    list.Add(lastMessageC.time);
                    list.Add(lastMessageC.CountNewMessage);
                    return list;
            }
            return null;
        }
    }
}