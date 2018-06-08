using System.ComponentModel.DataAnnotations;

namespace Server.DataBase
{
    class Message
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idUserMessage { get; set; }
        [Required]
        public int idSender { get; set; }
        [Required]
        public int idReseiver{ get; set; }
        [StringLength(200, MinimumLength = 1)]
        [Required]
        public string message { get; set; }
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string time { get; set; }
        [Required]
        public int status { get; set; }
        public User User { get; set; }
    }
}