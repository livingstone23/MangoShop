using AutoMapper;
using MangoShop.Api.Dto;
using MangoShop.Domain.Models;



namespace MangoShop.Api.Configuration;



/// <summary>
/// Configuration class for AutoMapper profiles. 
/// This class defines the mappings between domain entities and their corresponding DTOs (Data Transfer Objects).
/// </summary>
public class AutoMapperConfig : Profile
{

    /// <summary>
    /// Constructor of AutoMapperConfig
    /// </summary>
    public AutoMapperConfig()
    {

        CreateMap<WhatsAppMessage, WhatsAppMessageDTO>();
        CreateMap<WhatsAppMessageDTO, WhatsAppMessage>();

    }


}