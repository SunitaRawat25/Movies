using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Movies.Authentication;
using Movies.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MoviesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesContext") ?? throw new InvalidOperationException("Connection string 'MoviesContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
	c.AddSecurityDefinition("AdminApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
    Description	="API for Admin Access",
	Type =Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
	Name= "X-Api-Key",
	In=Microsoft.OpenApi.Models.ParameterLocation.Header,
	Scheme="ApiKeyScheme"
	});
	var scheme = new OpenApiSecurityScheme
	{
		Reference= new OpenApiReference
		{
			Type=ReferenceType.SecurityScheme,
			Id= "AdminApiKey"
		},
		In= Microsoft.OpenApi.Models.ParameterLocation.Header
	};
	var requirement = new OpenApiSecurityRequirement
	{
		{ scheme,new List<string>()}
	};
	c.AddSecurityRequirement(requirement);
});
builder.Services.AddScoped<AdminAPIKeyAuthentication>();
builder.Services.AddHttpClient("omdb",httpclient=>
{
	httpclient.BaseAddress = new Uri("http://www.omdbapi.com/");

});	

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
