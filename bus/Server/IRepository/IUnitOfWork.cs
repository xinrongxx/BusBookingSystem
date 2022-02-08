using bus.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bus.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Bus> Buses { get; }
        IGenericRepository<Feedback> Feedbacks { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Booking> Bookings { get; }
        IGenericRepository<Seat> Seats { get; }
        IGenericRepository<Service> Services { get; }
    }
}