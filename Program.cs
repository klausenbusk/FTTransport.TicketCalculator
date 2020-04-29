using System;
using System.Threading;

namespace FTTransport.TicketCalculator {
    class Program {
        private static Airport askAirport() {
            string input;
            while (true) {
                input = Console.ReadLine();
                switch (input) {
                    case "l":
                        foreach (Airport airport in Airports.GetAllAirports()) {
                            Console.WriteLine("{0} {1}", airport.IATA, airport.Name);
                        }
                        break;
                    case "r":
                        Airport randomAirport = Airports.GetRandomAirport();
                        Console.WriteLine("Vi har tilfældigt valgt følgende lufthavn: {0} {1}", randomAirport.IATA, randomAirport.Name);
                        return randomAirport;
                    case "q":
                        return null;
                    default:
                        Airport chosenAirport = Airports.GetAirport(input);
                        if (chosenAirport != null) {
                            return chosenAirport;
                        } else {
                            Console.WriteLine("Udkendt lufthavn: {0}", input);
                            Console.WriteLine("Prøv venligst igen");
                        }
                        break;
                }
            }
        }
        public static void Main(string[] args) {
            Console.WriteLine("Velkommen til Fly Transport A/S beregningssystem");

            // from airport
            Console.WriteLine("Hvor rejser du fra? (ex: CPH, AMS)");
            Console.WriteLine("Skriv l for at liste alle lufthavne eller r for en tilfældig lufthavn");
            Airport from = askAirport();
            // to airport
            Console.WriteLine("Hvor skal du hen? (ex: CPH, AMS)");
            Airport to = askAirport();

            // Antal billetter
            Console.WriteLine("Hvor mange billetter ønsker du?");
            int tickets;
            while (!Int32.TryParse(Console.ReadLine(), out tickets) || tickets == 0) {
                Console.WriteLine("Indtast venligst et gyldigt antal billetter");
            }
            // Returbillet?
            Console.WriteLine("Skal det være en returbillet? (ja/nej)");
            bool returnTicket;
            while (true) {
                String input = Console.ReadLine();
                // Switch eller if kommer vist ud på det samme her
                if (string.Equals(input, "ja", StringComparison.OrdinalIgnoreCase)) {
                    returnTicket = true;
                    break;
                } else if (string.Equals(input, "nej", StringComparison.OrdinalIgnoreCase)) {
                    returnTicket = false;
                    break;
                } else {
                    Console.WriteLine("Indtast venligst et gyldigt svar: ja eller nej");
                }
            }

            // Loading bar
            Console.Write("Udregner pris og tid");
            for (int i = 0; i < 5; i++) {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine("\n");

            // Udregn pris og tid og print det
            Plane businessJet = new BusinessJet();
            Plane commericalJet = new CommericalJet();
            // ^override virker (yeh!)
            int businessJetPrice = businessJet.CalculatePrice(from, to, tickets, returnTicket);
            int businessJetTime = businessJet.CalculateTime(from, to);
            int commericalJetPrice = commericalJet.CalculatePrice(from, to, tickets, returnTicket);
            int CommericalJetTime = commericalJet.CalculateTime(from, to);
            Console.WriteLine("Vi har fundet følgende muligheder:");
            Console.WriteLine("Fra {0} ({1}) til {2} ({3}):", from.IATA, from.Name, to.IATA, to.Name);
            Console.WriteLine("Privatfly: {0} kroner og det tager {1} minutter", businessJetPrice, businessJetTime);
            Console.WriteLine("Rutefly: {0} kroner og det tager {1} minutter", commericalJetPrice, CommericalJetTime);
        }
    }
}
