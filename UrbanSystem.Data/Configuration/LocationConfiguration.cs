using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using static UrbanSystem.Common.EntityValidationConstants.Location;

namespace UrbanSystem.Data.Configuration
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.CityName)
                .IsRequired()
                .HasMaxLength(CityNameMaxLength);

            builder.Property(l => l.StreetName)
                .IsRequired()
                .HasMaxLength(StreetNameMaxLength);

            builder.HasData(SeedLocations());
        }

        private List<Location> SeedLocations()
        {
            return new List<Location>
            {
                new Location { Id = Guid.NewGuid(), CityName = "Blagoevgrad", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Burgas", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Dobrich", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Gabrovo", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Haskovo", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Kardzhali", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Kyustendil", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Lovech", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Montana", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Pazardzhik", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Pernik", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Pleven", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Plovdiv", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Razgrad", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Ruse", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Shumen", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Silistra", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Sliven", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Smolyan", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Sofia", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Stara Zagora", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Targovishte", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Varna", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Veliko Tarnovo", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Vidin", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Vratsa", StreetName = "Main Street" },
                new Location { Id = Guid.NewGuid(), CityName = "Yambol", StreetName = "Main Street" }
            };
        }
    }
}
