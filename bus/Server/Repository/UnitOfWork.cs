using bus.Server.Data;
using bus.Server.IRepository;
using bus.Server.Models;
using bus.Server.Repository;
using bus.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bus.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Bus> _buses;
        private IGenericRepository<Seat> _seats;
        private IGenericRepository<Feedback> _feedbacks;
        private IGenericRepository<Booking> _bookings;
        private IGenericRepository<Customer> _customers;
        private IGenericRepository<Service> _services;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Bus> Buses
            => _buses ??= new GenericRepository<Bus>(_context);
        public IGenericRepository<Feedback> Feedbacks
            => _feedbacks ??= new GenericRepository<Feedback>(_context);
        public IGenericRepository<Booking> Bookings
           => _bookings ??= new GenericRepository<Booking>(_context);
        public IGenericRepository<Customer> Customers
          => _customers ??= new GenericRepository<Customer>(_context);
        public IGenericRepository<Seat> Seats
            => _seats ??= new GenericRepository<Seat>(_context);
        public IGenericRepository<Service> Services
            => _services ??= new GenericRepository<Service>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}