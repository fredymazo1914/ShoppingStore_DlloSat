using Microsoft.EntityFrameworkCore;
using ShoppingStore_DlloSat.DAL.Entities;
using System.Data.Common;

namespace ShoppingStore_DlloSat.DAL
{
    public class DataBaseContext : DbContext
    {
        //Este constructor crea la referencia que sirve para configurar las opciones de la BD,
        //como por ejemplo, usar SQL Server  y usar la cadena de conexión a la BD 
        public DataBaseContext(DbContextOptions<DataBaseContext>options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //La siguiente línea comtrola la duplicidad de los países
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
        }

        //DbSet sirve para convertir la clase Country en una tabla de BD.
        //El nombre de la tabla será "Contries"
        public DbSet<Country> Countries { get; set; }
     }
}
