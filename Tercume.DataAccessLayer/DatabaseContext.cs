using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tercume.Entities;

namespace Tercume.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(): base("DatabaseContext") { }
        public DbSet<TercumeUser> TercumeUsers { get; set; }
        public DbSet<Tercuman> Tercumanlar { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<Fatura> Faturalar { get; set; }
        public DbSet<Dil> Diller { get; set; }
        public DbSet<DilTercumen> DilTercumen { get; set; }



    }
}
