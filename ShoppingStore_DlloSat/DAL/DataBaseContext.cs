using Microsoft.EntityFrameworkCore;
using ShoppingStore_DlloSat.DAL.Entities;
using System.Data.Common;

namespace ShoppingStore_DlloSat.DAL
{
    public class DataBaseContext : DbContext
    {
        //Constructor
        public DataBaseContext(DbContextOptions<DataBaseContext>options) : base(options)
        {
            
        }

        public DbSet<Country> Countries { get; set; }
     }
}
