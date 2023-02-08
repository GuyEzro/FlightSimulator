using System;
using System.Numerics;
using Context;
using FlightController.Common;
using FlightController.Common.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FlightController.Services
{
    public class FlightRepository : IFlightRepository<Flight>
    {
        private FlightsContext _context;

        public FlightRepository(FlightsContext context)
        {
            _context = context;
        }

        public async Task Add(Flight plane)
        {
            await Task.FromResult(_context.Flights.Add(plane));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Flight>> GetAll() => await Task.FromResult(_context.Flights.ToList());

        public async Task<List<Flight>> GetLastInQ() => await Task.FromResult(_context.Flights.OrderByDescending(n => n.T1Time).Take(10).ToList());

        public async Task Update(Flight newplane)
        {
           await Task.FromResult(_context.Flights.Update(newplane));
           await _context.SaveChangesAsync();
           
        }
    }
}
