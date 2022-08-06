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
        private string? _connetionString;
        public DbSet<BiographyModel> Biographies => Set<BiographyModel>();
        public DbSet<ToDoListModel> ToDoLists => Set<ToDoListModel>();
        public DbSet<SourceFileModel> SourceFiles => Set<SourceFileModel>();
        public DbSet<ReferenceModel> References => Set<ReferenceModel>();
        public DbSet<RenderModel> Renders => Set<RenderModel>();

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
