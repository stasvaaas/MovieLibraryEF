using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieLibraryEF
{
    internal class AppDbConext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public AppDbConext()
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=movies.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
