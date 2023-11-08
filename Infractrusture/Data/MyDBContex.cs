using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Threading.Tasks;
using CleanTask.Domains;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Entitties;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data
{
    public class MyDBContex : IdentityDbContext<CleanTask.Domains.userapp>
    {
        public MyDBContex(DbContextOptions<MyDBContex> options) : base(options)
        {

        }
        public DbSet<CleanTask.Domains.productModel> product { get; set; }
        public DbSet<CleanTask.Domains.UserMe> userme { get; set; }
    }
    public class BloggingContextFactory:IDesignTimeDbContextFactory<MyDBContex>
    {
        public MyDBContex CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDBContex>();
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-MFT5R3I;Database=CleanTas;user id= YOURSQLUSERID;password=YOURSQLPASSWORD;integrated security=true; TrustServerCertificate = True;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new MyDBContex(optionsBuilder.Options);
        }
    }
}
