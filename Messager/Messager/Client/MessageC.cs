using System.Runtime.Serialization;

namespace Messager.Client
{
    [DataContract]
    public class MessageC
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
    }
}
