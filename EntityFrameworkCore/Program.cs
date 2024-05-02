using EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AirplaneDbContext context = new AirplaneDbContext();
            //context.Clients.Add(new Client()
            //{
            //    Name = "Volodia",
            //    Email = "vol@gmail.com",
            //    Birthday = new DateTime(2007, 11, 4)
            //});
            //context.SaveChanges();

            //foreach (var item in context.Clients)
            //{
            //    Console.WriteLine($"Client : name - {item.Name}. Email: {item.Email}. Birthday : {item.Birthday?.ToShortDateString()}");
            //}

            //LINQ to Entities

            //Loading data : Include(relation data)

            //select * from Flights join Airplane on f.AiplaneId = a.Id
            var filteredFlights = context.Flights.Include(f => f.Airplane)
                                    .Where(f => f.ArrivalCity == "Lviv")
                                    .OrderBy(f => f.DepartureTime);

            foreach (var f in filteredFlights)
            {
                Console.WriteLine($"Flight : {f.Number}. From : {f.DepartureCity,-10} to {f.ArrivalCity,7}  " +
                    $"   Date :{f.DepartureTime.ToShortDateString(),10}"
                    + $"Airplane : {f.AirplaneId} - {f.Airplane?.Model} Count Passangers : {f.Airplane?.MaxPassanger}");
            }

            //Exlicit data loading : context.Entry(entity).Collection/Reference(),Load();
            var client = context.Clients.Find(1);
            context.Entry(client).Collection(c => c.Flights).Load();
            Console.WriteLine($"Client {client?.Name} . Count flights : {client?.Flights.Count}");

            foreach(var f in client.Flights)
            {
                Console.WriteLine($"{f.DepartureCity} ====> {f.ArrivalCity}");
            }

            //if (client != null)
            //{
            //    context.Clients.Remove(client);
            //    context.SaveChanges();
            //}
            //foreach(var f in context.Flights)
            //{
            //    Console.WriteLine($"Flight : {f.Number}. From : {f.DepartureCity,15} to {f.ArrivalCity}  Rating : {f.Rating}");
            //}

        }
    }
}
