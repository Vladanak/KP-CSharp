using System;
using System.Runtime.Serialization;

namespace Server.Client
{
    [Serializable]
    [DataContract(Name = "User")]
    public class UserC
    {
        public UserC() { }
        public UserC(string userName, string email, string phone, string name, string surname)
        {
            userName = this.userName;
            email = this.email;
            phone = this.phone;
            name = this.name;
            surname = this.surname;
        }
        [DataMember(Name = "UserName")]
        public string userName { get; set; }
        [DataMember(Name="Email")]
        public string email { get; set; }
        [DataMember(Name = "Phone")]
        public string phone { get; set; }
        [DataMember(Name="Name")]
        public string name { get; set; }
        [DataMember(Name = "SurName")]
        public string surname { get; set; }
    }
}