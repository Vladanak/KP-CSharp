using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using Messager.Client;
using System.Text.RegularExpressions;

namespace Messager
{
    public class ServerWorks
    {
        public Uri TcpUri;
        public EndpointAddress Address;
        public BasicHttpBinding Binding;
        public ChannelFactory<IServerWorks> Factory;
        public IServerWorks Service;

        public ServerWorks() {
            TcpUri = new Uri("http://localhost:8000/Server");
            Address = new EndpointAddress(TcpUri);
            Binding = new BasicHttpBinding();
            Factory = new ChannelFactory<IServerWorks>(Binding, Address);
            Service = Factory.CreateChannel();
        }

        public void AddToContact(string hach, int id)
        {
            Service.AddToContact(hach,id);
        }

        public ArrayList GetMessage(string hach, int maxId)
        {
            ArrayList list = Service.GetMessage(hach, maxId);
            return list;
        }

        public string SendMessage(string hach, string userName, string message)
        {
            string s = Service.SendMessage(hach, userName, message);
            return s;
        }

        public ArrayList GetMyData(string hach)
        {
            try
            {
                var user = Service.GetMyData(hach);
                return user;
            }
            catch (Exception)
            {
                return null;
            }    
        }

        public UserC GetUserById(string hach, int id)
        {
            ArrayList user = Service.GetUserById(hach,id);
            UserC u = (UserC) Unpackage<UserC>.Upackage(user);
            return u;
        }

        public List<ArrayList> GetLastMessageUser(string hach)
        {
            try
            {
                List<ArrayList> list = Service.GetUserDialogs(hach);
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<MessageC> GetAllMessageUser(string hach, int id)
        {
            try
            {
                List<MessageC> list = Unpackage<MessageC>.Upackage(Service.GetAllUserMessage(hach, id));
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetIdByUserName(string hach, string userName)
        {
            try
            {
                int result = Service.GetIdByUserName(hach, userName);
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int GetLastIdMessage(string hach, int id)
        {
            return Service.GetLastIdMessage(Const.session, id);
        }

        public void UpdateNewMessage(string hach, int id)
        {
           Service.UpdateNewMessage(Const.session, id);
        }


        public int GetLastIdLastUser(string hach)
        {
            return Service.GetLastIdLastUser(hach);
        }

        public List<UserC> GetSearchResult(string hach, string search)
        {
            try
            {
                List<ArrayList> list = Service.GetSearchResult(hach, search);
                return Unpackage<UserC>.Upackage(list);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string SignIn(string login, string password)
        {
            try
            {
                string result = Service.SingIn(login, password);
                return result;
            }
            catch (Exception )
            {
                return "SERVER_ERROR";
            }
        }

        public string Registration(string login, string password, string email, string phone, string name, string surname)
        {
            bool match_email = Regex.IsMatch(email, "@");
            bool match_number = Regex.IsMatch(phone, "(\\+)[- _]*\\(?[- _]*(\\d{3}[- _]*\\)?([- _]*\\d){9}|\\d\\d[- _]*\\d\\d[- _]*\\)?([- _]*\\d){7})");
            bool match_password =
                Regex.IsMatch(password, "((?=.*\\d)(?=.*[a-zа-я])(?=.*[A-Zа-я])(?=.*[@#$%a-zа-я]).{6,20})");
            if (login.Contains("@") || login.Length > 15 || name.Length > 20 || phone.Length>18 || login.Equals("") ||
                login.Contains("-") || password.Length > 15 || 
                login.Contains("+") || password.Equals("") || phone.Equals("") || name.Equals("") || surname.Equals("")
                )
                return "VALID";
            if (match_email == false)
                return "VALID";
            if (match_number == false)
                return "VALID";
            if (match_password == false)
                return "VALID";
            try
            {
                string result = Service.Registration(login, password, email, phone, name, surname);
                return result;
            }
            catch (Exception)
            {
                return "SERVER_ERROR";
            }
        }

        public string ChangeSetting(string login, string password,string passwordOld,string email)
        {
            bool match_email = Regex.IsMatch(email, "@");
            bool match_PasswordNew=
                Regex.IsMatch(password, "((?=.*\\d)(?=.*[a-zа-я])(?=.*[A-Zа-я])(?=.*[@#$%a-zа-я]).{6,20})");
            bool match_PasswordOld =
                Regex.IsMatch(passwordOld, "((?=.*\\d)(?=.*[a-zа-я])(?=.*[A-Zа-я])(?=.*[@#$%a-zа-я]).{6,20})");
            if (login.Contains("@") || login.Length > 15 ||  login.Equals("") ||login.Contains("-") || password.Length > 15 ||
                login.Contains("+") || password.Equals("") || passwordOld.Equals("") || passwordOld.Length > 15)
                return "VALID";
            if (match_PasswordNew == false)
                return "VALID";
            if (match_PasswordOld == false)
                return "VALID";
            if (match_email == false)
                return "VALID";
            try
            {
                string change = Service.ChangeSetting(login, password,passwordOld,email);
                return change;
            }
            catch (Exception )
            {
                return "SERVER_ERROR";
            }
        }

        public string  RemoveHach(string userName)
        {
           string result = Service.RemoveHach(userName);
           return result;
        }
    }
}