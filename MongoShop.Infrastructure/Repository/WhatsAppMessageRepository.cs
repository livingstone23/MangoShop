using MangoShop.Domain.interfaces;
using MangoShop.Domain.Models;
using MangoShop.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace MangoShop.Infraestructure.repository;



/// <summary>
/// Class that represents the repository for the WhatsApp messages.
/// </summary>
public class WhatsAppMessageRepository : Repository<WhatsAppMessage>, IWhatsAppMessageRepository
{


    public WhatsAppMessageRepository(MangoDbContext context, ILogger<Repository<WhatsAppMessage>> logger) : base(context, logger)
    {

    }
    

    /// <summary>
    /// This method is responsible for getting all messages by a specific message ID.
    /// </summary>
    /// <param name="messageId"></param>
    /// <returns></returns>
    public async Task<WhatsAppMessage> GetMessageById(string messageId)
    {
        
        try
        {
            
            WhatsAppMessage foundWhatsAppMessage = await _dbSet.FirstOrDefaultAsync(x => x.MessageId == messageId);
            return foundWhatsAppMessage;

        }
        catch (System.Exception)
        {
            _logger.LogError("An error occurred while trying to get the message by ID.");
            throw;
        }

    }

}