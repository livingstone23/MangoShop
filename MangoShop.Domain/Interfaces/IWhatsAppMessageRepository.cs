using MangoShop.Domain.Models;



namespace MangoShop.Domain.interfaces;



/// <summary>
/// Interface for the WhatsAppMessage repository.
/// </summary>
public interface IWhatsAppMessageRepository : IRepository<WhatsAppMessage>
{


    /// <summary>
    /// Method to get all messages by a specific message ID.
    /// </summary>
    /// <param name="messageId"></param>
    /// <returns></returns>
    Task<WhatsAppMessage> GetMessageById(string messageId);


}