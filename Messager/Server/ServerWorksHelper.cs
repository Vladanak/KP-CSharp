using System;
using System.Collections.Generic;
using System.Linq;
using Server.Client;
using Server.Context;

namespace Server
{
    class ServerWorksHelper
    {
        public bool IsUserContact(int id, int lastId)
        {
            using (UserContext db1 = new UserContext())
            {
                if (Enumerable.Any(db1.Contacts, c => (c.idUser==id && c.idUserContact == lastId) || (c.idUser == lastId && c.idUserContact == id)))
                {
                    return false;
                }
            }
            return true;
        }

        public LastMessageC GetLastUserMessage(int idUser, int idLastUser)
        {
            DateTime max = new DateTime(1970, 1, 1, 0, 0, 0);
            LastMessageC message = null;
            int CountNewMessage = 0;
            using (UserContext db = new UserContext())
            {
                foreach (var m in db.Messages)
                {
                    if ((m.idUserMessage == idUser) && (m.idReseiver == idLastUser || m.idSender == idLastUser))
                    {
                        if (DateTime.Parse(m.time) > max)
                        {
                            message = new LastMessageC();
                            max = DateTime.Parse(m.time);
                            message.message = m.message;
                            message.id = m.id;
                            message.idReseiver = m.idReseiver;
                            message.idSender = m.idSender;
                            message.idUserMessage = m.idUserMessage;
                            message.status = m.status;
                            message.time = m.time;
                           
                        }
                        if (message != null && message.status == 1)
                        {
                            CountNewMessage++;
                        }
                    }
                }
            }
            if(message!=null)
                message.CountNewMessage = CountNewMessage;
            return message;
        }

        public static int GetMessageLastId(List<MessageC> list)
        {
            int max = -1;
            foreach (var m in list)
            {
                if (m.id > max)
                {
                    max = m.id;
                }
            }
            return max;
        }

        public int GetIdUserByHach(string hach)
        {
            using (UserContext db = new UserContext())
            {
                foreach (var s in db.Sessions)
                {
                    if (hach.Equals(s.sessionHach))
                    {
                        return s.idUser;
                    }
                }
            }
            return -1;
        }
    }
}