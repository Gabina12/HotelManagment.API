using HotelManagment.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hotelmanagment.DB
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<AttributeCode> AttributeCodes { get; set; }
        public DbSet<RoomAttribute> RoomAttributes { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<SaleType> SaleTypes { get; set; }
    }
}
