using MangoShop.Api.Dto;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;



namespace MangoShop.Api.Services.WhatsappCloud.SendMessage;



/// <summary>
/// Class for send a message to WhatsApp Cloud
/// </summary>
public class WhatsappCloudSendMessage : IWhatsappCloudSendMessage
{

    private readonly IConfiguration _configuration;

    public WhatsappCloudSendMessage(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<WhatsAppResponseDTO> Execute(object model)
    {
        var client = new HttpClient();

        WhatsAppResponseDTO responseDto = new WhatsAppResponseDTO();

        var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

        using (var content = new ByteArrayContent(byteData))
        {
            string endpoint = "https://graph.facebook.com";
            string phoneNumberId = _configuration["WhatsApp:PhoneNumberId"];
            string accessToken = _configuration["WhatsApp:AccessToken"];
            string uri = $"{endpoint}/v20.0/{phoneNumberId}/messages";

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                // Read the response
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserealize the json response
                dynamic responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);




                responseDto.MessageId = responseObject.messages[0].id;
                responseDto.MessageStatus = responseObject.messages[0].message_status;
                responseDto.IsSuccess = true;


                return responseDto;
            }

            responseDto.IsSuccess = false;
            return responseDto;

        }
    }
}