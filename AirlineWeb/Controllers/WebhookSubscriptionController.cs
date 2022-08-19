using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineWeb.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WebhookSubscriptionController : ControllerBase
    {
        private AirlineDbContext _context;
        private IMapper _mapper;

        public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{secret}", Name = "GetSubscriptionBySecret")]
        public ActionResult<WebhookSubscriptionReadDto> GetSubscriptionBySecret(string secret)
        {
            var subscription = _context.webhookSubscriptions.FirstOrDefault(x => x.Secret == secret);

            if (subscription == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WebhookSubscriptionReadDto>(subscription));
        }

        [HttpPost(Name = "CreateSubscription")]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookSubcriptionCreateDto)
        {
            var subscription = _context.webhookSubscriptions.FirstOrDefault(x => x.WebHookURI == webhookSubcriptionCreateDto.WebHookURI);

            if (subscription == null)
            {
                subscription = _mapper.Map<WebhookSubscription>(webhookSubcriptionCreateDto);

                subscription.Secret = Guid.NewGuid().ToString();
                subscription.WebhookPublisher = "PanAus"; //Usually you would not hardcode this but get it from config

                try
                {
                    _context.webhookSubscriptions.Add(subscription);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

                //Subscription was used as an input to create the webhooksubscription the database.
                //That means that by default the subscription variable will have been updated with the 
                //data from the database, for example the Id.
                var webhookSubscriptionReadDto = _mapper.Map<WebhookSubscriptionReadDto>(subscription);

                return CreatedAtRoute(nameof(GetSubscriptionBySecret), new {secret = webhookSubscriptionReadDto.Secret}, webhookSubscriptionReadDto);
            }
            else
            {
                return NoContent();
            }

        }

    }
}