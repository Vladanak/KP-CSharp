using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.DataBase
{
    class User
    {
        [Key]
        public int idUser { get; set; }
        [StringLength(15,MinimumLength = 1)]
        [Required]
        public string userName { get; set; }
        [StringLength(15,MinimumLength = 6)]
        [Required]
        public string userPassword { get; set; }
        [StringLength(30, MinimumLength = 1)]
        [Required]
        public string email { get; set; }
        [StringLength(25, MinimumLength = 10)]
        [Required]
        public string phone { get; set; }
        [StringLength(25, MinimumLength = 1)]
        [Required]
        public string name { get; set; }
        [StringLength(30, MinimumLength = 1)]
        [Required]
        public string surname { get; set; }
        public List<Session> Sessions { get; set; }
        public List<Message> Messages { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}