using Microsoft.EntityFrameworkCore;
using Normas.Api.Data.Configuration;
using Normas.Api.Models;
//using Normas.Api.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Normas.Api.Data
{
    public class ApplicationDbContext: DbContext 
     {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {

        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Normas");
            ModelConfig(modelBuilder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new NormasConfiguration(modelBuilder.Entity<Norma>());
            new TipoRequisitoConfiguration(modelBuilder.Entity<TipoRequisito>());
        }

        public DbSet <Norma> Normas { get; set; }
        public DbSet<TipoRequisito> TipoRequisitos { get; set; }
    }
}
