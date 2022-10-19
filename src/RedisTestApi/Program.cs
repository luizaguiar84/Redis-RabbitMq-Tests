using RedisTestApi.Infra;
using RedisTestApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.InstanceName = builder.Configuration["Redis:InstanceName"];//"TestRedis";
    options.Configuration = builder.Configuration["Redis:Configuration"]; //"localhost:6379";
});

builder.Services.AddTransient<IArticlesRepository, ArticlesMockRepository>();
builder.Services.AddTransient<IRedisRepository, RedisMockRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



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
