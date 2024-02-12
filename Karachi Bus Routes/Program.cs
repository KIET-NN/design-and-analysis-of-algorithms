using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Karachi_Bus_Routes
{
    public class BusRoute
    {
        public string Origin { get; set; }
        public string Terminus { get; set; }
        public string Name { get; set; }
        public int Stops { get; set; }
        public string[] Route {  get; set; }
        public BusRoute() { }

        public BusRoute(string name, string[] route)
        {
            if (route == null || route.Length == 0)
            {
                throw new ArgumentException("Invalid data!");
            }
            this.Origin = route[0];
            this.Terminus = route[route.Length - 1];
            this.Name = name;
            this.Route = route;
            this.Stops = route.Length - 1;
        }

        public string ToString()
        {
            return $"Origin: {Origin}, Terminus: {Terminus}, Name: {Name}, Stops: {Stops}, Route: {string.Join(">", Route)}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string start = "c";
            string destination = "g";
            List<BusRoute> busRoutes = ReadDummyData();
            List<BusRoute> solution;
            foreach (BusRoute route in busRoutes)
            {
                Console.WriteLine(route.ToString());
                Console.WriteLine();
            }
            Console.WriteLine($"To go from {start} to {destination}: ");
            solution = FindDirectRoute(busRoutes, start, destination);
            if (solution != null)
            {
                BusRoute route = solution[0];
                Console.WriteLine($"Take {route.Name} from {route.Origin} to {route.Terminus}, which includes {route.Stops} stops.");
            }

            start = "b";
            destination = "f";
            Console.WriteLine($"To go from {start} to {destination}: ");
            solution = FindShortestRoute(busRoutes, start, destination);
            if (solution != null)
            {
                BusRoute route = solution[0];
                Console.WriteLine($"Take {route.Name} from {route.Origin} to {route.Terminus}, which includes {route.Stops} stops.");
            }

            start = "e";
            destination = "j";
            Console.WriteLine($"To go from {start} to {destination}: ");
            solution = FindRouteGreedy(busRoutes, start, destination);
            if (solution != null)
            {
                foreach (var route in solution)
                {
                    Console.WriteLine($"Take {route.Name} from {route.Origin} to {route.Terminus}, which includes {route.Stops} stops.");
                }
            }

            start = "b";
            destination = "i";
            Console.WriteLine($"To go from {start} to {destination}: ");
            solution = FindRouteViaTerminus(busRoutes, start, destination);
            if (solution != null)
            {
                foreach (var route in solution)
                {
                    Console.WriteLine($"Take {route.Name} from {route.Origin} to {route.Terminus}, which includes {route.Stops} stops.");
                }
            }

            start = "b";
            destination = "k";
            Console.WriteLine($"To go from {start} to {destination}: ");
            solution = FindRouteViaHub(busRoutes, start, destination);
            if (solution != null)
            {
                foreach (var route in solution)
                {
                    Console.WriteLine($"Take {route.Name} from {route.Origin} to {route.Terminus}, which includes {route.Stops} stops.");
                }
            }

            //start = "b";
            //destination = "m";
            //Console.WriteLine($"To go from {start} to {destination}: ");
            //solution = FindRouteViaHubRecursive(busRoutes, start, destination);
            //if (solution != null)
            //{
            //    foreach (var route in solution)
            //    {
            //        Console.WriteLine($"Take {route.Name} from {route.Origin} to {route.Terminus}, which includes {route.Stops} stops.");
            //    }
            //}

            // Test on real data now
            //busRoutes = ReadCsv("data.csv");
            Console.ReadKey();
        }

        static List<BusRoute> ReadDummyData()
        {
            List <BusRoute> routes = new List<BusRoute>();
            routes.Add(new BusRoute("X2", new string[] { "b", "c", "e", "f", "g", "h" }));
            routes.Add(new BusRoute("4L", new string[] { "a", "b", "d", "f", "j" }));
            routes.Add(new BusRoute("F11", new string[] { "a", "c", "h", "i", "j" }));
            routes.Add(new BusRoute("F18", new string[] { "i", "j", "k" }));
            routes.Add(new BusRoute("W6", new string[] { "k", "l", "m" }));
            return routes;
        }

        static List<BusRoute> ReadCsv(string filePath)
        {
            List<BusRoute> busRoutes = new List<BusRoute>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader,
                new CsvConfiguration(CultureInfo.InvariantCulture)
                { Delimiter = "\t", HasHeaderRecord = true }))
            {
                while (csv.Read())
                {
                    BusRoute route = new BusRoute(csv.GetField<string>(2), csv.GetField<string>(4).Split('>'));
                    busRoutes.Add(route);
                }
            }
            return busRoutes;
        }

        static List<BusRoute> FindDirectRoute(List<BusRoute> map, string start, string destination)
        {
            foreach (var entry in map)
            {
                int j = Array.IndexOf(entry.Route, start);
                if (j == -1) continue;
                int k = Array.IndexOf(entry.Route, destination);
                if (k >= 0)
                {
                    string[] newRoute = entry.Route.Skip(j).Take(k - j + 1).ToArray();
                    BusRoute found = new BusRoute(entry.Name, newRoute);
                    return new List<BusRoute> { found };
                }
            }
            return null;
        }

        static List<BusRoute> FindShortestRoute(List<BusRoute> map, string start, string destination)
        {
            int min = Int16.MaxValue;
            BusRoute found = null;
            foreach (var entry in map)
            {
                int j = Array.IndexOf(entry.Route, start);
                if (j == -1) continue;
                int k = Array.IndexOf(entry.Route, destination);
                if (k >= 0 && (k - j < min) && j < k)
                {
                    min = k - j;
                    string[] newRoute = entry.Route.Skip(j).Take(k - j + 1).ToArray();
                    found = new BusRoute(entry.Name, newRoute);
                }
            }
            return found == null ? null : new List<BusRoute>() { found };
        }

        static List<BusRoute> FindRouteGreedy(List<BusRoute> map, string start, string destination)
        {
            List<BusRoute> found = FindShortestRoute(map, start, destination);
            if (found != null) return found;
            foreach (var entry in map)
            {
                int j = Array.IndexOf(entry.Route, start);
                if (j >= 0)
                {
                    List<string> neighbours = new List<string>(entry.Route.Skip(j + 1).ToArray());
                    foreach (var neighbour in neighbours)
                    {
                        found = FindShortestRoute(map, neighbour, destination);
                        if (found != null)
                        {
                            var first = FindShortestRoute(map, start, neighbour);
                            found.AddRange(first);
                            found.Reverse();
                            return found;
                        }
                    }
                }
            }
            return null;
        }

        static List<BusRoute> FindRouteViaTerminus(List<BusRoute> map, string start, string destination)
        {
            List<BusRoute> found = FindShortestRoute(map, start, destination);
            if (found != null) return found;
            foreach (var route in map)
            {
                int a = Array.IndexOf(route.Route, start);
                if (a == -1) continue;
                found = FindShortestRoute(map, route.Terminus, destination);
                if (found != null)
                {
                    var solution = new List<BusRoute>() { route };
                    solution.AddRange(found);
                    return solution;
                }
                foreach (var route2 in map)
                {
                    route2.Route = route2.Route.Reverse().ToArray();
                    int b = Array.IndexOf(route2.Route, destination);
                    int c = Array.IndexOf(route2.Route, route.Terminus);
                    if (b >= 0 && c >= 0)
                    {
                        route2.Route = route2.Route.Skip(c).Take(b - c + 1).ToArray();
                        return new List<BusRoute>() { route, new BusRoute(route2.Name, route2.Route) };
                    }
                }
            }
            return null;
        }

        
    }
}
