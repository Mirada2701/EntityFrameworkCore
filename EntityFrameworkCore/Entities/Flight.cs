using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Entities
{
    public class Flight
    {
        public int Number { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int? Rating { get; set; }
        public int AirplaneId { get; set; }
        public Airplane Airplane { get; set; }
        public ICollection<Client> Clients { get; set; }

    }
}
