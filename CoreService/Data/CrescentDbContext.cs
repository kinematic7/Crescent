using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace CoreService.Data
{
    public class CrescentDbContext : DbContext
    {
        public CrescentDbContext()
          : base(Settings.ConnectionString)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
