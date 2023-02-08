using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using FlightController.Common;
using FlightController.Common.Enums;
using FlightController.Common.Models;
using FlightController.Services;

namespace FlightController.Web.API.Worker
{
	public class TerminalManagerWorker : BackgroundService
    {
        private readonly ILogger<TerminalManagerWorker> _logger;
        private readonly IFlightConsumer _flightConsumer;
        private readonly IServiceProvider _service;

        Random rnd = new Random();
        FileStream _stream = new FileStream("log.txt",FileMode.OpenOrCreate);

        private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1,1);
        private static string[]? _destinationEnum;

        private Terminal _terminal2 = new Terminal();
        private Terminal _terminal3 = new Terminal();
        private Terminal _terminal4 = new Terminal();
        private Terminal _terminal5 = new Terminal();
        private Terminal _terminal6 = new Terminal();
        private Terminal _terminal7 = new Terminal();
        private Terminal _terminal8 = new Terminal();

        public TerminalManagerWorker(ILogger<TerminalManagerWorker> logger, IFlightConsumer flightConsumer, IServiceProvider service)
        {
            _logger = logger;
            _flightConsumer = flightConsumer;
            _service = service;
            _destinationEnum = Enum.GetValues<DestinationEnum>().Select(x => x.ToString()).ToArray();
        }

        public async Task Update(Flight flight) {
            using (var scope = _service.CreateScope()) {
                var scopedService = scope.ServiceProvider.GetRequiredService<IFlightRepository<Flight>>();
               await scopedService.Update(flight);
            }
        }
        public async Task Send(Flight flight) {
            using (var scope = _service.CreateScope()) {
                var scopedService = scope.ServiceProvider.GetRequiredService<ChatHub>();
                await scopedService.Send(flight);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
               await move();
            }
            await _stream.DisposeAsync();
        }

        private async Task move()
        {
            if (_terminal2.IsFree)
            {
                var flight = await _flightConsumer.GetAsync();
                if (flight != null) 
                    MoveToT2(flight);
            }
            if (_terminal3.IsFree && (!_terminal2.IsFree) && _terminal2.IsReady)
            {
                MoveToT3();
            }

            if (_terminal4.IsFree && (!_terminal8.IsFree) && _terminal8.IsReady && _terminal8._flight.IsInLanding == false)
            {
                MoveToT4From8();

            }

            if (_terminal4.IsFree && (!_terminal3.IsFree) && _terminal3.IsReady)
            {
                MoveToT4();

            }

            if (_terminal5.IsFree && (!_terminal4.IsFree) && _terminal4._flight.IsInLanding && _terminal4.IsReady)
            {
                MoveToT5();

            }


            if (_terminal6.IsFree && (!_terminal5.IsFree) && _terminal5._flight.IsInLanding && _terminal6.IsReady)
            {
                MoveToT6();

            }

            else if (_terminal7.IsFree && (!_terminal5.IsFree) && _terminal5._flight.IsInLanding && _terminal7.IsReady)
            {
                MoveToT7();

            }
            if (_terminal8.IsFree && (!_terminal6.IsFree) && _terminal6.IsReady)
            {
                MoveToT8(_terminal6);

            }
            if (_terminal8.IsFree && (!_terminal7.IsFree) && _terminal7.IsReady)
            {
                MoveToT8(_terminal7);

            }
            if ((!_terminal4.IsFree) && (!_terminal4._flight.IsInLanding) && _terminal4.IsReady)
            {
                _logger.LogInformation($"{_terminal4._flight.FlightCode} has taken off, {DateTime.Now}");
                byte[] bytes = Encoding.UTF8.GetBytes($"{_terminal4._flight.FlightCode} has taken off, {DateTime.Now}\n");
                await _stream.WriteAsync(bytes);
                await _stream.FlushAsync();
                await Send(_terminal4._flight);
                _terminal4.IsReady = false;
                _terminal4.IsFree = true;
                _terminal4.Enter(10);
                _terminal4._flight = new Flight();
                _terminal4._flight.IsInLanding = false;
            }
        }

        private async void MoveToT2(Flight flight)
        {
            _lock.Wait();
            try
            {
                _terminal2.IsFree = false;
                _terminal2.IsReady = false;
                _terminal2._flight = flight;
                _terminal2._flight.Leg = 2;
                _terminal2._flight.T2Time = DateTime.Now;
                _terminal2.Enter(7);
                await Send(_terminal2._flight);

                _logger.LogInformation($"{flight.FlightCode} is in T2, {_terminal2._flight.T2Time}");
                byte[] bytes = Encoding.UTF8.GetBytes($"{flight.FlightCode} is in T2, {_terminal2._flight.T2Time}\n");
                await _stream.WriteAsync(bytes);
                await _stream.FlushAsync();
            }
            finally {
                _lock.Release();
            }
        }
        private async void MoveToT3()
        {
            _lock.Wait();
            try
            {
                _terminal2.NextTerminal(_terminal3, 3, 6);
                _terminal3._flight.T3Time = DateTime.Now;
                await Send(_terminal3._flight);

               _logger.LogInformation($"{_terminal3._flight.FlightCode} is in T3, {_terminal3._flight.T3Time}");
                byte[] bytes = Encoding.UTF8.GetBytes($"{_terminal3._flight.FlightCode} is in T3, {_terminal3._flight.T3Time}\n");
                await _stream.WriteAsync(bytes);
                await _stream.FlushAsync();
            }
            finally
            {
                _lock.Release();
            }
        }
        private async void MoveToT4()
        {
            _lock.Wait();
            try
            {
                _terminal3.NextTerminal(_terminal4, 4, 10);
                _terminal4._flight.T4Time = DateTime.Now;
                await Send(_terminal4._flight);

                _logger.LogInformation($"{_terminal4._flight.FlightCode} is in T4, {_terminal4._flight.T4Time}");
                byte[] bytes = Encoding.UTF8.GetBytes($"{_terminal4._flight.FlightCode} is in T4, {_terminal4._flight.T4Time}\n");
                await _stream.WriteAsync(bytes);
                await _stream.FlushAsync();
            }
            finally
            {
                _lock.Release();
            }
        }
        private async void MoveToT4From8()
        {
            _lock.Wait();
            try
            {
                _terminal8.NextTerminal(_terminal4, 4, 10);
                _logger.LogInformation($"{_terminal4._flight.FlightCode} is in T4, {_terminal4._flight.T4Time}");
                byte[] bytes = Encoding.UTF8.GetBytes($"{_terminal4._flight.FlightCode} is in T4, {_terminal4._flight.T4Time}\n");
                await _stream.WriteAsync(bytes);
                await _stream.FlushAsync();
            }
            finally
            {
                _lock.Release();
            }
        }
        private async void MoveToT5()
        {
            _lock.Wait();
            try
            {
                _terminal4.NextTerminal(_terminal5, 5, 7);
                _terminal5._flight.T5Time = DateTime.Now;
                 await Send(_terminal5._flight);

                _logger.LogInformation($"{_terminal5._flight.FlightCode} is in T5, {_terminal5._flight.T5Time}");
                byte[] bytes = Encoding.UTF8.GetBytes($"{_terminal5._flight.FlightCode} is in T5, {_terminal5._flight.T5Time}\n");
                await _stream.WriteAsync(bytes);
                await _stream.FlushAsync();
            }
            finally
            {
                _lock.Release();
            }
        }
        private async void MoveToT6()
        {
            _lock.Wait();
            try
            {
                _terminal5.NextTerminal(_terminal6, 6, 10);
                _terminal6._flight.T6Time = DateTime.Now;
                _terminal6._flight.IsInLanding = false;
                await Send(_terminal6._flight);

                _terminal6._flight.origin = "TEL AVIV";
                string destination = _destinationEnum[rnd.Next(_destinationEnum.Length)];
                switch (destination)
                {
                    case "NEW_YORK":
                      _terminal6._flight.destination = "NEW YORK";
                        break;
                    case "ABU_DHABI":
                        _terminal6._flight.destination = "ABU DHABI";
                        break;
                    case "SAN_FRANCISCO":
                        _terminal6._flight.destination = "SAN FRANCISCO";
                        break;
                    default:
                        _terminal6._flight.destination = destination;
                        break;
                }

                _logger.LogInformation($"{_terminal6._flight.FlightCode} is in T6, {_terminal6._flight.T6Time}");
                byte[] bytes = Encoding.UTF8.GetBytes($"{_terminal6._flight.FlightCode} is in T6, {_terminal6._flight.T6Time}\n");
                await _stream.WriteAsync(bytes);
                await _stream.FlushAsync();
            }
            finally
            {
                _lock.Release();
            }
        }
        private async void MoveToT7()
        {
            _lock.Wait();
            try
            {
                _terminal5.NextTerminal(_terminal7, 7, 10);
                _terminal7._flight.IsInLanding = false;
                _terminal7._flight.T7Time = DateTime.Now;
                await Send(_terminal7._flight);

                _terminal7._flight.origin = "TEL AVIV";
                string destination = _destinationEnum[rnd.Next(_destinationEnum.Length)];
                switch (destination)
                {
                    case "NEW_YORK":
                        _terminal7._flight.destination = "NEW YORK";
                        break;
                    case "ABU_DHABI":
                        _terminal7._flight.destination = "ABU DHABI";
                        break;
                    case "SAN_FRANCISCO":
                        _terminal7._flight.destination = "SAN FRANCISCO";
                        break;
                    default:
                        _terminal7._flight.destination = destination;
                        break;
                }
                _logger.LogInformation($"{_terminal7._flight.FlightCode} is in T7, {_terminal7._flight.T7Time}");
                byte[] bytes = Encoding.UTF8.GetBytes($"{_terminal7._flight.FlightCode} is in T7, {_terminal7._flight.T7Time}\n");
                await _stream.WriteAsync(bytes);
                await _stream.FlushAsync();
            }
            finally
            {
                _lock.Release();
            }
        }
        private async void MoveToT8(Terminal t)
        {
            _lock.Wait();
            try
            {
                t.NextTerminal(_terminal8, 8, 7);
                _terminal8._flight.T8Time = DateTime.Now;
                Flight tempFlight = _terminal8._flight; 
                await Update(tempFlight);
                await Send(_terminal8._flight);

                _logger.LogInformation($"{_terminal8._flight.FlightCode} is in T8, {_terminal8._flight.T8Time}");
                byte[] bytes = Encoding.UTF8.GetBytes($"{_terminal8._flight.FlightCode} is in T8, {_terminal8._flight.T8Time}\n");
                await _stream.WriteAsync(bytes);
                await _stream.FlushAsync();

            }
            finally
            {
                _lock.Release();
            }
        }
    }
}