using System.ComponentModel.DataAnnotations;

namespace Server.DataBase
{
    class Contact
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idUser { get; set; }
        [Required]
        public int idUserContact { get; set; }
        public User User { get; set; }
    }
}