using System;

namespace FTTransport.TicketCalculator {
    // Der er taget udgangspunkt i: https://en.wikipedia.org/w/index.php?title=Boeing_737&oldid=952875667#Specifications
    // TODO: Der er sikkert nogle konventioner for hvilke enheder man bruger i
    // flyverdenen (fod?), det kunne være relevant at undersøge
    class Plane {
        // TODO: Man kunne overveje at sætte readonly
        // Note: Der bruges float flere steder fremfor f.eks. double,
        // det gøres der da float er præcist nok

        // Ex: Boeing, Airbus etc.
        public string Manufacturer { get; set; }
        // Ex: 737-100 (Boeing), A380-800 (Airbus) etc.
        public string Variant { get; set; }
        // Maks antal passagerer
        public int Passengers { get; set; }
        // Hvor mange passagerer, der kan evakueres med de døre der er
        public int ExitLimit { get; set; }
        // I meter
        public float Length { get; set; }
        // I meter
        public float WingSpan { get; set; }
        // I kubikmeter
        public float WingArea { get; set; }
        // I meter
        public float Height { get; set; }
        // I meter
        public float Width { get; set; }
        // I kubikmeter
        public int Cargo { get; set; }
        // Maximum takeoff weight i kg
        public int MTOW { get; set; }
        // Operating empty weight i kg
        public int OEW { get; set; }
        // I liter
        public int FuelCapacity { get; set; }
        // I km/t
        public int Speed { get; set; }
        // I meter
        public int Takeoff { get; set; }
        // I meter (maks højde)
        public int Ceiling { get; set; }
        // I km
        public int Range { get; set; }
        // Liter per km
        public float FuelEfficiency {
            get {
                // Det burde give et "nogenlunde" retvisende resultat, men average
                // kan ikke bruges til alverden, man bør tage højde for last og
                // ikke tanke mere brændstof end højest nødvendigt, men kræver
                // nogle andre udregninger.
                return (float)FuelCapacity / Range;
            }
        }
        // I kilonewton (kN) per motor(?)
        public float Thrust { get; set; }

        // Returnerer distancen i km
        public virtual double CalculateDistance(Airport from, Airport to) {
            // Naiv implementering, der ikke tagde højde for at det kan være
            // nødvendigt at lande undervejs (et fly har en begrænset range)
            Airport fromAirport = from;
            Airport toAirport = to;
            return Haversine.CalculateDistance(fromAirport.Latitude, fromAirport.Longitude, toAirport.Latitude, toAirport.Longitude);
        }

        // Returnerer antal minutter distancen tager at flyve
        public virtual int CalculateTime(Airport from, Airport to) {
            return CalculateTime((int)Math.Round(CalculateDistance(from, to)));
        }

        // distance i km
        public virtual int CalculateTime(int distance) {
            // Naiv implementering der ikke tager højde for check-in m.m.
            return (distance / Speed)*60;
        }

        // Returnerer brændstofforbruget i liter for distancen
        // distance i km
        public virtual int CalculateFuel(int distance) {
            // Naiv implementering, et fly bruger sikkert (betydelig) mere brændstof ved takeoff(?)
            return (int)Math.Round(distance / FuelEfficiency);
        }

        // Returnerer prisen i danske kroner
        public virtual int CalculatePrice(Airport from, Airport to, int tickets, bool returnTicket) {
            int distance = (int)Math.Round(CalculateDistance(from, to));
            return CalculatePrice(distance, tickets, returnTicket);
        }

        // Returnerer prisen i danske kroner for distancen
        // distance i km
        // tickets er antal billetter, den bruges dog ikke i den her naive implementering
        // returnTicket specificerer om det er en returbillet
        public virtual int CalculatePrice(int distance, int tickets, bool returnTicket) {
            // Naiv implementering, der ikke tager højde for bl.a. omkostninger til tankning, bagagehåndtering m.m.
            int liters = CalculateFuel(distance);
            if (returnTicket) {
                liters *= 2;
            }
            // https://www.rke.dk/en/pilot-info/fuel-prices
            // FIXME: Man bør ikke hardcode brændstofprisen
            return (int)Math.Round(liters * 7.75);
        }
    }
}
