using RedisTestApi.Infra;
using RedisTestApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

if (builder.Configuration.GetValue<bool>("UseMock"))
{
    builder.Services.AddTransient<IArticlesRepository, ArticlesMockRepository>();
    builder.Services.AddTransient<IRedisRepository, RedisMockRepository>();
}
else
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.InstanceName = builder.Configuration["Redis:InstanceName"];
        options.Configuration = $"{builder.Configuration["Redis:Configuration"]}";
        options.Configuration += $",password={builder.Configuration["Redis:Password"]}";
    });   

    builder.Services.AddTransient<IArticlesRepository, ArticlesRepository>();
    builder.Services.AddTransient<IRedisRepository, RedisRepository>();

}

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
