using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EFcoreExample
{
    public class AppDbContext : DbContext
    {
       
       public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string connectionString = "Data Source=.;Initial Catalog=DotNetDatabase;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
            options.UseSqlServer(connectionString);
        }
    }

   
}
