using System.ComponentModel.DataAnnotations;



namespace MangoShop.Domain.Models;



/// <summary>
/// Class for represent a WhatsApp message to send
/// </summary>
public class WhatsAppMessage : Entity
{ 

    /// <summary>
    /// Field for representa an internal unique identifier for the message
    /// </summary>
    /// <value></value>
    public Guid Oui { get; set; }

    /// <summary>
    /// Field for represent the message to send
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
    public string PhoneTo { get; set; }

    /// <summary>
    /// Field for represent the template name used for the message
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 20)]
    public string TemplanteNameUsed { get; set; }

    /// <summary>
    /// Field for represent the body of the message send
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 20)]
    public string? MessageBody { get; set; }

    /// <summary>
    /// Field for represent the message id of the message send
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
    public string MessageId { get; set; }

    /// <summary>
    /// Field for represent the phone number will receive the message
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
    public string PhoneFrom { get; set; }

    /// <summary>
    /// Field that represent the phone id of the phone that will used to send the message
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
    public string? PhoneId { get; set; }



    ///Group of fields for control the message status
    
    /// <summary>
    /// Field for represent if the message was sent
    /// </summary>
    /// <value></value>
    public bool? SendingAt { get; set; } = false;

    /// <summary>
    /// Field for represent the date when the message was sent
    /// </summary>
    /// <value></value>
    public DateTime? SendingDate { get; set; }
    
    /// <summary>
    /// Field for represent if the message was delivered
    /// </summary>
    /// <value></value>
    public bool? DeliveredAt { get; set; } = false;

    /// <summary>
    /// Field for represente whe was register the delivered
    /// </summary>
    /// <value></value>
    public DateTime? DeliveredDateConfirm { get; set; }

    /// <summary>
    /// Field that represente the real date when the message was delivered to the client
    /// </summary>
    /// <value></value>
    public DateTime? DeliveredDateRegister { get; set; }

    /// <summary>
    /// Field for represent if the message was readed
    /// </summary>
    /// <value></value>
    public bool? ReadedAt { get; set; } = false;

    /// <summary>
    /// Field for represent the register of the Raeded date
    /// </summary>
    /// <value></value>
    public DateTime? ReadedDate { get; set; }

    /// <summary>
    /// Field for represent the real date when the message was readed by the client
    /// </summary>
    /// <value></value>
    public DateTime? ReadedDateRegister { get; set; }

    /// <summary>
    /// Field for represent if the message was failed
    /// </summary>
    /// <value></value>
    public bool? FailedAt { get; set; } = false;

    /// <summary>
    /// Field for represent the date when is register the faild 
    /// </summary>
    /// <value></value>
    public DateTime? FailedDate { get; set; }

    /// <summary>
    /// Field for represent the real date when the message was failed
    /// </summary>
    /// <value></value>
    public DateTime? FailedDateRegister { get; set; }


}