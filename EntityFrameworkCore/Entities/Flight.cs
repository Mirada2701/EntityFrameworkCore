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
        //Primary key naming : Id/id/ID //Entityname+Id = FlightId
        [Key]//Primary key
        public int Number { get; set; }
        [Required, MaxLength(100)]
        public string DepartureCity { get; set; }
        [Required, MaxLength(100)]
        public string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        //Relational type : one to many(1....*)
        //Foreign key naming : RelatedEntityName + RelatedEntityPrimaryKey
        public int AirplaneId { get; set; }//foreign key
        public Airplane Airplane { get; set; }//null
                                              //Relational type : many to many(*....*)

        //Navigation properties
        public ICollection<Client> Clients { get; set; }

    }
}
