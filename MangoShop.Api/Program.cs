using MangoShop.Api.Services.WhatsappCloud.SendMessage;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MangoShop.Infraestructure.configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();


//Inject services
builder.Services.AddSingleton<IWhatsappCloudSendMessage, WhatsappCloudSendMessage>();


// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo { Title = "API Test con WhatsApp V1.0", Version = "v1" });

    //Enable Annotations
    opts.EnableAnnotations();

    // Configure XML documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opts.IncludeXmlComments(xmlPath);

});


///Extension method to register all the dependencies
builder.Services.AddAutoMapper(typeof(Program));

// Register the infrastructure dependencies
builder.Services.RegisterInfrastureDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
