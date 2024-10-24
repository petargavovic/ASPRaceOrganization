using BaseLibrary.DTOs.EntityDTOs;
using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Vozac> Vozaci { get; set; }
        public DbSet<Trka> Trke { get; set; }
        public DbSet<RangLista> RangListe { get; set; }
        public DbSet<Ucinak> Ucinci { get; set; }
        public DbSet<Plasman> Plasmani { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }
    }
}
