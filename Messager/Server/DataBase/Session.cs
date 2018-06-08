using System.ComponentModel.DataAnnotations;

namespace Server.DataBase
{
    class Session
    {
        [Key]
        public int id { get; set; }
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string sessionHach { get; set; }
        [Required]
        public int idUser { get; set; }
        public User User { get; set; }
    }
}