using System.Net.Http.Headers;
using System.Text.Json;
using AirlineSendAgent.Dtos;

namespace AirlineSendAgent.Client
{
    public class WebhookClient : IWebhookClient
    {
        private IHttpClientFactory _httpClientFatcory { get; }
        
        public WebhookClient(IHttpClientFactory httpClientFatcory)
        {
            _httpClientFatcory = httpClientFatcory;
        }

        public async Task SendWebhookNotification(FlightDetailChangePayloadDto flightDetailChangePayloadDto)
        {
            var serializedPayload = JsonSerializer.Serialize(flightDetailChangePayloadDto);
            var httpClient = _httpClientFatcory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, flightDetailChangePayloadDto.webhookURI);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("applicatiom/json"));

            request.Content = new StringContent(serializedPayload);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            try{
                using (var response = await httpClient.SendAsync(request))
                {
                    Console.WriteLine("Success");
                    response.EnsureSuccessStatusCode();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unsuccessful {ex.Message}");
            }
        }
    }
}