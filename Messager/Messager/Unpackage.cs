using System.Collections;
using System.Collections.Generic;
using Messager.Client;

namespace Messager
{
    class Unpackage<T>
    {
        public static List<T> Upackage(List<ArrayList> list)
        {
            if (list != null && list.Count != 0)
            {
                switch ((string) list[0][0])
                {
                    case "UserC":
                    {
                        List<T> listU = new List<T>();
                        foreach (ArrayList arrayList in list)
                        {
                            listU.Add((T) Upackage(arrayList));
                        }
                        return listU;
                    }
                    case "MessageC":
                    {
                        List<T> listU = new List<T>();
                        foreach (ArrayList arrayList in list)
                        {
                            listU.Add((T) Upackage(arrayList));
                        }
                        return listU;
                    }
                    case "LastMessageC":
                    {
                        List<T> listU = new List<T>();
                        foreach (ArrayList arrayList in list)
                        {
                            listU.Add((T)Upackage(arrayList));
                        }
                        return listU;
                    }
                    default: return null;
                }
            }
            return null;
        }

        public static object Upackage(ArrayList list)
        {
            if (list != null)
            {
                switch ((string) list[0])
                {
                    case "UserC":
                    {
                        UserC user = new UserC
                        {
                            email = (string) list[1],
                            name = (string) list[2],
                            phone = (string) list[3],
                            surname = (string) list[4],
                            userName = (string) list[5]
                        };
                        return user;
                    }
                    case "MessageC":
                    {
                        MessageC message = new MessageC
                        {
                            message = (string) list[1],
                            id = (int) list[2],
                            idReseiver = (int) list[3],
                            idSender = (int) list[4],
                            idUserMessage = (int) list[5],
                            status = (int) list[6],
                            time = (string) list[7]
                        };
                        return message;
                    }
                    case "LastMessageC":
                    {
                        LastMessageC message = new LastMessageC
                        {
                            message = (string) list[1],
                            id = (int) list[2],
                            idReseiver = (int) list[3],
                            idSender = (int) list[4],
                            idUserMessage = (int) list[5],
                            status = (int) list[6],
                            time = (string) list[7],
                            CountNewMessage = (int) list[8]
                        };
                        return message;
                    }
                    default: return null;
                }
            }
            return null;
        }
    }
}