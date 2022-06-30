using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using My_Characters.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.Context
{
    internal class ApplicationContext : DbContext
    {
        private string _connetionString;
        public DbSet<BiographyModel> Biographies { get; set; } = null!;
        public DbSet<ProgressModel> Progresses { get; set; } = null!;
        public DbSet<SourceFileModel> SourceFiles { get; set; } = null!;
        public DbSet<ReferenceModel> References { get; set; } = null!;
        public DbSet<RenderModel> Renders { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
           
        }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("ConnectionString.json");

            var config = builder.Build();

            _connetionString = config.GetConnectionString("DefaultConnection");
            return _connetionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }
}
