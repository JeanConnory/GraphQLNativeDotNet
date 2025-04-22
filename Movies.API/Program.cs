using GraphQL;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.GraphQL;
using Movies.API.GraphQL.Mutations;
using Movies.API.GraphQL.Queries;
using Movies.API.GraphQL.Subscriptions;
using Movies.API.GraphQL.Subscriptions.Messages;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Movies");
builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

builder.Services.AddSingleton<MovieQuery>();
builder.Services.AddSingleton<MovieMutation>();
builder.Services.AddSingleton<MovieSubscription>();
builder.Services.AddSingleton<MovieMessage>();
builder.Services.AddSingleton<MovieSchema>();

builder.Services.AddGraphQL(options => options.AddGraphTypes().AddSystemTextJson().AddDataLoader()
    .AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseWebSockets();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGraphQL<MovieSchema>();

app.UseHttpsRedirection();

app.UseGraphQLAltair();

app.UseAuthorization();

app.MapControllers();

app.Run();
