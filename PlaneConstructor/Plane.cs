using System;
namespace PlaneConstructor
{
	public class Plane
	{
        public string Id { get; set; } = null!;

        public string FlightCode { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int PassengerCount { get; set; }

        public bool IsInLanding { get; set; }

        public bool IsReady { get; set; } = true;

        public int Speed { get; set; }

        public int Leg { get; set; }

        public DateTime T1Time { get; set; }

        public DateTime T2Time { get; set; }

        public DateTime T3Time { get; set; }

        public DateTime T4Time { get; set; }

        public DateTime T5Time { get; set; }

        public DateTime T6Time { get; set; }

        public DateTime T7Time { get; set; }

        public DateTime T8Time { get; set; }

        public string origin { get; set; } = null!;

        public string destination { get; set; } = null!;
    }
}