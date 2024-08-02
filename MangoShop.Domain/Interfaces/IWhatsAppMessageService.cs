using MangoShop.Domain.Models;



namespace MangoShop.Domain.interfaces;



/// <summary>
/// Interface for the WhatsAppMessage service.
/// </summary>
public interface IWhatsAppMessageService
{

    /// <summary>
    /// Method to add a new message to the repository. 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task<WhatsAppMessage> Add (WhatsAppMessage message);

    /// <summary>
    /// Method to get an specific message by its ID.
    /// </summary>
    /// <returns></returns>
    Task<WhatsAppMessage> GetMessageById(string messageId);

}
