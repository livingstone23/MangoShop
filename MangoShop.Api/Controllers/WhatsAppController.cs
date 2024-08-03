using MangoShop.Api.Dto;
using MangoShop.Api.Services.WhatsappCloud.SendMessage;
using MangoShop.Domain.interfaces;
using MangoShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;



namespace MangoShop.Api.Controllers;



/// <summary>
/// Controller for the WhatsApp messages.
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class WhatsAppController : ControllerBase
{


    private readonly IWhatsappCloudSendMessage _whatsappCloudSendMessage;

    private readonly IWhatsAppMessageRepository _whatsAppMessageRepository;

    private readonly IConfiguration _configuration;

    private readonly ILogger<WhatsAppController> _logger;


    /// <summary>
    /// Constructor for the WhatsAppController class.
    /// </summary>
    /// <param name="whatsappCloudSendMessage"></param>
    /// <param name="whatsAppMessageRepository"></param>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    public WhatsAppController(IWhatsappCloudSendMessage whatsappCloudSendMessage,
                              IWhatsAppMessageRepository whatsAppMessageRepository, 
                              IConfiguration configuration, 
                              ILogger<WhatsAppController> logger)
    {
        _whatsappCloudSendMessage = whatsappCloudSendMessage;
        _whatsAppMessageRepository = whatsAppMessageRepository;
        _configuration = configuration;
        _logger = logger;
    }



    /// <summary>
    /// Method to send Message
    /// </summary>
    /// <remarks>
    /// Method to send Message to WhatsAppAPI
    /// </remarks>
    /// <response code="200">Success - JSON Array of BRPs</response>
    /// <response code="204">If no data exists but the request is otherwise valid</response>
    /// <response code="400">If validation failed for any reason</response>
    /// <response code="500">Server Error</response>
    [HttpPost]
    [Route("SendMessage")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WhatsAppMessageToSendDTO))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SendMessage([FromBody] WhatsAppMessageToSendDTO model)
    {

        try
        {

            WhatsAppResponseDTO whatsappResponse = await _whatsappCloudSendMessage.Execute(model);


            WhatsAppMessage newWhatsAppMessage = new WhatsAppMessage
            {

                PhoneTo = model.To,
                TemplanteNameUsed = model.Template.Name,
                PhoneFrom = _configuration["WhatsApp:PhoneNumber"],
                PhoneId = _configuration["WhatsApp:PhoneNumberId"],
                //Guardamos los parametros del mensaje
                MessageBody = JsonConvert.SerializeObject(model.Template.Components[0].Parameters),
                Created = DateTime.Now,
                Oui = Guid.NewGuid()
            };


            if (whatsappResponse.IsSuccess)
            {
                newWhatsAppMessage.MessageId = whatsappResponse.MessageId;
                newWhatsAppMessage.SendingAt = true;
                newWhatsAppMessage.SendingDate = DateTime.Now;

            }
            else
            {
                newWhatsAppMessage.FailedAt = true;
                newWhatsAppMessage.FailedDate = DateTime.Now;
            }


            await _whatsAppMessageRepository.Add(newWhatsAppMessage);

            string notification = $" Desde SendNotification El MessageId es: {whatsappResponse.MessageId}";

            var resultMail = SendEmail(notification);

            if (whatsappResponse.IsSuccess)
            {
                return Ok("EVENT_RECEIVED");
            }

            return Ok("EVENT_RECEIVED");

        }
        catch (Exception e)
        {
            return Ok("EVENT_RECEIVED");
        }
    }



    /// <summary>
    /// Method to receive Webhook from WhatsAppAPI
    /// </summary>
    /// <remarks>
    /// This method is used to receive the webhook from WhatsAppAPI
    /// </remarks>
    /// <response code="200">Success - JSON Array of BRPs</response>
    /// <response code="204">If no data exists but the request is otherwise valid</response>
    /// <response code="400">If validation failed for any reason</response>
    /// <response code="500">Server Error</response>
    [HttpPost]
    [Route("ReceiveWebhook")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WhatsAppWebhookPayload))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ReceiveWebhook([FromBody] WhatsAppWebhookPayload payload)
    {
        try
        {
            //Validamos que el payload no sea nulo
            if (payload == null || payload.Entry == null || payload.Entry.Count == 0)
            {
                return BadRequest("Invalid webhook payload.");
            }

            foreach (var entry in payload.Entry)
            {
                foreach (var change in entry.Changes)
                {
                    foreach (var status in change.Value.Statuses)
                    {

                        string notification = $" Desde Webhook El MessageId es: {status.Id} y el status es {status.StatusValue}";

                        var resultMail = SendEmail(notification);


                        var existingMessage = await _whatsAppMessageRepository.GetMessageById(status.Id);
                        if (existingMessage != null)
                        {
                            switch (status.StatusValue)
                            {
                                case "delivered":
                                    existingMessage.DeliveredAt = true;
                                    existingMessage.ReadedDateRegister = DateTime.Now;
                                    break;
                                case "read":
                                    existingMessage.ReadedAt = true;
                                    existingMessage.ReadedDateRegister = DateTime.Now;
                                    break;
                                case "failed":
                                    existingMessage.FailedAt = true;
                                    existingMessage.FailedDateRegister = DateTime.Now;
                                    break;
                                    // Puedes agregar más casos aquí si es necesario
                            }

                            existingMessage.Updated = DateTime.Now;
                            await _whatsAppMessageRepository.Update(existingMessage);
                        }
                    }
                }
            }




            return Ok("EVENT_RECEIVED");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing webhook");
            //return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            return Ok("EVENT_RECEIVED");
        }
    }



    /// <summary>
    /// Method to verify the token
    /// </summary>
    /// <remarks>
    /// Method to verify the token
    /// </remarks>
    /// <response code="200">Success - JSON Array of BRPs</response>
    /// <response code="204">If no data exists but the request is otherwise valid</response>
    /// <response code="400">If validation failed for any reason</response>
    /// <response code="500">Server Error</response>
    [HttpGet]
    [Route("VerifyToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult VerifyToken()
    {
        string AcessToken = "SAHKDSDTTAEFE232256456EWRE43";

        //Permite que o Facebook valide token
        var token = Request.Query["hub.verify_token"].ToString();

        //Capturamos codigo de verificacion generado por facebook
        var challenge = Request.Query["hub.challenge"].ToString();

        if (challenge != null && token != null && token == AcessToken)
        {
            return Ok(challenge);
        }
        else
        {
            return BadRequest();
        }

    }
    


    /// <summary>
    /// Method to get the message by its ID
    /// </summary>
    /// <remarks>
    /// Method to get the message by Id created by WhatsAppAPI
    /// </remarks>
    /// <response code="200">Success - JSON Array of BRPs</response>
    /// <response code="204">If no data exists but the request is otherwise valid</response>
    /// <response code="400">If validation failed for any reason</response>
    /// <response code="500">Server Error</response>
    [HttpGet]
    [Route("GetMessageById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMessageById(string messageId)
    {
        try
        {
            var message = await _whatsAppMessageRepository.GetMessageById(messageId);

            if (message != null)
            {
                return Ok(message);
            }

            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    

    
    /// <summary>
    /// metodo privado para envio de correo electronico
    /// si se envio correctamente retorna true de lo contrario false
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    private async Task<bool> SendEmail(string notification)
    {
        try
        {

            ///Objtengo sendgrid api key desde el archivo del appsettings.json
            var apiKey = _configuration["SendGrid:ApiKey"];
            

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("livingstone23@gmail.com", "Livingstone");
            var subject = "Asunto de correo : " + notification;
            var to = new EmailAddress("livingstone23@gmail.com", "Livingstone");
            var plainTextContent = "This is a test email sent from SendGrid.";
            var htmlContent = $"<strong> {notification} </strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

            if (response.Headers.TryGetValues("X-Message-Id", out var messageIds))
            {
                var messageId = messageIds.FirstOrDefault();
                return true; // Retorna el MessageId para su uso posterior
            }

            return true;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al enviar correo electrónico");
            return false;
        }
    }


    
}

