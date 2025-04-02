using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CaroGameAPI.Data;

namespace CaroGameAPI.Data
{
    public class CaroGameAPIContext : DbContext
    {
        public CaroGameAPIContext (DbContextOptions<CaroGameAPIContext> options)
            : base(options)
        {
        }

        public DbSet<CaroGameAPI.Data.Users> Users { get; set; } = default!;
        public DbSet<CaroGameAPI.Data.Rooms> Rooms { get; set; } = default!;
        public DbSet<CaroGameAPI.Data.GameMatches> GameMatches { get; set; } = default!;
        public DbSet<CaroGameAPI.Data.Moves> Moves { get; set; } = default!;
        public DbSet<CaroGameAPI.Data.Ranking> Ranking { get; set; } = default!;
    }
}
