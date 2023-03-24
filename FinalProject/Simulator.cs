using System.Net.Http.Headers;
using PlaneConstructor;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Net.Http.Json;

namespace FinalProject
{
	public class Simulator
	{
		static HttpClient http = new HttpClient { BaseAddress = new Uri("https://localhost:7052") };
	    static Random rnd = new Random();
		Timer _time = new Timer();

        public void Start() {
			_time.Elapsed += (s, e) => Activate();
			_time.Interval = rnd.Next(5000, 15000);
			_time.Start();
		}
		private void Activate() {
			
		    PlaneCtor newplane = new PlaneCtor();
		    http.PostAsJsonAsync<Plane>("/Flight/create", newplane.NewFlight());
		    Console.WriteLine(newplane.ToString());
        }
	}
}