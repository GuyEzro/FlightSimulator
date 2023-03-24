using System.ComponentModel;
using System.Reflection;
using System.Text;
namespace PlaneConstructor;

public class PlaneCtor
{
    Random rnd = new Random();
    private string[] _originEnum = Enum.GetValues<OriginsEnum>().Select(x => x.ToString()).ToArray();
    private string[] _brand = new string[] { "Boeing", "Airbus" };
    private string[] _models = new string[] { "Boeing 737", "Boeing 777", "Airbus A330", "Airbus A321" };
    private char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private Plane _plane = new Plane();

    public Plane NewFlight() {
        DateTime date = DateTime.MinValue;
        date = date.AddYears(1800);
        _plane.Brand = _brand[rnd.Next(0, 2)];
        _plane.T1Time = DateTime.Now;
        _plane.T2Time = date;
        _plane.T3Time = date;
        _plane.T4Time = date;
        _plane.T5Time = date;
        _plane.T6Time = date;
        _plane.T7Time = date;
        _plane.T8Time = date;
        _plane.Id = Guid.NewGuid().ToString();
        _plane.Leg = 1;
        _plane.FlightCode = FlightCodeRandom();
        _plane.IsReady = true;
        _plane.IsInLanding = true;
        _plane.origin = RandomOrigin();
        _plane.destination = "TEL AVIV";
        ModelRandom(_plane);
        return _plane;
    }

    private string RandomOrigin()
    {
       string origin = _originEnum[rnd.Next(_originEnum.Length)];
        switch (origin)
        {
            case "NEW_YORK":
                return "NEW YORK";

            case "ABU_DHABI":
                return "ABU DHABI";

            case "SAN_FRANCISCO":
                return "SAN FRANCISCO";

            default:
                return origin;
        }
    }

    public override string ToString()
    {
        return $"filght code: {_plane.FlightCode}" +
            $"\nflight id: {_plane.Id}" +
            $"\nflight brand: {_plane.Brand}" +
            $"\npassenger count: {_plane.PassengerCount}" +
            $"\nplane model: {_plane.Model}" +
            $"\nplane time of departure: {_plane.T1Time}" +
            $"\nleg: {_plane.Leg}" +
            $"\nplane speed: {_plane.Speed}" +
            $"\norigin: {_plane.origin}" +
            $"\ndestination: {_plane.destination}" +
            $"\n----------------------------------------\n";
    }

    private string FlightCodeRandom()
    {
        StringBuilder str = new StringBuilder();
        str.Append(_alpha[rnd.Next(26)]);
        str.Append(_alpha[rnd.Next(26)]);
        str.Append(rnd.Next(1000,10000));
        return str.ToString();
    }

    private void ModelRandom(Plane plane)
    {
        switch (plane.Brand)
        {
            case "Boeing":
               plane.Model = _models[rnd.Next(0,2)];
                if (plane.Model == "Boeing 737")
                {
                    plane.PassengerCount = rnd.Next(100,170);
                    plane.Speed = rnd.Next(600, 870);
                }
                else {
                    plane.PassengerCount = rnd.Next(200, 370);
                    plane.Speed = rnd.Next(650, 900);
                }
                break;
            case "Airbus":
               plane.Model = _models[rnd.Next(2,4)];
                if (plane.Model == "Airbus A330")
                {
                    plane.PassengerCount = rnd.Next(300, 440);
                    plane.Speed = rnd.Next(600, 870);
                }
                else
                {
                    plane.PassengerCount = rnd.Next(150, 230);
                    plane.Speed = rnd.Next(500, 800);
                }
                break;
            default:
               break;
        }
    }
}
