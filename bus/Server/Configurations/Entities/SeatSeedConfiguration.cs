using bus.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace bus.Server.Configurations.Entities
{
    public class SeatSeedConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Seat> builder)
        {
            builder.HasData(
                new Seat
                {
                    Id = 1,
                    Seats = "10",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Seat
                {
                    Id = 2,
                    Seats = "19",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                 new Seat
                 {
                     Id = 3,
                     Seats = "23",
                     DateCreated = DateTime.Now,
                     DateUpdated = DateTime.Now,
                     CreatedBy = "System",
                     UpdatedBy = "System"
                 },
                  new Seat
                  {
                      Id = 4,
                      Seats = "40",
                      DateCreated = DateTime.Now,
                      DateUpdated = DateTime.Now,
                      CreatedBy = "System",
                      UpdatedBy = "System"
                  },
                   new Seat
                   {
                       Id = 5,
                       Seats = "45",
                       DateCreated = DateTime.Now,
                       DateUpdated = DateTime.Now,
                       CreatedBy = "System",
                       UpdatedBy = "System"
                   },
                    new Seat
                    {
                        Id = 6,
                        Seats = "49",
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                        CreatedBy = "System",
                        UpdatedBy = "System"
                    }
                );
        }
    }
}