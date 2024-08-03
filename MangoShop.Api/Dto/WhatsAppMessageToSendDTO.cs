using Newtonsoft.Json;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace MangoShop.Api.Dto;

public class WhatsAppMessageToSendDTO
{
    [JsonProperty("messaging_product")]
    public string MessagingProduct { get; set; } = "whatsapp";

    [JsonProperty("to")]
    public string To { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("template")]
    public Template Template { get; set; }
}


public class Template
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("language")]
    public Language Language { get; set; }

    [JsonProperty("components")]
    public List<Component> Components { get; set; }
}


public class Language
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("policy")]
    public string Policy { get; set; }
}

public class Component
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("parameters")]
    public List<Parameter> Parameters { get; set; }
}


public class Parameter
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

}


