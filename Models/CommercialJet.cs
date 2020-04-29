using System;

namespace FTTransport.TicketCalculator {
    class CommericalJet : Plane {
        // Der er taget udgangspunkt i Boeing 737-100: https://en.wikipedia.org/w/index.php?title=Boeing_737&oldid=952875667#Specifications
        public CommericalJet() {
            this.Manufacturer = "Boeing";
            this.Variant = "737-100";
            // Den kan fås i forskellige konfigurationer, men 85 er minimum
            this.Passengers = 85;
            this.ExitLimit = 124;
            this.Length = 29;
            this.WingSpan = 28;
            this.WingArea = 91.04f;
            this.Height = 11;
            this.Width = 3.8f;
            this.Cargo = 18;
            this.MTOW = 50_000;
            this.OEW = 28_000;
            this.FuelCapacity = 17_865;
            // (796+876) / 2 = 836 km/t
            this.Speed = 836;
            // Der er ingen tal for 737-100, så har taget for 737-200
            this.Takeoff = 1_859;
            this.Range = 2_850;
            this.Ceiling = 11_300;
            this.Thrust = 62;
        }

        public override int CalculateTime(int distance) {
            // Rutefly så man skal møde ~2 timer før som passager,
            // derudover tager security/check-out hurtigt ~1 time
            int time = 120 + 60;
            return time + base.CalculateTime(distance);
        }

        public override int CalculatePrice(int distance, int tickets, bool returnTicket) {
            double price = base.CalculatePrice(distance, tickets, returnTicket);
            // Vi regner med en belægning på 80%
            price /= Passengers * 0.8;
            // Derudover har vi profitmargin på 20%
            price *= 1.2;
            price *= tickets;

            // Vi tager 100 kr per bookning i administrationsgebyr m.m.
            price += 100;
            return (int)Math.Round(price);
        }
    }
}
