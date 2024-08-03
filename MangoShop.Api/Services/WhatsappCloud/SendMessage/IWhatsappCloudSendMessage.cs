using MangoShop.Api.Dto;



namespace MangoShop.Api.Services.WhatsappCloud.SendMessage;



/// <summary>
/// Interface for send a message to WhatsApp Cloud
/// </summary>
public interface IWhatsappCloudSendMessage
{

    Task<WhatsAppResponseDTO> Execute(object model);
    
}