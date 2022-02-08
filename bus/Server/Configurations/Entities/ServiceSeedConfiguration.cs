using bus.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace bus.Server.Configurations.Entities
{
    public class ServiceSeedConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Service> builder)
        {
            builder.HasData(
                new Service
                {
                    Id = 1,
                    Type = "School bus",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Service
                {
                    Id = 2,
                    Type = "Shuttle bus",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Service
                {
                    Id = 3,
                    Type = "Concert / Event Trips",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Service
                {
                    Id = 4,
                    Type = "School Excursion / Field Trips / Camp",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Service
                {
                    Id = 5,
                    Type = "Funeral",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Service
                {
                    Id = 6,
                    Type = "Day Care Centres",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Service
                {
                    Id = 7,
                    Type = "Wedding Functions",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Service
                {
                    Id = 8,
                    Type = "Company Functions",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
                );
        }
    }
}