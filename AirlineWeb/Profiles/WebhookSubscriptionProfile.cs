using AutoMapper;
using AirlineWeb.Models;
using AirlineWeb.Dtos;

namespace Airlineweb.Profiles{

    public class WebhookSubscriptionProfile : Profile
    {
        public WebhookSubscriptionProfile()
        {
            //Mapping source to target
            CreateMap<WebhookSubscriptionCreateDto, WebhookSubscription>();
            CreateMap<WebhookSubscription, WebhookSubscriptionReadDto>();
        }
    }
}