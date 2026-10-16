using HotelProj.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace HotelProj;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        using (var db = new HotelDbContext())
        {
            db.Database.Migrate();
        }
    }
}