using System.ComponentModel.DataAnnotations;

namespace AirlineWeb.Dtos
{
    public class WebhookSubscriptionCreateDto
    {
        public string WebHookURI { get; set; }

        public string WebhookType { get; set; }   
    }
}