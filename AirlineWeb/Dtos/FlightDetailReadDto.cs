using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineWeb.Dtos
{

    public class FlightDetailReadDto
    {
        public int Id { get; set; }

        public string FlightCode { get; set; }

        public decimal Price { get; set; }
    }
}