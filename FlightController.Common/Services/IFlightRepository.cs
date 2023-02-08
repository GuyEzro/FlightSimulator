using System;
using System.Numerics;
using FlightController.Common.Models;

namespace FlightController.Services
{
	public interface IFlightRepository<T>
	{
        Task Add(Flight plane);
        Task<List<Flight>> GetAll();
        Task Update(Flight newplane);
        Task<List<Flight>> GetLastInQ();
    }
}

