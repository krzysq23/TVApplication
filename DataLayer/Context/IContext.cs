using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public interface IContext : IDisposable
    {
        DbSet<AppUser> AppUser { get; set; }
        DbSet<UserFriend> UserFriend { get; set; }
        DbSet<FavouriteMovie> FavouriteMovies { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<Notifications> Notifications { get; set; }

        int SaveChanges();
    }
}
