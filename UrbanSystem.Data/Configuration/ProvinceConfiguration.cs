using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using UrbanSystem.Data.Models;
using static UrbanSystem.Common.EntityValidationConstants.Province;

namespace UrbanSystem.Data.Configuration
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.CityName)
                .IsRequired()
                .HasMaxLength(CityNameMaxLength);

            builder.Property(m => m.StreetName)
                .IsRequired()
                .HasMaxLength(StreetMaxLength);

            builder.HasData(SeedProvinces());
        }

        private List<Province> SeedProvinces()
        {
            return new List<Province>
            {
                new Province { Id = Guid.Parse("b55cfa08-7e8e-4f1b-9f0f-dc7e4fbc6d97"), CityName = "Sofia", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("c86d7f51-e3c4-4f62-b557-b7bffdecbfe9"), CityName = "Plovdiv", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("f79fa82b-30e8-49ea-b1b1-df85cb202c9e"), CityName = "Varna", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("b789b78d-f118-4f7a-b22d-4e165cbe5303"), CityName = "Burgas", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("e865e9c8-bb7c-4a0d-8715-58c4f5dcd7d0"), CityName = "Ruse", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("679b9b57-98d8-4fc1-bc65-bb98a5159168"), CityName = "Stara Zagora", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("202b67cc-83f5-4e4e-b4d2-69d785f62739"), CityName = "Pleven", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("4c4b5ecf-83c7-4937-91b2-b19e9db55ac5"), CityName = "Sliven", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("5a3b29b8-017e-4e57-8155-d8651da8be5e"), CityName = "Shumen", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("2e6e8718-8bc3-4b26-bcf2-b7c3d5c54e5d"), CityName = "Gabrovo", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("e6c6468e-3f8b-4bc7-b0be-0d143785f637"), CityName = "Pernik", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("63205d79-f5fc-4903-8458-d64f658e0ae3"), CityName = "Haskovo", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("2b7d3f4e-b179-4d02-bc2a-3d54b4930387"), CityName = "Kyustendil", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("164ec9c4-49c0-43ed-8c63-9a88c0591d41"), CityName = "Yambol", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("69c62c8d-1040-469e-b7ea-688e8c1e6587"), CityName = "Dobrich", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("49738b4c-5c9d-4700-8aa6-89c0e62242c4"), CityName = "Razgrad", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("ee8d2310-e9ca-45bc-a0be-d5a5bc57857e"), CityName = "Targovishte", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("d09c4464-409b-41f2-b9e5-53bb459b0e25"), CityName = "Silistra", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("9a5726aa-76a0-4db7-9d08-660ae5ebdc1a"), CityName = "Vidin", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("f8b8c3b0-b9ab-43c4-9112-1869be6cc176"), CityName = "Montana", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("ff8f7f5c-e69d-4d7b-888b-f219a9ab4c71"), CityName = "Vratsa", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("89ee0af6-8a02-45cf-8a01-bb8f56a94b90"), CityName = "Blagoevgrad", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("f34d29f1-df9e-45e0-bd0b-c8e2322e17b4"), CityName = "Smolyan", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("4a4d4203-105c-4517-b375-09f64673b020"), CityName = "Lovech", StreetName = "Not Specified" },
                new Province { Id = Guid.Parse("4d23eb75-1dc0-4373-8bc4-d229e38e7f85"), CityName = "Pazardzhik", StreetName = "Not Specified" }
            };
        }
    }
}
