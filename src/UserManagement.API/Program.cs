using UserManagement.API.Configuration;
using UserManagement.Application;
using UserManagement.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder);
builder.Services.AddInfraData(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("default");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();