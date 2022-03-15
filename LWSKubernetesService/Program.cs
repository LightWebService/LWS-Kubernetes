using LWSKubernetesService.Configuration;
using LWSKubernetesService.Repository;
using LWSKubernetesService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add Configuration
var mongoSection = builder.Configuration.GetSection("MongoSection").Get<MongoConfiguration>();
builder.Services.AddSingleton(mongoSection);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Singleton(Mostly Data Logic)
builder.Services.AddSingleton<IKubernetesRepository, KubernetesRepository>();
builder.Services.AddSingleton<MongoContext>();
builder.Services.AddSingleton<IDeploymentRepository, DeploymentRepository>();
builder.Services.AddSingleton<KubernetesService>();

// Service
builder.Services.AddHostedService<KafkaConsumerService>();

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