using Microsoft.EntityFrameworkCore;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DAL
{
    public class ContextDev : DbContext
    {
        public ContextDev(DbContextOptions<ContextDev> opts) : base(opts) {}
        public DbSet<UserDB> Users { get; set; }
        public DbSet<FileDB> Files { get; set; }
    }
}
