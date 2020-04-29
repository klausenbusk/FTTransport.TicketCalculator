using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FTTransport.TicketCalculator {
    class Airports {
        private static Dictionary<string, Airport> airports = new Dictionary<string, Airport>();

        static Airports() {
            // Man bør overveje om lufthavene bør ligge i en (ekstern) database.
            // I det her tilfælde er min umiddelbare vurdering dog at der ikke er
            // nogen gode argumenter for sådan en løsning, og at kompleksiteten
            // ikke står mål med udbyttet.
            // Lazy loading i GetAiport er også en mulighed..
            // FIXME: Hard coding
            string jsonString = File.ReadAllText("airports.json");
            List<Airport> airportList = JsonSerializer.Deserialize<List<Airport>>(jsonString);
            foreach (Airport airport in airportList) {
                airports.Add(airport.IATA, airport);
            }
        }

        public static Airport GetAirport(string IATA) {
            Airport airport;
            airports.TryGetValue(IATA, out airport);
            return airport;
        }

        // Returnerer alle Airports
        // Man kunne overveje bare at gøre airports public..
        public static IEnumerable<Airport> GetAllAirports() {
            return airports.Values;
        }

        public static Airport GetRandomAirport() {
            int randomIndex = new Random().Next(0, airports.Count);
            return airports.ElementAt(randomIndex).Value;
        }
    }
}
