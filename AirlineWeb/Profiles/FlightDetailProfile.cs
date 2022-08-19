using AutoMapper;
using AirlineWeb.Models;
using AirlineWeb.Dtos;

namespace Airlineweb.Profiles{

    public class FlightDetailProfile : Profile
    {
        public FlightDetailProfile()
        {
            //Mapping source to target
            CreateMap<FlightDetailCreateDto, FlightDetail>();
            CreateMap<FlightDetailUpdateDto, FlightDetail>();
            CreateMap<FlightDetail, FlightDetailReadDto>();
        }
    }
}