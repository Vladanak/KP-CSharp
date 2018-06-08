using System.Data.Entity;
using Server.DataBase;

namespace Server.Context
{
    class UserContext:DbContext
    {
        public UserContext()
            : base("DConnection")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}