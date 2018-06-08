using System.Runtime.Serialization;

namespace Server.Client
{
    class LastMessageC
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public int idUserMessage { get; set; }

        [DataMember]
        public int idSender { get; set; }
        [DataMember]
        public int idReseiver { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public string time { get; set; }
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public int CountNewMessage { get; set; }
    }
}