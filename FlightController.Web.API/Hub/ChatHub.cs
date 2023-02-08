using System.Diagnostics;
using FlightController.Common.Models;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    private Mutex mutex = new Mutex();

    public async Task Send(Flight flight) {
        try
        {
            mutex.WaitOne();
            if(Clients != null)
            await Clients.All.SendAsync("Receive", flight);
        }
        catch (Exception ex)
        { }
        finally
        {
            mutex.ReleaseMutex();
        }
    }
}