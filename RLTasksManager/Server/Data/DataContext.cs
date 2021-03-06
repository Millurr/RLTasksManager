using Microsoft.EntityFrameworkCore;
using RLTasksManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// dotnet ef migrations add {name}
// dotnet ef database update

namespace RLTasksManager.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Site> Sites { get; set; }
    }
}
