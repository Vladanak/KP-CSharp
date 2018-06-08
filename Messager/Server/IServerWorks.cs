using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server
{
    [ServiceContract]
    internal interface IServerWorks
    {
        [OperationContract]
        int GetLastIdLastUser(string hach);

        [OperationContract]
        void UpdateNewMessage(string hach, int id);

        [OperationContract]
        List<ArrayList> GetAllUserMessage(string hach, int idUs);

        [OperationContract]
        int GetLastIdMessage(string hach, int idUs);

        [OperationContract]
        void AddToContact(string hach, int id);

        [OperationContract]
        int GetIdByUserName(string hach, string userName);

        [OperationContract]
        List<ArrayList> GetSearchResult(string hach, string search);

        [OperationContract]
        ArrayList GetUserById(string hach, int id);

        [OperationContract]
        List<ArrayList> GetUserDialogs(string hach);

        [OperationContract]
        ArrayList GetMessage(string hach, int maxId);

        [OperationContract]
        string SendMessage(string hach, string userName, string message);

        [OperationContract]
        ArrayList GetMyData(string hach);

        [OperationContract]
        string SingIn(string login,string password);

        [OperationContract]
        string Registration(string login, string password, string email, string phone,string name, string surname);
        [OperationContract]
        string ChangeSetting(string login, string password,string passwordOld,string email);
        [OperationContract]
        string RemoveHach(string userName);
    }
}