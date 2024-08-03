namespace MangoShop.Api.Dto;



/// <summary>
/// Class for represent a WhatsApp response
/// </summary>
public class WhatsAppResponseDTO
{

    public string MessageId { get; set; }

    public string MessageStatus { get; set; }

    public bool IsSuccess { get; set; } = false;

}