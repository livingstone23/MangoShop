using MangoShop.Domain.interfaces;
using MangoShop.Domain.Models;
using Microsoft.Extensions.Logging;



namespace MangoShop.Domain.services;



/// <summary>
/// Class for the WhatsAppMessage service that implements the IWhatsAppMessageService interface.
/// </summary>
public class WhatsAppMessageService : IWhatsAppMessageService
{


    private readonly IWhatsAppMessageRepository _whatsAppMessageRepository;
    private readonly ILogger<WhatsAppMessageService> _logger;


    /// <summary>
    /// Constructor for the WhatsAppMessageService class.
    /// </summary>
    /// <param name="whatsAppMessageRepository"></param>
    /// <param name="logger"></param>
    public WhatsAppMessageService(IWhatsAppMessageRepository whatsAppMessageRepository, ILogger<WhatsAppMessageService> logger)
    {
        _whatsAppMessageRepository = whatsAppMessageRepository;
        _logger = logger;
    }


    /// <summary>
    /// Method to add a new message to the repository.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<WhatsAppMessage> Add(WhatsAppMessage message)
    {
        try
        {
            await _whatsAppMessageRepository.Add(message);
            return message;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while adding a new message.");
            throw;
        }
    }
    
    
    /// <summary>
    /// Method to get an specific message by its ID.
    /// </summary>
    /// <param name="messageId"></param>
    /// <returns></returns>
    public async Task<WhatsAppMessage> GetMessageById(string messageId)
    {
        try
        {
            var foundWhatsAppMessage = await _whatsAppMessageRepository.GetMessageById(messageId);
            return foundWhatsAppMessage;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting a message by its ID.");
            throw;
        }
    }

}