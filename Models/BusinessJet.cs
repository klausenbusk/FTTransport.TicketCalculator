namespace FTTransport.TicketCalculator {
    class BusinessJet : Plane {
        // Der er taget udgangspunkt i Cessna CitationJet M2 (forbedret version af CJ1): https://en.wikipedia.org/w/index.php?title=Cessna_CitationJet/M2&oldid=930647582#Specifications
        public BusinessJet() {
            // Der er nogle "parametre" der mangler, da de ikke er opgivet i Cessna materiale
            this.Manufacturer = "Cessna";
            this.Variant = "M2";
            this.Passengers = 7;
            this.ExitLimit = 0;
            this.Length = 12.98f;
            this.WingSpan = 14.4f;
            this.WingArea = 22.3f;
            this.Height = 4.24f;
            this.Width = 0;
            this.Cargo = 0;
            this.MTOW = 4_853;
            this.OEW = 3_171;
            this.FuelCapacity = 1_862;
            // Max. Cruise
            this.Speed = 748;
            this.Takeoff = 978;
            this.Range = 2_871;
            this.Ceiling = 12_497;
            this.Thrust = 8.74f;
        }

        public override int CalculateTime(int distance) {
            // Vi er et privatfly, så vi kan lette med ~1 times varsel
            // og vi har fortrinsret til security check/check-in/check-out (~30 min)
            int time = 60 + 30;
            return time + base.CalculateTime(distance);
        }

        public override int CalculatePrice(int distance, int tickets, bool returnTicket) {
            // Vi er et privatfly, så vi tager os godt betalt, 10.000 per booking + brændstof
            int price = 10_000;
            // Man betaler altid for at flyet flyver tilbage (returnTicket = true)
            // Derudover betaler man altid de "faktisk omkostninger" (ticket = 1),
            // da vi ikke flyver med andre
            price += base.CalculatePrice(distance, 1, true);
            if (returnTicket) {
                // Derudover betaler man 5000 ekstra, hvis flyet skal holdes på
                // jordan så man kan komme hjem igen
                price += 5_000;
            }
            return price;
        }
    }
}
