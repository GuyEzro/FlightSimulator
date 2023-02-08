using System;
using FlightController.Common.Models;
using FlightController.Services;
using Timer = System.Timers.Timer;
namespace FlightController.Web.API
{
	public class Terminal
	{
		public bool IsFree { get; set; } = true;
		public bool IsReady { get; set; } = true;
		
        private readonly Mutex mutex = new Mutex();
		public Timer time = new Timer();
		public Flight _flight = new Flight { Speed = 1,IsInLanding = true};

		public Terminal()
		{
			time.Elapsed += (s, e) => IsReady = true;
			time.AutoReset = false;
		}

		public void Enter(int waitingTime = 4)
		{
			mutex.WaitOne();

			double overAllTime = _flight.Speed * waitingTime;
            time.Interval = 4000;

            if (overAllTime > 0)
			   time.Interval = overAllTime;

			time.Start();

			mutex.ReleaseMutex();
		}

		public void NextTerminal(Terminal nextT,int leg,int timerTime) {
			mutex.WaitOne();

            nextT._flight = this._flight;
            nextT._flight.Leg = leg;
			this._flight = new Flight();
			this.IsFree = true;
			nextT.IsFree = false;
			nextT.IsReady = false;
            nextT.Enter(timerTime);

			mutex.ReleaseMutex();
		}
	}
}