using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AirlineWeb.MessageBus;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public FlightsController(AirlineDbContext context, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _context = context;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }

        [HttpGet("{flightCode}", Name = "GetFlightDetailsByCode")]
        public ActionResult<FlightDetailReadDto> GetFlightDetailsByCode(string flightCode)
        {
            var flight = _context.flightDetails.FirstOrDefault(x => x.FlightCode == flightCode);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FlightDetailReadDto>(flight));
        }

        [HttpPost(Name = "CreateFlight")]
        public ActionResult<FlightDetailReadDto> CreateFlight(FlightDetailCreateDto flightDetailCreateDto)
        {
            var flight = _context.flightDetails.FirstOrDefault(x => x.FlightCode == flightDetailCreateDto.FlightCode);

            if (flight == null)
            {
                flight = _mapper.Map<FlightDetail>(flightDetailCreateDto);

                try
                {
                    _context.flightDetails.Add(flight);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

                //Subscription was used as an input to create the webhooksubscription the database.
                //That means that by default the subscription variable will have been updated with the 
                //data from the database, for example the Id.
                var flightReadDto = _mapper.Map<FlightDetailReadDto>(flight);

                return CreatedAtRoute(nameof(GetFlightDetailsByCode), new {flightCode = flightDetailCreateDto.FlightCode}, flightReadDto);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpPut("{id}")]
        public ActionResult UpdateFlightDetail(int id, FlightDetailUpdateDto flightDetailUpdateDto)
        {
            var flight = _context.flightDetails.FirstOrDefault(f => f.Id == id);

            if(flight == null)
            {
                return NotFound();
            }

            decimal oldPrice = flight.Price;

            _mapper.Map(flightDetailUpdateDto, flight);

            try
            {
                _context.SaveChanges();
                if(oldPrice != flight.Price)
                {
                    Console.WriteLine("Price changed - placing message onto bus");
                    var message = new NotificationMessageDto{
                          WebhookType = "pricechange",
                          OldPrice = oldPrice,
                          NewPrice = flight.Price,
                          FlightCode = flight.FlightCode  
                    };

                    _messageBusClient.SendMessage(message);
                }
                else
                {
                       Console.WriteLine("No price change"); 
                }
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

        }

    }
}