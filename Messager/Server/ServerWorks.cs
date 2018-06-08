using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Server.Client;
using Server.Context;
using Server.DataBase;

namespace Server
{
    class ServerWorks : IServerWorks
    {
        ServerWorksHelper swh = new ServerWorksHelper();
        public void AddToContact(string hach, int id)
        {
            int myId = swh.GetIdUserByHach(hach);
            if (swh.IsUserContact(myId,id))
            {
                using (UserContext db1 = new UserContext())
                {
                    Contact c = new Contact
                    {
                        idUser = myId,
                        idUserContact = id
                    };
                    db1.Contacts.Add(c);
                    db1.SaveChanges();
                }
            }
        }

        public ArrayList GetUserById(string hach, int id)
        {
            ArrayList al = new ArrayList();
            int myId = swh.GetIdUserByHach(hach);
            using (UserContext db1 = new UserContext())
            {
                foreach (var u in db1.Users)
                {
                    if (u.idUser == id)
                    {
                        UserC user = new UserC
                        {
                            email = u.email,
                            name = u.name,
                            phone = u.phone,
                            surname = u.surname,
                            userName = u.userName
                        };
                        return Package.PackageClass(user);
                    }
                }
            }
            return null;
        }

        public int GetLastIdMessage(string hach, int idUs)
        {
            List<MessageC> list = new List<MessageC>();
            int id = swh.GetIdUserByHach(hach);
            using (UserContext db1 = new UserContext())
            {
                foreach (var m in db1.Messages)
                {
                    if ((m.idUserMessage == id && m.idReseiver == idUs) || (m.idUserMessage == id && m.idSender == idUs))
                    {
                        MessageC message = new MessageC
                        {
                            message = m.message,
                            id = m.id,
                            idReseiver = m.idReseiver,
                            idSender = m.idSender,
                            idUserMessage = m.idUserMessage,
                            status = m.status,
                            time = m.time
                        };
                        list.Add(message);
                    }
                }
            }
            return ServerWorksHelper.GetMessageLastId(list);
        }


        public int GetLastIdLastUser(string hach)
        {
            int max = -1;
            List<ArrayList> list = new List<ArrayList>();
            int id = swh.GetIdUserByHach(hach);
                        using (UserContext db1 = new UserContext())
                        {
                            foreach (var c in db1.Contacts)
                            {
                                if (c.idUser == id && c.idUserContact != id)
                                {
                                    LastMessageC message = swh.GetLastUserMessage(id, c.idUserContact);
                                    if (message!=null)
                                    {
                                        if (message.id>max)
                                        {
                                            max = message.id;
                                        }
                                    }
                                }
                                else if (c.idUserContact == id && c.idUser != id)
                                {
                                    LastMessageC message = swh.GetLastUserMessage(id, c.idUser);
                                    if (message != null)
                                    {
                                        if (message.id > max)
                                        {
                                            max = message.id;
                                        }
                                    }
                                }
                            }
                        }
             
            return max;
        }


        public int GetIdByUserName(string hach, string userName)
        {
            using (UserContext db1 = new UserContext())
            {
                foreach (var u in db1.Users)
                {
                    if (u.userName == userName)
                    {             
                        return u.idUser;
                    }
                }
            }
            return -1;
        }

        public List<ArrayList> GetAllUserMessage(string hach, int idUs)
        {
            List<ArrayList> list = new List<ArrayList>();
            int id = swh.GetIdUserByHach(hach);
            using (UserContext db1 = new UserContext())
            {
                foreach (var m in db1.Messages)
                {
                    if ((m.idUserMessage==id&&m.idReseiver==idUs)||(m.idUserMessage==id&&m.idSender==idUs))
                    {
                        MessageC message = new MessageC
                        {
                            message = m.message,
                            id = m.id,
                            idReseiver = m.idReseiver,
                            idSender = m.idSender,
                            idUserMessage = m.idUserMessage,
                            status = m.status,
                            time = m.time
                        };
                        list.Add(Package.PackageClass(message));
                    }
                }
            }
            return list;
        }

        public List<ArrayList> GetSearchResult(string hach, string search)
        {
            int myId = swh.GetIdUserByHach(hach);
            List<ArrayList> list = new List<ArrayList>();
            switch (search[0])
            {
                case '+':
                {
                    using (UserContext db1 = new UserContext())
                    {
                        foreach (var u in db1.Users)
                        {
                            if (u.phone.Contains(search) && u.idUser != myId)
                            {
                                UserC user = new UserC
                                {
                                    email = u.email,
                                    name = u.name,
                                    phone = u.phone,
                                    surname = u.surname,
                                    userName = u.userName
                                };
                                list.Add(Package.PackageClass(user));
                            }
                        }
                    }
                    return list;
                }
                case '@':
                {
                    search=search.Remove(0, 1);
                    using (UserContext db1 = new UserContext())
                    {
                        foreach (var u in db1.Users)
                        {
                            if (u.userName.Contains(search) && u.idUser != myId)
                            {
                                UserC user = new UserC
                                {
                                    email = u.email,
                                    name = u.name,
                                    phone = u.phone,
                                    surname = u.surname,
                                    userName = u.userName
                                };
                                list.Add(Package.PackageClass(user));
                            }
                        }
                    }
                    return list;
                }
            }
            return null;
        }

        public List<ArrayList> GetUserDialogs(string hach)
        {
            List<ArrayList> list = new List<ArrayList>();
            int id = swh.GetIdUserByHach(hach);
                        using (UserContext db1 = new UserContext())
                        {
                            foreach (var c in db1.Contacts)
                            {
                                if (c.idUser == id && c.idUserContact != id)
                                {
                                    LastMessageC message = swh.GetLastUserMessage(id, c.idUserContact);
                                    if (message!=null)
                                    {
                                        list.Add(Package.PackageClass(message));
                                    }
                                }
                                else if (c.idUserContact == id && c.idUser != id)
                                {
                                    LastMessageC message = swh.GetLastUserMessage(id, c.idUser);
                                    if (message != null)
                                    {
                                        list.Add(Package.PackageClass(message));
                                    }
                                }
                            }
                        }
           return list;
        }

        public void UpdateNewMessage(string hach, int id)
        {
            using (UserContext db1 = new UserContext())
            {
                foreach (var m in db1.Messages)
                {
                    if (m.idSender.Equals(id) && m.status == 1)
                    {
                        Message mess = m;
                        mess.status = 0;
                    }
                }
                db1.SaveChanges();
            }
        }

        public ArrayList GetMessage(string hach, int maxId)
        {
            int id = swh.GetIdUserByHach(hach);
            ArrayList list = new ArrayList();
            using (UserContext db1 = new UserContext())
            {
                foreach (var m in db1.Messages)
                {
                    if (m.idUserMessage == id && m.id > maxId)
                    {
                        MessageC message = new MessageC
                        {
                            id = m.id,
                            idReseiver = m.idReseiver,
                            idSender = m.idSender,
                            idUserMessage = m.idUserMessage,
                            message = m.message,
                            status = m.status,
                            time = m.time
                        };
                        list.Add(message);
                    }
                }
            }
            return list;
        }

        public string SendMessage(string hach, string userName, string message)
        {
            int id = swh.GetIdUserByHach(hach);
            using (UserContext db1 = new UserContext())
            {
                foreach (var u in db1.Users)
                {
                    if (userName.Equals(u.userName))
                    {
                        using (UserContext db2 = new UserContext())
                        {
                            Message m = new Message
                            {
                                idReseiver = u.idUser,
                                idSender = id,
                                idUserMessage = id,
                                message = message,
                                status = 0,
                                time = DateTime.Now.ToString()
                            };
                            Message m1 = new Message
                            {
                                idReseiver = u.idUser,
                                idSender = id,
                                idUserMessage = u.idUser,
                                message = message,
                                status = 1,
                                time = DateTime.Now.ToString()
                            };
                            db2.Messages.Add(m);
                            db2.Messages.Add(m1);
                            db2.SaveChanges();
                        }
                    }
                }
            }
            return "CANCEL";
        }

        public ArrayList GetMyData(string hach)
        {
            int id = swh.GetIdUserByHach(hach);
            if (id == -1)
            {
                return null;
            }
            using (UserContext db1 = new UserContext())
            {
                foreach (var u in db1.Users)
                {
                    if (u.idUser == id)
                    {
                        UserC user = new UserC
                        {
                            email = u.email,
                            name = u.name,
                            phone = u.phone,
                            surname = u.surname,
                            userName = u.userName
                        };
                        return Package.PackageClass(user);
                    }
                }
            }
            return null;
        }

        public string SingIn(string login, string password)
        {
            using (UserContext db = new UserContext())
            {
                foreach (var u in db.Users)
                {
                    if (u.userName.Equals(login))
                    {
                        if (u.userPassword.Equals(password))
                        {
                            RandomKey key = new RandomKey();
                            string k = key.GetRandomKey(50);
                            using (UserContext db1 = new UserContext())
                            {
                                Session s = new Session
                                {
                                    sessionHach = k,
                                    idUser = u.idUser
                                };
                                db1.Sessions.Add(s);
                                db1.SaveChanges();
                            }
                            return k;
                        }
                    }
                }
            }
            return "CANCEL";
        }

        public string Registration(string login, string password, string email, string phone, string name, string surname)
        {
            bool match_email = Regex.IsMatch(email, "@");
            bool match_number = Regex.IsMatch(phone, "(\\+)[- _]*\\(?[- _]*(\\d{3}[- _]*\\)?([- _]*\\d){9}|\\d\\d[- _]*\\d\\d[- _]*\\)?([- _]*\\d){7})");
            bool match_password =
                Regex.IsMatch(password, "((?=.*\\d)(?=.*[a-zа-я])(?=.*[A-Zа-я])(?=.*[@#$%a-zа-я]).{6,20})");
            if (login.Contains("@") || login.Length > 15 || name.Length > 20 || phone.Length > 18 || login.Equals("") ||
                login.Contains("-") || password.Length > 15 ||
                login.Contains("+") || password.Equals("") || phone.Equals("") || name.Equals("") || surname.Equals("")
               )
                return "VALID";
            if (match_email == false)
                return "VALIDEmail";
            if (match_number == false)
                return "VALIDNumber";
            if (match_password == false)
                return "VALIDPass";
            try
            {
                using (UserContext db = new UserContext())
                {
                    foreach (var us in db.Users)
                    {
                        if (us.userName.Equals(login))
                            return "TRUE1";
                        if (us.email.Equals(email))
                            return "TRUE2";
                        if (us.phone.Equals(phone))
                            return "TRUE3";
                    }
                    User u = new User
                    {
                        userName = login,
                        userPassword = password,
                        email = email,
                        name = name,
                        phone = phone,
                        surname = surname
                    };
                    db.Users.Add(u);
                    db.SaveChanges();
                } 
            }
            catch (Exception)
            {
                return "CANCEL";
            }
            return "OK";
        }
        public string ChangeSetting(string login, string password,string passwordOld,string email)
        {
            bool match_PasswordNew =
                Regex.IsMatch(password, "((?=.*\\d)(?=.*[a-zа-я])(?=.*[A-Zа-я])(?=.*[@#$%a-zа-я]).{6,20})");
            bool match_PasswordOld =
                Regex.IsMatch(passwordOld, "((?=.*\\d)(?=.*[a-zа-я])(?=.*[A-Zа-я])(?=.*[@#$%a-zа-я]).{6,20})");
            bool match_email = Regex.IsMatch(email, "@");
            if (login.Contains("@") || login.Length > 15 || login.Equals("") || login.Contains("-") || password.Length > 15 ||
                login.Contains("+") || password.Equals("") || passwordOld.Equals("") || passwordOld.Length > 15)
                return "VALID";
            if (match_PasswordNew == false)
                return "VALID";
            if (match_PasswordOld == false)
                return "VALID";
            if (match_email == false)
                return "VALID";
            using (UserContext db = new UserContext())
            {
                foreach (var u in db.Users)
                {
                    if (!u.userName.Equals(login))
                    {
                        if (!u.userName.Equals(email))
                        {
                            if (u.userPassword.Equals(password))
                                return "TRUE";
                            if (u.userPassword.Equals(passwordOld))
                            {
                                u.userName = login;
                                u.userPassword = password;
                                u.email = email;
                            }
                            else
                            {
                                return "FALSE";
                            }
                        }
                        else
                        {
                            return "EMAIL";
                        }
                    }
                    else
                    {
                        return "NAME";
                    }
                }
                db.SaveChanges();
                return "OK";
            }
        }

        public string RemoveHach(string userName)
        {
            int id = 0;
            using (UserContext db = new UserContext())
            {
                foreach (var u in db.Users)
                {
                    if (u.userName.Equals(userName))
                    {
                        id = u.idUser;
                    }
                }
            }
            using (UserContext db1 = new UserContext())
            {
                foreach (var s in db1.Sessions)
                {
                    if (s.idUser.Equals(id))
                    {
                        db1.Sessions.Remove(s);
                    }
                }
                db1.SaveChanges();
            }
            return "OK";
        }
    }
}