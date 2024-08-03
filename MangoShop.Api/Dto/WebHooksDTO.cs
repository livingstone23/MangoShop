using System.Text.Json.Serialization;



namespace MangoShop.Api.Dto;



/// <summary>
/// Class for the WhatsApp webhook payload.
/// </summary>
public class WhatsAppWebhookPayload
{
    [JsonPropertyName("object")]
    public string Object { get; set; }

    [JsonPropertyName("entry")]
    public List<Entry> Entry { get; set; }
}

public class Entry
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("changes")]
    public List<Change> Changes { get; set; }
}

public class Change
{
    [JsonPropertyName("value")]
    public ChangeValue Value { get; set; }

    [JsonPropertyName("field")]
    public string Field { get; set; }
}

public class ChangeValue
{
    [JsonPropertyName("messaging_product")]
    public string MessagingProduct { get; set; }

    [JsonPropertyName("metadata")]
    public Metadata Metadata { get; set; }

    [JsonPropertyName("statuses")]
    public List<Status> Statuses { get; set; }
}

public class Metadata
{
    [JsonPropertyName("display_phone_number")]
    public string DisplayPhoneNumber { get; set; }

    [JsonPropertyName("phone_number_id")]
    public string PhoneNumberId { get; set; }
}

public class Status
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("status")]
    public string StatusValue { get; set; }

    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; }

    [JsonPropertyName("recipient_id")]
    public string RecipientId { get; set; }

    [JsonPropertyName("conversation")]
    public Conversation Conversation { get; set; }

    [JsonPropertyName("pricing")]
    public Pricing Pricing { get; set; }
}

public class Conversation
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("origin")]
    public Origin Origin { get; set; }
}

public class Origin
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class Pricing
{
    [JsonPropertyName("billable")]
    public bool Billable { get; set; }

    [JsonPropertyName("pricing_model")]
    public string PricingModel { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }
}